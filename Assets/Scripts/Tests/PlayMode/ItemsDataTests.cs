using System.Collections;
using System.Collections.Generic;
using Game.DataBase;
using Game.Serialization.World;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Universal.Events;

namespace Tests.PlayMode
{
    public class ItemsDataTests
    {
        #region fields & properties
        private ItemsData _itemsData;
        #endregion fields & properties

        #region methods
        [OneTimeSetUp]
        public void GlobalSetup()
        {
            AssetLoader.InitInstances();
        }

        [SetUp]
        public void Setup()
        {
            _itemsData = new ItemsData();
        }

        #region AddItem Tests

        [Test]
        public void AddItem_WithValidInfoId_AddsItemAndAssignsCorrectIds()
        {
            ItemData item = _itemsData.AddItem(infoId: 0);
            Assert.AreEqual(1, _itemsData.Items.Count);
            Assert.IsNotNull(item);
            Assert.AreEqual(0, item.Id);
            Assert.AreEqual(0, item.DataId);
        }

        [Test]
        public void AddItem_WhenCalledMultipleTimes_AssignsUniqueDataIds()
        {
            ItemData item1 = _itemsData.AddItem(infoId: 0);
            ItemData item2 = _itemsData.AddItem(infoId: 1);
            Assert.AreEqual(2, _itemsData.Items.Count);
            Assert.AreEqual(0, item1.DataId);
            Assert.AreEqual(1, item2.DataId);
        }

        [Test]
        public void AddItem_WithInvalidInfoId_ReturnsNullAndDoesNotAddItem()
        {
            ItemData item = _itemsData.AddItem(infoId: -1);
            Assert.IsNull(item);
            Assert.AreEqual(0, _itemsData.Items.Count);
        }

        [Test]
        public void AddItem_WhenItemAdded_InvokesOnItemAddedEvent()
        {
            ItemData receivedItem = null;
            _itemsData.OnItemAdded += (item) => { receivedItem = item; };
            ItemData addedItem = _itemsData.AddItem(infoId: 0);
            Assert.IsNotNull(receivedItem);
            Assert.AreEqual(addedItem.DataId, receivedItem.DataId);
        }

        #endregion

        #region RemoveItem Tests

        [Test]
        public void RemoveItem_WhenItemExists_RemovesItFromList()
        {
            ItemData itemToKeep = _itemsData.AddItem(infoId: 0);
            ItemData itemToRemove = _itemsData.AddItem(infoId: 1);
            _itemsData.RemoveItem(itemToRemove.DataId);
            Assert.AreEqual(1, _itemsData.Items.Count);
            Assert.IsNull(_itemsData.FindItem(itemToRemove.DataId));
            Assert.IsNotNull(_itemsData.FindItem(itemToKeep.DataId));
        }

        [Test]
        public void RemoveItem_WhenItemDoesNotExist_DoesNothing()
        {
            _itemsData.AddItem(infoId: 0);
            _itemsData.RemoveItem(dataId: 999);
            Assert.AreEqual(1, _itemsData.Items.Count);
        }

        [Test]
        public void RemoveItem_WhenItemRemoved_InvokesOnItemRemovedEvent()
        {
            ItemData itemToRemove = _itemsData.AddItem(infoId: 0);
            ItemData receivedItem = null;
            _itemsData.OnItemRemoved += (item) => { receivedItem = item; };

            _itemsData.RemoveItem(itemToRemove.DataId);
            Assert.IsNotNull(receivedItem);
            Assert.AreEqual(itemToRemove.DataId, receivedItem.DataId);
        }
        #endregion

        #endregion methods
    }
}