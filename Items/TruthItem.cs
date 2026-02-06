using System;
using System.Collections.Generic;
using System.Text;
using BrutalAPI.Items;

namespace A_Apocrypha.Items
{
    public class TruthItem
    {
        public static void Add()
        {
            EnvironmentTools.PrepareCombatEnvPrefab("Assets/Apocrypha_Environments/Truth_CombatEnv.prefab", "TruthCombatEnv", AApocrypha.assetBundle);

            SetScreenFadeEffect BlackoutOn = ScriptableObject.CreateInstance<SetScreenFadeEffect>();
            BlackoutOn._fadeToBlack = true;

            SetScreenFadeEffect BlackoutOff = ScriptableObject.CreateInstance<SetScreenFadeEffect>();
            BlackoutOff._fadeToBlack = false;

            PlayOneShotEffect CutOutSFX = ScriptableObject.CreateInstance<PlayOneShotEffect>();
            CutOutSFX.soundPath = "event:/AASFX/CutOut_SFX";

            PlayOneShotEffect CutInSFX = ScriptableObject.CreateInstance<PlayOneShotEffect>();
            CutInSFX.soundPath = "event:/AASFX/CutIn_SFX";

            PerformEffectViaSubaction BlackoutOffDelay = ScriptableObject.CreateInstance<PerformEffectViaSubaction>();
            BlackoutOffDelay.effects = [
                Effects.GenerateEffect(CutInSFX, 0),
                Effects.GenerateEffect(BlackoutOff, 0),
            ];

            PerformEffectViaSubaction BlackOutOffDelaySwap = ScriptableObject.CreateInstance<PerformEffectViaSubaction>();
            BlackOutOffDelaySwap.effects =
            [
                Effects.GenerateEffect(ScriptableObject.CreateInstance<ActivateTRUTHCombatEnvEffect>()),
                Effects.GenerateEffect(ScriptableObject.CreateInstance<ActivateTRUTHMusicEffect>()),
                Effects.GenerateEffect(BlackoutOffDelay, 0),
            ];

            AnimationVisualsEffect DelayAnim = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            DelayAnim._visuals = CustomVisuals.Nothing;
            DelayAnim._animationTarget = Targeting.Slot_SelfSlot;

            PreviousEffectCondition PreviousTrue = ScriptableObject.CreateInstance<PreviousEffectCondition>();
            PreviousTrue.wasSuccessful = true;

            FieldEffect_Apply_Effect ApplyShield = ScriptableObject.CreateInstance<FieldEffect_Apply_Effect>();
            ApplyShield._Field = StatusField.Shield;

            SpawnEnemyAnywhereEffect TestGuy = ScriptableObject.CreateInstance<SpawnEnemyAnywhereEffect>();
            TestGuy.enemy = LoadedAssetsHandler.GetEnemy("Truth_Immovable_EN");
            TestGuy.givesExperience = false;

            SpawnEnemyAnywhereEffect TestGuy2 = ScriptableObject.CreateInstance<SpawnEnemyAnywhereEffect>();
            TestGuy2.enemy = LoadedAssetsHandler.GetEnemy("Truth_Eye_EN");
            TestGuy2.givesExperience = false;

            SpawnEnemyAnywhereEffect TestGuy22 = ScriptableObject.CreateInstance<SpawnEnemyAnywhereEffect>();
            TestGuy22.enemy = LoadedAssetsHandler.GetEnemy("Truth_Pendulum_EN");
            TestGuy22.givesExperience = false;

            ExtraLootEffect Treasure = ScriptableObject.CreateInstance<ExtraLootEffect>();
            Treasure._isTreasure = true;
            Treasure._getLocked = false;

            ExtraCurrencyEffect Money = ScriptableObject.CreateInstance<ExtraCurrencyEffect>();
            Money._isMultiplier = false;
            Money._usePreviousExitValue = false;

            PerformEffectViaSubaction HandlerAction3 = ScriptableObject.CreateInstance<PerformEffectViaSubaction>();
            HandlerAction3.effects =
            [
                Effects.GenerateEffect(DelayAnim),
                Effects.GenerateEffect(Treasure, 2),
                Effects.GenerateEffect(Money, 10),
                Effects.GenerateEffect(BlackOutOffDelaySwap),
            ];

            PerformEffectViaSubaction HandlerAction2 = ScriptableObject.CreateInstance<PerformEffectViaSubaction>();
            HandlerAction2.effects =
            [
                Effects.GenerateEffect(TestGuy, 1),
                Effects.GenerateEffect(TestGuy2, 1),
                Effects.GenerateEffect(TestGuy22, 1),
                Effects.GenerateEffect(HandlerAction3),
            ];

            PerformEffectViaSubaction HandlerAction1 = ScriptableObject.CreateInstance<PerformEffectViaSubaction>();
            HandlerAction1.effects =
            [
                Effects.GenerateEffect(ScriptableObject.CreateInstance<FleeTargetEffect>(), 0, Targeting.Unit_AllOpponents),
                Effects.GenerateEffect(ScriptableObject.CreateInstance<ConsumeItemEffect>(), 1, Targeting.Slot_SelfSlot),
                Effects.GenerateEffect(HandlerAction2),
            ];

            CheckBundleDifficultyEffectorCondition NotBoss = ScriptableObject.CreateInstance<CheckBundleDifficultyEffectorCondition>();
            NotBoss._isEqual = false;

            PerformEffect_Item truth = new PerformEffect_Item("TRUTH_ID", null, false)
            {
                Item_ID = "TRUTH_TW",
                Name = "<color=#" + ColorUtility.ToHtmlStringRGB(Color.magenta) + ">TRUTH</color>",
                Flavour = "\"audi alteram partem.\"",
                Description = "At the beginning of combat, replace the current encounter. Victory will reward you with two treasure items and 10 Coins." +
                "\nThis item is destroyed upon activation. This item has no effect on bosses.",
                IsShopItem = false,
                DoesPopUpInfo = true,
                StartsLocked = true,
                Icon = ResourceLoader.LoadSprite("UnlockBlueSkyAnnaMolly"),
                TriggerOn = TriggerCalls.OnCombatStart,
                Conditions = [NotBoss],
                Effects =
                [
                    Effects.GenerateEffect(BlackoutOn, 0),
                    Effects.GenerateEffect(CutOutSFX, 0),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<StopMusicEffect>()),
                    Effects.GenerateEffect(DelayAnim),
                    Effects.GenerateEffect(HandlerAction1, 0)
                ],
            };

            ItemUtils.AddItemToTreasureStatsCategoryAndGamePool(truth.item, new ItemModdedUnlockInfo("TRUTH_TW", ResourceLoader.LoadSprite("UnlockBlueSkyAnnaMollyLocked", null, 32, null), "AApocrypha_AnnaMolly_Dreamer_ACH"));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement("AApocrypha_AnnaMolly_Dreamer_ACH", "TRUTH_TW");

            UnlockableModData thresholdBlueSkyUnlockData = new UnlockableModData("AApocrypha_AnnaMolly_Dreamer_Unlock")
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = "AApocrypha_AnnaMolly_Dreamer_ACH",
                hasItemUnlock = true,
                items = ["TRUTH_TW"],
            };

            FinalBossCharUnlockCheck UnlockDreamerThreshold = Unlocks.GetOrCreateUnlock_CustomFinalBoss("BlueSky_BOSS", ResourceLoader.LoadSprite("BlueSkyPearl", null, 32, null));
            UnlockDreamerThreshold.AddUnlockData("ThresholdFool_CH", thresholdBlueSkyUnlockData);

            ModdedAchievements thresholdblueskyachievement = new ModdedAchievements("<color=#" + ColorUtility.ToHtmlStringRGB(Color.magenta) + ">TRUTH</color>", "Unlocked a new item.", ResourceLoader.LoadSprite("AchievementBlueSkyAnnaMolly", null, 32, null), "AApocrypha_AnnaMolly_Dreamer_ACH");
            thresholdblueskyachievement.AddNewAchievementToCUSTOMCategory("BlueSky_BOSS", "The Dreamer");
        }
    }
}
