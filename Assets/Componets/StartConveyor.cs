using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class StartConveyor : MonoBehaviour {

	[SerializeField] GameObject missionPanel;
	private bool isStart = false;
	//[SerializeField] private List<ElemetsBehavior> elements = new List<ElemetsBehavior>();
	[SerializeField] public List<BeltConveyorController> beltConveyors = new List<BeltConveyorController> ();
	[SerializeField] private Slider playSlider;

	public GameObject changeObj;

	private AudioSource audioSource;
	private AudioClip execSE;

	// Use this for initialization
	void Start () 
	{
		Invoke ("DisableMissionPanel", 4F);

	//	UpdateElementsList ();
		UpdateConveyor ();

		audioSource = this.GetComponent<AudioSource> ();
		execSE = Resources.Load ("chime00") as AudioClip;
		Time.timeScale = 1F;
	}
	
	public void PutMissionPanle()
	{
		missionPanel.SetActive (true);
		Invoke ("DisableMissionPanel", 4F);
	}


	public void ExecConveyor()
	{
		Time.timeScale = 2F;

		ConveyorCircleProc (true);
		isStart = true;

		foreach( BeltConveyorController beltConveyor in beltConveyors)
		{
			beltConveyor.SendMessage("StartBeltConveyor");
		}

		audioSource.PlayOneShot (execSE);
	}

	public void ChangeTimeScale( float t)
	{
		Time.timeScale = playSlider.value / 2F;
		GameObject.FindWithTag ("GameController").SendMessage ( "RecordData", "Change execution speed : " + Time.timeScale);
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
		changeObj.GetComponent<FlowBoxBehaviorControl>().InitChangeBox( scKind);
	}

	public void Pause()
	{
		foreach( BeltConveyorController beltConveyor in beltConveyors)
		{
			beltConveyor.SendMessage("StopBeltConveyor");
		}

		// Conveyor Circles.
		ConveyorCircleProc (false);

		Time.timeScale = 0F;
	}

	private void UpdateConveyor()
	{
		beltConveyors = GameObject.FindGameObjectsWithTag ("BeltConveyor_R")
			.Concat( GameObject.FindGameObjectsWithTag ("BeltConveyor_L"))
				.Select( ele => ele.GetComponent<BeltConveyorController>())
				.ToList();
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
