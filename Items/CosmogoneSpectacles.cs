using System;
using System.Collections.Generic;
using System.Text;
using BrutalAPI.Items;

namespace A_Apocrypha.Items
{
    internal class CosmogoneSpectacles
    {
        public static void Add()
        {
            FullHealthDetectionEffectorCondition Injured = ScriptableObject.CreateInstance<FullHealthDetectionEffectorCondition>();
            Injured.checkFullHealth = false;

            FieldEffect_Apply_Effect ApplyShield = ScriptableObject.CreateInstance<FieldEffect_Apply_Effect>();
            ApplyShield._Field = StatusField.Shield;

            DoublePerformEffect_Item spectacles = new DoublePerformEffect_Item("CosmogoneSpectacles_ID", null, false)
            {
                Item_ID = "CosmogoneSpectacles_SW",
                Name = "Cosmogone Spectacles",
                Flavour = "\"The lenses are the colour of remembered sunshine.\"",
                Description = "Before this party member performs an ability, mirror their position.\nAfter this party member performs an ability, apply 4 Shield to their position.",
                IsShopItem = true,
                ShopPrice = 6,
                DoesPopUpInfo = false,
                StartsLocked = true,
                Icon = ResourceLoader.LoadSprite("UnlockOsmanKneynsberg"),
                TriggerOn = TriggerCalls.OnAbilityUsed,
                Effects =
                [
                    Effects.GenerateEffect(ApplyShield, 4, Targeting.Slot_SelfSlot),
                ],
                SecondaryDoesPopUpInfo = true,
                SecondaryTriggerOn = [TriggerCalls.OnAbilityWillBeUsed],
                SecondaryEffects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<MirrorPositionEffect>(), 1, Targeting.Slot_SelfSlot),
                ],
            };

            ItemUtils.AddItemToShopStatsCategoryAndGamePool(spectacles.item, new ItemModdedUnlockInfo("CosmogoneSpectacles_SW", ResourceLoader.LoadSprite("UnlockOsmanKneynsbergLocked", null, 32, null), "AApocrypha_Kneynsberg_Witness_ACH"));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement("AApocrypha_Kneynsberg_Witness_ACH", "CosmogoneSpectacles_SW");

            UnlockableModData kneynsbergOsmanUnlockData = new UnlockableModData("AApocrypha_Kneynsberg_Witness_Unlock")
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = "AApocrypha_Kneynsberg_Witness_ACH",
                hasItemUnlock = true,
                items = ["CosmogoneSpectacles_SW"],
            };

            FinalBossCharUnlockCheck UnlockWitnessKneynsberg = Unlocks.GetUnlock_OsmanFinalBoss();
            UnlockWitnessKneynsberg.AddUnlockData("Kneynsberg_CH", kneynsbergOsmanUnlockData);

            ModdedAchievements kneynsbergosmanachievement = new ModdedAchievements("Cosmogone Spectacles", "Unlocked a new item.", ResourceLoader.LoadSprite("AchievementOsmanKneynsberg", null, 32, null), "AApocrypha_Kneynsberg_Witness_ACH");
            kneynsbergosmanachievement.AddNewAchievementToInGameCategory(AchievementCategoryIDs.WitnessTitleLabel);
        }
    }
}
