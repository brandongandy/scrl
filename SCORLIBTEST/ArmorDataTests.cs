using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SCORLIB.ItemClasses;

namespace SCORLIBTEST
{
  [TestClass]
  public class ArmorDataTests
  {
    private static Armor baseCap;
    private static Armor newCap;
    private static Armor clonedNewCap;
    
    [ClassInitialize()]
    public static void InitializeProperties(TestContext testContext)
    {
      baseCap = SCORLIB.ArmorFactory.GetArmorByName("Cap");
      newCap = baseCap.GetNew();
      clonedNewCap = newCap.Clone();
    }

    [TestMethod]
    public void BaseItemIsNotNull()
    {
      Assert.IsNotNull(baseCap);
    }

    [TestMethod]
    public void BaseItemMatchesProperName()
    {
      Assert.AreEqual(baseCap.Name, "Cap");
    }

    [TestMethod]
    public void NewItemIsNotNull()
    {
      Assert.IsNotNull(newCap);
    }

    [TestMethod]
    public void NewItemMatchesProperName()
    {
      Assert.AreEqual(newCap.Name, baseCap.Name);
    }

    [TestMethod]
    public void ClonedItemIsNotNull()
    {
      Assert.IsNotNull(clonedNewCap);
    }

    [TestMethod]
    public void NewAndClonesItemAreEqual()
    {
      Assert.AreEqual(newCap, clonedNewCap);
    }
  }
}
