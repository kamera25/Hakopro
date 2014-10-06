using UnityEngine;
using System.Collections;

public class BeltCircleBehavior : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		if ( this.transform.parent.CompareTag ("BeltConveyor_R")) 
		{
			this.GetComponent<Animator>().SetBool("isRight", false);
		}
	}
}
