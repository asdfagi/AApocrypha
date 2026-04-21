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

            evilhoney.item._ItemTypeIDs =
            [
                "FoodID",
            ];

            string achievementID = "AApocrypha_Whitlock_Forgotten_ACH";
            string unlockID = "AApocrypha_Whitlock_Forgotten_Unlock";

            ItemUtils.AddItemToTreasureStatsCategoryAndGamePool(evilhoney.item, new ItemModdedUnlockInfo(evilhoney.Item_ID, ResourceLoader.LoadSprite("UnlockNobodyWhitlockLocked", null, 32, null), achievementID));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, evilhoney.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [evilhoney.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetOrCreateUnlock_CustomFinalBoss("Nobody_BOSS", ResourceLoader.LoadSprite("NobodyPearl", null, 32, null));
            unlockCheck.AddUnlockData("Whitlock_CH", unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements("Cardinal's Honey", "Unlocked a new item.", ResourceLoader.LoadSprite("AchievementNobodyWhitlock", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToCUSTOMCategory("ForgottenTitleLabel", "The Forgotten");
        }
    }
}
