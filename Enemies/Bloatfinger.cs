using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Enemies
{
    public class Bloatfinger
    {
        public static void Add()
        {
            Enemy bloatfinger = new Enemy("Bloatfinger", "Bloatfinger_EN")
            {
                Health = 30,
                HealthColor = Pigments.Red,
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("BloatfingerTimeline", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("BloatfingerDead", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("BloatfingerTimeline", new Vector2(0.5f, 0f), 32),
                DamageSound = LoadedAssetsHandler.GetEnemy("SilverSuckle_EN").damageSound,
                DeathSound = LoadedAssetsHandler.GetEnemy("SilverSuckle_EN").deathSound,
            };
            bloatfinger.PrepareEnemyPrefab("Assets/Apocrypha_Enemies/Bloatfinger_Enemy/Bloatfinger_Enemy.prefab", AApocrypha.assetBundle, AApocrypha.assetBundle.LoadAsset<GameObject>("Assets/Apocrypha_Enemies/Bloatfinger_Enemy/Bloatfinger_Giblets.prefab").GetComponent<ParticleSystem>());
            bloatfinger.AddPassives([Passives.Slippery]);

            AttackVisualsSO PoisonVisuals = Visuals.Exsanguinate;
            if (AApocrypha.CrossMod.IntoTheAbyss) { PoisonVisuals = LoadedAssetsHandler.GetCharacterAbility("FlorenzBasic_A").visuals; }

            StatusEffect_Apply_Effect RandomPreviousPoisonedApply = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            RandomPreviousPoisonedApply._Status = StatusField.GetCustomStatusEffect("Poisoned_ID");
            RandomPreviousPoisonedApply._RandomBetweenPrevious = true;

            DamageOnDoubleCascadeEffect DamageCascade = ScriptableObject.CreateInstance<DamageOnDoubleCascadeEffect>();
            DamageCascade._cascadeIsIndirect = true;
            DamageCascade._decreaseAsPercentage = true;
            DamageCascade._cascadeDecrease = 75;
            DamageCascade._returnKillAsSuccess = true;

            RemoveFieldEffectEffect RemoveShield = ScriptableObject.CreateInstance<RemoveFieldEffectEffect>();
            RemoveShield._field = StatusField.Shield;

            SwapToSidesEffect SwapEither = ScriptableObject.CreateInstance<SwapToSidesEffect>();

            DamageEffect DamageKillTrue = ScriptableObject.CreateInstance<DamageEffect>();
            DamageKillTrue._returnKillAsSuccess = true;

            AnimationVisualsEffect StinkyAllAnim = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            StinkyAllAnim._visuals = PoisonVisuals;
            StinkyAllAnim._animationTarget = Targeting.Unit_AllOpponentSlots;

            AnimationVisualsEffect StinkyFrontSidesAnim = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            StinkyFrontSidesAnim._visuals = PoisonVisuals;
            StinkyFrontSidesAnim._animationTarget = Targeting.Slot_FrontAndSides;

            AnimationVisualsEffect SadAnim = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            SadAnim._visuals = Visuals.Cry;
            SadAnim._animationTarget = Targeting.Slot_SelfSlot;

            QueueTimelineAbilityByNameEffect QueueGnash = ScriptableObject.CreateInstance<QueueTimelineAbilityByNameEffect>();
            QueueGnash._abilityName = "Gnashing Mouths";

            Ability gnashingmouths = new Ability("Gnashing Mouths", "AApocrypha_GnashingMouths_A")
            {
                Description = "Deal an Agonizing amount of damage to the Opposing party member.\nIf this kills, move Left or Right and queue this ability into the timeline again.",
                Cost = [Pigments.Red],
                Visuals = Visuals.Gnaw,
                AnimationTarget = Targeting.Slot_Front,
                Effects =
                [
                    Effects.GenerateEffect(DamageKillTrue, 8, Targeting.Slot_Front),
                    Effects.GenerateEffect(SwapEither, 1, Targeting.Slot_SelfSlot, PreviousGenerator(true, 1)),
                    Effects.GenerateEffect(QueueGnash, 1, Targeting.Slot_SelfSlot, PreviousGenerator(true, 2)),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.Normal,
            };
            gnashingmouths.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Damage_7_10)]);
            gnashingmouths.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Swap_Sides)]);
            gnashingmouths.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Misc_Additional)]);

            Ability causticpanic = new Ability("Caustic Panic", "AApocrypha_CausticPanic_A")
            {
                Description = "If there is an Opposing party member, remove all Shields from their position and apply 1-3 Poisoned to them, then move this enemy to the Left or Right. Repeat this up to 5 times or until no longer Opposing a party member.",
                Cost = [Pigments.Red],
                Visuals = Visuals.Scream,
                AnimationTarget = Targeting.Slot_SelfSlot,
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<CheckHasUnitEffect>(), 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(RemoveShield, 1, Targeting.Slot_Front, PreviousGenerator(true, 1)),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<ExtraVariableForNextEffect>(), 1, Targeting.Slot_SelfSlot, PreviousGenerator(true, 2)),
                    Effects.GenerateEffect(RandomPreviousPoisonedApply, 3, Targeting.Slot_Front, PreviousGenerator(true, 3)),
                    Effects.GenerateEffect(SwapEither, 1, Targeting.Slot_SelfSlot, PreviousGenerator(true, 4)),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<CheckHasUnitEffect>(), 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(RemoveShield, 1, Targeting.Slot_Front, PreviousGenerator(true, 1)),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<ExtraVariableForNextEffect>(), 1, Targeting.Slot_SelfSlot, PreviousGenerator(true, 2)),
                    Effects.GenerateEffect(RandomPreviousPoisonedApply, 3, Targeting.Slot_Front, PreviousGenerator(true, 3)),
                    Effects.GenerateEffect(SwapEither, 1, Targeting.Slot_SelfSlot, PreviousGenerator(true, 4)),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<CheckHasUnitEffect>(), 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(RemoveShield, 1, Targeting.Slot_Front, PreviousGenerator(true, 1)),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<ExtraVariableForNextEffect>(), 1, Targeting.Slot_SelfSlot, PreviousGenerator(true, 2)),
                    Effects.GenerateEffect(RandomPreviousPoisonedApply, 3, Targeting.Slot_Front, PreviousGenerator(true, 3)),
                    Effects.GenerateEffect(SwapEither, 1, Targeting.Slot_SelfSlot, PreviousGenerator(true, 4)),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<CheckHasUnitEffect>(), 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(RemoveShield, 1, Targeting.Slot_Front, PreviousGenerator(true, 1)),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<ExtraVariableForNextEffect>(), 1, Targeting.Slot_SelfSlot, PreviousGenerator(true, 2)),
                    Effects.GenerateEffect(RandomPreviousPoisonedApply, 3, Targeting.Slot_Front, PreviousGenerator(true, 3)),
                    Effects.GenerateEffect(SwapEither, 1, Targeting.Slot_SelfSlot, PreviousGenerator(true, 4)),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<CheckHasUnitEffect>(), 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(RemoveShield, 1, Targeting.Slot_Front, PreviousGenerator(true, 1)),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<ExtraVariableForNextEffect>(), 1, Targeting.Slot_SelfSlot, PreviousGenerator(true, 2)),
                    Effects.GenerateEffect(RandomPreviousPoisonedApply, 3, Targeting.Slot_Front, PreviousGenerator(true, 3)),
                    Effects.GenerateEffect(SwapEither, 1, Targeting.Slot_SelfSlot, PreviousGenerator(true, 4)),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<CheckHasUnitEffect>(), 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(SadAnim, 1, Targeting.Slot_Front, PreviousGenerator(true, 1)),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.Normal,
            };
            causticpanic.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Rem_Field_Shield)]);
            causticpanic.AddIntentsToTarget(Targeting.Slot_Front, ["Status_Poisoned"]);
            causticpanic.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Swap_Sides)]);
            causticpanic.AddIntentsToTarget(Targeting.Slot_SelfSlot, ["AA_Multi"]);

            bloatfinger.AddEnemyAbilities(
                [
                    gnashingmouths,
                    causticpanic,
                ]);

            bloatfinger.AddEnemy(true, true, false);

            Ability wait = new Ability("Hideaway", "AApocrypha_BloatfingerHiddenWait_A")
            {
                Description = "This enemy does nothing.",
                Cost = [Pigments.Grey],
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<ExtraVariableForNextEffect>(), 1),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.VerySlow,
            };

            Ability crumble = new Ability("Crumble", "AApocrypha_BloatfingerHiddenCrumble_A")
            {
                Description = "This enemy crumbles.",
                Cost = [Pigments.Grey],
                Visuals = Visuals.Crush,
                AnimationTarget = Targeting.Slot_SelfSlot,
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DirectDeathEffect>(), 1, Targeting.Slot_SelfSlot),
                ],
                Rarity = Rarity.VeryRare,
                Priority = Priority.VerySlow,
            };
            crumble.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Damage_Death)]);

            SpawnEnemyInSlotFromEntryEffect BloatfingerSpawning = ScriptableObject.CreateInstance<SpawnEnemyInSlotFromEntryEffect>();
            BloatfingerSpawning.enemy = bloatfinger.enemy;
            BloatfingerSpawning._spawnTypeID = "Spawn_Basic";
            BloatfingerSpawning.givesExperience = false;
            BloatfingerSpawning.trySpawnAnywhereIfFail = false;

            PerformEffectPassiveAbility DecayHiddenBloatfinger = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
            DecayHiddenBloatfinger.name = "Decay_HiddenBloatfinger_PA";
            DecayHiddenBloatfinger._passiveName = Passives.Example_Decay_MudLung._passiveName;
            DecayHiddenBloatfinger.m_PassiveID = Passives.Example_Decay_MudLung.m_PassiveID;
            DecayHiddenBloatfinger.passiveIcon = Passives.Example_Decay_MudLung.passiveIcon;
            DecayHiddenBloatfinger._characterDescription = "It's right behind me, isn't it?";
            DecayHiddenBloatfinger._enemyDescription = "On dying except from Withering, a hidden creature is revealed.";
            DecayHiddenBloatfinger.effects = [Effects.GenerateEffect(BloatfingerSpawning, 0, Targeting.Slot_SelfSlot)];
            DecayHiddenBloatfinger._triggerOn = [TriggerCalls.OnDeath];
            DecayHiddenBloatfinger.conditions = [ScriptableObject.CreateInstance<IsntWitheringDeathCondition>()];
            DecayHiddenBloatfinger.doesPassiveTriggerInformationPanel = true;

            Enemy bloatfingerhiddenorpheum = new Enemy("Sculpture?", "BloatfingerHiddenOrpheum_EN")
            {
                Health = 8,
                HealthColor = Pigments.Grey,
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("SculptureTimeline", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("BloatfingerDead", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("SculptureTimeline", new Vector2(0.5f, 0f), 32),
                DamageSound = LoadedAssetsHandler.GetCharacter("Gospel_CH").damageSound,
                DeathSound = LoadedAssetsHandler.GetCharacter("Gospel_CH").deathSound,
            };
            bloatfingerhiddenorpheum.PrepareEnemyPrefab("Assets/Apocrypha_Enemies/SculptorBird_Enemy/Sculpture_Enemy.prefab", AApocrypha.assetBundle, null);
            bloatfingerhiddenorpheum.AddPassives([Passives.Inanimate, Passives.Anchored, Passives.Infantile, Passives.Withering, DecayHiddenBloatfinger]);
            /*
            bloatfingerhiddenorpheum.AddEnemyAbilities(
                [
                    wait,
                    crumble,
                ]);
            */
            bloatfingerhiddenorpheum.AddEnemy(true, false, false);
        }

        static PreviousEffectCondition PreviousGenerator(bool wasTrue, int number)
        {
            PreviousEffectCondition previous = ScriptableObject.CreateInstance<PreviousEffectCondition>();
            previous.wasSuccessful = wasTrue;
            previous.previousAmount = number;
            return previous;
        }

        // from the Salt Enemies github (ForgetEffects.cs)
        public class IsntWitheringDeathCondition : EffectorConditionSO
        {
            public override bool MeetCondition(IEffectorChecks effector, object args)
            {
                if (args is DeathReference reffe && reffe.witheringDeath == false) return true;
                return false;
            }
        }
    }
}
