using System;
using System.Collections.Generic;
using System.Text;
using A_Apocrypha.Assets;
using A_Apocrypha.CustomOther;

namespace A_Apocrypha.Enemies
{
    public class Enlightened
    {
        public static void Add()
        {
            SetCasterAnimationParameterEffect Enlighten = ScriptableObject.CreateInstance<SetCasterAnimationParameterEffect>();
            Enlighten._parameterName = "Enlightened";
            Enlighten._parameterValue = 1;
            Enlighten._UsePrevious = false;

            SetCasterAnimationParameterEffect Unenlighten = ScriptableObject.CreateInstance<SetCasterAnimationParameterEffect>();
            Unenlighten._parameterName = "Enlightened";
            Unenlighten._parameterValue = 0;
            Unenlighten._UsePrevious = false;

            EnlightenedMusicHandlerEffect MusicToggleOn = ScriptableObject.CreateInstance<EnlightenedMusicHandlerEffect>();
            MusicToggleOn.Add = true;

            EnlightenedMusicHandlerEffect MusicToggleOff = ScriptableObject.CreateInstance<EnlightenedMusicHandlerEffect>();
            MusicToggleOff.Add = false;

            EnlightenedMusicHandlerEffect MusicToggleReset = ScriptableObject.CreateInstance<EnlightenedMusicHandlerEffect>();
            MusicToggleReset.ResetEffect = true;

            Enemy enlightenedvessel = new Enemy("Enlightened Vessel", "EnlightenedVessel_EN")
            {
                Health = 45,
                HealthColor = Pigments.Grey,
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("EnlightenedVesselTimeline", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("EnlightenedVesselDead", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("EnlightenedVesselTimeline", new Vector2(0.5f, 0f), 32),
                DamageSound = "event:/AAEnemy/Enlightened/EnlightenedHurt",
                DeathSound = "event:/AAEnemy/Enlightened/EnlightenedDeath",
                UnitTypes = [UnitType_GameIDs.Fish.ToString()],
            };
            enlightenedvessel.PrepareEnemyPrefab("Assets/Apocrypha_Enemies/Enlightened_Enemy/Enlightened_Enemy.prefab", AApocrypha.assetBundle, AApocrypha.assetBundle.LoadAsset<GameObject>("Assets/Apocrypha_Enemies/Enlightened_Enemy/Enlightened_Giblets.prefab").GetComponent<ParticleSystem>());

            Enemy enlightenedspirit = new Enemy("Enlightened Spirit", "EnlightenedSpirit_EN")
            {
                Health = 10,
                HealthColor = Pigments.Grey,
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("EnlightenedSpiritTimeline", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("AnomalyDead", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("EnlightenedSpiritTimeline", new Vector2(0.5f, 0f), 32),
                DamageSound = "event:/AAEnemy/Enlightened/SpiritHurt",
                DeathSound = "event:/AAEnemy/Enlightened/SpiritHurt",
                CombatEnterEffects = [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<CheckIsPlayerTurnEffect>(), 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<AddTurnCasterToTimelineEffect>(), 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(MusicToggleOn),
                ],
                CombatExitEffects = [Effects.GenerateEffect(MusicToggleOff)],
            };
            enlightenedspirit.PrepareEnemyPrefab("Assets/Apocrypha_Enemies/Enlightened_Enemy/EnlightenedSpirit_Enemy.prefab", AApocrypha.assetBundle, null);

            if (AApocrypha.CrossMod.IntoTheAbyss && LoadedDBsHandler.PigmentDB.GetPigment("Iridescent") != null)
            {
                enlightenedspirit.HealthColor = LoadedDBsHandler.PigmentDB.GetPigment("Iridescent");
            }

            ChangeToRandomHealthColorEffect RedNow = ScriptableObject.CreateInstance<ChangeToRandomHealthColorEffect>();
            RedNow._healthColors = [Pigments.Red];

            ChangeToRandomHealthColorEffect GreyNow = ScriptableObject.CreateInstance<ChangeToRandomHealthColorEffect>();
            GreyNow._healthColors = [Pigments.Grey];

            ReturnValueComparatorEffectorCondition TenOrMore = ScriptableObject.CreateInstance<ReturnValueComparatorEffectorCondition>();
            TenOrMore._lessThan = false;
            TenOrMore._comparator = 10;

            RemovePassiveEffect ghostBust = ScriptableObject.CreateInstance<RemovePassiveEffect>();
            ghostBust.m_PassiveID = "Haunted";

            SpecificEnemiesTargeting VesselsTargeting = ScriptableObject.CreateInstance<SpecificEnemiesTargeting>();
            VesselsTargeting._enemies = ["EnlightenedVessel_EN"];
            VesselsTargeting.slotOffsets = [0];
            VesselsTargeting.targetUnitAllySlots = true;

            SpecificEnemiesTargeting HollowVesselsTargeting = ScriptableObject.CreateInstance<SpecificEnemiesTargeting>();
            HollowVesselsTargeting._enemies = ["EnlightenedVessel_EN"];
            HollowVesselsTargeting.slotOffsets = [0];
            HollowVesselsTargeting.targetUnitAllySlots = true;
            HollowVesselsTargeting._passiveBlacklist = ["Haunted"];

            SpecificEnemiesTargeting OpposingVesselsTargeting = ScriptableObject.CreateInstance<SpecificEnemiesTargeting>();
            OpposingVesselsTargeting._enemies = ["EnlightenedVessel_EN"];
            OpposingVesselsTargeting.slotOffsets = [0];
            OpposingVesselsTargeting.targetUnitAllySlots = false;

            SpawnEnemyAnywhereEffect releaseGhost = ScriptableObject.CreateInstance<SpawnEnemyAnywhereEffect>();
            releaseGhost.enemy = enlightenedspirit.enemy;
            releaseGhost.givesExperience = false;
            releaseGhost._spawnTypeID = CombatType_GameIDs.Spawn_Basic.ToString();

            SwapCasterAbilitiesMaintainTimelineEffect EnlightenAbilities = ScriptableObject.CreateInstance<SwapCasterAbilitiesMaintainTimelineEffect>();

            SwapCasterAbilitiesMaintainTimelineEffect UnenlightenAbilities = ScriptableObject.CreateInstance<SwapCasterAbilitiesMaintainTimelineEffect>();

            IsAliveEffectorCondition IsThisAlive = ScriptableObject.CreateInstance<IsAliveEffectorCondition>();
            IsThisAlive.checkByCurrentHealth = true;

            PerformEffectPassiveAbility HauntedEnlightened = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
            HauntedEnlightened.name = "AA_HauntedEnlightened_PA";
            HauntedEnlightened._passiveName = "Haunted (10)";
            HauntedEnlightened.m_PassiveID = "Haunted";
            HauntedEnlightened.passiveIcon = ResourceLoader.LoadSprite("IconEnlightenedHollow");
            HauntedEnlightened._characterDescription = "the ghost! the ghost!! call the ghostbusterrr... the ghost is stealing! my money! he punched me in the face! and now he run off with my money! hey!! ghost come back that's my money!!!.";
            HauntedEnlightened._enemyDescription = "This enemy is inhabited by an Enlightened Spirit, empowering its attacks. On taking 10 or more direct damage, expel the spirit.";
            HauntedEnlightened._triggerOn = [TriggerCalls.OnDirectDamaged];
            HauntedEnlightened.conditions = [TenOrMore, IsThisAlive];
            HauntedEnlightened.doesPassiveTriggerInformationPanel = true;
            HauntedEnlightened.effects =
            [
                Effects.GenerateEffect(ghostBust, 1, Targeting.Slot_SelfSlot),
                Effects.GenerateEffect(GreyNow, 1, Targeting.Slot_SelfSlot),
                Effects.GenerateEffect(Unenlighten, 1, Targeting.Slot_SelfSlot),
                Effects.GenerateEffect(UnenlightenAbilities, 1, Targeting.Slot_SelfSlot),
                Effects.GenerateEffect(releaseGhost, 1, Targeting.Slot_SelfSlot),
                Effects.GenerateEffect(ScriptableObject.CreateInstance<ReloadTimelineEffect>()),
            ];
            Passives.AddCustomPassiveToPool("AA_HauntedEnlightened_PA", "Haunted (10)", HauntedEnlightened);

            AddPassiveEffect HauntPassive = ScriptableObject.CreateInstance<AddPassiveEffect>();
            HauntPassive._passiveToAdd = Passives.GetCustomPassive("AA_HauntedEnlightened_PA");
            Debug.Log("we're fine");

            RandomTargetPerformEffectViaSubaction HauntingHandler = ScriptableObject.CreateInstance<RandomTargetPerformEffectViaSubaction>();
            HauntingHandler.effects = [
                Effects.GenerateEffect(HauntPassive, 1, Targeting.Slot_SelfSlot),
                Effects.GenerateEffect(RedNow, 1, Targeting.Slot_SelfSlot),
                Effects.GenerateEffect(Enlighten, 1, Targeting.Slot_SelfSlot),
                Effects.GenerateEffect(EnlightenAbilities, 1, Targeting.Slot_SelfSlot),
                Effects.GenerateEffect(ScriptableObject.CreateInstance<ReloadTimelineEffect>()),
            ]; 
            
            PerformEffectPassiveAbility HaunterEnlightened = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
            HaunterEnlightened.name = "AA_HaunterEnlightened_PA";
            HaunterEnlightened._passiveName = "Haunter";
            HaunterEnlightened.m_PassiveID = "Haunter";
            HaunterEnlightened.passiveIcon = ResourceLoader.LoadSprite("IconEnlightenedPossess");
            HaunterEnlightened._characterDescription = "i poisoned\nthe water supply\nnow everyone dead\n\nwhoops";
            HaunterEnlightened._enemyDescription = "At the start of combat and at the end of the timeline, this enemy will possess a random Enlightened Vessel.";
            HaunterEnlightened._triggerOn = [TriggerCalls.OnCombatStart, TriggerCalls.OnRoundFinished];
            HaunterEnlightened.doesPassiveTriggerInformationPanel = true;
            HaunterEnlightened.effects =
            [
                Effects.GenerateEffect(ScriptableObject.CreateInstance<FleeTargetEffect>(), 1, Targeting.Slot_SelfSlot),
                Effects.GenerateEffect(MusicToggleReset, 1, Targeting.Slot_SelfSlot),
                Effects.GenerateEffect(HauntingHandler, 1, HollowVesselsTargeting),
            ];
            Passives.AddCustomPassiveToPool("AA_HaunterEnlightened_PA", "Haunter", HaunterEnlightened);
            
            enlightenedvessel.AddPassives([Passives.Inanimate]);
            enlightenedspirit.AddPassives([Passives.GetCustomPassive("AA_HaunterEnlightened_PA"), Passives.Withering]);

            FieldEffect_ApplyWithRandomDistribution_Effect SpreadShieldByPrevious = ScriptableObject.CreateInstance<FieldEffect_ApplyWithRandomDistribution_Effect>();
            SpreadShieldByPrevious.field = StatusField.Shield;
            SpreadShieldByPrevious.usePrevious = true;

            FieldEffect_ApplyWithRandomDistribution_Effect SpreadShield = ScriptableObject.CreateInstance<FieldEffect_ApplyWithRandomDistribution_Effect>();
            SpreadShield.field = StatusField.Shield;
            SpreadShield.usePrevious = false;

            ChangeMaxHealthEffect MaxHealthPlus = ScriptableObject.CreateInstance<ChangeMaxHealthEffect>();
            MaxHealthPlus._increase = true;
            MaxHealthPlus._entryAsPercentage = false;

            Ability vesselUn1 = new Ability("Stagnation", "AApocrypha_EnlightenedVesselUn1_A")
            {
                Description = "Deal a Painful amount of damage to the Opposing party member. Randomly distribute Shield among all positions occupied by Enlightened Vessels equal to the amount of damage dealt.",
                Cost = [Pigments.Red, Pigments.Grey],
                Visuals = Visuals.Crush,
                AnimationTarget = Targeting.Slot_Front,
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 5, Targeting.Slot_Front),
                    Effects.GenerateEffect(SpreadShieldByPrevious, 1, VesselsTargeting, Effects.CheckPreviousEffectCondition(true, 1))
                ],
                Rarity = Rarity.Common,
                Priority = Priority.Normal,
            };
            vesselUn1.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Damage_3_6)]);
            vesselUn1.AddIntentsToTarget(VesselsTargeting, [nameof(IntentType_GameIDs.Field_Shield)]);

            StatusEffect_Apply_Effect FrailApply = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            FrailApply._Status = StatusField.Frail;

            AnimationVisualsEffect ScrunchAnim = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            ScrunchAnim._visuals = Visuals.Equal;
            ScrunchAnim._animationTarget = Targeting.Slot_Front;

            Ability vesselUn2 = new Ability("Division", "AApocrypha_EnlightenedVesselUn2_A")
            {
                Description = "Deal a Painful amount of damage to the Left and Right party members. Apply 1 Frail to the Opposing party member and increase their maximum health by 2. This ability assumes the grid wraps around.",
                Cost = [Pigments.Red, Pigments.Grey],
                Visuals = Visuals.Mitosis,
                AnimationTarget = Targeting.GenerateSlotTarget([-4, -1, 1, 4], false),
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 5, Targeting.GenerateSlotTarget([-4, -1, 1, 4], false)),
                    Effects.GenerateEffect(ScrunchAnim),
                    Effects.GenerateEffect(FrailApply, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(MaxHealthPlus, 2, Targeting.Slot_Front),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.Normal,
            };
            vesselUn2.AddIntentsToTarget(Targeting.GenerateSlotTarget([-4, -1, 1, 4], false), [nameof(IntentType_GameIDs.Damage_3_6)]);
            vesselUn2.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Status_Frail), nameof(IntentType_GameIDs.Other_MaxHealth)]);

            Ability vesselUn3 = new Ability("Descension", "AApocrypha_EnlightenedVesselUn3_A")
            {
                Description = "Summon an Enlightened Spirit into this vessel.",
                Cost = [Pigments.Red, Pigments.Red],
                Visuals = Visuals.Providence,
                AnimationTarget = Targeting.Slot_SelfSlot,
                Effects =
                [
                    Effects.GenerateEffect(HauntingHandler, 1, Targeting.Slot_SelfSlot),
                ],
                Rarity = Rarity.Uncommon,
                Priority = Priority.Normal,
            };
            vesselUn3.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Misc)]);

            ChangeMaxHealthEffect PercentageReduceMaxHealth = ScriptableObject.CreateInstance<ChangeMaxHealthEffect>();
            PercentageReduceMaxHealth._entryAsPercentage = true;
            PercentageReduceMaxHealth._increase = false;

            ExtraPassiveAbility_Wearable_SMS wearablePassiveWithering = ScriptableObject.CreateInstance<ExtraPassiveAbility_Wearable_SMS>();
            wearablePassiveWithering._extraPassiveAbility = Passives.Withering;

            CopyCasterAndSpawnCharacterAnywhereEffect CopyMake = ScriptableObject.CreateInstance<CopyCasterAndSpawnCharacterAnywhereEffect>();
            CopyMake._rank = 0;
            CopyMake._nameAddition = NameAdditionLocID.NameAdditionNot;
            CopyMake._permanentSpawn = true;
            CopyMake._maximizeHealth = false;
            CopyMake._rankIsAdditive = false;
            CopyMake._extraModifiers = [wearablePassiveWithering];

            AnimationVisualsEffect SplitAnim = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            SplitAnim._visuals = Visuals.Mitosis;
            SplitAnim._animationTarget = Targeting.Slot_SelfSlot;

            AnimationVisualsEffect CurseAnim = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            CurseAnim._visuals = Visuals.UglyOnTheInside;
            CurseAnim._animationTarget = Targeting.Slot_SelfSlot;

            StatusEffect_Apply_Effect CurseApply = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            CurseApply._Status = StatusField.Cursed;

            TargetPerformEffectViaSubaction PartyCurser = ScriptableObject.CreateInstance<TargetPerformEffectViaSubaction>();
            PartyCurser.effects =
            [
                Effects.GenerateEffect(CurseAnim, 1, Targeting.Slot_SelfSlot),
                Effects.GenerateEffect(ScriptableObject.CreateInstance<HealEffect>(), 5, Targeting.Slot_SelfSlot),
                Effects.GenerateEffect(CurseApply, 1, Targeting.Slot_SelfSlot),
            ];

            TargetPerformEffectViaSubaction PartySplitter = ScriptableObject.CreateInstance<TargetPerformEffectViaSubaction>();
            PartySplitter.effects =
            [
                Effects.GenerateEffect(SplitAnim, 1, Targeting.Slot_SelfSlot),
                Effects.GenerateEffect(ScriptableObject.CreateInstance<HealthReductionToHalfOfCurrentEffect>(), 1, Targeting.Slot_SelfSlot),
                Effects.GenerateEffect(CopyMake, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 1)),
                Effects.GenerateEffect(PartyCurser, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 2)),
            ];

            RandomTargetPerformEffectViaSubaction SplitHandler = ScriptableObject.CreateInstance<RandomTargetPerformEffectViaSubaction>();
            SplitHandler.effects =
            [
                Effects.GenerateEffect(ScriptableObject.CreateInstance<CheckHasUnitEffect>(), 1, Targeting.GenerateGenericTarget([0], true)),
                Effects.GenerateEffect(ScriptableObject.CreateInstance<CheckHasUnitEffect>(), 1, Targeting.GenerateGenericTarget([1], true)),
                Effects.GenerateEffect(ScriptableObject.CreateInstance<CheckHasUnitEffect>(), 1, Targeting.GenerateGenericTarget([2], true)),
                Effects.GenerateEffect(ScriptableObject.CreateInstance<CheckHasUnitEffect>(), 1, Targeting.GenerateGenericTarget([3], true)),
                Effects.GenerateEffect(ScriptableObject.CreateInstance<CheckHasUnitEffect>(), 1, Targeting.GenerateGenericTarget([4], true)),
                Effects.GenerateEffect(ScriptableObject.CreateInstance<ExtraVariableForNextEffect>(), 1, Targeting.Slot_SelfSlot, Effects.CheckMultiplePreviousEffectsCondition([true, true, true, true, true], [1, 2, 3, 4, 5])),
                Effects.GenerateEffect(PartyCurser, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 1)),
                Effects.GenerateEffect(PartySplitter, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 2)),
            ];

            TargetPerformEffectViaSubaction ShieldSubaction = ScriptableObject.CreateInstance<TargetPerformEffectViaSubaction>();
            ShieldSubaction.effects = [Effects.GenerateEffect(SpreadShield, 2, VesselsTargeting)];

            AnimationVisualsEffect ProvidenceAnim = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            ProvidenceAnim._visuals = Visuals.Providence;
            ProvidenceAnim._animationTarget = Targeting.Slot_SelfSlot;

            Ability vesselEn1 = new Ability("Kenoma", "AApocrypha_EnlightenedVesselEn1_A")
            {
                Description = "Deal an Agonizing amount of damage to the Opposing party member. Randomly distribute 4 Shield, plus 2 shield for each enemy in combat, among all occupied enemy positions.",
                Cost = [Pigments.Red, Pigments.Red],
                Visuals = Visuals.Crush,
                AnimationTarget = Targeting.Slot_Front,
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 8, Targeting.Slot_Front),
                    Effects.GenerateEffect(SpreadShield, 4, VesselsTargeting),
                    Effects.GenerateEffect(ShieldSubaction, 1, Targeting.Unit_AllAllies),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.Fast,
            };
            vesselEn1.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Damage_7_10)]);
            vesselEn1.AddIntentsToTarget(Targeting.Unit_AllAllySlots, [nameof(IntentType_GameIDs.Field_Shield)]);

            Ability vesselEn2 = new Ability("Ousia", "AApocrypha_EnlightenedVesselEn2_A")
            {
                Description = "If there is a free party member position, split the Opposing party member in half, or a random party member if there is no Opposing party member." +
                "\nIf the selected party member is too weak to be split or all party member positions are occupied, Heal the selected party member and Curse them.",
                Cost = [Pigments.Red, Pigments.Red],
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<CheckHasUnitEffect>(), 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(SplitHandler, 1, Targeting.Slot_Front, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(SplitHandler, 1, Targeting.Unit_AllOpponents, Effects.CheckPreviousEffectCondition(false, 2)),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.Fast,
            };
            vesselEn2.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Misc_Hidden)]);
            vesselEn2.AddIntentsToTarget(Targeting.Slot_Front_ThenAllRights_ThenAllLefts, [nameof(IntentType_GameIDs.Other_MaxHealth_Alt), nameof(IntentType_GameIDs.Other_Spawn), nameof(IntentType_GameIDs.Status_Cursed)]);

            Ability vesselEn3 = new Ability("Pleroma", "AApocrypha_EnlightenedVesselEn3_A")
            {
                Description = "Deal a Painful amount of damage to All party members Opposing an Enlightened Vessel, then apply 1 Frail to them." +
                "\nDismiss the Enlightened Spirit inhabiting this vessel.",
                Cost = [Pigments.Red, Pigments.Red],
                Visuals = Visuals.Crush,
                AnimationTarget = OpposingVesselsTargeting,
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 4, OpposingVesselsTargeting),
                    Effects.GenerateEffect(FrailApply, 1, OpposingVesselsTargeting),
                    Effects.GenerateEffect(ProvidenceAnim),
                    Effects.GenerateEffect(ghostBust, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(GreyNow, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(Unenlighten, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(UnenlightenAbilities, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<ReloadTimelineEffect>()),
                ],
                Rarity = Rarity.Uncommon,
                Priority = Priority.Fast,
            };
            vesselEn3.AddIntentsToTarget(OpposingVesselsTargeting, [nameof(IntentType_GameIDs.Damage_3_6), nameof(IntentType_GameIDs.Status_Frail)]);
            vesselEn3.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Misc)]);

            ExtraAbilityInfo bonusEn1 = new()
            {
                ability = vesselEn1.GenerateEnemyAbility().ability,
                rarity = Rarity.Common,
            };

            ExtraAbilityInfo bonusEn2 = new()
            {
                ability = vesselEn2.GenerateEnemyAbility().ability,
                rarity = Rarity.Common,
            };

            ExtraAbilityInfo bonusEn3 = new()
            {
                ability = vesselEn3.GenerateEnemyAbility().ability,
                rarity = Rarity.Uncommon,
            };

            EnlightenAbilities._abilitiesToSwap =
            [
                bonusEn1,
                bonusEn2,
                bonusEn3,
            ];

            ExtraAbilityInfo bonusUn1 = new()
            {
                ability = vesselUn1.GenerateEnemyAbility().ability,
                rarity = Rarity.Common,
            };

            ExtraAbilityInfo bonusUn2 = new()
            {
                ability = vesselUn2.GenerateEnemyAbility().ability,
                rarity = Rarity.Common,
            };

            ExtraAbilityInfo bonusUn3 = new()
            {
                ability = vesselUn3.GenerateEnemyAbility().ability,
                rarity = Rarity.Uncommon,
            };

            UnenlightenAbilities._abilitiesToSwap =
            [
                bonusUn1,
                bonusUn2,
                bonusUn3,
            ];

            enlightenedvessel.AddEnemyAbilities(
            [
                vesselUn1.GenerateEnemyAbility(true),
                vesselUn2.GenerateEnemyAbility(true),
                vesselUn3.GenerateEnemyAbility(true),
            ]);

            SwapToOneSideEffect SwapLeft = ScriptableObject.CreateInstance<SwapToOneSideEffect>();
            SwapLeft._swapRight = false;

            SwapToOneSideEffect SwapRight = ScriptableObject.CreateInstance<SwapToOneSideEffect>();
            SwapRight._swapRight = true;

            AnimationVisualsEffect UnmakeAnim = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            UnmakeAnim._visuals = Visuals.Excommunicate;
            UnmakeAnim._animationTarget = Targeting.Slot_Front;

            HealthThresholdCheckEffect HealthAtOrLower = ScriptableObject.CreateInstance<HealthThresholdCheckEffect>();
            HealthAtOrLower._aboveThreshold = false;
            HealthAtOrLower._usePreviousAsMult = false;
            HealthAtOrLower._failIfMax = true;

            DirectDeathEffect Obliterate = ScriptableObject.CreateInstance<DirectDeathEffect>();
            Obliterate._obliterationDeath = true;
            Obliterate._killUnderMaxHealth = false;
            Obliterate._ExitValueIsHealthRemaining = false;

            Ability spiritAttackL = new Ability("Unstitch the Shell", "AApocrypha_EnlightenedSpiritLeftAttack_A")
            {
                Description = "Move Left.\nIf the Opposing party member's current health is 5 or lower and they are not at full health, unmake them.\nOtherwise, apply 2 Frail to the Opposing party member and Heal them.",
                Cost = [Pigments.Red, Pigments.Red],
                Effects =
                [
                    Effects.GenerateEffect(SwapLeft, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(UnmakeAnim, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(HealthAtOrLower, 5, Targeting.Slot_Front),
                    Effects.GenerateEffect(Obliterate, 1, Targeting.Slot_Front, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(FrailApply, 2, Targeting.Slot_Front, Effects.CheckPreviousEffectCondition(false, 2)),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<HealEffect>(), 5, Targeting.Slot_Front, Effects.CheckPreviousEffectCondition(false, 3)),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.Slow,
            };
            spiritAttackL.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Swap_Left)]);
            spiritAttackL.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Misc_Hidden), nameof(IntentType_GameIDs.Damage_Death), nameof(IntentType_GameIDs.Status_Frail), nameof(IntentType_GameIDs.Heal_5_10)]);

            Ability spiritAttackR = new Ability("Unmake the Skin", "AApocrypha_EnlightenedSpiritRightAttack_A")
            {
                Description = "Move Right.\nIf the Opposing party member's current health is 5 or lower and they are not at full health, unmake them.\nOtherwise, apply 2 Frail to the Opposing party member and Heal them.",
                Cost = [Pigments.Red, Pigments.Red],
                Effects =
                [
                    Effects.GenerateEffect(SwapRight, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(UnmakeAnim, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(HealthAtOrLower, 5, Targeting.Slot_Front),
                    Effects.GenerateEffect(Obliterate, 1, Targeting.Slot_Front, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(FrailApply, 2, Targeting.Slot_Front, Effects.CheckPreviousEffectCondition(false, 2)),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<HealEffect>(), 5, Targeting.Slot_Front, Effects.CheckPreviousEffectCondition(false, 3)),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.Slow,
            };
            spiritAttackR.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Swap_Right)]);
            spiritAttackR.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Misc_Hidden), nameof(IntentType_GameIDs.Damage_Death), nameof(IntentType_GameIDs.Status_Frail), nameof(IntentType_GameIDs.Heal_5_10)]);

            enlightenedspirit.AddEnemyAbilities(
            [
                spiritAttackL.GenerateEnemyAbility(true),
                spiritAttackR.GenerateEnemyAbility(true),
            ]);

            enlightenedvessel.AddEnemy(false, false, false);
            enlightenedspirit.AddEnemy(false, false, false);
        }
    }
}
