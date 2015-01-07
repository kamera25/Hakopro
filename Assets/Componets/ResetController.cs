using UnityEngine;
using System.Collections;

public class ResetController : MonoBehaviour 
{
	public void SceneReset()
	{
		Application.LoadLevel (Application.loadedLevel);
	}

	public void BackToMenu()
	{
		GameObject controller = GameObject.FindWithTag ("GameController");
		controller.SendMessage ( "RecordData", "Retire...");
		controller.SendMessage ( "WriteFile");

		Application.LoadLevel ("Menu");
	}
}
