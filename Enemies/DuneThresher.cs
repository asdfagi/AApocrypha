using System;
using System.Collections.Generic;
using System.Text;
using A_Apocrypha.CustomOther;
using MythosFriends.Effectsa;

namespace A_Apocrypha.Enemies
{
    public class DuneThresher
    {
        public static void Add()
        {
            Enemy emplacement = new Enemy("Dune Thresher", "DuneThresher_EN")
            {
                Health = 32,
                HealthColor = Pigments.Grey,
                Size = 2,
                CombatSprite = ResourceLoader.LoadSprite("DuneThresherTimeline", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("DuneThresherDead", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("DuneThresherTimeline", new Vector2(0.5f, 0f), 32),
                DamageSound = "event:/AAEnemy/EmplacementHurt",
                DeathSound = "event:/AAEnemy/EmplacementDeath",
                UnitTypes = ["Robot"],
            };
            emplacement.PrepareEnemyPrefab("Assets/Apocrypha_Enemies/Emplacement_Enemy/Emplacement_Enemy.prefab", AApocrypha.assetBundle, AApocrypha.assetBundle.LoadAsset<GameObject>("Assets/Apocrypha_Enemies/Emplacement_Enemy/Emplacement_Giblets.prefab").GetComponent<ParticleSystem>());

            CasterStoreValueSetterAdvancedEffect SetTargeter = ScriptableObject.CreateInstance<CasterStoreValueSetterAdvancedEffect>();
            SetTargeter.m_unitStoredDataID = "TargeterStoredValue";
            SetTargeter._ignoreIfContains = false;
            SetTargeter.usePreviousExitValue = true;

            OpponentByStoredValueTargeting TargeterTargeting = ScriptableObject.CreateInstance<OpponentByStoredValueTargeting>();
            TargeterTargeting._storedValueID = "TargeterStoredValue";
            TargeterTargeting.targetUnitAllySlots = false;
            TargeterTargeting.reduceByOne = true;

            OpponentByStoredValueTargeting TargeterTargetingSides = ScriptableObject.CreateInstance<OpponentByStoredValueTargeting>();
            TargeterTargetingSides._storedValueID = "TargeterStoredValue";
            TargeterTargetingSides.targetUnitAllySlots = false;
            TargeterTargetingSides.reduceByOne = true;
            TargeterTargetingSides._modifiers = [-1, 1];

            OpponentByStoredValueTargeting TargeterTargetingGroup = ScriptableObject.CreateInstance<OpponentByStoredValueTargeting>();
            TargeterTargetingGroup._storedValueID = "TargeterStoredValue";
            TargeterTargetingGroup.targetUnitAllySlots = false;
            TargeterTargetingGroup.reduceByOne = true;
            TargeterTargetingGroup._modifiers = [-1, 0, 1];

            PassivePopUpOnTargetEffect TargeterPopup = ScriptableObject.CreateInstance<PassivePopUpOnTargetEffect>();
            TargeterPopup._name = "Targeter";
            TargeterPopup._sprite = "IconTargeter";
            TargeterPopup._isUnitCharacter = false;

            AnimationVisualsEffect TargetAnim = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            TargetAnim._visuals = Visuals.Poke;
            TargetAnim._animationTarget = TargeterTargeting;

            PerformEffectPassiveAbility emplacementTargeterPassive = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
            emplacementTargeterPassive.name = "Targeter_PA";
            emplacementTargeterPassive._passiveName = "Targeter";
            emplacementTargeterPassive.m_PassiveID = "Targeter";
            emplacementTargeterPassive.passiveIcon = ResourceLoader.LoadSprite("IconTargeter");
            emplacementTargeterPassive._characterDescription = "might not work, also what'd you even DO with this?";
            emplacementTargeterPassive._enemyDescription = "At the start of combat and at the end of the timeline, this enemy will remember the position of the party member with the highest current health.";
            emplacementTargeterPassive._triggerOn = [TriggerCalls.TimelineEndReached, TriggerCalls.OnCombatStart];
            emplacementTargeterPassive.doesPassiveTriggerInformationPanel = true;
            emplacementTargeterPassive.effects =
            [
                Effects.GenerateEffect(ScriptableObject.CreateInstance<TargetGetSlotEffect>(), 1, Targeting.GenerateUnitTarget_Specific_Health(false, false, false, false)),
                Effects.GenerateEffect(SetTargeter, 1, Targeting.Slot_SelfSlot),
                Effects.GenerateEffect(TargetAnim),
            ];
            Passives.AddCustomPassiveToPool("AA_TargeterDuneThresher_PA", "Targeter", emplacementTargeterPassive);

            SwapToOneRandomSideXTimesEffect SwapRandomFar = ScriptableObject.CreateInstance<SwapToOneRandomSideXTimesEffect>();

            SwapToOneSideEffect SwapLeft = ScriptableObject.CreateInstance<SwapToOneSideEffect>();
            SwapLeft._swapRight = false;

            SwapToOneSideEffect SwapRight = ScriptableObject.CreateInstance<SwapToOneSideEffect>();
            SwapRight._swapRight = true;

            DamageOnDoubleCascadeEffect DamageCascade = ScriptableObject.CreateInstance<DamageOnDoubleCascadeEffect>();
            DamageCascade._usePreviousExitValue = false;
            DamageCascade._cascadeIsIndirect = true;
            DamageCascade._decreaseAsPercentage = true;
            DamageCascade._cascadeDecrease = 75;
            DamageCascade._returnKillAsSuccess = false;

            AnimationVisualsEffect ShrapnelAnim = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            ShrapnelAnim._visuals = Visuals.Exsanguinate;
            ShrapnelAnim._animationTarget = TargeterTargetingSides;

            StatusEffect_Apply_Effect RupturedApply = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            RupturedApply._Status = StatusField.Ruptured;

            AnimationVisualsEffect ShieldAnim = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            ShieldAnim._visuals = Visuals.Shield;
            ShieldAnim._animationTarget = Targeting.Slot_SelfAll;

            FieldEffect_Apply_Effect ShieldApply = ScriptableObject.CreateInstance<FieldEffect_Apply_Effect>();
            ShieldApply._Field = StatusField.Shield;

            AnimationVisualsEffect TargetCryAnim = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            TargetCryAnim._visuals = Visuals.Misery;
            TargetCryAnim._animationTarget = TargeterTargeting;

            GenerateColorManaPerTargetEffect TargetsGiveBluePigment = ScriptableObject.CreateInstance<GenerateColorManaPerTargetEffect>();
            TargetsGiveBluePigment.mana = Pigments.Blue;

            PreviousEffectCondition PreviousTrue = ScriptableObject.CreateInstance<PreviousEffectCondition>();
            PreviousTrue.wasSuccessful = true;

            PreviousEffectCondition PreviousFalse = ScriptableObject.CreateInstance<PreviousEffectCondition>();
            PreviousFalse.wasSuccessful = false;

            SpawnEnemyAnywhereEffect CallReinforcements = ScriptableObject.CreateInstance<SpawnEnemyAnywhereEffect>();
            CallReinforcements.enemy = LoadedAssetsHandler.GetEnemy("SandSifterSummon_EN");
            CallReinforcements._spawnTypeID = CombatType_GameIDs.Spawn_Basic.ToString();

            ReturnValueComparatorEffectorCondition TenOrMore = ScriptableObject.CreateInstance<ReturnValueComparatorEffectorCondition>();
            TenOrMore._lessThan = false;
            TenOrMore._comparator = 10;

            PerformEffectPassiveAbility deploymentEmplacement = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
            deploymentEmplacement.name = "DeploymentEmplacement_PA";
            deploymentEmplacement._passiveName = "Deployment (10)";
            deploymentEmplacement.m_PassiveID = "DeploymentEmplacement";
            deploymentEmplacement.passiveIcon = ResourceLoader.LoadSprite("IconDeployment");
            deploymentEmplacement._characterDescription = "I hope you like small robots that drill holes into the ground.";
            deploymentEmplacement._enemyDescription = "On taking 10 or more damage, attempt to summon a Sand Sifter with 8 health and Withering as a passive.";
            deploymentEmplacement.doesPassiveTriggerInformationPanel = true;
            deploymentEmplacement._triggerOn = [TriggerCalls.OnDirectDamaged];
            deploymentEmplacement.conditions = [TenOrMore];
            deploymentEmplacement.effects =
            [
                Effects.GenerateEffect(CallReinforcements, 1, TbazTargeting.Farthest(true)),
            ];
            Passives.AddCustomPassiveToPool("AA_DeploymentDuneThresher_PA", "Deployment (10)", deploymentEmplacement);

            GenerateRandomManaBetweenEffect WeirdRandomPigmentSplit = ScriptableObject.CreateInstance<GenerateRandomManaBetweenEffect>();
            WeirdRandomPigmentSplit.possibleMana = new ManaColorSO[]
            {
                Pigments.RedBlue,
                Pigments.BlueRed,
                Pigments.RedBlue,
                Pigments.BlueRed,
                Pigments.RedYellow,
                Pigments.RedYellow,
                Pigments.BlueYellow,
                Pigments.RedPurple,
                Pigments.RedPurple,
                Pigments.BluePurple,
                Pigments.YellowPurple,
                Pigments.PurpleYellow,
                Pigments.SplitPigment(new ManaColorSO[]
                {
                    Pigments.Red,
                    Pigments.Blue,
                    Pigments.Yellow
                }),
                Pigments.SplitPigment(new ManaColorSO[]
                {
                    Pigments.Red,
                    Pigments.Blue,
                    Pigments.Purple
                }),
                Pigments.SplitPigment(new ManaColorSO[]
                {
                    Pigments.Blue,
                    Pigments.Yellow,
                    Pigments.Purple
                }),
                Pigments.SplitPigment(new ManaColorSO[]
                {
                    Pigments.Red,
                    Pigments.Yellow,
                    Pigments.Purple
                }),
                Pigments.SplitPigment(new ManaColorSO[]
                {
                    Pigments.Red,
                    Pigments.Blue,
                    Pigments.Yellow,
                    Pigments.Purple
                }),
                Pigments.SplitPigment(new ManaColorSO[]
                {
                    Pigments.Grey,
                    Pigments.Green
                }),
                Pigments.SplitPigment(new ManaColorSO[]
                {
                    Pigments.Green,
                    Pigments.Grey
                }),
            };

            SpecificEnemiesTargeting AllSifters = ScriptableObject.CreateInstance<SpecificEnemiesTargeting>();
            AllSifters._enemies = ["SandSifter_EN", "SandSifterSummon_EN"];
            AllSifters.targetUnitAllySlots = true;
            AllSifters.slotOffsets = [0];

            TargetPerformEffectViaSubaction YouMakePigmentNow = ScriptableObject.CreateInstance<TargetPerformEffectViaSubaction>();
            YouMakePigmentNow.effects = 
            [
                Effects.GenerateEffect(WeirdRandomPigmentSplit, 1, Targeting.Slot_SelfSlot),
            ];

            Ability heshell = new Ability("High Explosive Shell", "AApocrypha_HighExplosiveShell_A")
            {
                Description = "Deal an Agonizing amount of damage to this enemy's Targeted position.",
                Cost = [Pigments.Grey],
                Visuals = ITAVisuals.Explode,
                AnimationTarget = TargeterTargeting,
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 10, TargeterTargeting),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.Normal,
            };
            heshell.AddIntentsToTarget(TargeterTargeting, [nameof(IntentType_GameIDs.Damage_7_10)]);

            Ability carpetshell = new Ability("Carpet Shell", "AApocrypha_CarpetShell_A")
            {
                Description = "Deal a Painful amount of damage to this enemy's Targeted position and a Barely Painful amount of damage to the positions to the Left and Right of that position.",
                Cost = [Pigments.Grey],
                Visuals = ITAVisuals.Explode,
                AnimationTarget = TargeterTargetingGroup,
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 5, TargeterTargeting),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 3, TargeterTargetingSides),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.Normal,
            };
            carpetshell.AddIntentsToTarget(TargeterTargeting, [nameof(IntentType_GameIDs.Damage_3_6)]);
            carpetshell.AddIntentsToTarget(TargeterTargetingSides, [nameof(IntentType_GameIDs.Damage_3_6)]);

            Ability shrapnelshell = new Ability("Fragmentation Shell", "AApocrypha_ShrapnelShell_A")
            {
                Description = "Deal a Painful amount of damage to this enemy's Targeted position.\nApply 2 Ruptured to the positions to the Left and Right of that position.",
                Cost = [Pigments.Grey],
                Visuals = ITAVisuals.Explode,
                AnimationTarget = TargeterTargeting,
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 5, TargeterTargeting),
                    Effects.GenerateEffect(ShrapnelAnim),
                    Effects.GenerateEffect(RupturedApply, 2, TargeterTargetingSides),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.Normal,
            };
            shrapnelshell.AddIntentsToTarget(TargeterTargeting, [nameof(IntentType_GameIDs.Damage_3_6)]);
            shrapnelshell.AddIntentsToTarget(TargeterTargetingSides, [nameof(IntentType_GameIDs.Status_Ruptured)]);

            Ability positionleft = new Ability("Advance West", "AApocrypha_EmplacementGoLeft_A")
            {
                Description = "Move as far Left as possible, then apply 4 Shield to this enemy's positions.",
                Cost = [Pigments.Grey],
                Effects =
                [
                    Effects.GenerateEffect(SwapLeft, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(SwapLeft, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(SwapLeft, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(SwapLeft, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(ShieldAnim),
                    Effects.GenerateEffect(ShieldApply, 4, Targeting.Slot_SelfAll),
                ],
                Rarity = Rarity.ImpossibleNoReroll,
                Priority = Priority.Slow,
            };
            positionleft.AddIntentsToTarget(Targeting.Slot_SelfAll, [nameof(IntentType_GameIDs.Swap_Left), nameof(IntentType_GameIDs.Swap_Left), nameof(IntentType_GameIDs.Swap_Left), nameof(IntentType_GameIDs.Swap_Left), nameof(IntentType_GameIDs.Field_Shield)]);

            Ability positionright = new Ability("Advance East", "AApocrypha_EmplacementGoRight_A")
            {
                Description = "Move as far Right as possible, then apply 4 Shield to this enemy's positions.",
                Cost = [Pigments.Grey],
                Effects =
                [
                    Effects.GenerateEffect(SwapRight, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(SwapRight, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(SwapRight, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(SwapRight, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(ShieldAnim),
                    Effects.GenerateEffect(ShieldApply, 4, Targeting.Slot_SelfAll),
                ],
                Rarity = Rarity.ImpossibleNoReroll,
                Priority = Priority.Slow,
            };
            positionright.AddIntentsToTarget(Targeting.Slot_SelfAll, [nameof(IntentType_GameIDs.Swap_Right), nameof(IntentType_GameIDs.Swap_Right), nameof(IntentType_GameIDs.Swap_Right), nameof(IntentType_GameIDs.Swap_Right), nameof(IntentType_GameIDs.Field_Shield)]);

            Ability reinforce = new Ability("Panic Signal", "AApocrypha_EmplacementSummon_A")
            {
                Description = "Attempt to summon a Sand Sifter with 8 health and Withering as a passive.\nIf this fails, force all Sand Sifters to produce 1 Random Split Pigment.",
                Cost = [Pigments.Grey],
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<CheckHasUnitEffect>(), 1, Targeting.GenerateGenericTarget([0], true)),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<CheckHasUnitEffect>(), 1, Targeting.GenerateGenericTarget([1], true)),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<CheckHasUnitEffect>(), 1, Targeting.GenerateGenericTarget([2], true)),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<CheckHasUnitEffect>(), 1, Targeting.GenerateGenericTarget([3], true)),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<CheckHasUnitEffect>(), 1, Targeting.GenerateGenericTarget([4], true)),
                    Effects.GenerateEffect(YouMakePigmentNow, 1, AllSifters, Effects.CheckMultiplePreviousEffectsCondition([true, true, true, true, true], [1, 2, 3, 4, 5])),
                    Effects.GenerateEffect(CallReinforcements, 1, TbazTargeting.Farthest(true)),
                ],
                Rarity = Rarity.ImpossibleNoReroll,
                Priority = Priority.Fast,
            };
            reinforce.AddIntentsToTarget(Targeting.Slot_SelfAll, [nameof(IntentType_GameIDs.Other_Spawn)]);
            reinforce.AddIntentsToTarget(AllSifters, [nameof(IntentType_GameIDs.Mana_Generate)]);

            ExtraAbilityInfo leftextra = new()
            {
                ability = positionleft.GenerateEnemyAbility().ability,
                rarity = Rarity.Common,
            };

            ExtraAbilityInfo rightextra = new()
            {
                ability = positionright.GenerateEnemyAbility().ability,
                rarity = Rarity.Common,
            };

            ExtraAbilityInfo reinforceextra = new()
            {
                ability = reinforce.GenerateEnemyAbility().ability,
                rarity = Rarity.Common,
            };

            emplacement.AddPassives([Passives.GetCustomPassive("AA_TargeterDuneThresher_PA"), Passives.GetCustomPassive("AA_DeploymentDuneThresher_PA"), CustomPassives.AltAttacksGenerator([leftextra, rightextra, reinforceextra])]);

            emplacement.AddEnemyAbilities([
                heshell.GenerateEnemyAbility(true),
                carpetshell.GenerateEnemyAbility(true),
                shrapnelshell.GenerateEnemyAbility(true),
            ]);
            emplacement.AddEnemy(false, false, false);
        }
    }
}
