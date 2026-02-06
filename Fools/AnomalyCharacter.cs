using System;
using System.Collections.Generic;
using System.Text;
using A_Apocrypha.CustomOther;
using BrutalAPI;
using UnityEngine.SocialPlatforms.Impl;
using Utility.SerializableCollection;

namespace A_Apocrypha.Fools
{
    public class AnomalyCharacter
    {
        public static void Add()
        {
            ExtraGetRandomCCSprites_ArraySO anomalySpriteArray = ScriptableObject.CreateInstance<ExtraGetRandomCCSprites_ArraySO>();
            anomalySpriteArray._doesLoop = true;
            anomalySpriteArray._DefaultID = "ThresholdFoolSpritesDefault";
            anomalySpriteArray._SpecialID = "ThresholdFoolSpritesSpecial";
            anomalySpriteArray._frontSprite = [
                ResourceLoader.LoadSprite("ThresholdFoolFront1", new Vector2(0.5f, 0f), 32),
                ResourceLoader.LoadSprite("ThresholdFoolFront2", new Vector2(0.5f, 0f), 32),
                ResourceLoader.LoadSprite("ThresholdFoolFront3", new Vector2(0.5f, 0f), 32),
            ];
            anomalySpriteArray._backSprite = [
                ResourceLoader.LoadSprite("ThresholdFoolBack1", new Vector2(0.5f, 0f), 32),
                ResourceLoader.LoadSprite("ThresholdFoolBack2", new Vector2(0.5f, 0f), 32),
                ResourceLoader.LoadSprite("ThresholdFoolBack3", new Vector2(0.5f, 0f), 32),
            ];

            SetCasterExtraSpritesEffect Waver = ScriptableObject.CreateInstance<SetCasterExtraSpritesEffect>();
            Waver._ExtraSpriteID = "ThresholdFoolSpritesSpecial";

            PerformEffectPassiveAbility anomalyCosmeticPassive = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
            anomalyCosmeticPassive.name = "ThresholdFoolSpriteHandler_PA";
            anomalyCosmeticPassive._passiveName = "Anomalous";
            anomalyCosmeticPassive.m_PassiveID = "Anomalous";
            anomalyCosmeticPassive.passiveIcon = ResourceLoader.LoadSprite("ThresholdFoolIcon");
            anomalyCosmeticPassive._characterDescription = "This party member's physical form is everchanging.";
            anomalyCosmeticPassive._enemyDescription = "This enemy's physical form is everchanging.";
            anomalyCosmeticPassive._triggerOn = [
                TriggerCalls.OnAnyAbilityUsed,
                TriggerCalls.OnDamaged,
                TriggerCalls.OnMoved,
                TriggerCalls.OnTurnFinished,
                TriggerCalls.OnHealed,
                TriggerCalls.OnDidApplyDamage,
            ];
            anomalyCosmeticPassive.doesPassiveTriggerInformationPanel = false;
            anomalyCosmeticPassive.effects =
            [
                Effects.GenerateEffect(Waver),
            ];

            TargetSplitOrReplaceHealthEffect purplify = ScriptableObject.CreateInstance<TargetSplitOrReplaceHealthEffect>();
            purplify._color = Pigments.Purple;
            purplify._colorBlacklist = [Pigments.Grey];
            purplify._transformBlacklist = false;

            Ability alter = new Ability("Alter", "AnomalyAlter_A")
            {
                Description = "Split purple into the Opposing enemy's health color if it is not grey. If successful, refresh this party member's ability usage.\nDeal 1 damage to the Opposing enemy.",
                AbilitySprite = ResourceLoader.LoadSprite("IconThresholdFoolAlter"),
                Cost = [Pigments.YellowPurple],
                Visuals = CustomVisuals.GazeVisualsSO,
                AnimationTarget = Targeting.Slot_Front,
                Effects =
                [
                    Effects.GenerateEffect(purplify, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<RefreshAbilityUseEffect>(), 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 1, Targeting.Slot_Front),
                ]
            };
            alter.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Mana_Modify), nameof(IntentType_GameIDs.Damage_1_2)]);
            alter.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Misc_Additional)]);

            Character anomaly = new Character("███████", "ThresholdFool_CH")
            {
                HealthColor = Pigments.Purple,
                UsesBasicAbility = true,
                UsesAllAbilities = false,
                MovesOnOverworld = false,
                FrontSprite = ResourceLoader.LoadSprite("ThresholdFoolFront", new Vector2(0.5f, 0f), 32),
                BackSprite = ResourceLoader.LoadSprite("ThresholdFoolBack", new Vector2(0.5f, 0f), 32),
                OverworldSprite = ResourceLoader.LoadSprite("ThresholdFoolOverworld", new Vector2(0.5f, 0f), 32),
                DamageSound = "event:/AAEnemy/Threshold/ThresholdHurt",
                DeathSound = "event:/AAEnemy/Threshold/ThresholdDeath",
                DialogueSound = "event:/AAEnemy/Anomaly1Roar",
                ExtraSprites = anomalySpriteArray,
                BasicAbility = alter,
                UnitTypes = ["Anomaly", "Sandwich_Spirit"],
            };

            UnityEngine.Object AnomalyCharacterAnimator = AApocrypha.assetBundle.LoadAsset("Assets/Apocrypha_Fools/AnomalyFoolOverrideController.overrideController");
            anomaly.Animator = (RuntimeAnimatorController)(AnomalyCharacterAnimator is RuntimeAnimatorController ? AnomalyCharacterAnimator : null);
            anomaly.GenerateMenuCharacter(ResourceLoader.LoadSprite("ThresholdFoolMenu"), ResourceLoader.LoadSprite("ThresholdFoolLocked2"));
            anomaly.AddPassives([Passives.Pure, Passives.EssencePurple, anomalyCosmeticPassive]);
            anomaly.SetMenuCharacterAsFullDPS();

            DamageWithPigmentBonusEffect damagePurpleBonus4 = ScriptableObject.CreateInstance<DamageWithPigmentBonusEffect>();
            damagePurpleBonus4._color = Pigments.Purple;
            damagePurpleBonus4._contains = true;
            damagePurpleBonus4._cap = 4;

            DamageWithPigmentBonusEffect damagePurpleBonus6 = ScriptableObject.CreateInstance<DamageWithPigmentBonusEffect>();
            damagePurpleBonus6._color = Pigments.Purple;
            damagePurpleBonus6._contains = true;
            damagePurpleBonus6._cap = 6;

            DamageWithPigmentBonusEffect damagePurpleBonus8 = ScriptableObject.CreateInstance<DamageWithPigmentBonusEffect>();
            damagePurpleBonus8._color = Pigments.Purple;
            damagePurpleBonus8._contains = true;
            damagePurpleBonus8._cap = 8;

            DamageWithPigmentBonusEffect damagePurpleBonus10 = ScriptableObject.CreateInstance<DamageWithPigmentBonusEffect>();
            damagePurpleBonus10._color = Pigments.Purple;
            damagePurpleBonus10._contains = true;
            damagePurpleBonus10._cap = 10;

            PigmentThresholdCheckEffect PurpleCheck = ScriptableObject.CreateInstance<PigmentThresholdCheckEffect>();
            PurpleCheck._color = Pigments.Purple;
            PurpleCheck._contains = false;

            ConsumePigmentSharingCasterHealthColorEffect EatHealthLike = ScriptableObject.CreateInstance<ConsumePigmentSharingCasterHealthColorEffect>();
            EatHealthLike.eatme = Pigments.Purple;

            ConsumePigmentSharingCasterHealthColorEffect EatNumberHealthLike = ScriptableObject.CreateInstance<ConsumePigmentSharingCasterHealthColorEffect>();
            EatNumberHealthLike.eatme = Pigments.Purple;
            EatNumberHealthLike.consumeAll = false;

            DamageWithHealthColorBonusEffect DamagePurpleHealthBonus3 = ScriptableObject.CreateInstance<DamageWithHealthColorBonusEffect>();
            DamagePurpleHealthBonus3._usePreviousExitValue = true;
            DamagePurpleHealthBonus3._bonusAmount = 3;
            DamagePurpleHealthBonus3._color = Pigments.Purple;
            DamagePurpleHealthBonus3._contains = true;
            DamagePurpleHealthBonus3._pureBlocked = false;
            DamagePurpleHealthBonus3._entryAsBaseDamage = true;

            DamageWithHealthColorBonusEffect DamagePurpleHealthBonus5 = ScriptableObject.CreateInstance<DamageWithHealthColorBonusEffect>();
            DamagePurpleHealthBonus5._usePreviousExitValue = true;
            DamagePurpleHealthBonus5._bonusAmount = 5;
            DamagePurpleHealthBonus5._color = Pigments.Purple;
            DamagePurpleHealthBonus5._contains = true;
            DamagePurpleHealthBonus5._pureBlocked = false;
            DamagePurpleHealthBonus5._entryAsBaseDamage = true;

            DamageWithHealthColorBonusEffect DamagePurpleHealthBonus7 = ScriptableObject.CreateInstance<DamageWithHealthColorBonusEffect>();
            DamagePurpleHealthBonus7._usePreviousExitValue = true;
            DamagePurpleHealthBonus7._bonusAmount = 7;
            DamagePurpleHealthBonus7._color = Pigments.Purple;
            DamagePurpleHealthBonus7._contains = true;
            DamagePurpleHealthBonus7._pureBlocked = false;
            DamagePurpleHealthBonus7._entryAsBaseDamage = true;

            DamageWithHealthColorBonusEffect DamagePurpleHealthBonus9 = ScriptableObject.CreateInstance<DamageWithHealthColorBonusEffect>();
            DamagePurpleHealthBonus9._usePreviousExitValue = true;
            DamagePurpleHealthBonus9._bonusAmount = 9;
            DamagePurpleHealthBonus9._color = Pigments.Purple;
            DamagePurpleHealthBonus9._contains = true;
            DamagePurpleHealthBonus9._pureBlocked = false;
            DamagePurpleHealthBonus9._entryAsBaseDamage = true;

            TargetExtractHealthColorEffect DePurple = ScriptableObject.CreateInstance<TargetExtractHealthColorEffect>();
            DePurple._color = Pigments.Purple;
            DePurple._fallbackColors = [Pigments.Red, Pigments.Yellow, Pigments.Blue];

            AddPassiveEffect PurpleBlooder = ScriptableObject.CreateInstance<AddPassiveEffect>();
            PurpleBlooder._passiveToAdd = Passives.GetCustomPassive("PurpleBlooded_1_PA");

            StatusEffect_ApplyPermanentRandom_NegativeEffect RandomNegativeRestrictor = ScriptableObject.CreateInstance<StatusEffect_ApplyPermanentRandom_NegativeEffect>();
            RandomNegativeRestrictor.AllowNegative = true;
            RandomNegativeRestrictor.AllowPositive = false;
            RandomNegativeRestrictor.ForceNew = false;
            RandomNegativeRestrictor._substituteBlacklistNormal = true;
            RandomNegativeRestrictor._blacklist = [
                StatusField.Stunned._StatusID,
                "Dazed_ID",
                "Muted_ID",
                "Inspiration_ID",
            ];

            PassivePopUpOnTargetEffect PurpleBlooderPopup = ScriptableObject.CreateInstance<PassivePopUpOnTargetEffect>();
            PurpleBlooderPopup._isUnitCharacter = false;
            PurpleBlooderPopup._sprite = "IconStonebloodPurple";
            PurpleBlooderPopup._name = "Purple-Blooded (1)";

            TargetSplitOrReplaceHealthEffect purplify2 = ScriptableObject.CreateInstance<TargetSplitOrReplaceHealthEffect>();
            purplify2._color = Pigments.Purple;
            purplify2._colorBlacklist = [Pigments.Grey];
            purplify2._transformBlacklist = true;

            PassiveLockingEffect LockPure = ScriptableObject.CreateInstance<PassiveLockingEffect>();
            LockPure.m_PassiveIDs = [Passives.Pure.m_PassiveID];
            LockPure._lock = true;

            PassiveLockingEffect UnlockPure = ScriptableObject.CreateInstance<PassiveLockingEffect>();
            UnlockPure.m_PassiveIDs = [Passives.Pure.m_PassiveID];
            UnlockPure._lock = true;

            RemovePassiveEffect Impurify = ScriptableObject.CreateInstance<RemovePassiveEffect>();
            Impurify.m_PassiveID = Passives.Pure.m_PassiveID;

            Ability reduce1 = new Ability("Reduced to Fragments", "AnomalyReduce_1_A")
            {
                Description = "Deal 7 damage to the Opposing enemy increased by the amount of purple-containing pigment in the pigment bar, up to +4." +
                "\nConsume up to 3 purple-containing pigment in the pigment bar, prioritizing purple pigment.",
                AbilitySprite = ResourceLoader.LoadSprite("IconThresholdFoolReduce"),
                Cost = [Pigments.Purple, Pigments.Yellow],
                Visuals = Visuals.Melt,
                AnimationTarget = Targeting.Slot_Front,
                Effects =
                [
                    Effects.GenerateEffect(damagePurpleBonus4, 7, Targeting.Slot_Front),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<ConsumeCasterColorManaEffect>(), 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(EatNumberHealthLike, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 1)),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<ConsumeCasterColorManaEffect>(), 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(EatNumberHealthLike, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 1)),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<ConsumeCasterColorManaEffect>(), 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(EatNumberHealthLike, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 1)),
                ]
            };
            reduce1.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Misc_Hidden), nameof(IntentType_GameIDs.Damage_7_10)]);

            Ability reduce2 = new Ability("Reduced to Particles", "AnomalyReduce_2_A")
            {
                Description = "Deal 8 damage to the Opposing enemy increased by the amount of purple-containing pigment in the pigment bar, up to +6." +
                "\nConsume up to 3 purple-containing pigment in the pigment bar, prioritizing purple pigment.",
                AbilitySprite = ResourceLoader.LoadSprite("IconThresholdFoolReduce"),
                Cost = [Pigments.Purple, Pigments.Yellow],
                Visuals = Visuals.Melt,
                AnimationTarget = Targeting.Slot_Front,
                Effects =
                [
                    Effects.GenerateEffect(damagePurpleBonus6, 8, Targeting.Slot_Front),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<ConsumeCasterColorManaEffect>(), 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(EatNumberHealthLike, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 1)),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<ConsumeCasterColorManaEffect>(), 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(EatNumberHealthLike, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 1)),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<ConsumeCasterColorManaEffect>(), 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(EatNumberHealthLike, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 1)),
                ]
            };
            reduce2.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Misc_Hidden), nameof(IntentType_GameIDs.Damage_7_10)]);

            Ability reduce3 = new Ability("Reduced to Notions", "AnomalyReduce_3_A")
            {
                Description = "Deal 9 damage to the Opposing enemy increased by the amount of purple-containing pigment in the pigment bar, up to +8." +
                "\nConsume up to 3 purple-containing pigment in the pigment bar, prioritizing purple pigment.",
                AbilitySprite = ResourceLoader.LoadSprite("IconThresholdFoolReduce"),
                Cost = [Pigments.Purple, Pigments.YellowPurple],
                Visuals = Visuals.Melt,
                AnimationTarget = Targeting.Slot_Front,
                Effects =
                [
                    Effects.GenerateEffect(damagePurpleBonus8, 9, Targeting.Slot_Front),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<ConsumeCasterColorManaEffect>(), 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(EatNumberHealthLike, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 1)),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<ConsumeCasterColorManaEffect>(), 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(EatNumberHealthLike, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 1)),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<ConsumeCasterColorManaEffect>(), 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(EatNumberHealthLike, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 1)),
                ]
            };
            reduce3.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Misc_Hidden), nameof(IntentType_GameIDs.Damage_7_10)]);

            Ability reduce4 = new Ability("Reduced to ███████", "AnomalyReduce_4_A")
            {
                Description = "Deal 10 damage to the Opposing enemy increased by the amount of purple-containing pigment in the pigment bar, up to +10." +
                "\nConsume up to 3 purple-containing pigment in the pigment bar, prioritizing purple pigment.",
                AbilitySprite = ResourceLoader.LoadSprite("IconThresholdFoolReduce"),
                Cost = [Pigments.Purple, Pigments.YellowPurple],
                Visuals = Visuals.Melt,
                AnimationTarget = Targeting.Slot_Front,
                Effects =
                [
                    Effects.GenerateEffect(damagePurpleBonus10, 10, Targeting.Slot_Front),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<ConsumeCasterColorManaEffect>(), 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(EatNumberHealthLike, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 1)),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<ConsumeCasterColorManaEffect>(), 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(EatNumberHealthLike, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 1)),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<ConsumeCasterColorManaEffect>(), 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(EatNumberHealthLike, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 1)),
                ]
            };
            reduce4.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Misc_Hidden), nameof(IntentType_GameIDs.Damage_7_10)]);

            Ability knowledge1 = new Ability("Hint at Knowledge", "AnomalyKnowledge_1_A")
            {
                Description = "Consume all stored purple-containing pigment and deal an equivalent amount of damage to the Left and Right enemies. This deals 3 more damage to enemies with purple-containing health." +
                "\nRemove purple from the Left and Right enemies' health, then randomize their health color if it would be colorless.",
                AbilitySprite = ResourceLoader.LoadSprite("IconThresholdFoolKnowledge"),
                Cost = [Pigments.Purple, Pigments.Purple],
                Visuals = Visuals.Genesis,
                AnimationTarget = Targeting.Slot_OpponentSides,
                Effects =
                [
                    Effects.GenerateEffect(EatHealthLike, 10, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(DamagePurpleHealthBonus3, 0, Targeting.Slot_OpponentSides),
                    Effects.GenerateEffect(DePurple, 1, Targeting.Slot_OpponentSides),
                ]
            };
            knowledge1.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Mana_Consume)]);
            knowledge1.AddIntentsToTarget(Targeting.Slot_OpponentSides, [nameof(IntentType_GameIDs.Misc_Hidden), nameof(IntentType_GameIDs.Damage_3_6), nameof(IntentType_GameIDs.Mana_Modify)]);

            Ability knowledge2 = new Ability("Reveal Knowledge", "AnomalyKnowledge_2_A")
            {
                Description = "Consume all stored purple-containing pigment and deal an equivalent amount of damage to the Left and Right enemies. This deals 5 more damage to enemies with purple-containing health." +
                "\nRemove purple from the Left and Right enemies' health, then randomize their health color if it would be colorless.",
                AbilitySprite = ResourceLoader.LoadSprite("IconThresholdFoolKnowledge"),
                Cost = [Pigments.Purple, Pigments.Purple],
                Visuals = Visuals.Genesis,
                AnimationTarget = Targeting.Slot_OpponentSides,
                Effects =
                [
                    Effects.GenerateEffect(EatHealthLike, 10, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(DamagePurpleHealthBonus5, 0, Targeting.Slot_OpponentSides),
                    Effects.GenerateEffect(DePurple, 1, Targeting.Slot_OpponentSides),
                ]
            };
            knowledge2.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Mana_Consume)]);
            knowledge2.AddIntentsToTarget(Targeting.Slot_OpponentSides, [nameof(IntentType_GameIDs.Misc_Hidden), nameof(IntentType_GameIDs.Damage_3_6), nameof(IntentType_GameIDs.Mana_Modify)]);

            Ability knowledge3 = new Ability("Impart Knowledge", "AnomalyKnowledge_3_A")
            {
                Description = "Consume all stored purple-containing pigment and deal an equivalent amount of damage to the Left and Right enemies. This deals 7 more damage to enemies with purple-containing health." +
                "\nRemove purple from the Left and Right enemies' health, then randomize their health color if it would be colorless.",
                AbilitySprite = ResourceLoader.LoadSprite("IconThresholdFoolKnowledge"),
                Cost = [Pigments.Purple, Pigments.Purple],
                Visuals = Visuals.Genesis,
                AnimationTarget = Targeting.Slot_OpponentSides,
                Effects =
                [
                    Effects.GenerateEffect(EatHealthLike, 10, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(DamagePurpleHealthBonus7, 0, Targeting.Slot_OpponentSides),
                    Effects.GenerateEffect(DePurple, 1, Targeting.Slot_OpponentSides),
                ]
            };
            knowledge3.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Mana_Consume)]);
            knowledge3.AddIntentsToTarget(Targeting.Slot_OpponentSides, [nameof(IntentType_GameIDs.Misc_Hidden), nameof(IntentType_GameIDs.Damage_7_10), nameof(IntentType_GameIDs.Mana_Modify)]);

            Ability knowledge4 = new Ability("███████ Knowledge", "AnomalyKnowledge_4_A")
            {
                Description = "Consume all stored purple-containing pigment and deal an equivalent amount of damage to the Left and Right enemies. This deals 9 more damage to enemies with purple-containing health." +
                "\nRemove purple from the Left and Right enemies' health, then randomize their health color if it would be colorless.",
                AbilitySprite = ResourceLoader.LoadSprite("IconThresholdFoolKnowledge"),
                Cost = [Pigments.Purple, Pigments.Purple],
                Visuals = Visuals.Genesis,
                AnimationTarget = Targeting.Slot_OpponentSides,
                Effects =
                [
                    Effects.GenerateEffect(EatHealthLike, 10, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(DamagePurpleHealthBonus9, 0, Targeting.Slot_OpponentSides),
                    Effects.GenerateEffect(DePurple, 1, Targeting.Slot_OpponentSides),
                ]
            };
            knowledge4.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Mana_Consume)]);
            knowledge4.AddIntentsToTarget(Targeting.Slot_OpponentSides, [nameof(IntentType_GameIDs.Misc_Hidden), nameof(IntentType_GameIDs.Damage_7_10), nameof(IntentType_GameIDs.Mana_Modify)]); 
            
            Ability transmute1 = new Ability("Transmute their Flesh", "AnomalyTransmute_1_A")
            {
                Description = "Remove purple from the Opposing enemy's health, then randomize its health color if it would be colorless." +
                "\nIf this succeeds, permanently apply Purple-Blooded (1) and a random negative status effect to the Opposing enemy." +
                "\nOtherwise, split purple into the Opposing enemy's health color, turning it purple if it was grey, and refresh this party member's movement.",
                AbilitySprite = ResourceLoader.LoadSprite("IconThresholdFoolTransmute"),
                Cost = [Pigments.Purple, Pigments.Purple, Pigments.Blue],
                Visuals = Visuals.BodySnatcher,
                AnimationTarget = Targeting.Slot_Front,
                Effects =
                [
                    Effects.GenerateEffect(DePurple, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(PurpleBlooder, 1, Targeting.Slot_Front, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(PurpleBlooderPopup, 1, Targeting.Slot_Front, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(RandomNegativeRestrictor, 1, Targeting.Slot_Front, Effects.CheckPreviousEffectCondition(true, 3)),
                    Effects.GenerateEffect(purplify2, 1, Targeting.Slot_Front, Effects.CheckPreviousEffectCondition(false, 4)),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<RestoreSwapUseEffect>(), 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 5)),
                ]
            };
            transmute1.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Mana_Modify), "AA_AddPassive", nameof(IntentType_GameIDs.Misc_Hidden)]);
            transmute1.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Misc_Additional)]);

            Ability transmute2 = new Ability("Transmute their Bone", "AnomalyTransmute_2_A")
            {
                Description = "Remove purple from the Opposing enemy's health, then randomize its health color if it would be colorless." +
                "\nIf this succeeds, permanently apply Purple-Blooded (1) and a random negative status effect to the Opposing enemy." +
                "\nOtherwise, split purple into the Opposing enemy's health color, turning it purple if it was grey, and refresh this party member's movement.",
                AbilitySprite = ResourceLoader.LoadSprite("IconThresholdFoolTransmute"),
                Cost = [Pigments.Purple, Pigments.Purple, Pigments.BluePurple],
                Visuals = Visuals.BodySnatcher,
                AnimationTarget = Targeting.Slot_Front,
                Effects =
                [
                    Effects.GenerateEffect(DePurple, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(PurpleBlooder, 1, Targeting.Slot_Front, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(PurpleBlooderPopup, 1, Targeting.Slot_Front, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(RandomNegativeRestrictor, 1, Targeting.Slot_Front, Effects.CheckPreviousEffectCondition(true, 3)),
                    Effects.GenerateEffect(purplify2, 1, Targeting.Slot_Front, Effects.CheckPreviousEffectCondition(false, 4)),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<RestoreSwapUseEffect>(), 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 5)),
                ]
            };
            transmute2.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Mana_Modify), "AA_AddPassive", nameof(IntentType_GameIDs.Misc_Hidden)]);
            transmute2.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Misc_Additional)]);

            Ability transmute3 = new Ability("Transmute their Being", "AnomalyTransmute_3_A")
            {
                Description = "Remove purple from the Opposing enemy's health, then randomize its health color if it would be colorless." +
                "\nIf this succeeds, permanently apply Purple-Blooded (1) and two random negative status effects to the Opposing enemy." +
                "\nOtherwise, split purple into the Opposing enemy's health color, turning it purple if it was grey, and refresh this party member's movement.",
                AbilitySprite = ResourceLoader.LoadSprite("IconThresholdFoolTransmute"),
                Cost = [Pigments.Purple, Pigments.Purple, Pigments.BluePurple],
                Visuals = Visuals.BodySnatcher,
                AnimationTarget = Targeting.Slot_Front,
                Effects =
                [
                    Effects.GenerateEffect(DePurple, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(PurpleBlooder, 1, Targeting.Slot_Front, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(PurpleBlooderPopup, 1, Targeting.Slot_Front, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(RandomNegativeRestrictor, 1, Targeting.Slot_Front, Effects.CheckPreviousEffectCondition(true, 3)),
                    Effects.GenerateEffect(RandomNegativeRestrictor, 1, Targeting.Slot_Front, Effects.CheckPreviousEffectCondition(true, 4)),
                    Effects.GenerateEffect(purplify2, 1, Targeting.Slot_Front, Effects.CheckPreviousEffectCondition(false, 5)),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<RestoreSwapUseEffect>(), 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 6)),
                ]
            };
            transmute3.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Mana_Modify), "AA_AddPassive", nameof(IntentType_GameIDs.Misc_Hidden)]);
            transmute3.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Misc_Additional)]);

            Ability transmute4 = new Ability("Transmute their █████", "AnomalyTransmute_4_A")
            {
                Description = "Remove purple from the Opposing enemy's health, then randomize its health color if it would be colorless." +
                "\nIf this succeeds, permanently apply Purple-Blooded (1) and three random negative status effects to the Opposing enemy." +
                "\nOtherwise, split purple into the Opposing enemy's health color, turning it purple if it was grey, and refresh this party member's movement." +
                "\nRemove Pure from the Opposing enemy.",
                AbilitySprite = ResourceLoader.LoadSprite("IconThresholdFoolTransmute"),
                Cost = [Pigments.Purple, Pigments.Purple, Pigments.BluePurple],
                Visuals = Visuals.BodySnatcher,
                AnimationTarget = Targeting.Slot_Front,
                Effects =
                [
                    Effects.GenerateEffect(DePurple, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(PurpleBlooder, 1, Targeting.Slot_Front, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(PurpleBlooderPopup, 1, Targeting.Slot_Front, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(RandomNegativeRestrictor, 1, Targeting.Slot_Front, Effects.CheckPreviousEffectCondition(true, 3)),
                    Effects.GenerateEffect(RandomNegativeRestrictor, 1, Targeting.Slot_Front, Effects.CheckPreviousEffectCondition(true, 4)),
                    Effects.GenerateEffect(RandomNegativeRestrictor, 1, Targeting.Slot_Front, Effects.CheckPreviousEffectCondition(true, 5)),
                    Effects.GenerateEffect(purplify2, 1, Targeting.Slot_Front, Effects.CheckPreviousEffectCondition(false, 6)),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<RestoreSwapUseEffect>(), 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 7)),
                    Effects.GenerateEffect(Impurify, 1, Targeting.Slot_Front),
                ]
            };
            transmute4.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Mana_Modify), "AA_AddPassive", nameof(IntentType_GameIDs.Misc_Hidden)]);
            transmute4.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Misc_Additional)]);
            transmute4.AddIntentsToTarget(Targeting.Slot_Front, ["AA_RemPassive"]);

            anomaly.AddLevelData(10, [reduce1, knowledge1, transmute1]);
            anomaly.AddLevelData(14, [reduce2, knowledge2, transmute2]);
            anomaly.AddLevelData(18, [reduce3, knowledge3, transmute3]);
            anomaly.AddLevelData(22, [reduce4, knowledge4, transmute4]);

            anomaly.AddFinalBossAchievementData(BossType_GameIDs.OsmanSinnoks.ToString(), "AApocrypha_AnnaMolly_Witness_ACH");
            anomaly.AddFinalBossAchievementData(BossType_GameIDs.Heaven.ToString(), "AApocrypha_AnnaMolly_Divine_ACH");
            if (AApocrypha.CrossMod.EnemyPack) { anomaly.AddFinalBossAchievementData("DoulaBoss", "AApocrypha_AnnaMolly_Abstraction_ACH"); }
            if (AApocrypha.CrossMod.GlitchsFreaks) { anomaly.AddFinalBossAchievementData("March_BOSS", "AApocrypha_AnnaMolly_Inevitable_ACH"); }
            if (AApocrypha.CrossMod.IntoTheAbyss) { anomaly.AddFinalBossAchievementData("Nobody_BOSS", "AApocrypha_AnnaMolly_Forgotten_ACH"); }
            if (AApocrypha.CrossMod.SaltEnemies) { anomaly.AddFinalBossAchievementData("BlueSky_BOSS", "AApocrypha_AnnaMolly_Dreamer_ACH"); }

            ModUnlockInfo modUnlockInfo = new ModUnlockInfo();
            modUnlockInfo.m_ModSPrite = ResourceLoader.LoadSprite("AchievementFoolAnnaMolly", null, 32, null);
            modUnlockInfo.m_ModTitle = "The Incomprehensible";
            modUnlockInfo.m_ModDescription = "Unlocked ███████.";

            ModdedAchievements unlockAchievement = new ModdedAchievements("The Incomprehensible", "Unlock ███████.", ResourceLoader.LoadSprite("AchievementFoolAnnaMolly", null, 32, null), "AApocrypha_Fool_AnnaMolly_ACH");
            unlockAchievement.AddNewAchievementToInGameCategory(AchievementCategoryIDs.PartyMembersTitleLabel);

            UnlockableModData unlockableModData = new UnlockableModData("AnnaMolly_Unlock");
            unlockableModData.hasCharacterUnlock = true;
            unlockableModData.character = "ThresholdFool_CH";
            unlockableModData.hasQuestCompletion = true;
            unlockableModData.questID = "AnnaMollyUnlock";
            unlockableModData.hasModdedAchievementUnlock = true;
            unlockableModData.moddedAchievementID = "AApocrypha_Fool_AnnaMolly_ACH";
            LoadedDBsHandler.UnlockablesDB.TryAddIDUnlock(unlockableModData);

            UnlockContentByIDEffect RevealMolly = ScriptableObject.CreateInstance<UnlockContentByIDEffect>();
            RevealMolly._unlockID = unlockableModData.id;

            GainLootCustomCharacterEffect GrantMolly = ScriptableObject.CreateInstance<GainLootCustomCharacterEffect>();
            GrantMolly._rank = 2;
            GrantMolly._nameAddition = NameAdditionLocID.NameAdditionNone;
            GrantMolly._characterCopy = "ThresholdFool_CH";

            PerformEffectPassiveAbility anomalyUnlockPassive = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
            anomalyUnlockPassive.name = "ThresholdFoolUnlockHandler_PA";
            anomalyUnlockPassive._passiveName = "Anomalous";
            anomalyUnlockPassive.m_PassiveID = "Anomalous";
            anomalyUnlockPassive.passiveIcon = ResourceLoader.LoadSprite("ThresholdFoolIcon");
            anomalyUnlockPassive._characterDescription = "hey, at least if this one dies you get a replacement";
            anomalyUnlockPassive._enemyDescription = "When this entity is banished, an ally will be left behind.";
            anomalyUnlockPassive._triggerOn = [TriggerCalls.OnDeath];
            anomalyUnlockPassive.doesPassiveTriggerInformationPanel = false;
            anomalyUnlockPassive.effects =
            [
                Effects.GenerateEffect(RevealMolly),
                Effects.GenerateEffect(GrantMolly, 1),
            ];
            Passives.AddCustomPassiveToPool("ThresholdFoolUnlockHandler_PA", "Anomalous", anomalyUnlockPassive);

            anomaly.AddCharacter(false, true);
            anomaly.MenuCharacterTrackData = ScriptableObject.CreateInstance<AnnaMollyTrackData>();
        }
    }
}
