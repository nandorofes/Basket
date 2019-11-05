using Extensions.System.SequentialArray;
using Extensions.UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine;

public class SkinsBuyPanel : MonoBehaviour
{
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Atributos
    // ---- ---- ---- ---- ---- ---- ---- ----
    [SerializeField]
    private Transform tabHolder;
    [SerializeField]
    private Transform[] tabList;

    [SerializeField]
    private SkinBuyItemController skinBuyItemPrefab;
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

    // Caché de controles
    private List<SkinBuyItemController> skinBuyItemControlList;

	public Transform lastTab;

    // ---- ---- ---- ---- ---- ---- ---- ----
    // Métodos
    // ---- ---- ---- ---- ---- ---- ---- ----
    private void Awake()
    {
        this.skinBuyItemControlList = new List<SkinBuyItemController>();

        IList<SkinDefinition> skinList = GameManager.Instance.SkinDefinitionList;

        foreach (var item in skinList)
        {
            if (item.Basic)
                continue;

            string skinItemKey = item.Key;

            // Crear y añadir ítem
            SkinBuyItemController instance = SkinBuyItemController.Instantiate<SkinBuyItemController>(this.skinBuyItemPrefab);
            instance.ReferencedSkinKey = item.Key;
            instance.SetAmount(item.Price);
            instance.SetAcquiredState(GameManager.Instance.GamePersistentData.AcquiredSkinsList.Contains(skinItemKey));
            if (item.Price == 0) instance.SetAcquiredState(true);
            instance.SetIcon(item.Icon);

            this.skinBuyItemControlList.Add(instance);
        }

        // Añadir a la jerarquía
        this.skinBuyItemControlList.Sort();
        foreach (SkinBuyItemController instance in this.skinBuyItemControlList)
        {
            // Obtener definicion de skin
            var referencedSkin = GameManager.Instance.GetSkinInfo(instance.ReferencedSkinKey);

            // Obtener contenedor
            Transform holder = this.normalBallsHolder;
            switch (referencedSkin.Type)
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

            // Mover la instancia dentro del contenedor
            instance.transform.SetParent(holder, false);

            // Crear y añadir separador
            //var separatorInstance = GameObject.Instantiate(this.skinBuySeparatorPrefab);
            //separatorInstance.transform.SetParent(holder, false);
        }

        // Eliminar el separador final de cada jerarquía de objetos
        //GameObject.Destroy(normalBallsHolder.LastChild().gameObject);
        //GameObject.Destroy(timeBallsHolder.LastChild().gameObject);
        //GameObject.Destroy(tripleBallHolder.LastChild().gameObject);
        //GameObject.Destroy(bonusBallHolder.LastChild().gameObject);

        // Actualizar controles
        this.UpdateControls();
    }

    public void UpdateControls()
    {
        foreach (SkinBuyItemController item in this.skinBuyItemControlList)
            item.SetAcquiredState(GameManager.Instance.GamePersistentData.AcquiredSkinsList.Contains(item.ReferencedSkinKey));
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
    }

    public void Show()
    {
        this.gameObject.SetActive(true);
        this.UpdateControls();
    }

}