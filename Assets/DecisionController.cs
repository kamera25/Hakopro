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
	public bool startDecision = false;
	private Animator anim;

	// Use this for initialization
	void Start () 
	{
		nowWaitTime = weitTime;
		anim = this.GetComponent<Animator> ();
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
			}
			else
			{
				notClearTextUI.SetActive(true);
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

	void OnCollisionEnter2D(Collision2D collision) 
	{

		if( collision.gameObject.CompareTag("Element"))
		{
			collision.transform.position = new Vector2( 999F, 999F);
			ElementsList.Add(collision.gameObject.name);
			anim.SetTrigger( "addObject");

			nowWaitTime = weitTime;
			startDecision = true;
		}

	}
}
