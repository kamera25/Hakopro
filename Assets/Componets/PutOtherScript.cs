using UnityEngine;
using System.Collections;

public class PutOtherScript : MonoBehaviour 
{
	void PutHelpDialog()
	{
		GameObject obj = GameObject.FindWithTag ("SelectMeFunction");

		GameObject dialog = Instantiate ( Resources.Load( "BoxHelpDialog")) as GameObject;
		SCRIPTTYPE scriptType = obj.GetComponent<FlowBoxBehaviorControl> ().scriptKind;

		obj.tag = "Function";
		dialog.SendMessage ("UpdateText", scriptType);

		// Save a log.
		this.SendMessage ( "RecordData", "12, Watch a box help : script = " + scriptType.ToString() + ", "+ ((int)scriptType).ToString());
	}

	public void PutExitDialog()
	{
		GameObject dialog = Instantiate ( Resources.Load( "ExitDialog")) as GameObject;
	}
}
