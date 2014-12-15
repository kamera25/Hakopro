using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum DIRECTION
{
	RIGHT = 1,
	LEFT = -1
}

public class BeltConveyorController : MonoBehaviour 
{
	private List<Rigidbody2D> ElementsList = new List<Rigidbody2D>();
	private bool isStart = false;
	private const float dump = 1.6F;
	private DIRECTION direction;

	void Start()
	{
		if (this.gameObject.CompareTag ("BeltConveyor_R")) 
		{
			direction = DIRECTION.RIGHT;
		} 
		else 
		{
			direction = DIRECTION.LEFT;
		}
	}

	void Update()
	{
		if (!isStart) 
		{
			return;
		}

		foreach( Rigidbody2D rig in ElementsList)
		{
			rig.AddForce( new Vector2( (float)direction * dump * Time.timeScale, 0F), ForceMode2D.Force);
		}

	}

	void StartBeltConveyor()
	{
		isStart = true;
	}

	void StopBeltConveyor()
	{
		isStart = false;
	}

	void OnCollisionEnter2D( Collision2D col)
	{
		if( IsElement( col.gameObject))
		{
			ElementsList.Add ( col.gameObject.rigidbody2D);
		}
	}
	
	void OnCollisionExit2D( Collision2D col)
	{
		if( IsElement( col.gameObject))
		{
			ElementsList.Remove ( col.gameObject.rigidbody2D);
		}
	}

	bool IsElement( GameObject obj)
	{
		return obj.CompareTag ("Element") || obj.CompareTag ("Card");
	}

}
