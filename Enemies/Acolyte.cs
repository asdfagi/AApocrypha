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
            acolyte.AddPassives([Passives.Skittish, Passives.GetCustomPassive("Zelator_PA")]);

            StatusEffect_Apply_Effect HexedApply = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            HexedApply._Status = StatusField.GetCustomStatusEffect("Hexed_ID");

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
                Description = "Deal a Little damage to the Left and Right party members. Apply 1 Hexed to the Left and Right party members.",
                Cost = [Pigments.RedPurple, Pigments.Purple],
                Visuals = Visuals.UglyOnTheInside,
                AnimationTarget = Targeting.Slot_OpponentSides,
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 2, Targeting.Slot_OpponentSides),
                    Effects.GenerateEffect(HexedApply, 1, Targeting.Slot_OpponentSides),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.Normal,
            };
            hex.AddIntentsToTarget(Targeting.Slot_OpponentSides, [nameof(IntentType_GameIDs.Damage_1_2), "Status_Hexed"]);

            Ability sunder = new Ability("Sunder", "AApocrypha_Sunder_A")
            {
                Description = "Deal a Painful amount of damage to the Opposing party member. Apply 2 Hexed to the Opposing party member.",
                Cost = [Pigments.Red, Pigments.RedPurple],
                Visuals = Visuals.InvadeTheVeins,
                AnimationTarget = Targeting.Slot_Front,
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 4, Targeting.Slot_Front),
                    Effects.GenerateEffect(HexedApply, 2, Targeting.Slot_Front),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.Normal,
            };
            sunder.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Damage_3_6), "Status_Hexed"]);
            
            Ability invokechaos = new Ability("Invoke Chaos", "AApocrypha_InvokeChaos_A")
            {
                Description = "Shuffle the positions of the Left, Right and Opposing party members. Apply 1 Hexed to the Left, Right and Opposing party members.",
                Cost = [Pigments.PurpleRed, Pigments.RedPurple],
                Visuals = CustomVisuals.GazeVisualsSO,
                AnimationTarget = Targeting.Slot_SelfSlot,
                Effects =
                [
                    Effects.GenerateEffect(Shuffle, 1, Targeting.Slot_FrontAndSides),
                    Effects.GenerateEffect(InvokeAnim, 1, Targeting.Slot_FrontAndSides),
                    Effects.GenerateEffect(HexedApply, 1, Targeting.Slot_FrontAndSides),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.VeryFast,
            };
            invokechaos.AddIntentsToTarget(Targeting.Slot_FrontAndSides, [nameof(IntentType_GameIDs.Swap_Mass), "Status_Hexed"]);

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
