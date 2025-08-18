using System.Collections;
using System.Collections.Generic;
using Game.Serialization.World;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Universal.Events;

namespace Tests.PlayMode
{
    public class ItemDataTests
    {
        #region fields & properties
        private const int UPGRADEABLE_ITEM_ID = 4;
        #endregion fields & properties

        #region methods
        [OneTimeSetUp]
        public void GlobalSetup()
        {
            AssetLoader.InitInstances();
        }
        #region Constructor and Properties Tests

        [Test]
        public void Constructor_WhenCalled_InitializesWithCorrectIdAndDefaultLevel()
        {
            var item = new ItemData(UPGRADEABLE_ITEM_ID, 10);
            Assert.AreEqual(UPGRADEABLE_ITEM_ID, item.Id);
            Assert.AreEqual(10, item.DataId);
            Assert.AreEqual(1, item.Level);
        }

        [Test]
        public void Info_WhenAccessedFirstTime_IsNotNull()
        {
            var item = new ItemData(UPGRADEABLE_ITEM_ID, 0);
            Assert.IsNotNull(item.Info);
        }

        #endregion

        #region Upgrade Tests

        [Test]
        public void TryUpgrade_WhenCanUpgrade_IncrementsLevelAndReturnsTrue()
        {
            var item = new ItemData(UPGRADEABLE_ITEM_ID, 0);
            Assume.That(item.CanUpgrade());
            bool result = item.TryUpgrade();
            Assert.IsTrue(result);
            Assert.AreEqual(2, item.Level);
        }


        [Test]
        public void TryUpgrade_WhenSuccessful_InvokesOnUpgradedEvent()
        {
            var item = new ItemData(UPGRADEABLE_ITEM_ID, 0);
            Assume.That(item.CanUpgrade());
            int receivedLevel = 0;
            item.OnUpgraded += (level) => { receivedLevel = level; };
            item.TryUpgrade();
            Assert.AreEqual(2, receivedLevel);
        }

        #endregion

        #endregion methods
    }
}