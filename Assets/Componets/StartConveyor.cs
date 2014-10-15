using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class StartConveyor : MonoBehaviour {

	[SerializeField] GameObject missionPanel;
	private bool isStart = false;
	//public GameObject[] elements;
	public List<GameObject> elements = new List<GameObject>();

	public GameObject changeObj;

	private AudioSource audioSource;
	private AudioClip execSE;

	// Use this for initialization
	void Start () 
	{
		Invoke ("DisableMissionPanel", 4F);


		elements = new List<GameObject>( GameObject.FindGameObjectsWithTag("Element"));
		elements.AddRange ( GameObject.FindGameObjectsWithTag ("Card"));

		audioSource = this.GetComponent<AudioSource> ();
		execSE = Resources.Load ("chime00") as AudioClip;
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

		audioSource.PlayOneShot (execSE);
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
		changeObj.GetComponent<BoxBehaviorControl>().InitChangeBox( scKind);
	}

	public void Pause()
	{
		// Elements.
		List<GameObject> elements = new List<GameObject>( GameObject.FindGameObjectsWithTag ("Element"));
		elements.AddRange ( GameObject.FindGameObjectsWithTag ("Card"));
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
