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
		Time.timeScale = 2F;

		GameObject[] conveyors = GameObject.FindGameObjectsWithTag("BeltCircle");

		foreach (GameObject conveyor in conveyors) 
		{
			conveyor.GetComponent<Animator>().SetBool("isStart", true);
		}

		isStart = true;

		foreach( GameObject element in elements)
		{
			element.GetComponent<ElemetsBehavior>().isStart = true;
			element.SendMessage("RestartElement");
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

	public void ApplyChangeButton( GameObject obj, SCRIPTTYPE scKind)
	{
		if (changeObj == null)
						return;
		changeObj.GetComponent<Image>().sprite = obj.GetComponent<Image>().sprite;
		changeObj.GetComponent<BoxBehaviorControl>().scriptKind = scKind;
	}

	public void Pause()
	{
		// Elemets.
		GameObject[] elements = GameObject.FindGameObjectsWithTag ("Element");
		foreach( GameObject element in elements)
		{
			element.SendMessage("PauseElement");
		}


		// Conveyor Circles.
		GameObject[] conveyors = GameObject.FindGameObjectsWithTag("BeltCircle");
		foreach (GameObject conveyor in conveyors) 
		{
			conveyor.GetComponent<Animator>().SetBool("isStart", false);
		}

		Time.timeScale = 0F;
	}
}
