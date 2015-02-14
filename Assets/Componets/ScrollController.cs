﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScrollController : MonoBehaviour 
{
	[SerializeField] float minX = 0F;
	[SerializeField] float maxX = 14F;
	[SerializeField] float minY = 0F;
	[SerializeField] float maxY = 0F;
	[SerializeField] float maxScale = 10F;
	[SerializeField] float minScale = 5F;

	private float beforeX = 0F;
	private float beforeY;
	private bool useScroll = false;

	private bool usePC = true;

	private BoxCollider2D collider;
	private LookUpCardController lookUpCtl;

	private float interval = 0.0f;
	private bool isPitched = false;
	public bool isMenu = false;
	public float moveDist = 0F;
	const float ZOOM_SPEED = 600.0f;
	const float MOUSE_ZOOM_SPEED = 3.6F;

	private UnityEngine.EventSystems.EventSystem eventSystem;

	void Start()
	{
		#if UNITY_ANDROID
			usePC = false;
		#endif

		eventSystem = GameObject.Find ("EventSystem").GetComponent<UnityEngine.EventSystems.EventSystem> ();
		collider = this.GetComponent<BoxCollider2D> ();
		if( !isMenu)
		{
			lookUpCtl = GameObject.FindWithTag("GameController").GetComponent<LookUpCardController> ();
		}
	}

	// Update is called once per frame
	void Update () 
	{
		if ( !collider.enabled) 
		{
			return;
		}

		MoveCamera ();

		if (Input.GetMouseButtonUp (0)) 
		{
			useScroll = false;
		}

		ScaleProcess ();


	}

	void MoveCamera()
	{
		bool onUI = UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject ();// Check a pointer on UI.
		if (isPitched || onUI) 
		{
			return;
		}

		/*if (selectedObj != null) 
		{
			Debug.Log(selectedObj.name);
			if( selectedObj.transform.parent.name == "BlockPalletCanvas")
			{
				return;
			}
		}*/

		if (Input.GetMouseButton(0)) 
		{
			if( lookUpCtl != null)
			{
				lookUpCtl.isUse = false;// Change flag to false.
			}

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
					moveDist += Vector3.SqrMagnitude( Camera.main.transform.position - pos);
					Camera.main.transform.position = pos;
				}
				
				mousePosWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				
				beforeX = mousePosWorld.x;
				beforeY = mousePosWorld.y;
				useScroll = true;
			}	
		}
		else
		{
			moveDist = 0F;
		}

	}

	public bool IsOutOfStage( Vector3 pos)
	{
		return !((minX < pos.x && pos.x < maxX) && (minY < pos.y && pos.y < maxY));
	}

	void ScaleProcess()
	{
		if (usePC) {
			Camera.main.orthographicSize -= Input.GetAxis ("Mouse ScrollWheel") * MOUSE_ZOOM_SPEED;
		} 
		else 
		{
			if (Input.touchCount == 2) {
				if (Input.touches[0].phase == TouchPhase.Began || Input.touches[1].phase == TouchPhase.Began) {
					interval = Vector2.Distance(Input.touches[0].position, Input.touches[1].position);
				}
				float tmpInterval = Vector2.Distance(Input.touches[0].position, Input.touches[1].position);
				Camera.main.orthographicSize += (tmpInterval - interval) / ZOOM_SPEED;
				isPitched = true;
			}
			else
			{
				isPitched = false;
			}
		}
		
		Camera.main.orthographicSize = Mathf.Clamp (Camera.main.orthographicSize, minScale, maxScale);
	}

	void SetLock()
	{
		collider.enabled = false;
	}

	void LiftLock()
	{
		collider.enabled = true;
	}
}
