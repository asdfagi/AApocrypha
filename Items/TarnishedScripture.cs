using System;
using System.Collections.Generic;
using System.Text;
using BrutalAPI;
using UnityEngine.SocialPlatforms.Impl;
using Utility.SerializableCollection;

namespace A_Apocrypha.Items
{
    public class TarnishedScripture
    {
        public static void Add()
        {
            ExtraPassiveAbility_Wearable_SMS wearableFragile = ScriptableObject.CreateInstance<ExtraPassiveAbility_Wearable_SMS>();
            wearableFragile._extraPassiveAbility = Passives.GetCustomPassive("Fragile_PA");

            HealthColorChange_Wearable_SMS wearableBroken = ScriptableObject.CreateInstance<HealthColorChange_Wearable_SMS>();
            wearableBroken._healthColor = LoadedDBsHandler.PigmentDB.GetPigment("Broken");

            GenerateColorManaEffect GiveBrokenPigment = ScriptableObject.CreateInstance<GenerateColorManaEffect>();
            GiveBrokenPigment.mana = LoadedDBsHandler.PigmentDB.GetPigment("Broken");

            RandomizeOneCostToColorEffect BreakCost = ScriptableObject.CreateInstance<RandomizeOneCostToColorEffect>();
            BreakCost._mana = LoadedDBsHandler.PigmentDB.GetPigment("Broken");

            DamagePercentModAndSecondaryEffect_Item scripture = new DamagePercentModAndSecondaryEffect_Item("TarnishedScripture_ID", 50, true, false, true)
            {
                Item_ID = "TarnishedScripture_TW",
                Name = "Tarnished Scripture",
                Flavour = "\"Words for nobody.\"",
                Description = "This party member now has broken health and Fragile as a passive and deals 50% more damage." +
                "\nAt the end of each turn, generate 2 Broken pigment and break one of this party member's pigment costs.",
                IsShopItem = false,
                ShopPrice = 10,
                DoesPopUpInfo = true,
                StartsLocked = true,
                Icon = ResourceLoader.LoadSprite("UnlockMinibossDogma"),
                TriggerOn = TriggerCalls.OnWillApplyDamage,
                SecondaryTriggerOn = [TriggerCalls.OnTurnFinished],
                SecondaryDoesPopUpInfo = true,
                SecondaryEffects =
                [
                    Effects.GenerateEffect(GiveBrokenPigment, 2, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(BreakCost, 1, Targeting.Slot_SelfSlot),
                ],
                EquippedModifiers = [wearableBroken, wearableFragile],
            };

            ItemUtils.AddItemToTreasureStatsCategoryAndGamePool(scripture.item, new ItemModdedUnlockInfo(scripture.Item_ID, ResourceLoader.LoadSprite("UnlockMinibossDogmaLocked", null, 32, null), "AApocrypha_Miniboss_TarnishedDivinity_ACH"));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement("AApocrypha_Miniboss_TarnishedDivinity_ACH", scripture.Item_ID);

            UnlockableModData unlockData = new UnlockableModData("MinibossTarnishedDivinity")
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = "AApocrypha_Miniboss_TarnishedDivinity_ACH",
                hasItemUnlock = true,
                items = [scripture.Item_ID],
            };

            EnemyDeathUnlockCheck deathUnlock = ScriptableObject.CreateInstance<EnemyDeathUnlockCheck>();
            deathUnlock.usesSimpleDeathData = true;
            deathUnlock.enemyID = "TarnishedDivinity_EN";
            deathUnlock.simpleDeathData = unlockData;
            deathUnlock.specialDeathData = new SerializableDictionary<string, UnlockableModData>();

            ModdedAchievements achievement = new ModdedAchievements("Deopsy", "Deny the Tarnished Divinity.", ResourceLoader.LoadSprite("AchievementMinibossTarnishedDivinity", null, 32, null), "AApocrypha_Miniboss_TarnishedDivinity_ACH");
            achievement.AddNewAchievementToInGameCategory(AchievementCategoryIDs.ComediesTitleLabel);

            Unlocks.AddUnlock_EnemyDeath(deathUnlock);
        }
    }
}
