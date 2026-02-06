using System;
using System.Collections.Generic;
using System.Text;
using BrutalAPI.Items;

namespace A_Apocrypha.Items
{
    public class RipEnemyPack
    {
        public static void Add()
        {
            PassivePopUpOnTargetEffect EnemyPackSpotted = ScriptableObject.CreateInstance<PassivePopUpOnTargetEffect>();
            EnemyPackSpotted._sprite = "IconEnemyPack";
            EnemyPackSpotted._isUnitCharacter = false;
            EnemyPackSpotted._name = "Enemy Pack spotted!";

            RandomTargetPerformEffectViaSubaction Handler1 = ScriptableObject.CreateInstance<RandomTargetPerformEffectViaSubaction>();
            Handler1.effects = [Effects.GenerateEffect(EnemyPackSpotted, 1, Targeting.Slot_SelfSlot)];

            DamagePercentActiveModsModAndSecondaryEffect_Item enemypack = new DamagePercentActiveModsModAndSecondaryEffect_Item("BrokenCode_ID", 5, true, false, true)
            {
                Item_ID = "BrokenCode_TW",
                Name = "Broken Code",
                Flavour = "\"I CAN FINALLY DIE\"",
                Description = "All damage dealt by this party member is increased by 5% for each Brutal Orchestra mod that is currently enabled.",
                IsShopItem = false,
                ShopPrice = 10,
                DoesPopUpInfo = true,
                StartsLocked = true,
                Icon = ResourceLoader.LoadSprite("UnlockDoulaAnnaMolly"),
                TriggerOn = TriggerCalls.OnWillApplyDamage,
                SecondaryTriggerOn = [TriggerCalls.OnCombatStart],
                SecondaryDoesPopUpInfo = false,
                SecondaryEffects =
                [
                    Effects.GenerateEffect(EnemyPackSpotted, 1, Targeting.Slot_Front),
                ],
            };

            string achievementID = "AApocrypha_AnnaMolly_Abstraction_ACH";
            string unlockID = "AApocrypha_AnnaMolly_Abstraction_Unlock";

            ItemUtils.AddItemToTreasureStatsCategoryAndGamePool(enemypack.item, new ItemModdedUnlockInfo(enemypack.Item_ID, ResourceLoader.LoadSprite("UnlockDoulaAnnaMollyLocked", null, 32, null), achievementID));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, enemypack.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [enemypack.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetOrCreateUnlock_CustomFinalBoss("DoulaBoss", ResourceLoader.LoadSprite("DoulaPearl", null, 32, null));
            unlockCheck.AddUnlockData("ThresholdFool_CH", unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements("Broken Code", "Unlocked a new item.", ResourceLoader.LoadSprite("AchievementDoulaAnnaMolly", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToCUSTOMCategory("AbstractionTitleLabel", "The Abstraction");
        }
    }
}
