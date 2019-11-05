using Extensions.System.SequentialArray;
using System;
using System.Collections;
using UnityEngine;

public class MultiscreenMenuSystem : MonoBehaviour
{
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Atributos
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Públicos
    [SerializeField]
    private GameObject[] menuItemList;

    // Privados
    private int currentMenu = 0;
    private bool locked = false;

    private int nextMenu = 0;

    // ---- ---- ---- ---- ---- ---- ---- ----
    // Propiedades
    // ---- ---- ---- ---- ---- ---- ---- ----
    public int CurrentMenu
    {
        get { return this.currentMenu; }
    }

    public bool Locked
    {
        get { return this.locked; }
    }

    // ---- ---- ---- ---- ---- ---- ---- ----
    // Eventos
    // ---- ---- ---- ---- ---- ---- ---- ----
    public event Action<int> OnMenuChange = delegate { };

    // ---- ---- ---- ---- ---- ---- ---- ----
    // Métodos
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Métodos de MonoBehaviour
    private void Awake()
    {
        MultiscreenMenuItem currentMenuItem = this.GetMenuItem(this.currentMenu);

        /*if (currentMenuItem != null)
        {
            currentMenuItem.Show();
            currentMenuItem.OnHideEnd += this.OnCurrentMenuHideEnd;
        }
        else
        {
            this.CallNextMenu();
        }*/
    }

    public void SetMenu(int menuNumber)
    {
        int effectiveMenuNumber = this.menuItemList.ClampToValidIndex(menuNumber);
        if ((effectiveMenuNumber != this.currentMenu) && !this.locked)
        {
            // Indicar el bloqueo del sistema de menús mientras se hace la transición
            this.locked = true;

            // Hacer la transición, según el tipo de objeto
            MultiscreenMenuItem currentMenuItem = this.GetMenuItem(this.currentMenu);

            this.nextMenu = effectiveMenuNumber;
            if (currentMenuItem != null)
            {
                currentMenuItem.Hide();
                currentMenuItem.OnHideEnd += this.OnCurrentMenuHideEnd;
            }
            else
            {
                this.CallNextMenu();
            }
        }
    }

    public void SetMenuInstantly(int menuNumber)
    {
        int effectiveMenuNumber = this.menuItemList.ClampToValidIndex(menuNumber);
//        if (effectiveMenuNumber != this.currentMenu)
//        {
            // Indicar el desbloqueo del sistema de menús
            this.locked = false;

            // Hacer la transición, al instante
            this.menuItemList[this.currentMenu].SetActive(false);
            this.menuItemList[effectiveMenuNumber].SetActive(true);

            this.currentMenu = effectiveMenuNumber;
            this.OnMenuChange(this.currentMenu);

            // Llamar a la animación del segundo si la tuviere
            MultiscreenMenuItem nextMenuItem = this.GetMenuItem(this.currentMenu);
            if (nextMenuItem != null)
            {
                nextMenuItem.ShowInstantly();
            }
//        }

    }

    // Métodos auxiliares
    private MultiscreenMenuItem GetMenuItem(int menuNumber)
    {
        GameObject gameObject = this.menuItemList[menuNumber];
        return gameObject.GetComponent<MultiscreenMenuItem>();
    }

    private void CallNextMenu()
    {
        // Ocultar menu antiguo
        this.menuItemList[currentMenu].SetActive(false);

        // Asignar nuevo número de menú (y llamar al evento)
        this.currentMenu = this.nextMenu;
        this.OnMenuChange(this.currentMenu);

        // Mostrar nuevo objeto de menú e invocar eventos si es necesario
        GameObject nextMenuItemGO = this.menuItemList[this.nextMenu];
        MultiscreenMenuItem nextMenuItem = this.GetMenuItem(this.nextMenu);

        nextMenuItemGO.SetActive(true);
        if (nextMenuItem != null)
        {
            nextMenuItem.Show();
            nextMenuItem.OnShowEnd += this.OnNextMenuShowEnd;
        }
    }

    // Manejadores de eventos
    private void OnCurrentMenuHideEnd(MultiscreenMenuItem sender)
    {
        this.CallNextMenu();
        sender.OnHideEnd -= this.OnCurrentMenuHideEnd;
    }

    private void OnNextMenuShowEnd(MultiscreenMenuItem sender)
    {
        this.locked = false;
        sender.OnShowEnd -= this.OnNextMenuShowEnd;
    }

}