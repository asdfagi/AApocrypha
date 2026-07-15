using System;
using System.Collections.Generic;
using System.Text;
using A_Apocrypha.CustomOther;

namespace A_Apocrypha.Fools
{
    public class AmbroseCharacter
    {
        public static void Add()
        {
            CasterStoredValueChangeWithMaxEffect TabletDecrement = ScriptableObject.CreateInstance<CasterStoredValueChangeWithMaxEffect>();
            TabletDecrement.m_unitStoredDataID = "LeadTabletStoredValue";
            TabletDecrement._minimumValue = 0;
            TabletDecrement._maximumValue = 100;
            TabletDecrement._exitValueIsChange = false;
            TabletDecrement._increase = false;
            TabletDecrement._randomBetweenPrevious = false;
            TabletDecrement._usePreviousExitValue = false;
            TabletDecrement._useFixedValue = true;
            TabletDecrement._fixedValue = 1;

            CasterStoredValueChangeWithMaxEncounterBonusEffect TabletIncrease = ScriptableObject.CreateInstance<CasterStoredValueChangeWithMaxEncounterBonusEffect>();
            TabletIncrease.m_unitStoredDataID = "LeadTabletStoredValue";
            TabletIncrease._minimumValue = 0;
            TabletIncrease._maximumValue = 5;
            TabletIncrease._exitValueIsChange = false;
            TabletIncrease._increase = true;
            TabletIncrease._randomBetweenPrevious = false;
            TabletIncrease._usePreviousExitValue = false;

            ExtraVariableForNext_SVEffect TabletGet = ScriptableObject.CreateInstance<ExtraVariableForNext_SVEffect>();
            TabletGet.m_unitStoredDataID = "LeadTabletStoredValue";

            PreviousComparatorCheckEffect OnePlus = ScriptableObject.CreateInstance<PreviousComparatorCheckEffect>();
            OnePlus._atOrAbove = true;
            OnePlus._entryIsComparator = false;
            OnePlus._fixedComparator = 1;

            PreviousComparatorCheckEffect SevenPlus = ScriptableObject.CreateInstance<PreviousComparatorCheckEffect>();
            SevenPlus._atOrAbove = true;
            SevenPlus._entryIsComparator = false;
            SevenPlus._fixedComparator = 7;

            CasterHasStatusEffectorCondition IsSmoulding = ScriptableObject.CreateInstance<CasterHasStatusEffectorCondition>();
            IsSmoulding._passIfTrue = true;
            IsSmoulding._statusID = "Smouldering_ID";

            PerformEffectPassiveAbility ambroseTabletPassive = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
            ambroseTabletPassive.name = "AmbroseTabletHandler_PA";
            ambroseTabletPassive._passiveName = "Leadwright (5)";
            ambroseTabletPassive.m_PassiveID = "AmbroseTabletHandler";
            ambroseTabletPassive.passiveIcon = ResourceLoader.LoadSprite("IconLeadwright");
            ambroseTabletPassive._characterDescription = "Dr. Ambrose will begin each battle with 5 lead tablets, or more in special encounters. They are spent when performing his abilities, which will apply 1 Smouldering to him if he runs out of tablets.";
            ambroseTabletPassive._enemyDescription = "this enemy can smoke a fat blunt, yo";
            ambroseTabletPassive._triggerOn = [TriggerCalls.OnBeforeCombatStart];
            ambroseTabletPassive.conditions = [];
            ambroseTabletPassive.doesPassiveTriggerInformationPanel = false;
            ambroseTabletPassive.effects =
            [
                Effects.GenerateEffect(TabletIncrease, 100),
            ];
            ambroseTabletPassive.specialStoredData = UnitStoreData.GetCustom_UnitStoreData("LeadTabletStoredValue");

            PerformEffectPassiveAbility ambroseFireSafety = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
            ambroseFireSafety.name = "AmbroseFireSafety_3_PA";
            ambroseFireSafety._passiveName = "Fire Safety (3)";
            ambroseFireSafety.m_PassiveID = "AA_FireSafety_PA";
            ambroseFireSafety.passiveIcon = ResourceLoader.LoadSprite("IconFireSafety");
            ambroseFireSafety._characterDescription = "If Dr. Ambrose has 3 or more Smouldering on round end, reduce it by 1 before it triggers.";
            ambroseFireSafety._enemyDescription = "If this enemy has 3 or more Smouldering on round end, reduce it by 1 before it triggers.";
            ambroseFireSafety._triggerOn = [TriggerCalls.OnRoundFinished];
            ambroseFireSafety.conditions = [IsSmoulding];
            ambroseFireSafety.effects = []; // actual effect is handled by Smouldering.cs - this is just for the popup

            Character ambrose = new Character("Dr. Ambrose", "Ambrose_CH")
            {
                HealthColor = Pigments.Purple,
                UsesBasicAbility = true,
                UsesAllAbilities = false,
                MovesOnOverworld = true,
                FrontSprite = ResourceLoader.LoadSprite("AmbroseFront", new Vector2(0.5f, 0f), 32),
                BackSprite = ResourceLoader.LoadSprite("AmbroseBack", new Vector2(0.5f, 0f), 32),
                OverworldSprite = ResourceLoader.LoadSprite("AmbroseOverworld", new Vector2(0.5f, 0f), 32),
                DamageSound = LoadedAssetsHandler.GetCharacter("Clive_CH").damageSound,
                DeathSound = LoadedAssetsHandler.GetCharacter("Clive_CH").deathSound,
                DialogueSound = LoadedAssetsHandler.GetCharacter("Clive_CH").dxSound,
                UnitTypes = ["MaleID", "Sandwich_Fire", "Neathy"],
            };
            ambrose.GenerateMenuCharacter(ResourceLoader.LoadSprite("AmbroseMenu"), ResourceLoader.LoadSprite("AmbroseLocked"));
            ambrose.AddPassives([ambroseTabletPassive, ambroseFireSafety]);
            ambrose.SetMenuCharacterAsFullDPS();

            AnimationVisualsEffect MeltVisuals = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            MeltVisuals._animationTarget = Targeting.Slot_SelfSlot;
            MeltVisuals._visuals = Visuals.Melt;

            PerformEffectViaSubaction MeltTablet = ScriptableObject.CreateInstance<PerformEffectViaSubaction>();
            MeltTablet.effects = [
                Effects.GenerateEffect(TabletGet),
                Effects.GenerateEffect(OnePlus),
                Effects.GenerateEffect(MeltVisuals, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 1)),
                Effects.GenerateEffect(TabletDecrement, 100, Targeting.Slot_SelfSlot),
            ];

            AnimationVisualsEffect AutoBurnVisuals = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            AutoBurnVisuals._animationTarget = Targeting.Slot_SelfSlot;
            AutoBurnVisuals._visuals = Visuals.Melt;

            StatusEffect_Apply_Effect Smould = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            Smould._Status = StatusField.GetCustomStatusEffect("Smouldering_ID");

            StatusEffect_ApplyFixedAmount_Effect Smould1 = ScriptableObject.CreateInstance<StatusEffect_ApplyFixedAmount_Effect>();
            Smould1._Status = Smould._Status;
            Smould1._fixedAmount = 1;

            DamageOfTypeEffect BurnDamage = ScriptableObject.CreateInstance<DamageOfTypeEffect>();
            BurnDamage._indirect = true;
            BurnDamage._DamageTypeID = CombatType_GameIDs.Dmg_Fire.ToString();
            BurnDamage._DeathTypeID = DeathType_GameIDs.Obliteration.ToString();

            DamageWithStatusBonusEffect SmouldBoostedDamage = ScriptableObject.CreateInstance<DamageWithStatusBonusEffect>();
            SmouldBoostedDamage._status = StatusField.GetCustomStatusEffect("Smouldering_ID");
            SmouldBoostedDamage._bonusAmount = 1;
            SmouldBoostedDamage._bonusStacking = true;

            TargetStatusCheckEffect SmouldChecker = ScriptableObject.CreateInstance<TargetStatusCheckEffect>();
            SmouldChecker._status = StatusField.GetCustomStatusEffect("Smouldering_ID");

            RemoveStatusEffectEffect Extinguish = ScriptableObject.CreateInstance<RemoveStatusEffectEffect>();
            Extinguish._status = StatusField.GetCustomStatusEffect("Smouldering_ID");

            Ability sigil1 = new Ability("Red Sigil", "AmbroseSigil_1_A")
            {
                Description = "Deal 7 damage and apply 1 Smouldering to the Opposing enemy." +
                //"\nIf this party member has no lead tablets remaining, apply 1 Smouldering to them." +
                "\n75% chance to melt one lead tablet.",
                AbilitySprite = ResourceLoader.LoadSprite("IconAmbroseSigil"),
                Cost = [Pigments.Red, Pigments.Red, Pigments.Yellow],
                Visuals = Visuals.Torched,
                AnimationTarget = Targeting.Slot_Front,
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 7, Targeting.Slot_Front),
                    Effects.GenerateEffect(Smould, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(TabletGet),
                    Effects.GenerateEffect(OnePlus),
                    Effects.GenerateEffect(Smould1, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 1)),
                    Effects.GenerateEffect(MeltTablet, 1, Targeting.Slot_SelfSlot, Effects.ChanceCondition(75)),
                ],
                UnitStoreData = UnitStoreData.GetCustom_UnitStoreData("LeadTabletStoredValue"),
            };
            sigil1.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Damage_7_10), "Status_Smouldering"]);
            sigil1.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Misc_Hidden), "Status_Smouldering"]);

            Ability sigil2 = new Ability("Bloody Sigil", "AmbroseSigil_2_A")
            {
                Description = "Deal 10 damage and apply 1 Smouldering to the Opposing enemy." +
                //"\nIf this party member has no lead tablets remaining, apply 1 Smouldering to them." +
                "\n60% chance to melt one lead tablet.",
                AbilitySprite = ResourceLoader.LoadSprite("IconAmbroseSigil"),
                Cost = [Pigments.Red, Pigments.Red, Pigments.Yellow],
                Visuals = Visuals.Torched,
                AnimationTarget = Targeting.Slot_Front,
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 10, Targeting.Slot_Front),
                    Effects.GenerateEffect(Smould, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(TabletGet),
                    Effects.GenerateEffect(OnePlus),
                    Effects.GenerateEffect(Smould1, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 1)),
                    Effects.GenerateEffect(MeltTablet, 1, Targeting.Slot_SelfSlot, Effects.ChanceCondition(60)),
                ],
                UnitStoreData = UnitStoreData.GetCustom_UnitStoreData("LeadTabletStoredValue"),
            };
            sigil2.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Damage_7_10), "Status_Smouldering"]);
            sigil2.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Misc_Hidden), "Status_Smouldering"]);

            Ability sigil3 = new Ability("Crimson Sigil", "AmbroseSigil_3_A")
            {
                Description = "Deal 12 damage and apply 2 Smouldering to the Opposing enemy." +
                //"\nIf this party member has no lead tablets remaining, apply 1 Smouldering to them." +
                "\n45% chance to melt one lead tablet.",
                AbilitySprite = ResourceLoader.LoadSprite("IconAmbroseSigil"),
                Cost = [Pigments.Red, Pigments.Red, Pigments.YellowRed],
                Visuals = Visuals.Torched,
                AnimationTarget = Targeting.Slot_Front,
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 12, Targeting.Slot_Front),
                    Effects.GenerateEffect(Smould, 2, Targeting.Slot_Front),
                    Effects.GenerateEffect(TabletGet),
                    Effects.GenerateEffect(OnePlus),
                    Effects.GenerateEffect(Smould1, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 1)),
                    Effects.GenerateEffect(MeltTablet, 1, Targeting.Slot_SelfSlot, Effects.ChanceCondition(45)),
                ],
                UnitStoreData = UnitStoreData.GetCustom_UnitStoreData("LeadTabletStoredValue"),
            };
            sigil3.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Damage_11_15), "Status_Smouldering"]);
            sigil3.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Misc_Hidden), "Status_Smouldering"]);

            Ability sigil4 = new Ability("Violant Sigil", "AmbroseSigil_4_A")
            {
                Description = "Deal 15 damage and apply 2 Smouldering to the Opposing enemy." +
                //"\nIf this party member has no lead tablets remaining, apply 1 Smouldering to them." +
                "\n30% chance to melt one lead tablet.",
                AbilitySprite = ResourceLoader.LoadSprite("IconAmbroseSigil"),
                Cost = [Pigments.Red, Pigments.YellowRed, Pigments.YellowRed],
                Visuals = Visuals.Torched,
                AnimationTarget = Targeting.Slot_Front,
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 15, Targeting.Slot_Front),
                    Effects.GenerateEffect(Smould, 2, Targeting.Slot_Front),
                    Effects.GenerateEffect(TabletGet),
                    Effects.GenerateEffect(OnePlus),
                    Effects.GenerateEffect(Smould1, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 1)),
                    Effects.GenerateEffect(MeltTablet, 1, Targeting.Slot_SelfSlot, Effects.ChanceCondition(30)),
                ],
                UnitStoreData = UnitStoreData.GetCustom_UnitStoreData("LeadTabletStoredValue"),
            };
            sigil4.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Damage_11_15), "Status_Smouldering"]);
            sigil4.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Misc_Hidden), "Status_Smouldering"]);

            Ability experiment1 = new Ability("Ordinary Experiment", "AmbroseExperiment_1_A")
            {
                Description = "Deal 4 damage to the Left and Right enemies, increased by the amount of Smouldering they have, then apply 1 Smouldering to them." +
                //"\nIf this party member has no lead tablets remaining, apply 1 Smouldering to them." +
                "\n100% chance to melt one lead tablet.",
                AbilitySprite = ResourceLoader.LoadSprite("IconAmbroseExperiment"),
                Cost = [Pigments.Red, Pigments.Red, Pigments.Red],
                Visuals = Visuals.Pyre,
                AnimationTarget = Targeting.Slot_OpponentSides,
                Effects =
                [
                    Effects.GenerateEffect(SmouldBoostedDamage, 4, Targeting.Slot_OpponentSides),
                    Effects.GenerateEffect(Smould, 1, Targeting.Slot_OpponentSides),
                    Effects.GenerateEffect(TabletGet),
                    Effects.GenerateEffect(OnePlus),
                    Effects.GenerateEffect(Smould1, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 1)),
                    Effects.GenerateEffect(MeltTablet, 1, Targeting.Slot_SelfSlot, Effects.ChanceCondition(100)),
                ],
                UnitStoreData = UnitStoreData.GetCustom_UnitStoreData("LeadTabletStoredValue"),
            };
            experiment1.AddIntentsToTarget(Targeting.Slot_OpponentSides, [nameof(IntentType_GameIDs.Damage_3_6), "Status_Smouldering"]);
            experiment1.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Misc_Hidden), "Status_Smouldering"]);

            Ability experiment2 = new Ability("Adventurous Experiment", "AmbroseExperiment_2_A")
            {
                Description = "Deal 5 damage to the Left and Right enemies, increased by the amount of Smouldering they have, then apply 1 Smouldering to them." +
                //"\nIf this party member has no lead tablets remaining, apply 1 Smouldering to them." +
                "\n90% chance to melt one lead tablet.",
                AbilitySprite = ResourceLoader.LoadSprite("IconAmbroseExperiment"),
                Cost = [Pigments.Red, Pigments.Red, Pigments.Red],
                Visuals = Visuals.Pyre,
                AnimationTarget = Targeting.Slot_OpponentSides,
                Effects =
                [
                    Effects.GenerateEffect(SmouldBoostedDamage, 5, Targeting.Slot_OpponentSides),
                    Effects.GenerateEffect(Smould, 1, Targeting.Slot_OpponentSides),
                    Effects.GenerateEffect(TabletGet),
                    Effects.GenerateEffect(OnePlus),
                    Effects.GenerateEffect(Smould1, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 1)),
                    Effects.GenerateEffect(MeltTablet, 1, Targeting.Slot_SelfSlot, Effects.ChanceCondition(90)),
                ],
                UnitStoreData = UnitStoreData.GetCustom_UnitStoreData("LeadTabletStoredValue"),
            };
            experiment2.AddIntentsToTarget(Targeting.Slot_OpponentSides, [nameof(IntentType_GameIDs.Damage_3_6), "Status_Smouldering"]);
            experiment2.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Misc_Hidden), "Status_Smouldering"]);

            Ability experiment3 = new Ability("Groundbreaking Experiment", "AmbroseExperiment_3_A")
            {
                Description = "Deal 6 damage to the Left and Right enemies, increased by the amount of Smouldering they have, then apply 2 Smouldering to them." +
                //"\nIf this party member has no lead tablets remaining, apply 1 Smouldering to them." +
                "\n80% chance to melt one lead tablet.",
                AbilitySprite = ResourceLoader.LoadSprite("IconAmbroseExperiment"),
                Cost = [Pigments.Red, Pigments.Red, Pigments.Red],
                Visuals = Visuals.Pyre,
                AnimationTarget = Targeting.Slot_OpponentSides,
                Effects =
                [
                    Effects.GenerateEffect(SmouldBoostedDamage, 6, Targeting.Slot_OpponentSides),
                    Effects.GenerateEffect(Smould, 2, Targeting.Slot_OpponentSides),
                    Effects.GenerateEffect(TabletGet),
                    Effects.GenerateEffect(OnePlus),
                    Effects.GenerateEffect(Smould1, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 1)),
                    Effects.GenerateEffect(MeltTablet, 1, Targeting.Slot_SelfSlot, Effects.ChanceCondition(80)),
                ],
                UnitStoreData = UnitStoreData.GetCustom_UnitStoreData("LeadTabletStoredValue"),
            };
            experiment3.AddIntentsToTarget(Targeting.Slot_OpponentSides, [nameof(IntentType_GameIDs.Damage_3_6), "Status_Smouldering"]);
            experiment3.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Misc_Hidden), "Status_Smouldering"]);

            Ability experiment4 = new Ability("Tragic Experiment", "AmbroseExperiment_4_A")
            {
                Description = "Deal 7 damage to the Left and Right enemies, increased by the amount of Smouldering they have, then apply 2 Smouldering to them." +
                //"\nIf this party member has no lead tablets remaining, apply 1 Smouldering to them." +
                "\n70% chance to melt one lead tablet.",
                AbilitySprite = ResourceLoader.LoadSprite("IconAmbroseExperiment"),
                Cost = [Pigments.Red, Pigments.Red, Pigments.Red],
                Visuals = Visuals.Pyre,
                AnimationTarget = Targeting.Slot_OpponentSides,
                Effects =
                [
                    Effects.GenerateEffect(SmouldBoostedDamage, 7, Targeting.Slot_OpponentSides),
                    Effects.GenerateEffect(Smould, 2, Targeting.Slot_OpponentSides),
                    Effects.GenerateEffect(TabletGet),
                    Effects.GenerateEffect(OnePlus),
                    Effects.GenerateEffect(Smould1, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 1)),
                    Effects.GenerateEffect(MeltTablet, 1, Targeting.Slot_SelfSlot, Effects.ChanceCondition(70)),
                ],
                UnitStoreData = UnitStoreData.GetCustom_UnitStoreData("LeadTabletStoredValue"),
            };
            experiment4.AddIntentsToTarget(Targeting.Slot_OpponentSides, [nameof(IntentType_GameIDs.Damage_7_10), "Status_Smouldering"]);
            experiment4.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Misc_Hidden), "Status_Smouldering"]);

            Ability science1 = new Ability("Inane Science", "AmbroseScience_1_A")
            {
                Description = "Apply 2 Smouldering to the Opposing enemy. If the Opposing enemy already had 7 stacks of Smouldering, instead deal 7 damage to the Opposing enemy and remove all Smouldering from them." +
                //"\nIf this party member has no lead tablets remaining, apply 1 Smouldering to them." +
                "\n100% chance to melt one lead tablet if direct damage was dealt.",
                AbilitySprite = ResourceLoader.LoadSprite("IconAmbroseScience"),
                Cost = [Pigments.RedPurple],
                Visuals = Visuals.Equal,
                AnimationTarget = Targeting.Slot_Front,
                Effects =
                [
                    Effects.GenerateEffect(SmouldChecker, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(SevenPlus),
                    Effects.GenerateEffect(Smould, 2, Targeting.Slot_Front, Effects.CheckPreviousEffectCondition(false, 1)),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 7, Targeting.Slot_Front, Effects.CheckPreviousEffectCondition(true, 2)),
                    Effects.GenerateEffect(Extinguish, 1, Targeting.Slot_Front, Effects.CheckPreviousEffectCondition(true, 3)),
                    Effects.GenerateEffect(TabletGet),
                    Effects.GenerateEffect(OnePlus),
                    Effects.GenerateEffect(Smould1, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 1)),
                    Effects.GenerateEffect(MeltTablet, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 5)),
                ],
                UnitStoreData = UnitStoreData.GetCustom_UnitStoreData("LeadTabletStoredValue"),
            };
            science1.AddIntentsToTarget(Targeting.Slot_Front, ["Status_Smouldering", nameof(IntentType_GameIDs.Damage_7_10), "Rem_Status_Smouldering"]);
            science1.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Misc_Hidden), "Status_Smouldering"]);

            Ability science2 = new Ability("Bizarre Science", "AmbroseScience_2_A")
            {
                Description = "Apply 3 Smouldering to the Opposing enemy. If the Opposing enemy already had 7 stacks of Smouldering, instead deal 9 damage to the Opposing enemy and remove all Smouldering from them." +
                //"\nIf this party member has no lead tablets remaining, apply 1 Smouldering to them." +
                "\n100% chance to melt one lead tablet if direct damage was dealt.",
                AbilitySprite = ResourceLoader.LoadSprite("IconAmbroseScience"),
                Cost = [Pigments.RedPurple],
                Visuals = Visuals.Equal,
                AnimationTarget = Targeting.Slot_Front,
                Effects =
                [
                    Effects.GenerateEffect(SmouldChecker, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(SevenPlus),
                    Effects.GenerateEffect(Smould, 3, Targeting.Slot_Front, Effects.CheckPreviousEffectCondition(false, 1)),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 9, Targeting.Slot_Front, Effects.CheckPreviousEffectCondition(true, 2)),
                    Effects.GenerateEffect(Extinguish, 1, Targeting.Slot_Front, Effects.CheckPreviousEffectCondition(true, 3)),
                    Effects.GenerateEffect(TabletGet),
                    Effects.GenerateEffect(OnePlus),
                    Effects.GenerateEffect(Smould1, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 1)),
                    Effects.GenerateEffect(MeltTablet, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 5)),
                ],
                UnitStoreData = UnitStoreData.GetCustom_UnitStoreData("LeadTabletStoredValue"),
            };
            science2.AddIntentsToTarget(Targeting.Slot_Front, ["Status_Smouldering", nameof(IntentType_GameIDs.Damage_7_10), "Rem_Status_Smouldering"]);
            science2.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Misc_Hidden), "Status_Smouldering"]);

            Ability science3 = new Ability("Occult Science", "AmbroseScience_3_A")
            {
                Description = "Apply 4 Smouldering to the Opposing enemy. If the Opposing enemy already had 7 stacks of Smouldering, instead deal 11 damage to the Opposing enemy and remove all Smouldering from them." +
                //"\nIf this party member has no lead tablets remaining, apply 1 Smouldering to them." +
                "\n100% chance to melt one lead tablet if direct damage was dealt.",
                AbilitySprite = ResourceLoader.LoadSprite("IconAmbroseScience"),
                Cost = [Pigments.RedPurple],
                Visuals = Visuals.Equal,
                AnimationTarget = Targeting.Slot_Front,
                Effects =
                [
                    Effects.GenerateEffect(SmouldChecker, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(SevenPlus),
                    Effects.GenerateEffect(Smould, 4, Targeting.Slot_Front, Effects.CheckPreviousEffectCondition(false, 1)),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 11, Targeting.Slot_Front, Effects.CheckPreviousEffectCondition(true, 2)),
                    Effects.GenerateEffect(Extinguish, 1, Targeting.Slot_Front, Effects.CheckPreviousEffectCondition(true, 3)),
                    Effects.GenerateEffect(TabletGet),
                    Effects.GenerateEffect(OnePlus),
                    Effects.GenerateEffect(Smould1, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 1)),
                    Effects.GenerateEffect(MeltTablet, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 5)),
                ],
                UnitStoreData = UnitStoreData.GetCustom_UnitStoreData("LeadTabletStoredValue"),
            };
            science3.AddIntentsToTarget(Targeting.Slot_Front, ["Status_Smouldering", nameof(IntentType_GameIDs.Damage_11_15), "Rem_Status_Smouldering"]);
            science3.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Misc_Hidden), "Status_Smouldering"]);

            Ability science4 = new Ability("Red Science", "AmbroseScience_4_A")
            {
                Description = "Apply 5 Smouldering to the Opposing enemy. If the Opposing enemy already had 7 stacks of Smouldering, instead deal 13 damage to the Opposing enemy and remove all Smouldering from them." +
                //"\nIf this party member has no lead tablets remaining, apply 1 Smouldering to them." +
                "\n100% chance to melt one lead tablet if direct damage was dealt.",
                AbilitySprite = ResourceLoader.LoadSprite("IconAmbroseScience"),
                Cost = [Pigments.RedPurple],
                Visuals = Visuals.Equal,
                AnimationTarget = Targeting.Slot_Front,
                Effects =
                [
                    Effects.GenerateEffect(SmouldChecker, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(SevenPlus),
                    Effects.GenerateEffect(Smould, 5, Targeting.Slot_Front, Effects.CheckPreviousEffectCondition(false, 1)),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 13, Targeting.Slot_Front, Effects.CheckPreviousEffectCondition(true, 2)),
                    Effects.GenerateEffect(Extinguish, 1, Targeting.Slot_Front, Effects.CheckPreviousEffectCondition(true, 3)),
                    Effects.GenerateEffect(TabletGet),
                    Effects.GenerateEffect(OnePlus),
                    Effects.GenerateEffect(Smould1, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 1)),
                    Effects.GenerateEffect(MeltTablet, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 5)),
                ],
                UnitStoreData = UnitStoreData.GetCustom_UnitStoreData("LeadTabletStoredValue"),
            };
            science4.AddIntentsToTarget(Targeting.Slot_Front, ["Status_Smouldering", nameof(IntentType_GameIDs.Damage_11_15), "Rem_Status_Smouldering"]);
            science4.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Misc_Hidden), "Status_Smouldering"]);

            ambrose.AddLevelData(13, [experiment1, sigil1, science1]);
            ambrose.AddLevelData(15, [experiment2, sigil2, science2]);
            ambrose.AddLevelData(17, [experiment3, sigil3, science3]);
            ambrose.AddLevelData(19, [experiment4, sigil4, science4]);

            ambrose.AddFinalBossAchievementData(BossType_GameIDs.OsmanSinnoks.ToString(), "AApocrypha_Ambrose_Witness_ACH");
            ambrose.AddFinalBossAchievementData(BossType_GameIDs.Heaven.ToString(), "AApocrypha_Ambrose_Divine_ACH");
            if (AApocrypha.CrossMod.EnemyPack) { ambrose.AddFinalBossAchievementData("DoulaBoss", "AApocrypha_Ambrose_Abstraction_ACH"); }
            if (AApocrypha.CrossMod.GlitchsFreaks) { ambrose.AddFinalBossAchievementData("March_BOSS", "AApocrypha_Ambrose_Inevitable_ACH"); }
            if (AApocrypha.CrossMod.IntoTheAbyss) { ambrose.AddFinalBossAchievementData("Nobody_BOSS", "AApocrypha_Ambrose_Forgotten_ACH"); }
            //if (AApocrypha.CrossMod.IntoTheAbyss) { ambrose.AddFinalBossAchievementData("Katalixi_BOSS", "AApocrypha_Ambrose_Boundary_ACH"); }
            //if (AApocrypha.CrossMod.SaltEnemies) { ambrose.AddFinalBossAchievementData("BlueSky_BOSS", "AApocrypha_Ambrose_Dreamer_ACH"); }
            ambrose.AddCharacter(true, false);

            SpeakerBundle speakerBundleAmbrose = new SpeakerBundle();
            speakerBundleAmbrose.bundleTextColor = new Color32(144, 0, 0, 255);
            speakerBundleAmbrose.dialogueSound = LoadedAssetsHandler.GetCharacter("Ambrose_CH").dxSound;
            speakerBundleAmbrose.portrait = ResourceLoader.LoadSprite("AmbroseTalk", new Vector2(0.5f, 0f), 32);
            var dia = Dialogues.CreateAndAddCustom_SpeakerData("Ambrose", speakerBundleAmbrose, true, false, new SpeakerEmote[0]);

            SpeakerBundle speakerBundleAmbroseMain = new SpeakerBundle();
            speakerBundleAmbroseMain.bundleTextColor = new Color32(144, 0, 0, 255);
            speakerBundleAmbroseMain.dialogueSound = LoadedAssetsHandler.GetCharacter("Ambrose_CH").dxSound;
            speakerBundleAmbroseMain.portrait = ResourceLoader.LoadSprite("AmbroseFront", new Vector2(0.5f, 0f), 32);
            var dia2 = Dialogues.CreateAndAddCustom_SpeakerData("AmbroseMain", speakerBundleAmbroseMain, false, false, new SpeakerEmote[0]);
        }
    }
}
