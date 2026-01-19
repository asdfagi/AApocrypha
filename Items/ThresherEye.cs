using System;
using System.Collections.Generic;
using System.Text;
using A_Apocrypha.CustomOther;
using BrutalAPI.Items;

namespace A_Apocrypha.Items
{
    public class ThresherEye
    {
        public static void Add()
        {
            CasterStoreValueSetterAdvancedEffect SetTargeter = ScriptableObject.CreateInstance<CasterStoreValueSetterAdvancedEffect>();
            SetTargeter.m_unitStoredDataID = "TargeterStoredValue";
            SetTargeter._ignoreIfContains = false;
            SetTargeter.usePreviousExitValue = true;

            OpponentByStoredValueTargeting TargeterTargeting = ScriptableObject.CreateInstance<OpponentByStoredValueTargeting>();
            TargeterTargeting._storedValueID = "TargeterStoredValue";
            TargeterTargeting.targetUnitAllySlots = false;
            TargeterTargeting.reduceByOne = true;

            PassivePopUpOnTargetEffect TargeterPopup = ScriptableObject.CreateInstance<PassivePopUpOnTargetEffect>();
            TargeterPopup._name = "Targeter";
            TargeterPopup._sprite = "IconTargeter";
            TargeterPopup._isUnitCharacter = true;

            AnimationVisualsEffect TargetAnim = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            TargetAnim._visuals = Visuals.Poke;
            TargetAnim._animationTarget = TargeterTargeting;

            AnimationVisualsEffect BoomAnim = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            BoomAnim._visuals = ITAVisuals.Explode;
            BoomAnim._animationTarget = TargeterTargeting;

            PerformEffectPassiveAbility thresherEyeTargeterPassive = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
            thresherEyeTargeterPassive.name = "AA_TargeterThresherEye_PA";
            thresherEyeTargeterPassive._passiveName = "Targeter";
            thresherEyeTargeterPassive.m_PassiveID = "Targeter";
            thresherEyeTargeterPassive.passiveIcon = ResourceLoader.LoadSprite("IconTargeter");
            thresherEyeTargeterPassive._characterDescription = "At the end of each turn, this party member will remember the position of the enemy with the highest current health.";
            thresherEyeTargeterPassive._enemyDescription = "hey, no, this one is for party members. no. bad. nuh uh. no.";
            thresherEyeTargeterPassive._triggerOn = [TriggerCalls.OnTurnFinished];
            thresherEyeTargeterPassive.doesPassiveTriggerInformationPanel = true;
            thresherEyeTargeterPassive.effects =
            [
                Effects.GenerateEffect(ScriptableObject.CreateInstance<TargetGetSlotEffect>(), 1, Targeting.GenerateUnitTarget_Specific_Health(false, false, false, false)),
                Effects.GenerateEffect(SetTargeter, 1, Targeting.Slot_SelfSlot),
                Effects.GenerateEffect(TargetAnim),
            ];
            Passives.AddCustomPassiveToPool("AA_TargeterThresherEye_PA", "Targeter", thresherEyeTargeterPassive);

            ExtraPassiveAbility_Wearable_SMS wearablePassiveTargeter = ScriptableObject.CreateInstance<ExtraPassiveAbility_Wearable_SMS>();
            wearablePassiveTargeter._extraPassiveAbility = Passives.GetCustomPassive("AA_TargeterThresherEye_PA");

            PerformEffect_Item mortar = new PerformEffect_Item("ThresherEye_TW", null, false)
            {
                Item_ID = "ThresherEye_TW",
                Name = "Thresher's Eye",
                Flavour = "\"TARGET ACQUIRED.\"",
                Description = "This party member now has Targeter as a passive, marking the position of one of the highest health enemies at the end of your turn. At the start of your turn, deal 7 damage to this party member's Targeted position.",
                IsShopItem = false,
                ShopPrice = 8,
                DoesPopUpInfo = true,
                StartsLocked = true,
                Icon = ResourceLoader.LoadSprite("LootDuneThresher"),
                TriggerOn = TriggerCalls.OnTurnStart,
                Effects =
                [
                    Effects.GenerateEffect(BoomAnim),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 7, TargeterTargeting),
                ],
                EquippedModifiers = [wearablePassiveTargeter],
            };

            ItemUtils.AddItemToTreasureStatsCategoryAndGamePool(mortar.item, new ItemModdedUnlockInfo("ThresherEye_TW", ResourceLoader.LoadSprite("LootDuneThresherLocked", null, 32, null), "AApocrypha_Loot_DuneThresher_ACH"));

            /*BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement("AApocrypha_Whitlock_Dreamer_ACH", "FourthCityAirag_SW");

            UnlockableModData whitlockDoulaUnlockData = new UnlockableModData("AApocrypha_Whitlock_Dreamer_Unlock")
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = "AApocrypha_Whitlock_Dreamer_ACH",
                hasItemUnlock = true,
                items = ["FourthCityAirag_SW"],
            };

            FinalBossCharUnlockCheck UnlockAbstractionWhitlock = Unlocks.GetOrCreateUnlock_CustomFinalBoss("BlueSky_BOSS", ResourceLoader.LoadSprite("BlueSkyPearl", null, 32, null));
            UnlockAbstractionWhitlock.AddUnlockData("Whitlock_CH", whitlockDoulaUnlockData);

            ModdedAchievements whitlockdoulaachievement = new ModdedAchievements("Fourth City Airag", "Unlocked a new item.", ResourceLoader.LoadSprite("AchievementBlueSkyWhitlock", null, 32, null), "AApocrypha_Whitlock_Dreamer_ACH");
            whitlockdoulaachievement.AddNewAchievementToCUSTOMCategory("BlueSky_BOSS", "The Dreamer");*/
        }
    }
}
