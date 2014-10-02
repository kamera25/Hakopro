using UnityEngine;
using System.Collections;

public class ElemetsBehavior : MonoBehaviour 
{
	public bool isStart = false;
	public bool onBelt = false;

	void Update()
	{
		if ( isStart && onBelt) 
		{
			this.rigidbody2D.AddForce( new Vector2( 10F, 0F));
		}
	}

	void OnCollisionEnter2D( Collision2D col)
	{
		if( col.collider.CompareTag("BeltConver"))
		{
			onBelt = true;
		}
	}

	void OnCollisionExit2D( Collision2D col)
	{
		if( col.collider.CompareTag("BeltConver"))
		{
			onBelt = false;
		}
	}
}
