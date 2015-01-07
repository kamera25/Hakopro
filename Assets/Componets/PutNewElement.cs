using UnityEngine;
using System.Collections;

public class PutNewElement : MonoBehaviour 
{

	public void PushButton()
	{
		GameObject.FindWithTag ("GameController").SendMessage ( "RecordData", "Push QuestionButton : position=" + this.transform.position);
		this.tag = "SelectMe";
	}
}
