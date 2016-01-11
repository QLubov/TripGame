using UnityEngine;
using System.Collections;

public class Slot : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

  public void SetItem(GameObject item)
  {
    item.transform.SetParent(transform, false);
    item.transform.SetSiblingIndex(0);
  }

  public bool IsEmpty()
  {
    return transform.childCount == 1;
  }

  public GameObject GetItem()
  {
    return transform.GetChild(0).gameObject;
  }

  public uint GetItemCount()
  {
    return GetItem().GetComponent<ItemCounter>().Count;
  } 
}
