using UnityEngine;
using System.Collections;

public class ElemetsBehavior : MonoBehaviour 
{
	public bool isStart = false;
	
	private Vector2 velocity = Vector2.zero;

	private Vector2 aimPosotion = Vector2.zero;
	public bool existAimPos = false;

	private BoxCollider2D boxCol;
	private AudioClip fallSound;
	private AudioSource audioSource;
	private Rigidbody2D regid;

	void Awake()
	{
		boxCol = this.GetComponent<BoxCollider2D> ();
		fallSound = Resources.Load ("bosu06") as AudioClip;
		audioSource = GameObject.FindWithTag ("GameController").GetComponent<AudioSource> ();
		regid = this.rigidbody2D;
	}

	void Update()
	{

		if (existAimPos) 
		{
			Vector2 vec = new Vector2( this.transform.position.x, this.transform.position.y) - aimPosotion;
			if( vec.sqrMagnitude < 4F && regid.velocity.y < 0F)
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
		if( col.collider.CompareTag("BeltConveyor_R") || col.collider.CompareTag("BeltConveyor_L"))
		{
			audioSource.PlayOneShot(fallSound);
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

}
