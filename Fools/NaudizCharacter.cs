using System;
using System.Collections.Generic;
using System.Text;
using A_Apocrypha.CustomOther;
using UnityEngine.Analytics;

namespace A_Apocrypha.Fools
{
    public class NaudizCharacter
    {
        public static void Add()
        {
            ExtraCCSprites_ArraySO NaudizAltSprites = ScriptableObject.CreateInstance<ExtraCCSprites_ArraySO>();
            NaudizAltSprites._doesLoop = true;
            NaudizAltSprites._DefaultID = "NaudizSpritesDefault";
            NaudizAltSprites._SpecialID = "NaudizSpritesSpecial";
            NaudizAltSprites._frontSprite = [
                ResourceLoader.LoadSprite("Naudiz4FrontHatless", new Vector2(0.5f, 0f), 32),
            ];
            NaudizAltSprites._backSprite = [
                ResourceLoader.LoadSprite("Naudiz4BackHatless", new Vector2(0.5f, 0f), 32),
            ];

            RemovePassiveEffect losehatpassive = ScriptableObject.CreateInstance<RemovePassiveEffect>();
            losehatpassive.m_PassiveID = "NaudizHatHandler";

            SetCasterExtraSpritesEffect losehat = ScriptableObject.CreateInstance<SetCasterExtraSpritesEffect>();
            losehat._ExtraSpriteID = "NaudizSpritesSpecial";

            ReturnValueComparatorEffectorCondition TenOrMore = ScriptableObject.CreateInstance<ReturnValueComparatorEffectorCondition>();
            TenOrMore._lessThan = false;
            TenOrMore._comparator = 10;

            PerformEffectPassiveAbility naudizCosmeticPassive = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
            naudizCosmeticPassive.name = "NaudizHatHandler_PA";
            naudizCosmeticPassive._passiveName = "Hat (10)";
            naudizCosmeticPassive.m_PassiveID = "NaudizHatHandler";
            naudizCosmeticPassive.passiveIcon = ResourceLoader.LoadSprite("NaudizHatIcon");
            naudizCosmeticPassive._characterDescription = "This party member has a hat.";
            naudizCosmeticPassive._enemyDescription = "This enemy has a hat.";
            naudizCosmeticPassive._triggerOn = [TriggerCalls.OnDamaged];
            naudizCosmeticPassive.conditions = [TenOrMore];
            naudizCosmeticPassive.doesPassiveTriggerInformationPanel = false;
            naudizCosmeticPassive.effects =
            [
                Effects.GenerateEffect(losehatpassive, 1, Targeting.Slot_SelfSlot),
                Effects.GenerateEffect(losehat, 1),
            ];

            CasterAnalysisStoreValueSetterEffect SetAnalysis = ScriptableObject.CreateInstance<CasterAnalysisStoreValueSetterEffect>();
            SetAnalysis.m_unitStoredDataID = "NaudizCurrentStoredValue";

            CasterAnalysisStoredValueCheckerEffect AnalysisNotNull = ScriptableObject.CreateInstance<CasterAnalysisStoredValueCheckerEffect>();
            AnalysisNotNull.m_unitStoredDataID = "NaudizCurrentStoredValue";

            CasterAnalysisIsUnitAliveCheckerEffect AnalysisAlive = ScriptableObject.CreateInstance<CasterAnalysisIsUnitAliveCheckerEffect>();
            AnalysisAlive.m_unitStoredDataID = "NaudizCurrentStoredValue";

            CasterStoreValueSetterAdvancedEffect AddKill = ScriptableObject.CreateInstance<CasterStoreValueSetterAdvancedEffect>();
            AddKill.m_unitStoredDataID = "NaudizKillStoredValue";
            AddKill._ignoreIfContains = false;
            AddKill.usePreviousExitValue = false;
            AddKill._increment = true;

            CasterStoreValueSetterAdvancedStringEffect WipeAnalysisTarget = ScriptableObject.CreateInstance<CasterStoreValueSetterAdvancedStringEffect>();
            WipeAnalysisTarget._stringData = "None";
            WipeAnalysisTarget.m_unitStoredDataID = "NaudizCurrentStoredValue";

            OpponentByAnalysisStoredValueTargeting AnalysisTarget = ScriptableObject.CreateInstance<OpponentByAnalysisStoredValueTargeting>();
            AnalysisTarget.targetUnitAllySlots = false;
            AnalysisTarget.getAllUnitSelfSlots = false;
            AnalysisTarget._storedValueID = "NaudizCurrentStoredValue";

            AnimationVisualsEffect AnalysisAnim = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            AnalysisAnim._visuals = Visuals.Providence;
            AnalysisAnim._animationTarget = AnalysisTarget;

            PerformDoubleEffectPassiveAbility naudizAnalyzer = ScriptableObject.CreateInstance<PerformDoubleEffectPassiveAbility>();
            naudizAnalyzer.name = "AnalyzerNaudiz_PA";
            naudizAnalyzer._passiveName = "Analyzer";
            naudizAnalyzer.m_PassiveID = "Analyzer";
            naudizAnalyzer.passiveIcon = ResourceLoader.LoadSprite("IconAnalyzer");
            naudizAnalyzer._characterDescription = "At the start of each turn, mark an enemy for analysis if none are marked. This selection is mostly random. Only one enemy can be marked for analysis at a time.";
            naudizAnalyzer._enemyDescription = "not intended for enemies.";
            naudizAnalyzer._triggerOn = [TriggerCalls.OnTurnStart];
            naudizAnalyzer.doesPassiveTriggerInformationPanel = true;
            naudizAnalyzer.effects =
            [
                Effects.GenerateEffect(AnalysisNotNull),
                Effects.GenerateEffect(SetAnalysis, 1, Targeting.Unit_AllOpponents, Effects.CheckPreviousEffectCondition(false, 1)),
                Effects.GenerateEffect(AnalysisAnim),
            ];
            naudizAnalyzer._secondTriggerOn = [TriggerCalls.OnOpponentHasDied];
            naudizAnalyzer._secondDoesPerformPopUp = true;
            naudizAnalyzer._secondEffects =
            [
                Effects.GenerateEffect(AnalysisAlive, 1, Targeting.Unit_AllOpponents),
                Effects.GenerateEffect(AnalysisNotNull),
                Effects.GenerateEffect(AddKill, 1, Targeting.Slot_SelfSlot, Effects.CheckMultiplePreviousEffectsCondition([true, false], [1, 2])),
                Effects.GenerateEffect(WipeAnalysisTarget, -5, Targeting.Slot_SelfSlot, Effects.CheckMultiplePreviousEffectsCondition([true, false], [2, 3])),
            ];
            naudizAnalyzer.specialStoredData = UnitStoreData.GetCustom_UnitStoreData("NaudizCurrentStoredValue");

            SpecificOpponentsNotOpposingTargeting ReconsiderTargeting = ScriptableObject.CreateInstance<SpecificOpponentsNotOpposingTargeting>();
            ReconsiderTargeting.slotOffsets = [0];
            ReconsiderTargeting.targetUnitAllySlots = false;
            ReconsiderTargeting.getAllUnitSelfSlots = false;

            Ability naudizslap = new Ability("Reconsider", "NaudizSlap_A")
            {
                Description = "Deal 1 damage to the Opposing enemy. If the Opposing enemy was marked for analysis, mark a random other enemy.",
                AbilitySprite = ResourceLoader.LoadSprite("IconNaudiz4Reconsider"),
                Visuals = Visuals.Slap,
                AnimationTarget = Targeting.Slot_Front,
                Cost = [Pigments.Yellow],
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<CheckHasAtLeastOneUnitEffect>(), 1, ReconsiderTargeting),
                    Effects.GenerateEffect(AnalysisAlive, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(SetAnalysis, 1, ReconsiderTargeting, Effects.CheckMultiplePreviousEffectsCondition([true, true], [1, 2])),
                    Effects.GenerateEffect(AnalysisAnim, 1, Targeting.Slot_SelfSlot, Effects.CheckMultiplePreviousEffectsCondition([true, true], [2, 3])),
                ],
                UnitStoreData = UnitStoreData.GetCustom_UnitStoreData("NaudizCurrentStoredValue"),
            };
            naudizslap.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Damage_1_2)]);
            naudizslap.AddIntentsToTarget(AnalysisTarget, [nameof(IntentType_GameIDs.Misc_Hidden)]);
            naudizslap.AddIntentsToTarget(ReconsiderTargeting, [nameof(IntentType_GameIDs.Misc)]);

            Character naudiz = new Character("Naudiz 4", "Naudiz4_CH")
            {
                HealthColor = Pigments.Grey,
                UsesBasicAbility = true,
                UsesAllAbilities = false,
                MovesOnOverworld = true,
                FrontSprite = ResourceLoader.LoadSprite("Naudiz4Front", new Vector2(0.5f, 0f), 32),
                BackSprite = ResourceLoader.LoadSprite("Naudiz4Back", new Vector2(0.5f, 0f), 32),
                OverworldSprite = ResourceLoader.LoadSprite("Naudiz4Overworld", new Vector2(0.5f, 0f), 32),
                DamageSound = "event:/AASFX/Fools/Naudiz4Hurt",
                DeathSound = "event:/AASFX/Fools/Naudiz4Die",
                DialogueSound = "event:/AASFX/Fools/Naudiz4Talk",
                ExtraSprites = NaudizAltSprites,
                BasicAbility = naudizslap,
                UnitTypes = [/*"Male_ID", */"Sandwich_Robot", "Robot"],
            };
            naudiz.GenerateMenuCharacter(ResourceLoader.LoadSprite("Naudiz4Menu"), ResourceLoader.LoadSprite("Naudiz4Locked"));
            naudiz.AddPassives([naudizAnalyzer, naudizCosmeticPassive]);
            naudiz.SetMenuCharacterAsFullSupport();
            //naudiz.ignoredDPS = [2];
            //naudiz.ignoredSupport = [0];

            AttackVisualsSO AnalysisVisuals = ITAVisuals.Divide;

            StatusEffect_Apply_Effect AddScars = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            AddScars._Status = StatusField.Scars;

            DamageEffect KillAsSuccessDamage = ScriptableObject.CreateInstance<DamageEffect>();
            KillAsSuccessDamage._returnKillAsSuccess = true;

            Ability analysis1 = new Ability("Unabridged Analysis", "NaudizAnalysis_1_A")
            {
                Description = "Deal 4 damage to the Opposing enemy. If the Opposing enemy is marked for analysis, this deals 6 damage instead." +
                "\nIf this damage kills, apply 1 Scar to all enemies.",
                AbilitySprite = ResourceLoader.LoadSprite("IconNaudiz4Analysis"),
                Visuals = AnalysisVisuals,
                AnimationTarget = Targeting.Slot_Front,
                Cost = [Pigments.Red, Pigments.Red],
                Effects =
                [
                    Effects.GenerateEffect(AnalysisAlive, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(KillAsSuccessDamage, 6, Targeting.Slot_Front, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(KillAsSuccessDamage, 4, Targeting.Slot_Front, Effects.CheckPreviousEffectCondition(false, 2)),
                    Effects.GenerateEffect(AddScars, 1, Targeting.Unit_AllOpponents, Effects.CheckPreviousEffectCondition(true, 2)),
                    Effects.GenerateEffect(AddScars, 1, Targeting.Unit_AllOpponents, Effects.CheckPreviousEffectCondition(true, 2)),
                ],
                UnitStoreData = UnitStoreData.GetCustom_UnitStoreData("NaudizCurrentStoredValue"),
            };
            analysis1.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Damage_3_6)]);
            analysis1.AddIntentsToTarget(AnalysisTarget, [nameof(IntentType_GameIDs.Misc_Hidden)]);
            analysis1.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Damage_3_6)]);
            analysis1.AddIntentsToTarget(Targeting.Unit_AllOpponents, [nameof(IntentType_GameIDs.Status_Scars)]);

            Ability analysis2 = new Ability("Broad-Spectrum Analysis", "NaudizAnalysis_2_A")
            {
                Description = "Deal 5 damage to the Opposing enemy. If the Opposing enemy is marked for analysis, this deals 8 damage instead." +
                "\nIf this damage kills, apply 1 Scar to all enemies.",
                AbilitySprite = analysis1.ability.abilitySprite,
                Visuals = AnalysisVisuals,
                AnimationTarget = Targeting.Slot_Front,
                Cost = [Pigments.Red, Pigments.Red],
                Effects =
                [
                    Effects.GenerateEffect(AnalysisAlive, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(KillAsSuccessDamage, 8, Targeting.Slot_Front, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(KillAsSuccessDamage, 5, Targeting.Slot_Front, Effects.CheckPreviousEffectCondition(false, 2)),
                    Effects.GenerateEffect(AddScars, 1, Targeting.Unit_AllOpponents, Effects.CheckPreviousEffectCondition(true, 2)),
                    Effects.GenerateEffect(AddScars, 1, Targeting.Unit_AllOpponents, Effects.CheckPreviousEffectCondition(true, 2)),
                ],
                UnitStoreData = UnitStoreData.GetCustom_UnitStoreData("NaudizCurrentStoredValue"),
            };
            analysis2.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Damage_3_6)]);
            analysis2.AddIntentsToTarget(AnalysisTarget, [nameof(IntentType_GameIDs.Misc_Hidden)]);
            analysis2.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Damage_7_10)]);
            analysis2.AddIntentsToTarget(Targeting.Unit_AllOpponents, [nameof(IntentType_GameIDs.Status_Scars)]);

            Ability analysis3 = new Ability("Calculated Analysis", "NaudizAnalysis_3_A")
            {
                Description = "Deal 6 damage to the Opposing enemy. If the Opposing enemy is marked for analysis, this deals 10 damage instead." +
                "\nIf this damage kills, apply 2 Scars to all enemies.",
                AbilitySprite = analysis1.ability.abilitySprite,
                Visuals = AnalysisVisuals,
                AnimationTarget = Targeting.Slot_Front,
                Cost = [Pigments.Red, Pigments.Red],
                Effects =
                [
                    Effects.GenerateEffect(AnalysisAlive, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(KillAsSuccessDamage, 10, Targeting.Slot_Front, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(KillAsSuccessDamage, 6, Targeting.Slot_Front, Effects.CheckPreviousEffectCondition(false, 2)),
                    Effects.GenerateEffect(AddScars, 2, Targeting.Unit_AllOpponents, Effects.CheckPreviousEffectCondition(true, 2)),
                    Effects.GenerateEffect(AddScars, 2, Targeting.Unit_AllOpponents, Effects.CheckPreviousEffectCondition(true, 2)),
                ],
                UnitStoreData = UnitStoreData.GetCustom_UnitStoreData("NaudizCurrentStoredValue"),
            };
            analysis3.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Damage_3_6)]);
            analysis3.AddIntentsToTarget(AnalysisTarget, [nameof(IntentType_GameIDs.Misc_Hidden)]);
            analysis3.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Damage_7_10)]);
            analysis3.AddIntentsToTarget(Targeting.Unit_AllOpponents, [nameof(IntentType_GameIDs.Status_Scars)]);

            Ability analysis4 = new Ability("Inerrant Analysis", "NaudizAnalysis_4_A")
            {
                Description = "Deal 7 damage to the Opposing enemy. If the Opposing enemy is marked for analysis, this deals 12 damage instead." +
                "\nIf this damage kills, apply 2 Scars to all enemies.",
                AbilitySprite = analysis1.ability.abilitySprite,
                Visuals = AnalysisVisuals,
                AnimationTarget = Targeting.Slot_Front,
                Cost = [Pigments.Red, Pigments.Red],
                Effects =
                [
                    Effects.GenerateEffect(AnalysisAlive, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(KillAsSuccessDamage, 12, Targeting.Slot_Front, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(KillAsSuccessDamage, 7, Targeting.Slot_Front, Effects.CheckPreviousEffectCondition(false, 2)),
                    Effects.GenerateEffect(AddScars, 2, Targeting.Unit_AllOpponents, Effects.CheckPreviousEffectCondition(true, 2)),
                    Effects.GenerateEffect(AddScars, 2, Targeting.Unit_AllOpponents, Effects.CheckPreviousEffectCondition(true, 2)),
                ],
                UnitStoreData = UnitStoreData.GetCustom_UnitStoreData("NaudizCurrentStoredValue"),
            };
            analysis4.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Damage_7_10)]);
            analysis4.AddIntentsToTarget(AnalysisTarget, [nameof(IntentType_GameIDs.Misc_Hidden)]);
            analysis4.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Damage_11_15)]);
            analysis4.AddIntentsToTarget(Targeting.Unit_AllOpponents, [nameof(IntentType_GameIDs.Status_Scars)]);

            HealByStoredValueMultiEffect healByKills = ScriptableObject.CreateInstance<HealByStoredValueMultiEffect>();
            healByKills.m_unitStoredDataID = "NaudizKillStoredValue";
            healByKills._defaultStoredValue = 0;
            healByKills._directHeal = true;

            Ability prepare1 = new Ability("Prepare for Observation", "NaudizPrepare_1_A")
            {
                Description = "Mark the Opposing enemy for analysis. Apply 1 Scar to them and produce 2 pigment of their health color." +
                "\nIf there is no Opposing enemy, heal this party member 1 health per enemy killed this combat while marked for analysis.",
                AbilitySprite = ResourceLoader.LoadSprite("IconNaudiz4Prepare"),
                Visuals = Visuals.Providence,
                AnimationTarget = Targeting.Slot_Front,
                Cost = [Pigments.Yellow],
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<CheckHasUnitEffect>(), 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(AnalysisAlive, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(SetAnalysis, 1, Targeting.Slot_Front, Effects.CheckMultiplePreviousEffectsCondition([false, true], [1, 2])),
                    Effects.GenerateEffect(AddScars, 1, Targeting.Slot_Front, Effects.CheckPreviousEffectCondition(true, 3)),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<GenerateHealthColorManaPerTargetEffect>(), 2, Targeting.Slot_Front, Effects.CheckPreviousEffectCondition(true, 4)),
                    Effects.GenerateEffect(healByKills, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 5)),
                ],
                UnitStoreData = UnitStoreData.GetCustom_UnitStoreData("NaudizKillStoredValue"),
            };
            prepare1.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Misc)]);
            prepare1.AddIntentsToTarget(AnalysisTarget, [nameof(IntentType_GameIDs.Misc_Hidden)]);
            prepare1.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Status_Scars), nameof(IntentType_GameIDs.Mana_Generate)]);
            prepare1.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Heal_1_4)]);

            Ability prepare2 = new Ability("Prepare for Examination", "NaudizPrepare_2_A")
            {
                Description = "Mark the Opposing enemy for analysis. Apply 2 Scars to them and produce 2 pigment of their health color." +
                "\nIf there is no Opposing enemy, heal this party member 1 health per enemy killed this combat while marked for analysis.",
                AbilitySprite = prepare1.ability.abilitySprite,
                Visuals = Visuals.Providence,
                AnimationTarget = Targeting.Slot_Front,
                Cost = [Pigments.YellowBlue],
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<CheckHasUnitEffect>(), 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(AnalysisAlive, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(SetAnalysis, 1, Targeting.Slot_Front, Effects.CheckMultiplePreviousEffectsCondition([false, true], [1, 2])),
                    Effects.GenerateEffect(AddScars, 2, Targeting.Slot_Front, Effects.CheckPreviousEffectCondition(true, 3)),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<GenerateHealthColorManaPerTargetEffect>(), 2, Targeting.Slot_Front, Effects.CheckPreviousEffectCondition(true, 4)),
                    Effects.GenerateEffect(healByKills, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 5)),
                ],
                UnitStoreData = UnitStoreData.GetCustom_UnitStoreData("NaudizKillStoredValue"),
            };
            prepare2.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Misc)]);
            prepare2.AddIntentsToTarget(AnalysisTarget, [nameof(IntentType_GameIDs.Misc_Hidden)]);
            prepare2.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Status_Scars), nameof(IntentType_GameIDs.Mana_Generate)]);
            prepare2.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Heal_1_4)]);

            Ability prepare3 = new Ability("Prepare for Dissection", "NaudizPrepare_3_A")
            {
                Description = "Mark the Opposing enemy for analysis. Apply 2 Scars to them and produce 2 pigment of their health color." +
                "\nIf there is no Opposing enemy, heal this party member 2 health per enemy killed this combat while marked for analysis.",
                AbilitySprite = prepare1.ability.abilitySprite,
                Visuals = Visuals.Providence,
                AnimationTarget = Targeting.Slot_Front,
                Cost = [Pigments.YellowBlue],
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<CheckHasUnitEffect>(), 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(AnalysisAlive, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(SetAnalysis, 1, Targeting.Slot_Front, Effects.CheckMultiplePreviousEffectsCondition([false, true], [1, 2])),
                    Effects.GenerateEffect(AddScars, 2, Targeting.Slot_Front, Effects.CheckPreviousEffectCondition(true, 3)),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<GenerateHealthColorManaPerTargetEffect>(), 2, Targeting.Slot_Front, Effects.CheckPreviousEffectCondition(true, 4)),
                    Effects.GenerateEffect(healByKills, 2, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 5)),
                ],
                UnitStoreData = UnitStoreData.GetCustom_UnitStoreData("NaudizKillStoredValue"),
            };
            prepare3.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Misc)]);
            prepare3.AddIntentsToTarget(AnalysisTarget, [nameof(IntentType_GameIDs.Misc_Hidden)]);
            prepare3.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Status_Scars), nameof(IntentType_GameIDs.Mana_Generate)]);
            prepare3.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Heal_1_4)]);

            Ability prepare4 = new Ability("Prepare for Vivisection", "NaudizPrepare_4_A")
            {
                Description = "Mark the Opposing enemy for analysis. Apply 3 Scars to them and produce 2 pigment of their health color." +
                "\nIf there is no Opposing enemy, heal this party member 2 health per enemy killed this combat while marked for analysis.",
                AbilitySprite = prepare1.ability.abilitySprite,
                Visuals = Visuals.Providence,
                AnimationTarget = Targeting.Slot_Front,
                Cost = [Pigments.YellowBlue],
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<CheckHasUnitEffect>(), 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(AnalysisAlive, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(SetAnalysis, 1, Targeting.Slot_Front, Effects.CheckMultiplePreviousEffectsCondition([false, true], [1, 2])),
                    Effects.GenerateEffect(AddScars, 3, Targeting.Slot_Front, Effects.CheckPreviousEffectCondition(true, 3)),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<GenerateHealthColorManaPerTargetEffect>(), 2, Targeting.Slot_Front, Effects.CheckPreviousEffectCondition(true, 4)),
                    Effects.GenerateEffect(healByKills, 2, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 5)),
                ],
                UnitStoreData = UnitStoreData.GetCustom_UnitStoreData("NaudizKillStoredValue"),
            };
            prepare4.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Misc)]);
            prepare4.AddIntentsToTarget(AnalysisTarget, [nameof(IntentType_GameIDs.Misc_Hidden)]);
            prepare4.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Status_Scars), nameof(IntentType_GameIDs.Mana_Generate)]);
            prepare4.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Heal_1_4)]);

            HealByStoredValueAdditiveEffect healPlusKills1 = ScriptableObject.CreateInstance<HealByStoredValueAdditiveEffect>();
            healPlusKills1.m_unitStoredDataID = "NaudizKillStoredValue";
            healPlusKills1._defaultStoredValue = 0;
            healPlusKills1._directHeal = true;
            healPlusKills1._modifier = 1;

            HealByStoredValueAdditiveEffect healPlusKills2 = ScriptableObject.CreateInstance<HealByStoredValueAdditiveEffect>();
            healPlusKills2.m_unitStoredDataID = "NaudizKillStoredValue";
            healPlusKills2._defaultStoredValue = 0;
            healPlusKills2._directHeal = true;
            healPlusKills2._modifier = 2;

            HealByStoredValueAdditiveEffect healPlusKills3 = ScriptableObject.CreateInstance<HealByStoredValueAdditiveEffect>();
            healPlusKills3.m_unitStoredDataID = "NaudizKillStoredValue";
            healPlusKills3._defaultStoredValue = 0;
            healPlusKills3._directHeal = true;
            healPlusKills3._modifier = 3;

            HealByStoredValueAdditiveEffect healPlusKills4 = ScriptableObject.CreateInstance<HealByStoredValueAdditiveEffect>();
            healPlusKills4.m_unitStoredDataID = "NaudizKillStoredValue";
            healPlusKills4._defaultStoredValue = 0;
            healPlusKills4._directHeal = true;
            healPlusKills4._modifier = 4;

            Ability share1 = new Ability("Share Plans", "NaudizShare_1_A")
            {
                Description = "Heal the Left and Right party members 3 health. Increase this healing by 1 per enemy killed this combat while marked for analysis.",
                AbilitySprite = ResourceLoader.LoadSprite("IconNaudiz4Share"),
                Cost = [Pigments.Blue, Pigments.RedBlue],
                Visuals = CustomVisuals.StaticVisualsSO,
                AnimationTarget = Targeting.Slot_AllySides,
                Effects =
                [
                    Effects.GenerateEffect(healPlusKills1, 3, Targeting.Slot_AllySides),
                ],
                UnitStoreData = UnitStoreData.GetCustom_UnitStoreData("NaudizKillStoredValue"),
            };
            share1.AddIntentsToTarget(AnalysisTarget, [nameof(IntentType_GameIDs.Misc_Hidden)]);
            share1.AddIntentsToTarget(Targeting.Slot_AllySides, [nameof(IntentType_GameIDs.Heal_1_4), nameof(IntentType_GameIDs.Heal_1_4)]);

            Ability share2 = new Ability("Share Findings", "NaudizShare_2_A")
            {
                Description = "Heal the Left and Right party members 4 health. Increase this healing by 2 per enemy killed this combat while marked for analysis.",
                AbilitySprite = share1.ability.abilitySprite,
                Cost = [Pigments.Blue, Pigments.RedBlue],
                Visuals = CustomVisuals.StaticVisualsSO,
                AnimationTarget = Targeting.Slot_AllySides,
                Effects =
                [
                    Effects.GenerateEffect(healPlusKills2, 4, Targeting.Slot_AllySides),
                ],
                UnitStoreData = UnitStoreData.GetCustom_UnitStoreData("NaudizKillStoredValue"),
            };
            share2.AddIntentsToTarget(AnalysisTarget, [nameof(IntentType_GameIDs.Misc_Hidden)]);
            share2.AddIntentsToTarget(Targeting.Slot_AllySides, [nameof(IntentType_GameIDs.Heal_1_4), nameof(IntentType_GameIDs.Heal_1_4)]);

            Ability share3 = new Ability("Share Facts", "NaudizShare_3_A")
            {
                Description = "Heal the Left and Right party members 5 health. Increase this healing by 3 per enemy killed this combat while marked for analysis.",
                AbilitySprite = share1.ability.abilitySprite,
                Cost = [Pigments.Blue, Pigments.RedBlue],
                Visuals = CustomVisuals.StaticVisualsSO,
                AnimationTarget = Targeting.Slot_AllySides,
                Effects =
                [
                    Effects.GenerateEffect(healPlusKills3, 5, Targeting.Slot_AllySides),
                ],
                UnitStoreData = UnitStoreData.GetCustom_UnitStoreData("NaudizKillStoredValue"),
            };
            share3.AddIntentsToTarget(AnalysisTarget, [nameof(IntentType_GameIDs.Misc_Hidden)]);
            share3.AddIntentsToTarget(Targeting.Slot_AllySides, [nameof(IntentType_GameIDs.Heal_5_10), nameof(IntentType_GameIDs.Heal_1_4)]);

            Ability share4 = new Ability("Share Truths", "NaudizShare_4_A")
            {
                Description = "Heal the Left and Right party members 6 health. Increase this healing by 4 per enemy killed this combat while marked for analysis.",
                AbilitySprite = share1.ability.abilitySprite,
                Cost = [Pigments.Blue, Pigments.RedBlue],
                Visuals = CustomVisuals.StaticVisualsSO,
                AnimationTarget = Targeting.Slot_AllySides,
                Effects =
                [
                    Effects.GenerateEffect(healPlusKills4, 6, Targeting.Slot_AllySides),
                ],
                UnitStoreData = UnitStoreData.GetCustom_UnitStoreData("NaudizKillStoredValue"),
            };
            share4.AddIntentsToTarget(AnalysisTarget, [nameof(IntentType_GameIDs.Misc_Hidden)]);
            share4.AddIntentsToTarget(Targeting.Slot_AllySides, [nameof(IntentType_GameIDs.Heal_5_10), nameof(IntentType_GameIDs.Heal_1_4)]);

            naudiz.AddLevelData(14, [analysis1, prepare1, share1]);
            naudiz.AddLevelData(17, [analysis2, prepare2, share2]);
            naudiz.AddLevelData(20, [analysis3, prepare3, share3]);
            naudiz.AddLevelData(23, [analysis4, prepare4, share4]);

            naudiz.AddFinalBossAchievementData(BossType_GameIDs.OsmanSinnoks.ToString(), "AApocrypha_Naudiz4_Witness_ACH");
            naudiz.AddFinalBossAchievementData(BossType_GameIDs.Heaven.ToString(), "AApocrypha_Naudiz4_Divine_ACH");
            if (AApocrypha.CrossMod.EnemyPack) { naudiz.AddFinalBossAchievementData("DoulaBoss", "AApocrypha_Naudiz4_Abstraction_ACH"); }
            if (AApocrypha.CrossMod.GlitchsFreaks) { naudiz.AddFinalBossAchievementData("March_BOSS", "AApocrypha_Naudiz4_Inevitable_ACH"); }
            if (AApocrypha.CrossMod.IntoTheAbyss) { naudiz.AddFinalBossAchievementData("Nobody_BOSS", "AApocrypha_Naudiz4_Forgotten_ACH"); }
            //if (AApocrypha.CrossMod.IntoTheAbyss) { naudiz.AddFinalBossAchievementData("Katalixi_BOSS", "AApocrypha_Naudiz4_Boundary_ACH"); }
            if (AApocrypha.CrossMod.SaltEnemies) { naudiz.AddFinalBossAchievementData("BlueSky_BOSS", "AApocrypha_Naudiz4_Dreamer_ACH"); }
            naudiz.AddCharacter(true, false);

            SpeakerBundle speakerBundleNaudiz4 = new SpeakerBundle();
            speakerBundleNaudiz4.bundleTextColor = new Color32(104, 134, 102, 255);
            speakerBundleNaudiz4.dialogueSound = LoadedAssetsHandler.GetCharacter("Naudiz4_CH").dxSound;
            speakerBundleNaudiz4.portrait = ResourceLoader.LoadSprite("Naudiz4Talk", new Vector2(0.5f, 0f), 32);

            SpeakerBundle speakerBundleNaudiz4Idle = new SpeakerBundle();
            speakerBundleNaudiz4Idle.bundleTextColor = new Color32(73, 85, 73, 255);
            speakerBundleNaudiz4Idle.dialogueSound = LoadedAssetsHandler.GetCharacter("Naudiz4_CH").dxSound;
            speakerBundleNaudiz4Idle.portrait = ResourceLoader.LoadSprite("Naudiz4TalkIdle", new Vector2(0.5f, 0f), 32);

            SpeakerBundle speakerBundleNaudiz4Hatless = new SpeakerBundle();
            speakerBundleNaudiz4Hatless.bundleTextColor = new Color32(104, 134, 102, 255);
            speakerBundleNaudiz4Hatless.dialogueSound = LoadedAssetsHandler.GetCharacter("Naudiz4_CH").dxSound;
            speakerBundleNaudiz4Hatless.portrait = ResourceLoader.LoadSprite("Naudiz4TalkHatless", new Vector2(0.5f, 0f), 32);

            SpeakerBundle speakerBundleNaudiz4HatlessIdle = new SpeakerBundle();
            speakerBundleNaudiz4HatlessIdle.bundleTextColor = new Color32(73, 85, 73, 255);
            speakerBundleNaudiz4HatlessIdle.dialogueSound = LoadedAssetsHandler.GetCharacter("Naudiz4_CH").dxSound;
            speakerBundleNaudiz4HatlessIdle.portrait = ResourceLoader.LoadSprite("Naudiz4TalkHatlessIdle", new Vector2(0.5f, 0f), 32);
            Dialogues.CreateAndAddCustom_SpeakerData("Naudiz4", speakerBundleNaudiz4, true, false, new SpeakerEmote[3]
            {
                new SpeakerEmote { 
                    emotion = "Idle",
                    bundle = speakerBundleNaudiz4Idle,
                },
                new SpeakerEmote {
                    emotion = "Hatless",
                    bundle = speakerBundleNaudiz4Hatless,
                },
                new SpeakerEmote {
                    emotion = "HatlessIdle",
                    bundle = speakerBundleNaudiz4HatlessIdle,
                },
            });
        }
    }
}
