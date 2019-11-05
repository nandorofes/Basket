using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinSelectionArrowsShop : MonoBehaviour {

	[SerializeField]
	private ScrollSnapRect scrollNormal;
	[SerializeField]
	private GameObject nextNormal;
	[SerializeField]
	private GameObject prevNormal;
	[SerializeField]
	private ScrollSnapRect scrollTime;
	[SerializeField]
	private GameObject nextTime;
	[SerializeField]
	private GameObject prevTime;
	[SerializeField]
	private ScrollSnapRect scrollTriple;
	[SerializeField]
	private GameObject nextTriple;
	[SerializeField]
	private GameObject prevTriple;
	[SerializeField]
	private ScrollSnapRect scrollBonus;
	[SerializeField]
	private GameObject nextBonus;
	[SerializeField]
	private GameObject prevBonus;
	[SerializeField]
	private Transform normalBallsHolder;
	[SerializeField]
	private Transform timeBallsHolder;
	[SerializeField]
	private Transform tripleBallHolder;
	[SerializeField]
	private Transform bonusBallHolder;

	[SerializeField]
	private SkinsBuyPanel skinPanel;

	void Update(){
		if (skinPanel.lastTab.name == "Normal tab") {
			if (scrollNormal._currentPage == 0 && (normalBallsHolder.childCount - 1) == 0) {
				prevNormal.SetActive (false);
				prevBonus.SetActive (false);
				prevTriple.SetActive (false);
				prevTime.SetActive (false);
				nextNormal.SetActive (false);
				nextTime.SetActive (false);
				nextTriple.SetActive (false);
				nextBonus.SetActive (false);
			} else if (scrollNormal._currentPage == 0) {
				prevNormal.SetActive (false);
				prevBonus.SetActive (false);
				prevTriple.SetActive (false);
				prevTime.SetActive (false);
				nextNormal.SetActive (true);
			} else if (scrollNormal._currentPage == (normalBallsHolder.childCount - 1)) {
				nextNormal.SetActive (false);
				nextTime.SetActive (false);
				nextTriple.SetActive (false);
				nextBonus.SetActive (false);
				prevNormal.SetActive (true);
			} else {
				prevNormal.SetActive (true);
				nextNormal.SetActive (true);
			}
		} else if (skinPanel.lastTab.name == "Triple tab") {
			if (scrollTriple._currentPage == 0 && (tripleBallHolder.childCount - 1) == 0) {
				prevNormal.SetActive (false);
				prevBonus.SetActive (false);
				prevTriple.SetActive (false);
				prevTime.SetActive (false);
				nextNormal.SetActive (false);
				nextTime.SetActive (false);
				nextTriple.SetActive (false);
				nextBonus.SetActive (false);
			} else if (scrollTriple._currentPage == 0) {
				prevNormal.SetActive (false);
				prevBonus.SetActive (false);
				prevTriple.SetActive (false);
				prevTime.SetActive (false);
				nextTriple.SetActive (true);
			} else if (scrollTriple._currentPage == (tripleBallHolder.childCount - 1)) {
				nextNormal.SetActive (false);
				nextTime.SetActive (false);
				nextTriple.SetActive (false);
				nextBonus.SetActive (false);
				prevTriple.SetActive (true);
			} else {
				prevTriple.SetActive (true);
				nextTriple.SetActive (true);
			}
		} else if (skinPanel.lastTab.name == "Bonus tab") {
			if (scrollBonus._currentPage == 0 && (bonusBallHolder.childCount - 1) == 0) {
				prevNormal.SetActive (false);
				prevBonus.SetActive (false);
				prevTriple.SetActive (false);
				prevTime.SetActive (false);
				nextNormal.SetActive (false);
				nextTime.SetActive (false);
				nextTriple.SetActive (false);
				nextBonus.SetActive (false);
			} else if (scrollBonus._currentPage == 0) {
				prevNormal.SetActive (false);
				prevBonus.SetActive (false);
				prevTriple.SetActive (false);
				prevTime.SetActive (false);
				nextBonus.SetActive (true);
			} else if (scrollBonus._currentPage == (bonusBallHolder.childCount - 1)) {
				nextNormal.SetActive (false);
				nextTime.SetActive (false);
				nextTriple.SetActive (false);
				nextBonus.SetActive (false);
				prevBonus.SetActive (true);
			} else {
				prevBonus.SetActive (true);
				nextBonus.SetActive (true);
			}
		} else {
			if (scrollTime._currentPage == 0 && (timeBallsHolder.childCount - 1) == 0) {
				prevNormal.SetActive (false);
				prevBonus.SetActive (false);
				prevTriple.SetActive (false);
				prevTime.SetActive (false);
				nextNormal.SetActive (false);
				nextTime.SetActive (false);
				nextTriple.SetActive (false);
				nextBonus.SetActive (false);
			} else if (scrollTime._currentPage == 0) {
				prevNormal.SetActive (false);
				prevBonus.SetActive (false);
				prevTriple.SetActive (false);
				prevTime.SetActive (false);
				nextTime.SetActive (true);
			} else if (scrollTime._currentPage == (timeBallsHolder.childCount - 1)) {
				nextNormal.SetActive (false);
				nextTime.SetActive (false);
				nextTriple.SetActive (false);
				nextBonus.SetActive (false);
				prevTime.SetActive (true);
			} else {
				prevTime.SetActive (true);
				nextTime.SetActive (true);
			}
		}
	}
}
