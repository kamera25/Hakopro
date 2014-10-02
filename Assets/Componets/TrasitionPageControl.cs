using UnityEngine;
using System.Collections;

public class TrasitionPageControl : MonoBehaviour 
{
	public int maxPage = 1;
	public int nowPage = 0;
	public GameObject canvasParent;

	void TransitionNextPage()
	{
		canvasParent.transform.Find ("Page" + (nowPage + 1)).gameObject.SetActive (false);
		nowPage++;

		nowPage = nowPage % maxPage;

		canvasParent.transform.Find ("Page" + (nowPage + 1)).gameObject.SetActive (true);
	}
}
