using System;
using System.Collections.Generic;
using System.Text;
using BrutalAPI.Items;

namespace A_Apocrypha.Items
{
    public class ToadFungusRorschach
    {
        public static void Add()
        {
            TargetSplitOrReplaceHealthFromListEffect colorize = ScriptableObject.CreateInstance<TargetSplitOrReplaceHealthFromListEffect>();
            colorize._colors = [Pigments.Red, Pigments.Blue, Pigments.Yellow, Pigments.Purple];
            colorize._transformBlacklist = false;
            colorize._colorBlacklist = [Pigments.Grey];

            DamageIntModHealthColorsAndSecondaryEffect_Item tmtrained = new DamageIntModHealthColorsAndSecondaryEffect_Item("ToadFungusRorschach_ID", 2, true, false, true)
            {
                Item_ID = "ToadFungusRorschach_TW",
                Name = "oad?ny Mushroo█Rorscha^^",
                Flavour = "\"gspaw's a bear!Why are y\"",
                Description = "At the start of each turn, split the Opposing enemy's health with a random color if it isn't grey." +
                "\nThis party member deals 2 more damage to a target for each non-grey color in their health color.",
                IsShopItem = false,
                ShopPrice = -1,
                DoesPopUpInfo = true,
                StartsLocked = true,
                Icon = ResourceLoader.LoadSprite("UnlockHeavenAnnaMolly"),
                TriggerOn = TriggerCalls.OnWillApplyDamage,
                SecondaryTriggerOn = [TriggerCalls.OnTurnStart],
                SecondaryDoesPopUpInfo = true,
                SecondaryEffects =
                [
                    Effects.GenerateEffect(colorize, 1, Targeting.Slot_Front),
                ],
            };

            string achievementID = "AApocrypha_AnnaMolly_Divine_ACH";
            string unlockID = "AApocrypha_AnnaMolly_Divine_Unlock";

            ItemUtils.AddItemToTreasureStatsCategoryAndGamePool(tmtrained.item, new ItemModdedUnlockInfo(tmtrained.Item_ID, ResourceLoader.LoadSprite("UnlockHeavenAnnaMollyLocked", null, 32, null), achievementID));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, tmtrained.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [tmtrained.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetUnlock_HeavenFinalBoss();
            unlockCheck.AddUnlockData("ThresholdFool_CH", unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements("oad?ny Mushroo█Rorscha^^", "Unlocked a new item.", ResourceLoader.LoadSprite("AchievementHeavenAnnaMolly", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToInGameCategory(AchievementCategoryIDs.DivineTitleLabel);
        }
    }
}
