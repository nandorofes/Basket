using Extensions.System.SequentialArray;
using Extensions.UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine;

public class SkinSelectionPanelController : MonoBehaviour
{
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Atributos
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Referencias
    [SerializeField]
    private Transform tabHolder;
    [SerializeField]
    private Transform[] tabList;

    [SerializeField]
    private SkinSelectionItem skinSelectionItem;
    [SerializeField]
    private SkinSelectionBuyShortcut SkinSelectionBuyShortcutPrefab;

    //[SerializeField]
    //private GameObject skinBuySeparatorPrefab;

    [SerializeField]
    private Transform normalBallsHolder;
    [SerializeField]
    private Transform timeBallsHolder;
    [SerializeField]
    private Transform tripleBallHolder;
    [SerializeField]
    private Transform bonusBallHolder;

	public ScrollSnapRect [] scrollSnapRectSkins;

    // Caché de controles
    private List<SkinSelectionItem> skinSelectionItemControlList;

	public Transform lastTab;

    // ---- ---- ---- ---- ---- ---- ---- ----
    // Métodos
    // ---- ---- ---- ---- ---- ---- ---- ----
    private void Awake()
    {
        this.CreateControls();
    }

    private void CreateControls()
    {
        this.skinSelectionItemControlList = new List<SkinSelectionItem>();
        IList<SkinDefinition> skinList = GameManager.Instance.SkinDefinitionList;
		GameManager.Instance.SkinSelection = true;

        // Limpiar contenedores
        this.normalBallsHolder.DestroyChildren();
        this.timeBallsHolder.DestroyChildren();
        this.tripleBallHolder.DestroyChildren();
        this.bonusBallHolder.DestroyChildren();
        foreach (var item in skinList)
        {
            Transform holder = this.normalBallsHolder;
            switch (item.Type)
            {
                case SkinTypes.Time:
                    holder = this.timeBallsHolder;
                    break;
                case SkinTypes.Triple:
                    holder = this.tripleBallHolder;
                    break;
                case SkinTypes.Bonus:
                    holder = this.bonusBallHolder;
                    break;
            }
            string skinItemKey = item.Key;

            // Comprobar si la skin está adquirida
            if (GameManager.Instance.GamePersistentData.AcquiredSkinsList.Contains(skinItemKey) || item.Basic)
            {
                if (item.Basic)
                {
                    if (item.Type == SkinTypes.Time && !GameManager.Instance.GamePersistentData.BalonSegundoExtra)
                        continue;
                    if (item.Type == SkinTypes.Triple && !GameManager.Instance.GamePersistentData.BalonTriple)
                        continue;
                    if (item.Type == SkinTypes.Bonus && !GameManager.Instance.GamePersistentData.BalonTicketExtra)
                        continue;
                }

                // Crear y añadir ítem
                SkinSelectionItem instance = SkinSelectionItem.Instantiate<SkinSelectionItem>(this.skinSelectionItem);

                instance.ReferencedSkinKey = item.Key;
                bool selected = GameManager.Instance.GamePersistentData.SkinIsSelected(skinItemKey);
                instance.SetSelectedState(selected);
                instance.SetIcon(item.Icon);

                instance.transform.SetParent(holder, false);
                this.skinSelectionItemControlList.Add(instance);

                // Crear y añadir separador
                //GameObject separatorInstance = GameObject.Instantiate(this.skinBuySeparatorPrefab);
                //separatorInstance.transform.SetParent(holder, false);
            }
        }

        // Añadir atajo a la tienda al final de cada lista
        for (int i = 0; i < 4; i++)
        {
            Transform holder = this.normalBallsHolder;
            if (i == 1)
                holder = this.timeBallsHolder;
            if (i == 2)
                holder = this.tripleBallHolder;
            if (i == 3)
                holder = this.bonusBallHolder;
            
            SkinSelectionBuyShortcut bsInstance = SkinSelectionBuyShortcut.Instantiate<SkinSelectionBuyShortcut>(this.SkinSelectionBuyShortcutPrefab);
            bsInstance.transform.SetParent(holder, false);
            //GameObject separatorInstance = GameObject.Instantiate(this.skinBuySeparatorPrefab);
            //separatorInstance.transform.SetParent(holder, false);
        }

        // Eliminar el separador final de cada jerarquía de objetos
        //if (this.normalBallsHolder.childCount > 0)
        //    GameObject.Destroy(normalBallsHolder.LastChild().gameObject);
        //if (this.timeBallsHolder.childCount > 0)
        //    GameObject.Destroy(timeBallsHolder.LastChild().gameObject);
        //if (this.tripleBallHolder.childCount > 0)
        //    GameObject.Destroy(tripleBallHolder.LastChild().gameObject);
        //if (this.bonusBallHolder.childCount > 0)
        //    GameObject.Destroy(bonusBallHolder.LastChild().gameObject);
		foreach (ScrollSnapRect scroll in scrollSnapRectSkins) {
			scroll.CorreccionBugSkins ();
		}

        // Actualizar controles
        this.UpdateControls();
    }

    public void UpdateControls()
    {
		
        foreach (SkinSelectionItem item in this.skinSelectionItemControlList)
        {
            item.SetSelectedState(false);
			if (GameManager.Instance.GamePersistentData.SkinIsSelected (item.ReferencedSkinKey)) {
				item.SetSelectedState (true);
			}
        }
    }

    public void SetActiveTab(int tabNumber)
    {
        if (tabList.IndexInRange(tabNumber))
        {
            Transform selectedTab = this.tabList[tabNumber];
            selectedTab.SetAsLastSibling();
			lastTab = selectedTab;
        }
    }

    public void Hide()
    {
        this.gameObject.SetActive(false);
		GameManager.Instance.SkinSelection = false;
    }

    public void Show()
    {
        this.gameObject.SetActive(true);
        this.CreateControls();
        this.UpdateControls();
    }



}