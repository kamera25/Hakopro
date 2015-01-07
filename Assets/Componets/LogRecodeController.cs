using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

class LogData
{
	public DateTime nowDateTime;
	public string detail;
};

public class LogRecodeController : MonoBehaviour 
{
	private string fileName;
	private string directory;
	static List<LogData> logs = new List<LogData>();

	void Start()
	{
		DateTime dt = DateTime.Now;
		GameObject dicisionBox = GameObject.Find ("DecisionBox");

		directory = "Logs";
		fileName = dt.Year.ToString () + dt.Month.ToString () + "_" + dt.Day.ToString () 
						+ "_" + dt.Hour.ToString () + dt.Minute.ToString () + dt.Millisecond.ToString ()
						+ "_stage" + dicisionBox.GetComponent<DecisionController> ().stageNum.ToString () + ".txt"; 
	}

	public void RecordData( string str)
	{
		LogData log = new LogData();
		log.nowDateTime = DateTime.Now;
		log.detail = str;

		logs.Add (log);
	}

	public void WriteFile()
	{

		//Check exisiting directory.
		if( !Directory.Exists(directory))
		{
			Directory.CreateDirectory(directory);
		}

		string filePath = directory + "/" + fileName;

		Debug.Log ( "Output : " + filePath);
		// Check exisit filename.
		if ( !File.Exists(filePath)) 
		{
			FileStream f = new FileStream( filePath, FileMode.Create, FileAccess.Write);
			BinaryWriter writer = new BinaryWriter(f);

			string str = "";
			// Write log datas to file.
			foreach( LogData log in logs)
			{
				str += log.nowDateTime + ", " + log.detail + "\r\n";

			}
			writer.Write( str);
			writer.Close();

			logs.Clear();// Delete all log.
		}
	}
}
