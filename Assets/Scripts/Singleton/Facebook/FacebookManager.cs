using Facebook.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Prefab("Facebook manager", true)]
public class FacebookManager : Singleton<FacebookManager>
{
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Constantes
    // ---- ---- ---- ---- ---- ---- ---- ----
    private readonly string[] basicPermissions = { "public_profile", "user_friends", "user_location" };
    private readonly string[] publishPermissions = { "publish_actions" };

    // ---- ---- ---- ---- ---- ---- ---- ----
    // Atributos
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Datos de usuario
    private FacebookUser currentUser;
	private FacebookUser currentRival;
    private FacebookUser lastRequestedUser;
    
    // Datos de puntuaciones
    private FacebookScore currentUserScore;
    private FacebookScore lastRequestedUserScore;
    
    // Datos de grupos
    private FacebookFriendList friendList;
    private FacebookLeaderboards leaderboards;
    
    // Varibales de control
    private AccessToken accessToken;
    
    private FacebookUser friendInfo = null;
    private bool friendInfoReady = false;
    
    private FacebookUser leaderboardsUserInfo = null;
    private bool leaderboardsUserInfoReady = false;
    
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Propiedades
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Datos de usuario
    public FacebookUser CurrentUser
    {
        get { return FacebookManager.Instance.currentUser; }
    }

    public FacebookUser LastRequestedUser
    {
        get { return FacebookManager.Instance.lastRequestedUser; }
    }
    
	public FacebookUser CurrentRival
	{
		get { return FacebookManager.Instance.currentRival; }
	}
    // Datos de puntuaciones
    public FacebookScore CurrentUserScore
    {
        get { return FacebookManager.Instance.currentUserScore; }
    }

    public FacebookScore LastRequestedUserScore
    {
        get { return FacebookManager.Instance.lastRequestedUserScore; }
    }
    
    // Datos de grupo
    public FacebookFriendList FriendList
    {
        get { return FacebookManager.Instance.friendList; }
    }

    public FacebookLeaderboards Leaderboards
    {
        get { return FacebookManager.Instance.leaderboards; }
    }
    
    // Datos calculados
    public bool HavePublishActions
    {
        get
        {
            return (FB.IsLoggedIn &&
            (AccessToken.CurrentAccessToken.Permissions as List<string>).Contains("publish_actions")) ? true : false;
			return false;
        }
    }

    public bool LoggedIn
    {
		get { return  FB.IsLoggedIn; }
    }

    // ---- ---- ---- ---- ---- ---- ---- ----
    // Eventos
    // ---- ---- ---- ---- ---- ---- ---- ----
    public event Action<string> OnLoginError = delegate { };
    public event Action OnLoginSuccess = delegate { };

    public event Action<string> OnLogoutError = delegate { };
    public event Action OnLogoutSuccess = delegate { };

    public event Action<string> OnRequestAppInvitationError = delegate { };
    public event Action OnRequestAppInvitationSuccess = delegate { };

    public event Action<string> OnRequestShareStoryError = delegate { };
    public event Action OnRequestShareStorySuccess = delegate { };

    public event Action<string> OnRequestUserDataError = delegate { };
    public event Action OnRequestUserDataSuccess = delegate { };

	public event Action<string> OnRequestRivalDataError = delegate { };
	public event Action OnRequestRivalDataSuccess = delegate { };
    
    public event Action<string> OnRequestGetScoreError = delegate { };
    public event Action OnRequestGetScoreSuccess = delegate { };
    
    public event Action<string> OnRequestSetScoreError = delegate { };
    public event Action OnRequestSetScoreSuccess = delegate { };
    
    public event Action<string> OnRequestFriendListError = delegate { };
    public event Action OnRequestFriendListSuccess = delegate { };
    public event Action OnRequestFriendListUpdate = delegate { };
    
    public event Action<string> OnRequestLeaderboardsError = delegate { };
    public event Action OnRequestLeaderboardsSuccess = delegate { };
    public event Action OnRequestLeaderboardsUpdate = delegate { };

    public event Action OnLoginForFirstTime = delegate { };

    // ---- ---- ---- ---- ---- ---- ---- ----
    // Métodos
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Métodos de registro
    public void LogApplicationOpen()
    {
        FB.LogAppEvent(AppEventName.ActivatedApp);
    }

    public void LogCustomEvent(string eventName, string description)
    {
        var eventParams = new Dictionary<string, object>();
        eventParams["description"] = description;

        FB.LogAppEvent(eventName, parameters: eventParams);
    }

    public void LogHardPurchase(string purchasedPackageName, float spentMoney, string usedCurrency)
    {
        var purchaseParameters = new Dictionary<string, object>();
        purchaseParameters["purchased_package_name"] = purchasedPackageName;
        FB.LogPurchase(spentMoney, usedCurrency, purchaseParameters);
    }

    public void LogSoftPurchase(string purchasedItemName, float spentMoney)
    {
        var softPurchaseParameters = new Dictionary<string, object>();
        softPurchaseParameters["purchased_item_name"] = purchasedItemName;
        FB.LogAppEvent(Facebook.Unity.AppEventName.SpentCredits, spentMoney, softPurchaseParameters);
    }

    public void LogTutorialCompletion(string tutorialName)
    {
        var tutorialParams = new Dictionary<string, object>();
        tutorialParams[AppEventParameterName.ContentID] = tutorialName;
        tutorialParams[AppEventParameterName.Success] = "1";
        
        FB.LogAppEvent(AppEventName.CompletedTutorial, parameters: tutorialParams);
    }

    // Métodos de control
    /// <summary>
    /// Solicita la apertura del sistema de autenticación en Facebook, que
    /// permite al jugador iniciar sesión, informarse acerca de los datos a
    /// los que la aplicación desea acceder, y aceptar o rechazar dichos
    /// accesos.
    /// </summary>
    public void RequestLogin()
    {
		FB.LogInWithReadPermissions(FacebookManager.Instance.basicPermissions,AuthCallback );
    }

	private void AuthCallback (ILoginResult result) {
		if (FB.IsLoggedIn) {
			// AccessToken class will have session details
			var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;
			FacebookManager.Instance.accessToken = Facebook.Unity.AccessToken.CurrentAccessToken;
			// Print current access token's User ID
			//Debug.Log(aToken.UserId);
			// Print current access token's granted permissions
			//foreach (string perm in aToken.Permissions) {
			//	Debug.Log(perm);
			//}
			OnLoginSuccess();
		} else {
			Debug.Log("User cancelled login");
		}
	}

    /// <summary>
    /// Solicita el cierre de la sesión con Facebook.
    /// </summary>
    public void RequestLogout()
    {
        FB.LogOut();
        FacebookManager.Instance.accessToken = null;

        FacebookManager.Instance.StartCoroutine(FacebookManager.Instance.CheckLogout(2.0f));
    }

    public void RequestShareStory(string title, string description, Uri shareLink, Uri imageLink)
    {
        /*FB.FeedShare(
            Facebook.Unity.AccessToken.CurrentAccessToken.UserId,
            shareLink,
            title, caption, description,
            imageLink, string.Empty,
            result =>
            {
                if (result.Error == null)
                    FacebookManager.Instance.OnRequestShareStorySuccess();
                else
                    FacebookManager.Instance.OnRequestShareStoryError(result.Error);
            });*/
            
        FB.ShareLink(shareLink, title, description, imageLink,
            result =>
            {
                if (result.Error == null)
                    FacebookManager.Instance.OnRequestShareStorySuccess();
                else
                    FacebookManager.Instance.OnRequestShareStoryError(result.Error);
            }
        );
    }

    public void RequestAppInvitation(Uri appLink)/*string title, string message)*/
    {
        // Prompt user to send a Game Request using FB.AppRequest
        // https://developers.facebook.com/docs/unity/reference/current/FB.AppRequest
        /*FB.AppRequest(message, null, null, null, null, string.Empty, title,
            result =>
            {
                // Error checking
                Debug.Log("AppRequestCallback");
                if (result.Error != null)
                {
                    Debug.LogError(result.Error);
                    FacebookManager.Instance.OnRequestAppInvitationError(result.Error);
                    return;
                }
                Debug.Log(result.RawResult);

                // Check response for success - show user a success popup if so
                object obj;
                if (result.ResultDictionary.TryGetValue ("cancelled", out obj))
                {
                    Debug.Log("Request cancelled");
                    FacebookManager.Instance.OnRequestAppInvitationError("Cancelled.");
                }
                else if (result.ResultDictionary.TryGetValue ("request", out obj))
                {
                    Debug.Log("Request sent");
                    FacebookManager.Instance.OnRequestAppInvitationSuccess();
                }
            }
        );*/

        /*FB.AppRequest(message, null, null, null, null, string.Empty, title,
            result =>
            {
                if (result.Error == null)
                    FacebookManager.Instance.OnRequestAppInvitationSuccess();
                else
                    FacebookManager.Instance.OnRequestAppInvitationError(result.Error);
            }
        );*/

        FB.Mobile.AppInvite(appLink, null, 
            result =>
            {
                if (result.Error == null)
                    FacebookManager.Instance.OnRequestAppInvitationSuccess();
                else
                    FacebookManager.Instance.OnRequestAppInvitationError(result.Error);
            }
        );
    }

    // Métodos de obtención de información
    /// <summary>
    /// Solicita la información del usuario que se ha autenticado en Facebook
    /// a través de la aplicación. Lanza el evento "OnRequestUserDataReceived"
    /// cuando se han obtenido sus datos de perfil. Los datos se pueden
    /// consultar tanto en la propiedad "CurrentUser" como en
    /// "LastRequestedUser".
    /// </summary>
    public void RequestUserData()
    {
		//Debug.Log ("Request user data");
		if (FacebookManager.Instance.accessToken == null) {
			Debug.Log ("accessToken=null");
			FacebookManager.Instance.OnRequestUserDataError ("Application has not logged in to Facebook yet.");
			return;
		} else {
			FacebookManager.Instance.RequestUserData (FacebookManager.Instance.accessToken.UserId);
		}
    }

    /// <summary>
    /// Solicita la información de un usuario de Facebook que ha permitido el
    /// acceso de esta aplicación para obtener datos. Lanza el evento
    /// "OnRequestUserDataReceived" cuando se han obtenido sus datos de
    /// perfil. Los datos se pueden consultar en la propiedad
    /// "LastRequestedUser".
    /// </summary>
    public void RequestUserData(string userId)
    {
		//Debug.Log ("Request user data. UserID="+userId);
        string requestUri;
        requestUri = string.Format("{0}/?fields=id,name,first_name,last_name,age_range,gender,locale", userId);
        FB.API(requestUri, HttpMethod.GET,
            result =>
            {
                // Esto se ejecuta cuando la respuesta se ha recibido, no inmediatamente
				Debug.Log ("Request user data done");
                if (result.Error == null)
                {          
                    FacebookManager.Instance.lastRequestedUser = FacebookUser.Parse(result.ResultDictionary);
                    if (FacebookManager.Instance.lastRequestedUser.Id == Facebook.Unity.AccessToken.CurrentAccessToken.UserId)
                        FacebookManager.Instance.currentUser = FacebookUser.Parse(result.ResultDictionary);
                    
                    requestUri = string.Format("{0}/picture", userId);
                    FB.API(requestUri, HttpMethod.GET,
                        pictureRequestresult =>
                        {
                            if (pictureRequestresult.Error == null)
                            {          
                                FacebookManager.Instance.lastRequestedUser.ProfilePicture = pictureRequestresult.Texture;
                                if (FacebookManager.Instance.lastRequestedUser.Id == Facebook.Unity.AccessToken.CurrentAccessToken.UserId)
                                    FacebookManager.Instance.currentUser.ProfilePicture = pictureRequestresult.Texture;

                                FacebookManager.Instance.OnRequestUserDataSuccess();
                            }
                            else
                            {
                                FacebookManager.Instance.OnRequestUserDataError(result.Error);
                            }
                        }
                    );
                }
                else
                {
                    FacebookManager.Instance.OnRequestUserDataError(result.Error);
                }
            }
        );
    }

	public void RequestRivalData(string userId)
	{
		//Debug.Log ("Request user data. UserID="+userId);
		string requestUri;
		requestUri = string.Format("{0}/?fields=id,name,first_name,last_name,age_range,gender,locale", userId);
		FB.API(requestUri, HttpMethod.GET,
			result =>
			{
				// Esto se ejecuta cuando la respuesta se ha recibido, no inmediatamente
				//Debug.Log ("Request user data done");
				if (result.Error == null)
				{          
					FacebookManager.Instance.currentRival = FacebookUser.Parse(result.ResultDictionary);

					requestUri = string.Format("{0}/picture", userId);
					FB.API(requestUri, HttpMethod.GET,
						pictureRequestresult =>
						{
							if (pictureRequestresult.Error == null)
							{          
								FacebookManager.Instance.currentRival.ProfilePicture = pictureRequestresult.Texture;

								FacebookManager.Instance.OnRequestRivalDataSuccess();
							}
							else
							{
								FacebookManager.Instance.OnRequestRivalDataError(result.Error);
							}
						}
					);
				}
				else
				{
					FacebookManager.Instance.OnRequestRivalDataError(result.Error);
				}
			}
		);
	}

    /// <summary>
    /// Solicita la lista de amigos del usuario que se ha autenticado en
    /// Facebook. Lanza el evento "OnRequestFriendListSuccess" cuando se ha
    /// obtenido. Una vez obtenidos estos datos, pueden consultarse en la
    /// propiedad "FriendList".
    /// </summary>
    public void RequestUserFriendList()
    {
        string requestUri = string.Format("{0}/friends", FacebookManager.Instance.accessToken.UserId);
        FB.API(requestUri, HttpMethod.GET,
            result =>
            {
                if (result.Error == null)
                {          
                    FacebookManager.Instance.friendList = FacebookFriendList.Parse(result.ResultDictionary);
                    FacebookManager.Instance.OnRequestFriendListSuccess();
                    
                    FacebookManager.Instance.StartCoroutine(FacebookManager.Instance.PopulateFriendInfo());
                }
                else
                {
                    FacebookManager.Instance.OnRequestFriendListError(result.Error);
                }
            }
        );
    }
    
    // Métodos de puntuación
    /// <summary>
    /// Solicita la puntuación del usuario que se ha autenticado en Facebook a
    /// través de la aplicación. Lanza el evento "OnRequestGetScoreSuccess"
    /// cuando se ha obtenido. La puntuación se puede consultar tanto en la
    /// propiedad "CurrentUserScore" como en "LastRequestedUserScore".
    /// </summary>
    public void RequestGetScore()
    {
        if (FacebookManager.Instance.accessToken == null)
        {
            FacebookManager.Instance.OnRequestGetScoreError("Application has not logged in to Facebook yet.");
            return;
        }
        else
            FacebookManager.Instance.RequestGetScore(FacebookManager.Instance.accessToken.UserId);
    }

    /// <summary>
    /// Solicita la puntuación de un usuario de Facebook que ha permitido el
    /// acceso de esta aplicación para obtener datos. Lanza el evento
    /// "OnRequestGetScoreSuccess" cuando se ha obtenido. La puntuación se
    /// puede consultar en la propiedad "LastRequestedUserScore".
    /// </summary>
    /// <param name="userId"></param>
    public void RequestGetScore(string userId)
    {
        string requestUri;
        requestUri = string.Format("{0}/scores", userId);
        
        FB.API(requestUri, HttpMethod.GET,
            result =>
            {
                // Esto se ejecuta cuando la respuesta se ha recibido, no inmediatamente
                if (result.Error == null)
                {          
                    IDictionary<string, object> dictResult = result.ResultDictionary;
                    IList<object> scoreTableResult = dictResult["data"] as IList<object>;
                    IDictionary<string, object> scoreResult = scoreTableResult[0] as IDictionary<string, object>;
                    if (scoreResult != null)
                    {
                        object scoreObject = scoreResult["score"];
                        if (scoreObject is long)
                        {
                            FacebookManager.Instance.lastRequestedUserScore = new FacebookScore((uint)scoreObject);
                            if (userId == FacebookManager.Instance.accessToken.UserId)
                                FacebookManager.Instance.currentUserScore =
                                    FacebookManager.Instance.lastRequestedUserScore;
                            
                            FacebookManager.Instance.OnRequestGetScoreSuccess();
                        }
                    }
                }
                else
                {
                    FacebookManager.Instance.OnRequestGetScoreError(result.Error);
                }
            }
        );
    }

    /// <summary>
    /// Solicita la lista de puntuaciones que han obtenido el usuario y sus
    /// amigos de Facebook.
    /// Lanza el evento "OnRequestLeaderboardsSuccess" cuando se ha obtenido
    /// una lista básica con los identificadores de usuario y sus
    /// puntuaciones.
    /// Después de esto, se lanza el evento "OnRequestLeaderboardsUpdate"
    /// de forma sucesiva conforme se van obteniendo informaciones más
    /// completas sobre cada usuario, como sus nombres, y sus imágenes de
    /// perfil.
    /// </summary>
    public void RequestLeaderboards()
    {
        string requestUri;
        requestUri = string.Format("app/scores?fields=user,score&limit=25");
        
        FB.API(requestUri, HttpMethod.GET,
            result =>
            {
                // Esto se ejecuta cuando la respuesta se ha recibido, no inmediatamente
                if (result.Error == null)
                {          
                    FacebookManager.Instance.leaderboards = FacebookLeaderboards.Parse(result.ResultDictionary);
                    FacebookManager.Instance.OnRequestLeaderboardsSuccess();
                    
                    FacebookManager.Instance.StartCoroutine(FacebookManager.Instance.PopulateLeaderboardsUserInfo());
                }
                else
                {
                    FacebookManager.Instance.OnRequestLeaderboardsError(result.Error);
                }
            }
        );
    }

    /// <summary>
    /// Solicita guardar la puntuación del usuario que se ha autenticado en
    /// Facebook a través de la aplicación.
    /// Lanza el evento "OnRequestSetScoreSuccess" cuando se ha completado
    /// la operación con éxito.
    /// </summary>
    /// <param name="facebookScore"></param>
    public void RequestSetScore(FacebookScore facebookScore)
    {
        // Check for 'publish_actions' as the Scores API requires it for submitting scores
        if (FacebookManager.Instance.HavePublishActions)
        {
            var query = new Dictionary<string, string>();
            query["score"] = facebookScore.ScoreValue.ToString();
            FB.API(
                "/me/scores",
                HttpMethod.POST,
                result => Debug.Log("PostScore Result: " + result.RawResult),
                query
            );
        }
        else
        {
            // Showing context before prompting for publish actions
            // See Facebook Login Best Practices: https://developers.facebook.com/docs/facebook-login/best-practices
            //PopupScript.SetPopup("Prompting for Publish Permissions for Scores API", 4f, delegate
            //{
            // Prompt for `publish actions` and if granted, post score
            FacebookManager.Instance.PromptForPublish(() =>
                {
                    if (FacebookManager.Instance.HavePublishActions)
                    {
                        RequestSetScore(facebookScore);
                    }
                });
            //});
        }

        /*
        if (FacebookManager.Instance.accessToken == null)
        {
            FacebookManager.Instance.OnRequestSetScoreError("Application has not logged in to Facebook yet.");
            return;
        }

        if (FacebookManager.Instance.PermissionIsGranted("publish_actions"))
        {
            string requestUri;
            requestUri = string.Format("{0}/scores?score={1}",
                FacebookManager.Instance.accessToken.UserId,
                facebookScore.ScoreValue.ToString());

            FB.API(requestUri, HttpMethod.POST,
                scoreSetRequestresult =>
                {
                    // Esto se ejecuta cuando la respuesta se ha recibido, no inmediatamente
                    if (scoreSetRequestresult.Error != null)
                    {
                        FacebookManager.Instance.OnRequestSetScoreError(scoreSetRequestresult.Error);
                    }
                    else
                    {
                        FacebookManager.Instance.OnRequestSetScoreSuccess();
                    }
                }
            );
        }
        else
        {
            // Pedir permisos para publicar puntuaciones
            FB.LogInWithPublishPermissions(FacebookManager.Instance.publishPermissions,
                permissionRequestresult =>
                {
                    // Esto se ejecuta cuando la respuesta se ha recibido, no inmediatamente
                    if (permissionRequestresult.Error != null)
                    {
                        string requestUri;
                        requestUri = string.Format("{0}/scores?score={1}",
                            FacebookManager.Instance.accessToken.UserId,
                            facebookScore.ScoreValue.ToString());
                    
                        FB.API(requestUri, HttpMethod.POST,
                            scoreSetRequestresult =>
                            {
                                // Esto se ejecuta cuando la respuesta se ha recibido, no inmediatamente
                                if (scoreSetRequestresult.Error != null)
                                {
                                    FacebookManager.Instance.OnRequestSetScoreError(scoreSetRequestresult.Error);
                                }
                                else
                                {
                                    FacebookManager.Instance.OnRequestSetScoreSuccess();
                                }
                            }
                        );
                    }
                    else
                    {
                        FacebookManager.Instance.OnRequestSetScoreError(permissionRequestresult.Error);
                    }
                }
            );
        }
        */
    }
    
    // Métodos auxiliares
    private bool PermissionIsGranted(string permission)
    {
        AccessToken token = Facebook.Unity.AccessToken.CurrentAccessToken;//FacebookManager.Instance.accessToken;
        foreach (string grantedPermission in token.Permissions)
        {
            if (grantedPermission == permission)
                return true;
        }
        
        return false;
    }

    /*public static void PromptForLogin(Action callback = null)
    {
        // Login for read permissions
        // https://developers.facebook.com/docs/unity/reference/current/FB.LogInWithReadPermissions
        FB.LogInWithReadPermissions(FacebookManager.Instance.publishPermissions, result =>
            {
                Debug.Log("LoginCallback");
                if (FB.IsLoggedIn)
                {
                    Debug.Log("Logged in with ID: " + AccessToken.CurrentAccessToken.UserId + "\nGranted Permissions: " + AccessToken.CurrentAccessToken.Permissions.ToCommaSeparateList());
                }
                else
                {
                    if (result.Error != null)
                    {
                        Debug.LogError(result.Error);
                    }
                    Debug.Log("Not Logged In");
                }
                if (callback != null)
                {
                    callback();
                }
            });
    }*/

    private void PromptForPublish(Action callback = null)
    {
        FB.LogInWithPublishPermissions(publishPermissions, delegate (ILoginResult result)
            {
                Debug.Log("LoginCallback");
                if (FB.IsLoggedIn)
                {
                    Debug.Log("Logged in with ID: " + AccessToken.CurrentAccessToken.UserId +
                        "\nGranted Permissions: " + AccessToken.CurrentAccessToken.Permissions.ToCommaSeparateList());
                }
                else
                {
                    if (result.Error != null)
                    {
                        Debug.LogError(result.Error);
                    }
                    Debug.Log("Not Logged In");
                }
                if (callback != null)
                {
                    callback();
                }
            });
    }

    private void RequestUserDataForFriendList(string userId)
    {
        string requestUri;
        requestUri = string.Format("{0}/?fields=id,name,first_name,last_name,age_range,gender,locale", userId);
        FB.API(requestUri, HttpMethod.GET,
            dataRequestResult =>
            {
                // Esto se ejecuta cuando la respuesta se ha recibido, no inmediatamente
                if (dataRequestResult.Error == null)
                {
                    FacebookUser user = FacebookUser.Parse(dataRequestResult.ResultDictionary);
                    
                    requestUri = string.Format("{0}/picture", userId);
                    FB.API(requestUri, HttpMethod.GET,
                        pictureRequestResult =>
                        {
                            if (pictureRequestResult.Error == null)
                            {
                                user.ProfilePicture = pictureRequestResult.Texture;
                                
                                FacebookManager.Instance.friendInfo = user;
                                FacebookManager.Instance.friendInfoReady = true;
                            }
                        }
                    );
                }
            }
        );
    }

    private void RequestUserDataForLeaderboards(string userId)
    {
        string requestUri;
        requestUri = string.Format("{0}/?fields=id,name,first_name,last_name,age_range,gender,locale", userId);
        FB.API(requestUri, HttpMethod.GET,
            dataRequestResult =>
            {
                // Esto se ejecuta cuando la respuesta se ha recibido, no inmediatamente
                if (dataRequestResult.Error == null)
                {
                    FacebookUser user = FacebookUser.Parse(dataRequestResult.ResultDictionary);
                    
                    requestUri = string.Format("{0}/picture", userId);
                    FB.API(requestUri, HttpMethod.GET,
                        pictureRequestResult =>
                        {
                            if (pictureRequestResult.Error == null)
                            {
                                user.ProfilePicture = pictureRequestResult.Texture;
                                
                                FacebookManager.Instance.leaderboardsUserInfo = user;
                                FacebookManager.Instance.leaderboardsUserInfoReady = true;
                            }
                        }
                    );
                }
            }
        );
    }

    private void SendLoginEvent()
    {
        FacebookManager.Instance.OnLoginSuccess();

        if (!PlayerPrefs.HasKey("LoggedForFirstTime"))
        {
            PlayerPrefs.SetInt("LoggedForFirstTime", 1);
            FacebookManager.Instance.OnLoginForFirstTime();
        }
    }

    // Métodos de MonoBehaviour
    private void Awake()
    {
        if (!FB.IsInitialized)
        {
            // Initialize the Facebook SDK
			FB.Init(InitializationCallback,OnHideUnity);
        }
        else
        {
            // Already initialized, signal an app activation App Event
            FB.ActivateApp();
            FacebookManager.Instance.LogApplicationOpen();
        }

        FacebookManager.Instance.StartCoroutine(FacebookManager.Instance.CheckLogin(1.0f));
    }
    
    // Corrutinas
    private IEnumerator CheckLogin(float maxTime)
    {
        bool loggedIn = false;
        float currentTime = 0.0f;

        while (currentTime < maxTime)
        {
            if (FB.IsLoggedIn)
            {
                loggedIn = true;
                break;
            }

            currentTime += Time.deltaTime;
            yield return null;
        }

        if (loggedIn)
        {
            yield return new WaitForSeconds(0.20f);

            FacebookManager.Instance.accessToken = Facebook.Unity.AccessToken.CurrentAccessToken;
            FacebookManager.Instance.SendLoginEvent();
        }
    }

    private IEnumerator CheckLogout(float time)
    {
        bool error = true;
        float currentTime = 0.0f;

        while (currentTime < time)
        {
            if (!FB.IsLoggedIn)
            {
                error = false;
                break;
            }

            currentTime += Time.deltaTime;
            yield return null;
        }

        if (!error)
            FacebookManager.Instance.OnLogoutSuccess();
        else
            FacebookManager.Instance.OnLogoutError("Logout has not been possible.");
    }

    private IEnumerator PopulateFriendInfo()
    {
        for (int i = 0; i < FacebookManager.Instance.friendList.Count; i++)
        {
            FacebookUser user = FacebookManager.Instance.friendList[i];
            
            // Inicializar variables de control
            float iterationTime = 0.0f;
            FacebookManager.Instance.friendInfo = null;
            FacebookManager.Instance.friendInfoReady = false;
            
            // Solicitar información del usuario actual
            FacebookManager.Instance.RequestUserDataForFriendList(user.Id);
            
            // Bucle de espera
            while (!FacebookManager.Instance.friendInfoReady && iterationTime < 2.0f)
            {
                iterationTime += Time.deltaTime;
                yield return null;
            }
            
            // Comprobar respuesta
            if (FacebookManager.Instance.friendInfo != null)
            {
                FacebookManager.Instance.friendList[i] = FacebookManager.Instance.friendInfo;
                FacebookManager.Instance.OnRequestFriendListUpdate();
            }
        }
    }

    private IEnumerator PopulateLeaderboardsUserInfo()
    {
        foreach (FacebookLeaderboardsEntry entry in FacebookManager.Instance.leaderboards)
        {
            FacebookUser user = entry.User;
            
            // Inicializar variables de control
            float iterationTime = 0.0f;
            FacebookManager.Instance.leaderboardsUserInfo = null;
            FacebookManager.Instance.leaderboardsUserInfoReady = false;
            
            // Solicitar información del usuario actual
            FacebookManager.Instance.RequestUserDataForLeaderboards(user.Id);
            
            // Bucle de espera
            while (!FacebookManager.Instance.leaderboardsUserInfoReady && iterationTime < 2.0f)
            {
                iterationTime += Time.deltaTime;
                yield return null;
            }
            
            // Comprobar respuesta
            if (FacebookManager.Instance.leaderboardsUserInfo != null)
            {
                entry.User = FacebookManager.Instance.leaderboardsUserInfo;
                FacebookManager.Instance.OnRequestLeaderboardsUpdate();
            }
        }
    }
    
    // Métodos de retrollamada
    private void InitializationCallback()
    {
        if (FB.IsInitialized)
        {
            FB.ActivateApp();
            FacebookManager.Instance.LogApplicationOpen();
        }
        else
        {
            Debug.LogWarning("Failed to Initialize the Facebook SDK.");
        }
    }
    
	private void OnHideUnity (bool isGameShown)
	{
		if (!isGameShown) {
			// Pause the game - we will need to hide
			Time.timeScale = 0;
		} else {
			// Resume the game - we're getting focus again
			Time.timeScale = 1;
		}
	}

}