using UnityEngine;
using System.Collections.Generic;

public class SwipeHandler : MonoBehaviour
{
  public bool Enabled { get; set; }
  [Range(1.0f, 240.0f)]
  public float SwipeLength = 100.0f;

  [Range(0.0f, 1.0f)]
  public float Aberration = 0.2f;

  public void Start()
  {
    Enabled = true;
  }

  public string ParentScreen;

  public void Update()
  {
    if (!Enabled)
    {
      OldPosition = Input.mousePosition;
      return;
    }

    Vector2 movement = GetMovement();
    if (movement.magnitude >= SwipeLength)
    {
      movement.Normalize();
      //checking Swipe Up
      if (movement.y > 0)
      {
        if (Mathf.Abs(movement.x) <= Aberration)
        {
          FindObjectOfType<Inventory>().Show();
          return;
        }
      }
      //checking Swipe Left
      if (movement.x < 0)
      {
        if (Mathf.Abs(movement.y) <= Aberration)
        {
          if(ParentScreen == "")
          {
            Debug.Log("Parent screen is not set for " + Application.loadedLevelName);
            return;
          }

          FindObjectOfType<GameMechanic>().OnSave();
          Application.LoadLevel(ParentScreen);
          
          return;

        }
      }
    }
  }
  Vector2 OldPosition;

  Vector2 GetMovement()
  {
    Vector2 movement = Vector2.zero;
#if UNITY_EDITOR_WIN
    if (Input.GetMouseButtonDown(0))
      OldPosition = Input.mousePosition;
    if (Input.GetMouseButtonUp(0))
      movement = (Vector2)Input.mousePosition - OldPosition;
#endif

#if UNITY_ANDROID
    movement = Input.GetTouch(0).deltaPosition;
#endif
    return movement;
  }

}