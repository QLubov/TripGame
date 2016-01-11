using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Data;

public class GameMechanic : MonoBehaviour
{
  public static string SceneObjectTag = "SceneObject";
  public static string GameSceneTag = "GameScene";
  public static string InventoryObjectTag = "InventoryObject";
  public static string InventoryViewTag = "InventoryView";

  // Use this for initialization
  void Start()
  {
    try
    {
      Input.multiTouchEnabled = false;

      string path = GetScenePath();

      AddLog(path);

      if (!File.Exists(@path))
      {
        AddLog("Scene file doesn't exist");
        return;
      }
      using (var fs = new FileStream(@path, FileMode.Open))
      {
        AddLog("Loading Scene... ");

        ClearObjectsByTag(SceneObjectTag);
        LoadScene(fs);

        AddLog(string.Format("Scene {0} successfully loaded ", path));
      }
      using (var fs = new FileStream(@GetInventoryPath(), FileMode.OpenOrCreate))
      {
        AddLog("Loading inventory... ");

        ClearObjectsByTag(InventoryObjectTag);
        LoadInventory(fs);

        AddLog("Inventory successfully loaded");
      }
    }
    catch (System.Exception e)
    {
      AddLog(e.Message);
    }
  }

  private static string GetScenePath()
  {
    return Application.persistentDataPath + "/" + Application.loadedLevelName;
  }

  private static string GetInventoryPath()
  {
    return Application.persistentDataPath + "/inventory";
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
    if (Input.GetKeyDown(KeyCode.Escape))
    {
      // Implement YES/NO message
      Application.Quit();
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
    var scene = StoreScene(SceneObjectTag);
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
        sceneObject.transform.SetParent(GameObject.FindGameObjectWithTag(GameSceneTag).transform, false);
        sceneObject.transform.position = so.Position;
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
    var inventory = StoreInventory(InventoryObjectTag);
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
    foreach (var item in inventory.InventoryObjects)
    {
      var invView = FindObjectOfType<Inventory>();
      invView.AddItem(item.Name, item.Count);
    }
  }

  void LoadInventory(FileStream stream)
  {
    var inventory = Load(stream) as Data.Inventory;
    RepairInventory(inventory);
  }

  public void OnSave()
  {
    AddLog("Path is " + Application.persistentDataPath);
    try
    {
      SaveScene(new FileStream(GetScenePath(), FileMode.Create));
      SaveInventory(new FileStream(GetInventoryPath(), FileMode.Create));
    }
    catch (System.Exception e)
    {
      AddLog(e.Message);
    }
  }

  public void OnClean()
  {
    string inventoryPath = GetInventoryPath();
    string scenePath = GetScenePath();
    if (File.Exists(@inventoryPath))
      File.Delete(@inventoryPath);
    if (File.Exists(@scenePath))
      File.Delete(@scenePath);
  }

  public static void AddLog(string message)
  {
    GameObject.Find("DebugConsole").GetComponent<UnityEngine.UI.Text>().text += message + "\n";
  }

  public void Back()
  {
    Debug.Log("Back to previous scene");
  }
}
