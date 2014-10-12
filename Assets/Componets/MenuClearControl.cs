using UnityEngine;
using System.Collections;

public class MenuClearControl : MonoBehaviour {

	// Use this for initialization
	void Awake () 
	{

		// Put Clear Texts.
		int clearFlag = PlayerPrefs.GetInt ("ClearFlag");
		GameObject[] buttons = GameObject.FindGameObjectsWithTag("MenuButton");

		foreach (GameObject button in buttons) 
		{
			int textNum = int.Parse( button.name[button.name.Length-1].ToString());
			int condition = 1 << (textNum);

			if( (clearFlag & condition) == condition)
			{

				button.transform.FindChild("ClearText").gameObject.SetActive(true);
			}
		}


	}
}
