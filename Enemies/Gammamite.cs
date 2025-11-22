using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Enemies
{
    public class Gammamite
    {
        public static void Add()
        {
            Enemy tickmutant = new Enemy("Gammamite", "Gammamite_EN")
            {
                Health = 28,
                HealthColor = Pigments.Red,
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("RadtickTimeline", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("RadtickDead", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("RadtickTimeline", new Vector2(0.5f, 0f), 32),
                DamageSound = "event:/AAEnemy/RadtickHurt",
                DeathSound = "event:/AAEnemy/RadtickDeath",
                UnitTypes = [],
            };
            tickmutant.PrepareEnemyPrefab("Assets/Apocrypha_Enemies/Carcinogen_Enemy/Carcinogen_Enemy.prefab", AApocrypha.assetBundle, AApocrypha.assetBundle.LoadAsset<GameObject>("Assets/Apocrypha_Enemies/Carcinogen_Enemy/Carcinogen_Giblets.prefab").GetComponent<ParticleSystem>());
            tickmutant.AddPassives([Passives.Skittish]);

            StatusEffect_Apply_Effect RandomPreviousPoisonedApply = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            RandomPreviousPoisonedApply._Status = StatusField.GetCustomStatusEffect("Poisoned_ID");
            RandomPreviousPoisonedApply._RandomBetweenPrevious = true;

            StatusEffect_Apply_Effect ApplyIrradiated = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            ApplyIrradiated._Status = StatusField.GetCustomStatusEffect("Irradiated_ID");

            AttackVisualsSO PoisonVisuals = Visuals.Exsanguinate;
            if (AApocrypha.CrossMod.IntoTheAbyss) { PoisonVisuals = LoadedAssetsHandler.GetCharacterAbility("FlorenzBasic_A").visuals; }

            IncreaseStatusEffectsEffect IncreaseStatusBad = ScriptableObject.CreateInstance<IncreaseStatusEffectsEffect>();
            IncreaseStatusBad.m_AffectStatusEffects = true;
            IncreaseStatusBad.m_AffectFieldEffects = false;
            IncreaseStatusBad._increasePositives = false;

            IncreaseStatusEffectsEffect IncreaseStatusGood = ScriptableObject.CreateInstance<IncreaseStatusEffectsEffect>();
            IncreaseStatusGood.m_AffectStatusEffects = true;
            IncreaseStatusGood.m_AffectFieldEffects = false;
            IncreaseStatusGood._increasePositives = true;

            DamageWithStatusBonusEffect RadBoostedDamage = ScriptableObject.CreateInstance<DamageWithStatusBonusEffect>();
            RadBoostedDamage._status = StatusField.GetCustomStatusEffect("Irradiated_ID");
            RadBoostedDamage._bonusAmount = 2;
            RadBoostedDamage._bonusStacking = true;

            ChangeMaxHealthEffect ReduceMaxHealth = ScriptableObject.CreateInstance<ChangeMaxHealthEffect>();
            ReduceMaxHealth._increase = false;

            Ability radiumfangs = new Ability("Radium Fangs", "AApocrypha_RadiumFangs_A")
            {
                Description = "Deal an Agonizing amount of damage to the Opposing party member.\nReduce the Opposing party member's maximum health by 4 and apply 2 Irradiated to them.",
                Cost = [Pigments.Red],
                Visuals = Visuals.Chomp,
                AnimationTarget = Targeting.Slot_Front,
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 8, Targeting.Slot_Front),
                    Effects.GenerateEffect(ReduceMaxHealth, 4, Targeting.Slot_Front),
                    Effects.GenerateEffect(ApplyIrradiated, 2, Targeting.Slot_Front),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.Normal,
            };
            radiumfangs.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Damage_7_10)]);
            radiumfangs.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Other_MaxHealth)]);
            radiumfangs.AddIntentsToTarget(Targeting.Slot_Front, ["Status_Irradiated"]);

            Ability radiotherapy = new Ability("Radiotherapy", "AApocrypha_Radiotherapy_A")
            {
                Description = "Reduce the Left, Right and Opposing party members' maximum health by 2 and apply 1 Irradiated to them.\nIncrease All status effects on the Left, Right and Opposing party members by 1.",
                Cost = [Pigments.Red],
                Visuals = CustomVisuals.MicrowaveVisualsSO,
                AnimationTarget = Targeting.Slot_FrontAndSides,
                Effects =
                [
                    Effects.GenerateEffect(ReduceMaxHealth, 2, Targeting.Slot_FrontAndSides),
                    Effects.GenerateEffect(ApplyIrradiated, 1, Targeting.Slot_FrontAndSides),
                    Effects.GenerateEffect(IncreaseStatusBad, 1, Targeting.Slot_FrontAndSides),
                    Effects.GenerateEffect(IncreaseStatusGood, 1, Targeting.Slot_FrontAndSides),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.Normal,
            };
            radiotherapy.AddIntentsToTarget(Targeting.Slot_FrontAndSides, [nameof(IntentType_GameIDs.Other_MaxHealth)]);
            radiotherapy.AddIntentsToTarget(Targeting.Slot_FrontAndSides, ["Status_Irradiated"]);
            radiotherapy.AddIntentsToTarget(Targeting.Slot_FrontAndSides, [nameof(IntentType_GameIDs.Misc)]);

            Ability carbondating = new Ability("Carbon Dating", "AApocrypha_CarbonDating_A")
            {
                Description = "Deal a Painful amount of damage to the Left and Right party members.\nEach point of Irradiated on a target increases the damage they take by 2.",
                Cost = [Pigments.Red],
                Visuals = Visuals.Gnaw,
                AnimationTarget = Targeting.Slot_OpponentSides,
                Effects =
                [
                    Effects.GenerateEffect(RadBoostedDamage, 5, Targeting.Slot_OpponentSides),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.Normal,
            };
            carbondating.AddIntentsToTarget(Targeting.Slot_OpponentSides, [nameof(IntentType_GameIDs.Damage_3_6)]);
            carbondating.AddIntentsToTarget(Targeting.Slot_OpponentSides, [nameof(IntentType_GameIDs.Misc)]);

            tickmutant.AddEnemyAbilities(
            [
                radiumfangs,
                radiotherapy,
                carbondating,
            ]);

            tickmutant.AddEnemy(true, true, false);
        }
    }
}
