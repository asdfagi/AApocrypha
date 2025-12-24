using System;
using System.Collections.Generic;
using System.Text;
using A_Apocrypha.CustomOther;

namespace A_Apocrypha.Enemies
{
    public class Smoldergeist
    {
        public static void Add()
        {
            RemovePassiveEffect UnFire = ScriptableObject.CreateInstance<RemovePassiveEffect>();
            UnFire.m_PassiveID = "MadeOfFire";

            AddPassiveEffect ReFire = ScriptableObject.CreateInstance<AddPassiveEffect>();
            ReFire._passiveToAdd = Passives.GetCustomPassive("MadeOfFire_PA");

            SetCasterAnimationParameterEffect MakeSad = ScriptableObject.CreateInstance<SetCasterAnimationParameterEffect>();
            MakeSad._parameterName = "Sad";
            MakeSad._parameterValue = 1;
            MakeSad._UsePrevious = false;

            SetCasterAnimationParameterEffect MakeHappy = ScriptableObject.CreateInstance<SetCasterAnimationParameterEffect>();
            MakeHappy._parameterName = "Sad";
            MakeHappy._parameterValue = 0;
            MakeHappy._UsePrevious = false;

            ReturnValueComparatorEffectorCondition EightOrMore = ScriptableObject.CreateInstance<ReturnValueComparatorEffectorCondition>();
            EightOrMore._lessThan = false;
            EightOrMore._comparator = 8;

            PerformEffectPassiveAbility rectifySmoldergeist = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
            rectifySmoldergeist.name = "AA_RectifySmoldergeist_PA";
            rectifySmoldergeist._passiveName = "Rectify (8)";
            rectifySmoldergeist.m_PassiveID = "RectifySmoldergeist";
            rectifySmoldergeist.passiveIcon = ResourceLoader.LoadSprite("ChalicePassiveA");
            rectifySmoldergeist._characterDescription = "Nice try, but you're not made of fire, are you?";
            rectifySmoldergeist._enemyDescription = "On taking 8 or more damage, remove Made Of Fire from this enemy.";
            rectifySmoldergeist.doesPassiveTriggerInformationPanel = true;
            rectifySmoldergeist._triggerOn = [TriggerCalls.OnDirectDamaged];
            rectifySmoldergeist.conditions = [EightOrMore];
            rectifySmoldergeist.effects =
            [
                Effects.GenerateEffect(UnFire, 1, Targeting.Slot_SelfSlot),
                Effects.GenerateEffect(MakeSad),
            ];
            Passives.AddCustomPassiveToPool("AA_RectifySmoldergeist_PA", "Rectify (8)", rectifySmoldergeist);

            Enemy smoldergeist = new Enemy("Smoldergeist", "Smoldergeist_EN")
            {
                Health = 40,
                HealthColor = Pigments.Red,
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("SmoldergeistTimeline", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("SmoldergeistDead", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("SmoldergeistTimeline", new Vector2(0.5f, 0f), 32),
                DamageSound = LoadedAssetsHandler.GetEnemy("Spoggle_Writhing_EN").damageSound,
                DeathSound = LoadedAssetsHandler.GetEnemy("Spoggle_Writhing_EN").deathSound,
            };
            smoldergeist.PrepareEnemyPrefab("Assets/Apocrypha_Enemies/Smoldergeist_Enemy/Smoldergeist_Enemy.prefab", AApocrypha.assetBundle, AApocrypha.assetBundle.LoadAsset<GameObject>("Assets/Apocrypha_Enemies/Smoldergeist_Enemy/Smoldergeist_Giblets.prefab").GetComponent<ParticleSystem>());

            StatusEffect_Apply_Effect OilApply = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            OilApply._Status = StatusField.OilSlicked;

            FieldEffect_Apply_Effect FireApply = ScriptableObject.CreateInstance<FieldEffect_Apply_Effect>();
            FireApply._Field = StatusField.OnFire;

            FieldEffect_Apply_Effect FireApplyRandom = ScriptableObject.CreateInstance<FieldEffect_Apply_Effect>();
            FireApplyRandom._Field = StatusField.OnFire;
            FireApplyRandom._UseRandomBetweenPrevious = true;

            Ability petroleum = new Ability("Petroleum Spill", "AApocrypha_PetroleumSpill_A")
            {
                Description = "Inflict 2 Oil Slicked to the Opposing party member, then deal a Little damage to them.\nApply 0-1 Fire to this enemy's position and the Left and Right allied positions.",
                Cost = [Pigments.Red, Pigments.Red],
                Visuals = Visuals.Doused,
                AnimationTarget = Targeting.Slot_Front,
                Effects =
                [
                    Effects.GenerateEffect(OilApply, 2, Targeting.Slot_Front),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 2, Targeting.Slot_Front),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<ExtraVariableForNextEffect>(), 0, Targeting.Slot_Front),
                    Effects.GenerateEffect(FireApplyRandom, 1, Targeting.Slot_SelfAndSides),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.Fast,
            };
            petroleum.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Status_OilSlicked)]);
            petroleum.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Damage_1_2)]);
            petroleum.AddIntentsToTarget(Targeting.Slot_SelfAndSides, [nameof(IntentType_GameIDs.Field_Fire)]);

            Ability immolate = new Ability("Gleeful Immolation", "AApocrypha_GleefulImmolation_A")
            {
                Description = "Deal a Painful amount of damage to the Opposing party member and apply 2 Fire to them.\nApply 0-1 Fire to this enemy's position and the Left and Right allied positions.",
                Cost = [Pigments.Red, Pigments.Red],
                Visuals = Visuals.Torched,
                AnimationTarget = Targeting.Slot_Front,
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 6, Targeting.Slot_Front),
                    Effects.GenerateEffect(FireApply, 2, Targeting.Slot_Front),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<ExtraVariableForNextEffect>(), 0, Targeting.Slot_Front),
                    Effects.GenerateEffect(FireApplyRandom, 1, Targeting.Slot_SelfAndSides),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.Fast,
            };
            immolate.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Damage_3_6)]);
            immolate.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Field_Fire)]);
            immolate.AddIntentsToTarget(Targeting.Slot_SelfAndSides, [nameof(IntentType_GameIDs.Field_Fire)]);

            Ability corpsewax = new Ability("Corpse Wax", "AApocrypha_CorpseWax_A")
            {
                Description = "If this enemy does not have Made Of Fire as a passive, apply it to this enemy.\nOtherwise, apply 1 Fire to the Left, Right and Opposing party member positions as well as this enemy's position and its Left and Right allied positions.",
                Cost = [Pigments.Red, Pigments.Red],
                Visuals = Visuals.Pyre,
                AnimationTarget = Targeting.Slot_SelfSlot,
                Effects =
                [
                    Effects.GenerateEffect(ReFire, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(MakeHappy, 1, Targeting.Slot_SelfSlot, PreviousGenerator(true, 1)),
                    Effects.GenerateEffect(FireApply, 1, Targeting.Slot_FrontAndSides, PreviousGenerator(false, 2)),
                    Effects.GenerateEffect(FireApply, 1, Targeting.Slot_SelfAndSides, PreviousGenerator(false, 3)),
                ],
                Rarity = Rarity.Rare,
                Priority = Priority.Fast,
            };
            corpsewax.AddIntentsToTarget(Targeting.Slot_SelfSlot, ["Passive_MadeOfFire"]);
            corpsewax.AddIntentsToTarget(Targeting.Slot_FrontAndSides, [nameof(IntentType_GameIDs.Field_Fire)]);
            corpsewax.AddIntentsToTarget(Targeting.Slot_SelfAndSides, [nameof(IntentType_GameIDs.Field_Fire)]);

            smoldergeist.AddEnemyAbilities(
            [
                petroleum.GenerateEnemyAbility(true),
                immolate.GenerateEnemyAbility(true),
                corpsewax.GenerateEnemyAbility(true),
            ]);

            smoldergeist.AddPassives([Passives.Skittish, CustomPassives.ThresholdMasochismGenerator(8), Passives.GetCustomPassive("AA_RectifySmoldergeist_PA"), Passives.GetCustomPassive("MadeOfFire_PA")]);
            smoldergeist.AddEnemy(false, false, false);
        }
        static PreviousEffectCondition PreviousGenerator(bool wasTrue, int number)
        {
            PreviousEffectCondition previous = ScriptableObject.CreateInstance<PreviousEffectCondition>();
            previous.wasSuccessful = wasTrue;
            previous.previousAmount = number;
            return previous;
        }
    }
}

    
