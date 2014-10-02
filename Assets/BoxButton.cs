using UnityEngine;
using System.Collections;

public class BoxButton : MonoBehaviour 
{

	void NotifyButtonPallet()
	{
		GameObject.FindWithTag ("GameController").SendMessage ( "SetButtonPallet", this.gameObject);
	}
}
