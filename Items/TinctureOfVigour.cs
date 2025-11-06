using System;
using System.Collections.Generic;
using System.Text;
using BrutalAPI.Items;

namespace A_Apocrypha.Items
{
    public class TinctureOfVigour
    {
        public static void Add()
        {
            FullHealthDetectionEffectorCondition Injured = ScriptableObject.CreateInstance<FullHealthDetectionEffectorCondition>();
            Injured.checkFullHealth = false;

            ExtraLootOptionsEffect HalfFull = ScriptableObject.CreateInstance<ExtraLootOptionsEffect>();
            HalfFull._itemName = "TinctureOfVigourHalf_ExtraW";

            PerformEffect_Item tincture = new PerformEffect_Item("TinctureOfVigour_ID", null, false)
            {
                Item_ID = "TinctureOfVigour_SW",
                Name = "Tincture of Vigour",
                Flavour = "\"Cures pain, sets bones, curls hair, wards off spiders.\"",
                Description = "At the start of combat, if this party member is not at full health, heal them 10 health.\nContains two doses.",
                IsShopItem = true,
                ShopPrice = 6,
                DoesPopUpInfo = true,
                StartsLocked = true,
                Icon = ResourceLoader.LoadSprite("UnlockOsmanWhitlock"),
                TriggerOn = TriggerCalls.OnCombatStart,
                Conditions = [Injured],
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<HealEffect>(), 10, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<ConsumeItemEffect>(), 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(HalfFull),
                ],
            };

            PerformEffect_Item tincturehalf = new PerformEffect_Item("TinctureOfVigourHalf_ID", null, false)
            {
                Item_ID = "TinctureOfVigourHalf_ExtraW",
                Name = "Tincture of Vigour",
                Flavour = "\"Cures pain, sets bones, curls hair, wards off spiders.\"",
                Description = "At the start of combat, if this party member is not at full health, heal them 10 health.\nContains one dose.",
                IsShopItem = false,
                ShopPrice = 3,
                DoesPopUpInfo = true,
                StartsLocked = false,
                Icon = ResourceLoader.LoadSprite("UnlockOsmanWhitlockAlt"),
                TriggerOn = TriggerCalls.OnCombatStart,
                Conditions = [Injured],
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<HealEffect>(), 10, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<ConsumeItemEffect>(), 1, Targeting.Slot_SelfSlot),
                ],
            };

            ItemUtils.AddItemToShopStatsCategoryAndGamePool(tincture.item, new ItemModdedUnlockInfo("TinctureOfVigour_SW", ResourceLoader.LoadSprite("UnlockOsmanWhitlockLocked", null, 32, null), "AApocrypha_Whitlock_Witness_ACH"));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement("AApocrypha_Whitlock_Witness_ACH", "TinctureOfVigour_SW");

            UnlockableModData whitlockOsmanUnlockData = new UnlockableModData("AApocrypha_Whitlock_Witness_Unlock")
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = "AApocrypha_Whitlock_Witness_ACH",
                hasItemUnlock = true,
                items = ["TinctureOfVigour_SW"],
            };

            FinalBossCharUnlockCheck UnlockWitnessWhitlock = Unlocks.GetUnlock_OsmanFinalBoss();
            UnlockWitnessWhitlock.AddUnlockData("Whitlock_CH", whitlockOsmanUnlockData);

            ModdedAchievements whitlockosmanachievement = new ModdedAchievements("Tincture of Vigour", "Unlocked a new item.", ResourceLoader.LoadSprite("AchievementOsmanWhitlock", null, 32, null), "AApocrypha_Whitlock_Witness_ACH");
            whitlockosmanachievement.AddNewAchievementToInGameCategory(AchievementCategoryIDs.WitnessTitleLabel);

            ItemUtils.JustAddItemSoItCanBeLoaded(tincturehalf.item);
        }
    }
}
