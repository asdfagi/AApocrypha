using System;
using System.Collections.Generic;
using System.Text;
using A_Apocrypha.CustomOther;
using BrutalAPI;
using BrutalAPI.Items;

namespace A_Apocrypha.Items
{
    public class BetrayerOfMeasures
    {
        public static void Add()
        {
            PerformEffectPassiveAbility betrayerMark = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
            betrayerMark.name = "BetrayerOfMeasuresMark_PA";
            betrayerMark._passiveName = "Marked (Betrayer of Measures)";
            betrayerMark.m_PassiveID = "BetrayerOfMeasuresMark";
            betrayerMark.passiveIcon = ResourceLoader.LoadSprite("IconSmouldering");
            betrayerMark._characterDescription = "This party member is marked as a target for the Betrayer of Measures.";
            betrayerMark._enemyDescription = "This enemy is marked as a target for the Betrayer of Measures.";
            betrayerMark._triggerOn = [];
            betrayerMark.conditions = [];
            betrayerMark.doesPassiveTriggerInformationPanel = false;
            betrayerMark.effects = [];

            SpecificUnitsByPassiveTargeting AllMarked = ScriptableObject.CreateInstance<SpecificUnitsByPassiveTargeting>();
            AllMarked._passive = betrayerMark;
            AllMarked.targetUnitAllySlots = true;
            AllMarked.slotOffsets = [0];

            BetrayerOfMeasuresDamageDistributionCondition DamageCondition = ScriptableObject.CreateInstance<BetrayerOfMeasuresDamageDistributionCondition>();
            DamageCondition._targeting = AllMarked;

            AddPassiveEffect thisIsGoodNewsMark = ScriptableObject.CreateInstance<AddPassiveEffect>();
            thisIsGoodNewsMark._passiveToAdd = betrayerMark;

            DoublePerformEffect_Item betrayer = new DoublePerformEffect_Item("BetrayerOfMeasures_ID", null, false)
            {
                Item_ID = "BetrayerOfMeasures_SW",
                Name = "Betrayer of Measures",
                Flavour = "\"Trained into defiance.\"",
                Description = "At the start of combat, mark the Opposing enemy." +
                "\nAll damage dealt by this party member is also dealt indirectly to the marked enemy.",
                IsShopItem = true,
                ShopPrice = 6,
                StartsLocked = true,
                DoesPopUpInfo = true,
                Icon = ResourceLoader.LoadSprite("UnlockDoulaAmbrose"),
                TriggerOn = TriggerCalls.OnCombatStart,
                Effects =
                [
                    Effects.GenerateEffect(thisIsGoodNewsMark, 1, Targeting.Slot_Front),
                ],
                SecondaryDoesPopUpInfo = false,
                SecondaryConsumeOnUse = false,
                SecondaryTriggerOn = [TriggerCalls.OnDidApplyDamage],
                SecondaryConditions = [DamageCondition],
                SecondaryEffects = [],
                OnUnlockUsesTHE = true,
            };

            string achievementID = "AApocrypha_Ambrose_Abstraction_ACH";
            string unlockID = "AApocrypha_Ambrose_Abstraction_Unlock";

            ItemUtils.AddItemToShopStatsCategoryAndGamePool(betrayer.item, new ItemModdedUnlockInfo(betrayer.Item_ID, ResourceLoader.LoadSprite("UnlockDoulaAmbroseLocked", null, 32, null), achievementID));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, betrayer.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [betrayer.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetOrCreateUnlock_CustomFinalBoss("DoulaBoss", ResourceLoader.LoadSprite("DoulaPearl", null, 32, null));
            unlockCheck.AddUnlockData("Ambrose_CH", unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements("Betrayer of Measures", "Unlocked a new item.", ResourceLoader.LoadSprite("AchievementDoulaAmbrose", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToCUSTOMCategory("AbstractionTitleLabel", "The Abstraction");
        }
    }
}
