using UnityEngine;
using System.Collections;

public class SaucerBehavior : MonoBehaviour 
{
	public GameObject onElement;

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.CompareTag ("Element")) 
		{
			onElement = col.gameObject;
		}
	}
	
	void OnCollisionExit2D(Collision2D col)
	{
		if ( onElement == col.gameObject) 
		{
			onElement = null;
		}
	}
}
