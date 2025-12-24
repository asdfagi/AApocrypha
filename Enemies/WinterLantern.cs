using System;
using System.Collections.Generic;
using System.Text;
using A_Apocrypha.CustomOther;
using UnityEngine.SocialPlatforms;

namespace A_Apocrypha.Enemies
{
    public class WinterLantern
    {
        public static void Add()
        {
            Enemy winterlantern = new Enemy("Winter Lantern", "WinterLantern_EN")
            {
                Health = 30,
                HealthColor = Pigments.Grey,
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("WinterLanternTimeline", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("WinterLanternDead", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("WinterLanternTimeline", new Vector2(0.5f, 0f), 32),
                DamageSound = LoadedAssetsHandler.GetCharacter("Gospel_CH").damageSound,
                DeathSound = LoadedAssetsHandler.GetCharacter("Gospel_CH").deathSound,
            };
            winterlantern.PrepareEnemyPrefab("Assets/Apocrypha_Enemies/WinterLantern_Enemy/WinterLantern_Enemy.prefab", AApocrypha.assetBundle, AApocrypha.assetBundle.LoadAsset<GameObject>("Assets/Apocrypha_Enemies/WinterLantern_Enemy/WinterLantern_Giblets.prefab").GetComponent<ParticleSystem>());

            SpecificAlliesByPassiveTargeting InanimateAllies = ScriptableObject.CreateInstance<SpecificAlliesByPassiveTargeting>();
            InanimateAllies._passive = Passives.Inanimate;
            InanimateAllies.targetUnitAllySlots = true;
            InanimateAllies.slotOffsets = [0];

            SpecificAlliesByPassiveTargeting OpposingInanimateAllies = ScriptableObject.CreateInstance<SpecificAlliesByPassiveTargeting>();
            OpposingInanimateAllies._passive = Passives.Inanimate;
            OpposingInanimateAllies.targetUnitAllySlots = false;
            OpposingInanimateAllies.slotOffsets = [0];

            AnimationVisualsEffect ContusionAnim = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            ContusionAnim._visuals = Visuals.Contusion;
            ContusionAnim._animationTarget = Targeting.Slot_Front;

            TargetPerformEffectViaSubaction HitOpposing = ScriptableObject.CreateInstance<TargetPerformEffectViaSubaction>();
            HitOpposing.effects = [
                Effects.GenerateEffect(ContusionAnim, 4, Targeting.Slot_Front),
                Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 4, Targeting.Slot_Front),
            ];

            DamageEffect IndirectDamage = ScriptableObject.CreateInstance<DamageEffect>();
            IndirectDamage._indirect = true;

            ChangeMaxHealthEffect MaxHealthIncrease = ScriptableObject.CreateInstance<ChangeMaxHealthEffect>();
            MaxHealthIncrease._increase = true;
            MaxHealthIncrease._entryAsPercentage = false;

            GenerateColorManaEffect GiveRedPigment = ScriptableObject.CreateInstance<GenerateColorManaEffect>();
            GiveRedPigment.mana = Pigments.Red;

            GenerateColorManaPerTargetEffect TargetsGiveRedPigment = ScriptableObject.CreateInstance<GenerateColorManaPerTargetEffect>();
            TargetsGiveRedPigment.mana = Pigments.Red;

            SwapToOneSideEffect SwapLeft = ScriptableObject.CreateInstance<SwapToOneSideEffect>();
            SwapLeft._swapRight = false;

            SwapToOneSideEffect SwapRight = ScriptableObject.CreateInstance<SwapToOneSideEffect>();
            SwapRight._swapRight = true;

            AnimationVisualsEffect FlayAnim = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            FlayAnim._visuals = Visuals.Flay;
            FlayAnim._animationTarget = Targeting.Slot_SelfSlot;

            EnemySO tooth = LoadedAssetsHandler.GetEnemy("PetrifiedTooth_EN").Clone();
            
            tooth.name = "WinterLanternTooth_EN";
            tooth._enemyName = "Petrified \"Tooth\"";

            SpawnEnemyAnywhereEffect ShedTooth = ScriptableObject.CreateInstance<SpawnEnemyAnywhereEffect>();
            ShedTooth.enemy = tooth;
            ShedTooth._spawnTypeID = CombatType_GameIDs.Spawn_Basic.ToString();

            CheckPassiveAbilityEffect IsInanimate = ScriptableObject.CreateInstance<CheckPassiveAbilityEffect>();
            IsInanimate.m_PassiveID = Passives.Inanimate.m_PassiveID;

            FieldEffect_Apply_Effect ApplyShield = ScriptableObject.CreateInstance<FieldEffect_Apply_Effect>();
            ApplyShield._Field = StatusField.Shield;

            //Pygmalion Abilities
            Ability pygmalionattackleft = new Ability("Scramble Left", "AApocrypha_PygmalionAttackLeft_A")
            {
                Description = "Deal a Barely Painful amount of damage to the Left party member. Move Left.",
                Cost = [Pigments.Grey],
                Visuals = Visuals.Clobber_Left,
                AnimationTarget = Targeting.Slot_OpponentLeft,
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 3, Targeting.Slot_OpponentLeft),
                    Effects.GenerateEffect(SwapLeft, 1, Targeting.Slot_SelfSlot),
                ],
                Rarity = Rarity.Uncommon,
                Priority = Priority.Normal,
            };

            pygmalionattackleft.AddIntentsToTarget(Targeting.Slot_OpponentLeft, [nameof(IntentType_GameIDs.Damage_3_6)]);
            pygmalionattackleft.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Swap_Left)]);

            Ability pygmalionattackright = new Ability("Tumble Right", "AApocrypha_PygmalionAttackRight_A")
            {
                Description = "Deal a Barely Painful amount of damage to the Right party member. Move Right.",
                Cost = [Pigments.Grey],
                Visuals = Visuals.Clobber_Right,
                AnimationTarget = Targeting.Slot_OpponentRight,
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 3, Targeting.Slot_OpponentRight),
                    Effects.GenerateEffect(SwapRight, 1, Targeting.Slot_SelfSlot),
                ],
                Rarity = Rarity.Uncommon,
                Priority = Priority.Normal,
            };
            pygmalionattackright.AddIntentsToTarget(Targeting.Slot_OpponentRight, [nameof(IntentType_GameIDs.Damage_3_6)]);
            pygmalionattackright.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Swap_Right)]);

            Ability pygmalionattackfront = new Ability("Strike and Scatter", "AApocrypha_PygmalionAttackFront_A")
            {
                Description = "Deal a Painful amount of damage to the Opposing party member. Move Left or Right.",
                Cost = [Pigments.Grey],
                Visuals = Visuals.Contusion,
                AnimationTarget = Targeting.Slot_Front,
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 5, Targeting.Slot_Front),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<SwapToSidesEffect>(), 1, Targeting.Slot_SelfSlot),
                ],
                Rarity = Rarity.Uncommon,
                Priority = Priority.Normal,
            };
            pygmalionattackfront.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Damage_3_6)]);
            pygmalionattackfront.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Swap_Sides)]);

            Ability pygmalionpanic = new Ability("Panic", "AApocrypha_PygmalionPanic_A")
            {
                Description = "Move to the Left or Right three times. \"Danger! Run!\"",
                Cost = [Pigments.Grey],
                Visuals = Visuals.Scream,
                AnimationTarget = Targeting.Slot_SelfSlot,
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<SwapToSidesEffect>(), 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<SwapToSidesEffect>(), 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<SwapToSidesEffect>(), 1, Targeting.Slot_SelfSlot),
                ],
                Rarity = Rarity.Uncommon,
                Priority = Priority.Normal,
            };
            pygmalionpanic.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Swap_Sides), "AA_Multi3"]);

            Ability pygmalionpressure = new Ability("Crack under Pressure", "AApocrypha_PygmalionPressure_A")
            {
                Description = "Deal a Little damage to this enemy and produce 1 Red Pigment. \"Too much, too much!\"",
                Cost = [Pigments.Grey],
                Visuals = Visuals.Quills,
                AnimationTarget = Targeting.Slot_SelfSlot,
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 2, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(GiveRedPigment, 1, Targeting.Slot_SelfSlot),
                ],
                Rarity = Rarity.Uncommon,
                Priority = Priority.Normal,
            };
            pygmalionpressure.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Damage_1_2), nameof(IntentType_GameIDs.Mana_Generate)]);

            Ability pygmalionshield = new Ability("Run and Hide", "AApocrypha_PygmalionShield_A")
            {
                Description = "Apply 3 Shield to the Left and Right allied positions. Move Left or Right.",
                Cost = [Pigments.Grey],
                Visuals = Visuals.Shield,
                AnimationTarget = Targeting.Slot_AllySides,
                Effects =
                [
                    Effects.GenerateEffect(ApplyShield, 3, Targeting.Slot_AllySides),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<SwapToSidesEffect>(), 1, Targeting.Slot_SelfSlot),
                ],
                Rarity = Rarity.Uncommon,
                Priority = Priority.Normal,
            };
            pygmalionshield.AddIntentsToTarget(Targeting.Slot_AllySides, [nameof(IntentType_GameIDs.Field_Shield)]);
            pygmalionshield.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Swap_Sides)]);

            AddEnemyAbilityFromListEffect PygmalionHandler = ScriptableObject.CreateInstance<AddEnemyAbilityFromListEffect>();
            PygmalionHandler._abilityList = [
                pygmalionattackleft,
                pygmalionattackright,
                pygmalionattackfront,
                pygmalionpanic,
                pygmalionpressure,
                pygmalionshield,
            ];

            //Winter Lantern Abilities
            Ability falselife = new Ability("False Life", "AApocrypha_FalseLife_A")
            {
                Description = "Force All Inanimate enemies to deal a Painful amount of damage to their Opposing party member.",
                Cost = [Pigments.Red, Pigments.Red],
                Effects =
                [
                    Effects.GenerateEffect(HitOpposing, 4, ScriptableObject.CreateInstance<InanimateTargeting>()),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.Slow,
            };
            falselife.AddIntentsToTarget(ScriptableObject.CreateInstance<InanimateTargeting>(), [nameof(IntentType_GameIDs.Misc)]);
            falselife.AddIntentsToTarget(ScriptableObject.CreateInstance<OpposingInanimateTargeting>(), [nameof(IntentType_GameIDs.Damage_3_6)]);

            Ability bloodfromastone = new Ability("Blood from a Stone", "AApocrypha_BloodFromAStone_A")
            {
                Description = "Raise the maximum and current health of All Inanimate enemies by 2.\nForce All Inanimate enemies to produce 1 Red Pigment.",
                Cost = [Pigments.Red, Pigments.Red],
                Visuals = Visuals.Quills,
                AnimationTarget = ScriptableObject.CreateInstance<InanimateTargeting>(),
                Effects =
                [
                    Effects.GenerateEffect(MaxHealthIncrease, 2, ScriptableObject.CreateInstance<InanimateTargeting>()),
                    Effects.GenerateEffect(IndirectDamage, -2, ScriptableObject.CreateInstance<InanimateTargeting>()),
                    Effects.GenerateEffect(TargetsGiveRedPigment, 1, ScriptableObject.CreateInstance<InanimateTargeting>()),
                ],
                Rarity = Rarity.Rare,
                Priority = Priority.Slow,
            };
            bloodfromastone.AddIntentsToTarget(ScriptableObject.CreateInstance<InanimateTargeting>(), [nameof(IntentType_GameIDs.Misc)]);
            bloodfromastone.AddIntentsToTarget(ScriptableObject.CreateInstance<InanimateTargeting>(), [nameof(IntentType_GameIDs.Other_MaxHealth)]);
            bloodfromastone.AddIntentsToTarget(ScriptableObject.CreateInstance<InanimateTargeting>(), [nameof(IntentType_GameIDs.Mana_Generate)]);

            Ability pygmalion = new Ability("Pygmalion", "AApocrypha_Pygmalion_A")
            {
                Description = "Grant a random ability, chosen from a set, to each Inanimate enemy.",
                Cost = [Pigments.Red, Pigments.Red, Pigments.Red],
                Visuals = CustomVisuals.Whispers,
                AnimationTarget = Targeting.Slot_SelfSlot,
                Effects =
                [
                    Effects.GenerateEffect(PygmalionHandler, 10, ScriptableObject.CreateInstance<InanimateTargeting>()),
                ],
                Rarity = Rarity.Rare,
                Priority = Priority.Slow,
            };
            pygmalion.AddIntentsToTarget(ScriptableObject.CreateInstance<InanimateTargeting>(), [nameof(IntentType_GameIDs.Misc)]);

            Ability tantalize = new Ability("Tantalize", "AApocrypha_Tantalize_A")
            {
                Description = "Move All Inanimate enemies towards this enemy.\nIf there are no Inanimate enemies, deal a Barely Painful amount of damage to this enemy and shed a \"tooth\".",
                Cost = [Pigments.Red],
                Effects =
                [
                    Effects.GenerateEffect(IsInanimate, 1, Targeting.GenerateSlotTarget([-2], true)),
                    Effects.GenerateEffect(SwapRight, 1, Targeting.GenerateSlotTarget([-2], true), PreviousGenerator(true, 1)),
                    Effects.GenerateEffect(IsInanimate, 1, Targeting.GenerateSlotTarget([-3], true)),
                    Effects.GenerateEffect(SwapRight, 1, Targeting.GenerateSlotTarget([-3], true), PreviousGenerator(true, 1)),
                    Effects.GenerateEffect(IsInanimate, 1, Targeting.GenerateSlotTarget([-4], true)),
                    Effects.GenerateEffect(SwapRight, 1, Targeting.GenerateSlotTarget([-4], true), PreviousGenerator(true, 1)),
                    Effects.GenerateEffect(IsInanimate, 1, Targeting.GenerateSlotTarget([2], true)),
                    Effects.GenerateEffect(SwapLeft, 1, Targeting.GenerateSlotTarget([2], true), PreviousGenerator(true, 1)),
                    Effects.GenerateEffect(IsInanimate, 1, Targeting.GenerateSlotTarget([3], true)),
                    Effects.GenerateEffect(SwapLeft, 1, Targeting.GenerateSlotTarget([3], true), PreviousGenerator(true, 1)),
                    Effects.GenerateEffect(IsInanimate, 1, Targeting.GenerateSlotTarget([4], true)),
                    Effects.GenerateEffect(SwapLeft, 1, Targeting.GenerateSlotTarget([4], true), PreviousGenerator(true, 1)),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<CheckHasUnitEffect>(), 1, ScriptableObject.CreateInstance<InanimateTargeting>()),
                    Effects.GenerateEffect(FlayAnim, 1, Targeting.Slot_SelfSlot, PreviousGenerator(false, 1)),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 3, Targeting.Slot_SelfSlot, PreviousGenerator(false, 2)),
                    Effects.GenerateEffect(ShedTooth, 1, Targeting.Slot_SelfSlot, PreviousGenerator(false, 3)),
                ],
                Rarity = Rarity.ImpossibleNoReroll,
                Priority = Priority.ExtremelySlow,
            };
            tantalize.AddIntentsToTarget(Targeting.Slot_AlliesAllLefts, [nameof(IntentType_GameIDs.Swap_Right)]);
            tantalize.AddIntentsToTarget(Targeting.Slot_AlliesAllRights, [nameof(IntentType_GameIDs.Swap_Left)]);
            tantalize.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Misc_Hidden), nameof(IntentType_GameIDs.Damage_3_6), nameof(IntentType_GameIDs.Other_Spawn)]);

            ExtraAbilityInfo tantalizeextra = new()
            {
                ability = tantalize.GenerateEnemyAbility().ability,
                rarity = Rarity.ImpossibleNoReroll,
            };

            winterlantern.AddPassives([Passives.Anchored, Passives.GetCustomPassive("RedBlooded_1_PA"), Passives.BonusAttackGenerator(tantalizeextra)]);

            winterlantern.AddEnemyAbilities(
            [
                falselife.GenerateEnemyAbility(true),
                bloodfromastone.GenerateEnemyAbility(true),
                pygmalion.GenerateEnemyAbility(true),
            ]);
            winterlantern.AddEnemy(false, false, false);
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
