using System;
using System.Collections.Generic;
using System.Text;
using A_Apocrypha.Animations;
using BrutalAPI;
using UnityEngine;

namespace A_Apocrypha.Enemies
{
    public class Macerator
    {
        public static void Add()
        {
            Enemy macerator = new Enemy("Macerator", "Macerator_EN")
            {
                Health = 10,
                HealthColor = Pigments.Grey,
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("MaceratorTimeline", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("MaceratorDead", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("MaceratorTimeline", new Vector2(0.5f, 0f), 32),
                DamageSound = "event:/AAEnemy/Anomaly1Hurt",
                DeathSound = "event:/AAEnemy/Anomaly1Death",
            };
            macerator.PrepareEnemyPrefab("Assets/Apocrypha_Enemies/Macerator_Enemy/Macerator_Enemy.prefab", AApocrypha.assetBundle, AApocrypha.assetBundle.LoadAsset<GameObject>("Assets/Apocrypha_Enemies/Macerator_Enemy/Macerator_Giblets.prefab").GetComponent<ParticleSystem>());
            macerator.AddPassives([]);

            StatusEffect_Apply_Effect RupturedApply = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            RupturedApply._Status = StatusField.Ruptured;

            SwapToSidesEffect SwapEither = ScriptableObject.CreateInstance<SwapToSidesEffect>();

            Ability pulverize = new Ability("Pulverize", "AApocrypha_Pulverize_A")
            {
                Description = "Deal a painful amount of damage to the opposing party member.",
                Cost = [Pigments.Grey],
                Visuals = CustomVisuals.TestCannonVisualsSO,
                AnimationTarget = Targeting.Slot_Front,
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 4, Targeting.Slot_Front),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.Normal,
            };
            pulverize.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Damage_3_6)]);

            Ability sand_down = new Ability("Sand Down", "AApocrypha_SandDown_A")
            {
                Description = "Move to the left or right, then apply 2 Ruptured to the opposing party member and move them to the left or right.",
                Cost = [Pigments.Grey, Pigments.Red],
                Visuals = Visuals.Flay,
                AnimationTarget = Targeting.Slot_SelfSlot,
                Effects =
                [
                    Effects.GenerateEffect(SwapEither, 10, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(RupturedApply, 2, Targeting.Slot_Front),
                    Effects.GenerateEffect(SwapEither, 1, Targeting.Slot_Front)
                ],
                Rarity = Rarity.Common,
                Priority = Priority.Normal,
            };
            sand_down.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Swap_Sides)]);
            sand_down.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Status_Ruptured)]);
            sand_down.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Swap_Sides)]);

            macerator.AddEnemyAbilities(
                [
                    pulverize,
                    sand_down,
                ]);
            macerator.AddEnemy(true, true, true);
        }
    }
}
