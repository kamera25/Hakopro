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
	public List<Rigidbody2D> ElementsList = new List<Rigidbody2D>();
	private bool isStart = false;
	private const float dump = 330F;
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
	

	void FixedUpdate()
	{
		
		if (!isStart) 
		{
			return;
		}
		
		foreach( Rigidbody2D rig in ElementsList)
		{
			// Check a missing object. when finding it, remove it.
			if( rig == null)
			{
				ElementsList.Remove( rig);
				break;
			}
			
			rig.AddForce( new Vector2( (float)direction * dump * Time.fixedDeltaTime, 0F), ForceMode2D.Force);
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
			ElementsList.Add ( col.gameObject.GetComponent<Rigidbody2D>());
		}
	}
	
	void OnCollisionExit2D( Collision2D col)
	{
		if( IsElement( col.gameObject))
		{
			ElementsList.Remove ( col.gameObject.GetComponent<Rigidbody2D>());
		}
	}

	bool IsElement( GameObject obj)
	{
		return obj.CompareTag ("Element") || obj.CompareTag ("Card");
	}

}
