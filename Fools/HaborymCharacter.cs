using System;
using System.Collections.Generic;
using System.Text;
using A_Apocrypha.CustomOther;
using UnityEngine;

namespace A_Apocrypha.Fools
{
    public class HaborymCharacter
    {
        public static void Add()
        {
            GenerateColorManaEffect GiveBluePigment = ScriptableObject.CreateInstance<GenerateColorManaEffect>();
            GiveBluePigment.mana = Pigments.Blue;

            TargetSplitOrReplaceHealthEffect ConscriptedIntoTheBlueManGroup = ScriptableObject.CreateInstance<TargetSplitOrReplaceHealthEffect>();
            ConscriptedIntoTheBlueManGroup._transformBlacklist = false;
            ConscriptedIntoTheBlueManGroup._colorBlacklist = [Pigments.Grey];
            ConscriptedIntoTheBlueManGroup._color = Pigments.Blue;

            RemoveStatusEffectEffect TriggerFrostbite = ScriptableObject.CreateInstance<RemoveStatusEffectEffect>();
            TriggerFrostbite._status = StatusField.GetCustomStatusEffect("Frostbite_ID");

            StatusEffect_Apply_Effect AddFrostbite = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            AddFrostbite._Status = StatusField.GetCustomStatusEffect("Frostbite_ID");

            FieldEffect_Apply_Effect AddHoarfrost = ScriptableObject.CreateInstance<FieldEffect_Apply_Effect>();
            AddHoarfrost._Field = StatusField.GetCustomFieldEffect("Hoarfrost_ID");

            StatusEffectCheckerEffect IsFrostbitten = ScriptableObject.CreateInstance<StatusEffectCheckerEffect>();
            IsFrostbitten._status = AddFrostbite._Status;

            Ability haborymslap = new Ability("Cold Snap", "HaborymSlap_A")
            {
                Description = "Apply 1 Frostbite to the Opposing enemy, then increase their Frostbite intensity by 1, then remove all Frostbite from them." +
                "\nGenerate 1 Blue pigment.",
                AbilitySprite = ResourceLoader.LoadSprite("IconHaborymColdSnap"),
                Visuals = Visuals.Poke,
                AnimationTarget = Targeting.Slot_Front,
                Cost = [Pigments.Yellow],
                Effects =
                [
                    Effects.GenerateEffect(AddFrostbite, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<TargetFrostValueModifyEffect>(), 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(TriggerFrostbite, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(GiveBluePigment, 1, Targeting.Slot_SelfSlot),
                ],
            };
            haborymslap.AddIntentsToTarget(Targeting.Slot_Front, ["Status_Frostbite", nameof(IntentType_GameIDs.Misc), "Rem_Status_Frostbite"]);
            haborymslap.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Mana_Generate)]);

            Character haborym = new Character("Haborym", "Haborym_CH")
            {
                HealthColor = Pigments.Blue,
                UsesBasicAbility = true,
                UsesAllAbilities = false,
                MovesOnOverworld = true,
                FrontSprite = ResourceLoader.LoadSprite("HaborymFront", new Vector2(0.5f, 0f), 32),
                BackSprite = ResourceLoader.LoadSprite("HaborymBack", new Vector2(0.5f, 0f), 32),
                OverworldSprite = ResourceLoader.LoadSprite("HaborymOverworld", new Vector2(0.5f, 0f), 32),
                DamageSound = "event:/AASFX/Fools/HaborymHurt",
                DeathSound = "event:/AASFX/Fools/HaborymDeath",
                DialogueSound = "event:/AASFX/Fools/HaborymTalk",
                BasicAbility = haborymslap,
                UnitTypes = ["Male_ID", "Sandwich_Spirit"],
            };
            haborym.GenerateMenuCharacter(ResourceLoader.LoadSprite("HaborymMenu"), ResourceLoader.LoadSprite("HaborymLocked"));
            haborym.AddPassives([Passives.GetCustomPassive("Snowstorm_1_PA"), Passives.GetCustomPassive("Antifreeze_PA")]);
            haborym.SetMenuCharacterAsFullDPS();

            AttackVisualsSO WindVisuals = ITAVisuals.Wind;

            Ability gale1 = new Ability("Crisp Gale", "HaborymGale_1_A")
            {
                Description = "Apply 1 Frostbite to the Left and Right enemies, then increase their Frostbite intensity by 1. If either target is already Frostbitten, apply 1 Hoarfrost to their position.",
                AbilitySprite = ResourceLoader.LoadSprite("IconHaborymGale"),
                Visuals = WindVisuals,
                AnimationTarget = Targeting.Slot_OpponentSides,
                Cost = [Pigments.Blue, Pigments.YellowBlue],
                Effects =
                [
                    Effects.GenerateEffect(IsFrostbitten, 1, Targeting.Slot_OpponentLeft),
                    Effects.GenerateEffect(AddHoarfrost, 1, Targeting.Slot_OpponentLeft, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(IsFrostbitten, 1, Targeting.Slot_OpponentRight),
                    Effects.GenerateEffect(AddHoarfrost, 1, Targeting.Slot_OpponentRight, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(AddFrostbite, 1, Targeting.Slot_OpponentSides),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<TargetFrostValueModifyEffect>(), 1, Targeting.Slot_OpponentSides),
                ],
            };
            gale1.AddIntentsToTarget(Targeting.Slot_OpponentSides, ["Status_Frostbite", nameof(IntentType_GameIDs.Misc), nameof(IntentType_GameIDs.Misc_Hidden), "Field_Hoarfrost"]);

            Ability gale2 = new Ability("Frigid Gale", "HaborymGale_2_A")
            {
                Description = "Apply 1 Frostbite to the Left and Right enemies, then increase their Frostbite intensity by 2. If either target is already Frostbitten, apply 1 Hoarfrost to their position.",
                AbilitySprite = ResourceLoader.LoadSprite("IconHaborymGale"),
                Visuals = WindVisuals,
                AnimationTarget = Targeting.Slot_OpponentSides,
                Cost = [Pigments.Blue, Pigments.YellowBlue],
                Effects =
                [
                    Effects.GenerateEffect(IsFrostbitten, 1, Targeting.Slot_OpponentLeft),
                    Effects.GenerateEffect(AddHoarfrost, 1, Targeting.Slot_OpponentLeft, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(IsFrostbitten, 1, Targeting.Slot_OpponentRight),
                    Effects.GenerateEffect(AddHoarfrost, 1, Targeting.Slot_OpponentRight, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(AddFrostbite, 1, Targeting.Slot_OpponentSides),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<TargetFrostValueModifyEffect>(), 2, Targeting.Slot_OpponentSides),
                ],
            };
            gale2.AddIntentsToTarget(Targeting.Slot_OpponentSides, ["Status_Frostbite", nameof(IntentType_GameIDs.Misc), nameof(IntentType_GameIDs.Misc_Hidden), "Field_Hoarfrost"]);

            Ability gale3 = new Ability("Bone-Chilling Gale", "HaborymGale_3_A")
            {
                Description = "Apply 2 Frostbite to the Left and Right enemies, then increase their Frostbite intensity by 2. If either target is already Frostbitten, apply 2 Hoarfrost to their position.",
                AbilitySprite = ResourceLoader.LoadSprite("IconHaborymGale"),
                Visuals = WindVisuals,
                AnimationTarget = Targeting.Slot_OpponentSides,
                Cost = [Pigments.Blue, Pigments.YellowBlue],
                Effects =
                [
                    Effects.GenerateEffect(IsFrostbitten, 1, Targeting.Slot_OpponentLeft),
                    Effects.GenerateEffect(AddHoarfrost, 2, Targeting.Slot_OpponentLeft, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(IsFrostbitten, 1, Targeting.Slot_OpponentRight),
                    Effects.GenerateEffect(AddHoarfrost, 2, Targeting.Slot_OpponentRight, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(AddFrostbite, 2, Targeting.Slot_OpponentSides),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<TargetFrostValueModifyEffect>(), 2, Targeting.Slot_OpponentSides),
                ],
            };
            gale3.AddIntentsToTarget(Targeting.Slot_OpponentSides, ["Status_Frostbite", nameof(IntentType_GameIDs.Misc), nameof(IntentType_GameIDs.Misc_Hidden), "Field_Hoarfrost"]);

            Ability gale4 = new Ability("Arctic Gale", "HaborymGale_4_A")
            {
                Description = "Apply 2 Frostbite to the Left and Right enemies, then increase their Frostbite intensity by 3. If either target is already Frostbitten, apply 2 Hoarfrost to their position.",
                AbilitySprite = ResourceLoader.LoadSprite("IconHaborymGale"),
                Visuals = WindVisuals,
                AnimationTarget = Targeting.Slot_OpponentSides,
                Cost = [Pigments.Blue, Pigments.YellowBlue],
                Effects =
                [
                    Effects.GenerateEffect(IsFrostbitten, 1, Targeting.Slot_OpponentLeft),
                    Effects.GenerateEffect(AddHoarfrost, 2, Targeting.Slot_OpponentLeft, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(IsFrostbitten, 1, Targeting.Slot_OpponentRight),
                    Effects.GenerateEffect(AddHoarfrost, 2, Targeting.Slot_OpponentRight, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(AddFrostbite, 2, Targeting.Slot_OpponentSides),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<TargetFrostValueModifyEffect>(), 3, Targeting.Slot_OpponentSides),
                ],
            };
            gale4.AddIntentsToTarget(Targeting.Slot_OpponentSides, ["Status_Frostbite", nameof(IntentType_GameIDs.Misc), nameof(IntentType_GameIDs.Misc_Hidden), "Field_Hoarfrost"]);

            ReduceSetStatusEffectEffect ReduceFrostbite = ScriptableObject.CreateInstance<ReduceSetStatusEffectEffect>();
            ReduceFrostbite._Status = StatusField.GetCustomStatusEffect("Frostbite_ID");

            Ability frost1 = new Ability("Fading Frost", "HaborymFrost_1_A")
            {
                Description = "Apply 2 Frostbite and 1 Hoarfrost to the Opposing enemy and increase their Frostbite intensity by 1. Remove 1 Frostbite from the Left and Right enemies.",
                AbilitySprite = ResourceLoader.LoadSprite("IconHaborymFrost"),
                Visuals = Visuals.Thorns,
                AnimationTarget = Targeting.Slot_Front,
                Cost = [Pigments.BlueRed, Pigments.Blue],
                Effects = [
                    Effects.GenerateEffect(AddFrostbite, 2, Targeting.Slot_Front),
                    Effects.GenerateEffect(AddHoarfrost, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<TargetFrostValueModifyEffect>(), 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(ReduceFrostbite, 1, Targeting.Slot_OpponentSides),
                ],
            };
            frost1.AddIntentsToTarget(Targeting.Slot_Front, ["Status_Frostbite", "Field_Hoarfrost", nameof(IntentType_GameIDs.Misc)]);
            frost1.AddIntentsToTarget(Targeting.Slot_OpponentSides, ["Rem_Status_Frostbite"]);

            Ability frost2 = new Ability("Setting Frost", "HaborymFrost_2_A")
            {
                Description = "Apply 2 Frostbite and 2 Hoarfrost to the Opposing enemy and increase their Frostbite intensity by 2. Remove 1 Frostbite from the Left and Right enemies.",
                AbilitySprite = ResourceLoader.LoadSprite("IconHaborymFrost"),
                Visuals = Visuals.Thorns,
                AnimationTarget = Targeting.Slot_Front,
                Cost = [Pigments.BlueRed, Pigments.Blue],
                Effects = [
                    Effects.GenerateEffect(AddFrostbite, 2, Targeting.Slot_Front),
                    Effects.GenerateEffect(AddHoarfrost, 2, Targeting.Slot_Front),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<TargetFrostValueModifyEffect>(), 2, Targeting.Slot_Front),
                    Effects.GenerateEffect(ReduceFrostbite, 1, Targeting.Slot_OpponentSides),
                ],
            };
            frost2.AddIntentsToTarget(Targeting.Slot_Front, ["Status_Frostbite", "Field_Hoarfrost", nameof(IntentType_GameIDs.Misc)]);
            frost2.AddIntentsToTarget(Targeting.Slot_OpponentSides, ["Rem_Status_Frostbite"]);

            Ability frost3 = new Ability("Creeping Frost", "HaborymFrost_3_A")
            {
                Description = "Apply 3 Frostbite and 3 Hoarfrost to the Opposing enemy and increase their Frostbite intensity by 3. Remove 1 Frostbite from the Left and Right enemies.",
                AbilitySprite = ResourceLoader.LoadSprite("IconHaborymFrost"),
                Visuals = Visuals.Thorns,
                AnimationTarget = Targeting.Slot_Front,
                Cost = [Pigments.BlueRed, Pigments.Blue],
                Effects = [
                    Effects.GenerateEffect(AddFrostbite, 3, Targeting.Slot_Front),
                    Effects.GenerateEffect(AddHoarfrost, 3, Targeting.Slot_Front),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<TargetFrostValueModifyEffect>(), 3, Targeting.Slot_Front),
                    Effects.GenerateEffect(ReduceFrostbite, 1, Targeting.Slot_OpponentSides),
                ],
            };
            frost3.AddIntentsToTarget(Targeting.Slot_Front, ["Status_Frostbite", "Field_Hoarfrost", nameof(IntentType_GameIDs.Misc)]);
            frost3.AddIntentsToTarget(Targeting.Slot_OpponentSides, ["Rem_Status_Frostbite"]);

            Ability frost4 = new Ability("Blooming Frost", "HaborymFrost_4_A")
            {
                Description = "Apply 3 Frostbite and 4 Hoarfrost to the Opposing enemy and increase their Frostbite intensity by 4. Remove 1 Frostbite from the Left and Right enemies.",
                AbilitySprite = ResourceLoader.LoadSprite("IconHaborymFrost"),
                Visuals = Visuals.Thorns,
                AnimationTarget = Targeting.Slot_Front,
                Cost = [Pigments.BlueRed, Pigments.Blue],
                Effects = [
                    Effects.GenerateEffect(AddFrostbite, 3, Targeting.Slot_Front),
                    Effects.GenerateEffect(AddHoarfrost, 4, Targeting.Slot_Front),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<TargetFrostValueModifyEffect>(), 4, Targeting.Slot_Front),
                    Effects.GenerateEffect(ReduceFrostbite, 1, Targeting.Slot_OpponentSides),
                ],
            };
            frost4.AddIntentsToTarget(Targeting.Slot_Front, ["Status_Frostbite", "Field_Hoarfrost", nameof(IntentType_GameIDs.Misc)]);
            frost4.AddIntentsToTarget(Targeting.Slot_OpponentSides, ["Rem_Status_Frostbite"]);

            SpecificOpponentsByStatusTargeting TheFrostbit = ScriptableObject.CreateInstance<SpecificOpponentsByStatusTargeting>();
            TheFrostbit._status = StatusField.GetCustomStatusEffect("Frostbite_ID");
            TheFrostbit.getAllUnitSelfSlots = false;
            TheFrostbit.slotOffsets = [0];
            TheFrostbit.targetUnitAllySlots = true;

            AnimationVisualsEffect WindFrostbit = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            WindFrostbit._visuals = WindVisuals;
            WindFrostbit._animationTarget = TheFrostbit;

            Ability cascade1 = new Ability("Cascading Slush", "HaborymCascade_1_A")
            {
                Description = "Apply 4 Frostbite to the Opposing enemy, then increase their Frostbite intensity by 3. Apply 1 Frostbite to all Frostbitten enemies.",
                AbilitySprite = ResourceLoader.LoadSprite("IconHaborymCascade"),
                Visuals = Visuals.Crush,
                AnimationTarget = Targeting.Slot_Front,
                Cost = [Pigments.Blue, Pigments.YellowBlue, Pigments.Yellow],
                Effects = [
                    Effects.GenerateEffect(AddFrostbite, 4, Targeting.Slot_Front),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<TargetFrostValueModifyEffect>(), 3, Targeting.Slot_Front),
                    Effects.GenerateEffect(WindFrostbit),
                    Effects.GenerateEffect(AddFrostbite, 1, TheFrostbit),
                ],
            };
            cascade1.AddIntentsToTarget(Targeting.Slot_Front, ["Status_Frostbite", nameof(IntentType_GameIDs.Misc)]);
            cascade1.AddIntentsToTarget(TheFrostbit, ["Status_Frostbite"]);

            Ability cascade2 = new Ability("Cascading Snow", "HaborymCascade_2_A")
            {
                Description = "Apply 4 Frostbite to the Opposing enemy, then increase their Frostbite intensity by 4. Apply 1 Frostbite to all Frostbitten enemies.",
                AbilitySprite = ResourceLoader.LoadSprite("IconHaborymCascade"),
                Visuals = Visuals.Crush,
                AnimationTarget = Targeting.Slot_Front,
                Cost = [Pigments.Blue, Pigments.YellowBlue, Pigments.Yellow],
                Effects = [
                    Effects.GenerateEffect(AddFrostbite, 4, Targeting.Slot_Front),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<TargetFrostValueModifyEffect>(), 4, Targeting.Slot_Front),
                    Effects.GenerateEffect(WindFrostbit),
                    Effects.GenerateEffect(AddFrostbite, 1, TheFrostbit),
                ],
            };
            cascade2.AddIntentsToTarget(Targeting.Slot_Front, ["Status_Frostbite", nameof(IntentType_GameIDs.Misc)]);
            cascade2.AddIntentsToTarget(TheFrostbit, ["Status_Frostbite"]);

            Ability cascade3 = new Ability("Cascading Ice", "HaborymCascade_3_A")
            {
                Description = "Apply 4 Frostbite to the Opposing enemy, then increase their Frostbite intensity by 5. Apply 2 Frostbite to all Frostbitten enemies.",
                AbilitySprite = ResourceLoader.LoadSprite("IconHaborymCascade"),
                Visuals = Visuals.Crush,
                AnimationTarget = Targeting.Slot_Front,
                Cost = [Pigments.Blue, Pigments.YellowBlue, Pigments.Yellow],
                Effects = [
                    Effects.GenerateEffect(AddFrostbite, 4, Targeting.Slot_Front),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<TargetFrostValueModifyEffect>(), 5, Targeting.Slot_Front),
                    Effects.GenerateEffect(WindFrostbit),
                    Effects.GenerateEffect(AddFrostbite, 2, TheFrostbit),
                ],
            };
            cascade3.AddIntentsToTarget(Targeting.Slot_Front, ["Status_Frostbite", nameof(IntentType_GameIDs.Misc)]);
            cascade3.AddIntentsToTarget(TheFrostbit, ["Status_Frostbite"]);

            Ability cascade4 = new Ability("Cascading Permafrost", "HaborymCascade_4_A")
            {
                Description = "Apply 4 Frostbite to the Opposing enemy, then increase their Frostbite intensity by 6. Apply 2 Frostbite to all Frostbitten enemies.",
                AbilitySprite = ResourceLoader.LoadSprite("IconHaborymCascade"),
                Visuals = Visuals.Crush,
                AnimationTarget = Targeting.Slot_Front,
                Cost = [Pigments.YellowBlue, Pigments.YellowBlue, Pigments.Yellow],
                Effects = [
                    Effects.GenerateEffect(AddFrostbite, 4, Targeting.Slot_Front),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<TargetFrostValueModifyEffect>(), 6, Targeting.Slot_Front),
                    Effects.GenerateEffect(WindFrostbit),
                    Effects.GenerateEffect(AddFrostbite, 2, TheFrostbit),
                ],
            };
            cascade4.AddIntentsToTarget(Targeting.Slot_Front, ["Status_Frostbite", nameof(IntentType_GameIDs.Misc)]);
            cascade4.AddIntentsToTarget(TheFrostbit, ["Status_Frostbite"]);

            haborym.AddLevelData(15, [gale1, frost1, cascade1]);
            haborym.AddLevelData(18, [gale2, frost2, cascade2]);
            haborym.AddLevelData(21, [gale3, frost3, cascade3]);
            haborym.AddLevelData(24, [gale4, frost4, cascade4]);

            haborym.AddFinalBossAchievementData(BossType_GameIDs.OsmanSinnoks.ToString(), "AApocrypha_Haborym_Witness_ACH");
            haborym.AddFinalBossAchievementData(BossType_GameIDs.Heaven.ToString(), "AApocrypha_Haborym_Divine_ACH");
            if (AApocrypha.CrossMod.EnemyPack) { haborym.AddFinalBossAchievementData("DoulaBoss", "AApocrypha_Haborym_Abstraction_ACH"); }
            if (AApocrypha.CrossMod.GlitchsFreaks) { haborym.AddFinalBossAchievementData("March_BOSS", "AApocrypha_Haborym_Inevitable_ACH"); }
            //if (AApocrypha.CrossMod.IntoTheAbyss) { haborym.AddFinalBossAchievementData("Nobody_BOSS", "AApocrypha_Haborym_Forgotten_ACH"); }
            //if (AApocrypha.CrossMod.IntoTheAbyss) { haborym.AddFinalBossAchievementData("Katalixi_BOSS", "AApocrypha_Haborym_Boundary_ACH"); }
            if (AApocrypha.CrossMod.SaltEnemies) { haborym.AddFinalBossAchievementData("BlueSky_BOSS", "AApocrypha_Haborym_Dreamer_ACH"); }
            haborym.AddCharacter(true, false);

            SpeakerBundle speakerBundleHaborym = new SpeakerBundle();
            speakerBundleHaborym.bundleTextColor = new Color32(177, 225, 235, 255);
            speakerBundleHaborym.dialogueSound = LoadedAssetsHandler.GetCharacter("Haborym_CH").dxSound;
            speakerBundleHaborym.portrait = ResourceLoader.LoadSprite("HaborymFront", new Vector2(0.5f, 0f), 32);

            SpeakerBundle speakerBundleHaborymBroken = new SpeakerBundle();
            speakerBundleHaborymBroken.bundleTextColor = speakerBundleHaborym.bundleTextColor;
            speakerBundleHaborymBroken.dialogueSound = LoadedAssetsHandler.GetCharacter("Haborym_CH").deathSound;
            speakerBundleHaborymBroken.portrait = ResourceLoader.LoadSprite("AnomalyDead", new Vector2(0.5f, 0f), 32);

            Dialogues.CreateAndAddCustom_SpeakerData("Haborym", speakerBundleHaborym, true, true, new SpeakerEmote[1]
            {
                new SpeakerEmote {
                    emotion = "Whoops",
                    bundle = speakerBundleHaborymBroken,
                },
            });
        }
    }
}
