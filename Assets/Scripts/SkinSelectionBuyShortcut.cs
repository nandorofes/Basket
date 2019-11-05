using Extensions.System.Numeric;
using Extensions.System.String;
using System;
using UnityEngine;
using UnityEngine.UI;

public class SkinSelectionBuyShortcut : MonoBehaviour
{
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Eventos
    // ---- ---- ---- ---- ---- ---- ---- ----
    public static Action OnSkinSelectionBuyShortcutPressed = delegate { };

    // ---- ---- ---- ---- ---- ---- ---- ----
    // Métodos
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Manejadores de eventos de Unity
	public void ButtonPressed()
    {
        SkinSelectionBuyShortcut.OnSkinSelectionBuyShortcutPressed();
    }

}