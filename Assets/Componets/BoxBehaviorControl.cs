using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public enum SCRIPTTYPE 
{
	NONE 		= -999,
	FLAGGREEN	= -1,
	SWAPFIRST	= 0,
	SWAPEND		= 1,
	GOTOGREEN	= 2,
	SWAPMIDDLE	= 3,
	PRINT		= 4,
	STACK		= 5
}

public class BoxBehaviorControl : MonoBehaviour 
{

	public SCRIPTTYPE scriptKind = SCRIPTTYPE.NONE;
	public List<GameObject> ElementsList = new List<GameObject>();

	public float weitTime = 4F;
	private float nowWaitTime;
	public int maxExec = 1;
	public bool addElements = false;
	private bool execDecision = false;

	private bool putParameta = false;

	public ElementSensorControl elementSensor;
	

	// Update is called once per frame
	void Update () 
	{
		/*
		if (putParameta == false && scriptKind == SCRIPTTYPE.GOTOGREEN) 
		{
			GameObject obj = GameObject.Find("BlockPalletCanvas").transform.Find("GotoPanel").gameObject;
			obj.SetActive(true);
			GameObject.Find("Background").SendMessage("SetLock");

			putParameta = true;
		}
		if (putParameta == true) {
						GameObject obj = GameObject.Find ("GotoPanel");
						
						if (obj != null) {	
								obj = obj.transform.FindChild("Slider").gameObject;
								maxExec = (int)(obj.GetComponent<Slider> ()).value + 1;
						}
		}
		*/



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
					case SCRIPTTYPE.SWAPMIDDLE:
						SwapMiddle();
						break;
					case SCRIPTTYPE.STACK:
						ExecStack();
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
		if( nowWaitTime < -1 && ElementsList.Count != 0 && !IsExistElemets())
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

				objRigid.AddForce( new Vector2( 0F, 15F), ForceMode2D.Impulse);
				if( aimObj != null)
				{
					float dump = 0.3F;

					Vector3 vec = aimObj.transform.position - this.transform.position;
					objRigid.AddForce( vec * dump, ForceMode2D.Impulse);
				}

				nowWaitTime = 2F; // slow..
			}
			else
			{
				if( scriptKind == SCRIPTTYPE.PRINT)
				{
					Print();
				}

				ElementsList[0].transform.position = this.transform.position + Vector3.right * 3F;
				nowWaitTime = 0F;
			}

			ElementsList.RemoveAt(0);
			if( ElementsList.Count == 0)
			{
				maxExec--;
				if( maxExec <= 0)
				{
					this.gameObject.SetActive(false);
				}
			}
		}

	}

	bool IsExistElemets()
	{
		return elementSensor.elementCount > 0;
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

	void SwapMiddle()
	{
		int count = ElementsList.Count / 2;
		GameObject tempObj = ElementsList[count];

		ElementsList [count] = ElementsList [count-1];
		ElementsList [count-1] = tempObj;
	}

	void ExecStack()
	{
		int loopNumber = ElementsList.Count;

		for( int i = 1; i <= loopNumber; i++)
		{
			int index = loopNumber - i;
			ElementsList.Add(ElementsList[index]);
			ElementsList.RemoveAt(index);
		}
	}

	private GameObject fukudashi;
	void Print()
	{
		if (fukudashi == null) 
		{
			fukudashi = Instantiate( Resources.Load( "Prefab/Fukidashi"), this.transform.position + Vector3.up * 3F, Quaternion.identity) as GameObject;
			fukudashi.transform.parent = GameObject.Find("Canvas").transform;
		}

		Text fukidashiText = fukudashi.transform.FindChild ("Text").GetComponent<Text> ();

		fukidashiText.text = fukidashiText.text + ElementsList [0].name;

	}

	void OnTriggerEnter2D(Collider2D collision) 
	{
		
		if( collision.gameObject.CompareTag("Element") && 0 <= (int)scriptKind)
		{
			collision.transform.position = new Vector2( 999F, -999F);
			ElementsList.Add(collision.gameObject);
			
			nowWaitTime = weitTime;
			addElements = true;
			execDecision = false;
		}
		
	}
}
