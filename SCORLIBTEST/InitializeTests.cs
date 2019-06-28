using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SCORLIBTEST
{
  [TestClass]
  public class InitializeTests
  {
    [AssemblyInitialize]
    public static void Initialize(TestContext testContext)
    {
      SCORLIB.GameData.Initialize();
    }

    [TestMethod]
    public void WeaponDataIsNotNull()
    {
      Assert.IsNotNull(SCORLIB.GameData.Weapons);
    }

    [TestMethod]
    public void WeaponDataIsNotEmpty()
    {
      Assert.IsTrue(0 < SCORLIB.GameData.Weapons.Count);
    }

    [TestMethod]
    public void ArmorDataIsNotNull()
    {
      Assert.IsNotNull(SCORLIB.GameData.Armors);
    }

    [TestMethod]
    public void ArmorDataIsNotEmpty()
    {
      Assert.IsTrue(0 < SCORLIB.GameData.Armors.Count);
    }

    [TestMethod]
    [ExpectedException(typeof(KeyNotFoundException))]
    public void EmptyTreasureClassTest()
    {
      var treasure = SCORLIB.TreasureFactory.GetDropFromTreasureClass("");
    }
  }
}
