using System;
using System.Collections.Generic;
using System.Text;
using BrutalAPI.Items;

namespace A_Apocrypha.Items
{
    public class LetterTile
    {
        public static void Add()
        {
            int[] scores = [1, 3, 3, 2, 1, 4, 2, 4, 1, 8, 5, 1, 3, 1, 1, 3, 10, 1, 1, 1, 1, 4, 4, 8, 4, 10];

            Dictionary<int, char> tiles = new Dictionary<int, char>();
            tiles.Add(0, 'A');
            tiles.Add(1, 'B');
            tiles.Add(2, 'C');
            tiles.Add(3, 'D');
            tiles.Add(4, 'E');
            tiles.Add(5, 'F');
            tiles.Add(6, 'G');
            tiles.Add(7, 'H');
            tiles.Add(8, 'I');
            tiles.Add(9, 'J');
            tiles.Add(10, 'K');
            tiles.Add(11, 'L');
            tiles.Add(12, 'M');
            tiles.Add(13, 'N');
            tiles.Add(14, 'O');
            tiles.Add(15, 'P');
            tiles.Add(16, 'Q');
            tiles.Add(17, 'R');
            tiles.Add(18, 'S');
            tiles.Add(19, 'T');
            tiles.Add(20, 'U');
            tiles.Add(21, 'V');
            tiles.Add(22, 'W');
            tiles.Add(23, 'X');
            tiles.Add(24, 'Y');
            tiles.Add(25, 'Z');

            List<LootItemProbability> lootList = new List<LootItemProbability>();

            foreach (var tile in tiles)
            {
                DamageIntModContainsLetterAndSecondaryEffect_Item lettertile = new DamageIntModContainsLetterAndSecondaryEffect_Item("LetterTile_" + tile.Value + "_ID", scores[tile.Key], true, false, false, tile.Value, false)
                {
                    Item_ID = "LetterTile_" + tile.Value + "_ExtraW",
                    Name = "Letter Tile (" + tile.Value + ")",
                    Flavour = "\"Scores " + scores[tile.Key] + " point" + (scores[tile.Key] == 1 ? "!\"" : "s!\""),
                    Description = $"All damage dealt by this party member is increased by {scores[tile.Key]} if the target's name contains the letter \'{tile.Value}\'." +
                    $"\nThis item is consumed at the end of combat.",
                    IsShopItem = false,
                    ShopPrice = tile.Key,
                    DoesPopUpInfo = true,
                    StartsLocked = false,
                    Icon = ResourceLoader.LoadSprite("LetterTile" + tile.Value),
                    TriggerOn = TriggerCalls.OnWillApplyDamage,
                    SecondaryTriggerOn = [TriggerCalls.OnCombatEnd],
                    SecondaryDoesPopUpInfo = false,
                    SecondaryEffects =
                    [
                        Effects.GenerateEffect(ScriptableObject.CreateInstance<ConsumeItemEffect>(), 1, Targeting.Slot_SelfSlot),
                    ],
                };
                ItemUtils.JustAddItemSoItCanBeLoaded(lettertile.item);

                lootList.Add(new LootItemProbability("LetterTile_" + tile.Value + "_ExtraW", 1));
            }

            ExtraLootListEffect HitMe = ScriptableObject.CreateInstance<ExtraLootListEffect>();
            HitMe._treasurePercentage = 0;
            HitMe._shopPercentage = 0;
            HitMe._nothingPercentage = 0;
            HitMe._lootableItems = lootList;
            HitMe._lockedLootableItems = [];

            DamagePercentScrabbleModAndSecondaryEffect_Item scrabble = new DamagePercentScrabbleModAndSecondaryEffect_Item("LetterTileRack_ID", 2, true, false, true)
            {
                Item_ID = "LetterTileRack_SW",
                Name = "Letter Tile Rack",
                Flavour = "\"What's a \'zyqxuwy\'?\"",
                Description = "All damage dealt by this party member is increased by a percentage equal to twice the Scrabble score of the target's name." +
                "\nAt the end of combat, produce 1-3 random Letter Tiles.",
                IsShopItem = true,
                ShopPrice = 8,
                DoesPopUpInfo = true,
                StartsLocked = true,
                Icon = ResourceLoader.LoadSprite("UnlockTragedyPhobophobiaLongWords"),
                TriggerOn = TriggerCalls.OnWillApplyDamage,
                SecondaryTriggerOn = [TriggerCalls.OnCombatEnd],
                SecondaryDoesPopUpInfo = true,
                SecondaryEffects =
                [
                    Effects.GenerateEffect(HitMe, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(HitMe, 1, Targeting.Slot_SelfSlot, Effects.ChanceCondition(50)),
                    Effects.GenerateEffect(HitMe, 1, Targeting.Slot_SelfSlot, Effects.ChanceCondition(25)),
                ],
                OnUnlockUsesTHE = true,
            };

            string achievementID = "AApocrypha_Tragedy_PhobophobiaLongWords_ACH";
            string unlockID = "TragedyPhobophobiaLongWords";

            ItemUtils.AddItemToShopStatsCategoryAndGamePool(scrabble.item, new ItemModdedUnlockInfo(scrabble.Item_ID, ResourceLoader.LoadSprite("UnlockTragedyPhobophobiaLongWordsLocked", null, 32, null), achievementID));
            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, scrabble.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [scrabble.Item_ID],
            };

            ModdedAchievements unlockAchievement = new ModdedAchievements("Antidisestablishmentarianism", "Witness the actions of the fear of long words made manifest.", ResourceLoader.LoadSprite("AchievementTragedyPhobophobiaLongWords", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToInGameCategory(AchievementCategoryIDs.TragediesTitleLabel);

            Unlocks.AddUnlock_ByID(unlockData);
        }
    }
}
