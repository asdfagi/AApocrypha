using System;
using System.Collections.Generic;
using System.Text;
using BrutalAPI.Items;

namespace A_Apocrypha.Items
{
    public class BoneMarketManifest
    {
        public static void Add()
        {
            CheckBundleDifficultyEffectorCondition NotBoss = ScriptableObject.CreateInstance<CheckBundleDifficultyEffectorCondition>();
            NotBoss._isEqual = false;

            PerformEffect_Item implausible = new PerformEffect_Item("BoneMarketManifest_ID", null, false)
            {
                Item_ID = "BoneMarketManifest_SW",
                Name = "Bone Market Manifest",
                Flavour = "\"Four-legged? Three-winged? Seven-necked??\"",
                Description = "At the start of combat, swap the abilities of the Left and Right enemies. This item has no effect in boss encounters.",
                IsShopItem = true,
                ShopPrice = 6,
                DoesPopUpInfo = true,
                StartsLocked = true,
                Icon = ResourceLoader.LoadSprite("UnlockNobodyAmbrose"),
                TriggerOn = TriggerCalls.OnCombatStart,
                Conditions = [NotBoss],
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<SwapAbilitiesTwoTargetsEffect>(), 1, Targeting.Slot_OpponentSides),
                ],
                OnUnlockUsesTHE = true,
            };

            string achievementID = "AApocrypha_Ambrose_Forgotten_ACH";
            string unlockID = "AApocrypha_Ambrose_Forgotten_Unlock";

            ItemUtils.AddItemToShopStatsCategoryAndGamePool(implausible.item, new ItemModdedUnlockInfo(implausible.Item_ID, ResourceLoader.LoadSprite("UnlockNobodyAmbroseLocked", null, 32, null), achievementID));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, implausible.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [implausible.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetOrCreateUnlock_CustomFinalBoss("Nobody_BOSS", ResourceLoader.LoadSprite("NobodyPearl", null, 32, null));
            unlockCheck.AddUnlockData("Ambrose_CH", unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements("Bone Market Manifest", "Unlocked a new item.", ResourceLoader.LoadSprite("AchievementNobodyAmbrose", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToCUSTOMCategory("ForgottenTitleLabel", "The Forgotten");
        }
    }
}
