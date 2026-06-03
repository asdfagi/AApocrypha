using System;
using System.Collections.Generic;
using System.Text;
using BrutalAPI.Items;

namespace A_Apocrypha.Items
{
    public class ElementOfDawn
    {
        public static void Add()
        {
            StatusEffect_Apply_Effect Smoulder = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            Smoulder._Status = StatusField.GetCustomStatusEffect("Smouldering_ID");

            DoublePerformEffect_Item dawnburn = new DoublePerformEffect_Item("ElementOfDawn_ID", null, false)
            {
                Item_ID = "ElementOfDawn_TW",
                Name = "Element of Dawn",
                Flavour = "\"HE SUN THE SUN THE SUN TH\"",
                Description = "When this party member deals direct damage, apply one third of the damage dealt as Smouldering to the damaged unit.\nApply 1 Smouldering to this party member if they kill something.",
                IsShopItem = false,
                ShopPrice = 8,
                DoesPopUpInfo = false,
                StartsLocked = true,
                Icon = ResourceLoader.LoadSprite("UnlockHeavenAmbrose"),
                TriggerOn = TriggerCalls.OnWillApplyDamage,
                Conditions = [ScriptableObject.CreateInstance<ElementOfDawnHitEffectCondition>()],
                Effects = [],
                SecondaryDoesPopUpInfo = true,
                SecondaryTriggerOn = [TriggerCalls.OnKill],
                SecondaryEffects =
                [
                    Effects.GenerateEffect(Smoulder, 1, Targeting.Slot_SelfSlot),
                ],
                OnUnlockUsesTHE = true,
            };

            string achievementID = "AApocrypha_Ambrose_Divine_ACH";
            string unlockID = "AApocrypha_Ambrose_Divine_Unlock";

            ItemUtils.AddItemToTreasureStatsCategoryAndGamePool(dawnburn.item, new ItemModdedUnlockInfo(dawnburn.Item_ID, ResourceLoader.LoadSprite("UnlockHeavenAmbroseLocked", null, 32, null), achievementID));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, dawnburn.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [dawnburn.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetUnlock_HeavenFinalBoss();
            unlockCheck.AddUnlockData("Ambrose_CH", unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements("Element of Dawn", "Unlocked a new item.", ResourceLoader.LoadSprite("AchievementHeavenAmbrose", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToInGameCategory(AchievementCategoryIDs.DivineTitleLabel);
        }
    }
}
