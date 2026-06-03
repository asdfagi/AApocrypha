using System;
using System.Collections.Generic;
using System.Text;
using A_Apocrypha.CustomOther;

namespace A_Apocrypha.Enemies
{
    public class CobaltCurator
    {
        public static void Add()
        {
            GenerateColorManaEffect GiveRedPigment = ScriptableObject.CreateInstance<GenerateColorManaEffect>();
            GiveRedPigment.mana = Pigments.Red;

            GenerateColorManaEffect GiveBluePigment = ScriptableObject.CreateInstance<GenerateColorManaEffect>();
            GiveBluePigment.mana = Pigments.Blue;

            PerformEffectPassiveAbility redBlueBlooded = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
            redBlueBlooded.name = "RedAndBlueBlooded_2_PA";
            redBlueBlooded._passiveName = "Red- & Blue-Blooded (2)";
            redBlueBlooded.m_PassiveID = "PigmentBlooded";
            redBlueBlooded.passiveIcon = ResourceLoader.LoadSprite("IconStonebloodRedAndBlue");
            redBlueBlooded._characterDescription = "Upon receiving direct damage this party member produces 1 additional Red pigment & 1 additional Blue pigment.";
            redBlueBlooded._enemyDescription = "Upon receiving direct damage this enemy produces 1 additional Red pigment & 1 additional Blue pigment.";
            redBlueBlooded._triggerOn = [TriggerCalls.OnDirectDamaged];
            redBlueBlooded.doesPassiveTriggerInformationPanel = true;
            redBlueBlooded.effects =
            [
                Effects.GenerateEffect(GiveRedPigment, 1, Targeting.Slot_SelfSlot),
                Effects.GenerateEffect(GiveBluePigment, 1, Targeting.Slot_SelfSlot),
            ];

            CasterAnalysisStoreValueSetterEffect SetAnalysis = ScriptableObject.CreateInstance<CasterAnalysisStoreValueSetterEffect>();
            SetAnalysis.m_unitStoredDataID = "NaudizCurrentStoredValue";

            CasterAnalysisStoredValueCheckerEffect AnalysisNotNull = ScriptableObject.CreateInstance<CasterAnalysisStoredValueCheckerEffect>();
            AnalysisNotNull.m_unitStoredDataID = "NaudizCurrentStoredValue";

            CasterAnalysisIsUnitAliveCheckerEffect AnalysisAlive = ScriptableObject.CreateInstance<CasterAnalysisIsUnitAliveCheckerEffect>();
            AnalysisAlive.m_unitStoredDataID = "NaudizCurrentStoredValue";

            OpponentByAnalysisStoredValueTargeting AnalysisTarget = ScriptableObject.CreateInstance<OpponentByAnalysisStoredValueTargeting>();
            AnalysisTarget.targetUnitAllySlots = false;
            AnalysisTarget.getAllUnitSelfSlots = false;
            AnalysisTarget._storedValueID = "NaudizCurrentStoredValue";

            AnimationVisualsEffect AnalysisAnim = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            AnalysisAnim._visuals = Visuals.Providence;
            AnalysisAnim._animationTarget = AnalysisTarget;

            CasterStoreValueSetterAdvancedStringEffect WipeAnalysisTarget = ScriptableObject.CreateInstance<CasterStoreValueSetterAdvancedStringEffect>();
            WipeAnalysisTarget._stringData = "None";
            WipeAnalysisTarget.m_unitStoredDataID = "NaudizCurrentStoredValue";

            PerformDoubleEffectPassiveAbility cobaltAnalyzer = ScriptableObject.CreateInstance<PerformDoubleEffectPassiveAbility>();
            cobaltAnalyzer.name = "AnalyzerCurator_PA";
            cobaltAnalyzer._passiveName = "Analyzer";
            cobaltAnalyzer.m_PassiveID = "Analyzer";
            cobaltAnalyzer.passiveIcon = ResourceLoader.LoadSprite("IconAnalyzer");
            cobaltAnalyzer._characterDescription = "cobalt is a vital material for any projects that involve militarizing mercury";
            cobaltAnalyzer._enemyDescription = "At the start of the player's turn, mark a party member for analysis if none are marked. This selection is mostly random. Only one party member can be marked for analysis at a time.";
            cobaltAnalyzer._triggerOn = [TriggerCalls.OnCombatStart, TriggerCalls.OnRoundFinished];
            cobaltAnalyzer.doesPassiveTriggerInformationPanel = true;
            cobaltAnalyzer.effects =
            [
                Effects.GenerateEffect(AnalysisNotNull),
                Effects.GenerateEffect(SetAnalysis, 1, Targeting.Unit_AllOpponents, Effects.CheckPreviousEffectCondition(false, 1)),
                Effects.GenerateEffect(AnalysisAnim),
            ];
            cobaltAnalyzer._secondTriggerOn = [TriggerCalls.OnOpponentHasDied];
            cobaltAnalyzer._secondDoesPerformPopUp = true;
            cobaltAnalyzer._secondEffects =
            [
                Effects.GenerateEffect(AnalysisAlive, 1, Targeting.Unit_AllOpponents),
                Effects.GenerateEffect(AnalysisNotNull),
                Effects.GenerateEffect(WipeAnalysisTarget, -5, Targeting.Slot_SelfSlot, Effects.CheckMultiplePreviousEffectsCondition([true, false], [1, 2])),
            ];
            cobaltAnalyzer.specialStoredData = UnitStoreData.GetCustom_UnitStoreData("NaudizCurrentStoredValue");

            Enemy cobaltcurator = new Enemy("Cobalt Curator", "CobaltCurator_EN")
            {
                Health = 28,
                HealthColor = Pigments.Grey,
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("CobaltCuratorTimeline", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("CobaltCuratorDead", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("CobaltCuratorTimeline", new Vector2(0.5f, 0f), 32),
                DamageSound = "event:/AAEnemy/SandSifterHurt",
                DeathSound = "event:/AAEnemy/SandSifterDeath",
                UnitTypes = ["Robot"],
            };
            cobaltcurator.PrepareEnemyPrefab("Assets/Apocrypha_Enemies/CobaltCurator_Enemy/CobaltCurator_Enemy.prefab", AApocrypha.assetBundle, AApocrypha.assetBundle.LoadAsset<GameObject>("Assets/Apocrypha_Enemies/CobaltCurator_Enemy/CobaltCurator_Giblets.prefab").GetComponent<ParticleSystem>());
            cobaltcurator.AddPassives([cobaltAnalyzer, redBlueBlooded]);

            AttackVisualsSO AnalysisVisuals = ITAVisuals.Divide;

            AnimationVisualsEffect SampleAnim = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            SampleAnim._visuals = AnalysisVisuals;
            SampleAnim._animationTarget = Targeting.Slot_Front;

            SwapToOneSideEffect swapLeft = ScriptableObject.CreateInstance<SwapToOneSideEffect>();
            swapLeft._swapRight = false;

            SwapToOneSideEffect swapRight = ScriptableObject.CreateInstance<SwapToOneSideEffect>();
            swapRight._swapRight = true;

            StatusEffect_Apply_Effect AddScars = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            AddScars._Status = StatusField.Scars;

            Ability specimen = new Ability("Prepare Specimen", "AApocrypha_CuratorSpecimen_A")
            {
                Description = "Apply 2 Scars to the party member marked for analysis and produce 2 pigment of their health color." +
                "\nIf no party member is marked for analysis, mark the highest-health party member.",
                Cost = [Pigments.BlueRed],
                Visuals = Visuals.UglyOnTheInside,
                AnimationTarget = AnalysisTarget,
                Effects =
                [
                    Effects.GenerateEffect(AnalysisNotNull),
                    Effects.GenerateEffect(AddScars, 2, AnalysisTarget, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<GenerateHealthColorManaPerTargetEffect>(), 2, AnalysisTarget, Effects.CheckPreviousEffectCondition(true, 2)),
                    Effects.GenerateEffect(SetAnalysis, 1, Targeting.GenerateUnitTarget_Specific_Health(false, false, false, false), Effects.CheckPreviousEffectCondition(false, 3)),
                    Effects.GenerateEffect(AnalysisAnim, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 4)),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.Fast,
            };
            specimen.AddIntentsToTarget(AnalysisTarget, [nameof(IntentType_GameIDs.Status_Scars), nameof(IntentType_GameIDs.Mana_Generate)]);
            specimen.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Misc_Hidden)]);
            specimen.AddIntentsToTarget(Targeting.GenerateUnitTarget_Specific_Health(false, false, false, false), [nameof(IntentType_GameIDs.Misc)]);

            Ability sampleleft = new Ability("Search and Sample", "AApocrypha_CuratorSampleL_A")
            {
                Description = "Move Left three times or until Opposing a party member marked for analysis." +
                "\nDeal a Painful amount of damage to the Opposing party member.",
                Cost = [Pigments.BlueRed],
                Visuals = CustomVisuals.GazeVisualsSO,
                AnimationTarget = Targeting.Slot_SelfSlot,
                Effects =
                [
                    Effects.GenerateEffect(AnalysisAlive, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(swapLeft, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 1)),
                    Effects.GenerateEffect(AnalysisAlive, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(swapLeft, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 1)),
                    Effects.GenerateEffect(AnalysisAlive, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(swapLeft, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 1)),
                    Effects.GenerateEffect(SampleAnim),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 6, Targeting.Slot_Front),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.Normal,
            };
            sampleleft.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Swap_Left), "AA_Multi3"]);
            sampleleft.AddIntentsToTarget(AnalysisTarget, [nameof(IntentType_GameIDs.Misc_Hidden)]);
            sampleleft.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Damage_3_6)]);

            Ability sampleright = new Ability("Seek and Sample", "AApocrypha_CuratorSampleR_A")
            {
                Description = "Move Right three times or until Opposing a party member marked for analysis." +
                "\nDeal a Painful amount of damage to the Opposing party member.",
                Cost = [Pigments.BlueRed],
                Visuals = CustomVisuals.GazeVisualsSO,
                AnimationTarget = Targeting.Slot_SelfSlot,
                Effects =
                [
                    Effects.GenerateEffect(AnalysisAlive, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(swapRight, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 1)),
                    Effects.GenerateEffect(AnalysisAlive, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(swapRight, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 1)),
                    Effects.GenerateEffect(AnalysisAlive, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(swapRight, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 1)),
                    Effects.GenerateEffect(SampleAnim),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 6, Targeting.Slot_Front),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.Normal,
            };
            sampleright.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Swap_Right), "AA_Multi3"]);
            sampleright.AddIntentsToTarget(AnalysisTarget, [nameof(IntentType_GameIDs.Misc_Hidden)]);
            sampleright.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Damage_3_6)]);

            cobaltcurator.AddEnemyAbilities(
            [
                specimen.GenerateEnemyAbility(true),
                sampleleft.GenerateEnemyAbility(true),
                sampleright.GenerateEnemyAbility(true),
            ]);

            cobaltcurator.AddEnemy(true, true, false);

            //LoadedAssetsHandler.GetEnemy("CobaltCurator_EN").enemyTemplate = LoadedAssetsHandler.GetEnemy("SandSifter_EN").enemyTemplate;
        }
    }
}
