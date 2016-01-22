using UnityEngine;
using System.Collections;
using System.IO;

public class Logger : MonoBehaviour {

  StreamWriter logFile;
  public string FileName = "log.log";
  // Use this for initialization
  void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

  void Awake()
  {
   /* Application.logMessageReceivedThreaded += HandleLogMessage;
    string logPath = GetLogFilePath();
    logFile = new StreamWriter(logPath, true);
    logFile.AutoFlush = true;*/
  }

  public void HandleLogMessage(string logString, string stackTrace, LogType type)
  {
    try
    {
      lock (logFile)
      {
        logFile.WriteLine("{0} : {1}", type.ToString(), logString); 
      }
    }
    catch (System.Exception e)
    {
      logFile.Close();
      throw;
    }  
  }

  private string GetLogFilePath()
  {
    return Application.persistentDataPath + "/" + FileName;
  }

  void OnApplicationQuit()
  {
    //logFile.Close();
  }

  string GetTime()
  {
    return System.DateTime.Now.ToString("HH:mm:ss.f");
  }
}
