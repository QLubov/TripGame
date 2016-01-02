using UnityEngine;
using System.Collections;

public class GenerateInventoryItem : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

  public void Generate()
  {
    string prefix = "ItemView";
    int suffix = new System.Random().Next(0, 4);
    
    FindObjectOfType<Inventory>().AddItem(prefix + suffix);
  }
}
