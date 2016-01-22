using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class DragHandler : MonoBehaviour
{
  SwipeHandler swHandler;
  public void OnDrag()
  {
    swHandler.Enabled = false;
    transform.position = Input.mousePosition;
  }

  public void SetToDefualtPosition()
  {
    swHandler.Enabled = true;
    transform.position = transform.parent.position;
  }

  // Use this for initialization
  void Start ()
  {
    swHandler = FindObjectOfType<SwipeHandler>();
  }
	
	// Update is called once per frame
	void Update () {
	
	}
}
