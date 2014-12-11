using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class StartConveyor : MonoBehaviour {

	[SerializeField] GameObject missionPanel;
	private bool isStart = false;
	[SerializeField] private List<ElemetsBehavior> elements = new List<ElemetsBehavior>();

	public GameObject changeObj;

	private AudioSource audioSource;
	private AudioClip execSE;

	// Use this for initialization
	void Start () 
	{
		Invoke ("DisableMissionPanel", 4F);

		UpdateElementsList ();

		audioSource = this.GetComponent<AudioSource> ();
		execSE = Resources.Load ("chime00") as AudioClip;
	}
	


	void ExecConveyor()
	{
		Time.timeScale = 2F;

		ConveyorCircleProc (true);
		isStart = true;

		foreach( ElemetsBehavior element in elements)
		{
			element.isStart = true;
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
		UpdateElementsList ();
		foreach( ElemetsBehavior element in elements)
		{
			element.SendMessage("PauseElement");
		}

		// Conveyor Circles.
		ConveyorCircleProc (false);

		Time.timeScale = 0F;
	}

	private void UpdateElementsList()
	{
		elements = GameObject.FindGameObjectsWithTag ("Element")
					.Concat ( GameObject.FindGameObjectsWithTag ("Card"))
					.Select ( ele => ele.GetComponent<ElemetsBehavior> ())
					.ToList ();
	}

	private void ConveyorCircleProc( bool isExec)
	{
		// Conveyor Circles.
		List<Animator> conveyorAnims = GameObject.FindGameObjectsWithTag ("BeltCircle")
										.Select (anim => anim.GetComponent<Animator> ())
										.ToList();

		foreach (Animator conveyorAnim in conveyorAnims) 
		{
			conveyorAnim.SetBool("isStart", isExec);
		}
	}
}
