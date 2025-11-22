using System;
using System.Collections.Generic;
using System.Text;
using A_Apocrypha.CustomStatusField;

namespace A_Apocrypha.Enemies
{
    public class CustomColophons
    {
        public static void Add()
        {
            PreviousEffectCondition PreviousTrue = ScriptableObject.CreateInstance<PreviousEffectCondition>();
            PreviousTrue.wasSuccessful = true;

            PreviousEffectCondition PreviousFalse = ScriptableObject.CreateInstance<PreviousEffectCondition>();
            PreviousFalse.wasSuccessful = false;

            AnimationVisualsEffect MigraineAnim = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            MigraineAnim._visuals = Visuals.InvadeTheVeins;
            MigraineAnim._animationTarget = Targeting.Slot_Front;

            SwapToOneSideEffect SwapLeft = ScriptableObject.CreateInstance<SwapToOneSideEffect>();
            SwapLeft._swapRight = false;

            SwapToOneSideEffect SwapRight = ScriptableObject.CreateInstance<SwapToOneSideEffect>();
            SwapRight._swapRight = true;

            Ability bias = new Ability("Bias", "AApocrypha_ColoBias_A")
            {
                Description = "Transform 4 Pigment not of this Enemy's health colour, into this Enemy's health colour.",
                Cost = [],
                Visuals = Visuals.Weep,
                AnimationTarget = Targeting.Slot_SelfSlot,
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<RandomizeNumberPigmentCasterHealthColorEffect>(), 4, Targeting.Slot_SelfSlot),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.Normal,
            };
            bias.AddIntentsToTarget(Targeting.Slot_SelfSlot, ["AA_Pigment_Transform"]);

            Ability migraine = new Ability("Migraine", "AApocrypha_ColoMigraine_A")
            {
                Description = "Moves this enemy to the Left or Right.\nDeals a Painful amount of damage to the Opposing party member.\nTransform 2 Pigment not of this Enemy's health colour, into this Enemy's health colour.",
                Cost = [],
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<SwapToSidesEffect>(), 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(MigraineAnim, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 5, Targeting.Slot_Front),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<RandomizeNumberPigmentCasterHealthColorEffect>(), 2, Targeting.Slot_SelfSlot),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.Normal,
            };
            migraine.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Swap_Sides)]);
            migraine.AddIntentsToTarget(Targeting.Slot_SelfSlot, ["AA_Pigment_Transform"]);
            migraine.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Damage_3_6)]);

            Ability objectivity = new Ability("Objectivity", "AApocrypha_ColoObjectivity_A")
            {
                Description = "Transform 2 Pigment not of this Enemy's health colour, into this Enemy's health colour.",
                Cost = [],
                Visuals = Visuals.Weep,
                AnimationTarget = Targeting.Slot_SelfSlot,
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<RandomizeNumberPigmentCasterHealthColorEffect>(), 2, Targeting.Slot_SelfSlot),
                ],
                Rarity = Rarity.VeryRare,
                Priority = Priority.Normal,
            };
            objectivity.AddIntentsToTarget(Targeting.Slot_SelfSlot, ["AA_Pigment_Transform"]);

            Ability tensionheadache = new Ability("Tension Headache", "AApocrypha_ColoHeadache_A")
            {
                Description = "Moves this enemy to the Left or Right.\nDeals an Agonizing amount of damage to the Opposing party member.\nTransform 1 Pigment not of this Enemy's health colour, into this Enemy's health colour.",
                Cost = [],
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<SwapToSidesEffect>(), 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(MigraineAnim, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 9, Targeting.Slot_Front),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<RandomizeNumberPigmentCasterHealthColorEffect>(), 1, Targeting.Slot_SelfSlot),
                ],
                Rarity = Rarity.Uncommon,
                Priority = Priority.Normal,
            };
            tensionheadache.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Swap_Sides)]);
            tensionheadache.AddIntentsToTarget(Targeting.Slot_SelfSlot, ["AA_Pigment_Transform"]);
            tensionheadache.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Damage_7_10)]);

            Enemy redblueColo = new Enemy("Dualistic Colophon", "ColophonDualistic_EN")
            {
                Health = 12,
                HealthColor = Pigments.RedBlue,
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("ColophonDualisticTimeline", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("ColophonDualisticDead", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("ColophonDualisticTimeline", new Vector2(0.5f, 0f), 32),
                DamageSound = LoadedAssetsHandler.GetEnemy("ColophonComposed_EN").damageSound,
                DeathSound = LoadedAssetsHandler.GetEnemy("ColophonDefeated_EN").deathSound,
                //DamageSound = "event:/AAEnemy/ColophonSaccharineHurt",
                //DeathSound = "event:/AAEnemy/ColophonSaccharineDeath",
            };
            redblueColo.PrepareEnemyPrefab("Assets/Apocrypha_Enemies/ColophonDualistic_Enemy/ColophonDualistic_Enemy.prefab", AApocrypha.assetBundle, AApocrypha.assetBundle.LoadAsset<GameObject>("Assets/Apocrypha_Enemies/ColophonDualistic_Enemy/ColophonDualistic_Giblets.prefab").GetComponent<ParticleSystem>());
            redblueColo.AddPassives([Passives.Pure, Passives.GetCustomPassive("Pollute_PA")]);

            StatusEffect_Apply_Effect RupturedApply = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            RupturedApply._Status = StatusField.Ruptured;

            StatusEffect_Apply_Effect FrailApply = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            FrailApply._Status = StatusField.Frail;

            ChangeToRandomHealthColorEffect YoureBlueNow = ScriptableObject.CreateInstance<ChangeToRandomHealthColorEffect>();
            YoureBlueNow._healthColors = [Pigments.Blue];

            ChangeToRandomHealthColorEffect YoureRedNow = ScriptableObject.CreateInstance<ChangeToRandomHealthColorEffect>();
            YoureRedNow._healthColors = [Pigments.Red];

            AnimationVisualsEffect TranquilityAnim = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            TranquilityAnim._visuals = Visuals.Equal;
            TranquilityAnim._animationTarget = Targeting.Slot_OpponentAllLefts;

            AnimationVisualsEffect SufferingAnim = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            SufferingAnim._visuals = Visuals.Misery;
            SufferingAnim._animationTarget = Targeting.Slot_OpponentAllRights;

            Ability tranquility = new Ability("Tranquility", "AApocrypha_Tranquility_A")
            {
                Description = "Move this enemy to the Right.\nChange the health colour of All party members to the Left of this enemy to blue.\nApply 1 Ruptured to All party members whose health colour did not change.",
                Cost = [Pigments.BlueRed, Pigments.Blue],
                Effects =
                [
                    Effects.GenerateEffect(SwapRight, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(TranquilityAnim, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(YoureBlueNow, 1, Targeting.Slot_OpponentLeft),
                    Effects.GenerateEffect(RupturedApply, 1, Targeting.Slot_OpponentLeft, PreviousGenerator(false, 1)),
                    Effects.GenerateEffect(YoureBlueNow, 1, Targeting.GenerateSlotTarget([-2], false)),
                    Effects.GenerateEffect(RupturedApply, 1, Targeting.GenerateSlotTarget([-2], false), PreviousGenerator(false, 1)),
                    Effects.GenerateEffect(YoureBlueNow, 1, Targeting.GenerateSlotTarget([-3], false)),
                    Effects.GenerateEffect(RupturedApply, 1, Targeting.GenerateSlotTarget([-3], false), PreviousGenerator(false, 1)),
                    Effects.GenerateEffect(YoureBlueNow, 1, Targeting.GenerateSlotTarget([-4], false)),
                    Effects.GenerateEffect(RupturedApply, 1, Targeting.GenerateSlotTarget([-4], false), PreviousGenerator(false, 1)),
                ],
                Rarity = Rarity.Uncommon,
                Priority = Priority.Normal,
            };
            tranquility.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Swap_Right)]);
            tranquility.AddIntentsToTarget(Targeting.Slot_OpponentAllLefts, [nameof(IntentType_GameIDs.Mana_Modify)]);
            tranquility.AddIntentsToTarget(Targeting.Slot_OpponentAllLefts, [nameof(IntentType_GameIDs.Status_Ruptured)]);

            Ability suffering = new Ability("Suffering", "AApocrypha_Suffering_A")
            {
                Description = "Move this enemy to the Left.\nChange the health colour of All party members to the Right of this enemy to red.\nApply 2 Frail to All party members whose health colour did not change.",
                Cost = [Pigments.RedBlue, Pigments.Red],
                Effects =
                [
                    Effects.GenerateEffect(SwapLeft, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(SufferingAnim, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(YoureRedNow, 1, Targeting.Slot_OpponentRight),
                    Effects.GenerateEffect(FrailApply, 1, Targeting.Slot_OpponentRight, PreviousGenerator(false, 1)),
                    Effects.GenerateEffect(YoureRedNow, 1, Targeting.GenerateSlotTarget([2], false)),
                    Effects.GenerateEffect(FrailApply, 1, Targeting.GenerateSlotTarget([2], false), PreviousGenerator(false, 1)),
                    Effects.GenerateEffect(YoureRedNow, 1, Targeting.GenerateSlotTarget([3], false)),
                    Effects.GenerateEffect(FrailApply, 1, Targeting.GenerateSlotTarget([3], false), PreviousGenerator(false, 1)),
                    Effects.GenerateEffect(YoureRedNow, 1, Targeting.GenerateSlotTarget([4], false)),
                    Effects.GenerateEffect(FrailApply, 1, Targeting.GenerateSlotTarget([4], false), PreviousGenerator(false, 1)),
                ],
                Rarity = Rarity.Uncommon,
                Priority = Priority.Normal,
            };
            suffering.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Swap_Left)]);
            suffering.AddIntentsToTarget(Targeting.Slot_OpponentAllRights, [nameof(IntentType_GameIDs.Mana_Modify)]);
            suffering.AddIntentsToTarget(Targeting.Slot_OpponentAllRights, [nameof(IntentType_GameIDs.Status_Frail)]);

            redblueColo.AddEnemyAbilities(
            [
                bias,
                migraine,
                tranquility,
                suffering,
            ]);
            redblueColo.AddEnemy(true, false, false);

            if (AApocrypha.CrossMod.pigmentPeppermint)
            {
                Enemy peppermintColo = new Enemy("Saccharine Colophon", "ColophonSaccharine_EN")
                {
                    Health = 35,
                    HealthColor = LoadedDBsHandler.PigmentDB.GetPigment("Peppermint"),
                    Size = 1,
                    CombatSprite = ResourceLoader.LoadSprite("ColophonPeppermintTimeline", new Vector2(0.5f, 0f), 32),
                    OverworldDeadSprite = ResourceLoader.LoadSprite("ColophonPeppermintDead", new Vector2(0.5f, 0f), 32),
                    OverworldAliveSprite = ResourceLoader.LoadSprite("ColophonPeppermintTimeline", new Vector2(0.5f, 0f), 32),
                    DamageSound = "event:/AAEnemy/ColophonSaccharineHurt",
                    DeathSound = "event:/AAEnemy/ColophonSaccharineDeath",
                };
                peppermintColo.PrepareEnemyPrefab("Assets/Apocrypha_Enemies/ColophonPeppermint_Enemy/ColophonPeppermint_Enemy.prefab", AApocrypha.assetBundle, AApocrypha.assetBundle.LoadAsset<GameObject>("Assets/Apocrypha_Enemies/ColophonPeppermint_Enemy/ColophonPeppermint_Giblets.prefab").GetComponent<ParticleSystem>());
                peppermintColo.AddPassives([Passives.Pure, Passives.GetCustomPassive("Pollute_PA")]);

                StatusEffect_Apply_Effect HasteApply = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
                HasteApply._Status = StatusField.GetCustomStatusEffect("Haste_ID");

                QueueTimelineAbilityByNameEffect QueueRush = ScriptableObject.CreateInstance<QueueTimelineAbilityByNameEffect>();
                QueueRush._abilityName = "Sugar Rush";

                Ability sugarrush = new Ability("Sugar Rush", "AApocrypha_SugarRush_A")
                {
                    // technically the wording on this is inaccurate but I can't for the life of me find a better way to word it
                    Description = "Move this enemy to the Left or Right, prioritizing occupied spaces.\nIf this enemy swapped positions with (or was blocked by) another enemy, apply 1 Haste to that enemy, else 50% chance to queue this ability into the timeline again.",
                    Cost = [],
                    Visuals = Visuals.Bosch,
                    AnimationTarget = Targeting.Slot_SelfSlot,
                    Effects =
                    [
                        Effects.GenerateEffect(ScriptableObject.CreateInstance<LeftOrRightToEnemyChanceForNextEffect>(), 1, Targeting.Slot_SelfSlot),
                        Effects.GenerateEffect(ScriptableObject.CreateInstance<CheckHasUnitEffect>(), 1, Targeting.Slot_AllyLeft, PreviousGenerator(false, 1)),
                        Effects.GenerateEffect(HasteApply, 1, Targeting.Slot_AllyLeft, PreviousGenerator(true, 1)),
                        Effects.GenerateEffect(SwapLeft, 1, Targeting.Slot_SelfSlot, PreviousGenerator(false, 3)),
                        Effects.GenerateEffect(ScriptableObject.CreateInstance<CheckHasUnitEffect>(), 1, Targeting.Slot_AllyRight, PreviousGenerator(true, 4)),
                        Effects.GenerateEffect(HasteApply, 1, Targeting.Slot_AllyRight, PreviousGenerator(true, 1)),
                        Effects.GenerateEffect(SwapRight, 1, Targeting.Slot_SelfSlot, PreviousGenerator(true, 6)),
                        Effects.GenerateEffect(ScriptableObject.CreateInstance<PercentageChanceForNextEffect>(), 50, Targeting.Slot_SelfSlot, Effects.CheckMultiplePreviousEffectsCondition([false, false], [7, 5])),
                        Effects.GenerateEffect(ScriptableObject.CreateInstance<CheckHasUnitEffect>(), 1, Targeting.Unit_OtherAlliesSlots),
                        Effects.GenerateEffect(QueueRush, 1, Targeting.Slot_SelfSlot, Effects.CheckMultiplePreviousEffectsCondition([false, true], [1, 2])),
                        Effects.GenerateEffect(ScriptableObject.CreateInstance<PercentageChanceForNextEffect>(), 50, Targeting.Slot_SelfSlot, Effects.CheckMultiplePreviousEffectsCondition([true, false], [9, 4])),
                        Effects.GenerateEffect(ScriptableObject.CreateInstance<CheckHasUnitEffect>(), 1, Targeting.Unit_OtherAlliesSlots),
                        Effects.GenerateEffect(QueueRush, 1, Targeting.Slot_SelfSlot, Effects.CheckMultiplePreviousEffectsCondition([false, true], [1, 2])),
                    ],
                    Rarity = Rarity.Common,
                    Priority = Priority.Normal,
                };
                sugarrush.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Swap_Sides)]);
                sugarrush.AddIntentsToTarget(Targeting.Slot_AllySides, ["Status_Haste"]);
                sugarrush.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Misc_Additional)]);

                peppermintColo.AddEnemyAbilities(
                [
                    objectivity,
                    tensionheadache,
                    sugarrush,
                ]);
                peppermintColo.AddEnemy(true, true, true);

                Enemy peppermintColoAlt = new Enemy("Saccharine Colophon", "ColophonSaccharineAlt_EN")
                {
                    Health = 35,
                    HealthColor = LoadedDBsHandler.PigmentDB.GetPigment("Peppermint"),
                    Size = 1,
                    CombatSprite = ResourceLoader.LoadSprite("ColophonPeppermintAltTimeline", new Vector2(0.5f, 0f), 32),
                    OverworldDeadSprite = ResourceLoader.LoadSprite("ColophonPeppermintAltDead", new Vector2(0.5f, 0f), 32),
                    OverworldAliveSprite = ResourceLoader.LoadSprite("ColophonPeppermintAltTimeline", new Vector2(0.5f, 0f), 32),
                    DamageSound = "event:/AAEnemy/ColophonSaccharineHurt",
                    DeathSound = "event:/AAEnemy/ColophonSaccharineDeath",
                };
                peppermintColoAlt.PrepareEnemyPrefab("Assets/Apocrypha_Enemies/ColophonPeppermint_Enemy/ColophonPeppermintAlt_Enemy.prefab", AApocrypha.assetBundle, AApocrypha.assetBundle.LoadAsset<GameObject>("Assets/Apocrypha_Enemies/ColophonPeppermint_Enemy/ColophonPeppermintAlt_Giblets.prefab").GetComponent<ParticleSystem>());
                peppermintColoAlt.AddPassives([Passives.Pure, Passives.GetCustomPassive("Pollute_PA")]);

                peppermintColoAlt.AddEnemyAbilities(
                [
                    objectivity,
                    tensionheadache,
                    sugarrush,
                ]);
                peppermintColoAlt.AddEnemy(false, false, false);
            }
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
