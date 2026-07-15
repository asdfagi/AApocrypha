using System;
using System.Collections.Generic;
using System.Text;
using A_Apocrypha.CustomOther;
using BrutalAPI.Items;

namespace A_Apocrypha.Items
{
    public class AgeOfDestruction
    {
        public static void Add()
        {
            SpawnEnemyInSlotFromEntryEffect WAR = ScriptableObject.CreateInstance<SpawnEnemyInSlotFromEntryEffect>();
            WAR.enemy = LoadedAssetsHandler.GetEnemy("AgeOfDestruction_Obliterator_EN");
            WAR.trySpawnAnywhereIfFail = true;
            WAR.givesExperience = false;
            WAR._spawnTypeID = CombatType_GameIDs.Spawn_Basic.ToString();

            PerformEffect_Item deathrobot = new PerformEffect_Item("AgeOfDestruction_ID", null, false)
            {
                Item_ID = "AgeOfDestruction_TW",
                Name = "The Age of Destruction",
                Flavour = "\"...but we must not let it fade into oblivion.\"",
                Description = "At the start of combat, summon a vestige of the Age of Destruction that irradiates your party members and incinerates your opponents.",
                IsShopItem = false,
                ShopPrice = 9,
                DoesPopUpInfo = true,
                StartsLocked = true,
                Icon = ResourceLoader.LoadSprite("UnlockDeathmatchNaudiz4"),
                TriggerOn = TriggerCalls.OnCombatStart,
                Conditions = [],
                Effects = [Effects.GenerateEffect(WAR, 1, Targeting.Slot_Front)],
                OnUnlockUsesTHE = false,
            };

            string achievementID = "AApocrypha_Naudiz4_Antagonist_ACH";
            string unlockID = "AApocrypha_Naudiz4_Antagonist_Unlock";

            ItemUtils.AddItemToTreasureStatsCategoryAndGamePool(deathrobot.item, new ItemModdedUnlockInfo(deathrobot.Item_ID, ResourceLoader.LoadSprite("UnlockDeathmatchNaudiz4Locked", null, 32, null), achievementID));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, deathrobot.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [deathrobot.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetOrCreateUnlock_CustomFinalBoss("Deathmatch_BOSS", ResourceLoader.LoadSprite("DeathmatchPearl", null, 32, null));
            unlockCheck.AddUnlockData("Naudiz4_CH", unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements("The Age of Destruction", "Unlocked a new item.", ResourceLoader.LoadSprite("AchievementDeathmatchNaudiz4", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToCUSTOMCategory("AntagonistTitleLabel", "The Antagonist");

            LoadedAssetsHandler.GetCharacter("Naudiz4_CH").m_BossAchData.Add(new CharFinalBossAchData("Deathmatch_BOSS", achievementID));
        }
    }
}
