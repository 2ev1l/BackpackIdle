using Game.DataBase;
using Game.Serialization.World;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.TestTools;
using Universal.Core;
using Universal.Events;

namespace Tests.PlayMode
{
    public class InventoryDataTests
    {
        #region fields & properties
        private InventoryData _inventory;
        private ItemsData _itemsData;

        private const int INFO_ID_1x1 = 4;
        private const int INFO_ID_2x2 = 2;
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
            _inventory = new InventoryData();
            _itemsData = _inventory.ItemsData;
        }

        #region FindTopLeftCellOfItem Tests

        [Test]
        public void FindTopLeftCellOfItem_WhenItemExists_ReturnsCorrectIndex()
        {
            ItemData item = _itemsData.AddItem(INFO_ID_1x1);
            _inventory.TryPlaceItem(item, 1, 1);
            int expectedIndex = 1 * _inventory.Width + 1; // y * width + x = 4
            int actualIndex = _inventory.FindTopLeftCellOfItem(item.DataId);
            Assert.AreEqual(expectedIndex, actualIndex);
        }

        [Test]
        public void FindTopLeftCellOfItem_WhenItemDoesNotExist_ReturnsMinusOne()
        {
            int index = _inventory.FindTopLeftCellOfItem(999);
            Assert.AreEqual(-1, index);
        }

        #endregion

        #region IncreaseHeight / IncreaseWidth Tests

        [Test]
        public void IncreaseHeight_WhenCalled_AllowsPlacingItemsInNewRow()
        {
            int initialHeight = _inventory.Height;
            _inventory.IncreaseHeight();
            ItemData item = _itemsData.AddItem(INFO_ID_1x1);
            Assert.AreEqual(initialHeight + 1, _inventory.Height);
            Assert.IsTrue(_inventory.TryPlaceItem(item, 0, initialHeight));
        }

        [Test]
        public void IncreaseWidth_WhenCalled_AllowsPlacingItemsInNewColumn()
        {
            int initialWidth = _inventory.Width;
            _inventory.IncreaseWidth();
            ItemData item = _itemsData.AddItem(INFO_ID_1x1);
            Assert.AreEqual(initialWidth + 1, _inventory.Width);
            Assert.IsTrue(_inventory.TryPlaceItem(item, initialWidth, 0));
        }

        #endregion

        #region CanPlaceItem Tests

        [Test]
        public void CanPlaceItem_OnEmptySpace_ReturnsTrue()
        {
            ItemData itemToPlace = _itemsData.AddItem(INFO_ID_1x1);
            bool canPlace = _inventory.CanPlaceItem(itemToPlace, 0, 0, out ItemData occupiedItem);
            Assert.IsTrue(canPlace);
            Assert.IsNull(occupiedItem);
        }

        [Test]
        public void CanPlaceItem_OnOccupiedSpace_ReturnsFalseAndCorrectOccupiedItem()
        {
            ItemData existingItem = _itemsData.AddItem(INFO_ID_1x1);
            _inventory.TryPlaceItem(existingItem, 1, 1);
            ItemData newItem = _itemsData.AddItem(INFO_ID_1x1);
            bool canPlace = _inventory.CanPlaceItem(newItem, 1, 1, out ItemData occupiedItem);
            Assert.IsFalse(canPlace);
            Assert.IsNotNull(occupiedItem);
            Assert.AreEqual(existingItem.DataId, occupiedItem.DataId);
        }

        [Test]
        public void CanPlaceItem_WhenOutOfBounds_ReturnsFalse()
        {
            ItemData itemToPlace = _itemsData.AddItem(INFO_ID_2x2);
            bool canPlace = _inventory.CanPlaceItem(itemToPlace, 2, 2, out _);
            Assert.IsFalse(canPlace);
        }

        #endregion

        #region TryPlaceItem Tests

        [Test]
        public void TryPlaceItem_OnEmptySpace_PlacesItemSuccessfully()
        {
            ItemData item = _itemsData.AddItem(INFO_ID_1x1);
            bool placed = _inventory.TryPlaceItem(item, 2, 2);
            Assert.IsTrue(placed);
            Assert.AreNotEqual(-1, _inventory.FindTopLeftCellOfItem(item.DataId));
        }

        [Test]
        public void TryPlaceItem_OnOccupiedSpaceWithDifferentItem_Fails()
        {
            ItemData itemA = _itemsData.AddItem(INFO_ID_2x2);
            _inventory.TryPlaceItem(itemA, 0, 0);
            ItemData itemB = _itemsData.AddItem(INFO_ID_1x1);
            bool placed = _inventory.TryPlaceItem(itemB, 0, 0);
            Assert.IsFalse(placed);
            Assert.AreNotEqual(-1, _inventory.FindTopLeftCellOfItem(itemA.DataId));
            Assert.AreEqual(-1, _inventory.FindTopLeftCellOfItem(itemB.DataId));
        }

        [Test]
        public void TryPlaceItem_OnIdenticalItem_UpgradesAndRemovesMovingItem()
        {
            ItemData itemInPlace = _itemsData.AddItem(INFO_ID_1x1);
            _inventory.TryPlaceItem(itemInPlace, 0, 0);
            ItemData movingItem = _itemsData.AddItem(INFO_ID_1x1);
            bool result = _inventory.TryPlaceItem(movingItem, 0, 0);
            Assert.IsTrue(result);
            Assert.AreEqual(2, itemInPlace.Level);
            Assert.IsNull(_itemsData.FindItem(movingItem.DataId));
        }

        #endregion

        #region RemoveItem Tests

        [Test]
        public void RemoveItem_WhenItemExists_RemovesItFromGridAndData()
        {
            ItemData item = _itemsData.AddItem(INFO_ID_2x2);
            _inventory.TryPlaceItem(item, 0, 0);
            _inventory.RemoveItem(item.DataId);
            Assert.AreEqual(-1, _inventory.FindTopLeftCellOfItem(item.DataId));
            Assert.IsNull(_itemsData.FindItem(item.DataId));
        }

        [Test]
        public void RemoveItem_WhenItemDoesNotExist_DoesNothing()
        {
            ItemData item = _itemsData.AddItem(INFO_ID_1x1);
            _inventory.TryPlaceItem(item, 0, 0);
            _inventory.RemoveItem(999);
            Assert.IsNotNull(_itemsData.FindItem(item.DataId));
            Assert.AreNotEqual(-1, _inventory.FindTopLeftCellOfItem(item.DataId));
        }
        #endregion

        #endregion methods
    }
}