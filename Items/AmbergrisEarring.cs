using System;
using System.Collections.Generic;
using System.Text;
using A_Apocrypha.CustomOther;
using BrutalAPI.Items;

namespace A_Apocrypha.Items
{
    public class AmbergrisEarring
    {
        public static void Add()
        {
            ReduceStatusEffectsEffect diminish = ScriptableObject.CreateInstance<ReduceStatusEffectsEffect>();
            diminish._decreaseNegatives = true;
            diminish._decreasePositives = false;

            PerformEffect_Item earring = new PerformEffect_Item("AmbergrisEarring_ID", null, false)
            {
                Item_ID = "AmbergrisEarring_SW",
                Name = "Always-Returning Ambergris Earring",
                Flavour = "\"A helpful reminder in disorienting situations.\"",
                Description = "Reduce the duration of all negative status effects on this party member by 1 when manually moving.",
                IsShopItem = true,
                ShopPrice = 6,
                DoesPopUpInfo = true,
                StartsLocked = true,
                Icon = ResourceLoader.LoadSprite("UnlockDoulaVaughan"),
                TriggerOn = TriggerCalls.OnSwapTo,
                Conditions = [ScriptableObject.CreateInstance<CasterHasStatusEffectorCondition>()],
                Effects = [Effects.GenerateEffect(diminish, 1, Targeting.Slot_SelfSlot)],
                OnUnlockUsesTHE = true,
            };

            string achievementID = "AApocrypha_Vaughan_Abstraction_ACH";
            string unlockID = "AApocrypha_Vaughan_Abstraction_Unlock";

            ItemUtils.AddItemToShopStatsCategoryAndGamePool(earring.item, new ItemModdedUnlockInfo(earring.Item_ID, ResourceLoader.LoadSprite("UnlockDoulaVaughanLocked", null, 32, null), achievementID));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, earring.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [earring.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetOrCreateUnlock_CustomFinalBoss("DoulaBoss", ResourceLoader.LoadSprite("DoulaPearl", null, 32, null));
            unlockCheck.AddUnlockData("Vaughan_CH", unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements("Always-Returning Ambergris Earring", "Unlocked a new item.", ResourceLoader.LoadSprite("AchievementDoulaVaughan", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToCUSTOMCategory("AbstractionTitleLabel", "The Abstraction");
        }
    }
}
