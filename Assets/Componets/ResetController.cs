using UnityEngine;
using System.Collections;

public class ResetController : MonoBehaviour 
{
	
	void SceneReset()
	{
		Application.LoadLevel (Application.loadedLevel);
	}
}
