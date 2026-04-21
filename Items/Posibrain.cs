using System;
using System.Collections.Generic;
using System.Text;
using A_Apocrypha.CustomOther;
using BrutalAPI.Items;

namespace A_Apocrypha.Items
{
    internal class Posibrain
    {
        public static void Add()
        {
            CopyAndSpawnOneOfCustomCharactersAnywhereEffect MakeRobot = ScriptableObject.CreateInstance<CopyAndSpawnOneOfCustomCharactersAnywhereEffect>();
            MakeRobot._characterCopies = ["AA_RobotMinionClaw_CH", "AA_RobotMinionSaw_CH"];
            MakeRobot._permanentSpawn = false;
            MakeRobot._rank = 0;
            MakeRobot._usePreviousAsHealth = false;
            MakeRobot._extraModifiers = [];
            MakeRobot._nameAddition = new NameAdditionLocID();

            PerformEffect_Item posibrain = new PerformEffect_Item("PositronicBrain_ID", null, false)
            {
                Item_ID = "PositronicBrain_TW",
                Name = "Positronic Brain",
                Flavour = "\"How may I serve?\"",
                Description = "At the start of combat, construct a temporary robot ally to assist in battle.",
                IsShopItem = false,
                ShopPrice = 8,
                DoesPopUpInfo = true,
                StartsLocked = true,
                Icon = ResourceLoader.LoadSprite("UnlockHeavenNaudiz4"),
                TriggerOn = TriggerCalls.OnCombatStart,
                Effects =
                [
                    Effects.GenerateEffect(MakeRobot, 1, Targeting.Slot_SelfSlot),
                ],
                OnUnlockUsesTHE = true,
            };

            string achievementID = "AApocrypha_Naudiz4_Divine_ACH";
            string unlockID = "AApocrypha_Naudiz4_Divine_Unlock";

            ItemUtils.AddItemToTreasureStatsCategoryAndGamePool(posibrain.item, new ItemModdedUnlockInfo(posibrain.Item_ID, ResourceLoader.LoadSprite("UnlockHeavenNaudiz4Locked", null, 32, null), achievementID));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, posibrain.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [posibrain.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetUnlock_HeavenFinalBoss();
            unlockCheck.AddUnlockData("Naudiz4_CH", unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements("Phasic Scanning Module", "Unlocked a new item.", ResourceLoader.LoadSprite("AchievementHeavenNaudiz4", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToInGameCategory(AchievementCategoryIDs.DivineTitleLabel);
        }
    }
}
