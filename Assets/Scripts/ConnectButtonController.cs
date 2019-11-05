using System;
using UnityEngine;
using UnityEngine.UI;

public class ConnectButtonController : MonoBehaviour
{
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Atributos
    // ---- ---- ---- ---- ---- ---- ---- ----
    [SerializeField]
    private GameObject loginText;
    [SerializeField]
    private GameObject logoutText;
    [SerializeField]
    private GameObject proxilePictureObject;
	[SerializeField]
	private GameObject fForFacebook;
    [SerializeField]
    private RawImage profilePictureRawImage;

    // ---- ---- ---- ---- ---- ---- ---- ----
    // Métodos
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Métodos de MonoBehaviour
    private void Awake()
    {
        this.UpdateControls();

        FacebookManager.Instance.OnLoginSuccess += OnLoginSuccess;
        FacebookManager.Instance.OnRequestUserDataSuccess += OnRequestUserDataSuccess;
		FacebookManager.Instance.OnLogoutSuccess += OnLogOut;
    }

    private void OnDestroy()
    {
        FacebookManager facebookManager = FacebookManager.Instance;
        if (facebookManager != null)
        {
            FacebookManager.Instance.OnLoginSuccess -= OnLoginSuccess;
            FacebookManager.Instance.OnRequestUserDataSuccess -= OnRequestUserDataSuccess;
			FacebookManager.Instance.OnLogoutSuccess -= OnLogOut;
        }
    }

    // Métodos auxiliares
    private void UpdateControls()
    {
        if (!FacebookManager.Instance.LoggedIn)
        {
            this.loginText.SetActive(true);
            this.logoutText.SetActive(false);
            this.proxilePictureObject.SetActive(false);
			this.fForFacebook.SetActive (true);
        }
        else
        {
			logoutText.GetComponent<Text> ().text = FacebookManager.Instance.CurrentUser.FirstName;
			this.profilePictureRawImage.texture = FacebookManager.Instance.CurrentUser.ProfilePicture;
            this.loginText.SetActive(false);
            this.logoutText.SetActive(true);
            this.proxilePictureObject.SetActive(true);
			this.fForFacebook.SetActive (false);
        }
    }

	private void OnLogOut(){
		this.UpdateControls ();
	}

    // Manejadores de eventos
    private void OnLoginSuccess()
    {
		Debug.Log ("Login succes");
        FacebookManager.Instance.RequestUserData();

        this.UpdateControls();
    }

    private void OnRequestUserDataSuccess()
    {
		//Debug.Log (FacebookManager.Instance.CurrentUser.Name);
		//Debug.Log (FacebookManager.Instance.CurrentUser.FirstName);
		//Debug.Log (FacebookManager.Instance.CurrentUser.LastName);
		//Debug.Log (FacebookManager.Instance.LastRequestedUser.Name);
		//Debug.Log (FacebookManager.Instance.LastRequestedUser.FirstName);
		//Debug.Log (FacebookManager.Instance.LastRequestedUser.LastName);
        this.UpdateControls();
    }

}