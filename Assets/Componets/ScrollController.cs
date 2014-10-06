using UnityEngine;
using System.Collections;

public class ScrollController : MonoBehaviour 
{
	[SerializeField] float minX = 0F;
	[SerializeField] float maxX = 14F;
	[SerializeField] float minY = 0F;
	[SerializeField] float maxY = 0F;
	
	private float beforeX = 0F;
	private float beforeY;
	private bool useScroll = false;

	private bool isLock = false;

	// Update is called once per frame
	void Update () 
	{
		if (isLock) 
		{
			return;
		}

		if (Input.GetMouseButton(0)) 
		{
			Vector3 mousePosWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast( mousePosWorld, Vector2.zero);					
			if( hit.collider != null)
			{
				if( hit.collider.gameObject == this.gameObject && useScroll)
				{
					float X = mousePosWorld.x - beforeX;
					float Y = mousePosWorld.y - beforeY;
					Vector3 pos = Camera.main.transform.position;

					pos.x = Mathf.Clamp( pos.x - X, minX, maxX);
					pos.y = Mathf.Clamp( pos.y - Y, minY, maxY);
					Camera.main.transform.position = pos;
				}

				mousePosWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);

				beforeX = mousePosWorld.x;
				beforeY = mousePosWorld.y;
				useScroll = true;
			}	
		}
		if (Input.GetMouseButtonUp (0)) 
		{
			useScroll = false;
		}

	}

	void SetLock()
	{
		isLock = true;
	}

	void LiftLock()
	{
		isLock = false;
	}
}
