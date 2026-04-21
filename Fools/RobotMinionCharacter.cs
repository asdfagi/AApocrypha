using System;
using System.Collections.Generic;
using System.Text;
using BrutalAPI;

namespace A_Apocrypha.Fools
{
    public class RobotMinionCharacter
    {
        public static void Add()
        {
            List<string> roboTypes = ["Robot", "Sandwich_Robot"];
            string roboHurt = LoadedAssetsHandler.GetCharacter("Gospel_CH").damageSound;
            string roboDie = LoadedAssetsHandler.GetCharacter("Gospel_CH").deathSound;
            string roboTalk = LoadedAssetsHandler.GetCharacter("Gospel_CH").dxSound;
            Sprite roboBack = ResourceLoader.LoadSprite("RobotMinionBackBase", new Vector2(0.5f, 0f), 32);
            Sprite roboWorld = ResourceLoader.LoadSprite("RobotMinionOverworld", new Vector2(0.5f, 0f), 32);

            Character robotclaw = new Character("Pincer Robot", "AA_RobotMinionClaw_CH")
            {
                HealthColor = Pigments.Grey,
                UsesBasicAbility = false,
                UsesAllAbilities = true,
                MovesOnOverworld = true,
                FrontSprite = ResourceLoader.LoadSprite("RobotMinionFrontClaw", new Vector2(0.5f, 0f), 32),
                BackSprite = roboBack,
                OverworldSprite = roboWorld,
                DamageSound = roboHurt,
                DeathSound = roboDie,
                DialogueSound = roboTalk,
                UnitTypes = roboTypes,
            };
            robotclaw.AddPassives([]);

            Character robotsaw = new Character("Sawblade Robot", "AA_RobotMinionSaw_CH")
            {
                HealthColor = Pigments.Grey,
                UsesBasicAbility = false,
                UsesAllAbilities = true,
                MovesOnOverworld = true,
                FrontSprite = ResourceLoader.LoadSprite("RobotMinionFrontBlade", new Vector2(0.5f, 0f), 32),
                BackSprite = roboBack,
                OverworldSprite = roboWorld,
                DamageSound = roboHurt,
                DeathSound = roboDie,
                DialogueSound = roboTalk,
                UnitTypes = roboTypes,
            };
            robotsaw.AddPassives([]);

            AddPassiveEffect AddLeaky = ScriptableObject.CreateInstance<AddPassiveEffect>();
            AddLeaky._passiveToAdd = Passives.Leaky1;

            HealEffect HealPrevious = ScriptableObject.CreateInstance<HealEffect>();
            HealPrevious.usePreviousExitValue = true;

            ConsumeCommonColorManaEffect ConsumeMostCommonMana = ScriptableObject.CreateInstance<ConsumeCommonColorManaEffect>();

            SwapToSidesEffect SwapEither = ScriptableObject.CreateInstance<SwapToSidesEffect>();

            SwapToOneSideEffect SwapLeft = ScriptableObject.CreateInstance<SwapToOneSideEffect>();
            SwapLeft._swapRight = false;

            SwapToOneSideEffect SwapRight = ScriptableObject.CreateInstance<SwapToOneSideEffect>();
            SwapRight._swapRight = true;

            PreviousEffectCondition PreviousTrue = ScriptableObject.CreateInstance<PreviousEffectCondition>();
            PreviousTrue.wasSuccessful = true;

            SwapHealthColorTwoTargetsEffect HealthSwapper = ScriptableObject.CreateInstance<SwapHealthColorTwoTargetsEffect>();

            StatusEffect_Apply_Effect RupturedApply = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            RupturedApply._Status = StatusField.Ruptured;

            Ability clawL = new Ability("Drag Right", "RobotMinionClawL_A")
            {
                Description = "Deal 5 damage to the Left enemy and move it in front of this party member.",
                AbilitySprite = ResourceLoader.LoadSprite("IconRobotMinionClawL"),
                Cost = [Pigments.Red, Pigments.Yellow],
                Visuals = Visuals.Crush,
                AnimationTarget = Targeting.Slot_OpponentLeft,
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 5, Targeting.Slot_OpponentLeft),
                    Effects.GenerateEffect(SwapRight, 1, Targeting.Slot_OpponentLeft),
                ]
            };
            clawL.AddIntentsToTarget(Targeting.Slot_OpponentLeft, [nameof(IntentType_GameIDs.Damage_3_6), nameof(IntentType_GameIDs.Swap_Right)]);

            Ability clawR = new Ability("Pull Left", "RobotMinionClawR_A")
            {
                Description = "Deal 5 damage to the Right enemy and move it in front of this party member.",
                AbilitySprite = ResourceLoader.LoadSprite("IconRobotMinionClawR"),
                Cost = [Pigments.Red, Pigments.Yellow],
                Visuals = Visuals.Crush,
                AnimationTarget = Targeting.Slot_OpponentRight,
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 5, Targeting.Slot_OpponentRight),
                    Effects.GenerateEffect(SwapLeft, 1, Targeting.Slot_OpponentRight),
                ]
            };
            clawR.AddIntentsToTarget(Targeting.Slot_OpponentRight, [nameof(IntentType_GameIDs.Damage_3_6), nameof(IntentType_GameIDs.Swap_Left)]);

            Ability saw = new Ability("Excision", "RobotMinionSaw_A")
            {
                Description = "Deal 5 damage to the Opposing enemy. Apply 2 Ruptured to the Opposing enemy.",
                AbilitySprite = ResourceLoader.LoadSprite("IconRobotMinionSaw"),
                Cost = [Pigments.Red, Pigments.Yellow],
                Visuals = Visuals.Slash,
                AnimationTarget = Targeting.Slot_Front,
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 5, Targeting.Slot_Front),
                    Effects.GenerateEffect(RupturedApply, 2, Targeting.Slot_Front),
                ]
            };
            saw.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Damage_3_6), nameof(IntentType_GameIDs.Status_Ruptured)]);

            robotclaw.AddLevelData(10, [clawL, clawR]);
            robotclaw.AddCharacter(true, true);

            robotsaw.AddLevelData(10, [saw]);
            robotsaw.AddCharacter(true, true);
        }
    }
}
