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
      ClearObjectsByTag("SceneObject");
      LoadScene(fs);
    }
    using (var fs = new FileStream("inventory.dat", FileMode.OpenOrCreate))
    {
      ClearObjectsByTag("InventoryObject");
      LoadInventory(fs);
    }
  }

  void ClearObjectsByTag(string tag)
  {
    foreach (var obj in GameObject.FindGameObjectsWithTag(tag))
    {
      DestroyImmediate(obj);
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
        var sceneObject = Instantiate<GameObject>(pref);
        sceneObject.transform.SetParent(GameObject.Find("GameScene").transform);
        sceneObject.name = so.Name;
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
    var inventory = StoreInventory("InventoryObject");
    Save(stream, inventory);
  }

  Data.Inventory StoreInventory(string tag)
  {
    var inventory = new Data.Inventory();
    foreach (var item in GameObject.FindGameObjectsWithTag(tag))
    {
      var counter = item.GetComponent<ItemCounter>();
      inventory.InventoryObjects.Add(new InventoryObject(item.name, counter.Count));
    }
    return inventory;
  }

  void RepairInventory(Data.Inventory inventory)
  {
    foreach (var item in inventory.InventoryObjects )
    {
      var invView = GameObject.Find("InventoryView").GetComponent<Inventory>();
      invView.AddItem(item.Name, item.Count);
    }
  }

  void LoadInventory(FileStream stream)
  {
    var inventory = Load(stream) as Data.Inventory;
    RepairInventory(inventory);
  }
}
