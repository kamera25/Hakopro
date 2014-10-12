using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public enum SCRIPTTYPE 
{
	NONE 		= -999,
	FLAGBLUE	= -2,
	FLAGGREEN	= -1,
	SWAPFIRST	= 0,
	SWAPEND		= 1,
	GOTOGREEN	= 2,
	SWAPMIDDLE	= 3,
	PRINT		= 4,
	STACK		= 5,
	ADD			= 6,
	SUB			= 7,
	MUL			= 8,
	DIV			= 9,
	GOTOBLUE	= 10,
	SWAPARRAY	= 11
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
	private GameObject fukudashi;

	// relation parameter.
	private int param = 0;
	public List<SaucerBehavior> SaucerList = new List<SaucerBehavior>();

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
					case SCRIPTTYPE.ADD:
						Add();
						break;
					case SCRIPTTYPE.SUB:
						Sub();
						break;
					case SCRIPTTYPE.MUL:
						Mul();
						break;
					case SCRIPTTYPE.DIV:
						Div();
						break;
					case SCRIPTTYPE.SWAPARRAY:
						SwapArray();
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

				switch( scriptKind)
				{
					case SCRIPTTYPE.GOTOBLUE:
						GoToFlag( SCRIPTTYPE.FLAGBLUE);
						nowWaitTime = 2F; // slow..
						break;
					case SCRIPTTYPE.GOTOGREEN:
						GoToFlag( SCRIPTTYPE.FLAGGREEN);
						nowWaitTime = 2F; // slow..
						break;
					default:
						if( scriptKind == SCRIPTTYPE.PRINT) Print();
						ElementsList[0].transform.position = this.transform.position + Vector3.right * 3F;
						nowWaitTime = 0F;
				break;
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

	void GoToFlag( SCRIPTTYPE flag)
	{
		GameObject[] objs = GameObject.FindGameObjectsWithTag("Function");
		GameObject aimObj = null;
		
		foreach( GameObject obj in objs)
		{
			if( obj.GetComponent<BoxBehaviorControl>().scriptKind == flag)
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

		return;
	}

	bool IsExistElemets()
	{
		return elementSensor.elementCount > 0;
	}

	void SwapFirst()
	{
		Swap ( 0, 1);
	}

	void SwapEnd()
	{
		Swap (ElementsList.Count - 2, ElementsList.Count - 1);
	}

	void SwapMiddle()
	{
		int count = ElementsList.Count / 2;
		Swap (count, count - 1);
	}

	void SwapArray()
	{
		GameObject pram1 = SaucerList [0].onElement;
		GameObject pram2 = SaucerList [1].onElement;
		
		if (pram1 == null || pram2 == null) 
		{
			return;
		}
		
		Swap (pram1.GetComponent<CardBehaviour> ().CardNumberForInt(), pram2.GetComponent<CardBehaviour> ().CardNumberForInt());
	}

	void Swap( int index1, int index2 )
	{
		GameObject tempObj = ElementsList[index1];
		
		ElementsList [index1] = ElementsList [index2];
		ElementsList [index2] = tempObj;
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
	

	void Add()
	{
		GameObject Obj = SaucerList [0].onElement;

		if (Obj == null) 
		{
			return;
		}
		
		int num = Obj.GetComponent<CardBehaviour>().CardNumberForInt();
		ElementsList [0].GetComponent<CardBehaviour> ().Add ( num);
	}

	void Sub()
	{
		GameObject Obj = SaucerList [0].onElement;
		
		if (Obj == null) 
		{
			return;
		}
		
		int num = Obj.GetComponent<CardBehaviour>().CardNumberForInt();
		ElementsList [0].GetComponent<CardBehaviour> ().Sub ( num);
	}

	void Mul()
	{
		GameObject Obj = SaucerList [0].onElement;
		
		if (Obj == null) 
		{
			return;
		}
		
		int num = Obj.GetComponent<CardBehaviour>().CardNumberForInt();
		ElementsList [0].GetComponent<CardBehaviour> ().Mul ( num);
	}

	void Div()
	{
		GameObject Obj = SaucerList [0].onElement;
		
		if (Obj == null) 
		{
			return;
		}
		
		int num = Obj.GetComponent<CardBehaviour>().CardNumberForInt();
		ElementsList [0].GetComponent<CardBehaviour> ().Div ( num);
	}


	
	void Print()
	{
		if (fukudashi == null) 
		{
			fukudashi = Instantiate( Resources.Load( "Prefab/Fukidashi"), this.transform.position + Vector3.up * 3F, Quaternion.identity) as GameObject;
			fukudashi.transform.parent = GameObject.Find("Buttons").transform;
		}

		Text fukidashiText = fukudashi.transform.FindChild ("Text").GetComponent<Text> ();


		if (ElementsList [0].name == "NewLine") 
		{
			fukidashiText.text = fukidashiText.text + "\n";
		}
		else 
		{
			fukidashiText.text = fukidashiText.text + ElementsList [0].name;
		}


	}

	void OnTriggerEnter2D(Collider2D collision) 
	{
		
		if( collision.gameObject.CompareTag("Element") && 0 <= (int)scriptKind)
		{
			collision.transform.position = new Vector2( 999F, -999F);
			ElementsList.Add(collision.gameObject);
			
			if( scriptKind == SCRIPTTYPE.ADD)
			{
				nowWaitTime = 0;
			}
			else
			{
				nowWaitTime = weitTime;
			}

			addElements = true;
			execDecision = false;
		}
		
	}

	// Initialize Function Box Process.
	public void InitChangeBox( SCRIPTTYPE scKind)
	{
		this.scriptKind = scKind;

		switch (scKind) 
		{
			case SCRIPTTYPE.SWAPARRAY:
				MakeSaucer( 2);
				break;
			case SCRIPTTYPE.ADD:
			case SCRIPTTYPE.SUB:
			case SCRIPTTYPE.DIV:
			case SCRIPTTYPE.MUL:
				MakeSaucer(1);
				break;
		}

		return;
	}

	public void MakeSaucer( int param)
	{
		this.param = param;

		for (int i = 1; i <= param; i++) 
		{
			Vector3 pos = this.transform.position + Vector3.up * 2.1F + Vector3.right * (i*2 - param) * 1.1F + Vector3.left * 1.3F;
			GameObject clone = Instantiate( Resources.Load("Prefab/Saucer"), pos,Quaternion.identity) as GameObject;
			SaucerList.Add(clone.GetComponent<SaucerBehavior>());
		}

	}

	
}
