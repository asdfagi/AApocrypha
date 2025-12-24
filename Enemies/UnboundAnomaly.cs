using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Enemies
{
    public class UnboundAnomaly
    {
        public static Ability anomalywhispers;
        public static Ability anomalytears;
        public static Ability anomalystare;
        public static void Add()
        {
            Enemy unboundanomaly = new Enemy("Unbound Anomaly", "UnboundAnomaly_EN")
            {
                Health = 16,
                HealthColor = Pigments.Purple,
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("AnomalyTimeline", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("AnomalyDead", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("AnomalyTimeline", new Vector2(0.5f, 0f), 32),
                DamageSound = "event:/AAEnemy/Anomaly1Hurt",
                DeathSound = "event:/AAEnemy/Anomaly1Death",
                UnitTypes = ["Anomaly"],
            };
            unboundanomaly.PrepareEnemyPrefab("Assets/Apocrypha_Enemies/Anomaly_Enemy/Anomaly_Enemy.prefab", AApocrypha.assetBundle, AApocrypha.assetBundle.LoadAsset<GameObject>("Assets/Apocrypha_Enemies/Anomaly_Enemy/Anomaly_Giblets.prefab").GetComponent<ParticleSystem>());

            DamageEffect IndirectDamage = ScriptableObject.CreateInstance<DamageEffect>();
            IndirectDamage._indirect = true;

            MassSwapZoneEffect Shuffle = ScriptableObject.CreateInstance<MassSwapZoneEffect>();

            StatusEffect_Apply_Effect ScarsApply = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            ScarsApply._Status = StatusField.Scars;

            GenerateColorManaEffect GivePurple = ScriptableObject.CreateInstance<GenerateColorManaEffect>();
            GivePurple.mana = Pigments.Purple;

            AnimationVisualsIfUnitEffect CrushSelfAnim = ScriptableObject.CreateInstance<AnimationVisualsIfUnitEffect>();
            CrushSelfAnim._visuals = Visuals.Crush;
            CrushSelfAnim._animationTarget = Targeting.Slot_SelfSlot;

            RandomDamageBetweenPreviousAndEntryEffect IndirectRandom = ScriptableObject.CreateInstance<RandomDamageBetweenPreviousAndEntryEffect>();
            IndirectRandom._indirect = true;

            AnomalyFreeMusicHandlerEffect MusicToggleOn = ScriptableObject.CreateInstance<AnomalyFreeMusicHandlerEffect>();
            MusicToggleOn.Add = true;

            AnomalyFreeMusicHandlerEffect MusicToggleOff = ScriptableObject.CreateInstance<AnomalyFreeMusicHandlerEffect>();
            MusicToggleOff.Add = false;

            unboundanomaly.CombatEnterEffects = [Effects.GenerateEffect(MusicToggleOn)];
            unboundanomaly.CombatExitEffects = [Effects.GenerateEffect(MusicToggleOff)];

            Ability stare = new Ability("Stare", "AApocrypha_Stare_A")
            {
                Description = "This enemy does nothing.",
                Cost = [Pigments.Purple, Pigments.Purple],
                Visuals = CustomVisuals.GazeVisualsSO,
                AnimationTarget = Targeting.Slot_SelfSlot,
                Effects =
                [
                    
                ],
                Rarity = Rarity.Uncommon,
                Priority = Priority.Fast,
            };
            stare.AddIntentsToTarget(Targeting.Slot_SelfSlot, ["AA_Nothing"]);
            anomalystare = stare;

            Ability screamsfrombeyond = new Ability("Screams from Beyond", "AApocrypha_ScreamsFromBeyond_A")
            {
                Description = "Deal a Little indirect damage to the Opposing party member and this enemy, then move this enemy to the Left or Right. Repeat this two more times.",
                Cost = [Pigments.Purple, Pigments.Purple],
                Visuals = Visuals.Mitosis,
                AnimationTarget = Targeting.Slot_SelfSlot,
                Effects =
                [
                    Effects.GenerateEffect(IndirectDamage, 2, Targeting.Slot_Front),
                    Effects.GenerateEffect(IndirectDamage, 2, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<SwapToSidesEffect>(), 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(IndirectDamage, 2, Targeting.Slot_Front),
                    Effects.GenerateEffect(IndirectDamage, 2, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<SwapToSidesEffect>(), 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(IndirectDamage, 2, Targeting.Slot_Front),
                    Effects.GenerateEffect(IndirectDamage, 2, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<SwapToSidesEffect>(), 1, Targeting.Slot_SelfSlot),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.Fast,
            };
            screamsfrombeyond.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Damage_1_2)]);
            screamsfrombeyond.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Damage_1_2)]);
            screamsfrombeyond.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Swap_Sides)]);
            screamsfrombeyond.AddIntentsToTarget(Targeting.Slot_SelfSlot, ["AA_Multi3"]);

            Ability whispersfrombeyond = new Ability("Whispers from Beyond", "AApocrypha_WhispersFromBeyond_A")
            {
                Description = "Apply 1 Scar to all party members.",
                Cost = [Pigments.Purple, Pigments.Purple],
                Visuals = Visuals.Genesis,
                AnimationTarget = Targeting.Slot_OpponentAllSlots,
                Effects =
                [
                    Effects.GenerateEffect(ScarsApply, 1, Targeting.Slot_OpponentAllSlots),
                ],
                Rarity = Rarity.Uncommon,
                Priority = Priority.Fast,
            };
            whispersfrombeyond.AddIntentsToTarget(Targeting.Slot_OpponentAllSlots, [nameof(IntentType_GameIDs.Status_Scars)]);
            anomalywhispers = whispersfrombeyond;

            Ability tearsfrombeyond = new Ability("Tears from Beyond", "AApocrypha_TearsFromBeyond_A")
            {
                Description = "This enemy cries and produces 2 Purple Pigment.",
                Cost = [Pigments.Purple, Pigments.Purple],
                Visuals = Visuals.Weep,
                AnimationTarget = Targeting.Slot_SelfSlot,
                Effects =
                [
                    Effects.GenerateEffect(GivePurple, 2, Targeting.Slot_SelfSlot),
                ],
                Rarity = Rarity.Uncommon,
                Priority = Priority.Fast,
            };
            tearsfrombeyond.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Mana_Generate)]);
            anomalytears = tearsfrombeyond;

            Ability dissipate = new Ability("Dissipate", "AApocrypha_Dissipate_A")
            {
                Description = "Deal an Agonizing amount of damage to the Opposing party member and destroy this enemy.",
                Cost = [Pigments.Purple, Pigments.Purple],
                Visuals = Visuals.Crush,
                AnimationTarget = Targeting.Slot_Front,
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 8, Targeting.Slot_Front),
                    Effects.GenerateEffect(CrushSelfAnim, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DirectDeathEffect>(), 1, Targeting.Slot_SelfSlot),
                ],
                Rarity = Rarity.VeryRare,
                Priority = Priority.VerySlow,
            };
            dissipate.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Damage_7_10)]);
            dissipate.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Damage_Death)]);

            Ability instability = new Ability("Instability", "AApocrypha_Instability_A")
            {
                Description = "Deal anywhere between a Little and a Painful amount of indirect damage to this enemy.",
                Cost = [],
                Visuals = null,
                AnimationTarget = Targeting.Slot_SelfSlot,
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<ExtraVariableForNextEffect>(), 2, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(IndirectRandom, 4, Targeting.Slot_SelfSlot),
                ],
                Rarity = Rarity.Impossible,
                Priority = Priority.ExtremelySlow,
            };
            instability.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Damage_1_2)]);
            instability.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Damage_3_6)]);

            ExtraAbilityInfo instabilityextra = new()
            {
                ability = instability.GenerateEnemyAbility().ability,
                rarity = Rarity.Impossible,
            };

            unboundanomaly.AddPassives([Passives.Pure, Passives.Absorb, Passives.GetCustomPassive("Confrontational_PA"), Passives.BonusAttackGenerator(instabilityextra)]);

            unboundanomaly.AddEnemyAbilities(
                [
                    stare,
                    screamsfrombeyond,
                    whispersfrombeyond,
                    tearsfrombeyond,
                    dissipate,
                ]);
            unboundanomaly.AddEnemy(true, true, true);
        }
    }
}
