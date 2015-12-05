using UnityEngine;
using System.Collections.Generic;
using System;

namespace Data
{
  [Serializable]
  public class SceneObject
  {
    float x, y, z;
    public string Name { get; set; }

    public Vector3 Position
    {
      get
      {
        return new Vector3(x, y, z);
      }
      set
      {
        x = value.x;
        y = value.y;
        z = value.z;
      }
    }

    public string UserData { get; set; }

    public SceneObject()
    {

    }
    public SceneObject(string name, Vector3 position, string data)
    {
      Name = name;
      Position = position;
      UserData = data;
    }
  }

  [Serializable]
  public class Scene
  {
    public string Name { get; set; }
    public List<SceneObject> SceneObjects = new List<SceneObject>();
  }
}