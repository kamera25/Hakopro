using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class DecisionController : MonoBehaviour 
{

	public GameObject clearTextUI;
	public GameObject notClearTextUI;

	public List<string> ElementsList = new List<string>();
	public List<string> CorrectElementsList = new List<string>();
	private bool inputObj = false;

	[SerializeField] int stageNum = 0;
	
	private Animator anim;

	private AudioSource audioSource;
	private AudioClip clapSE;
	private AudioClip absorbSE;
	private AudioClip beepSE;
	

	// Use this for initialization
	void Start () 
	{
		anim = this.GetComponent<Animator> ();

		audioSource = GameObject.FindWithTag ("GameController").GetComponent<AudioSource>();

		clapSE = Resources.Load ("clap04_fade") as AudioClip;
		absorbSE = Resources.Load ("push24") as AudioClip;
		beepSE = Resources.Load ("beep14") as AudioClip;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!inputObj) 
		{
			return;
		}

		if (!IsMatchListCount ()) 
		{
			bool ret = CheckNowMatchList ();
			if ( !ret ) 
			{
				FailureProcess ();
			}
		} 
		else 
		{
			bool ret = ExecDicision();
			if( ret )
			{
				ClearProcess();
			}
			else 
			{
				FailureProcess();
			}
		}


		inputObj = false;
	}

	bool ExecDicision()
	{

		// Dosent match numbers.
		if ( !IsMatchListCount()) 
		{
			return false;
		}

		bool ret = CheckNowMatchList ();
		return ret;
	}

	bool CheckNowMatchList()
	{
		int i = 0;
		foreach (string element in ElementsList) 
		{
			// if dosent match a array of correct names.
			if( element != CorrectElementsList[i] )
			{
				return false;
			}
			i++;
		}

		return true;
	}

	bool IsMatchListCount()
	{
		return ElementsList.Count == CorrectElementsList.Count;
	}

	void ClearProcess()
	{
		clearTextUI.SetActive(true);
		audioSource.PlayOneShot(clapSE);
		Invoke("GoToMenu", 3F);
		this.enabled = false;
	}

	void FailureProcess()
	{
		notClearTextUI.SetActive(true);
		audioSource.PlayOneShot(beepSE);
		this.enabled = false;
	}

	void GoToMenu()
	{
		int flag = PlayerPrefs.GetInt ("ClearFlag");
		int condition = 1 << (stageNum-1);

		flag = flag | condition;
		PlayerPrefs.SetInt ("ClearFlag", flag);
		Application.LoadLevel ("Menu");
	}

	void OnCollisionEnter2D(Collision2D collision) 
	{

		if( collision.gameObject.CompareTag("Element") || collision.gameObject.CompareTag("Card"))
		{
			collision.gameObject.SendMessage("DropElement");

			AddCardElementsList( collision.gameObject);
			anim.SetTrigger( "addObject");
			audioSource.PlayOneShot( absorbSE);

			inputObj = true;
		}
	}

	void AddCardElementsList( GameObject obj)
	{
		string str;

		CardBehaviour cardBhav = obj.GetComponent<CardBehaviour> ();

		if( cardBhav == null)
		{// Add objectname to ElementsList
			str = obj.name;
		}
		else
		{// This is a card.
			if( cardBhav.putInside) // It is variavle, and visible a assignning value.
			{
				str = cardBhav.CardNumberForInt().ToString();
			}
			else // It is variavle, and invisiable a assignning value.
			{
				str = obj.name;
			}
		}

		ElementsList.Add(str);
	}
}
