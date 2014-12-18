using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BackSceneController : MonoBehaviour 
{
	private List<GameObject> qButtonList = new List<GameObject>();
	private List<GameObject> qObjectList = new List<GameObject>();

	public void AssignObject( GameObject button, GameObject obj)
	{
		qButtonList.Add (button);
		qObjectList.Add (obj);
	}

	public void PushBackButton()
	{
		int nowCount = qButtonList.Count - 1;

		if (nowCount == -1) 
		{
			return;
		}

		qButtonList [nowCount].SetActive (true);
		Destroy (qObjectList [nowCount]);

		qButtonList.RemoveAt (nowCount);
		qObjectList.RemoveAt (nowCount);
	}
}
