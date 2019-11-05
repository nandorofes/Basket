using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Purchasing;

// Deriving the Purchaser class from IStoreListener enables it to receive messages from Unity Purchasing.
public class IAPManager : MonoBehaviour, IStoreListener
{
    private static IStoreController m_StoreController;
    // Reference to the Purchasing system.
    private static IExtensionProvider m_StoreExtensionProvider;
    // Reference to store-specific Purchasing subsystems.

	public IStoreController controller{
		get{ return m_StoreController; }
	}

    void Start()
    {
        // If we haven't set up the Unity Purchasing reference
        if (m_StoreController == null)
        {
            // Begin to configure our connection to Purchasing
            InitializePurchasing();
        }
    }

    public void InitializePurchasing()
    {
        // If we have already connected to Purchasing ...
        if (IsInitialized())
        {
            // ... we are done here.
            return;
        }

        // Create a builder, first passing in a suite of Unity provided stores.
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        // Add a product to sell / restore by way of its identifier, associating the general identifier with its store-specific identifiers.

		//TICKETS
        builder.AddProduct("150tickets", ProductType.Consumable, new IDs()
            {
				{ "150tickets", AppleAppStore.Name },
                { "150tickets",  GooglePlay.Name },
            });
		
        builder.AddProduct("750tickets", ProductType.Consumable, new IDs()
            {
				{ "750tickets", AppleAppStore.Name },
				{ "750tickets",  GooglePlay.Name },
            });
		
        builder.AddProduct("2250tickets", ProductType.Consumable, new IDs()
            {
				{ "2250tickets", AppleAppStore.Name },
				{ "2250tickets",  GooglePlay.Name },
            });

		builder.AddProduct ("6000tickets", ProductType.Consumable, new IDs () {
				{ "6000tickets", AppleAppStore.Name },
				{ "6000tickets",  GooglePlay.Name },
			});

		//CREDITOS
		builder.AddProduct("horas1", ProductType.Consumable, new IDs()
			{
				{ "horas1", AppleAppStore.Name },
				{ "horas1",  GooglePlay.Name },
			});

		builder.AddProduct("horas3", ProductType.Consumable, new IDs()
			{
				{ "horas3", AppleAppStore.Name },
				{ "horas3",  GooglePlay.Name },
			});

		builder.AddProduct("horas12", ProductType.Consumable, new IDs()
			{
				{ "horas12", AppleAppStore.Name },
				{ "horas12",  GooglePlay.Name },
			});

		builder.AddProduct ("horas24", ProductType.Consumable, new IDs () {
				{ "horas24", AppleAppStore.Name },
				{ "horas24",  GooglePlay.Name },
			});

		builder.AddProduct ("horas48", ProductType.Consumable, new IDs () {
				{ "horas48", AppleAppStore.Name },
				{ "horas48",  GooglePlay.Name },
			});

		builder.AddProduct ("semana", ProductType.Consumable, new IDs () {
				{ "semana", AppleAppStore.Name },
				{ "semana",  GooglePlay.Name },
			});

        UnityPurchasing.Initialize(this, builder);
    }


    private bool IsInitialized()
    {
        // Only say we are initialized if both the Purchasing references are set.
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }


	public void BuyTickets(string tickets)
    {
        // Buy the consumable product using its general identifier. Expect a response either through ProcessPurchase or OnPurchaseFailed asynchronously.
        BuyProductID(tickets);
    }

	public void BuyCredits(string credits)
    {
        BuyProductID(credits);
    }


    void BuyProductID(string productId)
    {
        // If the stores throw an unexpected exception, use try..catch to protect my logic here.
        try
        {
            // If Purchasing has been initialized ...
            if (IsInitialized())
            {
                // ... look up the Product reference with the general product identifier and the Purchasing system's products collection.
                Product product = m_StoreController.products.WithID(productId);

                // If the look up found a product for this device's store and that product is ready to be sold ... 
                if (product != null && product.availableToPurchase)
                {
                    Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));// ... buy the product. Expect a response either through ProcessPurchase or OnPurchaseFailed asynchronously.
                    m_StoreController.InitiatePurchase(product);
                }
					// Otherwise ...
					else
                {
                    // ... report the product look-up failure situation  
                    Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
                }
            }
				// Otherwise ...
				else
            {
                // ... report the fact Purchasing has not succeeded initializing yet. Consider waiting longer or retrying initiailization.
                Debug.Log("BuyProductID FAIL. Not initialized.");
            }
        }
			// Complete the unexpected exception handling ...
			catch (Exception e)
        {
            // ... by reporting any unexpected exception for later diagnosis.
            Debug.Log("BuyProductID: FAIL. Exception during purchase. " + e);
        }
    }


    // Restore purchases previously made by this customer. Some platforms automatically restore purchases. Apple currently requires explicit purchase restoration for IAP.
    public void RestorePurchases()
    {
        // If Purchasing has not yet been set up ...
        if (!IsInitialized())
        {
            // ... report the situation and stop restoring. Consider either waiting longer, or retrying initialization.
            Debug.Log("RestorePurchases FAIL. Not initialized.");
            return;
        }

        // If we are running on an Apple device ... 
        if (Application.platform == RuntimePlatform.IPhonePlayer ||
        Application.platform == RuntimePlatform.OSXPlayer)
        {
            // ... begin restoring purchases
            Debug.Log("RestorePurchases started ...");

            // Fetch the Apple store-specific subsystem.
//            var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
//            // Begin the asynchronous process of restoring purchases. Expect a confirmation response in the Action<bool> below, and ProcessPurchase if there are previously purchased products to restore.
//            apple.RestoreTransactions((result) =>
//                {
//                    // The first phase of restoration. If no more responses are received on ProcessPurchase then no purchases are available to be restored.
//                    Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
//                });
        }
			// Otherwise ...
			else
        {
            // We are not running on an Apple device. No work is necessary to restore purchases.
            Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
        }
    }


    //
    // --- IStoreListener
    //

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        // Purchasing has succeeded initializing. Collect our Purchasing references.
        Debug.Log("OnInitialized: PASS");

        // Overall Purchasing system, configured with products for this application.
        m_StoreController = controller;
        // Store specific subsystem, for accessing device-specific store features.
        m_StoreExtensionProvider = extensions;
    }


    public void OnInitializeFailed(InitializationFailureReason error)
    {
        // Purchasing set-up has not succeeded. Check error for reason. Consider sharing this reason with the user.
        Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
    }


    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        // A consumable product has been purchased by this user.

		//TICKETS
        if (String.Equals(args.purchasedProduct.definition.id, "150tickets", StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));//If the consumable item has been successfully purchased, add 1000 coins to the player's in-game score.
			GameManager.Instance.GamePersistentData.Tickets += 500;

            /*FacebookManager.Instance.LogCustomEvent("real_purchase", "monedas1");
			GameManager.Instance.GoogleAnalyticsInstance.LogItem("T001", "1000 Bills", "monedas1", "Items", 0.99, 1);
			GameManager.Instance.GoogleAnalyticsInstance.LogTransaction("T001", "In-Game Store", 0.99, 0.00, 0.00);
			*/
			#if UNITY_ANDROID
			//GameAnalytics.NewBusinessEventGooglePlay ("EUR", 99, "500Tickets", "150tickets", SceneManager.GetActiveScene().name, null, null);
			#elif UNITY_IPHONE
			//GameAnalytics.NewBusinessEventIOS ("EUR", 99, "500Tickets", "150tickets", SceneManager.GetActiveScene().name, null);
			#endif

        }

        if (String.Equals(args.purchasedProduct.definition.id, "750tickets", StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));//If the consumable item has been successfully purchased, add 2500 coins to the player's in-game score.
			GameManager.Instance.GamePersistentData.Tickets += 2500;
            
			/*FacebookManager.Instance.LogCustomEvent("real_purchase", "monedas2");
			GameManager.Instance.GoogleAnalyticsInstance.LogItem("T002", "2500 Bills", "monedas2", "Items", 1.99, 1);
            GameManager.Instance.GoogleAnalyticsInstance.LogTransaction("T002", "In-Game Store", 1.99, 0.00, 0.00);
            */
			#if UNITY_ANDROID
			//GameAnalytics.NewBusinessEventGooglePlay ("EUR", 399, "2500Tickets", "750tickets", SceneManager.GetActiveScene().name, null, null);
			#elif UNITY_IPHONE
			//GameAnalytics.NewBusinessEventIOS ("EUR", 399, "2500Tickets", "750tickets", SceneManager.GetActiveScene().name, null);
			#endif
        }

        if (String.Equals(args.purchasedProduct.definition.id, "2250tickets", StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));//If the consumable item has been successfully purchased, add 5000 coins to the player's in-game score.
			GameManager.Instance.GamePersistentData.Tickets += 6000;
            
			/*FacebookManager.Instance.LogCustomEvent("real_purchase", "monedas3");
			GameManager.Instance.GoogleAnalyticsInstance.LogItem("T003", "5000 Bills", "monedas3", "Items", 3.99, 1);
            GameManager.Instance.GoogleAnalyticsInstance.LogTransaction("T003", "In-Game Store", 3.99, 0.00, 0.00);
            */
			#if UNITY_ANDROID
			//GameAnalytics.NewBusinessEventGooglePlay ("EUR", 699, "6000Tickets", "2250tickets", SceneManager.GetActiveScene().name, null, null);
			#elif UNITY_IPHONE
			//GameAnalytics.NewBusinessEventIOS ("EUR",699, "6000Tickets", "2250tickets", SceneManager.GetActiveScene().name, null);
			#endif
        }

        if (String.Equals(args.purchasedProduct.definition.id, "6000tickets", StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));//If the consumable item has been successfully purchased, add 7500 coins to the player's in-game score.
			GameManager.Instance.GamePersistentData.Tickets += 35000;
            
			/*FacebookManager.Instance.LogCustomEvent("real_purchase", "monedas4");
            GameManager.Instance.GoogleAnalyticsInstance.LogItem("T004", "7500 Bills", "monedas4", "Items", 5.99, 1);
            GameManager.Instance.GoogleAnalyticsInstance.LogTransaction("T004", "In-Game Store", 5.99, 0.00, 0.00);
            */
			#if UNITY_ANDROID
			//GameAnalytics.NewBusinessEventGooglePlay ("EUR", 3999, "35000Tickets", "6000tickets", SceneManager.GetActiveScene().name, null, null);
			#elif UNITY_IPHONE
			//GameAnalytics.NewBusinessEventIOS ("EUR", 3999, "35000Tickets", "6000tickets", SceneManager.GetActiveScene().name, null);
			#endif
        }

        //CREDITS

		if (String.Equals (args.purchasedProduct.definition.id, "horas1", StringComparison.Ordinal)) {
			Debug.Log (string.Format ("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));//If the consumable item has been successfully purchased, add 7500 coins to the player's in-game score.
			CreditsManagement creditsManager = this.gameObject.GetComponent<CreditsManagement>();
			creditsManager.OnTimePurchased ("horas1");

			#if UNITY_ANDROID
			//GameAnalytics.NewBusinessEventGooglePlay ("EUR", 99, "1Hora", "horas1", SceneManager.GetActiveScene().name, null, null);
			#elif UNITY_IPHONE
			//GameAnalytics.NewBusinessEventIOS ("EUR", 99, "1Hora", "horas1", SceneManager.GetActiveScene().name, null);
			#endif
		}

		if (String.Equals (args.purchasedProduct.definition.id, "horas3", StringComparison.Ordinal)) {
			Debug.Log (string.Format ("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));//If the consumable item has been successfully purchased, add 7500 coins to the player's in-game score.
			CreditsManagement creditsManager = this.gameObject.GetComponent<CreditsManagement>();
			creditsManager.OnTimePurchased ("horas3");

			#if UNITY_ANDROID
			//GameAnalytics.NewBusinessEventGooglePlay ("EUR", 199, "3Horas", "horas3", SceneManager.GetActiveScene().name, null, null);
			#elif UNITY_IPHONE
			//GameAnalytics.NewBusinessEventIOS ("EUR", 199, "3Horas", "horas3", SceneManager.GetActiveScene().name, null);
			#endif
		}

		if (String.Equals (args.purchasedProduct.definition.id, "horas12", StringComparison.Ordinal)) {
			Debug.Log (string.Format ("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));//If the consumable item has been successfully purchased, add 7500 coins to the player's in-game score.
			CreditsManagement creditsManager = this.gameObject.GetComponent<CreditsManagement>();
			creditsManager.OnTimePurchased ("horas12");

			#if UNITY_ANDROID
			//GameAnalytics.NewBusinessEventGooglePlay ("EUR", 399, "12Horas", "horas12", SceneManager.GetActiveScene().name, null, null);
			#elif UNITY_IPHONE
			//GameAnalytics.NewBusinessEventIOS ("EUR", 399, "12Horas", "horas12", SceneManager.GetActiveScene().name, null);
			#endif
		}

		if (String.Equals (args.purchasedProduct.definition.id, "horas24", StringComparison.Ordinal)) {
			Debug.Log (string.Format ("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));//If the consumable item has been successfully purchased, add 7500 coins to the player's in-game score.
			CreditsManagement creditsManager = this.gameObject.GetComponent<CreditsManagement>();
			creditsManager.OnTimePurchased ("horas24");

			#if UNITY_ANDROID
			//GameAnalytics.NewBusinessEventGooglePlay ("EUR", 599, "24Horas", "horas24", SceneManager.GetActiveScene().name, null, null);
			#elif UNITY_IPHONE
			//GameAnalytics.NewBusinessEventIOS ("EUR", 599, "24Horas", "horas24", SceneManager.GetActiveScene().name, null);
			#endif
		}

		if (String.Equals (args.purchasedProduct.definition.id, "horas48", StringComparison.Ordinal)) {
			Debug.Log (string.Format ("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));//If the consumable item has been successfully purchased, add 7500 coins to the player's in-game score.
			CreditsManagement creditsManager = this.gameObject.GetComponent<CreditsManagement>();
			creditsManager.OnTimePurchased ("horas48");

			#if UNITY_ANDROID
			//GameAnalytics.NewBusinessEventGooglePlay ("EUR", 999, "48Horas", "horas48", SceneManager.GetActiveScene().name, null, null);
			#elif UNITY_IPHONE
			//GameAnalytics.NewBusinessEventIOS ("EUR", 999, "48Horas", "horas48", SceneManager.GetActiveScene().name, null);
			#endif
		}

		if (String.Equals (args.purchasedProduct.definition.id, "semana", StringComparison.Ordinal)) {
			Debug.Log (string.Format ("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));//If the consumable item has been successfully purchased, add 7500 coins to the player's in-game score.
			CreditsManagement creditsManager = this.gameObject.GetComponent<CreditsManagement>();
			creditsManager.OnTimePurchased ("semana");

			#if UNITY_ANDROID
			//GameAnalytics.NewBusinessEventGooglePlay ("EUR", 3499, "1Semana", "semana", SceneManager.GetActiveScene().name, null, null);
			#elif UNITY_IPHONE
			//GameAnalytics.NewBusinessEventIOS ("EUR", 3499, "1Semana", "semana", SceneManager.GetActiveScene().name, null);
			#endif
		}

			// Or ... an unknown product has been purchased by this user. Fill in additional products here.
			else
        {
            Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
        }// Return a flag indicating wither this product has completely been received, or if the application needs to be reminded of this purchase at next app launch. Is useful when saving purchased products to the cloud, and when that save is delayed.



        return PurchaseProcessingResult.Complete;
    }


    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        // A product purchase attempt did not succeed. Check failureReason for more detail. Consider sharing this reason with the user.
        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
    }
}
//}