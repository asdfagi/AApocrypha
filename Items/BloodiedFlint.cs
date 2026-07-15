using System;
using System.Collections.Generic;
using System.Text;
using A_Apocrypha.CustomOther;
using BrutalAPI.Items;

namespace A_Apocrypha.Items
{
    public class BloodiedFlint
    {
        public static void Add()
        {
            PassiveLockingEffect IgnorePure = ScriptableObject.CreateInstance<PassiveLockingEffect>();
            IgnorePure._lock = true;
            IgnorePure.m_PassiveIDs = [Passives.Pure.m_PassiveID];

            PassiveLockingEffect AllowPure = ScriptableObject.CreateInstance<PassiveLockingEffect>();
            AllowPure._lock = false;
            AllowPure.m_PassiveIDs = [Passives.Pure.m_PassiveID];

            TargetHasCasterHealthColorEffectorCondition otherColorCondition = ScriptableObject.CreateInstance<TargetHasCasterHealthColorEffectorCondition>();
            otherColorCondition._passIfTrue = false;

            ChangeToRandomHealthColorEffect shed = ScriptableObject.CreateInstance<ChangeToRandomHealthColorEffect>();
            shed._healthColors = [Pigments.Red, Pigments.Blue, Pigments.Yellow, Pigments.Purple];

            DamagePercentModAndSecondaryEffect_Item flint = new DamagePercentModAndSecondaryEffect_Item("BloodiedFlint_ID", 50, true, false, true)
            {
                Item_ID = "BloodiedFlint_TW",
                Name = "Bloodied Flint",
                Flavour = "\"What it has been, it works to destroy.\"",
                Description = "This party member deals 50% more damage to targets with a different health color." +
                "\nAt the end of each turn, randomize this party member's health color, ignoring the effects of Pure.",
                IsShopItem = false,
                ShopPrice = 7,
                DoesPopUpInfo = true,
                StartsLocked = true,
                Icon = ResourceLoader.LoadSprite("UnlockDeathmatchWhitlock"),
                TriggerOn = TriggerCalls.OnWillApplyDamage,
                Conditions = [otherColorCondition],
                SecondaryTriggerOn = [TriggerCalls.OnRoundFinished],
                SecondaryConditions = [],
                SecondaryDoesPopUpInfo = true,
                SecondaryEffects = [
                    Effects.GenerateEffect(IgnorePure),
                    Effects.GenerateEffect(shed, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(AllowPure),
                ],
                OnUnlockUsesTHE = true,
            };

            string achievementID = "AApocrypha_Whitlock_Antagonist_ACH";
            string unlockID = "AApocrypha_Whitlock_Antagonist_Unlock";

            ItemUtils.AddItemToTreasureStatsCategoryAndGamePool(flint.item, new ItemModdedUnlockInfo(flint.Item_ID, ResourceLoader.LoadSprite("UnlockDeathmatchWhitlockLocked", null, 32, null), achievementID));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, flint.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [flint.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetOrCreateUnlock_CustomFinalBoss("Deathmatch_BOSS", ResourceLoader.LoadSprite("DeathmatchPearl", null, 32, null));
            unlockCheck.AddUnlockData("Whitlock_CH", unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements("Bloodied Flint", "Unlocked a new item.", ResourceLoader.LoadSprite("AchievementDeathmatchWhitlock", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToCUSTOMCategory("AntagonistTitleLabel", "The Antagonist");

            //WhitlockCharacter.whitlock.AddFinalBossAchievementData("Deathmatch_BOSS", achievementID);
            LoadedAssetsHandler.GetCharacter("Whitlock_CH").m_BossAchData.Add(new CharFinalBossAchData("Deathmatch_BOSS", achievementID));
        }
    }
}
