using UnityEngine;
using System.Collections;

public class ElemetsBehavior : MonoBehaviour 
{
	public bool isStart = false;
	private bool onBelt = false;
	private bool moveRight = false;

	private float force = 240F;

	void Update()
	{
		if (isStart && onBelt) 
		{
			if (moveRight) 
			{
				this.rigidbody2D.AddForce (new Vector2 (force * Time.deltaTime, 0F));
			}
			else
			{
				this.rigidbody2D.AddForce (new Vector2 (-force * Time.deltaTime, 0F));
			}
		}

		if (this.rigidbody2D.velocity.y > 1F) 
		{
			DisableCollision ();
		} 
		else 
		{
			EnableCollision();
		}
	}

	void OnCollisionEnter2D( Collision2D col)
	{
		if( col.collider.CompareTag("BeltConveyor_R"))
		{
			onBelt = true;
			moveRight = true;
		}
		else if( col.collider.CompareTag("BeltConveyor_L"))
		{
			onBelt = true;
			moveRight = false;
		}
	}

	void OnCollisionExit2D( Collision2D col)
	{
		if( col.collider.CompareTag("BeltConveyor_R") || col.collider.CompareTag("BeltConveyor_L"))
		{
			onBelt = false;
		}
	}

	public void DisableCollision()
	{
		this.GetComponent<BoxCollider2D> ().isTrigger = true;
	}

	public void EnableCollision()
	{
		this.GetComponent<BoxCollider2D> ().isTrigger = false;
	}
}
