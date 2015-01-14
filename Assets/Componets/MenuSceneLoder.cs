using UnityEngine;
using System.Collections;

public class MenuSceneLoder : MonoBehaviour 
{
	public string scene;
	private ScrollController scrollCtr;

	void Start()
	{
		scrollCtr = GameObject.Find("Panel_b").GetComponent<ScrollController> ();
	}

	public void LoadSceneButton()
	{
				if (scene == null) {
						Debug.LogError ("No set scene!!!");
						return;
				}

				if (scrollCtr.moveDist < 0.001F) {
						Application.LoadLevel (scene);
				}
	}
}
