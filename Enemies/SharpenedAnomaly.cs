using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Enemies
{
    public class SharpenedAnomaly
    {
        public static void Add()
        {
            Enemy sharpenedanomaly = new Enemy("Sharpened Anomaly", "SharpenedAnomaly_EN")
            {
                Health = 20,
                HealthColor = Pigments.Purple,
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("SharpenedAnomalyTimeline", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("SharpenedAnomalyDead", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("SharpenedAnomalyTimeline", new Vector2(0.5f, 0f), 32),
                DamageSound = LoadedAssetsHandler.GetEnemy("TaMaGoa_EN").damageSound,
                DeathSound = LoadedAssetsHandler.GetEnemy("TaMaGoa_EN").damageSound,
                UnitTypes = ["AnomalyID"],
            };
            sharpenedanomaly.PrepareEnemyPrefab("Assets/Apocrypha_Enemies/PiercingAnomaly_Enemy/PiercingAnomaly_Enemy.prefab", AApocrypha.assetBundle, AApocrypha.assetBundle.LoadAsset<GameObject>("Assets/Apocrypha_Enemies/Anomaly_Enemy/AnomalyShell_Giblets.prefab").GetComponent<ParticleSystem>());
            sharpenedanomaly.AddPassives([Passives.Pure, Passives.GetCustomPassive("Confrontational_PA"), Passives.DecayGenerator(LoadedAssetsHandler.GetEnemy("UnboundAnomaly_EN"), 60)]);

            GenerateColorManaEffect GivePurplePigment = ScriptableObject.CreateInstance<GenerateColorManaEffect>();
            GivePurplePigment.mana = Pigments.Purple;

            ConsumeColorManaEffect ConsumePurplePigment = ScriptableObject.CreateInstance<ConsumeColorManaEffect>();
            ConsumePurplePigment.mana = Pigments.Purple;

            FieldEffect_Apply_Effect ShieldByPrevious = ScriptableObject.CreateInstance<FieldEffect_Apply_Effect>();
            ShieldByPrevious._Field = StatusField.Shield;
            ShieldByPrevious._UsePreviousExitValueAsMultiplier = true;

            RemoveFieldEffectEffect RemoveShield = ScriptableObject.CreateInstance<RemoveFieldEffectEffect>();
            RemoveShield._field = StatusField.Shield;

            MassSwapZoneEffect Shuffle = ScriptableObject.CreateInstance<MassSwapZoneEffect>();

            PreviousEffectCondition PreviousTrue = ScriptableObject.CreateInstance<PreviousEffectCondition>();
            PreviousTrue.wasSuccessful = true;

            PreviousEffectCondition PreviousFalse = ScriptableObject.CreateInstance<PreviousEffectCondition>();
            PreviousFalse.wasSuccessful = false;

            AnimationVisualsEffect StrikeLeftAnim = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            StrikeLeftAnim._animationTarget = Targeting.Slot_OpponentLeft;
            StrikeLeftAnim._visuals = Visuals.Intrusion;

            AnimationVisualsEffect StrikeRightAnim = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            StrikeRightAnim._animationTarget = Targeting.Slot_OpponentRight;
            StrikeRightAnim._visuals = Visuals.Extrusion;

            SwapToOneSideEffect SwapLeft = ScriptableObject.CreateInstance<SwapToOneSideEffect>();
            SwapLeft._swapRight = false;

            SwapToOneSideEffect SwapRight = ScriptableObject.CreateInstance<SwapToOneSideEffect>();
            SwapRight._swapRight = true;

            Ability frontstrike = new Ability("Central Strike", "AApocrypha_CentralStrike_A")
            {
                Description = "Remove all Shield from the Opposing slot, then deal an Agonizing amount of damage to the Opposing party member.",
                Cost = [],
                Visuals = Visuals.Decimate,
                AnimationTarget = Targeting.Slot_Front,
                Effects = [
                    Effects.GenerateEffect(RemoveShield, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 8, Targeting.Slot_Front),
                ],
                Rarity = Rarity.Uncommon,
                Priority = Priority.Normal,
            };
            frontstrike.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Rem_Field_Shield)]);
            frontstrike.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Damage_7_10)]);

            Ability leftstrike = new Ability("Sinistral Strike", "AApocrypha_SinistralStrike_A")
            {
                Description = "If there is a party member Opposing this enemy, move Right.\nDeal a Painful amount of damage to the Left party member.",
                Cost = [],
                Effects = [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<CheckHasUnitEffect>(), 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(SwapRight, 1, Targeting.Slot_SelfSlot, PreviousTrue),
                    Effects.GenerateEffect(StrikeLeftAnim),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 6, Targeting.Slot_OpponentLeft),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.Normal,
            };
            leftstrike.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Misc_Hidden)]);
            leftstrike.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Swap_Right)]);
            leftstrike.AddIntentsToTarget(Targeting.Slot_OpponentLeft, [nameof(IntentType_GameIDs.Damage_3_6)]);

            Ability rightstrike = new Ability("Dextral Strike", "AApocrypha_DextralStrike_A")
            {
                Description = "If there is a party member Opposing this enemy, move Left.\nDeal a Painful amount of damage to the Right party member.",
                Cost = [],
                Effects = [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<CheckHasUnitEffect>(), 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(SwapLeft, 1, Targeting.Slot_SelfSlot, PreviousTrue),
                    Effects.GenerateEffect(StrikeRightAnim),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 6, Targeting.Slot_OpponentRight),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.Normal,
            };
            rightstrike.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Misc_Hidden)]);
            rightstrike.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Swap_Left)]);
            rightstrike.AddIntentsToTarget(Targeting.Slot_OpponentRight, [nameof(IntentType_GameIDs.Damage_3_6)]);

            sharpenedanomaly.AddEnemyAbilities(
                [
                    leftstrike,
                    rightstrike,
                    frontstrike,
                    UnboundAnomaly.anomalywhispers,
                ]);
            sharpenedanomaly.AddEnemy(true, true, true);
        }
    }
}
