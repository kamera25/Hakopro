using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CardBehaviour : MonoBehaviour 
{
	public string cardString = "";
	private Text cardText;
	public int variable = 0;

	// Use this for initialization
	void Start () 
	{
		cardText = this.transform.FindChild ("Canvas").FindChild ("Text").GetComponent<Text> ();

		UpdateCardData ();
	}

	void UpdateCardData()
	{
		cardText.text = cardString;
		if (cardString == "\\n") 
		{
			this.name = "NewLine";
		}
		else 
		{
			this.name = cardString;
		}

		return;
	}

	public void Add( int num)
	{
		int textNum = CardNumberForInt();

		textNum += num;
		cardString = textNum.ToString();

		UpdateCardData ();
	}

	public void Sub( int num)
	{
		int textNum = CardNumberForInt();
		
		textNum -= num;
		cardString = textNum.ToString();
		
		UpdateCardData ();
	}

	public void Mul( int num)
	{
		int textNum = CardNumberForInt();
		
		textNum *= num;
		cardString = textNum.ToString();
		
		UpdateCardData ();
	}

	public void Div( int num)
	{
		int textNum = CardNumberForInt();
		
		textNum /= num;
		cardString = textNum.ToString();
		
		UpdateCardData ();
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
