using UnityEngine;
using System.Collections;

public class LoadMenuScene : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
		Invoke ("LoadMenu", 5F);
	}
	
	void LoadMenu()
	{
		Application.LoadLevel("Menu");
	}
}
