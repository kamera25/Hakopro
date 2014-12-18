using UnityEngine;
using System.Collections;

public class SendGachaponData : MonoBehaviour 
{
	public GameObject model;

	public void PushButton()
	{
		GameObject selectedButton = GameObject.FindWithTag ("SelectMe");

		GameObject clone = Instantiate (model, selectedButton.GetComponent<RectTransform> ().position, Quaternion.identity) as GameObject;
		selectedButton.gameObject.SetActive (false);
		GameObject.FindWithTag ("GameController").GetComponent<BackSceneController>().AssignObject( selectedButton, clone);
	}
}
