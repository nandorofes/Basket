using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Marcador : MonoBehaviour
{
    public Text texttime;
    public Text targetpoints;
    public Text targettext;
    public Text pointspoints;
    public Text pointstext;
    public GameObject displayLightGroup;
    public Color white;
    public Color red;
    public Color frozenColor;
    public Color transparent;
    GameSceneManager sceneManager;

    // Use this for initialization
    void Start()
    {
        sceneManager = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<GameSceneManager>();
        displayLightGroup.SetActive(false);
        texttime.color = white;
        targetpoints.color = white;
        targettext.color = white;
        pointspoints.color = white;
        pointstext.color = white;
    }
	
    // Update is called once per frame
    void Update()
    {
        if (sceneManager.TimeFrozen)
        {
            texttime.color = frozenColor;
            targetpoints.color = frozenColor;
            targettext.color = frozenColor;
            pointspoints.color = frozenColor;
            pointstext.color = frozenColor;
            displayLightGroup.SetActive(true);
        }
        else
        {
            if (sceneManager.time >= 5.0f)
            {
                texttime.color = white;
                targetpoints.color = white;
                targettext.color = white;
                pointspoints.color = white;
                pointstext.color = white;
                displayLightGroup.SetActive(false);
            }
            if (sceneManager.time < 5.0f)
            {
                texttime.color = red;
                targetpoints.color = red;
                targettext.color = red;
                pointspoints.color = red;
                pointstext.color = red;
                displayLightGroup.SetActive(true);
            }
            if (sceneManager.time < 4.5f)
            {
                texttime.color = white;
                targetpoints.color = white;
                targettext.color = white;
                pointspoints.color = white;
                pointstext.color = white;
                displayLightGroup.SetActive(false);
            }
            if (sceneManager.time < 4.0f)
            {
                texttime.color = red;
                targetpoints.color = red;
                targettext.color = red;
                pointspoints.color = red;
                pointstext.color = red;
                displayLightGroup.SetActive(true);
            }
            if (sceneManager.time < 3.5f)
            {
                texttime.color = white;
                targetpoints.color = white;
                targettext.color = white;
                pointspoints.color = white;
                pointstext.color = white;
                displayLightGroup.SetActive(false);

            }
            if (sceneManager.time < 3.0f)
            {
                texttime.color = red;
                targetpoints.color = red;
                targettext.color = red;
                pointspoints.color = red;
                pointstext.color = red;
                displayLightGroup.SetActive(true);
            }
            if (sceneManager.time < 2.5)
            {
                texttime.color = white;
                targetpoints.color = white;
                targettext.color = white;
                pointspoints.color = white;
                pointstext.color = white;
                displayLightGroup.SetActive(false);

            }
            if (sceneManager.time < 2.0f)
            {
                texttime.color = red;
                targetpoints.color = red;
                targettext.color = red;
                pointspoints.color = red;
                pointstext.color = red;
                displayLightGroup.SetActive(true);
            }
            if (sceneManager.time < 1.5f)
            {
                texttime.color = white;
                targetpoints.color = white;
                targettext.color = white;
                pointspoints.color = white;
                pointstext.color = white;
                displayLightGroup.SetActive(false);

            }
            if (sceneManager.time < 1.0f)
            {
                texttime.color = red;
                targetpoints.color = red;
                targettext.color = red;
                pointspoints.color = red;
                pointstext.color = red;
                displayLightGroup.SetActive(true);

            }
            if (sceneManager.time < 0.5f)
            {
                texttime.color = white;
                targetpoints.color = white;
                targettext.color = white;
                pointspoints.color = white;
                pointstext.color = white;
                displayLightGroup.SetActive(false);

            }
        }
    }
}
