using UnityEngine;
using System.Collections;

public class SendGachaponData : MonoBehaviour 
{
	public GameObject model;

	public void PushButton()
	{
		GameObject.FindWithTag ("SelectMe").SendMessage ("CreateNewElement", model);
	}
}
