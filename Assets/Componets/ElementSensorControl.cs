using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ElementSensorControl : MonoBehaviour 
{

	public int elementCount = 0;

	void OnTriggerEnter2D(Collider2D collision) 
	{
		
		if( collision.gameObject.CompareTag("Element") || collision.gameObject.CompareTag("Card"))
		{
			elementCount++;
		}
	}

	void OnTriggerExit2D(Collider2D collision) 
	{
		
		if( collision.gameObject.CompareTag("Element") || collision.gameObject.CompareTag("Card"))
		{
			elementCount--;
		}
	}
}
