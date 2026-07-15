using System;
using System.Collections.Generic;
using System.Text;
using A_Apocrypha.CustomOther;
using static A_Apocrypha.Encounters.Orph.H;
using static A_Apocrypha.Enemies.Bloatfinger;

namespace A_Apocrypha.Fools
{
    public class TeneralNymphCharacter
    {
        public static void Add()
        {
            SpecificPartyMembersTargeting nymphsTarget = ScriptableObject.CreateInstance<SpecificPartyMembersTargeting>();
            nymphsTarget.slotOffsets = [0];
            nymphsTarget.getAllUnitSelfSlots = false;
            nymphsTarget.targetUnitAllySlots = false;
            nymphsTarget._characters = ["TeneralNymph_CH"];

            PerformEffectPassiveAbility ExuviaHandler = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
            ExuviaHandler.name = "AA_NymphExuviaHandler_PA";
            ExuviaHandler._passiveName = "Exuviae";
            ExuviaHandler.m_PassiveID = "AA_NymphExuviaHandler";
            ExuviaHandler.passiveIcon = ResourceLoader.LoadSprite("IconOpportunity");
            ExuviaHandler._characterDescription = "If any Teneral Nymphs or Discarded Exuviae are killed escept by Withering, destroy any items equipped by all Nymphs.";
            ExuviaHandler._enemyDescription = ExuviaHandler._characterDescription;
            ExuviaHandler._triggerOn = [TriggerCalls.OnDeath];
            ExuviaHandler.conditions = [ScriptableObject.CreateInstance<IsntWitheringDeathCondition>()];
            ExuviaHandler.doesPassiveTriggerInformationPanel = true;
            ExuviaHandler.effects = [Effects.GenerateEffect(ScriptableObject.CreateInstance<ConsumeItemEffect>(), 1, nymphsTarget)];
            Passives.AddCustomPassiveToPool("AA_NymphExuviaHandler_PA", "Exuviae", ExuviaHandler);
            //Debug.Log("load nymph start");
            Character nymph = new Character("Teneral Nymph", "TeneralNymph_CH")
            {
                HealthColor = Pigments.Purple,
                UsesBasicAbility = false,
                UsesAllAbilities = true,
                MovesOnOverworld = true,
                FrontSprite = ResourceLoader.LoadSprite("TeneralNymphFront", new Vector2(0.5f, 0f), 32),
                BackSprite = ResourceLoader.LoadSprite("TeneralNymphFront", new Vector2(0.5f, 0f), 32),
                OverworldSprite = ResourceLoader.LoadSprite("TeneralNymphOverworld", new Vector2(0.5f, 0f), 32),
                DamageSound = LoadedAssetsHandler.GetEnemy("SilverSuckle_EN").damageSound,
                DeathSound = LoadedAssetsHandler.GetEnemy("SilverSuckle_EN").deathSound,
                DialogueSound = LoadedAssetsHandler.GetEnemy("SilverSuckle_EN").damageSound,
                UnitTypes = ["Sandwich_Gore"],
            };
            //Debug.Log("load nymph getanim");
            UnityEngine.Object NymphCharacterAnimator = AApocrypha.assetBundle.LoadAsset("Assets/Apocrypha_Fools/InstantSwapFoolOverrideController.overrideController");
            //Debug.Log("load nymph setanim"); 
            nymph.Animator = (RuntimeAnimatorController)(NymphCharacterAnimator is RuntimeAnimatorController ? NymphCharacterAnimator : null);
            //Debug.Log("load nymph passives");
            nymph.AddPassives([Passives.Slippery, Passives.Withering, Passives.GetCustomPassive("AA_NymphExuviaHandler_PA")]);

            Ability wetslap = new Ability("Wet Slap", "TeneralNymphWetSlap_A")
            {
                Description = "Deal 1 damage to the Opposing enemy. Move the Opposing enemy to the Left or Right.\n\"Eurgh...\"",
                AbilitySprite = ResourceLoader.LoadSprite("IconNymphSlap"),
                Cost = [Pigments.Yellow],
                Visuals = Visuals.Slap,
                AnimationTarget = Targeting.Slot_Front,
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<SwapToSidesEffect>(), 1, Targeting.Slot_Front),
                ]
            };
            wetslap.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Damage_1_2), nameof(IntentType_GameIDs.Swap_Sides)]);
            //Debug.Log("load nymph ab1");

            GenerateRandomManaBetweenEffect warbleGen = ScriptableObject.CreateInstance<GenerateRandomManaBetweenEffect>();
            warbleGen.possibleMana = [Pigments.Red, Pigments.Red, Pigments.Blue, Pigments.Yellow, Pigments.Purple];

            Ability warble = new Ability("Warble", "TeneralNymphWarble_A")
            {
                Description = "Produce 2 Pigment of random colors.",
                AbilitySprite = ResourceLoader.LoadSprite("IconNymphWarble"),
                Cost = [Pigments.SplitPigment([Pigments.Red, Pigments.Blue, Pigments.Purple])],
                Visuals = Visuals.Wriggle,
                AnimationTarget = Targeting.Slot_SelfSlot,
                Effects =
                [
                    Effects.GenerateEffect(warbleGen, 2, Targeting.Slot_SelfSlot),
                ]
            };
            warble.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Mana_Generate)]);
            //Debug.Log("load nymph ab2");

            nymph.AddLevelData(5, [wetslap, warble]);
            nymph.AddCharacter(true, true);
        }
    }
}
