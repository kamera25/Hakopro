using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class Paint : MonoBehaviour 
{

	Texture2D texture;
	public RawImage rawImage;

	string color_str = "000000";
	bool write = false;
	Vector3 beforeMousePos;

	// Use this for initialization
	void Start () 
	{
		texture = rawImage.texture as Texture2D;
		if (texture == null) 
		{
			texture = new Texture2D(256,256);
			rawImage.texture = texture;
		}
	}

	void OnGUI()
	{
		color_str = GUI.TextField (new Rect (0, 0, 100, 20), color_str);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (write) 
		{
			Vector3 v = Input.mousePosition;
			lineTo( beforeMousePos, v, getColor());
			beforeMousePos = v;
			texture.Apply();
		//	Debug.Log( v+"  "+ beforeMousePos);
		}
	}

	public Color getColor()
	{
		try
		{
			float r = Convert.ToInt32( color_str.Substring(0,2), 16);
			float g = Convert.ToInt32( color_str.Substring(2,2), 16);
			float b = Convert.ToInt32( color_str.Substring(4,2), 16);
			return new Color( r, g, b);
		}
		catch( Exception e)
		{
			return Color.black;
		}
	}

	public void lineTo( Vector3 start, Vector3 end, Color color)
	{

		Color[] wcolor = {color};

		Vector3 startv = Camera.main.ScreenToWorldPoint ( start);
		Vector3 endv = Camera.main.ScreenToWorldPoint (end);

		float x = startv.x;
		float y = startv.y;

		float dy = (endv.y - startv.y) / ( end.x - startv.x);
		float dx = start.x < end.x ? 1 : -1;

		while( y <= endv.y)
		{
			texture.SetPixels( (int)x, (int)y, 1, 1, wcolor);
			x += dx;
			y += dy*dy;
			if (startv.x <= endv.x && x >= endv.x ||
			    startv.x >= endv.x && x <= endv.x) {
				break;	
			}
		}


		/*
		if (Mathf.Abs(start.x-end.x) >= Mathf.Abs(start.y-end.y)) {
			float dy = end.x == start.x ? 0 : (end.y-start.y) / (end.x-start.x);
			float dx = start.x < end.x ? 1 : -1;
			//draw line loop
			while (x >= 0 && x < texture.width && y >= 0 && y < texture.height) {
				try {
					texture.SetPixels((int)x,(int)y,1,1,wcolor);
					x+=dx;
					y+=dx*dy;
					if (start.x <= end.x && x >= end.x ||
					    start.x >= end.x && x <= end.x) {
						break;	
					}
				} catch (Exception e) {
					Debug.LogException(e);
					break;
				}
			}
		} else if (Mathf.Abs(start.x-end.x) < Mathf.Abs(start.y-end.y)) {
			float dx = start.y == end.y ? 0 : (end.x-start.x) / (end.y-start.y);
			float dy = start.y < end.y?1:-1;
			while (x >= 0 && x < texture.width && y >= 0 && y < texture.height) {
				try {
					texture.SetPixels((int)x,(int)y,1,1,wcolor);
					x+=dx*dy;
					y+=dy;
					if (start.y <= end.y && y >= end.y ||
					    start.y >= end.y && y <= end.y) {
						break;	
					}
				} catch (Exception e) {
					Debug.LogException(e);
					break;
				}
			}
		}*/
	}

	void OnMouseDown()
	{
		beforeMousePos = Input.mousePosition;
		write = true;
	}

	void OnMouseUp()
	{
		write = false;
	}
}
