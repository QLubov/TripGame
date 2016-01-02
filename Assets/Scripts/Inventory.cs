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

    slot.SetItem(item); //.transform.SetParent(slot.transform, false);
  }

  ItemCounter FindItemCounter(string name)
  {
    var item = FindItem(name);
    return item == null ? null : item.GetComponent<ItemCounter>(); 
  }

  GameObject FindItem(string name)
  {
    foreach (var item in GameObject.FindGameObjectsWithTag(GameMechanic.InventoryObjectTag))
    {
      if (item.name == name)
        return item;
    }
    return null;
  }

  Slot FindEmptySlot()
  {
    for (int i = 0; i < transform.childCount; ++i)
    {
      var slot = transform.GetChild(i).GetComponent<Slot>();
      if (slot.IsEmpty())
        return slot;
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

  public void RemoveItem(string name, bool all = true)
  {
    var item = FindItem(name);
    if (item != null)
    {
      if (all)
        Destroy(item);
      else
      {
        var counter = item.GetComponent<ItemCounter>();
        if (counter.Count > 1)
          counter.Count--;
        else
          Destroy(item);
      }
    }      
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
      var currentSlot = transform.GetChild(i).GetComponent<Slot>();
      if (currentSlot.IsEmpty())
        index = i;
      else if (index >= 0)
      {
        //set new parent for item. Item == GetChild(0)
        transform.GetChild(index).GetComponent<Slot>().SetItem(currentSlot.transform.GetChild(0).gameObject);
        i = index;
      }
    }
  }
}
