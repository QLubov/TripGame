using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Data;

public class GameMechanic : MonoBehaviour
{
  // Use this for initialization
  void Start()
  {
    string sceneName = Application.loadedLevelName + ".dat";
    if (!File.Exists(sceneName))
    {
      return;
    }
    using (var fs = new FileStream(sceneName, FileMode.Open))
    {
      LoadScene(fs);
    }
    using (var fs = new FileStream("inventory.dat", FileMode.OpenOrCreate))
    {
      LoadInventory(fs);
    }
  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetButtonDown("Space"))
    {
      SaveScene(new FileStream(Application.loadedLevelName + ".dat", FileMode.Create));
      SaveInventory(new FileStream("inventory.dat", FileMode.Create));
    }
  }

  public void Save(FileStream stream, object graph)
  {
    BinaryFormatter formatter = new BinaryFormatter();
    formatter.Serialize(stream, graph);
  }

  public object Load(FileStream stream)
  {
    BinaryFormatter formatter = new BinaryFormatter();
    return formatter.Deserialize(stream);
  }

  public void SaveScene(FileStream stream)
  {
    var scene = StoreScene("SceneObject");
    Save(stream, scene);    
  }

  void LoadScene(FileStream stream)
  {
    Scene scene = Load(stream) as Scene;
    RepairScene(scene);
  }

  void RepairScene(Scene scene)
  {
    foreach (var so in scene.SceneObjects)
    {
      var pref = Resources.Load<GameObject>(so.Name);
      if (pref != null)
      { 
        Instantiate<GameObject>(pref).transform.SetParent(GameObject.Find("Canvas").transform);
      }
    }
  }

  Scene StoreScene(string tag)
  {
    var scene = new Scene();
    foreach (var go in GameObject.FindGameObjectsWithTag(tag))
    {
      scene.SceneObjects.Add(new SceneObject(go.name, go.transform.position, "blah"));
    }
    return scene;
  }

  public void SaveInventory(FileStream stream)
  {
    Inventory inventory = StoreInventory("InventoryObject");
    Save(stream, inventory);
  }

  Inventory StoreInventory(string tag)
  {
    var inventory = new Inventory();
    foreach (var item in GameObject.FindGameObjectsWithTag(tag))
    {
      inventory.InventoryObjects.Add(new InventoryObject(item.name, 1));
    }
    return inventory;
  }

  void RepairInventory(Inventory inventory)
  {
    foreach (var item in inventory.InventoryObjects)
    {
      var pref = Resources.Load<GameObject>(item.Name);
      if (pref != null)
      {
        var itemView = Instantiate<GameObject>(pref);
        var inventoryView = GameObject.Find("InventoryView");
        itemView.transform.SetParent(inventoryView.transform);
      }
    }
  }

  void LoadInventory(FileStream stream)
  {
    Inventory inventory = Load(stream) as Inventory;
    RepairInventory(inventory);
  }
}
