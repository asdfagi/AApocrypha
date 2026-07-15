using System;
using System.Collections.Generic;
using System.Text;
using BrutalAPI.Items;

namespace A_Apocrypha.Items
{
    public class Chthonosophy
    {
        public static void Add()
        {
            PerformEffect_Item deepthink = new PerformEffect_Item("Chthonosophy_ID", null, false)
            {
                Item_ID = "Chthonosophy_SW",
                Name = "Chthonosophy",
                Flavour = "\"The study of the root of things.\"",
                Description = "At the start of each turn, reset the passives, abilities and health color of this party member.",
                IsShopItem = false,
                ShopPrice = 77,
                DoesPopUpInfo = true,
                StartsLocked = true,
                ConsumeOnUse = false,
                Icon = ResourceLoader.LoadSprite("UnlockDeathmatchVaughan"),
                TriggerOn = TriggerCalls.OnTurnStart,
                Conditions = [],
                Effects = [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<ResetCasterAbilitiesToDefaultEffect>(), 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<ResetCasterChthonosophyEffect>(), 1, Targeting.Slot_SelfSlot),
                ],
            };

            string achievementID = "AApocrypha_Vaughan_Antagonist_ACH";
            string unlockID = "AApocrypha_Vaughan_Antagonist_Unlock";

            ItemUtils.AddItemToTreasureStatsCategoryAndGamePool(deepthink.item, new ItemModdedUnlockInfo(deepthink.Item_ID, ResourceLoader.LoadSprite("UnlockDeathmatchVaughanLocked", null, 32, null), achievementID));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, deepthink.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [deepthink.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetOrCreateUnlock_CustomFinalBoss("Deathmatch_BOSS", ResourceLoader.LoadSprite("DeathmatchPearl", null, 32, null));
            unlockCheck.AddUnlockData("Vaughan_CH", unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements("Chthonosophy", "Unlocked a new item.", ResourceLoader.LoadSprite("AchievementDeathmatchVaughan", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToCUSTOMCategory("AntagonistTitleLabel", "The Antagonist");

            LoadedAssetsHandler.GetCharacter("Vaughan_CH").m_BossAchData.Add(new CharFinalBossAchData("Deathmatch_BOSS", achievementID));
        }
    }
}
