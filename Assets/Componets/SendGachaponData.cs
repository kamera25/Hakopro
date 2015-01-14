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

		GameObject controller = GameObject.FindWithTag ("GameController");
		controller.GetComponent<BackSceneController>().AssignObject( selectedButton, clone);

		controller.SendMessage ( "RecordData", " -1, ->Select variavle :" + model.name);
	}
}
