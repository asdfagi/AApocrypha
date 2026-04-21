using System;
using System.Collections.Generic;
using System.Text;
using BrutalAPI.Items;

namespace A_Apocrypha.Items
{
    public class RadarDish
    {
        public static void Add()
        {
            RemoveFieldEffectEffect NoHairs = ScriptableObject.CreateInstance<RemoveFieldEffectEffect>();
            NoHairs._field = StatusField.GetCustomFieldEffect("Crosshairs_ID");

            FieldEffect_Apply_Effect YesHairs = ScriptableObject.CreateInstance<FieldEffect_Apply_Effect>();
            YesHairs._Field = StatusField.GetCustomFieldEffect("Crosshairs_ID");

            RemoveFieldEffectEffect NoShield = ScriptableObject.CreateInstance<RemoveFieldEffectEffect>();
            NoShield._field = StatusField.Shield;

            RandomTargetPerformEffectViaSubaction theEffect = ScriptableObject.CreateInstance<RandomTargetPerformEffectViaSubaction>();
            theEffect.effects = [
                Effects.GenerateEffect(YesHairs, 5, Targeting.Slot_SelfSlot),
                Effects.GenerateEffect(NoShield, 1, Targeting.Slot_SelfSlot),
            ];

            PerformEffect_Item radar = new PerformEffect_Item("RadarDish_ID", null, false)
            {
                Item_ID = "RadarDish_SW",
                Name = "Radar Dish",
                Flavour = "\"Shoot Here!\"",
                Description = "At the start of each turn, remove Crosshairs from all enemy positions, then apply 5 Crosshairs to a random occupied enemy position and remove all Shield from that position.",
                IsShopItem = true,
                ShopPrice = 6,
                DoesPopUpInfo = true,
                StartsLocked = true,
                Icon = ResourceLoader.LoadSprite("UnlockNobodyNaudiz4"),
                TriggerOn = TriggerCalls.OnTurnStart,
                Effects =
                [
                    Effects.GenerateEffect(NoHairs, 1, Targeting.Unit_AllOpponentSlots),
                    Effects.GenerateEffect(theEffect, 1, Targeting.Unit_AllOpponentSlots),
                ],
                OnUnlockUsesTHE = true,
            };

            string achievementID = "AApocrypha_Naudiz4_Forgotten_ACH";
            string unlockID = "AApocrypha_Naudiz4_Forgotten_Unlock";

            ItemUtils.AddItemToShopStatsCategoryAndGamePool(radar.item, new ItemModdedUnlockInfo(radar.Item_ID, ResourceLoader.LoadSprite("UnlockNobodyWhitlockLocked", null, 32, null), achievementID));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, radar.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [radar.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetOrCreateUnlock_CustomFinalBoss("Nobody_BOSS", ResourceLoader.LoadSprite("NobodyPearl", null, 32, null));
            unlockCheck.AddUnlockData("Naudiz4_CH", unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements("Radar Dish", "Unlocked a new item.", ResourceLoader.LoadSprite("AchievementNobodyNaudiz4", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToCUSTOMCategory("ForgottenTitleLabel", "The Forgotten");
        }
    }
}
