﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PalletButtonBehavior : MonoBehaviour 
{

	public SCRIPTTYPE scriptKind;
	public int useCount = 1;

	private Text text;

	void Start()
	{
		text = this.transform.FindChild("Text").GetComponent<Text> ();
	}

	void Update()
	{
		text.text = useCount + "コ";
	}

	void ApplyChangeButton()
	{
		StartConveyor convCtr = GameObject.FindWithTag ("GameController").GetComponent<StartConveyor>();

		if (useCount == 0 || convCtr.changeObj == null) 
		{
						return;
		}

		useCount--;
		convCtr.ApplyChangeButton( this.gameObject, scriptKind);
	}
}
