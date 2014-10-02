using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum SCRIPTTYPE 
{
	NONE 		= -999,
	FLAGGREEN	= -1,
	SWAPFIRST	= 0,
	SWAPEND		= 1,
	GOTOGREEN	= 2
}

public class BoxBehaviorControl : MonoBehaviour 
{

	public SCRIPTTYPE scriptKind = SCRIPTTYPE.NONE;
	public List<GameObject> ElementsList = new List<GameObject>();

	public float weitTime = 4F;
	private float nowWaitTime;
	public bool addElements = false;
	private bool execDecision = false;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!addElements) 
			return;

		nowWaitTime -= Time.deltaTime;
		if (nowWaitTime < 0 ) 
		{
			if( !execDecision)
			{
				switch( scriptKind)
				{
					case SCRIPTTYPE.SWAPFIRST:
						SwapFirst();
						break;
					case SCRIPTTYPE.SWAPEND:
						SwapEnd();
						break;
				}

				execDecision = true;
			}
			else
			{
				PopElement();
			}
		}
	}

	void PopElement()
	{
		if( nowWaitTime < -1 && ElementsList.Count != 0)
		{
			if( scriptKind == SCRIPTTYPE.GOTOGREEN)
			{
				GameObject[] objs = GameObject.FindGameObjectsWithTag("Function");
				GameObject aimObj = null;

				foreach( GameObject obj in objs)
				{
					if( obj.GetComponent<BoxBehaviorControl>().scriptKind == SCRIPTTYPE.FLAGGREEN)
					{
						aimObj = obj;
						break;
					}
				}

				ElementsList[0].transform.position = this.transform.position + Vector3.up * 3F;

				Rigidbody2D objRigid = ElementsList[0].GetComponent<Rigidbody2D>();
				objRigid.Sleep();

				objRigid.AddForce( new Vector2( 0F, 10F), ForceMode2D.Impulse);
				if( aimObj != null)
				{
					float dump = 0.4F;

					Vector3 vec = aimObj.transform.position - this.transform.position;
					objRigid.AddForce( vec * dump, ForceMode2D.Impulse);
					Debug.Log(vec);
				}

				nowWaitTime = 2F; // slow..
			}
			else
			{
				ElementsList[0].transform.position = this.transform.position + Vector3.right * 3F;
				nowWaitTime = 0F;
			}

			ElementsList.RemoveAt(0);

		}

	}

	void SwapFirst()
	{
		GameObject tempObj = ElementsList[0];

		ElementsList [0] = ElementsList [1];
		ElementsList [1] = tempObj;
	}

	void SwapEnd()
	{
		GameObject tempObj = ElementsList[ElementsList.Count-2];
		
		ElementsList [ElementsList.Count-2] = ElementsList [ElementsList.Count-1];
		ElementsList [ElementsList.Count-1] = tempObj;
	}

	void OnTriggerEnter2D(Collider2D collision) 
	{
		
		if( collision.gameObject.CompareTag("Element") && 0 <= (int)scriptKind)
		{
			collision.transform.position = new Vector2( 999F, -999F);
			ElementsList.Add(collision.gameObject);
			
			nowWaitTime = weitTime;
			addElements = true;
		}
		
	}
}
