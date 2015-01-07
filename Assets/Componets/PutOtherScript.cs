using UnityEngine;
using System.Collections;

public class PutOtherScript : MonoBehaviour 
{
	void PutHelpDialog()
	{
		GameObject obj = GameObject.FindWithTag ("SelectMeFunction");

		GameObject dialog = Instantiate ( Resources.Load( "BoxHelpDialog")) as GameObject;
		obj.tag = "Function";
		dialog.SendMessage ("UpdateText", obj.GetComponent<FlowBoxBehaviorControl>().scriptKind);

	}
}
