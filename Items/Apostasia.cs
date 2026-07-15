using System;
using System.Collections.Generic;
using System.Text;
using A_Apocrypha.CustomOther;
using BrutalAPI.Items;

namespace A_Apocrypha.Items
{
    public class Apostasia
    {
        public static void Add()
        {
            StatusEffect_ApplyPermanent_Effect poopleEffect = ScriptableObject.CreateInstance<StatusEffect_ApplyPermanent_Effect>();
            poopleEffect._Status = StatusField.OilSlicked;

            DamagePercentModAndSecondaryEffect_Item apostasia = new DamagePercentModAndSecondaryEffect_Item("Apostasia_ID", 50, true, false, true)
            {
                Item_ID = "Apostasia_TW",
                Name = "Apostasia",
                Flavour = "\"Left it all behind\"",
                Description = "This party member deals 50% more damage to enemies of which there are at least two in combat.",
                IsShopItem = false,
                ShopPrice = 6,
                DoesPopUpInfo = true,
                StartsLocked = true,
                Icon = ResourceLoader.LoadSprite("UnlockMarchHaborym"),
                TriggerOn = TriggerCalls.OnWillApplyDamage,
                Conditions = [ScriptableObject.CreateInstance<DetectTargetDuplicateEffectorCondition>()],
                SecondaryTriggerOn = [],
                SecondaryConditions = [],
                SecondaryEffects =
                [
                ],
            };

            string achievementID = "AApocrypha_Haborym_Inevitable_ACH";
            string unlockID = "AApocrypha_Haborym_Inevitable_Unlock";

            ItemUtils.AddItemToTreasureStatsCategoryAndGamePool(apostasia.item, new ItemModdedUnlockInfo(apostasia.Item_ID, ResourceLoader.LoadSprite("UnlockMarchHaborymLocked", null, 32, null), achievementID));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, apostasia.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [apostasia.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetOrCreateUnlock_CustomFinalBoss("March_BOSS", ResourceLoader.LoadSprite("MarchPearl", null, 32, null));
            unlockCheck.AddUnlockData("Haborym_CH", unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements("Apostasia", "Unlocked a new item.", ResourceLoader.LoadSprite("AchievementMarchHaborym", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToCUSTOMCategory("InevitableTitleLabel", "The Inevitable");
        }
    }
}
