using UnityEngine;
using System.Collections;

public class ElemetsBehavior : MonoBehaviour 
{

	private Vector2 aimPosotion = Vector2.zero;
	public bool existAimPos = false;

	private BoxCollider2D boxCol;
	private AudioClip fallSound;
	private AudioSource audioSource;
	private Rigidbody2D regid;

	private Vector2 INFPOS = new Vector2 ( 999F, -999F);

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

	public void SetAimPosition( Vector3 pos)
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

	public void HideElement()
	{
		//boxCol.enabled = false;
		//this.GetComponent<SpriteRenderer> ().enabled = false;
		this.transform.position = INFPOS;
	}

	public void PopElement( Vector3 pos)
	{
		this.transform.position = pos;
		boxCol.enabled = true;
		this.GetComponent<SpriteRenderer> ().enabled = true;
	}

	public void DropElement()
	{
	//	lastPos = this.transform.position;
		this.transform.position = INFPOS;
	}

}
