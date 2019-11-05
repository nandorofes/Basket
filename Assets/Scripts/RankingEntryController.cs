using Extensions.System.Numeric;
using Extensions.System.String;
using System;
using UnityEngine;
using UnityEngine.UI;

public class RankingEntryController : MonoBehaviour
{
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Atributos
    // ---- ---- ---- ---- ---- ---- ---- ----
    [SerializeField]
    private RawImage profileImage;
    [SerializeField]
    private Text userNameText;
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Text positionText;

    private string referencedId;

    // ---- ---- ---- ---- ---- ---- ---- ----
    // Atributos
    // ---- ---- ---- ---- ---- ---- ---- ----
    public string ReferencedId
    {
        get { return this.referencedId; }
    }

    // ---- ---- ---- ---- ---- ---- ---- ----
    // Métodos
    // ---- ---- ---- ---- ---- ---- ---- ----
    public void SetProfilePicture(Texture2D texture2D)
    {
        this.profileImage.texture = texture2D;
    }

    public void SetUserName(string userName)
    {
        this.userNameText.text = userName.Truncate(80);
    }

    public void SetScore(int score)
    {
        this.scoreText.text = string.Format("{0}", score.ClampToPositive());
    }

    public void SetPosition(int position)
    {
        this.positionText.text = string.Format("{0}", position.ClampTo(0, 99));
    }

    public void SetReferencedId(string referencedId)
    {
        this.referencedId = referencedId;
    }

}