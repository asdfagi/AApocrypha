using System;
using System.Collections.Generic;
using System.Text;
using BrutalAPI.Items;

namespace A_Apocrypha.Items
{
    public class FrontalLobeImplant
    {
        public static void Add()
        {
            StatusEffect_Apply_Effect Backflip = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            Backflip._Status = StatusField.GetCustomStatusEffect("Dodge_ID");

            PercentageEffectorCondition TheChance = ScriptableObject.CreateInstance<PercentageEffectorCondition>();
            TheChance.triggerPercentage = 10;

            PerformEffect_Item implant = new PerformEffect_Item("FrontalLobeImplant_ID", null, false)
            {
                Item_ID = "FrontalLobeImplant_TW",
                Name = "Frontal Lobe Implant",
                Flavour = "\"See them coming.\"",
                Description = "When anything performs an ability, 10% chance to apply 1 Dodge to this party member.",
                IsShopItem = false,
                ShopPrice = 6,
                DoesPopUpInfo = true,
                StartsLocked = true,
                Icon = ResourceLoader.LoadSprite("UnlockBlueSkyNaudiz4"),
                TriggerOn = TriggerCalls.OnAnyAbilityUsed,
                Conditions = [TheChance],
                Effects =
                [
                    Effects.GenerateEffect(Backflip, 1, Targeting.Slot_SelfSlot),
                ],
                OnUnlockUsesTHE = true,
            };

            string achievementID = "AApocrypha_Naudiz4_Dreamer_ACH";
            string unlockID = "AApocrypha_Naudiz4_Dreamer_Unlock";

            ItemUtils.AddItemToTreasureStatsCategoryAndGamePool(implant.item, new ItemModdedUnlockInfo(implant.Item_ID, ResourceLoader.LoadSprite("UnlockBlueSkyNaudiz4Locked", null, 32, null), achievementID));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, implant.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [implant.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetOrCreateUnlock_CustomFinalBoss("BlueSky_BOSS", ResourceLoader.LoadSprite("BlueSkyPearl", null, 32, null));
            unlockCheck.AddUnlockData("Naudiz4_CH", unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements("Frontal Lobe Implant", "Unlocked a new item.", ResourceLoader.LoadSprite("AchievementBlueSkyNaudiz4", null, 32, null), achievementID);
            unlockAchievement.IsSecret = true;
            unlockAchievement.AddNewAchievementToCUSTOMCategory("BlueSky_BOSS", "The Dreamer");
        }
    }
}
