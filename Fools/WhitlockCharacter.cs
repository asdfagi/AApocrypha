using System;
using System.Collections.Generic;
using System.Text;
using BrutalAPI;

namespace A_Apocrypha.Fools
{
    public class WhitlockCharacter
    {
        public static void Add()
        {
            Character whitlock = new Character("Whitlock", "Whitlock_CH")
            {
                HealthColor = Pigments.Purple,
                UsesBasicAbility = true,
                UsesAllAbilities = false,
                MovesOnOverworld = true,
                FrontSprite = ResourceLoader.LoadSprite("WhitlockFront", new Vector2(0.5f, 0f), 32),
                BackSprite = ResourceLoader.LoadSprite("WhitlockBack", new Vector2(0.5f, 0f), 32),
                OverworldSprite = ResourceLoader.LoadSprite("WhitlockOverworld", new Vector2(0.5f, 0f), 32),
                DamageSound = LoadedAssetsHandler.GetCharacter("Rags_CH").damageSound,
                DeathSound = LoadedAssetsHandler.GetCharacter("Rags_CH").deathSound,
                DialogueSound = LoadedAssetsHandler.GetCharacter("Rags_CH").dxSound,
                UnitTypes = ["FemaleID", "Sandwich_Gore"],
            };
            whitlock.GenerateMenuCharacter(ResourceLoader.LoadSprite("WhitlockMenu"), ResourceLoader.LoadSprite("WhitlockLocked")); //Whitlocked, one might say...
            whitlock.AddPassives([]);
            whitlock.SetMenuCharacterAsFullDPS();

            AddPassiveEffect AddLeaky = ScriptableObject.CreateInstance<AddPassiveEffect>();
            AddLeaky._passiveToAdd = Passives.Leaky1;

            HealEffect HealPrevious = ScriptableObject.CreateInstance<HealEffect>();
            HealPrevious.usePreviousExitValue = true;

            ConsumeCommonColorManaEffect ConsumeMostCommonMana = ScriptableObject.CreateInstance<ConsumeCommonColorManaEffect>();

            SwapToSidesEffect SwapEither = ScriptableObject.CreateInstance<SwapToSidesEffect>();

            PreviousEffectCondition PreviousTrue = ScriptableObject.CreateInstance<PreviousEffectCondition>();
            PreviousTrue.wasSuccessful = true;

            SwapHealthColorTwoTargetsEffect HealthSwapper = ScriptableObject.CreateInstance<SwapHealthColorTwoTargetsEffect>();

            Ability face1 = new Ability("Score the Face", "WhitlockFace_1")
            {
                Description = "Deal 7 damage to the Opposing enemy.\nApply Leaky(1) to the Opposing enemy if they do not have it.\nMove the Opposing enemy to the Left or Right.",
                AbilitySprite = ResourceLoader.LoadSprite("IconWhitlockFace"),
                Cost = [Pigments.Red, Pigments.Yellow],
                Visuals = Visuals.Flay,
                AnimationTarget = Targeting.Slot_Front,
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 7, Targeting.Slot_Front),
                    Effects.GenerateEffect(AddLeaky, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(SwapEither, 1, Targeting.Slot_Front),
                ]
            };
            face1.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Damage_7_10)]);
            face1.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.PA_Leaky)]);
            face1.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Swap_Sides)]);

            Ability face2 = new Ability("Cut the Face", "WhitlockFace_2")
            {
                Description = "Deal 9 damage to the Opposing enemy.\nApply Leaky(1) to the Opposing enemy if they do not have it.\nMove the Opposing enemy to the Left or Right.",
                AbilitySprite = ResourceLoader.LoadSprite("IconWhitlockFace"),
                Cost = [Pigments.Red, Pigments.YellowRed],
                Visuals = Visuals.Flay,
                AnimationTarget = Targeting.Slot_Front,
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 9, Targeting.Slot_Front),
                    Effects.GenerateEffect(AddLeaky, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(SwapEither, 1, Targeting.Slot_Front),
                ]
            };
            face2.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Damage_7_10)]);
            face2.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.PA_Leaky)]);
            face2.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Swap_Sides)]);

            Ability face3 = new Ability("Rip the Face", "WhitlockFace_3")
            {
                Description = "Deal 11 damage to the Opposing enemy.\nApply Leaky(1) to the Opposing enemy if they do not have it.\nMove the Opposing enemy to the Left or Right.",
                AbilitySprite = ResourceLoader.LoadSprite("IconWhitlockFace"),
                Cost = [Pigments.Red, Pigments.YellowRed],
                Visuals = Visuals.Flay,
                AnimationTarget = Targeting.Slot_Front,
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 11, Targeting.Slot_Front),
                    Effects.GenerateEffect(AddLeaky, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(SwapEither, 1, Targeting.Slot_Front),
                ]
            };
            face3.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Damage_11_15)]);
            face3.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.PA_Leaky)]);
            face3.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Swap_Sides)]);

            Ability face4 = new Ability("Excise the Face", "WhitlockFace_4")
            {
                Description = "Deal 13 damage to the Opposing enemy.\nApply Leaky to the Opposing enemy if they do not have it.\nMove the Opposing enemy to the Left or Right.",
                AbilitySprite = ResourceLoader.LoadSprite("IconWhitlockFace"),
                Cost = [Pigments.YellowRed, Pigments.YellowRed],
                Visuals = Visuals.Flay,
                AnimationTarget = Targeting.Slot_Front,
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 13, Targeting.Slot_Front),
                    Effects.GenerateEffect(AddLeaky, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(SwapEither, 1, Targeting.Slot_Front),
                ]
            };
            face4.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Damage_11_15)]);
            face4.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.PA_Leaky)]);
            face4.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Swap_Sides)]);

            Ability ointment1 = new Ability("Calming Ointment", "WhitlockOintment_1")
            {
                Description = "Consume up to 2 Pigment of the most common colour in the pigment bar.\nHeal the Left and Right party members by double the amount consumed.",
                AbilitySprite = ResourceLoader.LoadSprite("IconWhitlockOintment"),
                Cost = [Pigments.Blue, Pigments.Red],
                Visuals = Visuals.OilSlicked,
                AnimationTarget = Targeting.Slot_AllySides,
                Effects =
                [
                    Effects.GenerateEffect(ConsumeMostCommonMana, 2, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(HealPrevious, 2, Targeting.Slot_AllySides),
                ]
            };
            ointment1.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Mana_Consume)]);
            ointment1.AddIntentsToTarget(Targeting.Slot_AllySides, [nameof(IntentType_GameIDs.Heal_1_4)]);

            Ability ointment2 = new Ability("Numbing Ointment", "WhitlockOintment_2")
            {
                Description = "Consume up to 3 Pigment of the most common colour in the pigment bar.\nHeal the Left and Right party members by double the amount consumed.",
                AbilitySprite = ResourceLoader.LoadSprite("IconWhitlockOintment"),
                Cost = [Pigments.Blue, Pigments.Red],
                Visuals = Visuals.OilSlicked,
                AnimationTarget = Targeting.Slot_AllySides,
                Effects =
                [
                    Effects.GenerateEffect(ConsumeMostCommonMana, 3, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(HealPrevious, 2, Targeting.Slot_AllySides),
                ]
            };
            ointment2.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Mana_Consume)]);
            ointment2.AddIntentsToTarget(Targeting.Slot_AllySides, [nameof(IntentType_GameIDs.Heal_5_10)]);

            Ability ointment3 = new Ability("Sedative Ointment", "WhitlockOintment_3")
            {
                Description = "Consume up to 4 Pigment of the most common colour in the pigment bar.\nHeal the Left and Right party members by double the amount consumed.",
                AbilitySprite = ResourceLoader.LoadSprite("IconWhitlockOintment"),
                Cost = [Pigments.RedBlue, Pigments.RedBlue],
                Visuals = Visuals.OilSlicked,
                AnimationTarget = Targeting.Slot_AllySides,
                Effects =
                [
                    Effects.GenerateEffect(ConsumeMostCommonMana, 4, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(HealPrevious, 2, Targeting.Slot_AllySides),
                ]
            };
            ointment3.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Mana_Consume)]);
            ointment3.AddIntentsToTarget(Targeting.Slot_AllySides, [nameof(IntentType_GameIDs.Heal_5_10)]);

            Ability ointment4 = new Ability("Anesthetic Ointment", "WhitlockOintment_4")
            {
                Description = "Consume up to 5 Pigment of the most common colour in the pigment bar.\nHeal the Left and Right party members by double the amount consumed.",
                AbilitySprite = ResourceLoader.LoadSprite("IconWhitlockOintment"),
                Cost = [Pigments.RedBlue, Pigments.RedBlue],
                Visuals = Visuals.OilSlicked,
                AnimationTarget = Targeting.Slot_AllySides,
                Effects =
                [
                    Effects.GenerateEffect(ConsumeMostCommonMana, 5, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(HealPrevious, 2, Targeting.Slot_AllySides),
                ]
            };
            ointment4.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Mana_Consume)]);
            ointment4.AddIntentsToTarget(Targeting.Slot_AllySides, [nameof(IntentType_GameIDs.Heal_5_10)]);

            Ability transplant1 = new Ability("Improvised Transplant", "WhitlockTransplant_1")
            {
                Description = "Deal 5 damage to the Left and Right enemies.\nAttempt to swap the health colours of the Left and Right enemies.\nHeal this party member 1 health if health colours were swapped.",
                AbilitySprite = ResourceLoader.LoadSprite("IconWhitlockTransplant"),
                Cost = [Pigments.RedBlue, Pigments.RedBlue, Pigments.Yellow],
                Visuals = Visuals.Absolve,
                AnimationTarget = Targeting.Slot_OpponentSides,
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 5, Targeting.Slot_OpponentSides),
                    Effects.GenerateEffect(HealthSwapper, 1, Targeting.Slot_OpponentSides),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<HealEffect>(), 1, Targeting.Slot_SelfSlot, PreviousTrue),
                ]
            };
            transplant1.AddIntentsToTarget(Targeting.Slot_OpponentSides, [nameof(IntentType_GameIDs.Damage_3_6)]);
            transplant1.AddIntentsToTarget(Targeting.Slot_OpponentSides, [nameof(IntentType_GameIDs.Mana_Modify)]);
            transplant1.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Heal_1_4)]);

            Ability transplant2 = new Ability("Amateur Transplant", "WhitlockTransplant_2")
            {
                Description = "Deal 7 damage to the Left and Right enemies.\nAttempt to swap the health colours of the Left and Right enemies.\nHeal this party member 1 health if health colours were swapped.",
                AbilitySprite = ResourceLoader.LoadSprite("IconWhitlockTransplant"),
                Cost = [Pigments.RedBlue, Pigments.RedBlue, Pigments.Yellow],
                Visuals = Visuals.Absolve,
                AnimationTarget = Targeting.Slot_OpponentSides,
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 7, Targeting.Slot_OpponentSides),
                    Effects.GenerateEffect(HealthSwapper, 1, Targeting.Slot_OpponentSides),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<HealEffect>(), 1, Targeting.Slot_SelfSlot, PreviousTrue),
                ]
            };
            transplant2.AddIntentsToTarget(Targeting.Slot_OpponentSides, [nameof(IntentType_GameIDs.Damage_7_10)]);
            transplant2.AddIntentsToTarget(Targeting.Slot_OpponentSides, [nameof(IntentType_GameIDs.Mana_Modify)]);
            transplant2.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Heal_1_4)]);

            Ability transplant3 = new Ability("Unpracticed Transplant", "WhitlockTransplant_3")
            {
                Description = "Deal 9 damage to the Left and Right enemies.\nAttempt to swap the health colours of the Left and Right enemies.\nHeal this party member 2 health if health colours were swapped.",
                AbilitySprite = ResourceLoader.LoadSprite("IconWhitlockTransplant"),
                Cost = [Pigments.RedBlue, Pigments.RedBlue, Pigments.Yellow],
                Visuals = Visuals.Absolve,
                AnimationTarget = Targeting.Slot_OpponentSides,
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 9, Targeting.Slot_OpponentSides),
                    Effects.GenerateEffect(HealthSwapper, 1, Targeting.Slot_OpponentSides),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<HealEffect>(), 2, Targeting.Slot_SelfSlot, PreviousTrue),
                ]
            };
            transplant3.AddIntentsToTarget(Targeting.Slot_OpponentSides, [nameof(IntentType_GameIDs.Damage_7_10)]);
            transplant3.AddIntentsToTarget(Targeting.Slot_OpponentSides, [nameof(IntentType_GameIDs.Mana_Modify)]);
            transplant3.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Heal_1_4)]);

            Ability transplant4 = new Ability("Competent Transplant", "WhitlockTransplant_4")
            {
                Description = "Deal 11 damage to the Left and Right enemies.\nAttempt to swap the health colours of the Left and Right enemies.\nHeal this party member 3 health if health colours were swapped.",
                AbilitySprite = ResourceLoader.LoadSprite("IconWhitlockTransplant"),
                Cost = [Pigments.RedBlue, Pigments.RedBlue, Pigments.Yellow],
                Visuals = Visuals.Absolve,
                AnimationTarget = Targeting.Slot_OpponentSides,
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 11, Targeting.Slot_OpponentSides),
                    Effects.GenerateEffect(HealthSwapper, 1, Targeting.Slot_OpponentSides),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<HealEffect>(), 3, Targeting.Slot_SelfSlot, PreviousTrue),
                ]
            };
            transplant4.AddIntentsToTarget(Targeting.Slot_OpponentSides, [nameof(IntentType_GameIDs.Damage_11_15)]);
            transplant4.AddIntentsToTarget(Targeting.Slot_OpponentSides, [nameof(IntentType_GameIDs.Mana_Modify)]);
            transplant4.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Heal_1_4)]);

            whitlock.AddLevelData(15, [face1, transplant1, ointment1]);
            whitlock.AddLevelData(18, [face2, transplant2, ointment2]);
            whitlock.AddLevelData(20, [face3, transplant3, ointment3]);
            whitlock.AddLevelData(22, [face4, transplant4, ointment4]);

            whitlock.AddFinalBossAchievementData(BossType_GameIDs.OsmanSinnoks.ToString(), "AApocrypha_Whitlock_Witness_ACH");
            whitlock.AddFinalBossAchievementData(BossType_GameIDs.Heaven.ToString(), "AApocrypha_Whitlock_Divine_ACH");
            if (AApocrypha.CrossMod.EnemyPack) { whitlock.AddFinalBossAchievementData("DoulaBoss", "AApocrypha_Whitlock_Abstraction_ACH"); }
            if (AApocrypha.CrossMod.GlitchsFreaks) { whitlock.AddFinalBossAchievementData("March_BOSS", "AApocrypha_Whitlock_Inevitable_ACH"); }
            if (AApocrypha.CrossMod.IntoTheAbyss) { whitlock.AddFinalBossAchievementData("Nobody_BOSS", "AApocrypha_Whitlock_Forgotten_ACH"); }
            whitlock.AddCharacter(true, false);
        }
    }
}
