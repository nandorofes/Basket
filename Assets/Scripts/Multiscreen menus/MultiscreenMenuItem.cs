using System;
using UnityEngine;

public interface MultiscreenMenuItem
{
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Eventos
    // ---- ---- ---- ---- ---- ---- ---- ----
    event Action<MultiscreenMenuItem> OnHideEnd;
    event Action<MultiscreenMenuItem> OnShowEnd;

    // ---- ---- ---- ---- ---- ---- ---- ----
    // Métodos
    // ---- ---- ---- ---- ---- ---- ---- ----
    void Hide();
    void Show();
    void ShowInstantly();

}