using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class BoxBehaviorControl : FunctionBehavior
{
	public float weitTime = 6F;
	public float nowWaitTime;
	public int maxExec = 1;
	public bool addElements = false;
	private bool execDecision = false;

	public bool putRight = true;
	public ElementSensorControl elementSensor;

	private Animator animator;
	private AudioSource audioSource;
	private AudioClip explosionSE;
	private AudioClip absorbSE;
	private AudioClip outputSE;

	// relation parameter.
	private List<SaucerBehavior> SaucerList = new List<SaucerBehavior>();

	void Start()
	{
		InitChangeBox (scriptKind);
		animator = this.GetComponent<Animator> ();

		audioSource = GameObject.FindWithTag ("GameController").GetComponent<AudioSource>();
		explosionSE = Resources.Load ("crash28") as AudioClip;
		absorbSE = Resources.Load ("open24") as AudioClip;
		outputSE = Resources.Load ("open43") as AudioClip;
	}

	// Initialize Function Box Process.
	public void InitChangeBox( SCRIPTTYPE scKind)
	{
		this.scriptKind = scKind;
		
		switch (scKind) 
		{
		case SCRIPTTYPE.SWAPARRAY:
			MakeSaucer( 2);
			break;
		case SCRIPTTYPE.ADD:
		case SCRIPTTYPE.SUB:
		case SCRIPTTYPE.DIV:
		case SCRIPTTYPE.MUL:
		case SCRIPTTYPE.SUBSTITUTE:
		case SCRIPTTYPE.COUNTER:
		case SCRIPTTYPE.MINIMUMINDEX:
			MakeSaucer(1);
			break;
		}
		
		return;
	}

	// Update is called once per frame
	void Update () 
	{
		/*
		if (putParameta == false && scriptKind == SCRIPTTYPE.GOTOGREEN) 
		{
			GameObject obj = GameObject.Find("BlockPalletCanvas").transform.Find("GotoPanel").gameObject;
			obj.SetActive(true);
			GameObject.Find("Background").SendMessage("SetLock");

			putParameta = true;
		}
		if (putParameta == true) {
						GameObject obj = GameObject.Find ("GotoPanel");
						
						if (obj != null) {	
								obj = obj.transform.FindChild("Slider").gameObject;
								maxExec = (int)(obj.GetComponent<Slider> ()).value + 1;
						}
		}
		*/


		if (!addElements) 
			return;

		nowWaitTime -= Time.deltaTime;
		if (nowWaitTime < 0 ) 
		{
			if( !execDecision)
			{
				ExecScript();
			}
			else
			{
				PopElement();
			}
		}
	}

	void ExecScript()
	{
		switch( scriptKind)
		{
		case SCRIPTTYPE.SWAPFIRST:
			SwapFirst();
			break;
		case SCRIPTTYPE.SWAPEND:
			SwapEnd();
			break;
		case SCRIPTTYPE.SWAPMIDDLE:
			SwapMiddle();
			break;
		case SCRIPTTYPE.STACK:
			ExecStack();
			break;
		case SCRIPTTYPE.ADD:
			Add(SaucerList [0].onElement);
			break;
		case SCRIPTTYPE.SUB:
			Sub(SaucerList [0].onElement);
			break;
		case SCRIPTTYPE.MUL:
			Mul(SaucerList [0].onElement);
			break;
		case SCRIPTTYPE.DIV:
			Div(SaucerList [0].onElement);
			break;
		case SCRIPTTYPE.SWAPARRAY:
			SwapArray(SaucerList [0].onElement, SaucerList [1].onElement);
			break;
		case SCRIPTTYPE.SUBSTITUTE:
			Substitute(SaucerList [0].onElement);
			break;
		case SCRIPTTYPE.COUNTER:
			Counter(SaucerList [0].onElement);
			break;
		case SCRIPTTYPE.MINIMUMINDEX:
			FindMinimumIndex(SaucerList [0].onElement);
			break;
		}	
		
		execDecision = true;

		return;
	}

	void PopElement()
	{
		if( nowWaitTime < -1 && ElementsList.Count != 0 && !IsExistSideElement())
		{

			nowWaitTime = 0F;
		
			switch( scriptKind)
			{
				case SCRIPTTYPE.GOTOBLUE:
					GoToFlag( SCRIPTTYPE.FLAGBLUE);
					nowWaitTime = 3F; // slow..
					break;
				case SCRIPTTYPE.GOTOGREEN:
					GoToFlag( SCRIPTTYPE.FLAGGREEN);
					nowWaitTime = 3F; // slow..
					break;
				case SCRIPTTYPE.PRINT:  // Opeariton of PRINTs ... 
				case SCRIPTTYPE.PRINT_LN:
				case SCRIPTTYPE.PRINT_LN_VAR:
					Print ();
					PutElement( ElementsList[0], putRight);
					break;
				case SCRIPTTYPE.SUBSTITUTE:
					GameObject clone = Instantiate (SaucerList [0].onElement) as GameObject;
					clone.transform.parent = GameObject.Find ("Elements").transform;
					PutElement( clone, putRight);
					break;
				default:
					PutElement( ElementsList[0], putRight);
					break;
			}

			animator.SetTrigger("inElement");
			audioSource.PlayOneShot(outputSE);

			ElementsList.RemoveAt(0);
			CheckDestroyBox();
		}

	}

	void PutElement( GameObject obj, bool isRight)
	{
		if( isRight)
		{
			obj.transform.position = this.transform.position + Vector3.right * 3F;
		}	
		else 
		{
			obj.transform.position = this.transform.position + Vector3.left * 3F;
		}
	}

	void CheckDestroyBox()
	{
		if( ElementsList.Count == 0)
		{
			maxExec--;
			if( maxExec <= 0)
			{
				//ExplosionCards();
				audioSource.PlayOneShot(explosionSE);
				this.gameObject.SetActive(false);
			}
		}

		return;
	}

	/*
	void ExplosionCards()
	{
		foreach (SaucerBehavior saucer in SaucerList) 
		{
			saucer.onElement.SetActive(false);
			saucer.gameObject.SetActive(false);
		}
	}
	*/

	void GoToFlag( SCRIPTTYPE flag)
	{
		GameObject[] objs = GameObject.FindGameObjectsWithTag("Function");
		GameObject aimObj = null;

		// Search a flag of aiming.
		foreach( GameObject obj in objs)
		{
			if( obj.GetComponent<BoxBehaviorControl>().scriptKind == flag)
			{
				aimObj = obj;
				break;
			}
		}
		
		ElementsList[0].transform.position = this.transform.position + Vector3.up * 3F;
		
		Rigidbody2D objRigid = ElementsList[0].GetComponent<Rigidbody2D>();
		objRigid.Sleep();
		
		objRigid.AddForce( new Vector2( 0F, 15F), ForceMode2D.Impulse);
		if( aimObj != null)
		{
			float dump = 0.3F;
			Vector3 aimPos = aimObj.transform.position;

			ElementsList[0].SendMessage( "SetAimPosition", aimPos);

			Vector3 vec = aimPos - this.transform.position;
			objRigid.AddForce( vec * dump, ForceMode2D.Impulse);
		}

		return;
	}

	bool IsExistSideElement()
	{
		if (scriptKind == SCRIPTTYPE.BLACKHOLE) 
		{
			return true;
		}

		return elementSensor.elementCount > 0;
	}



	public void MakeSaucer( int param)
	{

		for (int i = 1; i <= param; i++) 
		{
			Vector3 pos = this.transform.position + Vector3.up * 2.1F + Vector3.right * (i*2 - param) * 1.1F + Vector3.left * 1.3F;
			GameObject clone = Instantiate( Resources.Load("Prefab/Saucer"), pos,Quaternion.identity) as GameObject;
			SaucerList.Add(clone.GetComponent<SaucerBehavior>());
			clone.transform.parent = this.transform;
		}

	}

	void OnTriggerEnter2D(Collider2D collision) 
	{
		
		if( ( collision.gameObject.CompareTag("Element") || collision.gameObject.CompareTag("Card")) 
		   && 0 <= (int)scriptKind)
		{
			if( collision.GetComponent<ElemetsBehavior>().existAimPos)
			{
				return;
			}

			collision.transform.position = new Vector2( 999F, -999F);
			ElementsList.Add(collision.gameObject);

			// These is executed soon.
			if( scriptKind == SCRIPTTYPE.ADD || scriptKind == SCRIPTTYPE.SUBSTITUTE || scriptKind == SCRIPTTYPE.BLACKHOLE || scriptKind == SCRIPTTYPE.PRINT_LN_VAR)
			{
				nowWaitTime = 0;
			}
			else
			{
				nowWaitTime = weitTime;
			}
			
			addElements = true;
			execDecision = false;

			animator.SetTrigger("inElement");
			audioSource.PlayOneShot(absorbSE);
		}
		
	}

	new void PlayOutPutSE()
	{
		audioSource.PlayOneShot(outputSE);
	}
	
}
