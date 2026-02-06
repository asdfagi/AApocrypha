using System;
using System.Collections.Generic;
using System.Text;
using MythosFriends.Effectsa;

namespace A_Apocrypha.Fools
{
    public class KneynsbergCharacter
    {
        public static void Add()
        {
            Character kneynsberg = new Character("Kneynsberg", "Kneynsberg_CH")
            {
                HealthColor = Pigments.Purple,
                UsesBasicAbility = true,
                UsesAllAbilities = false,
                MovesOnOverworld = true,
                FrontSprite = ResourceLoader.LoadSprite("KneynsbergFront", new Vector2(0.5f, 0f), 32),
                BackSprite = ResourceLoader.LoadSprite("KneynsbergBack", new Vector2(0.5f, 0f), 32),
                OverworldSprite = ResourceLoader.LoadSprite("KneynsbergOverworld", new Vector2(0.5f, 0f), 32),
                DamageSound = LoadedAssetsHandler.GetCharacter("Griffin_CH").damageSound,
                DeathSound = LoadedAssetsHandler.GetCharacter("Griffin_CH").deathSound,
                DialogueSound = LoadedAssetsHandler.GetCharacter("Griffin_CH").dxSound,
                UnitTypes = ["Male_ID", "Sandwich_Spirit", "Neathy"],
            };
            kneynsberg.GenerateMenuCharacter(ResourceLoader.LoadSprite("KneynsbergMenu"), ResourceLoader.LoadSprite("KneynsbergLocked"));
            kneynsberg.AddPassives([Passives.GetCustomPassive("DriedOut_PA")]);
            kneynsberg.SetMenuCharacterAsFullDPS();

            StatusEffect_Apply_Effect AddRuptured = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            AddRuptured._Status = StatusField.Ruptured;

            StatusEffect_Apply_Effect AddScars = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            AddScars._Status = StatusField.Scars;

            AddPassiveEffect AddLeaky = ScriptableObject.CreateInstance<AddPassiveEffect>();
            AddLeaky._passiveToAdd = Passives.Leaky1;

            HealEffect HealPrevious = ScriptableObject.CreateInstance<HealEffect>();
            HealPrevious.usePreviousExitValue = true;

            PreviousEffectCondition PreviousTrue = ScriptableObject.CreateInstance<PreviousEffectCondition>();
            PreviousTrue.wasSuccessful = true;

            AttackVisualsSO JauntVisuals = Visuals.Crush;
            if (AApocrypha.CrossMod.SaltEnemies)
            {
                JauntVisuals = LoadedAssetsHandler.GetEnemyAbility("Reflections_A").visuals;
            }

            AnimationVisualsEffect JauntAnim = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            JauntAnim._visuals = JauntVisuals;
            JauntAnim._animationTarget = Targeting.Slot_Front;

            AnimationVisualsEffect JauntAnimSides = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            JauntAnimSides._visuals = JauntVisuals;
            JauntAnimSides._animationTarget = Targeting.Slot_OpponentSides;

            AnimationVisualsEffect JauntAnimFrontSides = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            JauntAnimFrontSides._visuals = JauntVisuals;
            JauntAnimFrontSides._animationTarget = Targeting.Slot_FrontAndSides;

            AnimationVisualsEffect InvAnimSides = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            InvAnimSides._visuals = Visuals.Exsanguinate;
            InvAnimSides._animationTarget = Targeting.Slot_OpponentSides;

            AnimationVisualsEffect InvAnimFarSides = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            InvAnimFarSides._visuals = Visuals.Exsanguinate;
            InvAnimFarSides._animationTarget = Targeting.Slot_OpponentSidesAndFarSides;

            IncreaseStatusEffectsEffect IncreaseBadStatus = ScriptableObject.CreateInstance<IncreaseStatusEffectsEffect>();
            IncreaseBadStatus.m_AffectStatusEffects = true;
            IncreaseBadStatus.m_AffectFieldEffects = false;
            IncreaseBadStatus._increasePositives = false;

            IncreaseStatusEffectsEffect IncreaseGoodStatus = ScriptableObject.CreateInstance<IncreaseStatusEffectsEffect>();
            IncreaseBadStatus.m_AffectStatusEffects = true;
            IncreaseBadStatus.m_AffectFieldEffects = false;
            IncreaseBadStatus._increasePositives = true;

            DamageEffect IndirectDamage = ScriptableObject.CreateInstance<DamageEffect>();
            IndirectDamage._indirect = true;

            Ability jaunt1 = new Ability("Cutting Jaunt", "KneynsbergJaunt_1")
            {
                Description = "Mirror this party member's position and deal 5 damage to the newly Opposing enemy.\nApply 2 Ruptured to the Opposing enemy.",
                AbilitySprite = ResourceLoader.LoadSprite("IconKneynsbergJaunt"),
                Cost = [Pigments.Red, Pigments.Red],
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<MirrorPositionEffect>(), 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(JauntAnim, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 5, Targeting.Slot_Front),
                    Effects.GenerateEffect(AddRuptured, 2, Targeting.Slot_Front),
                ]
            };
            jaunt1.AddIntentsToTarget(TbazTargeting.MirrorAndSelf(true), [nameof(IntentType_GameIDs.Swap_Mass)]);
            jaunt1.AddIntentsToTarget(TbazTargeting.Mirror(false), [nameof(IntentType_GameIDs.Damage_3_6)]);
            jaunt1.AddIntentsToTarget(TbazTargeting.Mirror(false), [nameof(IntentType_GameIDs.Status_Ruptured)]);

            Ability jaunt2 = new Ability("Splintering Jaunt", "KneynsbergJaunt_2")
            {
                Description = "Mirror this party member's position and deal 7 damage to the newly Opposing enemy.\nApply 2 Ruptured to the Opposing enemy.",
                AbilitySprite = ResourceLoader.LoadSprite("IconKneynsbergJaunt"),
                Cost = [Pigments.Red, Pigments.Red],
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<MirrorPositionEffect>(), 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(JauntAnim, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 7, Targeting.Slot_Front),
                    Effects.GenerateEffect(AddRuptured, 2, Targeting.Slot_Front),
                ]
            };
            jaunt2.AddIntentsToTarget(TbazTargeting.MirrorAndSelf(true), [nameof(IntentType_GameIDs.Swap_Mass)]);
            jaunt2.AddIntentsToTarget(TbazTargeting.Mirror(false), [nameof(IntentType_GameIDs.Damage_7_10)]);
            jaunt2.AddIntentsToTarget(TbazTargeting.Mirror(false), [nameof(IntentType_GameIDs.Status_Ruptured)]);

            Ability jaunt3 = new Ability("Shattering Jaunt", "KneynsbergJaunt_3")
            {
                Description = "Mirror this party member's position and deal 9 damage to the newly Opposing enemy.\nApply 3 Ruptured to the Opposing enemy.",
                AbilitySprite = ResourceLoader.LoadSprite("IconKneynsbergJaunt"),
                Cost = [Pigments.Red, Pigments.Red],
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<MirrorPositionEffect>(), 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(JauntAnim, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 9, Targeting.Slot_Front),
                    Effects.GenerateEffect(AddRuptured, 3, Targeting.Slot_Front),
                ]
            };
            jaunt3.AddIntentsToTarget(TbazTargeting.MirrorAndSelf(true), [nameof(IntentType_GameIDs.Swap_Mass)]);
            jaunt3.AddIntentsToTarget(TbazTargeting.Mirror(false), [nameof(IntentType_GameIDs.Damage_7_10)]);
            jaunt3.AddIntentsToTarget(TbazTargeting.Mirror(false), [nameof(IntentType_GameIDs.Status_Ruptured)]);

            Ability jaunt4 = new Ability("Eviscerating Jaunt", "KneynsbergJaunt_4")
            {
                Description = "Mirror this party member's position and deal 11 damage to the newly Opposing enemy.\nApply 3 Ruptured to the Opposing enemy.",
                AbilitySprite = ResourceLoader.LoadSprite("IconKneynsbergJaunt"),
                Cost = [Pigments.Red, Pigments.Red],
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<MirrorPositionEffect>(), 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(JauntAnim, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 11, Targeting.Slot_Front),
                    Effects.GenerateEffect(AddRuptured, 3, Targeting.Slot_Front),
                ]
            };
            jaunt4.AddIntentsToTarget(TbazTargeting.MirrorAndSelf(true), [nameof(IntentType_GameIDs.Swap_Mass)]);
            jaunt4.AddIntentsToTarget(TbazTargeting.Mirror(false), [nameof(IntentType_GameIDs.Damage_11_15)]);
            jaunt4.AddIntentsToTarget(TbazTargeting.Mirror(false), [nameof(IntentType_GameIDs.Status_Ruptured)]);

            Ability inversion1 = new Ability("Trivial Inversion", "KneynsbergInversion_1")
            {
                Description = "Apply 2 Ruptured to the Opposing enemy.\nTry to swap the positions of the Left and Right enemies.\nDeal 2 indirect damage to any targets that were not moved.",
                AbilitySprite = ResourceLoader.LoadSprite("IconKneynsbergInversion"),
                Cost = [Pigments.Purple, Pigments.Red, Pigments.Red],
                Effects =
                [
                    Effects.GenerateEffect(JauntAnim),
                    Effects.GenerateEffect(AddRuptured, 2, Targeting.Slot_Front),
                    Effects.GenerateEffect(InvAnimSides),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<SwapTwoTargetsEffect>(), 1, Targeting.Slot_OpponentSides),
                    Effects.GenerateEffect(IndirectDamage, 2, Targeting.Slot_OpponentSides, PreviousGenerator(false, 1)),
                ]
            };
            inversion1.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Status_Ruptured)]);
            inversion1.AddIntentsToTarget(Targeting.Slot_OpponentSides, [nameof(IntentType_GameIDs.Swap_Mass)]);
            inversion1.AddIntentsToTarget(Targeting.Slot_OpponentSides, [nameof(IntentType_GameIDs.Damage_1_2)]);

            Ability inversion2 = new Ability("Simple Inversion", "KneynsbergInversion_2")
            {
                Description = "Apply 2 Ruptured to the Left and Right enemies.\nTry to swap the positions of the Left and Right enemies.\nDeal 3 indirect damage to any targets that were not moved.",
                AbilitySprite = ResourceLoader.LoadSprite("IconKneynsbergInversion"),
                Cost = [Pigments.RedPurple, Pigments.Red, Pigments.Red],
                Effects =
                [
                    Effects.GenerateEffect(JauntAnimSides),
                    Effects.GenerateEffect(AddRuptured, 2, Targeting.Slot_OpponentSides),
                    Effects.GenerateEffect(InvAnimSides),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<SwapTwoTargetsEffect>(), 1, Targeting.Slot_OpponentSides),
                    Effects.GenerateEffect(IndirectDamage, 3, Targeting.Slot_OpponentSides, PreviousGenerator(false, 1)),
                ]
            };
            inversion2.AddIntentsToTarget(Targeting.Slot_OpponentSides, [nameof(IntentType_GameIDs.Status_Ruptured)]);
            inversion2.AddIntentsToTarget(Targeting.Slot_OpponentSides, [nameof(IntentType_GameIDs.Swap_Mass)]);
            inversion2.AddIntentsToTarget(Targeting.Slot_OpponentSides, [nameof(IntentType_GameIDs.Damage_3_6)]);

            Ability inversion3 = new Ability("Complex Inversion", "KneynsbergInversion_3")
            {
                Description = "Apply 3 Ruptured to the Left, Right and Opposing enemies.\nTry to swap the positions of the Left and Right enemies and the Far Left and Far Right enemies.\nDeal 3 indirect damage to any targets that were not moved.",
                AbilitySprite = ResourceLoader.LoadSprite("IconKneynsbergInversion"),
                Cost = [Pigments.RedPurple, Pigments.Red, Pigments.Red],
                Effects =
                [
                    Effects.GenerateEffect(JauntAnimFrontSides),
                    Effects.GenerateEffect(AddRuptured, 3, Targeting.Slot_FrontAndSides),
                    Effects.GenerateEffect(InvAnimFarSides),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<SwapTwoTargetsEffect>(), 1, Targeting.Slot_OpponentSides),
                    Effects.GenerateEffect(IndirectDamage, 3, Targeting.Slot_OpponentSides, PreviousGenerator(false, 1)),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<SwapTwoTargetsEffect>(), 1, Targeting.Slot_OpponentFarSides),
                    Effects.GenerateEffect(IndirectDamage, 3, Targeting.Slot_OpponentFarSides, PreviousGenerator(false, 1)),
                ]
            };
            inversion3.AddIntentsToTarget(Targeting.Slot_FrontAndSides, [nameof(IntentType_GameIDs.Status_Ruptured)]);
            inversion3.AddIntentsToTarget(Targeting.Slot_OpponentSidesAndFarSides, [nameof(IntentType_GameIDs.Swap_Mass)]);
            inversion3.AddIntentsToTarget(Targeting.Slot_OpponentSidesAndFarSides, [nameof(IntentType_GameIDs.Damage_3_6)]);

            Ability inversion4 = new Ability("Parabolan Inversion", "KneynsbergInversion_4")
            {
                Description = "Apply 3 Ruptured to the Left, Right and Opposing enemies.\nTry to swap the positions of the Left and Right enemies and the Far Left and Far Right enemies.\nDeal 4 indirect damage to any targets that were not moved.",
                AbilitySprite = ResourceLoader.LoadSprite("IconKneynsbergInversion"),
                Cost = [Pigments.RedPurple, Pigments.RedPurple, Pigments.Red],
                Effects =
                [
                    Effects.GenerateEffect(JauntAnimFrontSides),
                    Effects.GenerateEffect(AddRuptured, 3, Targeting.Slot_FrontAndSides),
                    Effects.GenerateEffect(InvAnimFarSides),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<SwapTwoTargetsEffect>(), 1, Targeting.Slot_OpponentSides),
                    Effects.GenerateEffect(IndirectDamage, 4, Targeting.Slot_OpponentSides, PreviousGenerator(false, 1)),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<SwapTwoTargetsEffect>(), 1, Targeting.Slot_OpponentFarSides),
                    Effects.GenerateEffect(IndirectDamage, 4, Targeting.Slot_OpponentFarSides, PreviousGenerator(false, 1)),
                ]
            };
            inversion4.AddIntentsToTarget(Targeting.Slot_FrontAndSides, [nameof(IntentType_GameIDs.Status_Ruptured)]);
            inversion4.AddIntentsToTarget(Targeting.Slot_OpponentSidesAndFarSides, [nameof(IntentType_GameIDs.Swap_Mass)]);
            inversion4.AddIntentsToTarget(Targeting.Slot_OpponentSidesAndFarSides, [nameof(IntentType_GameIDs.Damage_3_6)]);

            Ability reflection1 = new Ability("Faint Reflection", "KneynsbergReflection_1")
            {
                Description = "Swap status effects with the Opposing enemy.\nMove the Opposing enemy to the Left or Right.\nIncrease all status effects on this party member by 2. If this fails, apply 1 Scar to this party member.",
                AbilitySprite = ResourceLoader.LoadSprite("IconKneynsbergReflection"),
                Cost = [Pigments.Purple, Pigments.Blue],
                Visuals = Visuals.Exsanguinate,
                AnimationTarget = Targeting.Slot_SelfSlot,
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<CasterSwapStatusWithTargetEffect>(), 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<SwapToSidesEffect>(), 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(IncreaseGoodStatus, 2, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(IncreaseBadStatus, 2, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(AddScars, 1, Targeting.Slot_SelfSlot, Effects.CheckMultiplePreviousEffectsCondition([false, false], [1, 2])),
                ]
            };
            reflection1.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Misc)]);
            reflection1.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Misc)]);
            reflection1.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Swap_Sides)]);
            reflection1.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Status_Scars)]);

            Ability reflection2 = new Ability("Hazy Reflection", "KneynsbergReflection_2")
            {
                Description = "Swap status effects with the Opposing enemy.\nMove the Opposing enemy to the Left or Right.\nIncrease all status effects on this party member by 2. If this fails, apply 1 Scar to this party member.",
                AbilitySprite = ResourceLoader.LoadSprite("IconKneynsbergReflection"),
                Cost = [Pigments.BluePurple, Pigments.Blue],
                Visuals = Visuals.Exsanguinate,
                AnimationTarget = Targeting.Slot_SelfSlot,
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<CasterSwapStatusWithTargetEffect>(), 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<SwapToSidesEffect>(), 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(IncreaseGoodStatus, 2, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(IncreaseBadStatus, 2, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(AddScars, 1, Targeting.Slot_SelfSlot, Effects.CheckMultiplePreviousEffectsCondition([false, false], [1, 2])),
                ]
            };
            reflection2.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Misc)]);
            reflection2.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Misc)]);
            reflection2.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Swap_Sides)]);
            reflection2.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Status_Scars)]);

            Ability reflection3 = new Ability("Blurry Reflection", "KneynsbergReflection_3")
            {
                Description = "Swap status effects with the Opposing enemy.\nMove the Opposing enemy to the Left or Right.\nIncrease all status effects on this party member by 3. If this fails, apply 2 Scars to this party member.",
                AbilitySprite = ResourceLoader.LoadSprite("IconKneynsbergReflection"),
                Cost = [Pigments.BluePurple, Pigments.Blue],
                Visuals = Visuals.Exsanguinate,
                AnimationTarget = Targeting.Slot_SelfSlot,
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<CasterSwapStatusWithTargetEffect>(), 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<SwapToSidesEffect>(), 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(IncreaseGoodStatus, 3, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(IncreaseBadStatus, 3, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(AddScars, 2, Targeting.Slot_SelfSlot, Effects.CheckMultiplePreviousEffectsCondition([false, false], [1, 2])),
                ]
            };
            reflection3.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Misc)]);
            reflection3.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Misc)]);
            reflection3.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Swap_Sides)]);
            reflection3.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Status_Scars)]);

            Ability reflection4 = new Ability("Clear Reflection", "KneynsbergReflection_4")
            {
                Description = "Swap status effects with the Opposing enemy.\nMove the Opposing enemy to the Left or Right.\nIncrease all status effects on this party member by 3. If this fails, apply 3 Scars to this party member.",
                AbilitySprite = ResourceLoader.LoadSprite("IconKneynsbergReflection"),
                Cost = [Pigments.BluePurple, Pigments.BluePurple],
                Visuals = Visuals.Exsanguinate,
                AnimationTarget = Targeting.Slot_SelfSlot,
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<CasterSwapStatusWithTargetEffect>(), 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<SwapToSidesEffect>(), 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(IncreaseGoodStatus, 3, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(IncreaseBadStatus, 3, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(AddScars, 3, Targeting.Slot_SelfSlot, Effects.CheckMultiplePreviousEffectsCondition([false, false], [1, 2])),
                ]
            };
            reflection4.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Misc)]);
            reflection4.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Misc)]);
            reflection4.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Swap_Sides)]);
            reflection4.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Status_Scars)]);

            kneynsberg.AddLevelData(9, [inversion1, jaunt1, reflection1]);
            kneynsberg.AddLevelData(11, [inversion2, jaunt2, reflection2]);
            kneynsberg.AddLevelData(13, [inversion3, jaunt3, reflection3]);
            kneynsberg.AddLevelData(15, [inversion4, jaunt4, reflection4]);

            kneynsberg.AddFinalBossAchievementData(BossType_GameIDs.OsmanSinnoks.ToString(), "AApocrypha_Kneynsberg_Witness_ACH");
            kneynsberg.AddFinalBossAchievementData(BossType_GameIDs.Heaven.ToString(), "AApocrypha_Kneynsberg_Divine_ACH");
            if (AApocrypha.CrossMod.EnemyPack) { kneynsberg.AddFinalBossAchievementData("DoulaBoss", "AApocrypha_Kneynsberg_Abstraction_ACH"); }
            if (AApocrypha.CrossMod.GlitchsFreaks) { kneynsberg.AddFinalBossAchievementData("March_BOSS", "AApocrypha_Kneynsberg_Inevitable_ACH"); }
            if (AApocrypha.CrossMod.IntoTheAbyss) { kneynsberg.AddFinalBossAchievementData("Nobody_BOSS", "AApocrypha_Kneynsberg_Forgotten_ACH"); }
            //if (AApocrypha.CrossMod.SaltEnemies) { kneynsberg.AddFinalBossAchievementData("BlueSky_BOSS", "AApocrypha_Kneynsberg_Dreamer_ACH"); }
            kneynsberg.AddCharacter(true, false);

            SpeakerBundle speakerBundleKneynsberg = new SpeakerBundle();
            speakerBundleKneynsberg.bundleTextColor = new Color(0.93f, 0.76f, 0.6f);
            speakerBundleKneynsberg.dialogueSound = LoadedAssetsHandler.GetCharacter("Kneynsberg_CH").dxSound;
            speakerBundleKneynsberg.portrait = ResourceLoader.LoadSprite("KneynsbergFront", new Vector2(0.5f, 0f), 32);
            Dialogues.CreateAndAddCustom_SpeakerData("Kneynsberg", speakerBundleKneynsberg, true, false, new SpeakerEmote[0]);

            SpeakerBundle speakerBundleKneynsbergFingerking = new SpeakerBundle();
            speakerBundleKneynsbergFingerking.bundleTextColor = new Color(0.85f, 0.9f, 0.86f);
            speakerBundleKneynsbergFingerking.dialogueSound = LoadedAssetsHandler.GetCharacter("Kneynsberg_CH").dxSound;
            speakerBundleKneynsbergFingerking.portrait = ResourceLoader.LoadSprite("KneynsbergFrontFingerking", new Vector2(0.5f, 0f), 32);
            Dialogues.CreateAndAddCustom_SpeakerData("KneynsbergFingerking", speakerBundleKneynsbergFingerking, true, false, new SpeakerEmote[0]);

            SpeakerBundle speakerBundleKneynsbergMirror = new SpeakerBundle();
            speakerBundleKneynsbergMirror.bundleTextColor = new Color(187, 198, 212, 255);
            speakerBundleKneynsbergMirror.dialogueSound = "event:/AASFX/DX/kneynsberg-mirrored-dx";
            speakerBundleKneynsbergMirror.portrait = ResourceLoader.LoadSprite("KneynsbergFrontMirror", new Vector2(0.5f, 0f), 32);
            Dialogues.CreateAndAddCustom_SpeakerData("KneynsbergMirror", speakerBundleKneynsbergMirror, true, false, new SpeakerEmote[0]);
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
