using NUnit.Framework;
using System.Collections.Generic;

namespace csharp
{
[TestFixture]
    public class RoseGardenTest
    {
        [Test]
        public void UpdateQuality_ItemListIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            IList<Item> Items = null;
            
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new RoseGarden(Items));
        }

        [Test]
        public void UpdateQuality_ItemListIsEmpty_NoExceptionThrown()
        {
            // Arrange
            IList<Item> Items = new List<Item>();
            RoseGarden app = new RoseGarden(Items);

            // Act & Assert
            Assert.DoesNotThrow(() => app.UpdateQuality());
        }

        [Test]
        public void UpdateQuality_SulfurasItem_NoQualityOrSellInChange()
        {
            // Arrange
            IList<Item> Items = new List<Item> { new Item { Name = ItemNames.Sulfuras, SellIn = 10, Quality = 80 } };
            RoseGarden app = new RoseGarden(Items);

            // Act
            app.UpdateQuality();

            // Assert
            Assert.AreEqual(10, Items[0].SellIn);
            Assert.AreEqual(80, Items[0].Quality);
        }

        [Test]
        public void UpdateQuality_AgedBrieItem_IncreaseQuality()
        {
            // Arrange
            IList<Item> Items = new List<Item> { new Item { Name = ItemNames.AgedBrie, SellIn = 10, Quality = 20 } };
            RoseGarden app = new RoseGarden(Items);

            // Act
            app.UpdateQuality();

            // Assert
            Assert.AreEqual(10, Items[0].SellIn);
            Assert.AreEqual(21, Items[0].Quality);
        }

        [Test]
        public void UpdateQuality_BackstagePassesItem_IncreaseQualityBasedOnSellIn()
        {
            // Arrange
            IList<Item> Items = new List<Item> { new Item { Name = ItemNames.BackstagePasses, SellIn = 15, Quality = 20 } };
            RoseGarden app = new RoseGarden(Items);

            // Act
            app.UpdateQuality();

            // Assert
            Assert.AreEqual(14, Items[0].SellIn);
            Assert.AreEqual(21, Items[0].Quality);
        }

        [Test]
        public void UpdateQuality_RegularItem_DecreaseQuality()
        {
            // Arrange
            IList<Item> Items = new List<Item> { new Item { Name = "Regular", SellIn = 10, Quality = 20 } };
            RoseGarden app = new RoseGarden(Items);

            // Act
            app.UpdateQuality();

            // Assert
            Assert.AreEqual(10, Items[0].SellIn);
            Assert.AreEqual(19, Items[0].Quality);
        }

        [Test]
        public void UpdateQuality_ExpiredItem_DecreaseQualityTwiceAsFast()
        {
            // Arrange
            IList<Item> Items = new List<Item> { new Item { Name = "Expired", SellIn = -1, Quality = 20 } };
            RoseGarden app = new RoseGarden(Items);

            // Act
            app.UpdateQuality();

            // Assert
            Assert.AreEqual(-2, Items[0].SellIn);
            Assert.AreEqual(18, Items[0].Quality);
        }

        [Test]
        public void UpdateQuality_QualityNeverNegative_QualityRemainsZero()
        {
            // Arrange
            IList<Item> Items = new List<Item> { new Item { Name = "Expired", SellIn = -1, Quality = 0 } };
            RoseGarden app = new RoseGarden(Items);

            // Act
            app.UpdateQuality();

            // Assert
            Assert.AreEqual(-2, Items[0].SellIn);
            Assert.AreEqual(0, Items[0].Quality);
        }

        [Test]
        public void UpdateQuality_SingleItem_NormalDecreaseQuality()
        {
            // Arrange
            IList<Item> Items = new List<Item> { new Item { Name = "Regular", SellIn = 10, Quality = 20 } };
            RoseGarden app = new RoseGarden(Items);

            // Act
            app.UpdateQuality();

            // Assert
            Assert.AreEqual(9, Items[0].SellIn);
            Assert.AreEqual(19, Items[0].Quality);
        }

        [Test]
        public void UpdateQuality_MultipleItems_MixedQualityChanges()
        {
            // Arrange
            IList<Item> Items = new List<Item>
            {
                new Item { Name = "Regular", SellIn = 10, Quality = 20 },
                new Item { Name = ItemNames.AgedBrie, SellIn = 5, Quality = 10 },
                new Item { Name = ItemNames.BackstagePasses, SellIn = 15, Quality = 30 },
                new Item { Name = ItemNames.Sulfuras, SellIn = 0, Quality = 80 },
                new Item { Name = "Expired", SellIn = -1, Quality = 25 }
            };
            RoseGarden app = new RoseGarden(Items);

            // Act
            app.UpdateQuality();

            // Assert
            Assert.AreEqual(9, Items[0].SellIn);
            Assert.AreEqual(19, Items[0].Quality);

            Assert.AreEqual(4, Items[1].SellIn);
            Assert.AreEqual(11, Items[1].Quality);

            Assert.AreEqual(14, Items[2].SellIn);
            Assert.AreEqual(31, Items[2].Quality);

            Assert.AreEqual(0, Items[3].SellIn);
            Assert.AreEqual(80, Items[3].Quality);

            Assert.AreEqual(-2, Items[4].SellIn);
            Assert.AreEqual(23, Items[4].Quality);
        }

        [Test]
        public void UpdateQuality_SingleItem_Expired_AgedBrieQualityIncreasesTwiceAsFast()
        {
            // Arrange
            IList<Item> Items = new List<Item> { new Item { Name = ItemNames.AgedBrie, SellIn = -1, Quality = 10 } };
            RoseGarden app = new RoseGarden(Items);

            // Act
            app.UpdateQuality();

            // Assert
            Assert.AreEqual(-2, Items[0].SellIn);
            Assert.AreEqual(12, Items[0].Quality);
        }

        [Test]
        public void UpdateQuality_SingleItem_Expired_BackstagePassesQualitySetToZero()
        {
            // Arrange
            IList<Item> Items = new List<Item> { new Item { Name = ItemNames.BackstagePasses, SellIn = -1, Quality = 30 } };
            RoseGarden app = new RoseGarden(Items);

            // Act
            app.UpdateQuality();

            // Assert
            Assert.AreEqual(-2, Items[0].SellIn);
            Assert.AreEqual(0, Items[0].Quality);
        }

        [Test]
        public void UpdateQuality_SingleItem_SellInAndQualityRemainUnchangedForSulfuras()
        {
            // Arrange
            IList<Item> Items = new List<Item> { new Item { Name = ItemNames.Sulfuras, SellIn = 10, Quality = 80 } };
            RoseGarden app = new RoseGarden(Items);

            // Act
            app.UpdateQuality();

            // Assert
            Assert.AreEqual(10, Items[0].SellIn);
            Assert.AreEqual(80, Items[0].Quality);
        }

        [Test]
        public void UpdateQuality_SingleItem_QualityCannotBecomeNegative()
        {
            // Arrange
            IList<Item> Items = new List<Item> { new Item { Name = "Regular", SellIn = 5, Quality = 0 } };
            RoseGarden app = new RoseGarden(Items);

            // Act
            app.UpdateQuality();

            // Assert
            Assert.AreEqual(4, Items[0].SellIn);
            Assert.AreEqual(0, Items[0].Quality);
        }

        [Test]
        public void UpdateQuality_NullItemList_ThrowsArgumentNullException()
        {
            // Arrange
            IList<Item> Items = null;
            RoseGarden app;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => app = new RoseGarden(Items));
        }

        [Test]
        public void UpdateQuality_EmptyItemList_NoItemsToUpdate()
        {
            // Arrange
            IList<Item> Items = new List<Item>();
            RoseGarden app = new RoseGarden(Items);

            // Act
            app.UpdateQuality();

            // Assert
            Assert.IsEmpty(Items);
        }

        [Test]
        public void UpdateQuality_ItemListWithNullItem_IgnoredDuringUpdate()
        {
            // Arrange
            IList<Item> Items = new List<Item> { null };
            RoseGarden app = new RoseGarden(Items);

            // Act
            app.UpdateQuality();

            // Assert
            Assert.IsNull(Items[0]);
        }

        [Test]
        public void UpdateQuality_ItemListWithNullItemAndValidItems_ItemsUpdatedExceptForNullItem()
        {
            // Arrange
            IList<Item> Items = new List<Item> { null, new Item { Name = "Regular", SellIn = 5, Quality = 20 } };
            RoseGarden app = new RoseGarden(Items);

            // Act
            app.UpdateQuality();

            // Assert
            Assert.IsNull(Items[0]);

            Assert.AreEqual(4, Items[1].SellIn);
            Assert.AreEqual(19, Items[1].Quality);
        }
    }

}
