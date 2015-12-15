using System;
using System.Collections.Generic;

namespace Data
{
  [Serializable]
  class InventoryObject
  {
    public string Name { get; set; }
    public uint Count { get; set; }

    public InventoryObject()
    { }

    public InventoryObject(string name, uint count)
    {
      Name = name;
      Count = count;
    }
  }

  [Serializable]
  class Inventory
  {
    public uint CellCount { get; set; }
    public List<InventoryObject> InventoryObjects = new List<InventoryObject>();
  }
}