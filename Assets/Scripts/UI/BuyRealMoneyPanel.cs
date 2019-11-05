using Extensions.System.SequentialArray;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BuyRealMoneyPanel : MonoBehaviour {

	// ---- ---- ---- ---- ---- ---- ---- ----
	// Atributos
	// ---- ---- ---- ---- ---- ---- ---- ----
	[SerializeField]
	private Transform tabHolder;
	[SerializeField]
	private Transform[] tabList;
	[SerializeField]
	private ScrollSnapRect[] scrollList;
	[SerializeField]
	private IAPManager iapManager;

	[SerializeField]
	private Text hour1;
	[SerializeField]
	private Text hours3;
	[SerializeField]
	private Text hours12;
	[SerializeField]
	private Text hours24;
	[SerializeField]
	private Text hours48;
	[SerializeField]
	private Text week;
	[SerializeField]
	private Text tickets500;
	[SerializeField]
	private Text tickets2500;
	[SerializeField]
	private Text tickets6000;
	[SerializeField]
	private Text tickets35000;






    // ---- ---- ---- ---- ---- ---- ---- ----
    // Métodos
    // ---- ---- ---- ---- ---- ---- ---- ----
    public void SetActiveTab(int tabNumber)
	{
		if (tabList.IndexInRange(tabNumber))
		{
			//Transform selectedTab = this.tabList[tabNumber];
			//selectedTab.SetAsLastSibling();
			for (int i = 0; i < tabList.Length; i++) {
				if (i == tabNumber) {
					tabList [i].gameObject.SetActive (true);
				} else {
					tabList [i].gameObject.SetActive (false);
				}
			}
		}
	}

	public void SetActiveButtonCredits(int buttonNumber)
	{
		if (tabList.IndexInRange(1))
		{
			scrollList [1].PublicSetPage(buttonNumber);
		}
	}

	public void SetActiveButtonTickets(int buttonNumber)
	{
		if (tabList.IndexInRange(0))
		{
			scrollList [0].PublicSetPage(buttonNumber);
		}
	}

    public void Hide()
	{
		this.gameObject.SetActive(false);
	}

	public void Show()
	{
		this.gameObject.SetActive(true);
		LoadPrices ();
	}

	void LoadPrices(){
		//hour1.text = "Tu puta, malder";
		
		if (iapManager.controller != null) {
			foreach (var product in iapManager.controller.products.all) {
				Debug.Log (product.metadata.localizedTitle);
				Debug.Log (product.transactionID);
				Debug.Log (product.metadata.localizedDescription);
				Debug.Log (product.metadata.localizedPriceString);
			}

			hour1.text = iapManager.controller.products.WithID ("horas1").metadata.localizedPriceString;
			hours3.text = iapManager.controller.products.WithID ("horas3").metadata.localizedPriceString;
			hours12.text = iapManager.controller.products.WithID ("horas12").metadata.localizedPriceString;
			hours24.text = iapManager.controller.products.WithID ("horas24").metadata.localizedPriceString;
			hours48.text = iapManager.controller.products.WithID ("horas48").metadata.localizedPriceString;
			week.text = iapManager.controller.products.WithID ("semana").metadata.localizedPriceString;

			tickets500.text = iapManager.controller.products.WithID ("150tickets").metadata.localizedPriceString;
			tickets2500.text = iapManager.controller.products.WithID ("750tickets").metadata.localizedPriceString;
			tickets6000.text = iapManager.controller.products.WithID ("2250tickets").metadata.localizedPriceString;
			tickets35000.text = iapManager.controller.products.WithID ("6000tickets").metadata.localizedPriceString;
		}
	}

	void Update(){
		if (scrollList[0].gameObject.activeInHierarchy) {
			if (scrollList[0]._currentPage == 0) {
				scrollList[0].prevButton.SetActive (false);
			} else if (scrollList[0]._currentPage == 4) {
				scrollList[0].nextButton.SetActive (false);
			} else {
				scrollList[0].prevButton.SetActive (true);
				scrollList[0].nextButton.SetActive (true);
			}
		} else if (scrollList[1].gameObject.activeInHierarchy) {
			if (scrollList[1]._currentPage == 0) {
				scrollList[1].prevButton.SetActive (false);
			} else if (scrollList[1]._currentPage == 5) {
				scrollList[1].nextButton.SetActive (false);
			} else {
				scrollList[1].prevButton.SetActive (true);
				scrollList[1].nextButton.SetActive (true);
			}
		}
	}
}