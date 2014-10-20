using UnityEngine;
using System.Collections;

public class GameSoundController : MonoBehaviour 
{

	AudioSource audioSource;
	AudioClip tapSE;

	// Use this for initialization
	void Start () 
	{
		audioSource = this.GetComponent<AudioSource> ();
		tapSE = Resources.Load ("tm2_put002-1") as AudioClip;
	}
	
	void PlayTapSE()
	{
		audioSource.PlayOneShot (tapSE);
	}
}
