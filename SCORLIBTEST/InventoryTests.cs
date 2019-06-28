using SCORLIB.ItemClasses;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SCORLIBTEST
{
  [TestClass]
  public class InventoryTests
  {
    private static Inventory Inventory;
    private static Armor FirstCap;
    private static Armor SecondCap;
    
    [ClassInitialize()]
    public static void InitializeInventory(TestContext testContext)
    {
      Inventory = new Inventory();
      FirstCap = SCORLIB.ArmorFactory.GetArmorByName("Cap");
      SecondCap = SCORLIB.ArmorFactory.GetArmorByName("Cap");
    }

    [TestMethod]
    public void InventoryIsNotNull()
    {
      Assert.IsNotNull(Inventory);
    }

    [TestMethod]
    public void AddItemToInventory()
    {
      Assert.IsTrue(Inventory.AddItem(FirstCap));
    }

    [TestMethod]
    public void AddNullItemToInventoryFails()
    {
      Assert.IsFalse(Inventory.AddItem(null));
    }

    [TestMethod]
    public void RemoveExistingItemFromInventory()
    {
      Inventory.AddItem(FirstCap);
      Assert.IsTrue(Inventory.RemoveItem(FirstCap));
    }

    [TestMethod]
    public void RemoveNullItemFromInventoryFails()
    {
      Assert.IsFalse(Inventory.RemoveItem(null));
    }

    [TestMethod]
    public void RemoveNonexistentItemFromInventory()
    {
      Inventory.AddItem(FirstCap);
      Assert.IsFalse(Inventory.RemoveItem(SecondCap));
    }
  }
}
