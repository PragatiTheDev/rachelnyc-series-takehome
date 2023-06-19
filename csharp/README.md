## Refactoring Changes

The following refactoring changes have been made to the code:

### Updated Code Structure

The code has been organized into a class called `RoseGarden`, which represents a garden of roses (items).
Private helper methods have been added to handle specific operations and update logic for different item types.
The `RoseGarden` class writes the updated information of each item to the console for tracking purposes.

### New Class: ItemNames

Added a new class called `ItemNames` to manage and reference item names throughout the codebase.
It provides static properties that define the names of different items, making it easier to maintain and modify item names.

### Updated Test Cases

The existing test cases have been refactored to align with the updated code structure and class names.
New test cases have been added to cover various scenarios and edge cases for the `UpdateQuality` method.

### New Test Cases (Summary)

- **UpdateQuality_ItemListIsNull_ThrowsArgumentNullException**: Throws an exception if the item list is null.
- **UpdateQuality_ItemListIsEmpty_NoExceptionThrown**: No exception is thrown when the item list is empty.
- **UpdateQuality_SulfurasItem_NoQualityOrSellInChange**: The quality and sellIn values of a Sulfuras item remain unchanged.
- **UpdateQuality_AgedBrieItem_IncreaseQuality**: The quality of an Aged Brie item increases.
- **UpdateQuality_BackstagePassesItem_IncreaseQualityBasedOnSellIn**: The quality of a Backstage Passes item increases based on its sellIn value.
- **UpdateQuality_RegularItem_DecreaseQuality**: The quality of a regular item decreases.
- **UpdateQuality_ExpiredItem_DecreaseQualityTwiceAsFast**: The quality of an expired item decreases twice as fast.
- **UpdateQuality_QualityNeverNegative_QualityRemainsZero**: The quality of an item remains zero and does not become negative.
- **UpdateQuality_SingleItem_NormalDecreaseQuality**: The quality of a single item decreases normally.
- **UpdateQuality_MultipleItems_MixedQualityChanges**: The quality of multiple items changes based on their specific characteristics.
- **UpdateQuality_SingleItem_Expired_AgedBrieQualityIncreasesTwiceAsFast**: The quality of an expired Aged Brie item increases twice as fast.
- **UpdateQuality_SingleItem_Expired_BackstagePassesQualitySetToZero**: The quality of an expired Backstage Passes item is set to zero.
- **UpdateQuality_SingleItem_SellInAndQualityRemainUnchangedForSulfuras**: The sellIn and quality values of a Sulfuras item remain unchanged.
- **UpdateQuality_SingleItem_QualityCannotBecomeNegative**: The quality of an item cannot become negative.
- **UpdateQuality_NullItemList_ThrowsArgumentNullException**: Throws an exception if the item list is null when creating a `RoseGarden` instance.
- **UpdateQuality_EmptyItemList_NoItemsToUpdate**: When the item list is empty, no items are updated.
- **UpdateQuality_ItemListWithNullItem_IgnoredDuringUpdate**: Items with null values in the item list are ignored during the update.
- **UpdateQuality_ItemListWithNullItemAndValidItems_ItemsUpdatedExceptForNullItem**: Items with null values in the item list are ignored, and other valid items are updated accordingly.
