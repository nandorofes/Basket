using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanastaOnline : MonoBehaviour {

	entraCanastaOnline entra;
	public MultiplayerSceneManager scenemanager;
	[SerializeField]
	private AnimationCurve xMovementCurve = new AnimationCurve(new Keyframe(0.0f, 0.0f), new Keyframe(1.0f, 0.0f));

	[SerializeField]
	private ParticleSystem normalParticleSystem;
	[SerializeField]
	private ParticleSystem timeParticleSystem;
	[SerializeField]
	private ParticleSystem tripleParticleSystem;
	[SerializeField]
	private ParticleSystem bonusParticleSystem;

	[SerializeField]
	public AudioClip freezeSound;
	[SerializeField]
	public AudioClip tripleSound;
	[SerializeField]
	public AudioClip bonusSound;

	private TextPopupPanelController textPopupPanel;
	private CameraShake cameraShake;

	public PlayerMultiplayer player;

	void Start()
	{
		entra = gameObject.GetComponentInChildren<entraCanastaOnline>();
		xMovementCurve.preWrapMode = WrapMode.Loop;
		xMovementCurve.postWrapMode = WrapMode.Loop;

		this.textPopupPanel = GameObject.FindWithTag("Text popup panel").GetComponent<TextPopupPanelController>();
		cameraShake = Camera.main.GetComponent<CameraShake>();
	}

	public void Puntuar(int bola)
	{
		if (entra.entro && scenemanager.jugando)
		{
			switch (bola)
			{
			case 1:
				scenemanager.puntuacion += 2;
				scenemanager.made += 1;
				scenemanager.tickets += 1;
				GameManager.Instance.GamePersistentData.TotalMade += 1;
				entra.entro = false;
				this.normalParticleSystem.Play();
				this.cameraShake.Shake(0.04f, 6.0f, 0.25f);
				this.textPopupPanel.ShowPointsText("+2");
				break;

			case 2:
				scenemanager.puntuacion += 3;
				scenemanager.made += 1;
				scenemanager.tickets += 1;
				GameManager.Instance.GamePersistentData.TotalMade += 1;
				entra.entro = false;
				this.tripleParticleSystem.Play();
				AudioManager.Instance.PlaySoundEffect(this.tripleSound);
				this.cameraShake.Shake(0.1f, 9f, 0.50f);
				this.textPopupPanel.ShowPointsText("+3");
				this.textPopupPanel.ShowTriple();
				break;

			case 3:
				scenemanager.puntuacion += 2;
				scenemanager.made += 1;
				scenemanager.tickets += 2;
				GameManager.Instance.GamePersistentData.TotalMade += 1;
				entra.entro = false;
				this.bonusParticleSystem.Play();
				AudioManager.Instance.PlaySoundEffect(this.bonusSound);
				this.cameraShake.Shake(0.1f, 9f, 0.50f);
				this.textPopupPanel.ShowPointsText("+2");
				this.textPopupPanel.ShowBonus();
				break;

			case 4:
				scenemanager.puntuacion += 2;
				scenemanager.made += 1;
				scenemanager.tickets += 1;
				GameManager.Instance.GamePersistentData.TotalMade += 1;
				entra.entro = false;
				this.timeParticleSystem.Play();
				AudioManager.Instance.PlaySoundEffect(this.freezeSound);
				this.cameraShake.Shake(0.1f, 9f, 0.50f);
				this.textPopupPanel.ShowPointsText("+2");
				this.textPopupPanel.ShowFreeze();
				break;
			}
			//Debug.Log ("antes de llamar a CmdPointsChanged");
			if (player != null) {
				player.LlamarACmd ();
			}
			//Debug.Log ("DESPUES de llamar a CmdPointsChanged");
		}
	}

	void Update()
	{
		if (GameManager.Instance.GamePersistentData.NumMaquina == 0 && scenemanager.fase >= 2)
		{
			for (int i = 2; i <= scenemanager.fase; i++)
			{
				float velocidad = 0.05f * i;
				transform.position = new Vector3(xMovementCurve.Evaluate(Time.time * velocidad), transform.position.y, transform.position.z);
			}
		}
		if (GameManager.Instance.GamePersistentData.NumMaquina == 4 && scenemanager.fase >= 2)
		{
			for (int i = 2; i <= scenemanager.fase; i++)
			{
				float velocidad = 0.05f * i;
				float modificador = 1;
				if (this.gameObject.name == "ARO (1)") {
					modificador = -1;
				}
				transform.position = new Vector3(xMovementCurve.Evaluate(Time.time * velocidad)*modificador, transform.position.y, transform.position.z);
			}
		}
	}

}
