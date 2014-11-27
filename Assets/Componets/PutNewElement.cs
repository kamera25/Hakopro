using UnityEngine;
using System.Collections;

public class PutNewElement : MonoBehaviour 
{
	public void PushButton()
	{
		this.tag = "SelectMe";
	}

	void CreateNewElement( GameObject model)
	{
		Instantiate (model, this.GetComponent<RectTransform> ().position, Quaternion.identity);
		Destroy ( this.gameObject);
	}

}
