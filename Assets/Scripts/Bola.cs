using Extensions.System.Numeric;
using System;
using System.Collections;
using UnityEngine;

public class Bola : MonoBehaviour
{
    public float fuerzaZ;
    public float fuerzaY;
    public float fuerzaX;
    bool zonaLanzamiento;
    bool tirado = false;
    public GameObject aro;
    public float masa;
    public Rigidbody rb;
    GameSceneManager sceneManager;

    public AudioClip groundBounceSound;
    public AudioClip machineBounceSound;
    public AudioClip basketinSound;

    private AudioSource audioSource;

    private const float gravitationalConstant = 6.672e-11f;

    public static Vector3 GAcceleration(Vector3 position, float mass, Rigidbody r)
    {

        Vector3 direction = new Vector3(position.x - r.position.x, 0, position.z - r.position.z);
        //Vector3 direction=position-r.position;

        float gravityForce = gravitationalConstant * ((mass * r.mass) / direction.sqrMagnitude);
        gravityForce /= r.mass;

        return direction.normalized * gravityForce * Time.fixedDeltaTime;
    }

    // Use this for initialization
    void Start()
    {
        #if UNITY_EDITOR
        this.fuerzaZ = 250.0f;
        this.fuerzaY = 250.0f;
        #endif

        rb = this.GetComponent<Rigidbody>();
        sceneManager = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<GameSceneManager>();
        this.audioSource = this.GetComponent<AudioSource>();

        this.StartCoroutine(this.CheckLostStatus());
    }


    public void Chutar()
    {
		
        this.rb.AddForce(new Vector3(fuerzaX, fuerzaY, fuerzaZ));
        tirado = true;
        GameManager.Instance.GamePersistentData.TotalAttempts += 1;
        sceneManager.attempts += 1;

    }

    void FixedUpdate (){

		if (this.rb.IsSleeping ())
			this.rb.WakeUp ();

		//if (tirado && this.gameObject.transform.position.y>aro.transform.position.y) {
		//	rb.velocity += GAcceleration (aro.transform.position, masa, rb);

	}

    void OnCollisionEnter(Collision obj)
    {
        float volume = obj.relativeVelocity.sqrMagnitude * 0.1f;
        float pitch = 0.75f + (obj.relativeVelocity.sqrMagnitude * 0.066f);

        this.audioSource.volume = volume.ClampTo(0.0f, 1.0f);
        this.audioSource.pitch = pitch.ClampTo(0.8f, 1.2f);
        this.audioSource.Play();
    }

    void OnTriggerEnter(Collider obj)
    {

        if (obj.gameObject.tag == "canasta")
        {
            tirado = false;
            AudioManager.Instance.PlaySoundEffect(this.basketinSound);
        }
    }

    // Corrutinas
    private IEnumerator CheckLostStatus()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.3333f);
            if (this.transform.position.sqrMagnitude > 64.0f)
            {
                this.transform.position = new Vector3(0.0f, 1.15f, -2.5f);
                this.rb.velocity = Vector3.zero;
                this.rb.angularVelocity *= 0.1f;
            }
        }
    }

}