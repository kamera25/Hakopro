using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class StartConveyor : MonoBehaviour {

	[SerializeField] GameObject missionPanel;
	private bool isStart = false;
	public GameObject[] elements;

	public GameObject changeObj;

	// Use this for initialization
	void Start () 
	{
		Invoke ("DisableMissionPanel", 4F);

		elements = GameObject.FindGameObjectsWithTag("Element");
	}
	


	void ExecConveyor()
	{
		GameObject[] conveyors = GameObject.FindGameObjectsWithTag("BeltCircle");

		foreach (GameObject conveyor in conveyors) 
		{
			conveyor.GetComponent<Animator>().SetBool("isStart", true);
		}

		isStart = true;

		foreach( GameObject element in elements)
		{
			element.GetComponent<ElemetsBehavior>().isStart = true;
		}
	}

	void DisableMissionPanel()
	{
		missionPanel.SetActive (false);
	}

	void SetButtonPallet( GameObject obj)
	{
		changeObj = obj;
	}

	public void ApplyChangeButton( GameObject obj, int scKind)
	{
		if (changeObj == null)
						return;
		changeObj.GetComponent<Image>().sprite = obj.GetComponent<Image>().sprite;
		changeObj.GetComponent<BoxBehaviorControl>().scriptKind = (SCRIPTTYPE) scKind;
	}
}
