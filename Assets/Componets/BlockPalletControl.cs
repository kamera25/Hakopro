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
		logRecCtrl.RecordData ( "Push ReloadButton.");
	}

	public void PutMissionButton()
	{
		controller.SendMessage ("PutMissionPanel");
		logRecCtrl.RecordData ( "Put Mission.");
	}

	public void PushAttentionButton()
	{
		controller.SendMessage ("PushLookUpButton");
		logRecCtrl.RecordData ( "Push AttetionButton.");
	}

	public void PushOneBackButton()
	{
		controller.SendMessage ("PushBackButton");
		logRecCtrl.RecordData ("Undo.");
	}

	public void PushPlayButton()
	{
		controller.SendMessage ("ExecConveyor");
		logRecCtrl.RecordData ( "Push StartButton.");
	}

	public void PushBackToMenu()
	{
		controller.SendMessage ("PutExitDialog");
		logRecCtrl.RecordData ( "Push BackToMenuButton.");
	}

}
