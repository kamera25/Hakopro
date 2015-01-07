using UnityEngine;
using System.Collections;

public class BoxButton : MonoBehaviour 
{

	void NotifyButtonPallet()
	{
		GameObject controller = GameObject.FindWithTag ("GameController");
		controller.SendMessage ( "SetButtonPallet", this.gameObject);

		this.tag = "SelectMeFunction";
		controller.SendMessage ("PutHelpDialog");
	}
}
