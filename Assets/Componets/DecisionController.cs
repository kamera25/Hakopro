using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DecisionController : MonoBehaviour 
{
	public GameObject clearTextUI;
	public GameObject notClearTextUI;

	public List<string> ElementsList = new List<string>();
	public List<string> CorrectElementsList = new List<string>();
	public float weitTime = 4F;
	private float nowWaitTime;
	public bool timeDecision = false;
	private bool startDecision = false;
	[SerializeField] int stageNum = 0;
	private Animator anim;

	private AudioSource audioSource;
	private AudioClip clapSE;
	private AudioClip absorbSE;
	private AudioClip beepSE;

	// Use this for initialization
	void Start () 
	{
		nowWaitTime = weitTime;
		anim = this.GetComponent<Animator> ();

		audioSource = GameObject.FindWithTag ("GameController").GetComponent<AudioSource>();

		clapSE = Resources.Load ("clap04_fade") as AudioClip;
		absorbSE = Resources.Load ("push24") as AudioClip;
		beepSE = Resources.Load ("beep14") as AudioClip;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!startDecision) 
						return;


		nowWaitTime -= Time.deltaTime;
		if (nowWaitTime < 0) 
		{
			bool ret = ExecDicision();
			if( ret )
			{
				clearTextUI.SetActive(true);
				audioSource.PlayOneShot(clapSE);
				Invoke("GoToMenu", 3F);
				this.enabled = false;
			}
			else if( timeDecision)
			{
				notClearTextUI.SetActive(true);
				audioSource.PlayOneShot(beepSE);
				this.enabled = false;
			}

		}
	}

	bool ExecDicision()
	{
		if (ElementsList.Count != CorrectElementsList.Count) 
		{
			return false;
		}

		for( int i = 0; i < ElementsList.Count; i++)
		{
			if( ElementsList[i] != CorrectElementsList[i])
			{
				return false;
			}
		}

		return true;
	}

	void GoToMenu()
	{
		int flag = PlayerPrefs.GetInt ("ClearFlag");
		flag = flag | stageNum;
		PlayerPrefs.SetInt ("ClearFlag", flag);
		Application.LoadLevel ("Menu");
	}

	void OnCollisionEnter2D(Collision2D collision) 
	{

		if( collision.gameObject.CompareTag("Element") || collision.gameObject.CompareTag("Card"))
		{
			collision.transform.position = new Vector2( 999F, 999F);
			ElementsList.Add(collision.gameObject.name);
			anim.SetTrigger( "addObject");
			audioSource.PlayOneShot( absorbSE);

			nowWaitTime = weitTime;
			startDecision = true;
		}

	}
}
