using UnityEngine;
using System.Collections;

public class LockBackGround : MonoBehaviour 
{
	GameObject backGround;

	void OnEnable()
	{
		if (backGround == null) 
		{
			backGround = GameObject.Find("Background");
		}

		backGround.SendMessage ("SetLock");
	}

	void OnDisable()
	{
		backGround.SendMessage ("LiftLock");
	}
}
