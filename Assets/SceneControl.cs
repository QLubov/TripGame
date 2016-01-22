using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class SceneControl : MonoBehaviour {

  Stack<string> scenes = new Stack<string>();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
  

  public void LoadScene(string name)
  {
    scenes.Push(Application.loadedLevelName);
    Application.LoadLevel(name);
  }

  public void Back()
  {
    try
    {
      string name = scenes.Pop();
      Application.LoadLevel(name);
    }
    catch (InvalidOperationException e)
    {
      Debug.Log(e.Message);
    }
  }
}
