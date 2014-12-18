using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class LookUpCardController : MonoBehaviour 
{
	public List<ElemetsBehavior> ElementsList = new List<ElemetsBehavior>();
	private ScrollController scrollCtl;
	private int nowLookUp = 0;
	public bool isUse = false;

	void Start()
	{
		scrollCtl = GameObject.FindWithTag ("Background").GetComponent<ScrollController>();
	}

	void Update()
	{
		if (isUse) 
		{
			Vector3 elepos = ElementsList [nowLookUp].transform.position;
			if (!scrollCtl.IsOutOfStage (elepos)) {
				Vector3 setPos = new Vector3 (elepos.x, elepos.y, Camera.main.transform.position.z);
				Camera.main.transform.position = setPos;
			}
			/*else
			{
				Vector2 lastElePos = ElementsList[nowLookUp].lastPos;
				Vector3 setPos = new Vector3 ( lastElePos.x, lastElePos.y, Camera.main.transform.position.z);
				Camera.main.transform.position = setPos;
			}*/
		}
	}

	private void UpdateElementsList()
	{
		ElementsList = GameObject.FindGameObjectsWithTag ("Element")
						.Concat (GameObject.FindGameObjectsWithTag ("Card"))
						.Where( go => scrollCtl.IsOutOfStage(go.transform.position) == false )
						.Select( go => go.GetComponent<ElemetsBehavior>())
						.ToList ();
	}

	public void PushLookUpButton()
	{
		UpdateElementsList ();

		nowLookUp++;
		nowLookUp = nowLookUp % ElementsList.Count;
		isUse = true;
	}

}
