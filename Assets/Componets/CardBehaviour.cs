using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CardBehaviour : MonoBehaviour 
{
	public string cardString = "";
	private Text cardText;
	public int variable = 0;

	// Use this for initialization
	void Awake () 
	{
		cardText = this.transform.FindChild ("Canvas").FindChild ("Text").GetComponent<Text> ();

		UpdateCardData ( cardString);
	}
	

	void UpdateCardData( string text)
	{
		cardString = text;
		cardText.text = text;
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


	// Update is called once per frame
	void Update () 
	{
	}
}
