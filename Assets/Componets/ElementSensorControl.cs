using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ElementSensorControl : MonoBehaviour 
{

	public int elementCount = 0;

	void OnTriggerEnter2D(Collider2D collision) 
	{
		
		if( collision.gameObject.CompareTag("Element"))
		{
			elementCount++;
		}
	}

	void OnTriggerExit2D(Collider2D collision) 
	{
		
		if( collision.gameObject.CompareTag("Element"))
		{
			elementCount--;
		}
	}
}
