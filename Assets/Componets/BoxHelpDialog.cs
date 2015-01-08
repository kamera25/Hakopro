using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Linq;

public class BoxHelpDialog : MonoBehaviour 
{
	[SerializeField] private Text subjectText;
	[SerializeField] private Text detailText;

	private BoxHelop_Sheet1 boxHelpData;
	private SCRIPTTYPE scType;

	public void Awake()
	{
		boxHelpData = Resources.Load("BoxHelp", typeof(BoxHelop_Sheet1)) as BoxHelop_Sheet1;
	}

	// Change subject and detail from XLS Data.
	public void UpdateText( SCRIPTTYPE type)
	{
		scType = type;

		BoxHelop_Sheet1.Param dataParam = boxHelpData.sheets [0].list
											.Where ( data => data.procedure == scType.ToString ())
											.FirstOrDefault ();
	
		subjectText.text = dataParam.name;
		detailText.text = dataParam.detail;

	}

	// Destroy this Dialog.
	public void DisableBoxHelp()
	{
		Destroy (this.gameObject);
	}
}
