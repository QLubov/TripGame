using UnityEngine;
using System.Collections;
using System;

public class SceneSwitcher : MonoBehaviour
{ 
  public string SwitchTo = "";
    
  public void SwitchScene(UnityEngine.EventSystems.BaseEventData be)
  {
    if (SwitchTo == "")
    {
      Debug.LogError("Scene to switch is not set");
      return;
    }
    FindObjectOfType<GameMechanic>().OnSave();
    Application.LoadLevel(SwitchTo);
  }
}
