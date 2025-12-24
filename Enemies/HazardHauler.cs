using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Enemies
{
    public class HazardHauler
    {
        public static void Add()
        {
            GenerateColorManaEffect GiveBluePigment = ScriptableObject.CreateInstance<GenerateColorManaEffect>();
            GiveBluePigment.mana = Pigments.Blue;

            SwapToOneRandomSideXTimesEffect SwapRandomFar = ScriptableObject.CreateInstance<SwapToOneRandomSideXTimesEffect>();

            SwapToOneSideEffect SwapLeft = ScriptableObject.CreateInstance<SwapToOneSideEffect>();
            SwapLeft._swapRight = false;

            SwapToOneSideEffect SwapRight = ScriptableObject.CreateInstance<SwapToOneSideEffect>();
            SwapRight._swapRight = true;

            StatusEffect_Apply_Effect IrradiatedApply = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            IrradiatedApply._Status = StatusField.GetCustomStatusEffect("Irradiated_ID");

            StatusEffect_Apply_Effect PetrifiedApply = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            PetrifiedApply._Status = StatusField.GetCustomStatusEffect("Petrified_ID");

            StatusEffect_Apply_Effect OilApply = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            OilApply._Status = StatusField.OilSlicked;

            ChangeMaxHealthEffect ReduceMaxHealth = ScriptableObject.CreateInstance<ChangeMaxHealthEffect>();
            ReduceMaxHealth._increase = false;

            AnimationVisualsEffect MicrowaveSidesAlly = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            MicrowaveSidesAlly._visuals = CustomVisuals.MicrowaveVisualsSO;
            MicrowaveSidesAlly._animationTarget = Targeting.Slot_AllySides;

            AnimationVisualsEffect MicrowaveSidesOpponent = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            MicrowaveSidesOpponent._visuals = CustomVisuals.MicrowaveVisualsSO;
            MicrowaveSidesOpponent._animationTarget = Targeting.Slot_OpponentSides;

            ConsumeRandomButCasterHealthManaEffect ConsumeNotHealth = ScriptableObject.CreateInstance<ConsumeRandomButCasterHealthManaEffect>();

            Ability perimeter = new Ability("Perimeter", "AApocrypha_Perimeter_A")
            {
                Description = "Apply 1 Irradiated to the Left and Right party members and enemies and move them away from this enemy.\nConsume 1 Pigment not of this enemy's health color.",
                Cost = [Pigments.Grey],
                Visuals = CustomVisuals.MicrowaveVisualsSO,
                AnimationTarget = Targeting.Slot_OpponentSides,
                Effects =
                    [
                        Effects.GenerateEffect(IrradiatedApply, 1, Targeting.Slot_OpponentSides),
                        Effects.GenerateEffect(SwapLeft, 1, Targeting.Slot_OpponentLeft),
                        Effects.GenerateEffect(SwapRight, 1, Targeting.Slot_OpponentRight),
                        Effects.GenerateEffect(MicrowaveSidesAlly),
                        Effects.GenerateEffect(IrradiatedApply, 1, Targeting.Slot_AllySides),
                        Effects.GenerateEffect(SwapLeft, 1, Targeting.Slot_AllyLeft),
                        Effects.GenerateEffect(SwapRight, 1, Targeting.Slot_AllyRight),
                        Effects.GenerateEffect(ConsumeNotHealth, 1, Targeting.Slot_SelfSlot),
                    ],
                Rarity = Rarity.Common,
                Priority = Priority.Fast,
            };
            perimeter.AddIntentsToTarget(Targeting.Slot_OpponentSides, ["Status_Irradiated"]);
            perimeter.AddIntentsToTarget(Targeting.Slot_AllySides, ["Status_Irradiated"]);
            perimeter.AddIntentsToTarget(Targeting.Slot_OpponentLeft, [nameof(IntentType_GameIDs.Swap_Left)]);
            perimeter.AddIntentsToTarget(Targeting.Slot_AllyLeft, [nameof(IntentType_GameIDs.Swap_Left)]);
            perimeter.AddIntentsToTarget(Targeting.Slot_OpponentRight, [nameof(IntentType_GameIDs.Swap_Right)]);
            perimeter.AddIntentsToTarget(Targeting.Slot_AllyRight, [nameof(IntentType_GameIDs.Swap_Right)]);
            perimeter.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Mana_Consume)]);

            Ability protocol = new Ability("Protocol", "AApocrypha_Protocol_A")
            {
                Description = "Reduce the Opposing party member's maximum health by 4 and apply 2 Irradiated to them.\nMove this enemy to the Left twice or to the Right twice.\nConsume 1 Pigment not of this enemy's health color.",
                Cost = [Pigments.Grey],
                Visuals = CustomVisuals.MicrowaveVisualsSO,
                AnimationTarget = Targeting.Slot_Front,
                Effects =
                [
                    Effects.GenerateEffect(ReduceMaxHealth, 4, Targeting.Slot_Front),
                    Effects.GenerateEffect(IrradiatedApply, 2, Targeting.Slot_Front),
                    Effects.GenerateEffect(SwapRandomFar, 2, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(ConsumeNotHealth, 1, Targeting.Slot_SelfSlot),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.Fast,
            };
            protocol.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Other_MaxHealth), "Status_Irradiated"]);
            protocol.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Swap_Left), nameof(IntentType_GameIDs.Swap_Right), nameof(IntentType_GameIDs.Mana_Consume)]);

            if (AApocrypha.CrossMod.Siren)
            {
                Enemy hazardhaulersiren = new Enemy("Hazard Hauler", "HazardHauler_Siren_EN")
                {
                    Health = 20,
                    HealthColor = Pigments.Grey,
                    Size = 1,
                    CombatSprite = ResourceLoader.LoadSprite("HazardHaulerTimeline", new Vector2(0.5f, 0f), 32),
                    OverworldDeadSprite = ResourceLoader.LoadSprite("HazardHaulerDead", new Vector2(0.5f, 0f), 32),
                    OverworldAliveSprite = ResourceLoader.LoadSprite("HazardHaulerTimeline", new Vector2(0.5f, 0f), 32),
                    DamageSound = "event:/AAEnemy/SandSifterHurt",
                    DeathSound = "event:/AAEnemy/SandSifterDeath",
                    UnitTypes = ["Robot"],
                };
                hazardhaulersiren.PrepareEnemyPrefab("Assets/Apocrypha_Enemies/HazardHauler_Enemy/HazardHauler_Enemy.prefab", AApocrypha.assetBundle, AApocrypha.assetBundle.LoadAsset<GameObject>("Assets/Apocrypha_Enemies/HazardHauler_Enemy/HazardHauler_Giblets.prefab").GetComponent<ParticleSystem>());

                Ability wastedisposalsiren = new Ability("Waste Disposal", "AApocrypha_WasteDisposal_Siren_A")
                {
                    Description = "Apply 2 Oil-Slicked and 2 Petrified to the Opposing party member.\nProduce 3 Blue Pigment.\nMove this enemy to the Left twice or to the Right twice.",
                    Cost = [Pigments.Grey, Pigments.Blue],
                    Visuals = Visuals.OilSlicked,
                    AnimationTarget = Targeting.Slot_Front,
                    Effects =
                    [
                        Effects.GenerateEffect(OilApply, 2, Targeting.Slot_Front),
                        Effects.GenerateEffect(PetrifiedApply, 2, Targeting.Slot_Front),
                        Effects.GenerateEffect(GiveBluePigment, 3, Targeting.Slot_SelfSlot),
                        Effects.GenerateEffect(SwapRandomFar, 2, Targeting.Slot_SelfSlot),
                    ],
                    Rarity = Rarity.Common,
                    Priority = Priority.Slow,
                };
                wastedisposalsiren.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Status_OilSlicked), "Status_Petrified"]);
                wastedisposalsiren.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Mana_Generate), nameof(IntentType_GameIDs.Swap_Left), nameof(IntentType_GameIDs.Swap_Right)]);

                hazardhaulersiren.AddEnemyAbilities(
                [
                    perimeter.GenerateEnemyAbility(true),
                    protocol.GenerateEnemyAbility(true),
                    wastedisposalsiren.GenerateEnemyAbility(true),
                ]);

                hazardhaulersiren.AddPassives([Passives.GetCustomPassive("BlueBlooded_1_PA"), Passives.GetCustomPassive("AA_CondenseBlue_PA")]);
                hazardhaulersiren.AddEnemy(true, true, false);
            }
        }
    }
}
