using System;
using System.Collections.Generic;
using System.Text;
using A_Apocrypha.CustomOther;
using FMOD;

namespace A_Apocrypha.Fools
{
    public class VaughanCharacter
    {
        public static List<BasePassiveAbilitySO> allPassives = new List<BasePassiveAbilitySO>();
        public static List<BasePassiveAbilitySO> offensePassives = new List<BasePassiveAbilitySO>();
        public static List<BasePassiveAbilitySO> defensePassives = new List<BasePassiveAbilitySO>();
        public static List<BasePassiveAbilitySO> utilityPassives = new List<BasePassiveAbilitySO>();
        public static void Add()
        {
            ArtsPassives();

            AddRandomPassiveEffect shapelingAny = ScriptableObject.CreateInstance<AddRandomPassiveEffect>();
            shapelingAny._passivesToAdd = allPassives.ToArray();
            shapelingAny._popup = true;
            shapelingAny._fixedCap = 10;

            AddRandomPassiveEffect shapelingOffense = ScriptableObject.CreateInstance<AddRandomPassiveEffect>();
            shapelingOffense._passivesToAdd = offensePassives.ToArray();
            shapelingOffense._popup = true;
            shapelingOffense._fixedCap = 10;

            AddRandomPassiveEffect shapelingDefense = ScriptableObject.CreateInstance<AddRandomPassiveEffect>();
            shapelingDefense._passivesToAdd = defensePassives.ToArray();
            shapelingDefense._popup = true;
            shapelingDefense._fixedCap = 10;

            AddRandomPassiveEffect shapelingUtility = ScriptableObject.CreateInstance<AddRandomPassiveEffect>();
            shapelingUtility._passivesToAdd = utilityPassives.ToArray();
            shapelingUtility._popup = true;
            shapelingUtility._fixedCap = 10;

            RemovePassiveEffect shapelingClear = ScriptableObject.CreateInstance<RemovePassiveEffect>();
            shapelingClear.m_PassiveID = "ShapelingArt";

            CheckPassiveAbilityEffect hasShapeling = ScriptableObject.CreateInstance<CheckPassiveAbilityEffect>();
            hasShapeling.m_PassiveID = "ShapelingArt";

            CopyCasterPassiveToTargetsByMIDEffect copyShapeling = ScriptableObject.CreateInstance<CopyCasterPassiveToTargetsByMIDEffect>();
            copyShapeling.m_passiveID = "ShapelingArt";

            PerformEffectPassiveAbility vaughanShapePassive = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
            vaughanShapePassive.name = "VaughanMalleable_PA";
            vaughanShapePassive._passiveName = "Malleable";
            vaughanShapePassive.m_PassiveID = "VaughanMalleable";
            vaughanShapePassive.passiveIcon = ResourceLoader.LoadSprite("IconMalleable");
            vaughanShapePassive._characterDescription = "Vaughan will gain a random Alteration on combat start. Alterations are Passives and do not stack; A newly applied Alteration will always replace the previous one.";
            vaughanShapePassive._enemyDescription = "shapeling arts? more like shapeling farts, lmao gottem";
            vaughanShapePassive._triggerOn = [TriggerCalls.OnCombatStart];
            vaughanShapePassive.conditions = [];
            vaughanShapePassive.doesPassiveTriggerInformationPanel = true;
            vaughanShapePassive.effects =
            [
                Effects.GenerateEffect(shapelingAny, 1, Targeting.Slot_SelfSlot),
            ];

            Character vaughan = new Character("Vaughan", "Vaughan_CH")
            {
                HealthColor = Pigments.Purple,
                UsesBasicAbility = true,
                UsesAllAbilities = false,
                MovesOnOverworld = true,
                FrontSprite = ResourceLoader.LoadSprite("VaughanFront", new Vector2(0.5f, 0f), 32),
                BackSprite = ResourceLoader.LoadSprite("VaughanBack", new Vector2(0.5f, 0f), 32),
                OverworldSprite = ResourceLoader.LoadSprite("VaughanOverworld", new Vector2(0.5f, 0f), 32),
                DamageSound = LoadedAssetsHandler.GetCharacter("Rags_CH").damageSound,
                DeathSound = LoadedAssetsHandler.GetCharacter("Rags_CH").deathSound,
                DialogueSound = LoadedAssetsHandler.GetCharacter("Rags_CH").dxSound,
                UnitTypes = ["FemaleID", UnitType_GameIDs.FemaleLooking.ToString(), "Sandwich_Gore", "Neathy"],
            };
            vaughan.GenerateMenuCharacter(ResourceLoader.LoadSprite("VaughanMenu"), ResourceLoader.LoadSprite("VaughanLocked"));
            vaughan.AddPassives([Passives.Delicate, vaughanShapePassive]);
            vaughan.SetMenuCharacterAsFullSupport();

            HealEffect heal = ScriptableObject.CreateInstance<HealEffect>();
            heal._directHeal = true;

            StatusEffect_Apply_Effect ScarsApply = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            ScarsApply._Status = StatusField.Scars;

            SpecificAlliesByMPassiveIDTargeting AlteredAllies = ScriptableObject.CreateInstance<SpecificAlliesByMPassiveIDTargeting>();
            AlteredAllies.slotOffsets = [0];
            AlteredAllies.targetUnitAllySlots = true;
            AlteredAllies.getAllUnitSelfSlots = false;
            AlteredAllies.m_passiveID = "ShapelingArt";

            TargetPerformEffectViaSubaction swapAlterSelf = ScriptableObject.CreateInstance<TargetPerformEffectViaSubaction>();
            swapAlterSelf.effects = [
                Effects.GenerateEffect(shapelingClear, 1, Targeting.Slot_SelfSlot),
                Effects.GenerateEffect(shapelingAny, 1, Targeting.Slot_SelfSlot),
            ];

            Ability amber1 = new Ability("Warm Amber", "VaughanAmber_1_A")
            {
                Description = "Heal the Left and Right party members 3 health. Apply an offensive Alteration to the Left ally and a defensive Alteration to the Right ally.",
                AbilitySprite = ResourceLoader.LoadSprite("IconVaughanAmber"),
                Cost = [Pigments.Blue, Pigments.Yellow],
                Visuals = Visuals.OilSlicked,
                AnimationTarget = Targeting.Slot_AllySides,
                Effects =
                [
                    Effects.GenerateEffect(heal, 3, Targeting.Slot_AllySides),
                    Effects.GenerateEffect(shapelingClear, 1, Targeting.Slot_AllySides),
                    Effects.GenerateEffect(shapelingOffense, 1, Targeting.Slot_AllyLeft),
                    Effects.GenerateEffect(shapelingDefense, 1, Targeting.Slot_AllyRight),
                ],
            };
            amber1.AddIntentsToTarget(Targeting.Slot_AllySides, [nameof(IntentType_GameIDs.Heal_1_4), "AA_AddPassive"]);

            Ability amber2 = new Ability("Trembling Amber", "VaughanAmber_2_A")
            {
                Description = "Heal the Left and Right party members 5 health. Apply an offensive Alteration to the Left ally and a defensive Alteration to the Right ally.",
                AbilitySprite = ResourceLoader.LoadSprite("IconVaughanAmber"),
                Cost = [Pigments.Blue, Pigments.Yellow],
                Visuals = Visuals.OilSlicked,
                AnimationTarget = Targeting.Slot_AllySides,
                Effects =
                [
                    Effects.GenerateEffect(heal, 5, Targeting.Slot_AllySides),
                    Effects.GenerateEffect(shapelingClear, 1, Targeting.Slot_AllySides),
                    Effects.GenerateEffect(shapelingOffense, 1, Targeting.Slot_AllyLeft),
                    Effects.GenerateEffect(shapelingDefense, 1, Targeting.Slot_AllyRight),
                ],
            };
            amber2.AddIntentsToTarget(Targeting.Slot_AllySides, [nameof(IntentType_GameIDs.Heal_5_10), "AA_AddPassive"]);

            Ability amber3 = new Ability("Pulsating Amber", "VaughanAmber_3_A")
            {
                Description = "Heal the Left and Right party members 7 health. Apply an offensive Alteration to the Left ally and a defensive Alteration to the Right ally.",
                AbilitySprite = ResourceLoader.LoadSprite("IconVaughanAmber"),
                Cost = [Pigments.Blue, Pigments.YellowBlue],
                Visuals = Visuals.OilSlicked,
                AnimationTarget = Targeting.Slot_AllySides,
                Effects =
                [
                    Effects.GenerateEffect(heal, 7, Targeting.Slot_AllySides),
                    Effects.GenerateEffect(shapelingClear, 1, Targeting.Slot_AllySides),
                    Effects.GenerateEffect(shapelingOffense, 1, Targeting.Slot_AllyLeft),
                    Effects.GenerateEffect(shapelingDefense, 1, Targeting.Slot_AllyRight),
                ],
            };
            amber3.AddIntentsToTarget(Targeting.Slot_AllySides, [nameof(IntentType_GameIDs.Heal_5_10), "AA_AddPassive"]);

            Ability amber4 = new Ability("Fecund Amber", "VaughanAmber_4_A")
            {
                Description = "Heal the Left and Right party members 9 health. Apply an offensive Alteration to the Left ally and a defensive Alteration to the Right ally.",
                AbilitySprite = ResourceLoader.LoadSprite("IconVaughanAmber"),
                Cost = [Pigments.Blue, Pigments.YellowBlue],
                Visuals = Visuals.OilSlicked,
                AnimationTarget = Targeting.Slot_AllySides,
                Effects =
                [
                    Effects.GenerateEffect(heal, 9, Targeting.Slot_AllySides),
                    Effects.GenerateEffect(shapelingClear, 1, Targeting.Slot_AllySides),
                    Effects.GenerateEffect(shapelingOffense, 1, Targeting.Slot_AllyLeft),
                    Effects.GenerateEffect(shapelingDefense, 1, Targeting.Slot_AllyRight),
                ],
            };
            amber4.AddIntentsToTarget(Targeting.Slot_AllySides, [nameof(IntentType_GameIDs.Heal_5_10), "AA_AddPassive"]);

            Ability ecdysis1 = new Ability("Hurried Ecdysis", "VaughanEcdysis_1_A")
            {
                Description = "Heal this party member 3 health and apply 1 Scar to this party member." +
                "\nIf this party member has an Alteration, remove it, apply it to the Left and Right allies and heal them 2 health." +
                "\nApply a defensive Alteration to this party member.",
                AbilitySprite = ResourceLoader.LoadSprite("IconVaughanEcdysis"),
                Cost = [Pigments.BluePurple, Pigments.Blue],
                Visuals = Visuals.ShedSkin,
                AnimationTarget = Targeting.Slot_SelfSlot,
                Effects =
                [
                    Effects.GenerateEffect(heal, 3, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(ScarsApply, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(hasShapeling, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(shapelingClear, 1, Targeting.Slot_AllySides, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(copyShapeling, 1, Targeting.Slot_AllySides, Effects.CheckPreviousEffectCondition(true, 2)),
                    Effects.GenerateEffect(heal, 2, Targeting.Slot_AllySides, Effects.CheckPreviousEffectCondition(true, 3)),
                    Effects.GenerateEffect(shapelingClear, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(shapelingDefense, 1, Targeting.Slot_SelfSlot),
                ],
            };
            ecdysis1.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Heal_1_4), nameof(IntentType_GameIDs.Status_Scars), "AA_RemPassive"]);
            ecdysis1.AddIntentsToTarget(Targeting.Slot_AllySides, ["AA_AddPassive", nameof(IntentType_GameIDs.Heal_1_4)]);
            ecdysis1.AddIntentsToTarget(Targeting.Slot_SelfSlot, ["AA_AddPassive"]);

            Ability ecdysis2 = new Ability("Cautious Ecdysis", "VaughanEcdysis_2_A")
            {
                Description = "Heal this party member 4 health and apply 1 Scar to this party member." +
                "\nIf this party member has an Alteration, remove it, apply it to the Left and Right allies and heal them 3 health." +
                "\nApply a defensive Alteration to this party member.",
                AbilitySprite = ResourceLoader.LoadSprite("IconVaughanEcdysis"),
                Cost = [Pigments.BluePurple, Pigments.Blue],
                Visuals = Visuals.ShedSkin,
                AnimationTarget = Targeting.Slot_SelfSlot,
                Effects =
                [
                    Effects.GenerateEffect(heal, 4, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(ScarsApply, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(hasShapeling, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(shapelingClear, 1, Targeting.Slot_AllySides, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(copyShapeling, 1, Targeting.Slot_AllySides, Effects.CheckPreviousEffectCondition(true, 2)),
                    Effects.GenerateEffect(heal, 3, Targeting.Slot_AllySides, Effects.CheckPreviousEffectCondition(true, 3)),
                    Effects.GenerateEffect(shapelingClear, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(shapelingDefense, 1, Targeting.Slot_SelfSlot),
                ],
            };
            ecdysis2.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Heal_1_4), nameof(IntentType_GameIDs.Status_Scars), "AA_RemPassive"]);
            ecdysis2.AddIntentsToTarget(Targeting.Slot_AllySides, ["AA_AddPassive", nameof(IntentType_GameIDs.Heal_1_4)]);
            ecdysis2.AddIntentsToTarget(Targeting.Slot_SelfSlot, ["AA_AddPassive"]);

            Ability ecdysis3 = new Ability("Natural Ecdysis", "VaughanEcdysis_3_A")
            {
                Description = "Heal this party member 5 health and apply 1 Scar to this party member." +
                "\nIf this party member has an Alteration, remove it, apply it to the Left and Right allies and heal them 4 health." +
                "\nApply a defensive Alteration to this party member.",
                AbilitySprite = ResourceLoader.LoadSprite("IconVaughanEcdysis"),
                Cost = [Pigments.BluePurple, Pigments.BluePurple],
                Visuals = Visuals.ShedSkin,
                AnimationTarget = Targeting.Slot_SelfSlot,
                Effects =
                [
                    Effects.GenerateEffect(heal, 5, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(ScarsApply, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(hasShapeling, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(shapelingClear, 1, Targeting.Slot_AllySides, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(copyShapeling, 1, Targeting.Slot_AllySides, Effects.CheckPreviousEffectCondition(true, 2)),
                    Effects.GenerateEffect(heal, 4, Targeting.Slot_AllySides, Effects.CheckPreviousEffectCondition(true, 3)),
                    Effects.GenerateEffect(shapelingClear, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(shapelingDefense, 1, Targeting.Slot_SelfSlot),
                ],
            };
            ecdysis3.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Heal_5_10), nameof(IntentType_GameIDs.Status_Scars), "AA_RemPassive"]);
            ecdysis3.AddIntentsToTarget(Targeting.Slot_AllySides, ["AA_AddPassive", nameof(IntentType_GameIDs.Heal_1_4)]);
            ecdysis3.AddIntentsToTarget(Targeting.Slot_SelfSlot, ["AA_AddPassive"]);

            Ability ecdysis4 = new Ability("Transcendent Ecdysis", "VaughanEcdysis_4_A")
            {
                Description = "Heal this party member 6 health and apply 1 Scar to this party member." +
                "\nIf this party member has an Alteration, remove it, apply it to the Left and Right allies and heal them 5 health." +
                "\nApply a defensive Alteration to this party member.",
                AbilitySprite = ResourceLoader.LoadSprite("IconVaughanEcdysis"),
                Cost = [Pigments.BluePurple, Pigments.BluePurple],
                Visuals = Visuals.ShedSkin,
                AnimationTarget = Targeting.Slot_SelfSlot,
                Effects =
                [
                    Effects.GenerateEffect(heal, 6, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(ScarsApply, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(hasShapeling, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(shapelingClear, 1, Targeting.Slot_AllySides, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(copyShapeling, 1, Targeting.Slot_AllySides, Effects.CheckPreviousEffectCondition(true, 2)),
                    Effects.GenerateEffect(heal, 5, Targeting.Slot_AllySides, Effects.CheckPreviousEffectCondition(true, 3)),
                    Effects.GenerateEffect(shapelingClear, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(shapelingDefense, 1, Targeting.Slot_SelfSlot),
                ],
            };
            ecdysis4.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Heal_5_10), nameof(IntentType_GameIDs.Status_Scars), "AA_RemPassive"]);
            ecdysis4.AddIntentsToTarget(Targeting.Slot_AllySides, ["AA_AddPassive", nameof(IntentType_GameIDs.Heal_5_10)]);
            ecdysis4.AddIntentsToTarget(Targeting.Slot_SelfSlot, ["AA_AddPassive"]);

            Ability form1 = new Ability("Stiff Form", "VaughanForm_1_A")
            {
                Description = "Heal this party member 1 health." +
                "\nApply a random Alteration to all party members with an Alteration.",
                AbilitySprite = ResourceLoader.LoadSprite("IconVaughanForm"),
                Cost = [Pigments.BluePurple],
                Visuals = Visuals.UglyOnTheInside,
                AnimationTarget = AlteredAllies,
                Effects =
                [
                    Effects.GenerateEffect(heal, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(swapAlterSelf, 1, AlteredAllies),
                ],
            };
            form1.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Heal_1_4)]);
            form1.AddIntentsToTarget(AlteredAllies, ["AA_AddPassive"]);

            Ability form2 = new Ability("Limber Form", "VaughanForm_2_A")
            {
                Description = "Heal this party member 2 health." +
                "\nApply a random Alteration to all party members with an Alteration.",
                AbilitySprite = ResourceLoader.LoadSprite("IconVaughanForm"),
                Cost = [Pigments.BluePurple],
                Visuals = Visuals.UglyOnTheInside,
                AnimationTarget = AlteredAllies,
                Effects =
                [
                    Effects.GenerateEffect(heal, 2, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(swapAlterSelf, 1, AlteredAllies),
                ],
            };
            form2.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Heal_1_4)]);
            form2.AddIntentsToTarget(AlteredAllies, ["AA_AddPassive"]);

            Ability form3 = new Ability("Flexible Form", "VaughanForm_3_A")
            {
                Description = "Heal this party member 3 health." +
                "\nApply a random Alteration to all party members with an Alteration.",
                AbilitySprite = ResourceLoader.LoadSprite("IconVaughanForm"),
                Cost = [Pigments.BluePurple],
                Visuals = Visuals.UglyOnTheInside,
                AnimationTarget = AlteredAllies,
                Effects =
                [
                    Effects.GenerateEffect(heal, 3, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(swapAlterSelf, 1, AlteredAllies),
                ],
            };
            form3.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Heal_1_4)]);
            form3.AddIntentsToTarget(AlteredAllies, ["AA_AddPassive"]);

            Ability form4 = new Ability("Unrestrained Form", "VaughanForm_4_A")
            {
                Description = "Heal this party member 4 health." +
                "\nApply a random Alteration to all party members with an Alteration.",
                AbilitySprite = ResourceLoader.LoadSprite("IconVaughanForm"),
                Cost = [Pigments.BluePurple],
                Visuals = Visuals.UglyOnTheInside,
                AnimationTarget = AlteredAllies,
                Effects =
                [
                    Effects.GenerateEffect(heal, 4, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(swapAlterSelf, 1, AlteredAllies),
                ],
            };
            form4.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Heal_1_4)]);
            form4.AddIntentsToTarget(AlteredAllies, ["AA_AddPassive"]);

            vaughan.AddLevelData(12, [amber1, ecdysis1, form1]);
            vaughan.AddLevelData(14, [amber2, ecdysis2, form2]);
            vaughan.AddLevelData(17, [amber3, ecdysis3, form3]);
            vaughan.AddLevelData(21, [amber4, ecdysis4, form4]);

            vaughan.AddFinalBossAchievementData(BossType_GameIDs.OsmanSinnoks.ToString(), "AApocrypha_Vaughan_Witness_ACH");
            vaughan.AddFinalBossAchievementData(BossType_GameIDs.Heaven.ToString(), "AApocrypha_Vaughan_Divine_ACH");
            if (AApocrypha.CrossMod.EnemyPack) { vaughan.AddFinalBossAchievementData("DoulaBoss", "AApocrypha_Vaughan_Abstraction_ACH"); }
            if (AApocrypha.CrossMod.GlitchsFreaks) { vaughan.AddFinalBossAchievementData("March_BOSS", "AApocrypha_Vaughan_Inevitable_ACH"); }
            if (AApocrypha.CrossMod.IntoTheAbyss) { vaughan.AddFinalBossAchievementData("Nobody_BOSS", "AApocrypha_Vaughan_Forgotten_ACH"); }
            //if (AApocrypha.CrossMod.IntoTheAbyss) { vaughan.AddFinalBossAchievementData("Katalixi_BOSS", "AApocrypha_Vaughan_Boundary_ACH"); }
            //if (AApocrypha.CrossMod.SaltEnemies) { vaughan.AddFinalBossAchievementData("BlueSky_BOSS", "AApocrypha_Vaughan_Dreamer_ACH"); }
            vaughan.AddCharacter(true, false);

            SpeakerBundle speakerBundleVaughan = new SpeakerBundle();
            speakerBundleVaughan.bundleTextColor = new Color32(202, 114, 24, 255);
            speakerBundleVaughan.dialogueSound = LoadedAssetsHandler.GetCharacter("Vaughan_CH").dxSound;
            speakerBundleVaughan.portrait = ResourceLoader.LoadSprite("VaughanFront", new Vector2(0.5f, 0f), 32);
            var dia = Dialogues.CreateAndAddCustom_SpeakerData("Vaughan", speakerBundleVaughan, true, false, new SpeakerEmote[0]);
        }

        public static void ArtsPassives()
        {
            UnityEngine.Debug.Log("Vaughan | passive generation initialized");
            SwapToSidesEffect LeftOrRight = ScriptableObject.CreateInstance<SwapToSidesEffect>();

            SwapToOneSideEffect Left = ScriptableObject.CreateInstance<SwapToOneSideEffect>();
            Left._swapRight = false;

            SwapToOneSideEffect Right = ScriptableObject.CreateInstance<SwapToOneSideEffect>();
            Right._swapRight = true;

            LeftOrRightToOpposeEnemyChanceForNextEffect NavigatorNotOpposing = ScriptableObject.CreateInstance<LeftOrRightToOpposeEnemyChanceForNextEffect>();
            NavigatorNotOpposing._inverted = true;

            LeftOrRightToOpposeEnemyChanceForNextEffect NavigatorOpposing = ScriptableObject.CreateInstance<LeftOrRightToOpposeEnemyChanceForNextEffect>();
            NavigatorOpposing._inverted = false;

            PerformEffectPassiveAbility rubberbones = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
            rubberbones.name = "Shapeling_RubberBones_PA";
            rubberbones._passiveName = "Rubber Bones";
            rubberbones.m_PassiveID = "ShapelingArt";
            rubberbones.passiveIcon = ResourceLoader.LoadSprite("ShapelingRubberBones");
            rubberbones._characterDescription = "On being directly damaged, move to the Left or Right, prioritizing unopposed spaces, if there is an Opposing enemy.";
            rubberbones._enemyDescription = "On being directly damaged, move to the Left or Right, prioritizing unopposed spaces, if there is an Opposing party member.";
            rubberbones._triggerOn = [TriggerCalls.OnDirectDamaged];
            rubberbones.effects =
            [
                Effects.GenerateEffect(ScriptableObject.CreateInstance<CheckHasUnitEffect>(), 1, Targeting.Slot_Front),
                Effects.GenerateEffect(NavigatorNotOpposing, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 1)),
                Effects.GenerateEffect(Right, 1, Targeting.Slot_SelfSlot, Effects.CheckMultiplePreviousEffectsCondition([true, true], [1, 2])),
                Effects.GenerateEffect(Left, 1, Targeting.Slot_SelfSlot, Effects.CheckMultiplePreviousEffectsCondition([false, true], [2, 3])),
            ];

            PercDmgModPassiveAbility cartilageplating = ScriptableObject.CreateInstance<PercDmgModPassiveAbility>();
            cartilageplating.name = "Shapeling_CartilagePlating_PA";
            cartilageplating._passiveName = "Cartilaginous Plating";
            cartilageplating.m_PassiveID = "ShapelingArt";
            cartilageplating.passiveIcon = ResourceLoader.LoadSprite("ShapelingCartilagePlating");
            cartilageplating._characterDescription = "Reduce all direct damage taken by this party member by 30%.";
            cartilageplating._enemyDescription = "Reduce all direct damage taken by this enemy by 30%.";
            cartilageplating._triggerOn = [TriggerCalls.OnBeingDamaged];
            cartilageplating.conditions = [ScriptableObject.CreateInstance<DamageReceivedWasDirectEffectorCondition>()];
            cartilageplating._doesIncrease = false;
            cartilageplating._useDealt = false;
            cartilageplating._useSimpleInt = false;
            cartilageplating._percentageToModify = 30;

            PercDmgModPassiveAbility sharpened = ScriptableObject.CreateInstance<PercDmgModPassiveAbility>();
            sharpened.name = "Shapeling_Sharpened_PA";
            sharpened._passiveName = "Sharpened";
            sharpened.m_PassiveID = "ShapelingArt";
            sharpened.passiveIcon = ResourceLoader.LoadSprite("ShapelingSharpened");
            sharpened._characterDescription = "Increase all damage dealt by this party member by 30%.";
            sharpened._enemyDescription = "Increase all damage dealt by this enemy by 30%.";
            sharpened._triggerOn = [TriggerCalls.OnWillApplyDamage];
            sharpened._doesIncrease = true;
            sharpened._useDealt = true;
            sharpened._useSimpleInt = false;
            sharpened._percentageToModify = 30;

            PerformEffectPassiveAbility bonespines = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
            bonespines.name = "Shapeling_BoneSpines_PA";
            bonespines._passiveName = "Bone Spines";
            bonespines.m_PassiveID = "ShapelingArt";
            bonespines.passiveIcon = ResourceLoader.LoadSprite("ShapelingBoneSpines");
            bonespines._characterDescription = "On moving, deal 2 indirect damage to the Opposing enemy.";
            bonespines._enemyDescription = "On moving, deal 2 indirect damage to the Opposing party member.";
            bonespines._triggerOn = [TriggerCalls.OnMoved];
            bonespines.effects = [
                Effects.GenerateEffect(BasicEffects.Indirect, 2, Targeting.Slot_Front),
            ];

            FieldEffect_Apply_Effect Shieldify = ScriptableObject.CreateInstance<FieldEffect_Apply_Effect>();
            Shieldify._Field = StatusField.Shield;

            PerformEffectPassiveAbility protectivegrowths = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
            protectivegrowths.name = "Shapeling_ProtGrowths_PA";
            protectivegrowths._passiveName = "Protective Growths";
            protectivegrowths.m_PassiveID = "ShapelingArt";
            protectivegrowths.passiveIcon = ResourceLoader.LoadSprite("ShapelingProtectiveGrowths");
            protectivegrowths._characterDescription = "Apply 4 Shield to this party member's position at the start of each turn.";
            protectivegrowths._enemyDescription = "Apply 4 Shield to this enemy's position at the start of each turn.";
            protectivegrowths._triggerOn = [TriggerCalls.OnTurnStart];
            protectivegrowths.effects = [
                Effects.GenerateEffect(Shieldify, 4, Targeting.Slot_SelfSlot),
            ];

            FieldEffect_Apply_Effect IDoBelieveIAmOnFire = ScriptableObject.CreateInstance<FieldEffect_Apply_Effect>();
            IDoBelieveIAmOnFire._Field = StatusField.OnFire;

            PerformEffectPassiveAbility flammableoils = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
            flammableoils.name = "Shapeling_FlammableOils_PA";
            flammableoils._passiveName = "Flammable Oils";
            flammableoils.m_PassiveID = "ShapelingArt";
            flammableoils.passiveIcon = ResourceLoader.LoadSprite("ShapelingFlammableOils");
            flammableoils._characterDescription = "When performing an ability, apply 1 Fire to the Opposing position.";
            flammableoils._enemyDescription = flammableoils._characterDescription;
            flammableoils._triggerOn = [TriggerCalls.OnAbilityUsed];
            flammableoils.effects = [
                Effects.GenerateEffect(IDoBelieveIAmOnFire, 1, Targeting.Slot_Front),
            ];

            GenerateColorManaEffect GiveRedPigment = ScriptableObject.CreateInstance<GenerateColorManaEffect>();
            GiveRedPigment.mana = Pigments.Red;

            PerformEffectPassiveAbility loosemarrow = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
            loosemarrow.name = "Shapeling_LooseMarrow_PA";
            loosemarrow._passiveName = "Loose Marrow";
            loosemarrow.m_PassiveID = "ShapelingArt";
            loosemarrow.passiveIcon = ResourceLoader.LoadSprite("ShapelingLooseMarrow");
            loosemarrow._characterDescription = "Produce 1 Red Pigment when performing an ability.";
            loosemarrow._enemyDescription = loosemarrow._characterDescription;
            loosemarrow._triggerOn = [TriggerCalls.OnAbilityUsed];
            loosemarrow.effects = [
                Effects.GenerateEffect(GiveRedPigment, 1, Targeting.Slot_SelfSlot),
            ];

            UnityEngine.Debug.Log("Vaughan | adding to lists");

            offensePassives.AddRange([sharpened, bonespines, flammableoils]);
            defensePassives.AddRange([rubberbones, cartilageplating, protectivegrowths]);
            utilityPassives.AddRange([loosemarrow]);

            allPassives.AddRange(offensePassives);
            allPassives.AddRange(defensePassives);
            allPassives.AddRange(utilityPassives);
            UnityEngine.Debug.Log("Vaughan | passive generation complete!");
        }
    }
}
