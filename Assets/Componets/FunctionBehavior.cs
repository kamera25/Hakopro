using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public enum SCRIPTTYPE 
{
	NONE 		= -999,
	SWITCHRESET	= -302,
	SWITCHBLUE 	= -301,
	SWITCHRED	= -300,
	CAPSEL		= -200,
	GOSIGN		= -101,
	STOPSIGN	= -100,
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
	SWAPARRAY	= 11,
	SUBSTITUTE	= 12,
	COUNTER		= 13,
	MINIMUMINDEX= 14,
	PRINT_LN	= 15,
	PRINT_LN_VAR= 16,
	BLACKHOLE	= 17,
	GREATER		= 18,
	LESS		= 19,
	EQUAL		= 20,
	REM			= 21
}

public class FunctionBehavior : MonoBehaviour 
{

	public List<GameObject> ElementsList = new List<GameObject>();
	private GameObject fukudashi;
	public SCRIPTTYPE scriptKind = SCRIPTTYPE.NONE;

	protected bool flag1 = false;

	protected void SwapFirst()
	{
		Swap ( 0, 1);
	}
	
	protected void SwapEnd()
	{
		Swap (ElementsList.Count - 2, ElementsList.Count - 1);
	}
	
	protected void SwapMiddle()
	{
		int count = ElementsList.Count / 2;
		Swap (count, count - 1);
	}
	
	protected void SwapArray( GameObject pram1, GameObject pram2)
	{
		
		if (pram1 == null || pram2 == null) 
		{
			return;
		}
		
		Swap (pram1.GetComponent<CardBehaviour> ().CardNumberForInt(), pram2.GetComponent<CardBehaviour> ().CardNumberForInt());
	}
	
	protected void Swap( int index1, int index2 )
	{
		if (ElementsList.Count - 1 < index1 || ElementsList.Count - 1 < index2) 
		{
			// Index error!!!
			return;
		}
		GameObject tempObj = ElementsList[index1];
		
		ElementsList [index1] = ElementsList [index2];
		ElementsList [index2] = tempObj;
	}
	
	protected void ExecStack()
	{
		int loopNumber = ElementsList.Count;
		
		for( int i = 1; i <= loopNumber; i++)
		{
			int index = loopNumber - i;
			ElementsList.Add(ElementsList[index]);
			ElementsList.RemoveAt(index);
		}
	}
	
	
	protected void Add( GameObject Obj)
	{
		if (Obj == null) 
		{
			return;
		}


		int num = Obj.GetComponent<CardBehaviour>().CardNumberForInt();
		ElementsList [0] = InstantiateCard ();
		ElementsList [0].GetComponent<CardBehaviour> ().Add ( num);

	}
	
	protected void Sub( GameObject Obj)
	{
		if (Obj == null) 
		{
			return;
		}
		
		int num = Obj.GetComponent<CardBehaviour>().CardNumberForInt();
		ElementsList [0] = InstantiateCard ();
		ElementsList [0].GetComponent<CardBehaviour> ().Sub ( num);
	}
	
	protected void Mul( GameObject Obj)
	{
		if (Obj == null) 
		{
			return;
		}
		
		int num = Obj.GetComponent<CardBehaviour>().CardNumberForInt();
		ElementsList [0] = InstantiateCard ();
		ElementsList [0].GetComponent<CardBehaviour> ().Mul ( num);
	}
	
	protected void Div( GameObject Obj)
	{
		if (Obj == null) 
		{
			return;
		}
		
		int num = Obj.GetComponent<CardBehaviour>().CardNumberForInt();
		ElementsList [0] = InstantiateCard ();
		ElementsList [0].GetComponent<CardBehaviour> ().Div ( num);
	}

	protected void Rem( GameObject Obj)
	{
		if (Obj == null) 
		{
			return;
		}
		
		int num = Obj.GetComponent<CardBehaviour>().CardNumberForInt();
		ElementsList [0] = InstantiateCard ();
		ElementsList [0].GetComponent<CardBehaviour> ().Rem ( num);
	}

	protected GameObject InstantiateCard()
	{
		GameObject obj = Instantiate ( Resources.Load("Prefab/Cards/Card")) as GameObject;
		obj.SendMessage ( "RestartElement");

		return obj;
	}
	
	protected void Counter( GameObject Obj)
	{
		if (Obj == null) 
		{
			return;
		}

		ThrowUpElement (Obj, "");

		return;
	}
	
	protected void FindMinimumIndex( GameObject Obj)
	{
		if (Obj == null) 
		{
			return;
		}
		
		
		int num = Obj.GetComponent<CardBehaviour>().CardNumberForInt();
		int minimamNum = 999;
		int minimam = -1;
		for (int i = num; i < ElementsList.Count; i++) 
		{
			CardBehaviour cardBhv = ElementsList[i].GetComponent<CardBehaviour>();
			if( cardBhv.CardNumberForInt() < minimamNum)
			{
				minimamNum = cardBhv.CardNumberForInt();
				minimam = i;
			}
		}

		ThrowUpElement (Obj, minimam.ToString ());
	}

	protected void ThrowUpElement( GameObject Obj, string newCardString)
	{
		GameObject clone = Instantiate ( Resources.Load("Prefab/Cards/Card"), this.transform.position + Vector3.up * 3F, Quaternion.identity) as GameObject;

		if( newCardString != "")
		{
			clone.SendMessage( "UpdateCardData", newCardString);
		}
		clone.SendMessage ( "RestartElement");
		clone.SendMessage( "SetAimPosition", this.transform.position + Vector3.up * 5F);
		clone.rigidbody2D.AddForce (Vector3.up * 10F, ForceMode2D.Impulse);

		clone.transform.parent = GameObject.Find ("Elements").transform;

		PlayOutPutSE ();

		return;
	}

	protected void PlayOutPutSE()
	{

	}
	
	protected void Print()
	{
		
		// Load Fukidashi <(___________) 
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
			switch (scriptKind) 
			{
			case SCRIPTTYPE.PRINT:
				fukidashiText.text = fukidashiText.text + ElementsList [0].name;
				break;
			case SCRIPTTYPE.PRINT_LN:
				fukidashiText.text = fukidashiText.text + ElementsList [0].name;
				break;
			case SCRIPTTYPE.PRINT_LN_VAR:
				fukidashiText.text = fukidashiText.text + ElementsList[0].GetComponent<CardBehaviour>().CardNumberForInt() + " ";
				break;
			}
		}
	}
	
	protected void Substitute( GameObject Obj)
	{
		CardBehaviour card = Obj.GetComponent<CardBehaviour> ();
		
		int num = ElementsList [ElementsList.Count - 1].GetComponent<CardBehaviour> ().CardNumberForInt ();
		card.SetVariable( num);
		
		
		// Assign variable all card of same name.
		GameObject[] cards = GameObject.FindGameObjectsWithTag ("Card");
		foreach( GameObject findCard in cards)
		{
			CardBehaviour cardBhv = findCard.GetComponent<CardBehaviour>();
			if( cardBhv.cardString == card.cardString)
			{
				cardBhv.SetVariable( num);
			}
		}
	}

	protected void Sign( BoxCollider2D col)
	{
		if(flag1)
		{
			col.enabled = false;
		}
		else
		{
			col.enabled = true;
		}
	}

	protected void PushBlue()
	{
		if (scriptKind == SCRIPTTYPE.STOPSIGN) 
		{
			this.GetComponent<BoxCollider2D>().enabled = false;
			Sprite sign = Load( "Element", "GoSign");
			this.GetComponent<Image>().sprite = sign;
		}
	}

	protected void Less( GameObject pram1, GameObject pram2)
	{
		int num1 = pram1.GetComponent<CardBehaviour>().CardNumberForInt();
		int num2 = pram2.GetComponent<CardBehaviour>().CardNumberForInt();

		if( num1 < num2)
		{
			flag1 = true;
		}
		else
		{
			flag1 = false;
		}
	}

	protected void Greater( GameObject pram1, GameObject pram2)
	{
		int num1 = pram1.GetComponent<CardBehaviour>().CardNumberForInt();
		int num2 = pram2.GetComponent<CardBehaviour>().CardNumberForInt();
		
		if( num1 > num2)
		{
			flag1 = true;
		}
		else
		{
			flag1 = false;
		}

	}

	protected void Equal( GameObject pram1, GameObject pram2)
	{
		int num1 = pram1.GetComponent<CardBehaviour>().CardNumberForInt();
		int num2 = pram2.GetComponent<CardBehaviour>().CardNumberForInt();
		
		if( num1 == num2)
		{
			flag1 = true;
		}
		else
		{
			flag1 = false;
		}

	}

	protected void SendMesseageForButtons( string method)
	{
		GameObject[] buttons = GameObject.FindGameObjectsWithTag("Function");
		foreach( GameObject button in buttons)
		{
			button.SendMessage(method);
		}
	}

	public static Sprite Load (string fileName , string spriteName) {
		// Resoucesから対象のテクスチャから生成したスプライト一覧を取得
		Sprite[] sprites = Resources.LoadAll<Sprite>(fileName);
		// 対象のスプライトを取得
		return System.Array.Find<Sprite>( sprites, (sprite) => sprite.name.Equals(spriteName));
	}
}
