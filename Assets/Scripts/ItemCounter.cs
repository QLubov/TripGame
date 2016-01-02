using UnityEngine;
using System.Collections;

public class ItemCounter : MonoBehaviour {

  public uint Count = 1;// { get; set; }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
  {
    var label = transform.parent.FindChild("CountLabel").GetComponent<UnityEngine.UI.Text>();
    if (Count > 1)
      label.text = Count.ToString();
    else
      label.text = "";
	}
}
