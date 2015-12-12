using UnityEngine;
using System.Collections;
using System;
using UnityEngine.EventSystems;

public class DropHandler : MonoBehaviour
{
  public void OnDrop(BaseEventData eventData)
  {
    var data = eventData as PointerEventData;
    Debug.Log(data.pointerDrag.name);

    GameObject.Find("InventoryView").GetComponent<Inventory>().RemoveItem(data.pointerDrag.name);
  }

  // Use this for initialization
  void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
