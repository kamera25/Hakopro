using UnityEngine;
using System.Collections;

public class BlockPalletControl : MonoBehaviour 
{
	GameObject controller;
	LogRecodeController logRecCtrl;

	void Awake()
	{
		controller = GameObject.FindWithTag ("GameController");
		logRecCtrl = controller.GetComponent<LogRecodeController> ();
	}

	public void PushReloadButton()
	{
		controller.SendMessage ("SceneReset");
		logRecCtrl.RecordData ( "1, Push ReloadButton.");
	}

	public void PutMissionButton()
	{
		controller.SendMessage ("PutMissionPanel");
		logRecCtrl.RecordData ( "2, Put Mission.");
	}

	public void PushAttentionButton()
	{
		controller.SendMessage ("PushLookUpButton");
		logRecCtrl.RecordData ( "3, Push AttetionButton.");
	}

	public void PushOneBackButton()
	{
		controller.SendMessage ("PushBackButton");
		logRecCtrl.RecordData ("4, Undo.");
	}

	public void PushPlayButton()
	{
		controller.SendMessage ("ExecConveyor");
		logRecCtrl.RecordData ( "5, Push StartButton.");
	}

	public void PushBackToMenu()
	{
		controller.SendMessage ("PutExitDialog");
		logRecCtrl.RecordData ( "6, Push BackToMenuButton.");
	}

}
