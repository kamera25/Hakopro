using UnityEngine;
using System.Collections;

public class PutNewElement : MonoBehaviour 
{

	public void PushButton()
	{
		GameObject.FindWithTag ("GameController").SendMessage ( "RecordData", "11, Push QuestionButton : position=" + this.transform.position);
		this.tag = "SelectMe";
	}
}
