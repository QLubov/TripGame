using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Collector : MonoBehaviour {

  public string InventoryObjectName = "";


  public void CollectObject()
  {
    FindObjectOfType<Inventory>().AddItem(InventoryObjectName);
    Destroy(gameObject);

    Image img = GetComponent<Image>();
  }
}
