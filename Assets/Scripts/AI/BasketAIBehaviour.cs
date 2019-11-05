using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class BasketAIBehaviour : AIBehaviour
{
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Atributos
    // ---- ---- ---- ---- ---- ---- ---- ----
    private int playerPoints = 0;
    private int myPoints = 0;

    // ---- ---- ---- ---- ---- ---- ---- ----
    // Propiedades
    // ---- ---- ---- ---- ---- ---- ---- ----
    public int PlayerPoints
    {
        get { return this.playerPoints; }
        set { this.playerPoints = value; }
    }

    public int MyPoints
    {
        get { return this.myPoints; }
        set { this.myPoints = value; }
    }

    // ---- ---- ---- ---- ---- ---- ---- ----
    // Métodos
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Métodos de AIBehaviour
//    protected override float CalculateAdvantage()
//    {
//        float advantage = this.myPoints - this.playerPoints;
//        return this.SigmoidFunction(0.25f, 0.0f, advantage);
//    }

}