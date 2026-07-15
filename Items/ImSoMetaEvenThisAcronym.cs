using System;
using System.Collections.Generic;
using System.Text;
using BrutalAPI.Items;

namespace A_Apocrypha.Items
{
    public class ImSoMetaEvenThisAcronym
    {
        public static void Add()
        {
            PassiveLockingEffect IgnorePure = ScriptableObject.CreateInstance<PassiveLockingEffect>();
            IgnorePure._lock = true;
            IgnorePure.m_PassiveIDs = [Passives.Pure.m_PassiveID];

            PassiveLockingEffect AllowPure = ScriptableObject.CreateInstance<PassiveLockingEffect>();
            AllowPure._lock = false;
            AllowPure.m_PassiveIDs = [Passives.Pure.m_PassiveID];

            ChangeToRandomHealthColorEffect purpleredify = ScriptableObject.CreateInstance<ChangeToRandomHealthColorEffect>();
            purpleredify._healthColors = [Pigments.RedPurple];

            PerformEffect_Item ismeta = new PerformEffect_Item("AApocryphaItem_ID", null, false)
            {
                Item_ID = "AApocryphaItem_SW",
                Name = "Asdfagi's Abominable Apocrypha",
                Flavour = "\"Wait, what?\"",
                Description = "At the beginning of combat, change all units' health colors to Split Red/Purple, ignoring the effects of Pure.",
                IsShopItem = true,
                ShopPrice = 6,
                DoesPopUpInfo = true,
                StartsLocked = true,
                Icon = ResourceLoader.LoadSprite("UnlockKatalixiAnnaMolly"),
                TriggerOn = TriggerCalls.OnCombatStart,
                Effects =
                [
                    Effects.GenerateEffect(IgnorePure),
                    Effects.GenerateEffect(purpleredify, 1, Targeting.AllUnits),
                    Effects.GenerateEffect(AllowPure),
                ],
                OnUnlockUsesTHE = false,
            };

            string achievementID = "AApocrypha_AnnaMolly_Boundary_ACH";
            string unlockID = "AApocrypha_AnnaMolly_Boundary_Unlock";

            ItemUtils.AddItemToShopStatsCategoryAndGamePool(ismeta.item, new ItemModdedUnlockInfo(ismeta.Item_ID, ResourceLoader.LoadSprite("UnlockKatalixiAnnaMollyLocked", null, 32, null), achievementID));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, ismeta.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [ismeta.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetOrCreateUnlock_CustomFinalBoss("Katalixi_BOSS", ResourceLoader.LoadSprite("KatalixiPearl", null, 16, null));
            unlockCheck.AddUnlockData("ThresholdFool_CH", unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements("Asdfagi's Abominable Apocrypha", "Unlocked a new item.", ResourceLoader.LoadSprite("AchievementKatalixiAnnaMolly", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToCUSTOMCategory("BoundaryTitleLabel", "The Boundary");
        }
    }
}
