using System;
using System.Collections.Generic;
using System.Text;
using BrutalAPI.Items;

namespace A_Apocrypha.Items
{
    public class CardinalHoney
    {
        public static void Add()
        {
            ExtraPassiveAbility_Wearable_SMS wearablePassiveDying = ScriptableObject.CreateInstance<ExtraPassiveAbility_Wearable_SMS>();
            wearablePassiveDying._extraPassiveAbility = Passives.Dying;

            DamagePercentageModifier_Item evilhoney = new DamagePercentageModifier_Item("CardinalHoney_ID", 50, false, false, false)
            {
                Item_ID = "CardinalHoney_TW",
                Name = "Cardinal's Honey",
                Flavour = "\"It smells sweetly of rot and sugar and damp wool.\"",
                Description = "This party member now has Dying as a passive, but takes 50% less damage from all sources.",
                IsShopItem = false,
                ShopPrice = 10,
                DoesPopUpInfo = true,
                StartsLocked = true,
                Icon = ResourceLoader.LoadSprite("UnlockNobodyWhitlock"),
                TriggerOn = TriggerCalls.OnBeingDamaged,
                EquippedModifiers = [wearablePassiveDying],
            };

            ItemUtils.AddItemToTreasureStatsCategoryAndGamePool(evilhoney.item, new ItemModdedUnlockInfo("CardinalHoney_TW", ResourceLoader.LoadSprite("UnlockNobodyWhitlockLocked", null, 32, null), "AApocrypha_Whitlock_Forgotten_ACH"));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement("AApocrypha_Whitlock_Forgotten_ACH", "CardinalHoney_TW");

            UnlockableModData whitlockNobodyUnlockData = new UnlockableModData("AApocrypha_Whitlock_Forgotten_Unlock")
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = "AApocrypha_Whitlock_Forgotten_ACH",
                hasItemUnlock = true,
                items = ["CardinalHoney_TW"],
            };

            FinalBossCharUnlockCheck UnlockForgottenWhitlock = Unlocks.GetOrCreateUnlock_CustomFinalBoss("Nobody_BOSS", ResourceLoader.LoadSprite("NobodyPearl", null, 32, null));
            UnlockForgottenWhitlock.AddUnlockData("Whitlock_CH", whitlockNobodyUnlockData);

            ModdedAchievements whitlocknobodyachievement = new ModdedAchievements("Cardinal's Honey", "Unlocked a new item.", ResourceLoader.LoadSprite("AchievementNobodyWhitlock", null, 32, null), "AApocrypha_Whitlock_Forgotten_ACH");
            whitlocknobodyachievement.AddNewAchievementToCUSTOMCategory("ForgottenTitleLabel", "The Forgotten");
        }
    }
}
