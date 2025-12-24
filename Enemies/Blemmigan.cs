using System;
using System.Collections.Generic;
using System.Text;
using A_Apocrypha.CustomOther;
using BrutalAPI;

namespace A_Apocrypha.Enemies
{
    public class Blemmigan
    {
        public static void Add()
        {
            ChangeCasterHealthColorBetweenColorsEffect BluePurple = ScriptableObject.CreateInstance<ChangeCasterHealthColorBetweenColorsEffect>();
            BluePurple._color1 = Pigments.Blue;
            BluePurple._color2 = Pigments.Purple;

            PerformEffectPassiveAbility TwoFacedBP = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
            TwoFacedBP.name = "AA_TwoFaced_BP";
            TwoFacedBP._passiveName = "Two Faced";
            TwoFacedBP.m_PassiveID = Passives.TwoFaced.m_PassiveID;
            TwoFacedBP.passiveIcon = ResourceLoader.LoadSprite("2facedBP_passive");
            TwoFacedBP._characterDescription = "Upon receiving direct damage this party member will change its health colour from blue to purple or vice versa.";
            TwoFacedBP._enemyDescription = "Upon receiving direct damage this enemy will change its health colour from blue to purple or vice versa.";
            TwoFacedBP._triggerOn = [TriggerCalls.OnDirectDamaged];
            TwoFacedBP.effects = [Effects.GenerateEffect(BluePurple, 1, Targeting.Slot_SelfSlot)];
            Passives.AddCustomPassiveToPool("AA_TwoFacedBP_PA", "Two Faced", TwoFacedBP);

            Enemy blemmigan = new Enemy("Blemmigan", "Blemmigan_EN")
            {
                Health = 8,
                HealthColor = Pigments.Blue,
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("BlemmiganTimeline", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("BlemmiganDead", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("BlemmiganTimeline", new Vector2(0.5f, 0f), 32),
                DamageSound = LoadedAssetsHandler.GetEnemy("SilverSuckle_EN").damageSound,
                DeathSound = LoadedAssetsHandler.GetEnemy("SilverSuckle_EN").deathSound,
                UnitTypes = ["Neathy"],
            };
            blemmigan.PrepareEnemyPrefab("Assets/Apocrypha_Enemies/Blemmigan_Enemy/Blemmigan_Enemy.prefab", AApocrypha.assetBundle, AApocrypha.assetBundle.LoadAsset<GameObject>("Assets/Apocrypha_Enemies/Blemmigan_Enemy/Blemmigan_Giblets.prefab").GetComponent<ParticleSystem>());
            blemmigan.AddPassives([Passives.GetCustomPassive("AA_TwoFacedBP_PA"), Passives.Slippery, Passives.Withering]);

            SpawnEnemyAnywhereEffect SpawnBlemmigan = ScriptableObject.CreateInstance<SpawnEnemyAnywhereEffect>();
            SpawnBlemmigan.enemy = blemmigan.enemy;
            SpawnBlemmigan._spawnTypeID = CombatType_GameIDs.Spawn_Basic.ToString();

            SwapToSidesEffect SwapEither = ScriptableObject.CreateInstance<SwapToSidesEffect>();

            DamageEffect DamageKillTrue = ScriptableObject.CreateInstance<DamageEffect>();
            DamageKillTrue._returnKillAsSuccess = true;

            FieldEffect_Apply_Effect ApplyShield = ScriptableObject.CreateInstance<FieldEffect_Apply_Effect>();
            ApplyShield._Field = StatusField.Shield;

            QueueTimelineAbilityByNameEffect QueuePeck = ScriptableObject.CreateInstance<QueueTimelineAbilityByNameEffect>();
            QueuePeck._abilityName = "Peck";

            SpecificEnemiesTargeting AllBlemmigans = ScriptableObject.CreateInstance<SpecificEnemiesTargeting>();
            AllBlemmigans._enemies = ["Blemmigan_EN"];
            AllBlemmigans.targetUnitAllySlots = true;
            AllBlemmigans.slotOffsets = [0];

            SpecificEnemiesOneSideTargeting AllBlemmigansL = ScriptableObject.CreateInstance<SpecificEnemiesOneSideTargeting>();
            AllBlemmigansL._enemies = ["Blemmigan_EN"];
            AllBlemmigansL.targetUnitAllySlots = true;
            AllBlemmigansL.slotOffsets = [0];
            AllBlemmigansL._right = false;

            SpecificEnemiesOneSideTargeting AllBlemmigansR = ScriptableObject.CreateInstance<SpecificEnemiesOneSideTargeting>();
            AllBlemmigansR._enemies = ["Blemmigan_EN"];
            AllBlemmigansR.targetUnitAllySlots = true;
            AllBlemmigansR.slotOffsets = [0];
            AllBlemmigansR._right = true;

            SpecificEnemiesOneSideTargeting AllBlemmigansL2 = ScriptableObject.CreateInstance<SpecificEnemiesOneSideTargeting>();
            AllBlemmigansL2._enemies = ["Blemmigan_EN"];
            AllBlemmigansL2.targetUnitAllySlots = true;
            AllBlemmigansL2.slotOffsets = [1];
            AllBlemmigansL2._right = false;

            SpecificEnemiesOneSideTargeting AllBlemmigansR2 = ScriptableObject.CreateInstance<SpecificEnemiesOneSideTargeting>();
            AllBlemmigansR2._enemies = ["Blemmigan_EN"];
            AllBlemmigansR2.targetUnitAllySlots = true;
            AllBlemmigansR2.slotOffsets = [-1];
            AllBlemmigansR2._right = true;

            SpecificEnemiesTargeting OpposingAllBlemmigans = ScriptableObject.CreateInstance<SpecificEnemiesTargeting>();
            OpposingAllBlemmigans._enemies = ["Blemmigan_EN"];
            OpposingAllBlemmigans.targetUnitAllySlots = false;
            OpposingAllBlemmigans.slotOffsets = [0];

            RandomTargetPerformEffectViaSubaction QueueABlemmigan = ScriptableObject.CreateInstance<RandomTargetPerformEffectViaSubaction>();
            QueueABlemmigan.effects =
            [
                Effects.GenerateEffect(QueuePeck, 1, Targeting.Slot_SelfSlot),
            ];

            TargetPerformEffectViaSubaction ProtectMeR = ScriptableObject.CreateInstance<TargetPerformEffectViaSubaction>();
            ProtectMeR.effects =
            [
                Effects.GenerateEffect(ScriptableObject.CreateInstance<CheckIsAliveEffect>(), 1, Targeting.Slot_SelfSlot),
                Effects.GenerateEffect(ApplyShield, 3, Targeting.Slot_AllyRight, PreviousGenerator(true, 1)),
            ];

            TargetPerformEffectViaSubaction ProtectMeL = ScriptableObject.CreateInstance<TargetPerformEffectViaSubaction>();
            ProtectMeL.effects =
            [
                Effects.GenerateEffect(ScriptableObject.CreateInstance<CheckIsAliveEffect>(), 1, Targeting.Slot_SelfSlot),
                Effects.GenerateEffect(ApplyShield, 3, Targeting.Slot_AllyLeft, PreviousGenerator(true, 1)),
            ];

            AnimationVisualsEffect PeckAnim = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            PeckAnim._visuals = Visuals.Nibble;
            PeckAnim._animationTarget = Targeting.Slot_Front;

            TargetPerformEffectViaSubaction AvengeMe = ScriptableObject.CreateInstance<TargetPerformEffectViaSubaction>();
            AvengeMe.effects =
            [
                Effects.GenerateEffect(ScriptableObject.CreateInstance<CheckIsAliveEffect>(), 1, Targeting.Slot_SelfSlot),
                Effects.GenerateEffect(PeckAnim, 1, Targeting.Slot_Front, PreviousGenerator(true, 1)),
                Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 3, Targeting.Slot_Front, PreviousGenerator(true, 2)),
            ];

            CheckHasUnitWithIDsEffect BlemmiganCheck = ScriptableObject.CreateInstance<CheckHasUnitWithIDsEffect>();
            BlemmiganCheck._ids = ["Blemmigan_EN"];

            StatusEffect_Apply_Effect RupturedApply = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            RupturedApply._Status = StatusField.Ruptured;

            StatusEffect_Apply_Effect RandomPreviousPoisonedApply = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            RandomPreviousPoisonedApply._Status = StatusField.GetCustomStatusEffect("Poisoned_ID");
            RandomPreviousPoisonedApply._RandomBetweenPrevious = true;

            SwapToOneSideEffect SwapLeft = ScriptableObject.CreateInstance<SwapToOneSideEffect>();
            SwapLeft._swapRight = false;

            SwapToOneSideEffect SwapRight = ScriptableObject.CreateInstance<SwapToOneSideEffect>();
            SwapRight._swapRight = true;

            AnimationVisualsEffect StankAnim = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            StankAnim._visuals = ITAVisuals.Stank;
            StankAnim._animationTarget = Targeting.Slot_Front;

            ConsumeCasterColorManaEffect ConsumePurple = ScriptableObject.CreateInstance<ConsumeCasterColorManaEffect>();

            ConsumeRandomButCasterHealthManaEffect ConsumeNotHealth = ScriptableObject.CreateInstance<ConsumeRandomButCasterHealthManaEffect>();

            ChangeToRandomHealthColorEffect YoureRedNow = ScriptableObject.CreateInstance<ChangeToRandomHealthColorEffect>();
            YoureRedNow._healthColors = [Pigments.Red];

            Ability peck = new Ability("Peck", "AApocrypha_BlemmiganPeck_A")
            {
                Description = "Deal a Barely Painful amount of damage to the Opposing party member.\nIf damage is dealt, move Left or Right.",
                Cost = [Pigments.BluePurple],
                Visuals = Visuals.Nibble,
                AnimationTarget = Targeting.Slot_Front,
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 3, Targeting.Slot_Front),
                    Effects.GenerateEffect(SwapEither, 1, Targeting.Slot_SelfSlot, PreviousGenerator(true, 1)),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.Normal,
            };
            peck.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Damage_3_6)]);
            peck.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Swap_Sides)]);

            Ability whistle = new Ability("Fluting Whistle", "AApocrypha_BlemmiganWhistle_A")
            {
                Description = "Apply 1 Ruptured to the Opposing party member.\nMove Left or Right.",
                Cost = [Pigments.BluePurple],
                Visuals = Visuals.Scream,
                AnimationTarget = Targeting.Slot_Front,
                Effects =
                [
                    Effects.GenerateEffect(RupturedApply, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(SwapEither, 1, Targeting.Slot_SelfSlot),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.Normal,
            };
            whistle.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Status_Ruptured)]);
            whistle.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Swap_Sides)]);

            Ability propagate = new Ability("Propagate", "AApocrypha_BlemmiganPropagate_A")
            {
                Description = "If there is space on the field, spawn a Blemmigan.\nOtherwise, apply 2-4 Poisoned to the Opposing party member.",
                Cost = [Pigments.BluePurple],
                Visuals = ITAVisuals.Stank,
                AnimationTarget = Targeting.Slot_SelfSlot,
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<CheckHasUnitEffect>(), 1, Targeting.GenerateGenericTarget([0], true)),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<CheckHasUnitEffect>(), 1, Targeting.GenerateGenericTarget([1], true)),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<CheckHasUnitEffect>(), 1, Targeting.GenerateGenericTarget([2], true)),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<CheckHasUnitEffect>(), 1, Targeting.GenerateGenericTarget([3], true)),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<CheckHasUnitEffect>(), 1, Targeting.GenerateGenericTarget([4], true)),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<ExtraVariableForNextEffect>(), 2, Targeting.Slot_SelfSlot, Effects.CheckMultiplePreviousEffectsCondition([true, true, true, true, true], [1, 2, 3, 4, 5])),
                    Effects.GenerateEffect(RandomPreviousPoisonedApply, 4, Targeting.Slot_Front, Effects.CheckMultiplePreviousEffectsCondition([true, true, true, true, true], [2, 3, 4, 5, 6])),
                    Effects.GenerateEffect(SpawnBlemmigan, 1, Targeting.Slot_SelfSlot),
                ],
                Rarity = Rarity.Uncommon,
                Priority = Priority.Slow,
            };
            propagate.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Other_Spawn)]);
            propagate.AddIntentsToTarget(Targeting.Slot_Front, ["Status_Poisoned"]);

            blemmigan.AddEnemyAbilities(
                [
                    peck.GenerateEnemyAbility(true),
                    whistle.GenerateEnemyAbility(true),
                    propagate.GenerateEnemyAbility(true),
                ]);
            blemmigan.AddEnemy(true, true, true);
            //LoadedAssetsHandler.GetEnemy("Blemmigan_EN").enemyTemplate = LoadedAssetsHandler.GetEnemy("SilverSuckle_EN").enemyTemplate;

            ReturnValueComparatorEffectorCondition TenOrMore = ScriptableObject.CreateInstance<ReturnValueComparatorEffectorCondition>();
            TenOrMore._lessThan = false;
            TenOrMore._comparator = 10;

            PerformEffectPassiveAbility rectifyUttershroom = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
            rectifyUttershroom.name = "AA_RectifyUttershroom_PA";
            rectifyUttershroom._passiveName = "Rectify (10)";
            rectifyUttershroom.m_PassiveID = "RectifyUttershroom";
            rectifyUttershroom.passiveIcon = ResourceLoader.LoadSprite("ChalicePassiveA");
            rectifyUttershroom._characterDescription = "mushrüm";
            rectifyUttershroom._enemyDescription = "On taking 10 or more damage, queue \"Peck\" on a random Blemmigan.";
            rectifyUttershroom.doesPassiveTriggerInformationPanel = true;
            rectifyUttershroom._triggerOn = [TriggerCalls.OnDirectDamaged];
            rectifyUttershroom.conditions = [TenOrMore];
            rectifyUttershroom.effects =
            [
                Effects.GenerateEffect(QueueABlemmigan, 1, AllBlemmigans),
            ];
            Passives.AddCustomPassiveToPool("AA_UttershroomPassive_PA", "Rectify (10)", rectifyUttershroom);

            Enemy uttershroom = new Enemy("Uttershroom Spore", "UttershroomSpore_EN")
            {
                Health = 50,
                HealthColor = Pigments.Blue,
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("UttershroomTimeline", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("UttershroomDead", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("UttershroomTimeline", new Vector2(0.5f, 0f), 32),
                DamageSound = LoadedAssetsHandler.GetEnemy("Sepulchre_EN").damageSound,
                DeathSound = LoadedAssetsHandler.GetEnemy("Sepulchre_EN").deathSound,
                UnitTypes = ["Neathy"],
            };
            uttershroom.PrepareEnemyPrefab("Assets/Apocrypha_Enemies/Blemmigan_Enemy/Uttershroom_Enemy.prefab", AApocrypha.assetBundle, AApocrypha.assetBundle.LoadAsset<GameObject>("Assets/Apocrypha_Enemies/Blemmigan_Enemy/Uttershroom_Giblets.prefab").GetComponent<ParticleSystem>());
            uttershroom.AddPassives([Passives.GetCustomPassive("AA_TwoFacedBP_PA"), Passives.GetCustomPassive("AA_UttershroomPassive_PA")]);

            Ability expelchildren = new Ability("Expel Children", "AApocrypha_BlemmiganChildren_A")
            {
                Description = "Change the health color of All Blemmigans to Red.\nSpawn a Blemmigan. If there were no Blemmigans in combat, spawn an additional Blemmigan.\nConsume 3 Pigment not of this enemy's health color.",
                Cost = [Pigments.Blue, Pigments.Purple],
                Visuals = Visuals.Exsanguinate,
                AnimationTarget = Targeting.Slot_SelfSlot,
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<CheckHasUnitEffect>(), 1, AllBlemmigans),
                    Effects.GenerateEffect(SpawnBlemmigan, 1, Targeting.Slot_SelfSlot, PreviousGenerator(false, 1)),
                    Effects.GenerateEffect(SpawnBlemmigan, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(YoureRedNow, 1, AllBlemmigans),
                    Effects.GenerateEffect(ConsumeNotHealth, 3, Targeting.Slot_SelfSlot),
                ],
                Rarity = Rarity.Uncommon,
                Priority = Priority.VerySlow,
            };
            expelchildren.AddIntentsToTarget(AllBlemmigans, [nameof(IntentType_GameIDs.Mana_Modify)]);
            expelchildren.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Other_Spawn), nameof(IntentType_GameIDs.Other_Spawn)]);
            expelchildren.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Mana_Consume)]);

            Ability protect = new Ability("Protect Mother", "AApocrypha_BlemmiganProtect_A")
            {
                Description = "Move All Blemmigans not adjacent to this enemy towards this enemy, then make All Blemmigans apply 3 Shield to their Left or Right, whichever side is closer to this enemy.\nIf there are no Blemmigans in combat, deal a Painful amount of damage to this enemy and summon a Blemmigan.",
                Cost = [Pigments.Blue, Pigments.Purple],
                Visuals = Visuals.Bellow,
                AnimationTarget = Targeting.Slot_SelfSlot,
                Effects =
                [
                    Effects.GenerateEffect(BlemmiganCheck, 1, Targeting.GenerateSlotTarget([-2], true)),
                    Effects.GenerateEffect(SwapRight, 1, Targeting.GenerateSlotTarget([-2], true), PreviousGenerator(true, 1)),
                    Effects.GenerateEffect(BlemmiganCheck, 1, Targeting.GenerateSlotTarget([-3], true)),
                    Effects.GenerateEffect(SwapRight, 1, Targeting.GenerateSlotTarget([-3], true), PreviousGenerator(true, 1)),
                    Effects.GenerateEffect(BlemmiganCheck, 1, Targeting.GenerateSlotTarget([-4], true)),
                    Effects.GenerateEffect(SwapRight, 1, Targeting.GenerateSlotTarget([-4], true), PreviousGenerator(true, 1)),
                    Effects.GenerateEffect(BlemmiganCheck, 1, Targeting.GenerateSlotTarget([2], true)),
                    Effects.GenerateEffect(SwapLeft, 1, Targeting.GenerateSlotTarget([2], true), PreviousGenerator(true, 1)),
                    Effects.GenerateEffect(BlemmiganCheck, 1, Targeting.GenerateSlotTarget([3], true)),
                    Effects.GenerateEffect(SwapLeft, 1, Targeting.GenerateSlotTarget([3], true), PreviousGenerator(true, 1)),
                    Effects.GenerateEffect(BlemmiganCheck, 1, Targeting.GenerateSlotTarget([4], true)),
                    Effects.GenerateEffect(SwapLeft, 1, Targeting.GenerateSlotTarget([4], true), PreviousGenerator(true, 1)),
                    Effects.GenerateEffect(ProtectMeL, 1, AllBlemmigansR),
                    Effects.GenerateEffect(ProtectMeR, 1, AllBlemmigansL),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<CheckHasUnitEffect>(), 1, AllBlemmigans),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 6, Targeting.Slot_SelfSlot, PreviousGenerator(false, 1)),
                    Effects.GenerateEffect(SpawnBlemmigan, 1, Targeting.Slot_SelfSlot, PreviousGenerator(false, 2)),
                ],
                Rarity = Rarity.Uncommon,
                Priority = Priority.VerySlow,
            };
            protect.AddIntentsToTarget(Targeting.Slot_AlliesAllLefts, [nameof(IntentType_GameIDs.Swap_Right)]);
            protect.AddIntentsToTarget(Targeting.Slot_AlliesAllRights, [nameof(IntentType_GameIDs.Swap_Left)]);
            protect.AddIntentsToTarget(AllBlemmigansL2, [nameof(IntentType_GameIDs.Field_Shield)]);
            protect.AddIntentsToTarget(AllBlemmigansR2, [nameof(IntentType_GameIDs.Field_Shield)]);
            protect.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Damage_3_6), nameof(IntentType_GameIDs.Other_Spawn)]);

            Ability avenge = new Ability("Avenge Mother", "AApocrypha_BlemmiganAvenge_A")
            {
                Description = "Move All Blemmigans away from this enemy, then make All Blemmigans deal a Barely Painful amount of damage to their Opposing party member.\nIf there are no Blemmigans in combat, deal a Painful amount of damage to this enemy and summon a Blemmigan.",
                Cost = [Pigments.Blue, Pigments.Purple],
                Visuals = Visuals.Bellow,
                AnimationTarget = Targeting.Slot_SelfSlot,
                Effects =
                [
                    Effects.GenerateEffect(BlemmiganCheck, 1, Targeting.GenerateSlotTarget([-4], true)),
                    Effects.GenerateEffect(SwapLeft, 1, Targeting.GenerateSlotTarget([-4], true), PreviousGenerator(true, 1)),
                    Effects.GenerateEffect(BlemmiganCheck, 1, Targeting.GenerateSlotTarget([-3], true)),
                    Effects.GenerateEffect(SwapLeft, 1, Targeting.GenerateSlotTarget([-3], true), PreviousGenerator(true, 1)),
                    Effects.GenerateEffect(BlemmiganCheck, 1, Targeting.GenerateSlotTarget([-2], true)),
                    Effects.GenerateEffect(SwapLeft, 1, Targeting.GenerateSlotTarget([-2], true), PreviousGenerator(true, 1)),
                    Effects.GenerateEffect(BlemmiganCheck, 1, Targeting.GenerateSlotTarget([-1], true)),
                    Effects.GenerateEffect(SwapLeft, 1, Targeting.GenerateSlotTarget([-1], true), PreviousGenerator(true, 1)),
                    Effects.GenerateEffect(BlemmiganCheck, 1, Targeting.GenerateSlotTarget([4], true)),
                    Effects.GenerateEffect(SwapRight, 1, Targeting.GenerateSlotTarget([4], true), PreviousGenerator(true, 1)),
                    Effects.GenerateEffect(BlemmiganCheck, 1, Targeting.GenerateSlotTarget([3], true)),
                    Effects.GenerateEffect(SwapRight, 1, Targeting.GenerateSlotTarget([3], true), PreviousGenerator(true, 1)),
                    Effects.GenerateEffect(BlemmiganCheck, 1, Targeting.GenerateSlotTarget([2], true)),
                    Effects.GenerateEffect(SwapRight, 1, Targeting.GenerateSlotTarget([2], true), PreviousGenerator(true, 1)),
                    Effects.GenerateEffect(BlemmiganCheck, 1, Targeting.GenerateSlotTarget([1], true)),
                    Effects.GenerateEffect(SwapRight, 1, Targeting.GenerateSlotTarget([1], true), PreviousGenerator(true, 1)),
                    Effects.GenerateEffect(AvengeMe, 1, AllBlemmigans),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<CheckHasUnitEffect>(), 1, AllBlemmigans),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 6, Targeting.Slot_SelfSlot, PreviousGenerator(false, 1)),
                    Effects.GenerateEffect(SpawnBlemmigan, 1, Targeting.Slot_SelfSlot, PreviousGenerator(false, 2)),
                ],
                Rarity = Rarity.Uncommon,
                Priority = Priority.VeryFast,
            };
            avenge.AddIntentsToTarget(Targeting.Slot_AlliesAllLefts, [nameof(IntentType_GameIDs.Swap_Left)]);
            avenge.AddIntentsToTarget(Targeting.Slot_AlliesAllRights, [nameof(IntentType_GameIDs.Swap_Right)]);
            avenge.AddIntentsToTarget(OpposingAllBlemmigans, [nameof(IntentType_GameIDs.Damage_3_6)]);
            avenge.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Damage_3_6), nameof(IntentType_GameIDs.Other_Spawn)]);

            uttershroom.AddEnemyAbilities(
                [
                    expelchildren.GenerateEnemyAbility(true),
                    protect.GenerateEnemyAbility(true),
                    avenge.GenerateEnemyAbility(true),
                ]);
            uttershroom.AddEnemy(true, true, false);
            //LoadedAssetsHandler.GetEnemy("UttershroomSpore_EN").enemyTemplate = LoadedAssetsHandler.GetEnemy("GildedGulper_EN").enemyTemplate;
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
