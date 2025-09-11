using System;
using System.Collections.Generic;
using System.Text;
using A_Apocrypha.Animations;
using UnityEngine.VFX;

namespace A_Apocrypha.Enemies
{
    public class Acolyte
    {
        public static void Add()
        {
            Enemy acolyte = new Enemy("Acolyte", "Acolyte_EN")
            {
                Health = 16,
                HealthColor = Pigments.RedPurple,
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("AcolyteTimeline", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("AcolyteDead", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("AcolyteTimeline", new Vector2(0.5f, 0f), 32),
                DamageSound = "event:/Combat/StatusEffects/SE_Divine_Trg",
                DeathSound = LoadedAssetsHandler.GetEnemy("TaMaGoa_EN").damageSound,
            };
            acolyte.PrepareEnemyPrefab("Assets/Apocrypha_Enemies/Acolyte_Enemy/Acolyte_Enemy.prefab", AApocrypha.assetBundle, AApocrypha.assetBundle.LoadAsset<GameObject>("Assets/Apocrypha_Enemies/Acolyte_Enemy/Acolyte_Giblets.prefab").GetComponent<ParticleSystem>());
            acolyte.AddPassives([Passives.Skittish]);

            StatusEffect_Apply_Effect ScarsApply = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            ScarsApply._Status = StatusField.Scars;

            HealEffect PreviousHeal = ScriptableObject.CreateInstance<HealEffect>();
            PreviousHeal.usePreviousExitValue = true;

            RemoveStatusEffectEffect ScarsRemove = ScriptableObject.CreateInstance<RemoveStatusEffectEffect>();
            ScarsRemove._status = StatusField.Scars;

            StatusEffectCheckerEffect HasScars = ScriptableObject.CreateInstance<StatusEffectCheckerEffect>();
            HasScars._status = StatusField.Scars;

            PreviousEffectCondition PreviousCondition = ScriptableObject.CreateInstance<PreviousEffectCondition>();

            MassSwapZoneEffect Shuffle = ScriptableObject.CreateInstance<MassSwapZoneEffect>();

            AnimationVisualsIfUnitEffect InvokeAnim = ScriptableObject.CreateInstance<AnimationVisualsIfUnitEffect>();
            InvokeAnim._visuals = Visuals.Melt;
            InvokeAnim._animationTarget = Targeting.Slot_FrontAndSides;

            Ability hex = new Ability("Hex", "AApocrypha_Hex_A")
            {
                Description = "Deals a Little damage to the Left and Right party members. Inflicts 1 Scar to the Left and Right party members.",
                Cost = [Pigments.RedPurple, Pigments.Purple],
                Visuals = Visuals.Genesis,
                AnimationTarget = Targeting.Slot_OpponentSides,
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 2, Targeting.Slot_OpponentSides),
                    Effects.GenerateEffect(ScarsApply, 1, Targeting.Slot_OpponentSides),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.Normal,
            };
            hex.AddIntentsToTarget(Targeting.Slot_OpponentSides, [nameof(IntentType_GameIDs.Damage_1_2)]);
            hex.AddIntentsToTarget(Targeting.Slot_OpponentSides, [nameof(IntentType_GameIDs.Status_Scars)]);

            Ability sunder = new Ability("Sunder", "AApocrypha_Sunder_A")
            {
                Description = "Deals a Painful amount of damage to the Opposing party member. If the Opposing party member has Scars, remove them and heal this enemy for the amount of Scars removed.",
                Cost = [Pigments.Red, Pigments.RedPurple],
                Visuals = Visuals.InvadeTheVeins,
                AnimationTarget = Targeting.Slot_Front,
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 4, Targeting.Slot_Front),
                    Effects.GenerateEffect(HasScars, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(ScarsRemove, 1, Targeting.Slot_Front, PreviousCondition),
                    Effects.GenerateEffect(PreviousHeal, 1, Targeting.Slot_SelfSlot, PreviousCondition)
                ],
                Rarity = Rarity.Common,
                Priority = Priority.Normal,
            };
            sunder.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Damage_3_6)]);
            sunder.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Rem_Status_Scars)]);
            sunder.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Heal_1_4)]);

            Ability invokechaos = new Ability("Invoke Chaos", "AApocrypha_InvokeChaos_A")
            {
                Description = "Shuffles the positions of the Left, Right and Opposing party members. Applies 1 Scar to the Left, Right and Opposing party members.",
                Cost = [Pigments.PurpleRed, Pigments.RedPurple],
                Visuals = CustomVisuals.GazeVisualsSO,
                AnimationTarget = Targeting.Slot_SelfSlot,
                Effects =
                [
                    Effects.GenerateEffect(Shuffle, 1, Targeting.Slot_FrontAndSides),
                    Effects.GenerateEffect(InvokeAnim, 1, Targeting.Slot_FrontAndSides),
                    Effects.GenerateEffect(ScarsApply, 1, Targeting.Slot_FrontAndSides),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.VeryFast,
            };
            invokechaos.AddIntentsToTarget(Targeting.Slot_FrontAndSides, [nameof(IntentType_GameIDs.Swap_Mass)]);
            invokechaos.AddIntentsToTarget(Targeting.Slot_FrontAndSides, [nameof(IntentType_GameIDs.Status_Scars)]);

            acolyte.AddEnemyAbilities(
                [
                    hex,
                    sunder,
                    invokechaos,
                ]);
            acolyte.AddEnemy(true, true, true);
        }
    }
}
