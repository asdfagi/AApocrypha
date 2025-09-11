using System;
using System.Collections.Generic;
using System.Text;
using A_Apocrypha.Animations;
using UnityEngine.VFX;

namespace A_Apocrypha.Enemies
{
    public class Asterism
    {
        public static void Add()
        {

            CasterStoreValueSetterAdvancedEffect SavePreviousToPosition = ScriptableObject.CreateInstance<CasterStoreValueSetterAdvancedEffect>();
            SavePreviousToPosition.usePreviousExitValue = true;
            SavePreviousToPosition.m_unitStoredDataID = "AA_Asterism_Pos";
            
            Enemy asterism = new Enemy("Asterism", "Asterism_EN")
            {
                Health = 12,
                HealthColor = Pigments.RedPurple,
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("AsterismTimeline", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("AsterismDead", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("AsterismTimeline", new Vector2(0.5f, 0f), 32),
                DamageSound = "event:/Combat/StatusEffects/SE_Divine_Trg",
                DeathSound = LoadedAssetsHandler.GetEnemy("TaMaGoa_EN").damageSound,
            };
            asterism.PrepareEnemyPrefab("Assets/Apocrypha_Enemies/Asterism_Enemy/Asterism_Enemy.prefab", AApocrypha.assetBundle, AApocrypha.assetBundle.LoadAsset<GameObject>("Assets/Apocrypha_Enemies/Acolyte_Enemy/Acolyte_Giblets.prefab").GetComponent<ParticleSystem>());
            asterism.AddPassives([Passives.Formless]);

            PreviousEffectCondition PreviousTrue = ScriptableObject.CreateInstance<PreviousEffectCondition>();
            PreviousTrue.wasSuccessful = true;

            PreviousEffectCondition PreviousFalse = ScriptableObject.CreateInstance<PreviousEffectCondition>();
            PreviousFalse.wasSuccessful = false;

            PreviousEffectCondition Previous2False = ScriptableObject.CreateInstance<PreviousEffectCondition>();
            Previous2False.wasSuccessful = false;
            Previous2False.previousAmount = 2;

            SwapToOneSideEffect SwapLeft = ScriptableObject.CreateInstance<SwapToOneSideEffect>();
            SwapLeft._swapRight = false;

            SwapToOneSideEffect SwapRight = ScriptableObject.CreateInstance<SwapToOneSideEffect>();
            SwapRight._swapRight = true;

            FieldEffect_Apply_Effect ApplyShield = ScriptableObject.CreateInstance<FieldEffect_Apply_Effect>();
            ApplyShield._Field = StatusField.Shield;

            AnimationVisualsEffect ApsisAnim = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            ApsisAnim._visuals = CustomVisuals.StarfallVisualsSO;
            ApsisAnim._animationTarget = Targeting.Slot_Front;

            AnimationVisualsEffect AppulseAnim = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            AppulseAnim._visuals = Visuals.Crush;
            AppulseAnim._animationTarget = Targeting.Slot_OpponentSides;

            CasterInSlotEffect SlotChecker = ScriptableObject.CreateInstance<CasterInSlotEffect>();
            SlotChecker.usePreviousExitValue = false;

            ExtraVariableForNextEffect Blank = ScriptableObject.CreateInstance<ExtraVariableForNextEffect>();

            Ability apoapsis = new Ability("Apoapsis", "AApocrypha_Apoapsis_A")
            {
                Description = "Move this enemy as far to the Right as possible, then deal a Painful amount of damage to the Opposing party member. If this enemy did not move, the damage is instead Agonizing.",
                Cost = [Pigments.Red, Pigments.RedPurple],
                Effects =
                [
                    Effects.GenerateEffect(SwapRight, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(SwapRight, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(SwapRight, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(SwapRight, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(ApsisAnim, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(Blank, 1, Targeting.Slot_SelfSlot, Effects.CheckMultiplePreviousEffectsCondition([false, false, false, false], [2, 3, 4, 5])),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 8, Targeting.Slot_Front, PreviousTrue),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 4, Targeting.Slot_Front, Previous2False),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.Normal,
            };
            apoapsis.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Swap_Right)]);
            apoapsis.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Swap_Right)]);
            apoapsis.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Swap_Right)]);
            apoapsis.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Swap_Right)]);
            apoapsis.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Damage_3_6)]);
            apoapsis.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Damage_7_10)]);

            Ability periapsis = new Ability("Periapsis", "AApocrypha_Periapsis_A")
            {
                Description = "Move this enemy as far to the Left as possible, then deal a Painful amount of damage to the Opposing party member. If this enemy did not move, the damage is instead Agonizing.",
                Cost = [Pigments.PurpleRed, Pigments.Red],
                Effects =
                [
                    Effects.GenerateEffect(SwapLeft, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(SwapLeft, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(SwapLeft, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(SwapLeft, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(ApsisAnim, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(Blank, 1, Targeting.Slot_SelfSlot, Effects.CheckMultiplePreviousEffectsCondition([false, false, false, false], [2, 3, 4, 5])),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 8, Targeting.Slot_Front, PreviousTrue),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 4, Targeting.Slot_Front, Previous2False),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.Normal,
            };
            periapsis.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Swap_Left)]);
            periapsis.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Swap_Left)]);
            periapsis.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Swap_Left)]);
            periapsis.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Swap_Left)]);
            periapsis.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Damage_3_6)]);
            periapsis.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Damage_7_10)]);

            Ability appulse = new Ability("Appulse", "AApocrypha_Appulse_A")
            {
                Description = "Move the Far Left and Far Right party members closer to this enemy, then deal a Painful amount of damage to the Left and Right party members.",
                Cost = [Pigments.Red, Pigments.RedPurple],
                Effects =
                [
                    Effects.GenerateEffect(SwapRight, 1, Targeting.GenerateSlotTarget([-2], false)),
                    Effects.GenerateEffect(SwapLeft, 1, Targeting.GenerateSlotTarget([2], false)),
                    Effects.GenerateEffect(AppulseAnim, 1, Targeting.Slot_OpponentSides),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 4, Targeting.Slot_OpponentSides),
                ],
                Rarity = Rarity.Uncommon,
                Priority = Priority.Normal,
            };
            appulse.AddIntentsToTarget(Targeting.GenerateSlotTarget([-2], false), [nameof(IntentType_GameIDs.Swap_Right)]);
            appulse.AddIntentsToTarget(Targeting.GenerateSlotTarget([2], false), [nameof(IntentType_GameIDs.Swap_Left)]);
            appulse.AddIntentsToTarget(Targeting.Slot_OpponentSides, [nameof(IntentType_GameIDs.Damage_3_6)]);

            Ability scintillation = new Ability("Scintillation", "AApocrypha_Scintillation_A")
            {
                Description = "Move this enemy to the Center position. Apply 3 Shield to this enemy's position and the Left and Right enemy positions.",
                Cost = [Pigments.PurpleRed, Pigments.RedPurple],
                Visuals = CustomVisuals.GazeVisualsSO,
                AnimationTarget = Targeting.Slot_SelfSlot,
                Effects =
                [
                    Effects.GenerateEffect(SlotChecker, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(SwapRight, 1, Targeting.Slot_SelfSlot, PreviousTrue),
                    Effects.GenerateEffect(SlotChecker, 2, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(SwapRight, 1, Targeting.Slot_SelfSlot, PreviousTrue),
                    Effects.GenerateEffect(SlotChecker, 5, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(SwapLeft, 1, Targeting.Slot_SelfSlot, PreviousTrue),
                    Effects.GenerateEffect(SlotChecker, 4, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(SwapLeft, 1, Targeting.Slot_SelfSlot, PreviousTrue),
                    Effects.GenerateEffect(ApplyShield, 3, Targeting.Slot_SelfAndSides),
                ],
                Rarity = Rarity.Uncommon,
                Priority = Priority.Normal,
            };
            scintillation.AddIntentsToTarget(Targeting.GenerateGenericTarget([0, 1], true), [nameof(IntentType_GameIDs.Swap_Right)]);
            scintillation.AddIntentsToTarget(Targeting.GenerateGenericTarget([3, 4], true), [nameof(IntentType_GameIDs.Swap_Left)]);
            scintillation.AddIntentsToTarget(Targeting.Slot_SelfAndSides, [nameof(IntentType_GameIDs.Field_Shield)]);

            asterism.AddEnemyAbilities(
                [
                    apoapsis,
                    periapsis,
                    appulse,
                    scintillation,
                ]);
            asterism.AddEnemy(true, true, true);
        }
    }
}
