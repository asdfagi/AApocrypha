using System;
using System.Collections.Generic;
using System.Text;
using BrutalAPI;
using BrutalAPI.Items;
using UnityEngine.SocialPlatforms.Impl;
using Utility.SerializableCollection;

namespace A_Apocrypha.Items
{
    public class LangtonAnt
    {
        public static void Add()
        {
            StoredValueParityCheckEffect LangtonEven = ScriptableObject.CreateInstance<StoredValueParityCheckEffect>();
            LangtonEven.m_unitStoredDataID = "LangtonAntStoredValue";

            CasterStoreValueSetterEffect InitLangtonValue = ScriptableObject.CreateInstance<CasterStoreValueSetterEffect>();
            InitLangtonValue.m_unitStoredDataID = "LangtonAntStoredValue";
            InitLangtonValue._ignoreIfContains = true;

            CasterStoredValueChangeEffect LangtonAdd = ScriptableObject.CreateInstance<CasterStoredValueChangeEffect>();
            LangtonAdd.m_unitStoredDataID = "LangtonAntStoredValue";
            LangtonAdd._increase = true;

            PerformEffectViaSubaction LangtonAddSub = ScriptableObject.CreateInstance<PerformEffectViaSubaction>();
            LangtonAddSub.effects = [Effects.GenerateEffect(LangtonAdd, 1, Targeting.Slot_SelfSlot)];

            SwapToOneSideEffect SwapLeft = ScriptableObject.CreateInstance<SwapToOneSideEffect>();
            SwapLeft._swapRight = false;

            SwapToOneSideEffect SwapRight = ScriptableObject.CreateInstance<SwapToOneSideEffect>();
            SwapRight._swapRight = true;

            CasterInOneEdgeCheckEffect CheckLeft = ScriptableObject.CreateInstance<CasterInOneEdgeCheckEffect>();
            CheckLeft._right = false;

            CasterInOneEdgeCheckEffect CheckRight = ScriptableObject.CreateInstance<CasterInOneEdgeCheckEffect>();
            CheckRight._right = true;

            PerformEffectViaSubaction LeftRepeatMover = ScriptableObject.CreateInstance<PerformEffectViaSubaction>();
            LeftRepeatMover.effects = [
                Effects.GenerateEffect(SwapLeft, 1, Targeting.Slot_SelfSlot),
                Effects.GenerateEffect(CheckLeft, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 1)),
                Effects.GenerateEffect(ScriptableObject.CreateInstance<MirrorPositionEffect>(), 1, Targeting.Slot_SelfSlot, Effects.CheckMultiplePreviousEffectsCondition([true, false], [1, 2])),
            ];

            PerformEffectViaSubaction RightRepeatMover = ScriptableObject.CreateInstance<PerformEffectViaSubaction>();
            RightRepeatMover.effects = [
                Effects.GenerateEffect(SwapRight, 1, Targeting.Slot_SelfSlot),
                Effects.GenerateEffect(CheckRight, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 1)),
                Effects.GenerateEffect(ScriptableObject.CreateInstance<MirrorPositionEffect>(), 1, Targeting.Slot_SelfSlot, Effects.CheckMultiplePreviousEffectsCondition([true, false], [1, 2])),
            ];

            PerformEffectXTimesStoredValueViaSubaction LeftRepeat = ScriptableObject.CreateInstance<PerformEffectXTimesStoredValueViaSubaction>();
            LeftRepeat.effects = [
                Effects.GenerateEffect(LeftRepeatMover),
            ];
            LeftRepeat.m_unitStoredDataID = "LangtonAntStoredValue";
            LeftRepeat.usePreviousExit = false;

            PerformEffectXTimesStoredValueViaSubaction RightRepeat = ScriptableObject.CreateInstance<PerformEffectXTimesStoredValueViaSubaction>();
            RightRepeat.effects = [
                Effects.GenerateEffect(RightRepeatMover),
            ];
            RightRepeat.m_unitStoredDataID = "LangtonAntStoredValue";
            RightRepeat.usePreviousExit = false;

            DoublePerformEffect_Item ant = new DoublePerformEffect_Item("LangtonAnt_ID", null, false)
            {
                Item_ID = "LangtonAnt_TW",
                Name = "Langton's Ant",
                Flavour = "\"→↓←↑←↑→↓←↓\"",
                Description = "On moving (or being moved), deal 1 indirect damage to the Opposing enemy." +
                "\nBefore performing an ability, move Left once, assuming the grid wraps around. Every time this effect is triggered, switch the movement direction and increase the amount of times moved by 1.",
                IsShopItem = false,
                ShopPrice = 10,
                DoesPopUpInfo = true,
                StartsLocked = true,
                Icon = ResourceLoader.LoadSprite("UnlockMinibossTuring"),
                TriggerOn = TriggerCalls.OnMoved,
                Effects = [Effects.GenerateEffect(BasicEffects.Indirect, 1, Targeting.Slot_Front)],
                SecondaryTriggerOn = [TriggerCalls.OnAbilityWillBeUsed],
                SecondaryDoesPopUpInfo = true,
                SecondaryEffects =
                [
                    Effects.GenerateEffect(InitLangtonValue, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(LangtonEven, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(LeftRepeat, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 1)),
                    Effects.GenerateEffect(RightRepeat, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 2)),
                    Effects.GenerateEffect(LangtonAddSub, 1, Targeting.Slot_SelfSlot),
                ],
                EquippedModifiers = [],
            };

            ItemUtils.AddItemToTreasureStatsCategoryAndGamePool(ant.item, new ItemModdedUnlockInfo(ant.Item_ID, ResourceLoader.LoadSprite("UnlockMinibossTuringLocked", null, 32, null), "AApocrypha_Miniboss_TarnishedDivinity_ACH"));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement("AApocrypha_Miniboss_TuringTarpit_ACH", ant.Item_ID);

            UnlockableModData unlockData = new UnlockableModData("MinibossTuringTarpit")
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = "AApocrypha_Miniboss_TuringTarpit_ACH",
                hasItemUnlock = true,
                items = [ant.Item_ID],
            };

            EnemyDeathUnlockCheck deathUnlock = ScriptableObject.CreateInstance<EnemyDeathUnlockCheck>();
            deathUnlock.usesSimpleDeathData = true;
            deathUnlock.enemyID = "TuringTarpit_EN";
            deathUnlock.simpleDeathData = unlockData;
            deathUnlock.specialDeathData = new SerializableDictionary<string, UnlockableModData>();

            ModdedAchievements achievement = new ModdedAchievements("Exceptional", "Decompile the Turing Tarpit.", ResourceLoader.LoadSprite("AchievementMinibossTuring", null, 32, null), "AApocrypha_Miniboss_TuringTarpit_ACH");
            achievement.AddNewAchievementToInGameCategory(AchievementCategoryIDs.ComediesTitleLabel);

            Unlocks.AddUnlock_EnemyDeath(deathUnlock);
        }
    }
}
