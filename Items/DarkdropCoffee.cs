using System;
using System.Collections.Generic;
using System.Text;
using BrutalAPI.Items;

namespace A_Apocrypha.Items
{
    public class DarkdropCoffee
    {
        public static void Add()
        {
            PercentageEffectCondition OneInThree = ScriptableObject.CreateInstance<PercentageEffectCondition>();
            OneInThree.percentage = 33;

            PerformEffect_Item coffee = new PerformEffect_Item("DarkdropCoffee_ID", null, false)
            {
                Item_ID = "DarkdropCoffee_SW",
                Name = "Darkdrop Coffee",
                Flavour = "\"Almost certainly not brewed from bat guano!\"",
                Description = "Refresh this party member's movement after performing an ability. 33% chance to refresh ability usage as well.",
                IsShopItem = true,
                ShopPrice = 8,
                DoesPopUpInfo = true,
                StartsLocked = true,
                Icon = ResourceLoader.LoadSprite("UnlockDoulaWhitlock"),
                TriggerOn = TriggerCalls.OnAbilityUsed,
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<RestoreSwapUseEffect>(), 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<RefreshAbilityUseEffect>(), 1, Targeting.Slot_SelfSlot, OneInThree),
                ],
            };

            ItemUtils.AddItemToShopStatsCategoryAndGamePool(coffee.item, new ItemModdedUnlockInfo("DarkdropCoffee_SW", ResourceLoader.LoadSprite("UnlockDoulaWhitlockLocked", null, 32, null), "AApocrypha_Whitlock_Abstraction_ACH"));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement("AApocrypha_Whitlock_Abstraction_ACH", "DarkdropCoffee_SW");

            UnlockableModData whitlockDoulaUnlockData = new UnlockableModData("AApocrypha_Whitlock_Abstraction_Unlock")
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = "AApocrypha_Whitlock_Abstraction_ACH",
                hasItemUnlock = true,
                items = ["DarkdropCoffee_SW"],
            };

            FinalBossCharUnlockCheck UnlockAbstractionWhitlock = Unlocks.GetOrCreateUnlock_CustomFinalBoss("DoulaBoss", ResourceLoader.LoadSprite("DoulaPearl", null, 32, null));
            UnlockAbstractionWhitlock.AddUnlockData("Whitlock_CH", whitlockDoulaUnlockData);

            ModdedAchievements whitlockdoulaachievement = new ModdedAchievements("Darkdrop Coffee", "Unlocked a new item.", ResourceLoader.LoadSprite("AchievementDoulaWhitlock", null, 32, null), "AApocrypha_Whitlock_Abstraction_ACH");
            whitlockdoulaachievement.AddNewAchievementToCUSTOMCategory("AbstractionTitleLabel", "The Abstraction");
        }
    }
}
