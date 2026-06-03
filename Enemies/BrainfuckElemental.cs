using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Enemies
{
    public class BrainfuckElemental
    {
        public static void Add()
        {
            if (Abyss.Exists && LoadedDBsHandler.StatusFieldDB._StatusEffects.ContainsKey("Malfunction_ID"))
            {
                Enemy brain = new Enemy("--[----->+<]>----.++++.", "BFElemental_EN")
                {
                    Health = 32,
                    HealthColor = Pigments.Grey,
                    Size = 1,
                    CombatSprite = ResourceLoader.LoadSprite("BFElementalTimeline", new Vector2(0.5f, 0f), 32),
                    OverworldDeadSprite = ResourceLoader.LoadSprite("AnomalyDead", new Vector2(0.5f, 0f), 32),
                    OverworldAliveSprite = ResourceLoader.LoadSprite("BFElementalOverworld", new Vector2(0.5f, 0f), 32),
                    DamageSound = "event:/AAEnemy/Anomaly1Hurt",
                    DeathSound = "event:/AAEnemy/Anomaly1Death",
                    UnitTypes = ["TuringComplete"],
                };
                brain.PrepareEnemyPrefab("Assets/Apocrypha_Enemies/BFElemental_Enemy/BFElemental_Enemy.prefab", AApocrypha.assetBundle, AApocrypha.assetBundle.LoadAsset<GameObject>("Assets/Apocrypha_Enemies/BFElemental_Enemy/BFElemental_Giblets.prefab").GetComponent<ParticleSystem>());
                if (LoadedAssetsHandler.GetEnemy("Omission_EN") != null)
                {
                    brain.DamageSound = LoadedAssetsHandler.GetEnemy("Omission_EN").damageSound;
                    brain.DeathSound = LoadedAssetsHandler.GetEnemy("Omission_EN").deathSound;
                }

                SwapToOneSideEffect SwapLeft = ScriptableObject.CreateInstance<SwapToOneSideEffect>();
                SwapLeft._swapRight = false;

                SwapToOneSideEffect SwapRight = ScriptableObject.CreateInstance<SwapToOneSideEffect>();
                SwapRight._swapRight = true;

                CasterInOneEdgeCheckEffect CheckLeft = ScriptableObject.CreateInstance<CasterInOneEdgeCheckEffect>();
                CheckLeft._right = false;

                CasterInOneEdgeCheckEffect CheckRight = ScriptableObject.CreateInstance<CasterInOneEdgeCheckEffect>();
                CheckRight._right = true;

                PerformEffectXTimesViaSubaction LeftRepeat = ScriptableObject.CreateInstance<PerformEffectXTimesViaSubaction>();
                LeftRepeat.effects = [
                    Effects.GenerateEffect(SwapLeft, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(CheckLeft, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 1)),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<MirrorPositionEffect>(), 1, Targeting.Slot_SelfSlot, Effects.CheckMultiplePreviousEffectsCondition([true, false], [1, 2])),
                ];

                PerformEffectXTimesViaSubaction RightRepeat = ScriptableObject.CreateInstance<PerformEffectXTimesViaSubaction>();
                RightRepeat.effects = [
                    Effects.GenerateEffect(SwapRight, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(CheckRight, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 1)),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<MirrorPositionEffect>(), 1, Targeting.Slot_SelfSlot, Effects.CheckMultiplePreviousEffectsCondition([true, false], [1, 2])),
                ];

                PerformEffectXTimesViaSubaction LeftRepeatPrevious = ScriptableObject.CreateInstance<PerformEffectXTimesViaSubaction>();
                LeftRepeatPrevious.effects = [
                    Effects.GenerateEffect(SwapLeft, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(CheckLeft, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 1)),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<MirrorPositionEffect>(), 1, Targeting.Slot_SelfSlot, Effects.CheckMultiplePreviousEffectsCondition([true, false], [1, 2])),
                ];
                LeftRepeatPrevious.usePreviousExit = true;

                PerformEffectXTimesViaSubaction RightRepeatPrevious = ScriptableObject.CreateInstance<PerformEffectXTimesViaSubaction>();
                RightRepeatPrevious.effects = [
                    Effects.GenerateEffect(SwapRight, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(CheckRight, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 1)),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<MirrorPositionEffect>(), 1, Targeting.Slot_SelfSlot, Effects.CheckMultiplePreviousEffectsCondition([true, false], [1, 2])),
                ];
                RightRepeatPrevious.usePreviousExit = true;

                CasterStoredValueChangeWithMaxEffect ByteValueUp = ScriptableObject.CreateInstance<CasterStoredValueChangeWithMaxEffect>();
                ByteValueUp.m_unitStoredDataID = "ByteStoredValue";
                ByteValueUp._minimumValue = 0;
                ByteValueUp._maximumValue = 256;
                ByteValueUp._exitValueIsChange = false;
                ByteValueUp._increase = true;
                ByteValueUp._randomBetweenPrevious = false;
                ByteValueUp._usePreviousExitValue = false;

                CasterStoredValueSetEffect ByteValueSet = ScriptableObject.CreateInstance<CasterStoredValueSetEffect>();
                ByteValueSet._valueName = "ByteStoredValue";

                ExtraVariableForNext_SVEffect ByteGet = ScriptableObject.CreateInstance<ExtraVariableForNext_SVEffect>();
                ByteGet.m_unitStoredDataID = "ByteStoredValue";

                brain.CombatEnterEffects = [Effects.GenerateEffect(ByteValueSet, 0, Targeting.Slot_SelfSlot)];
                
                StatusEffect_ApplyByPrevious_Effect Malfunctionize = ScriptableObject.CreateInstance<StatusEffect_ApplyByPrevious_Effect>();
                Malfunctionize._Status = StatusField.GetCustomStatusEffect("Malfunction_ID");

                Ability plus4 = new Ability("++++", "AApocrypha_BFPlus4_A")
                {
                    Description = "Increase Byte by 4.",
                    Cost = [Pigments.Grey],
                    Effects =
                    [
                        Effects.GenerateEffect(ByteValueUp, 4, Targeting.Slot_SelfSlot),
                    ],
                    Rarity = Rarity.Common,
                    Priority = Priority.Normal,
                };
                plus4.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Misc_Additional), "AA_Multi4"]);

                Ability l2plus2 = new Ability("<<++", "AApocrypha_BFLeft2Plus2_A")
                {
                    Description = "Move Left twice, assuming the grid wraps around.\nIncrease Byte by 2.",
                    Cost = [Pigments.Grey],
                    Effects =
                    [
                        Effects.GenerateEffect(LeftRepeat, 2, Targeting.Slot_SelfSlot),
                        Effects.GenerateEffect(ByteValueUp, 2, Targeting.Slot_SelfSlot),
                    ],
                    Rarity = Rarity.Common,
                    Priority = Priority.Normal,
                };
                l2plus2.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Swap_Left), nameof(IntentType_GameIDs.Misc_Additional), "AA_Multi2"]);

                Ability r2plus2 = new Ability(">>++", "AApocrypha_BFRight2Plus2_A")
                {
                    Description = "Move Right twice, assuming the grid wraps around.\nIncrease Byte by 2.",
                    Cost = [Pigments.Grey],
                    Effects =
                    [
                        Effects.GenerateEffect(RightRepeat, 2, Targeting.Slot_SelfSlot),
                        Effects.GenerateEffect(ByteValueUp, 2, Targeting.Slot_SelfSlot),
                    ],
                    Rarity = Rarity.Common,
                    Priority = Priority.Normal,
                };
                r2plus2.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Swap_Right), nameof(IntentType_GameIDs.Misc_Additional), "AA_Multi2"]);

                Ability lspecial = new Ability("[-<]++++", "AApocrypha_BFLeftSpecial_A")
                {
                    Description = "Move Left an amount of times equal to Byte, assuming the grid wraps around, then set Byte to 4.",
                    Cost = [Pigments.Grey],
                    Effects =
                    [
                        Effects.GenerateEffect(ByteGet, 1, Targeting.Slot_SelfSlot),
                        Effects.GenerateEffect(LeftRepeatPrevious, 1, Targeting.Slot_SelfSlot),
                        Effects.GenerateEffect(ByteValueSet, 4, Targeting.Slot_SelfSlot),
                    ],
                    Rarity = Rarity.Rare,
                    Priority = Priority.Normal,
                };
                lspecial.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Swap_Left), "AA_MultiX", nameof(IntentType_GameIDs.Misc)]);

                Ability rspecial = new Ability("[->]++++", "AApocrypha_BFRightSpecial_A")
                {
                    Description = "Move Right an amount of times equal to Byte, assuming the grid wraps around, then set Byte to 4.",
                    Cost = [Pigments.Grey],
                    Effects =
                    [
                        Effects.GenerateEffect(ByteGet, 1, Targeting.Slot_SelfSlot),
                        Effects.GenerateEffect(RightRepeatPrevious, 1, Targeting.Slot_SelfSlot),
                        Effects.GenerateEffect(ByteValueSet, 4, Targeting.Slot_SelfSlot),
                    ],
                    Rarity = Rarity.Rare,
                    Priority = Priority.Normal,
                };
                rspecial.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Swap_Right), "AA_MultiX", nameof(IntentType_GameIDs.Misc)]);

                AttackVisualsSO GlitchVisuals = LoadedAssetsHandler.GetCharacterAbility("SamDefrag_A").visuals;

                Ability malfunctionatorinator = new Ability(".", "AApocrypha_BFBonusAttack_A")
                {
                    Description = "Apply Malfunction to the Opposing party member equal to the value of Byte.",
                    Cost = [Pigments.Grey],
                    Visuals = GlitchVisuals,
                    AnimationTarget = Targeting.Slot_Front,
                    Effects =
                    [
                        Effects.GenerateEffect(ByteGet, 1, Targeting.Slot_SelfSlot),
                        Effects.GenerateEffect(Malfunctionize, 1, Targeting.Slot_Front),
                    ],
                    Rarity = Rarity.Impossible,
                    Priority = Priority.Slow,
                };
                malfunctionatorinator.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Misc)]);
                malfunctionatorinator.AddIntentsToTarget(Targeting.Slot_Front, ["Status_Malfunction"]);

                ExtraAbilityInfo malfextra = new()
                {
                    ability = malfunctionatorinator.GenerateEnemyAbility().ability,
                    rarity = Rarity.Impossible,
                };

                brain.AddEnemyAbilities([
                    plus4.GenerateEnemyAbility(false),
                    l2plus2.GenerateEnemyAbility(false),
                    r2plus2.GenerateEnemyAbility(false),
                    lspecial.GenerateEnemyAbility(false),
                    rspecial.GenerateEnemyAbility(false),
                ]);

                brain.AddPassives([Passives.Abomination1, Passives.GetCustomPassive("WhiteBlooded_1_PA"), Passives.BonusAttackGenerator(malfextra)]);
                brain.AddEnemy(true, true, false);

                //LoadedAssetsHandler.GetEnemy("BFElemental_EN").enemyTemplate = LoadedAssetsHandler.GetEnemy("Mung_EN").enemyTemplate;
            }
        }
    }
}
