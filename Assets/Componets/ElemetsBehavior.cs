using UnityEngine;
using System.Collections;

public class ElemetsBehavior : MonoBehaviour 
{
	public bool isStart = false;
	private bool onBelt = false;
	private bool moveRight = false;

	private float force = 240F;
	private Vector2 velocity = Vector2.zero;

	private Vector2 aimPosotion = Vector2.zero;
	private bool existAimPos = false;

	private BoxCollider2D boxCol;

	void Start()
	{
		boxCol = this.GetComponent<BoxCollider2D> ();
	}

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

		if (existAimPos) 
		{
			Vector2 vec = new Vector2( this.transform.position.x, this.transform.position.y) - aimPosotion;
			if( vec.sqrMagnitude < 10F)
			{
				EnableCollision();
				existAimPos = false;
			}
		}
	}

	void SetAimPosition( Vector3 pos)
	{
		aimPosotion = new Vector2 (pos.x, pos.y);
		DisableCollision ();
		existAimPos = true;
		return;
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
		boxCol.isTrigger = true;
	}

	public void EnableCollision()
	{
		boxCol.isTrigger = false;
	}

	public void PauseElement()
	{
		velocity = this.rigidbody2D.velocity;
		isStart = false;
		this.rigidbody2D.velocity = Vector2.zero;
	}

	public void RestartElement()
	{
		isStart = true;
		this.rigidbody2D.velocity = velocity;
	}
}
