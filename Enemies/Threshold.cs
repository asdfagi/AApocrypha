using System;
using System.Collections.Generic;
using System.Text;
using A_Apocrypha.CustomOther;

namespace A_Apocrypha.Enemies
{
    public class Threshold
    {
        public static void Add()
        {
            Enemy threshold = new Enemy("The Threshold", "Threshold_EN")
            {
                Health = 150,
                HealthColor = Pigments.Purple,
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("ThresholdTimeline", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("AnomalyDead", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("AnomalyMinibossTimeline", new Vector2(0.5f, 0f), 32),
                DamageSound = "event:/AAEnemy/Threshold/ThresholdHurt",
                DeathSound = "event:/AAEnemy/Threshold/ThresholdDeath",
                UnitTypes = ["Anomaly"],
                AbilitySelector = ScriptableObject.CreateInstance<AbilitySelector_Threshold>(),
            };
            threshold.PrepareEnemyPrefab("Assets/Apocrypha_Enemies/Threshold_Enemy/ThresholdMain_Enemy.prefab", AApocrypha.assetBundle, AApocrypha.assetBundle.LoadAsset<GameObject>("Assets/Apocrypha_Enemies/Threshold_Enemy/Threshold_Giblets.prefab").GetComponent<ParticleSystem>());

            Enemy thresholdgate = new Enemy("Ominous Gateway", "ThresholdGate_EN")
            {
                Health = 10,
                HealthColor = Pigments.Grey,
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("ThresholdGateTimeline", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("ThresholdGateDead", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("ThresholdGateTimeline", new Vector2(0.5f, 0f), 32),
                DamageSound = LoadedAssetsHandler.GetCharacter("Gospel_CH").damageSound,
                DeathSound = LoadedAssetsHandler.GetCharacter("Gospel_CH").deathSound,
                UnitTypes = ["Anomaly"],
            };
            thresholdgate.PrepareEnemyPrefab("Assets/Apocrypha_Enemies/Threshold_Enemy/ThresholdGate_Enemy.prefab", AApocrypha.assetBundle, null);//AApocrypha.assetBundle.LoadAsset<GameObject>("Assets/Apocrypha_Enemies/Anomaly_Enemy/Anomaly_Giblets.prefab").GetComponent<ParticleSystem>());

            Enemy partedveil = new Enemy("Parted Veil", "ThresholdMinion_EN")
            {
                Health = 10,
                HealthColor = Pigments.Purple,
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("AnomalyTimeline", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("AnomalyDead", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("AnomalyTimeline", new Vector2(0.5f, 0f), 32),
                DamageSound = "event:/AAEnemy/Anomaly1Hurt",
                DeathSound = "event:/AAEnemy/Threshold/VeilDeath",
                UnitTypes = ["Anomaly"],
            };
            partedveil.PrepareEnemyPrefab("Assets/Apocrypha_Enemies/Threshold_Enemy/Threshold_Enemy.prefab", AApocrypha.assetBundle, AApocrypha.assetBundle.LoadAsset<GameObject>("Assets/Apocrypha_Enemies/Threshold_Enemy/Threshold_Giblets.prefab").GetComponent<ParticleSystem>());

            AnomalyFreeMusicHandlerEffect MusicToggleOn = ScriptableObject.CreateInstance<AnomalyFreeMusicHandlerEffect>();
            MusicToggleOn.Add = true;

            threshold.CombatEnterEffects = [Effects.GenerateEffect(MusicToggleOn)];

            AnomalyFreeMusicHandlerEffect MusicToggleReset = ScriptableObject.CreateInstance<AnomalyFreeMusicHandlerEffect>();
            MusicToggleReset.ResetEffect = true;

            thresholdgate.CombatEnterEffects = [Effects.GenerateEffect(MusicToggleReset)];

            ExtraLootEffect Treasure = ScriptableObject.CreateInstance<ExtraLootEffect>();
            Treasure._isTreasure = true;
            Treasure._getLocked = true;

            threshold.CombatExitEffects = [Effects.GenerateEffect(Treasure, 2)];

            ChangeMaxHealthEffect PercentageReduceMaxHealth = ScriptableObject.CreateInstance<ChangeMaxHealthEffect>();
            PercentageReduceMaxHealth._entryAsPercentage = true;
            PercentageReduceMaxHealth._increase = false;

            SpawnEnemyAnywhereEffect OpenFurther = ScriptableObject.CreateInstance<SpawnEnemyAnywhereEffect>();
            OpenFurther.enemy = partedveil.enemy;
            OpenFurther._spawnTypeID = CombatType_GameIDs.Spawn_Basic.ToString();

            ConsumeRandomButCasterHealthManaEffect ConsumeNotHealth = ScriptableObject.CreateInstance<ConsumeRandomButCasterHealthManaEffect>();

            ConsumeCasterColorManaEffect ConsumeHealth = ScriptableObject.CreateInstance<ConsumeCasterColorManaEffect>();

            PerformEffectViaSubaction DelayedConsumeTheHealth = ScriptableObject.CreateInstance<PerformEffectViaSubaction>();
            DelayedConsumeTheHealth.effects = [Effects.GenerateEffect(ConsumeHealth, 5, Targeting.Slot_SelfSlot)];

            Ability parttheskin = new Ability("Part The Skin", "AApocrypha_PartTheSkin_A")
            {
                Description = "Spawn as many Parted Veils as possible.\n\"THE SKIN OF THE WORLD PARTS TO REVEAL THE TRUTH\"",
                Cost = [],
                Effects = 
                [
                    Effects.GenerateEffect(OpenFurther, 4, Targeting.Slot_SelfSlot),
                ],
                Rarity = Rarity.ImpossibleNoReroll,
                Priority = Priority.VerySlow,
            };
            parttheskin.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Other_Spawn), nameof(IntentType_GameIDs.Other_Spawn), nameof(IntentType_GameIDs.Other_Spawn), nameof(IntentType_GameIDs.Other_Spawn)]);

            ExtraAbilityInfo partskinextra = new()
            {
                ability = parttheskin.GenerateEnemyAbility().ability,
                rarity = Rarity.Impossible,
            };

            SpecificEnemiesTargeting VeilTargeting = ScriptableObject.CreateInstance<SpecificEnemiesTargeting>();
            VeilTargeting._enemies = ["Threshold_EN", "ThresholdMinion_EN"];
            VeilTargeting.slotOffsets = [0];
            VeilTargeting.targetUnitAllySlots = true;

            SpecificEnemiesTargeting OpposingVeilTargeting = ScriptableObject.CreateInstance<SpecificEnemiesTargeting>();
            OpposingVeilTargeting._enemies = ["Threshold_EN", "ThresholdMinion_EN"];
            OpposingVeilTargeting.slotOffsets = [0];
            OpposingVeilTargeting.targetUnitAllySlots = false;

            SpecificEnemiesTargeting VeilOnlyTargeting = ScriptableObject.CreateInstance<SpecificEnemiesTargeting>();
            VeilOnlyTargeting._enemies = ["ThresholdMinion_EN"];
            VeilOnlyTargeting.slotOffsets = [0];
            VeilOnlyTargeting.targetUnitAllySlots = true;

            SpecificEnemiesByHealthTargeting LowestHealthVeilOnlyTargeting = ScriptableObject.CreateInstance<SpecificEnemiesByHealthTargeting>();
            LowestHealthVeilOnlyTargeting._enemies = ["ThresholdMinion_EN"];
            LowestHealthVeilOnlyTargeting.slotOffsets = [0];
            LowestHealthVeilOnlyTargeting.targetUnitAllySlots = true;
            LowestHealthVeilOnlyTargeting._lowest = true;
            LowestHealthVeilOnlyTargeting._ignoreFullHealth = true;

            SpecificEnemiesByHealthTargeting OpposingLowestHealthVeilOnlyTargeting = ScriptableObject.CreateInstance<SpecificEnemiesByHealthTargeting>();
            OpposingLowestHealthVeilOnlyTargeting._enemies = ["ThresholdMinion_EN"];
            OpposingLowestHealthVeilOnlyTargeting.slotOffsets = [0];
            OpposingLowestHealthVeilOnlyTargeting.targetUnitAllySlots = false;
            OpposingLowestHealthVeilOnlyTargeting._lowest = true;
            OpposingLowestHealthVeilOnlyTargeting._ignoreFullHealth = true;

            PerformRandomAbilityFromTargetCharacterEffect RandomAbility = ScriptableObject.CreateInstance<PerformRandomAbilityFromTargetCharacterEffect>();
            RandomAbility._abilityBlacklist = ["Slap"];

            Ability reflectthetruth = new Ability("Reflect The Truth", "AApocrypha_ReflectTheTruth_A")
            {
                Description = "Make this enemy perform one of the Opposing party member's abilities, excluding \"Slap\" but otherwise chosen randomly.\nConsume up to 5 pigment of this enemy's health color.\n\"THE TRUTH REFLECTS YOU YOU NEED ONLY BEAR WITNESS\"",
                Cost = [],
                Visuals = CustomVisuals.GazeVisualsSO,
                AnimationTarget = Targeting.Slot_Front,
                Effects =
                [
                    Effects.GenerateEffect(RandomAbility, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(DelayedConsumeTheHealth),
                ],
                Rarity = Rarity.Rare,
                Priority = Priority.Slow,
            };
            reflectthetruth.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Misc_Additional)]);
            reflectthetruth.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Misc)]);
            reflectthetruth.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Mana_Consume)]);

            DamageEffect DamageByPrevious = ScriptableObject.CreateInstance<DamageEffect>();
            DamageByPrevious._usePreviousExitValue = true;

            Ability consumetheworld = new Ability("Consume The World", "AApocrypha_ConsumeTheWorld_A")
            {
                Description = "Consume up to 5 pigment not of this enemy's health color, then deal damage equal to twice the amount of pigment consumed to the Far Left and Far Right party members.\nConsume up to 5 pigment of this enemy's health color.\n\"THIS WORLD IS A LIE AND I SHALL MAKE IT TRUTH\"",
                Cost = [],
                Effects =
                [
                    Effects.GenerateEffect(ConsumeNotHealth, 5, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(DamageByPrevious, 2, Targeting.Slot_OpponentFarSides),
                    Effects.GenerateEffect(DelayedConsumeTheHealth),
                ],
                Rarity = Rarity.Uncommon,
                Priority = Priority.Slow,
            };
            consumetheworld.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Mana_Consume)]);
            consumetheworld.AddIntentsToTarget(Targeting.Slot_OpponentFarSides, [nameof(IntentType_GameIDs.Damage_7_10)]);
            consumetheworld.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Mana_Consume)]);

            Ability punishtheblind = new Ability("Punish The Unenlightened", "AApocrypha_PunishTheBlind_A")
            {
                Description = "Deal a Painful amount of damage to all party members Opposing the Parted Veil(s) with the lowest current health.\nConsume up to 5 pigment of this enemy's health color.\n\"THOSE THAT SEEK TO DESTROY THE TRUTH MUST BE PUNISHED\"",
                Cost = [],
                Visuals = Visuals.Mitosis,
                AnimationTarget = OpposingLowestHealthVeilOnlyTargeting,
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 6, OpposingLowestHealthVeilOnlyTargeting),
                    Effects.GenerateEffect(DelayedConsumeTheHealth),
                ],
                Rarity = Rarity.Uncommon,
                Priority = Priority.Slow,
            };
            punishtheblind.AddIntentsToTarget(LowestHealthVeilOnlyTargeting, [nameof(IntentType_GameIDs.Misc)]);
            punishtheblind.AddIntentsToTarget(OpposingLowestHealthVeilOnlyTargeting, [nameof(IntentType_GameIDs.Damage_3_6)]);
            punishtheblind.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Mana_Consume)]);

            SpawnEnemyInSpecificSlotEffect SummonIt = ScriptableObject.CreateInstance<SpawnEnemyInSpecificSlotEffect>();
            SummonIt.enemy = threshold.enemy;
            SummonIt.spawnSlot = 2;
            SummonIt.givesExperience = true;
            SummonIt._spawnTypeID = CombatType_GameIDs.Spawn_Basic.ToString();

            PerformEffectViaSubaction SummonDelay = ScriptableObject.CreateInstance<PerformEffectViaSubaction>();
            SummonDelay.effects = [
                Effects.GenerateEffect(SummonIt, 1, Targeting.Slot_SelfSlot),
                Effects.GenerateEffect(OpenFurther, 4, Targeting.Slot_SelfSlot),
            ];

            SwapToOneRandomSideXTimesEffect SwapRandomFar = ScriptableObject.CreateInstance<SwapToOneRandomSideXTimesEffect>();

            SwapToOneSideEffect SwapLeft = ScriptableObject.CreateInstance<SwapToOneSideEffect>();
            SwapLeft._swapRight = false;

            SwapToOneSideEffect SwapRight = ScriptableObject.CreateInstance<SwapToOneSideEffect>();
            SwapRight._swapRight = true;

            Ability veilleft2 = new Ability("Ventral Current", "AApocrypha_ThresholdMinionLeft2_A")
            {
                Description = "Move this enemy to the Left twice.",
                Cost = [Pigments.Purple],
                Effects =
                [
                    Effects.GenerateEffect(SwapLeft, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(SwapLeft, 1, Targeting.Slot_SelfSlot),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.Fast,
            };
            veilleft2.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Swap_Left), nameof(IntentType_GameIDs.Swap_Left)]);

            Ability veilright2 = new Ability("Dorsal Current", "AApocrypha_ThresholdMinionRight2_A")
            {
                Description = "Move this enemy to the Right twice.",
                Cost = [Pigments.Purple],
                Effects =
                [
                    Effects.GenerateEffect(SwapRight, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(SwapRight, 1, Targeting.Slot_SelfSlot),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.Fast,
            };
            veilright2.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Swap_Right), nameof(IntentType_GameIDs.Swap_Right)]);

            Ability veilleft4 = new Ability("Sinistral Current", "AApocrypha_ThresholdMinionLeft4_A")
            {
                Description = "Move this enemy as far Left as possible.",
                Cost = [Pigments.Purple],
                Effects =
                [
                    Effects.GenerateEffect(SwapLeft, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(SwapLeft, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(SwapLeft, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(SwapLeft, 1, Targeting.Slot_SelfSlot),
                ],
                Rarity = Rarity.Uncommon,
                Priority = Priority.Fast,
            };
            veilleft4.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Swap_Left), nameof(IntentType_GameIDs.Swap_Left), nameof(IntentType_GameIDs.Swap_Left), nameof(IntentType_GameIDs.Swap_Left)]);

            Ability veilright4 = new Ability("Dextral Current", "AApocrypha_ThresholdMinionRight4_A")
            {
                Description = "Move this enemy as far Right as possible.",
                Cost = [Pigments.Purple],
                Effects =
                [
                    Effects.GenerateEffect(SwapRight, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(SwapRight, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(SwapRight, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(SwapRight, 1, Targeting.Slot_SelfSlot),
                ],
                Rarity = Rarity.Uncommon,
                Priority = Priority.Fast,
            };
            veilright4.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Swap_Right), nameof(IntentType_GameIDs.Swap_Right), nameof(IntentType_GameIDs.Swap_Right), nameof(IntentType_GameIDs.Swap_Right)]);

            Ability veilrandom4 = new Ability("Spiralling Current", "AApocrypha_ThresholdMinionRandom4_A")
            {
                Description = "Move this enemy to the Left or Right.",
                Cost = [Pigments.Purple],
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<SwapToSidesEffect>(), 1, Targeting.Slot_SelfSlot),
                ],
                Rarity = Rarity.Rare,
                Priority = Priority.Fast,
            };
            veilrandom4.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Swap_Left), nameof(IntentType_GameIDs.Swap_Right)]);

            Ability opentheway = new Ability("Open The Way", "AApocrypha_ThresholdSummon_A")
            {
                Description = "Puncture the skin of the world and reveal the Threshold.\n\"WITNESS REALITY\"",
                Cost = [],
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DirectDeathEffect>(), 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(SummonDelay, 1, Targeting.Slot_SelfSlot),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.ExtremelySlow,
            };
            opentheway.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Damage_Death), nameof(IntentType_GameIDs.Other_Spawn), nameof(IntentType_GameIDs.Other_Spawn), nameof(IntentType_GameIDs.Other_Spawn), nameof(IntentType_GameIDs.Other_Spawn), nameof(IntentType_GameIDs.Other_Spawn)]);

            threshold.AddEnemyAbilities([
                reflectthetruth.GenerateEnemyAbility(true),
                consumetheworld.GenerateEnemyAbility(true),
                punishtheblind.GenerateEnemyAbility(true),
            ]);

            partedveil.AddEnemyAbilities([
                veilleft4.GenerateEnemyAbility(true),
                veilleft2.GenerateEnemyAbility(true),
                veilrandom4.GenerateEnemyAbility(true),
                veilright2.GenerateEnemyAbility(true),
                veilright4.GenerateEnemyAbility(true),
            ]);

            thresholdgate.AddEnemyAbilities([
                opentheway.GenerateEnemyAbility(true),
            ]);

            threshold.AddPassives([Passives.Pure, Passives.BonusAttackGenerator(partskinextra)]);
            thresholdgate.AddPassives([Passives.Inanimate, Passives.Anchored]);
            partedveil.AddPassives([Passives.Pure, Passives.GetCustomPassive("AA_CondensePrimaryLessPurple_PA"), Passives.Withering]);

            threshold.AddEnemy(false, false, false);
            thresholdgate.AddEnemy(false, false, false);
            partedveil.AddEnemy(false, false, false);
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
