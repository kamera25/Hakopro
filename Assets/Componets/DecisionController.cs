using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DecisionController : MonoBehaviour 
{
	enum DECISION
	{
		BYSTRING,
		BYVAR
	}

	public GameObject clearTextUI;
	public GameObject notClearTextUI;

	public List<GameObject> ElementsList = new List<GameObject>();
	public List<string> CorrectElementsList = new List<string>();
	public bool timeDecision = false;

	public float weitTime = 4F;
	private float nowWaitTime;

	[SerializeField] int stageNum = 0;
	[SerializeField] DECISION decision;

	private bool startDecision = false;
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
		switch (decision) 
		{
			case DECISION.BYSTRING:
				if (ElementsList.Count != CorrectElementsList.Count) 
				{
					return false;
				}
				
				for( int i = 0; i < ElementsList.Count; i++)
				{
					CardBehaviour card = ElementsList[i].GetComponent<CardBehaviour>();
					if( card != null)
					{

						if( card.putInside)
						{
							if( card.CardNumberForInt().ToString() == CorrectElementsList[i])
						    {
								continue;
							}
							else
							{
								return false;
							}
						}
					}

					if( ElementsList[i].name != CorrectElementsList[i])
					{
							return false;
					}

				}
				
				return true;
			case DECISION.BYVAR:
				if( ElementsList[0].GetComponent<CardBehaviour>().CardNumberForInt() == int.Parse( CorrectElementsList[0]))
				{
					return true;
				}
			break;
		}

		return false;
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
			collision.transform.position = new Vector2( 999F, 999F);
			ElementsList.Add(collision.gameObject);
			anim.SetTrigger( "addObject");
			audioSource.PlayOneShot( absorbSE);

			nowWaitTime = weitTime;
			startDecision = true;
		}

	}
}
