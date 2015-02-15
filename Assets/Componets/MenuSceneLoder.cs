using UnityEngine;
using System.Collections;

public class MenuSceneLoder : MonoBehaviour 
{
	public string scene;
	
	public void LoadSceneButton()
	{
		if (scene == null) 
		{
			Debug.LogError ("No set scene!!!");
			return;
		}

		Application.LoadLevel (scene);
	}
}
