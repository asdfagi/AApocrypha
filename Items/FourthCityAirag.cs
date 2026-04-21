using System;
using System.Collections.Generic;
using System.Text;
using BrutalAPI.Items;

namespace A_Apocrypha.Items
{
    public class FourthCityAirag
    {
        public static void Add()
        {
            ExtraPassiveAbility_Wearable_SMS wearablePassiveConfrontational = ScriptableObject.CreateInstance<ExtraPassiveAbility_Wearable_SMS>();
            wearablePassiveConfrontational._extraPassiveAbility = Passives.GetCustomPassive("Confrontational_PA");

            PreviousEffectCondition PreviousTrue = ScriptableObject.CreateInstance<PreviousEffectCondition>();
            PreviousTrue.wasSuccessful = true;

            FieldEffect_Apply_Effect ApplyShield = ScriptableObject.CreateInstance<FieldEffect_Apply_Effect>();
            ApplyShield._Field = StatusField.Shield;

            PerformEffect_Item airag = new PerformEffect_Item("FourthCityAirag_ID", null, false)
            {
                Item_ID = "FourthCityAirag_SW",
                Name = "Fourth City Airag",
                Flavour = "\"For the Khan of Dreams.\"",
                Description = "This party member now has Confrontational as a passive. Gain 3 Shield on moving or being moved in front of an enemy.",
                IsShopItem = true,
                ShopPrice = 6,
                DoesPopUpInfo = true,
                StartsLocked = true,
                Icon = ResourceLoader.LoadSprite("UnlockBlueSkyWhitlock"),
                TriggerOn = TriggerCalls.OnMoved,
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<CheckHasUnitEffect>(), 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(ApplyShield, 3, Targeting.Slot_SelfSlot, PreviousTrue),
                ],
                EquippedModifiers = [wearablePassiveConfrontational],
            };

            airag.item._ItemTypeIDs =
            [
                "FoodID",
                "Drink",
            ];

            string achievementID = "AApocrypha_Whitlock_Dreamer_ACH";
            string unlockID = "AApocrypha_Whitlock_Dreamer_Unlock";

            ItemUtils.AddItemToShopStatsCategoryAndGamePool(airag.item, new ItemModdedUnlockInfo(airag.Item_ID, ResourceLoader.LoadSprite("UnlockBlueSkyWhitlockLocked", null, 32, null), achievementID));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, airag.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [airag.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetOrCreateUnlock_CustomFinalBoss("BlueSky_BOSS", ResourceLoader.LoadSprite("BlueSkyPearl", null, 32, null));
            unlockCheck.AddUnlockData("Whitlock_CH", unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements("Fourth City Airag", "Unlocked a new item.", ResourceLoader.LoadSprite("AchievementBlueSkyWhitlock", null, 32, null), achievementID);
            unlockAchievement.IsSecret = true;
            unlockAchievement.AddNewAchievementToCUSTOMCategory("BlueSky_BOSS", "The Dreamer");
        }
    }
}
