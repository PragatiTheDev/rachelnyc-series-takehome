using System.Collections.Generic;

namespace csharp
{
    /// <summary>
    /// The RoseGarden class represents a garden of roses, where each rose is represented by an Item object.
    /// It provides methods for updating the quality and sellIn values of the roses/items in the garden,
    /// based on certain rules and conditions. The UpdateQuality method applies the necessary updates
    /// to each item in the garden according to its specific characteristics.
    /// 
    /// The class contains private helper methods for decreasing quality, increasing quality, decreasing sellIn,
    /// and handling the update logic for Backstage passes items. These methods are called within the UpdateQuality
    /// method to perform the required operations on each item.
    /// 
    /// The RoseGarden class also writes the updated information of each item to the console for tracking purposes.
    /// 
    /// To use the RoseGarden, an instance of the class needs to be created with a list of items as a parameter in
    /// the constructor. Then, the UpdateQuality method can be called to update the quality and sellIn values
    /// of the items in the garden.
    /// </summary>
    public class RoseGarden
    {
        IList<Item> Items;
        
        public RoseGarden(IList<Item> Items)
        {
            this.Items = Items ?? throw new ArgumentNullException(nameof(Items), "Items list cannot be null.");
        }

        public void UpdateQuality()
        {
            foreach (var item in Items)
            {
                // Skip Sulfuras item as it does not change
                if (item.Name == ItemNames.Sulfuras)
                {
                    continue;
                }

                // Decrease quality for regular items and Aged Brie
                if (item.Name != ItemNames.BackstagePasses)
                {
                    DecreaseQuality(item, 1);
                }
                else // Handle Backstage passes separately
                {
                    UpdateBackstagePasses(item);
                }

                // Decrease sellIn for all items except Sulfuras
                if (item.Name != ItemNames.Sulfuras)
                {
                    DecreaseSellIn(item);
                }

                // Handle quality degradation after sellIn date
                if (item.SellIn < 0)
                {
                    if (item.Name != ItemNames.AgedBrie)
                    {
                        if (item.Name != ItemNames.BackstagePasses)
                        {
                            DecreaseQuality(item, 1);
                        }
                        else
                        {
                            item.Quality = 0; // Set quality to 0 for expired Backstage passes
                        }
                    }
                    else
                    {
                        IncreaseQuality(item, 1);
                    }
                }
            }
            
            Console.WriteLine("Item updates completed.");
        }
        
        private void DecreaseQuality(Item item, int amount)
        {
            if (item.Quality >= 0)
            {
                item.Quality -= amount;
            }
        }

        private void IncreaseQuality(Item item, int amount)
        {
            if (item.Quality < 50)
            {
                item.Quality += amount;
            }
        }

        private void DecreaseSellIn(Item item)
        {
            item.SellIn -= 1;
        }

        private void UpdateBackstagePasses(Item item)
        {
            if (item.SellIn > 10)
            {
                IncreaseQuality(item, 1);
            }
            else if (item.SellIn > 5)
            {
                IncreaseQuality(item, 2);
            }
            else if (item.SellIn >= 0)
            {
                IncreaseQuality(item, 3);
            }
            else
            {
                item.Quality = 0; // Set quality to 0 for expired Backstage passes
            }
        }
    }
}
