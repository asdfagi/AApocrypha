using System;
using System.Collections.Generic;
using System.Text;
using A_Apocrypha.Encounters;
using BrutalAPI.Items;

namespace A_Apocrypha.Items
{
    public class GnomeHat
    {
        public static void Add()
        {
            string[] colors = ["Red", "Green", "Blue", "Purple"];
            int colorIndex = UnityEngine.Random.Range(0, colors.Length);
            string color = colors[colorIndex];

            ExtraPassiveAbility_Wearable_SMS wearablePassiveGnome = ScriptableObject.CreateInstance<ExtraPassiveAbility_Wearable_SMS>();
            wearablePassiveGnome._extraPassiveAbility = Passives.GetCustomPassive("Gnome_PA");

            SpawnEnemyAnywhereEffect GnomeSpawn = ScriptableObject.CreateInstance<SpawnEnemyAnywhereEffect>();
            GnomeSpawn.enemy = LoadedAssetsHandler.GetEnemy("MachineGnomes_Friendly_EN");
            GnomeSpawn._spawnTypeID = CombatType_GameIDs.Spawn_Basic.ToString();
            GnomeSpawn.givesExperience = false;

            PerformEffect_Item gnomehat = new PerformEffect_Item("GnomeHat_ID", null, false)
            {
                Item_ID = "GnomeHat_TW",
                Name = "Gnome Hat",
                Flavour = "\"Can you see us?\"",
                Description = "This party member is an honorary gnome." +
                "\nOn combat start, summons a horde of Machine Gnomes on the enemy side. They will do their best to help you.",
                IsShopItem = false,
                ShopPrice = 0,
                DoesPopUpInfo = true,
                StartsLocked = true,
                Icon = ResourceLoader.LoadSprite("UnlockComediesGnomeDeath" + color),
                TriggerOn = TriggerCalls.OnBeforeCombatStart,
                Effects =
                [
                    Effects.GenerateEffect(GnomeSpawn, 1, Targeting.Slot_Front),
                ],
                EquippedModifiers = [wearablePassiveGnome],
                OnUnlockUsesTHE = true,
            };

            string achievementID = "AApocrypha_Comedy_GnomeDeath_ACH";
            string unlockID = "ComedyGnomeDeath";

            ItemUtils.AddItemToTreasureStatsCategoryAndGamePool(gnomehat.item, new ItemModdedUnlockInfo(gnomehat.Item_ID, ResourceLoader.LoadSprite("UnlockComediesGnomeDeathLocked", null, 32, null), achievementID));
            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, gnomehat.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [gnomehat.Item_ID],
            };

            ModdedAchievements unlockAchievement = new ModdedAchievements("Dance Party", "Have 5 Gnomes \"die\" in the same run.", ResourceLoader.LoadSprite("AchievementComediesGnomeDeath" + color, null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToInGameCategory(AchievementCategoryIDs.ComediesTitleLabel);

            Unlocks.AddUnlock_ByID(unlockData);
        }
    }
}
