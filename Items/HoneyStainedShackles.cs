using System;
using System.Collections.Generic;
using System.Text;
using A_Apocrypha.CustomOther;
using BrutalAPI.Items;

namespace A_Apocrypha.Items
{
    public class HoneyStainedShackles
    {
        public static void Add()
        {
            SpawnEnemyInSlotFromEntryEffect HoneyDream = ScriptableObject.CreateInstance<SpawnEnemyInSlotFromEntryEffect>();
            HoneyDream.enemy = LoadedAssetsHandler.GetEnemy("CageGarden_Item_EN");
            HoneyDream.trySpawnAnywhereIfFail = true;
            HoneyDream.givesExperience = false;
            HoneyDream._spawnTypeID = CombatType_GameIDs.Spawn_Basic.ToString();

            PerformEffect_Item shackles = new PerformEffect_Item("HoneyStainedShackles_ID", null, false)
            {
                Item_ID = "HoneyStainedShackles_SW",
                Name = "Honey-Stained Shackles",
                Flavour = "\"Lost in the Chambers of the Heart.\"",
                Description = "At the start of combat, dream a Cage-Garden into being and destroy this item." +
                "\nDestroying the Cage-Garden before it vanishes or withers away will grant 14 Coins and a random Treasure item.",
                IsShopItem = true,
                ShopPrice = 7,
                DoesPopUpInfo = true,
                StartsLocked = true,
                ConsumeOnUse = true,
                Icon = ResourceLoader.LoadSprite("UnlockDeathmatchKneynsberg"),
                TriggerOn = TriggerCalls.OnCombatStart,
                Conditions = [],
                Effects = [Effects.GenerateEffect(HoneyDream, 1, Targeting.Slot_Front)],
                OnUnlockUsesTHE = true,
            };

            string achievementID = "AApocrypha_Kneynsberg_Antagonist_ACH";
            string unlockID = "AApocrypha_Kneynsberg_Antagonist_Unlock";

            ItemUtils.AddItemToShopStatsCategoryAndGamePool(shackles.item, new ItemModdedUnlockInfo(shackles.Item_ID, ResourceLoader.LoadSprite("UnlockDeathmatchKneynsbergLocked", null, 32, null), achievementID));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, shackles.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [shackles.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetOrCreateUnlock_CustomFinalBoss("Deathmatch_BOSS", ResourceLoader.LoadSprite("DeathmatchPearl", null, 32, null));
            unlockCheck.AddUnlockData("Kneynsberg_CH", unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements("Honey-Stained Shackles", "Unlocked a new item.", ResourceLoader.LoadSprite("AchievementDeathmatchKneynsberg", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToCUSTOMCategory("AntagonistTitleLabel", "The Antagonist");

            LoadedAssetsHandler.GetCharacter("Kneynsberg_CH").m_BossAchData.Add(new CharFinalBossAchData("Deathmatch_BOSS", achievementID));
        }
    }
}
