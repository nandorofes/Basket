using Extensions.System.Numeric;
using Extensions.System.String;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GamePersistentData
{
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Atributos
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Estado
    [SerializeField]
    private int tickets = 0;
    [SerializeField]
    private bool freeCreditsPurchased = false;
    [SerializeField]
    private int credits = 0;
    [SerializeField]
    private int numMaquina = 0;
	[SerializeField]
	private bool tutorial = false;

    // Estadísticas
    [SerializeField]
    private float totalAttempts = 0;
    [SerializeField]
    private float totalMade = 0;
    [SerializeField]
    private int matchRecordBullZone = 0;
    [SerializeField]
	private int matchRecordDroneZone = 0;
    [SerializeField]
	private int matchRecordPlanetZone = 0;
	[SerializeField]
	private int matchRecordPulpo = 0;
	[SerializeField]
	private int matchRecordTiburon = 0;


    // Estadísticas internas
    [SerializeField]
    private int totalMatches = 0;
    [SerializeField]
    private int totalPoints = 0;

    //Bolas
    [SerializeField]
    private bool normal = false;
    [SerializeField]
    private bool tresPuntos = false;
    [SerializeField]
    private bool segundoExtra = false;
    [SerializeField]
    private bool tiketExtra = false;

    //Trofeos
    [SerializeField]
    private bool trofeo1 = false;
    [SerializeField]
    private bool trofeo2 = false;
    [SerializeField]
    private bool trofeo3 = false;
    [SerializeField]
    private bool trofeo4 = false;
  
    // Skins
    [SerializeField]
    private List<string> acquiredSkinsList = new List<string>();
    [SerializeField]
    private List<string> selectedNormalSkinList = new List<string>(new string[] { "Skin.Normal" });
    [SerializeField]
    private string selectedTimeSkin = "Skin.Time";
    [SerializeField]
    private string selectedTripleSkin = "Skin.Triple";
    [SerializeField]
    private string selectedBonusSkin = "Skin.Bonus";

    // ---- ---- ---- ---- ---- ---- ---- ----
    // Propiedades
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Estado
    public int Tickets
    {
        get { return this.tickets; }
        internal set { this.tickets = value.ClampToPositive(); }
    }

    public int Credits
    {
        get { return this.credits; }
        internal set { this.credits = value.ClampToPositive(); }
    }

    public bool FreeCreditsPurchased
    {
        get { return this.freeCreditsPurchased; }
        internal set { this.freeCreditsPurchased = value; }
    }

    public int NumMaquina
    {
        get { return this.numMaquina; }
        internal set { this.numMaquina = value.ClampToPositive(); }
    }

	public bool Tutorial
	{
		get { return this.tutorial; }
		internal set { this.tutorial = value; }
	}

    // Estadísticas
    public float TotalAttempts
    {
        get { return this.totalAttempts; }
        internal set { this.totalAttempts = value.ClampToPositive(); }
    }

	public int MatchRecordBullZone
    {
		get { return this.matchRecordBullZone; }
		internal set { this.matchRecordBullZone = value.ClampToPositive(); }
    }

    public float TotalMade
    {
        get { return this.totalMade; }
        internal set { this.totalMade = value.ClampToPositive(); }
    }

	public int MatchRecordDroneZone
    {
		get { return this.matchRecordDroneZone; }
		internal set { this.matchRecordDroneZone = value.ClampToPositive(); }
    }

    public float SuccessRate
    {
        get { return this.totalAttempts > 0 ? ((float)this.totalMade) / ((float)this.totalAttempts) * 100.0f : 0.0f; }
    }

	public int MatchRecordPlanetZone
    {
		get { return this.matchRecordPlanetZone; }
		internal set { this.matchRecordPlanetZone = value.ClampToPositive(); }
    }

	public int MatchRecordTiburon
	{
		get { return this.matchRecordTiburon; }
		internal set { this.matchRecordTiburon = value.ClampToPositive(); }
	}

	public int MatchRecordPulpo
	{
		get { return this.matchRecordPulpo; }
		internal set { this.matchRecordPulpo = value.ClampToPositive(); }
	}

    // Estadísticas internas
    public int TotalMatches
    {
        get { return this.totalMatches; }
        set { this.totalMatches = value; }
    }

    public int TotalPoints
    {
        get { return this.totalPoints; }
        set { this.totalPoints = value; }
    }

    //Balones
    public bool BalonNormal
    {
        get { return this.normal; }
        set { this.normal = value; }
    }

    public bool BalonTriple
    {
        get { return this.tresPuntos; }
        set { this.tresPuntos = value; }
    }

    public bool BalonTicketExtra
    {
        get { return this.tiketExtra; }
        set { this.tiketExtra = value; }
    }

    public bool BalonSegundoExtra
    {
        get { return this.segundoExtra; }
        set { this.segundoExtra = value; }
    }

    // Trofeo
    public bool Escenario2
    {
        get { return this.trofeo1; }
        set { this.trofeo1 = value; }
    }

    public bool Escenario3
    {
        get { return this.trofeo2; }
        set { this.trofeo2 = value; }
    }

    public bool Escenario4
    {
        get { return this.trofeo3; }
        set { this.trofeo3 = value; }
    }

    public bool Escenario5
    {
        get { return this.trofeo4; }
        set { this.trofeo4 = value; }
    }

    public List<string> AcquiredSkinsList
    {
        get { return this.acquiredSkinsList; }
        set { this.acquiredSkinsList = value; }
    }

    public List<string> SelectedNormalSkinList
    {
        get { return this.selectedNormalSkinList; }
        set { this.selectedNormalSkinList = value; }
    }

    public string SelectedTimeSkin
    {
        get { return this.selectedTimeSkin; }
        set { this.selectedTimeSkin = value.Truncate(80); }
    }

    public string SelectedTripleSkin
    {
        get { return this.selectedTripleSkin; }
        set { this.selectedTripleSkin = value.Truncate(80); }
    }

    public string SelectedBonusSkin
    {
        get { return this.selectedBonusSkin; }
        set { this.selectedBonusSkin = value.Truncate(80); }
    }

    // ---- ---- ---- ---- ---- ---- ---- ----
    // Métodos
    // ---- ---- ---- ---- ---- ---- ---- ----
    public bool SkinIsSelected(string skinKey)
    {
        if (this.selectedNormalSkinList.Contains(skinKey))
            return true;
        if (this.selectedTimeSkin == skinKey)
            return true;
        if (this.selectedTripleSkin == skinKey)
            return true;
        if (this.selectedBonusSkin == skinKey)
            return true;
        
        return false;
    }

}