using System;
using System.Collections.Generic;
using System.Text;
using A_Apocrypha.CustomOther;
using UnityEngine;

namespace A_Apocrypha.Enemies
{
    public class TearDrinker
    {
        public static void Add()
        {
            Enemy teardrinker = new Enemy("Tear Drinker", "TearDrinker_EN")
            {
                Health = 12,
                HealthColor = Pigments.Red,
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("TearDrinkerTimeline", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("TearDrinkerDead", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("TearDrinkerTimeline", new Vector2(0.5f, 0f), 32),
                DamageSound = LoadedAssetsHandler.GetEnemy("Keko_EN").damageSound,
                DeathSound = LoadedAssetsHandler.GetEnemy("Keko_EN").deathSound,
            };
            teardrinker.PrepareEnemyPrefab("Assets/Apocrypha_Enemies/TearDrinker_Enemy/TearDrinker_Enemy.prefab", AApocrypha.assetBundle, AApocrypha.assetBundle.LoadAsset<GameObject>("Assets/Apocrypha_Enemies/TearDrinker_Enemy/TearDrinker_Giblets.prefab").GetComponent<ParticleSystem>());

            GenerateColorManaPerTargetEffect TargetsGiveBluePigment = ScriptableObject.CreateInstance<GenerateColorManaPerTargetEffect>();
            TargetsGiveBluePigment.mana = Pigments.Blue;

            ConsumeColorManaEffect ConsumeBluePigment = ScriptableObject.CreateInstance<ConsumeColorManaEffect>();
            ConsumeBluePigment.mana = Pigments.Blue;

            FieldEffect_Apply_Effect ShieldByPrevious = ScriptableObject.CreateInstance<FieldEffect_Apply_Effect>();
            ShieldByPrevious._Field = StatusField.Shield;
            ShieldByPrevious._UsePreviousExitValueAsMultiplier = true;

            AddPassiveEffect ApplySkittish = ScriptableObject.CreateInstance<AddPassiveEffect>();
            ApplySkittish._passiveToAdd = Passives.Skittish;

            AddPassiveEffect ApplyGouged = ScriptableObject.CreateInstance<AddPassiveEffect>();
            ApplyGouged._passiveToAdd = Passives.GetCustomPassive("Gouged_PA");

            RemovePassiveEffect RemoveSkittish = ScriptableObject.CreateInstance<RemovePassiveEffect>();
            RemoveSkittish.m_PassiveID = "Skittish";

            SpecificOpponentsByPassiveTargeting EyelessOpponents = ScriptableObject.CreateInstance<SpecificOpponentsByPassiveTargeting>();
            EyelessOpponents._passive = Passives.GetCustomPassive("Gouged_PA");
            EyelessOpponents.targetUnitAllySlots = false;
            EyelessOpponents.slotOffsets = [0];

            AnimationVisualsEffect EyelessCryAnim = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            EyelessCryAnim._visuals = Visuals.Weep;
            EyelessCryAnim._animationTarget = EyelessOpponents;

            AnimationVisualsEffect NibbleAnim = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            NibbleAnim._visuals = Visuals.Mandibles;
            NibbleAnim._animationTarget = Targeting.Slot_Front;

            AnimationVisualsIfUnitEffect GougeAnim = ScriptableObject.CreateInstance<AnimationVisualsIfUnitEffect>();
            GougeAnim._visuals = Visuals.InvadeTheVeins;
            GougeAnim._animationTarget = Targeting.Slot_Front;

            PreviousEffectCondition PreviousTrue = ScriptableObject.CreateInstance<PreviousEffectCondition>();
            PreviousTrue.wasSuccessful = true;

            PreviousEffectCondition Previous2True = ScriptableObject.CreateInstance<PreviousEffectCondition>();
            Previous2True.wasSuccessful = true;
            Previous2True.previousAmount = 2;

            PreviousEffectCondition PreviousFalse = ScriptableObject.CreateInstance<PreviousEffectCondition>();
            PreviousFalse.wasSuccessful = false;

            PreviousEffectCondition Previous2False = ScriptableObject.CreateInstance<PreviousEffectCondition>();
            Previous2False.wasSuccessful = false;
            Previous2False.previousAmount = 2;

            GougedMusicHandlerEffect MusicToggleReset = ScriptableObject.CreateInstance<GougedMusicHandlerEffect>();
            MusicToggleReset.ResetEffect = true;

            GougedMusicHandlerEffect MusicToggleAdd = ScriptableObject.CreateInstance<GougedMusicHandlerEffect>();
            MusicToggleAdd.Add = true;

            teardrinker.CombatEnterEffects = [Effects.GenerateEffect(MusicToggleReset)];

            SwapToSidesEffect SwapEither = ScriptableObject.CreateInstance<SwapToSidesEffect>();

            CheckPassiveAbilityEffect IsInanimate = ScriptableObject.CreateInstance<CheckPassiveAbilityEffect>();
            IsInanimate.m_PassiveID = Passives.Inanimate.m_PassiveID;

            ExtraVariableForNextEffect Blank = ScriptableObject.CreateInstance<ExtraVariableForNextEffect>();

            TargetPerformEffectViaSubaction GougedNameHandler = ScriptableObject.CreateInstance<TargetPerformEffectViaSubaction>();
            GougedNameHandler.effects = [
                Effects.GenerateEffect(ScriptableObject.CreateInstance<CasterGougedNameEffect>()),
            ];

            Ability nibble = new Ability("Nibble", "AApocrypha_TearDrinkerNibble_A")
            {
                Description = "If no party members are Opposing this enemy, move Left or Right.\nDeal a Little damage to the Opposing party member.\nIf damage is dealt, All Gouged party members produce 1 Blue Pigment as they relive the experience.",
                Cost = [Pigments.Yellow, Pigments.RedBlue],
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<CheckHasUnitEffect>(), 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(SwapEither, 1, Targeting.Slot_SelfSlot, PreviousFalse),
                    Effects.GenerateEffect(NibbleAnim, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 2, Targeting.Slot_Front),
                    Effects.GenerateEffect(EyelessCryAnim, 1, EyelessOpponents, PreviousTrue),
                    Effects.GenerateEffect(TargetsGiveBluePigment, 1, EyelessOpponents, Previous2True),
                ],
                Rarity = Rarity.Uncommon,
                Priority = Priority.Normal,
            };
            nibble.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Misc_Hidden)]);
            nibble.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Swap_Sides)]);
            nibble.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Damage_1_2)]);
            nibble.AddIntentsToTarget(EyelessOpponents, [nameof(IntentType_GameIDs.Mana_Generate)]);

            Ability drinktears = new Ability("Drink Tears", "AApocrypha_DrinkTears_A")
            {
                Description = "If no party members are Opposing this enemy, move Left or Right.\nConsume up to 3 Blue Pigment. Apply Shields to this enemy's position equal to twice the amount consumed.",
                Cost = [Pigments.PurpleRed, Pigments.RedPurple],
                Visuals = Visuals.Leech,
                AnimationTarget = Targeting.Slot_SelfSlot,
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<CheckHasUnitEffect>(), 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(SwapEither, 1, Targeting.Slot_SelfSlot, PreviousFalse),
                    Effects.GenerateEffect(ConsumeBluePigment, 3, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(ShieldByPrevious, 2, Targeting.Slot_SelfSlot),
                ],
                Rarity = Rarity.Uncommon,
                Priority = Priority.Normal,
            };
            drinktears.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Misc_Hidden)]);
            drinktears.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Swap_Sides)]);
            drinktears.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Mana_Consume)]);
            drinktears.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Field_Shield)]);

            Ability gouge = new Ability("Gouge", "AApocrypha_Gouge_A")
            {
                Description = "Attempt to gouge out one of the Opposing party member's eyes. If successful, deal an Agonizing amount of damage to them and produce 3 Blue Pigment.\nIf the target is already Gouged, instead deal a Painful amount of damage and produce 1 Blue Pigment.",
                Cost = [Pigments.PurpleRed, Pigments.RedPurple],
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<CheckHasUnitEffect>(), 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(NibbleAnim, 1, Targeting.Slot_Front, PreviousGenerator(false, 1)),
                    Effects.GenerateEffect(IsInanimate, 1, Targeting.Slot_Front, PreviousGenerator(true, 2)),
                    Effects.GenerateEffect(NibbleAnim, 1, Targeting.Slot_Front, PreviousGenerator(true, 1)),
                    Effects.GenerateEffect(GougeAnim, 1, Targeting.Slot_Front, PreviousGenerator(false, 2)),
                    Effects.GenerateEffect(ApplyGouged, 1, Targeting.Slot_Front, PreviousGenerator(false, 3)),
                    Effects.GenerateEffect(MusicToggleAdd, 1, Targeting.Slot_SelfSlot, PreviousGenerator(true, 1)),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 7, Targeting.Slot_Front, PreviousGenerator(true, 2)),
                    Effects.GenerateEffect(TargetsGiveBluePigment, 3, Targeting.Slot_Front, PreviousGenerator(true, 3)),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 4, Targeting.Slot_Front, PreviousGenerator(false, 4)),
                    Effects.GenerateEffect(TargetsGiveBluePigment, 1, Targeting.Slot_Front, PreviousGenerator(false, 5)),
                ],
                Rarity = Rarity.Uncommon,
                Priority = Priority.Normal,
            };
            gouge.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Misc)]);
            gouge.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Damage_7_10)]);
            gouge.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Damage_3_6)]);
            gouge.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Mana_Generate)]);

            teardrinker.AddPassives([Passives.Slippery]);
            teardrinker.AddEnemyAbilities(
            [
                nibble,
                drinktears,
                gouge,
            ]);
            teardrinker.AddEnemy(true, true, true);
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
