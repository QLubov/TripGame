using UnityEngine;
using System.Collections;

public class Inventory : MonoBehaviour
{
  public void AddItem(string name, uint count = 1)
  {
    var counter = FindItemCounter(name);
    if (counter != null)
    {
      counter.Count++;
      return;
    }

    var slot = FindEmptySlot();
    if (slot == null)
      throw new System.IndexOutOfRangeException("Inventory is full");

    var item = CreateInventoryItem(name, count);
    if (item == null)
      throw new System.InvalidOperationException(string.Format("Item {0} not found in DataBase", name));

    item.transform.SetParent(slot.transform, false);
  }

  ItemCounter FindItemCounter(string name)
  {
    var item = FindItem(name);
    return item == null ? null : item.GetComponent<ItemCounter>(); 
  }

  GameObject FindItem(string name)
  {
    foreach (var item in GameObject.FindGameObjectsWithTag("InventoryObject"))
    {
      if (item.name == name)
        return item;
    }
    return null;
  }

  GameObject FindEmptySlot()
  {
    for (int i = 0; i < transform.childCount; ++i)
    {
      var slot = transform.GetChild(i);
      if (slot.childCount == 0)
        return slot.gameObject;
    }
    return null;
  }

  GameObject CreateInventoryItem(string name, uint count)
  {
    var pref = Resources.Load<GameObject>(name);
    if (pref != null)
    {
      var item = Instantiate<GameObject>(pref);
      item.name = name;
      item.GetComponent<ItemCounter>().Count = count;
      return item;
    }
    return null;
  }

  public void RemoveItem(string name)
  {
    var item = FindItem(name);
    if (item != null)
      Destroy(item);
  }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
  {
    AlignItems();
  }

  private void AlignItems()
  {
    int index = -1;
    for (int i = 0; i < transform.childCount; ++i)
    {
      var currentSlot = transform.GetChild(i);
      if (currentSlot.childCount == 0)
        index = i;
      else if (index >= 0)
      {
        //set new parent for item. Item == GetChild(0)
        currentSlot.GetChild(0).SetParent(transform.GetChild(index), false);
        i = index;
      }
    }
  }
}
