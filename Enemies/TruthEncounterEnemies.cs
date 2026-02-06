using System;
using System.Collections.Generic;
using System.Text;
using A_Apocrypha.CustomOther;

namespace A_Apocrypha.Enemies
{
    public class TruthEncounterEnemies
    {
        public static void Add()
        {
            PassiveLockingEffect AnchoredLock = ScriptableObject.CreateInstance<PassiveLockingEffect>();
            AnchoredLock._lock = true;
            AnchoredLock.m_PassiveIDs = [Passives.Anchored.m_PassiveID];

            PassiveLockingEffect AnchoredUnlock = ScriptableObject.CreateInstance<PassiveLockingEffect>();
            AnchoredUnlock._lock = false;
            AnchoredUnlock.m_PassiveIDs = [Passives.Anchored.m_PassiveID];

            SwapToOneSideEffect SwapLeft = ScriptableObject.CreateInstance<SwapToOneSideEffect>();
            SwapLeft._swapRight = false;

            SwapToOneSideEffect SwapRight = ScriptableObject.CreateInstance<SwapToOneSideEffect>();
            SwapRight._swapRight = true;

            string PurpleID = ColorUtility.ToHtmlStringRGB(Color.magenta);

            Ability immovableLeft = new Ability("<color=#" + PurpleID + ">UNSTOPPABLE</color>", "AApocrypha_TruthImmovableLeft_A")
            {
                Description = "Disable the effects of Anchored on all units until this ability is completed." +
                "\nMove this enemy to the Left twice. This movement assumes the grid wraps around." +
                "\nDeal an Agonizing amount of damage to the Opposing party member.",
                Cost = [Pigments.Grey, Pigments.Purple],
                Effects =
                [
                    Effects.GenerateEffect(AnchoredLock),
                    Effects.GenerateEffect(SwapLeft, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<CasterInEdgesCheckEffect>(), 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 1)),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<MirrorPositionEffect>(), 1, Targeting.Slot_SelfSlot, Effects.CheckMultiplePreviousEffectsCondition([true, false], [1, 2])),
                    Effects.GenerateEffect(SwapLeft, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<CasterInEdgesCheckEffect>(), 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 1)),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<MirrorPositionEffect>(), 1, Targeting.Slot_SelfSlot, Effects.CheckMultiplePreviousEffectsCondition([true, false], [1, 2])),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 8, Targeting.Slot_Front),
                    Effects.GenerateEffect(AnchoredUnlock),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.Slow,
            };
            immovableLeft.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Misc), nameof(IntentType_GameIDs.Swap_Left), nameof(IntentType_GameIDs.Swap_Left)]);
            immovableLeft.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Damage_7_10)]);

            Ability immovableRight = new Ability("<color=#" + PurpleID + ">IMMUTABLE</color>", "AApocrypha_TruthImmovableRight_A")
            {
                Description = "Disable the effects of Anchored on all units until this ability is completed." +
                "\nMove this enemy to the Right twice. This movement assumes the grid wraps around." +
                "\nDeal an Agonizing amount of damage to the Opposing party member.",
                Cost = [Pigments.Grey, Pigments.Purple],
                Effects =
                [
                    Effects.GenerateEffect(AnchoredLock),
                    Effects.GenerateEffect(SwapRight, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<CasterInEdgesCheckEffect>(), 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 1)),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<MirrorPositionEffect>(), 1, Targeting.Slot_SelfSlot, Effects.CheckMultiplePreviousEffectsCondition([true, false], [1, 2])),
                    Effects.GenerateEffect(SwapRight, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<CasterInEdgesCheckEffect>(), 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 1)),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<MirrorPositionEffect>(), 1, Targeting.Slot_SelfSlot, Effects.CheckMultiplePreviousEffectsCondition([true, false], [1, 2])),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 8, Targeting.Slot_Front),
                    Effects.GenerateEffect(AnchoredUnlock),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.Slow,
            };
            immovableRight.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Misc), nameof(IntentType_GameIDs.Swap_Right), nameof(IntentType_GameIDs.Swap_Right)]);
            immovableRight.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Damage_7_10)]);

            ExtraAbilityInfo rightExtra = new()
            {
                ability = immovableRight.GenerateEnemyAbility().ability,
                rarity = Rarity.Common,
            };

            SwapCasterAbilitiesEffect reverseThePolarity = ScriptableObject.CreateInstance<SwapCasterAbilitiesEffect>();
            reverseThePolarity._abilitiesToSwap = [rightExtra];

            Enemy immovable = new Enemy("<color=#" + PurpleID + ">IMMOVABLE OBJECT</color>", "Truth_Immovable_EN")
            {
                Health = 40,
                HealthColor = Pigments.Grey,
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("AnomalyTimeline", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("AnomalyDead", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("AnomalyDead", new Vector2(0.5f, 0f), 32),
                DamageSound = LoadedAssetsHandler.GetCharacter("Gospel_CH").damageSound,
                DeathSound = LoadedAssetsHandler.GetCharacter("Gospel_CH").deathSound,
                UnitTypes = ["Anomaly"],
                CombatEnterEffects = [Effects.GenerateEffect(reverseThePolarity, 1, Targeting.Slot_SelfSlot, Effects.ChanceCondition(50))],
            };
            immovable.PrepareEnemyPrefab("Assets/Apocrypha_Enemies/TRUTH/TruthImmovable_Enemy.prefab", AApocrypha.assetBundle, null);
            immovable.AddPassives([Passives.GetCustomPassive("GreyBlooded_1_PA"), Passives.Anchored]);

            immovable.AddEnemyAbilities([immovableLeft.GenerateEnemyAbility()]);
            immovable.AddEnemy(true, false, false);

            Enemy eye = new Enemy("<color=#" + PurpleID + ">THOUSAND EYES</color>", "Truth_Eye_EN")
            {
                Health = 25,
                HealthColor = Pigments.Purple,
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("AnomalyTimeline", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("AnomalyDead", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("AnomalyDead", new Vector2(0.5f, 0f), 32),
                DamageSound = LoadedAssetsHandler.GetCharacter("Gospel_CH").damageSound,
                DeathSound = LoadedAssetsHandler.GetCharacter("Gospel_CH").deathSound,
                UnitTypes = ["Anomaly"],
            };
            eye.PrepareEnemyPrefab("Assets/Apocrypha_Enemies/TRUTH/TruthEye_Enemy.prefab", AApocrypha.assetBundle, null);
            eye.AddPassives([Passives.GetCustomPassive("AA_Heterochromia_PA"), Passives.Absorb, Passives.Forgetful]);

            LeftOrRightToOpposeEnemyChanceForNextEffect NavigatorOpposing = ScriptableObject.CreateInstance<LeftOrRightToOpposeEnemyChanceForNextEffect>();
            NavigatorOpposing._inverted = false;

            StatusEffect_Apply_Effect FrailApply = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            FrailApply._Status = StatusField.Frail;

            Ability eyeCopy = new Ability("<color=#" + PurpleID + ">SEE FROM YOUR EYES</color>", "AApocrypha_TruthEyeCopy_A")
            {
                Description = "If there is no party member Opposing this enemy, move this enemy to the Left or Right, prioritizing opposed positions." +
                "\nChange this enemy's health color to match the Opposing party member's health color." +
                "\nApply 2 Frail to the Opposing party member and this enemy if this enemy's health color was changed.",
                Cost = [Pigments.Grey, Pigments.Purple],
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<CheckHasUnitEffect>(), 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(NavigatorOpposing, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 1)),
                    Effects.GenerateEffect(SwapRight, 1, Targeting.Slot_SelfSlot, Effects.CheckMultiplePreviousEffectsCondition([true, false], [1, 2])),
                    Effects.GenerateEffect(SwapLeft, 1, Targeting.Slot_SelfSlot, Effects.CheckMultiplePreviousEffectsCondition([false, false], [2, 3])),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<ChangeCasterHealthColorByTargetEffect>(), 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(FrailApply, 2, Targeting.Slot_Front, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(FrailApply, 2, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 2)),
                ],
                Rarity = Rarity.ExtremelyCommon,
                Priority = Priority.Fast,
            };
            eyeCopy.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Misc_Hidden)]);
            eyeCopy.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Swap_Sides), nameof(IntentType_GameIDs.Mana_Modify), nameof(IntentType_GameIDs.Status_Frail)]);
            eyeCopy.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Status_Frail)]);

            Ability eyeDie = new Ability("<color=#" + PurpleID + ">DRINK FROM YOUR HEART</color>", "AApocrypha_TruthEyeDie_A")
            {
                Description = "If the Opposing party member's health color matches this enemy's health color, deal a Painful amount of damage to the Opposing party member and generate 2 pigment of their health color.",
                Cost = [Pigments.Grey, Pigments.Purple],
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<TargetHasCasterHealthColorCheckEffect>(), 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 4, Targeting.Slot_Front, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<GenerateCasterHealthColorManaPerTargetEffect>(), 2, Targeting.Slot_Front, Effects.CheckPreviousEffectCondition(true, 2)),
                ],
                Rarity = Rarity.AbsurdlyRare,
                Priority = Priority.Fast,
            };
            eyeDie.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Misc_Hidden), nameof(IntentType_GameIDs.Damage_3_6), nameof(IntentType_GameIDs.Mana_Modify)]);

            eye.AddEnemyAbilities([
                eyeCopy,
                eyeDie,
            ]);
            eye.AddEnemy();

            Enemy pendulum = new Enemy("<color=#" + PurpleID + ">TIME DOES NOT STOP FOR THEE</color>", "Truth_Pendulum_EN")
            {
                Health = 50,
                HealthColor = Pigments.RedPurple,
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("AnomalyTimeline", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("AnomalyDead", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("AnomalyDead", new Vector2(0.5f, 0f), 32),
                DamageSound = LoadedAssetsHandler.GetCharacter("Gospel_CH").damageSound,
                DeathSound = LoadedAssetsHandler.GetCharacter("Gospel_CH").deathSound,
                UnitTypes = ["Anomaly"],
            };
            pendulum.PrepareEnemyPrefab("Assets/Apocrypha_Enemies/TRUTH/TruthPendulum_Enemy.prefab", AApocrypha.assetBundle, null);
            pendulum.AddPassives([Passives.Pure, Passives.MultiAttack2]);

            TargetSplitOrReplaceHealthEffect purplify = ScriptableObject.CreateInstance<TargetSplitOrReplaceHealthEffect>();
            purplify._color = Pigments.Purple;
            purplify._colorBlacklist = [Pigments.Grey];
            purplify._transformBlacklist = false;

            Ability pendulumLeft = new Ability("<color=#" + PurpleID + ">LEFT BEHIND</color>", "AApocrypha_TruthPendulumLeft_A")
            {
                Description = "Deal a Painful amount of damage to the Left and Far Left party members, then split purple into their health color if it isn't grey." +
                "\nMove this enemy to the Left.",
                Cost = [Pigments.Grey, Pigments.Purple],
                Visuals = ITAVisuals.PendulumL,
                AnimationTarget = Targeting.GenerateSlotTarget([-1, -2], false),
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 5, Targeting.GenerateSlotTarget([-1, -2], false)),
                    Effects.GenerateEffect(purplify, 1, Targeting.GenerateSlotTarget([-1, -2], false)),
                    Effects.GenerateEffect(SwapLeft, 1, Targeting.Slot_SelfSlot),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.Normal,
            };
            pendulumLeft.AddIntentsToTarget(Targeting.GenerateSlotTarget([-1, -2], false), [nameof(IntentType_GameIDs.Damage_3_6), nameof(IntentType_GameIDs.Mana_Modify)]);
            pendulumLeft.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Swap_Left)]);

            Ability pendulumRight = new Ability("<color=#" + PurpleID + ">YET TO COME</color>", "AApocrypha_TruthPendulumRight_A")
            {
                Description = "Deal a Painful amount of damage to the Right and Far Right party members, then split purple into their health color if it isn't grey." +
                "\nMove this enemy to the Right.",
                Cost = [Pigments.Grey, Pigments.Purple],
                Visuals = ITAVisuals.PendulumR,
                AnimationTarget = Targeting.GenerateSlotTarget([1, 2], false),
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 5, Targeting.GenerateSlotTarget([1, 2], false)),
                    Effects.GenerateEffect(purplify, 1, Targeting.GenerateSlotTarget([1, 2], false)),
                    Effects.GenerateEffect(SwapRight, 1, Targeting.Slot_SelfSlot),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.Normal,
            };
            pendulumRight.AddIntentsToTarget(Targeting.GenerateSlotTarget([1, 2], false), [nameof(IntentType_GameIDs.Damage_3_6), nameof(IntentType_GameIDs.Mana_Modify)]);
            pendulumRight.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Swap_Right)]);

            SpecificOpponentsByHealthColorTargeting ThePurpleOnes = ScriptableObject.CreateInstance<SpecificOpponentsByHealthColorTargeting>();
            ThePurpleOnes._color = Pigments.Purple;
            ThePurpleOnes._contains = true;
            ThePurpleOnes.targetUnitAllySlots = true;
            ThePurpleOnes.slotOffsets = [0];

            TargetExtractHealthColorEffect DePurple = ScriptableObject.CreateInstance<TargetExtractHealthColorEffect>();
            DePurple._color = Pigments.Purple;
            DePurple._fallbackColors = [Pigments.Red, Pigments.Yellow, Pigments.Blue];

            Ability pendulumFrail = new Ability("<color=#" + PurpleID + ">TIME MOVES ON</color>", "AApocrypha_TruthPendulumFrail_A")
            {
                Description = "Apply 2 Frail to all party members whose health contains purple, then try to remove purple from their health color, randomizing it if it would be empty.",
                Cost = [Pigments.Grey, Pigments.Purple],
                Visuals = ITAVisuals.PendulumFinisher,
                AnimationTarget = Targeting.Slot_SelfSlot,
                Effects =
                [
                    Effects.GenerateEffect(FrailApply, 2, ThePurpleOnes),
                    Effects.GenerateEffect(DePurple, 1, ThePurpleOnes),
                ],
                Rarity = Rarity.Rare,
                Priority = Priority.Fast,
            };
            pendulumFrail.AddIntentsToTarget(ThePurpleOnes, [nameof(IntentType_GameIDs.Status_Frail), nameof(IntentType_GameIDs.Mana_Modify)]);

            pendulum.AddEnemyAbilities([
                pendulumLeft,
                pendulumRight,
                pendulumFrail,
            ]);
            pendulum.AddEnemy(true, false, false);
        }
    }
}
