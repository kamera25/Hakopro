using UnityEngine;
using System.Collections;

public class SaucerBehavior : MonoBehaviour 
{
	public GameObject onElement;

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.CompareTag ("Element") || col.gameObject.CompareTag ("Card")) 
		{
			if( onElement == null)
			{
				onElement = col.gameObject;
			}
		}
	}
	
	void OnTriggerExit2D(Collider2D col)
	{
		if ( onElement == col.gameObject) 
		{
			onElement = null;
		}
	}
	
}
