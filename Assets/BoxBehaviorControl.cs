using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoxBehaviorControl : MonoBehaviour 
{

	public int scriptKind = -1;
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
					case 0:
						SwapFirst();
						break;
					case 1:
						SwapEnd();
						break;
				}

				execDecision = true;
			}
			else
			{
				if( nowWaitTime < -1 && ElementsList.Count != 0)
				{
					ElementsList[0].transform.position = this.transform.position + Vector3.right * 3F;
					ElementsList.RemoveAt(0);
					nowWaitTime = 0F;
				}
			}
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
		
		if( collision.gameObject.CompareTag("Element") && scriptKind != -1)
		{
			collision.transform.position = new Vector2( 999F, -999F);
			ElementsList.Add(collision.gameObject);
			
			nowWaitTime = weitTime;
			addElements = true;
		}
		
	}
}
