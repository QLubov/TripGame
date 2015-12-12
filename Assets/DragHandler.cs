using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class DragHandler : MonoBehaviour
{
  public void OnDrag()
  {
    transform.position = Input.mousePosition;
  }

  public void SetToDefualtPosition()
  {
    transform.position = transform.parent.position;
  }

  // Use this for initialization
  void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
