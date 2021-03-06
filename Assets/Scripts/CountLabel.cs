﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CountLabel : MonoBehaviour {
  Slot mSlot;
  Text mText;

	// Use this for initialization
	void Start ()
  {
    mSlot = transform.parent.GetComponent<Slot>();
    mText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update ()
  {
    if (mSlot.IsEmpty())
      mText.text = "";
    else
    {
      uint count = mSlot.GetItemCount();
      if (count > 1)
        mText.text = mSlot.GetItemCount().ToString();
      else
        mText.text = "";
    }
	}
}
