using UnityEngine;
using System.Collections;

public class saleCanasta : MonoBehaviour
{
    [SerializeField]
    private AudioClip basketClothClip;

    private canasta basket;

    private void Start()
    {
        this.basket = gameObject.GetComponentInParent<canasta>();
    }

    void OnTriggerEnter(Collider obj)
    {
        float randomPitch = UnityEngine.Random.Range(0.96f, 1.04f);
        AudioManager.Instance.PlaySoundEffect(this.basketClothClip, randomPitch);

        if (obj.gameObject.tag == "balon")
        {
            basket.Puntuar(1);
        }
        else if (obj.gameObject.tag == "balonTriple")
        {
            basket.Puntuar(2);
        }
        else if (obj.gameObject.tag == "balonTicket")
        {
            basket.Puntuar(3);
        }
        else if (obj.gameObject.tag == "balonTiempo")
        {
            basket.Puntuar(4);
        }
    }

}