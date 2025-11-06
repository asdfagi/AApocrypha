using System;
using System.Collections.Generic;
using System.Text;
using BrutalAPI.Items;

namespace A_Apocrypha.Items
{
    public class EyelessSkull
    {
        public static void Add()
        {
            AddPassiveEffect Irrigo = ScriptableObject.CreateInstance<AddPassiveEffect>();
            Irrigo._passiveToAdd = Passives.Forgetful;

            PerformEffect_Item nadirskull = new PerformEffect_Item("EyelessSkull_ID", null, false)
            {
                Item_ID = "EyelessSkull_TW",
                Name = "Eyeless Skull",
                Flavour = "\"Who... where are you?\"",
                Description = "At the end of each turn, apply Forgetful to the enemy Opposing this party member.",
                IsShopItem = false,
                ShopPrice = 5,
                DoesPopUpInfo = true,
                StartsLocked = true,
                Icon = ResourceLoader.LoadSprite("UnlockMarchWhitlock"),
                TriggerOn = TriggerCalls.OnTurnFinished,
                Effects =
                [
                    Effects.GenerateEffect(Irrigo, 1, Targeting.Slot_Front),
                ],
            };

            ItemUtils.AddItemToTreasureStatsCategoryAndGamePool(nadirskull.item, new ItemModdedUnlockInfo("EyelessSkull_TW", ResourceLoader.LoadSprite("UnlockMarchWhitlockLocked", null, 32, null), "AApocrypha_Whitlock_Inevitable_ACH"));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement("AApocrypha_Whitlock_Inevitable_ACH", "EyelessSkull_TW");

            UnlockableModData whitlockMarchUnlockData = new UnlockableModData("AApocrypha_Whitlock_Inevitable_Unlock")
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = "AApocrypha_Whitlock_Inevitable_ACH",
                hasItemUnlock = true,
                items = ["EyelessSkull_TW"],
            };

            FinalBossCharUnlockCheck UnlockInevitableWhitlock = Unlocks.GetOrCreateUnlock_CustomFinalBoss("March_BOSS", ResourceLoader.LoadSprite("MarchPearl", null, 32, null));
            UnlockInevitableWhitlock.AddUnlockData("Whitlock_CH", whitlockMarchUnlockData);

            ModdedAchievements whitlockmarchachievement = new ModdedAchievements("Eyeless Skull", "Unlocked a new item.", ResourceLoader.LoadSprite("AchievementMarchWhitlock", null, 32, null), "AApocrypha_Whitlock_Inevitable_ACH");
            whitlockmarchachievement.AddNewAchievementToCUSTOMCategory("InevitableTitleLabel", "The Inevitable");
        }
    }
}
