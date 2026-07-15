using System;
using System.Collections.Generic;
using System.Text;
using A_Apocrypha.CustomOther;
using BrutalAPI.Items;

namespace A_Apocrypha.Items
{
    public class AtlasOfTheUnreal
    {
        public static void Add()
        {
            SetCombatEnvEffect smokethatshore = ScriptableObject.CreateInstance<SetCombatEnvEffect>();
            smokethatshore._envID = ParabolaHandler._parabolaID_SmokingShore_Env;

            CheckBundleDifficultyEffectorCondition NotBoss = ScriptableObject.CreateInstance<CheckBundleDifficultyEffectorCondition>();
            NotBoss._isEqual = false;

            AnimationVisualsEffect Transition = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            Transition._visuals = Visuals.RejectDeath;
            Transition._animationTarget = Targeting.Slot_SelfSlot;

            PerformEffect_Item dreambook = new PerformEffect_Item("AtlasOfTheUnreal_ID", null, false)
            {
                Item_ID = "AtlasOfTheUnreal_TW",
                Name = "Atlas of the Unreal",
                Flavour = "\"A work of futile impermanence.\"",
                Description = "Don't you think it's about time for a change of scenery?",
                IsShopItem = false,
                ShopPrice = 7,
                DoesPopUpInfo = true,
                StartsLocked = true,
                Icon = ResourceLoader.LoadSprite("UnlockKatalixiKneynsberg"),
                TriggerOn = TriggerCalls.OnBeforeCombatStart,
                Conditions = [NotBoss],
                Effects = [
                    Effects.GenerateEffect(Transition),
                    Effects.GenerateEffect(smokethatshore),
                ],
                OnUnlockUsesTHE = false,
            };

            string achievementID = "AApocrypha_Kneynsberg_Boundary_ACH";
            string unlockID = "AApocrypha_Kneynsberg_Boundary_Unlock";

            ItemUtils.AddItemToTreasureStatsCategoryAndGamePool(dreambook.item, new ItemModdedUnlockInfo(dreambook.Item_ID, ResourceLoader.LoadSprite("UnlockKatalixiKneynsbergLocked", null, 32, null), achievementID));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, dreambook.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [dreambook.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetOrCreateUnlock_CustomFinalBoss("Katalixi_BOSS", ResourceLoader.LoadSprite("KatalixiPearl", null, 32, null));
            unlockCheck.AddUnlockData("Kneynsberg_CH", unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements("Atlas of the Unreal", "Unlocked a new item.", ResourceLoader.LoadSprite("AchievementKatalixiKneynsberg", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToCUSTOMCategory("BoundaryTitleLabel", "The Boundary");
        }
    }
}
