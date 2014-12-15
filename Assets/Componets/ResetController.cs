using UnityEngine;
using System.Collections;

public class ResetController : MonoBehaviour 
{
	public void SceneReset()
	{
		Application.LoadLevel (Application.loadedLevel);
	}

	public void BackToMenu()
	{
		Application.LoadLevel ("Menu");
	}
}
