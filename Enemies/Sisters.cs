using System;
using System.Collections.Generic;
using System.Text;
using A_Apocrypha.CustomOther;
using MythosFriends.Effectsa;

namespace A_Apocrypha.Enemies
{
    public class Sisters
    {
        public static void Add()
        {
            ChangeCasterHealthColorBetweenColorsEffect GreyRed = ScriptableObject.CreateInstance<ChangeCasterHealthColorBetweenColorsEffect>();
            GreyRed._color1 = Pigments.Red;
            GreyRed._color2 = Pigments.Grey
                ;
            ChangeCasterHealthColorBetweenColorsEffect RedGrey = ScriptableObject.CreateInstance<ChangeCasterHealthColorBetweenColorsEffect>();
            RedGrey._color1 = Pigments.Grey;
            RedGrey._color2 = Pigments.Red;

            PerformEffectPassiveAbility TwoFacedXR = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
            TwoFacedXR.name = "AA_TwoFaced_XR";
            TwoFacedXR._passiveName = "Two Faced";
            TwoFacedXR.m_PassiveID = Passives.TwoFaced.m_PassiveID;
            TwoFacedXR.passiveIcon = ResourceLoader.LoadSprite("2facedXR_passive");
            TwoFacedXR._characterDescription = "Upon receiving direct damage this party member will change its health colour from grey to red or vice versa.";
            TwoFacedXR._enemyDescription = "Upon receiving direct damage this enemy will change its health colour from grey to red or vice versa.";
            TwoFacedXR._triggerOn = [TriggerCalls.OnDirectDamaged];
            TwoFacedXR.effects = [Effects.GenerateEffect(GreyRed, 1, Targeting.Slot_SelfSlot)];

            PerformEffectPassiveAbility TwoFacedRX = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
            TwoFacedRX.name = "AA_TwoFaced_RX";
            TwoFacedRX._passiveName = "Two Faced";
            TwoFacedRX.m_PassiveID = Passives.TwoFaced.m_PassiveID;
            TwoFacedRX.passiveIcon = ResourceLoader.LoadSprite("2facedRX_passive");
            TwoFacedRX._characterDescription = "Upon receiving direct damage this party member will change its health colour from red to grey or vice versa.";
            TwoFacedRX._enemyDescription = "Upon receiving direct damage this enemy will change its health colour from red to grey or vice versa.";
            TwoFacedRX._triggerOn = [TriggerCalls.OnDirectDamaged];
            TwoFacedRX.effects = [Effects.GenerateEffect(RedGrey, 1, Targeting.Slot_SelfSlot)];

            Enemy fullsister = new Enemy("Someone's Sister", "SomeoneSister_EN")
            {
                Health = 30,
                HealthColor = Pigments.Grey,
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("SomeoneSisterTimeline", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("SistersDead", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("SomeoneSisterOverworld", new Vector2(0.5f, 0f), 32),
                DamageSound = "event:/AAEnemy/SistersHurt",
                DeathSound = "event:/AAEnemy/SistersDeath",
            };
            fullsister.PrepareEnemyPrefab("Assets/Apocrypha_Enemies/Sisters_Enemy/SomeoneSister_Enemy.prefab", AApocrypha.assetBundle, AApocrypha.assetBundle.LoadAsset<GameObject>("Assets/Apocrypha_Enemies/Sisters_Enemy/Sisters_Giblets.prefab").GetComponent<ParticleSystem>());

            Enemy emptysister = new Enemy("No One's Sister", "NooneSister_EN")
            {
                Health = 30,
                HealthColor = Pigments.Red,
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("NooneSisterTimeline", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("SistersDead", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("NooneSisterOverworld", new Vector2(0.5f, 0f), 32),
                DamageSound = "event:/AAEnemy/SistersHurt",
                DeathSound = "event:/AAEnemy/SistersDeath",
            };
            emptysister.PrepareEnemyPrefab("Assets/Apocrypha_Enemies/Sisters_Enemy/NooneSister_Enemy.prefab", AApocrypha.assetBundle, AApocrypha.assetBundle.LoadAsset<GameObject>("Assets/Apocrypha_Enemies/Sisters_Enemy/Sisters_Giblets.prefab").GetComponent<ParticleSystem>());

            CasterTransformationEffect BecomeEmpty = ScriptableObject.CreateInstance<CasterTransformationEffect>();
            BecomeEmpty._maintainMaxHealth = true;
            BecomeEmpty._currentToMaxHealth = false;
            BecomeEmpty._fullyHeal = false;
            BecomeEmpty._enemyTransformation = emptysister.enemy;
            BecomeEmpty._characterTransformation = "Nowak_CH";

            CasterTransformationEffect BecomeFull = ScriptableObject.CreateInstance<CasterTransformationEffect>();
            BecomeFull._maintainMaxHealth = true;
            BecomeFull._currentToMaxHealth = false;
            BecomeFull._fullyHeal = false;
            BecomeFull._enemyTransformation = fullsister.enemy;
            BecomeFull._characterTransformation = "Nowak_CH";

            PreviousEffectCondition PreviousTrue = ScriptableObject.CreateInstance<PreviousEffectCondition>();
            PreviousTrue.wasSuccessful = true;

            SpecificHealthColorEffectorCondition IsRed = ScriptableObject.CreateInstance<SpecificHealthColorEffectorCondition>();
            IsRed._color = Pigments.Red;

            SpecificHealthColorEffectorCondition IsGrey = ScriptableObject.CreateInstance<SpecificHealthColorEffectorCondition>();
            IsGrey._color = Pigments.Grey;

            PerformEffectPassiveAbility MercurialSisterFull = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
            MercurialSisterFull.name = "AA_Mercurial_SistersSomeone";
            MercurialSisterFull._passiveName = "Mercurial";
            MercurialSisterFull.m_PassiveID = "Mercurial";
            MercurialSisterFull.passiveIcon = ResourceLoader.LoadSprite("IconTransformPassive");
            MercurialSisterFull._characterDescription = "if your health is red on tineline end this shit turns you into Nowak lmao";
            MercurialSisterFull._enemyDescription = "At the end of the timeline, if this enemy's health colour is red, this enemy transforms into No One's Sister.";
            MercurialSisterFull._triggerOn = [TriggerCalls.TimelineEndReached];
            MercurialSisterFull.conditions = [IsRed];
            MercurialSisterFull.doesPassiveTriggerInformationPanel = true;
            MercurialSisterFull.effects = [Effects.GenerateEffect(BecomeEmpty, 1, Targeting.Slot_SelfSlot)];

            PerformEffectPassiveAbility MercurialSisterEmpty = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
            MercurialSisterEmpty.name = "AA_Mercurial_SistersNoone";
            MercurialSisterEmpty._passiveName = "Mercurial";
            MercurialSisterEmpty.m_PassiveID = "Mercurial";
            MercurialSisterEmpty.passiveIcon = ResourceLoader.LoadSprite("IconTransformPassive");
            MercurialSisterEmpty._characterDescription = "if your health is grey on tineline end this shit turns you into Nowak lmao";
            MercurialSisterEmpty._enemyDescription = "At the end of the timeline, if this enemy's health colour is grey, this enemy transforms into Someone's Sister.";
            MercurialSisterEmpty._triggerOn = [TriggerCalls.TimelineEndReached];
            MercurialSisterEmpty.conditions = [IsGrey];
            MercurialSisterEmpty.doesPassiveTriggerInformationPanel = true;
            MercurialSisterEmpty.effects = [Effects.GenerateEffect(BecomeFull, 1, Targeting.Slot_SelfSlot)];

            fullsister.AddPassives([TwoFacedXR, MercurialSisterFull]);
            emptysister.AddPassives([TwoFacedRX, MercurialSisterEmpty]);

            Ability hillchanger = new Ability("Hillchanger", "AApocrypha_Hillchanger_A")
            {
                Description = "Mirror this enemy's position.\nThen, if this enemy is Opposing a party member, swap this enemy's health colour between grey and red.",
                Cost = [Pigments.Grey, Pigments.Red],
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<MirrorPositionEffect>(), 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<CheckHasUnitEffect>(), 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(GreyRed, 1, Targeting.Slot_SelfSlot, PreviousTrue),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.Normal,
            };
            hillchanger.AddIntentsToTarget(TbazTargeting.MirrorAndSelf(true), [nameof(IntentType_GameIDs.Swap_Mass)]);
            hillchanger.AddIntentsToTarget(TbazTargeting.Mirror(false), [nameof(IntentType_GameIDs.Misc_Hidden)]);
            hillchanger.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Mana_Modify)]);

            PercentageEffectCondition FiftyFifty = ScriptableObject.CreateInstance<PercentageEffectCondition>();
            FiftyFifty.percentage = 50;

            // the intents that have been commented out account for the left and right movements of this enemy, but that looks kinda weird

            Ability hillmover = new Ability("Hillmover", "AApocrypha_Hillmover_A")
            {
                Description = "Move this enemy to the Left or Right. This ability assumes the grid loops around.\nThen, if this enemy is Opposing a party member, swap this enemy's health colour between grey and red.",
                Cost = [Pigments.Grey, Pigments.Red],
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<CasterInEdgesCheckEffect>(), 1, Targeting.Slot_SelfSlot, FiftyFifty),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<MirrorPositionEffect>(), 1, Targeting.Slot_SelfSlot, PreviousGenerator(true, 1)),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<SwapToSidesEffect>(), 1, Targeting.Slot_SelfSlot, PreviousGenerator(false, 2)),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<CheckHasUnitEffect>(), 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(GreyRed, 1, Targeting.Slot_SelfSlot, PreviousTrue),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.Normal,
            };
            hillmover.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Swap_Sides)]);
            //hillmover.AddIntentsToTarget(Targeting.GenerateSlotTarget([1, -1, 4, -4], false), [nameof(IntentType_GameIDs.Misc_Hidden)]);
            hillmover.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Misc_Hidden)]);
            hillmover.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Mana_Modify)]);

            Ability oathbreaker = new Ability("Oathbreaker", "AApocrypha_Oathbreaker_A")
            {
                Description = "Mirror this enemy's position, then deal a Painful amount of damage to the previously AND newly Opposing party members and swap their positions.\nThen, if this enemy is Opposing a party member, swap this enemy's health colour between red and grey.",
                Cost = [Pigments.Grey, Pigments.Red],
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<MirrorPositionEffect>(), 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 5, TbazTargeting.MirrorAndSelf(false), PreviousGenerator(true, 1)),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<SwapTwoTargetsEffect>(), 1, TbazTargeting.MirrorAndSelf(false), PreviousGenerator(true, 2)),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 5, Targeting.Slot_Front, PreviousGenerator(false, 3)),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<CheckHasUnitEffect>(), 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(RedGrey, 1, Targeting.Slot_SelfSlot, PreviousTrue),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.Normal,
            };
            oathbreaker.AddIntentsToTarget(TbazTargeting.MirrorAndSelf(true), [nameof(IntentType_GameIDs.Swap_Mass)]);
            oathbreaker.AddIntentsToTarget(TbazTargeting.MirrorAndSelf(false), [nameof(IntentType_GameIDs.Damage_3_6)]);
            oathbreaker.AddIntentsToTarget(TbazTargeting.MirrorAndSelf(false), [nameof(IntentType_GameIDs.Swap_Mass)]);
            oathbreaker.AddIntentsToTarget(TbazTargeting.Mirror(false), [nameof(IntentType_GameIDs.Misc_Hidden)]);
            oathbreaker.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Mana_Modify)]);

            Ability oathtwister = new Ability("Oathtwister", "AApocrypha_Oathtwister_A")
            {
                Description = "Move this enemy to the Left or Right. This assumes the grid loops around.\nThen, deal a Painful amount of damage to the Left and Right party members and swap their positions. This assumes the grid loops around.\nThen, if this enemy is Opposing a party member, swap this enemy's health colour between red and grey.",
                Cost = [Pigments.Grey, Pigments.Red],
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<CasterInEdgesCheckEffect>(), 1, Targeting.Slot_SelfSlot, FiftyFifty),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<MirrorPositionEffect>(), 1, Targeting.Slot_SelfSlot, PreviousGenerator(true, 1)),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<SwapToSidesEffect>(), 1, Targeting.Slot_SelfSlot, PreviousGenerator(false, 2)),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 5, Targeting.GenerateSlotTarget([1, -1, 4, -4], false)),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<SwapTwoTargetsEffect>(), 1, Targeting.GenerateSlotTarget([1, -1, 4, -4], false)),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<CheckHasUnitEffect>(), 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(RedGrey, 1, Targeting.Slot_SelfSlot, PreviousTrue),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.Normal,
            };
            oathtwister.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Swap_Sides)]);
            //oathtwister.AddIntentsToTarget(Targeting.GenerateSlotTarget([0, 2, -2, 3, -3], false), [nameof(IntentType_GameIDs.Damage_3_6)]);
            //oathtwister.AddIntentsToTarget(Targeting.GenerateSlotTarget([0, 2, -2, 3, -3], false), [nameof(IntentType_GameIDs.Swap_Mass)]);
            //oathtwister.AddIntentsToTarget(Targeting.GenerateSlotTarget([1, -1, 4, -4], false), [nameof(IntentType_GameIDs.Misc_Hidden)]);
            oathtwister.AddIntentsToTarget(Targeting.GenerateSlotTarget([1, -1, 4, -4], false), [nameof(IntentType_GameIDs.Damage_3_6)]);
            oathtwister.AddIntentsToTarget(Targeting.GenerateSlotTarget([1, -1, 4, -4], false), [nameof(IntentType_GameIDs.Swap_Mass)]);
            oathtwister.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Misc_Hidden)]);
            oathtwister.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Mana_Modify)]);

            StatusEffect_Apply_Effect HasteApply = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            HasteApply._Status = StatusField.GetCustomStatusEffect("Haste_ID");

            Ability memoryofsilver = new Ability("Memory of Silver", "AApocrypha_SisterMemoryA_A")
            {
                Description = "Apply 1 Haste to this enemy.\n\"Better not dwell on this.\"",
                Cost = [Pigments.Grey, Pigments.Red],
                Visuals = Visuals.Bosch,
                AnimationTarget = Targeting.Slot_SelfSlot,
                Effects =
                [
                    Effects.GenerateEffect(HasteApply, 1, Targeting.Slot_SelfSlot),
                ],
                Rarity = Rarity.Rare,
                Priority = Priority.Normal,
            };
            memoryofsilver.AddIntentsToTarget(Targeting.Slot_SelfSlot, ["Status_Haste"]);

            Ability memoryoffire = new Ability("Memory of Fire", "AApocrypha_SisterMemoryB_A")
            {
                Description = "Apply 1 Haste to this enemy.\n\"It was painful to lose your sister-self.\"",
                Cost = [Pigments.Grey, Pigments.Red],
                Visuals = Visuals.Bosch,
                AnimationTarget = Targeting.Slot_SelfSlot,
                Effects =
                [
                    Effects.GenerateEffect(HasteApply, 1, Targeting.Slot_SelfSlot),
                ],
                Rarity = Rarity.Rare,
                Priority = Priority.Normal,
            };
            memoryoffire.AddIntentsToTarget(Targeting.Slot_SelfSlot, ["Status_Haste"]);

            fullsister.AddEnemyAbilities(
            [
                hillchanger,
                hillmover,
                memoryofsilver,
            ]);

            emptysister.AddEnemyAbilities(
            [
                oathbreaker,
                oathtwister,
                memoryoffire,
            ]);

            fullsister.AddEnemy(false, false, false);
            emptysister.AddEnemy(false, false, false);
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
