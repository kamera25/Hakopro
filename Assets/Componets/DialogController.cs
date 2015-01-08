using UnityEngine;
using System.Collections;

public class DialogController : MonoBehaviour 
{
	public void DialogDestroy()
	{
		Destroy (this.gameObject);
	}

	public void BackToMenu()
	{
		GameObject.FindWithTag ("GameController").SendMessage("BackToMenu");
	}
}
