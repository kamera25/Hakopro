using UnityEngine;
using System.Collections;

public class VariableIndicatorControl : MonoBehaviour 
{
	private CardBehaviour cardBhav;
	private int backNum;
	private Animator anim;
	private AudioSource audioSource;
	private AudioClip changeVarSE;

	// Use this for initialization
	void Start () 
	{
		anim = this.GetComponent<Animator> ();
		cardBhav = this.GetComponent<CardBehaviour> ();
		audioSource = GameObject.FindWithTag ("GameController").GetComponent<AudioSource>();
		changeVarSE = Resources.Load ("fm004") as AudioClip;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (cardBhav.variable != backNum) 
		{
			anim.SetTrigger( "changeVar");
			audioSource.PlayOneShot(changeVarSE);
		}

		backNum = cardBhav.variable;
	}
}
