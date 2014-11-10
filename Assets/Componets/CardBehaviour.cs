using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CardBehaviour : MonoBehaviour 
{
	public string cardString = "";
	private Text cardText;
	[SerializeField] private int variable = 0;
	[SerializeField] private bool putInside = false;
	private bool changeNum = false;

	// Use this for initialization
	void Awake () 
	{
		cardText = this.transform.FindChild ("Canvas").FindChild ("Text").GetComponent<Text> ();

		UpdateCardData ( cardString);
	}
	

	void UpdateCardData( string text)
	{
		cardString = text;
		if (putInside) 
		{
			cardText.text = CardNumberForInt().ToString();
		} 
		else 
		{
			cardText.text = text;
		}

		if (cardString == "\\n") 
		{
			this.name = "NewLine";
		}
		else 
		{
			this.name = text;
		}
	}

	public void Add( int num)
	{
		int textNum = CardNumberForInt();

		textNum += num;
		UpdateCardData (textNum.ToString());
	}

	public void Sub( int num)
	{
		int textNum = CardNumberForInt();
		
		textNum -= num;
		UpdateCardData (textNum.ToString());
	}

	public void Mul( int num)
	{
		int textNum = CardNumberForInt();
		
		textNum *= num;
		UpdateCardData (textNum.ToString());
	}

	public void Div( int num)
	{
		int textNum = CardNumberForInt();
		
		textNum /= num;
		UpdateCardData (textNum.ToString());
	}

	public void Rem( int num)
	{
		int textNum = CardNumberForInt();
		
		textNum %= num;
		UpdateCardData (textNum.ToString());
	}

	public int CardNumberForInt()
	{
		int num;


		if (int.TryParse (cardString, out num)) 
		{
			return num;
		} 
		else 
		{
			return variable;
		}
	}

	public void SetVariable( int num)
	{
		changeNum = true;
		variable = num;
	}

	// Update is called once per frame
	void Update () 
	{
		if (changeNum && putInside) 
		{
			cardText.text = CardNumberForInt().ToString();
		}
	}
}
