using System.Linq;
using A_Apocrypha.CustomOther;

namespace A_Apocrypha.Enemies
{
    public class Rift
    {
        public static void Add()
        {
            Enemy rift = new Enemy("Hyperdimensional Fracture", "RiftMiniboss_EN")
            {
                Health = 100,
                HealthColor = Pigments.Grey,
                Size = 2,
                CombatSprite = ResourceLoader.LoadSprite("RiftTimeline", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("AnomalyDead", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("RiftTimelineOutlined", new Vector2(0.5f, 0f), 32),
                DamageSound = "event:/AAEnemy/StaticHurt",
                DeathSound = "event:/AAEnemy/StaticHurt",
            };
            rift.PrepareEnemyPrefab("Assets/Apocrypha_Enemies/Rift_Enemy/Rift_Enemy.prefab", AApocrypha.assetBundle, null);

            SwapToOneSideEffect SwapLeft = ScriptableObject.CreateInstance<SwapToOneSideEffect>();
            SwapLeft._swapRight = false;

            SwapToOneSideEffect SwapRight = ScriptableObject.CreateInstance<SwapToOneSideEffect>();
            SwapRight._swapRight = true;

            AnimationVisualsIfUnitEffect FrontStaticAnim = ScriptableObject.CreateInstance<AnimationVisualsIfUnitEffect>();
            FrontStaticAnim._visuals = CustomVisuals.StaticColorVisualsSO;
            FrontStaticAnim._animationTarget = Targeting.Slot_Front;

            AddRandomPassiveEffect PassiveAdd = ScriptableObject.CreateInstance<AddRandomPassiveEffect>();
            PassiveAdd._passivesToAdd = RiftPassiveGetter();

            RemoveRandomPassiveEffect PassiveRemove = ScriptableObject.CreateInstance<RemoveRandomPassiveEffect>();

            PreviousEffectCondition PreviousTrue = ScriptableObject.CreateInstance<PreviousEffectCondition>();
            PreviousTrue.wasSuccessful = true;

            PreviousEffectCondition PreviousFalse = ScriptableObject.CreateInstance<PreviousEffectCondition>();
            PreviousFalse.wasSuccessful = false;

            PreviousEffectCondition Previous2False = ScriptableObject.CreateInstance<PreviousEffectCondition>();
            Previous2False.wasSuccessful = false;
            Previous2False.previousAmount = 2;

            PartyMembersByMostPassivesTargeting MostPassivesParty = ScriptableObject.CreateInstance<PartyMembersByMostPassivesTargeting>();
            MostPassivesParty.targetUnitAllySlots = false;
            MostPassivesParty.slotOffsets = [0];
            MostPassivesParty.oneOfTargets = true;

            PartyMembersByMostPassivesTargeting MostPassivesPartyAll = ScriptableObject.CreateInstance<PartyMembersByMostPassivesTargeting>();
            MostPassivesPartyAll.targetUnitAllySlots = false;
            MostPassivesPartyAll.slotOffsets = [0];
            MostPassivesPartyAll.oneOfTargets = false;

            StatusEffect_ApplyRandom_NegativeEffect RandomStatusNegative = ScriptableObject.CreateInstance<StatusEffect_ApplyRandom_NegativeEffect>();

            ExtraLootEffect Treasure = ScriptableObject.CreateInstance<ExtraLootEffect>();
            Treasure._isTreasure = true;
            Treasure._getLocked = true;

            rift.CombatExitEffects = [Effects.GenerateEffect(Treasure, 1)];

            Ability realityshift = new Ability("Reality Shift", "AApocrypha_RealityShift_A")
            {
                Description = "Transform 3 Pigment not of this enemy's health colour into this enemy's health colour.",
                Cost = [Pigments.Grey],
                Visuals = CustomVisuals.StaticColorVisualsSO,
                AnimationTarget = Targeting.Slot_SelfAll,
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<RandomizeNumberPigmentCasterHealthColorEffect>(), 3, Targeting.Slot_SelfSlot),
                ],
                Rarity = Rarity.Impossible,
                Priority = Priority.ExtremelyFast,
            };
            realityshift.AddIntentsToTarget(Targeting.Slot_SelfAll, ["AA_Pigment_Transform"]);

            ExtraAbilityInfo realityshiftextra = new()
            {
                ability = realityshift.GenerateEnemyAbility().ability,
                rarity = Rarity.Impossible,
            };

            Ability greater = new Ability("Greater Than Yourself", "AApocrypha_GreaterThanYourself_A")
            {
                Description = "Apply a random passive (to a maximum of 5) to each Opposing party member, or deal a Painful amount of damage to them if this fails, then move the Opposing party members away from this enemy.",
                Cost = [Pigments.Grey],
                Effects =
                [
                    Effects.GenerateEffect(FrontStaticAnim, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(PassiveAdd, 5, Targeting.GenerateBigUnitSlotTarget([0])),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 3, Targeting.GenerateBigUnitSlotTarget([0]), PreviousFalse),
                    Effects.GenerateEffect(PassiveAdd, 5, Targeting.GenerateBigUnitSlotTarget([1])),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 3, Targeting.GenerateBigUnitSlotTarget([1]), PreviousFalse),
                    Effects.GenerateEffect(SwapLeft, 1, Targeting.GenerateBigUnitSlotTarget([0])),
                    Effects.GenerateEffect(SwapRight, 1, Targeting.GenerateBigUnitSlotTarget([1])),
                ],
                Rarity = Rarity.Uncommon,
                Priority = Priority.Normal,
            };
            greater.AddIntentsToTarget(Targeting.Slot_Front, ["AA_AddPassive"]);
            greater.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Damage_3_6)]);
            greater.AddIntentsToTarget(Targeting.GenerateBigUnitSlotTarget([0]), [nameof(IntentType_GameIDs.Swap_Left)]);
            greater.AddIntentsToTarget(Targeting.GenerateBigUnitSlotTarget([1]), [nameof(IntentType_GameIDs.Swap_Right)]);

            Ability highrise = new Ability("The Higher You Rise", "AApocrypha_TheHigherYouRise_A")
            {
                Description = "Apply two random passives (to a maximum of 5) to the party member(s) with the highest health.\nIf no passives were added, deal a Painful amount of damage to them.",
                Cost = [Pigments.Grey],
                Visuals = Visuals.Mitosis,
                AnimationTarget = Targeting.GenerateUnitTarget_Specific_Health(false, false, false, false),
                Effects =
                [
                    Effects.GenerateEffect(PassiveAdd, 5, Targeting.GenerateUnitTarget_Specific_Health(false, false, false, false)),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 3, Targeting.GenerateUnitTarget_Specific_Health(false, false, false, false), PreviousFalse),
                    Effects.GenerateEffect(PassiveAdd, 5, Targeting.GenerateUnitTarget_Specific_Health(false, false, false, false)),
                ],
                Rarity = Rarity.Uncommon,
                Priority = Priority.Normal,
            };
            highrise.AddIntentsToTarget(Targeting.GenerateUnitTarget_Specific_Health(false, false, false, false), ["AA_AddPassive"]);
            highrise.AddIntentsToTarget(Targeting.GenerateUnitTarget_Specific_Health(false, false, false, false), [nameof(IntentType_GameIDs.Damage_3_6)]);

            Ability teardown = new Ability("Tear You Down", "AApocrypha_TearYouDown_A")
            {
                Description = "Deal a Painful amount of damage to one of the party members with the most passives.",
                Cost = [Pigments.Grey],
                Visuals = Visuals.Slash,
                AnimationTarget = MostPassivesPartyAll,
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 5, MostPassivesParty),
                ],
                Rarity = Rarity.Uncommon,
                Priority = Priority.Normal,
            };
            teardown.AddIntentsToTarget(MostPassivesPartyAll, [nameof(IntentType_GameIDs.Damage_3_6)]);

            Ability quiet = new Ability("Peace And Quiet", "AApocrypha_PeaceAndQuiet_A")
            {
                Description = "Remove a random passive from all party members not Opposing this enemy.",
                Cost = [Pigments.Grey],
                Visuals = CustomVisuals.StaticVisualsSO,
                AnimationTarget = Targeting.GenerateSlotTarget([-4, -3, -2, -1, 1, 2, 3, 4]),
                Effects =
                [
                    Effects.GenerateEffect(PassiveRemove, 1, Targeting.GenerateSlotTarget([-4, -3, -2, -1, 1, 2, 3, 4])),
                ],
                Rarity = Rarity.Uncommon,
                Priority = Priority.Normal,
            };
            quiet.AddIntentsToTarget(Targeting.GenerateSlotTarget([-4, -3, -2, -1, 1, 2, 3, 4]), ["AA_RemPassive"]);

            Ability forget = new Ability("They Will Forget You", "AApocrypha_TheyWillForgetYou_A")
            {
                Description = "Try to remove 3 passives from each Opposing party member. For each passive removed from a party member, apply 3 of a random negative status effect to them.",
                Cost = [Pigments.Grey],
                Visuals = CustomVisuals.StaticVisualsSO,
                AnimationTarget = Targeting.Slot_Front,
                Effects =
                [
                    Effects.GenerateEffect(PassiveRemove, 1, Targeting.GenerateBigUnitSlotTarget([0])),
                    Effects.GenerateEffect(RandomStatusNegative, 3, Targeting.GenerateBigUnitSlotTarget([0]), PreviousTrue),
                    Effects.GenerateEffect(PassiveRemove, 1, Targeting.GenerateBigUnitSlotTarget([0])),
                    Effects.GenerateEffect(RandomStatusNegative, 3, Targeting.GenerateBigUnitSlotTarget([0]), PreviousTrue),
                    Effects.GenerateEffect(PassiveRemove, 1, Targeting.GenerateBigUnitSlotTarget([0])),
                    Effects.GenerateEffect(RandomStatusNegative, 3, Targeting.GenerateBigUnitSlotTarget([0]), PreviousTrue),
                    Effects.GenerateEffect(PassiveRemove, 1, Targeting.GenerateBigUnitSlotTarget([1])),
                    Effects.GenerateEffect(RandomStatusNegative, 3, Targeting.GenerateBigUnitSlotTarget([1]), PreviousTrue),
                    Effects.GenerateEffect(PassiveRemove, 1, Targeting.GenerateBigUnitSlotTarget([1])),
                    Effects.GenerateEffect(RandomStatusNegative, 3, Targeting.GenerateBigUnitSlotTarget([1]), PreviousTrue),
                    Effects.GenerateEffect(PassiveRemove, 1, Targeting.GenerateBigUnitSlotTarget([1])),
                    Effects.GenerateEffect(RandomStatusNegative, 3, Targeting.GenerateBigUnitSlotTarget([1]), PreviousTrue),
                ],
                Rarity = Rarity.Uncommon,
                Priority = Priority.Normal,
            };
            forget.AddIntentsToTarget(Targeting.Slot_Front, ["AA_RemPassive"]);
            forget.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Misc_Hidden)]);

            rift.AddPassives([Passives.Skittish, Passives.Slippery, Passives.GetCustomPassive("Omnichromia_PA"), Passives.BonusAttackGenerator(realityshiftextra)]);

            rift.AddEnemyAbilities(
                [
                    greater,
                    highrise,
                    teardown,
                    quiet,
                    forget,
                ]);
            rift.AddEnemy(true, false, false);
        }

        static BasePassiveAbilitySO[] RiftPassiveGetter()
        {
            BasePassiveAbilitySO[] resultBase = [
                Passives.Skittish,
                Passives.Slippery,
                Passives.Focus,
                Passives.BoneSpurs2,
                Passives.Enfeebled,
                Passives.Delicate,
                Passives.Immortal,
                Passives.LeakyGenerator(2),
                Passives.Unstable,
                Passives.Pure,
                Passives.Dying,
                Passives.Inferno,
                Passives.TwoFaced,
                Passives.Infestation2,
                Passives.GetCustomPassive("Shy_PA"),
                Passives.GetCustomPassive("Confrontational_PA"),
                Passives.GetCustomPassive("AA_TornApart_PA"),
                Passives.GetCustomPassive("Gouged_PA"),
                Passives.GetCustomPassive("Omnichromia_PA"),
                Passives.GetCustomPassive("AA_Condense_PA"),
            ];
            var resultList = resultBase.ToList();
            if (AApocrypha.CrossMod.IntoTheAbyss)
            {
                resultList.Add(Passives.GetCustomPassive("Mammal_PA"));
                resultList.Add(Passives.GetCustomPassive("Refinement_PA"));
                resultList.Add(Passives.GetCustomPassive("ItemDupe_PA"));
                resultList.Add(Passives.GetCustomPassive("Tempo2_PA"));
                resultList.Add(Passives.GetCustomPassive("Bloat_PA"));
                resultList.Add(Passives.GetCustomPassive("ATwoFacedXY_PA"));
                resultList.Add(Passives.GetCustomPassive("ThreeFacedRBX_PA"));
                resultList.Add(Passives.GetCustomPassive("ATwoFacedXB_PA"));
                resultList.Add(Passives.GetCustomPassive("IsBasil_PA"));
                resultList.Add(Passives.GetCustomPassive("Euphony2_PA"));
                resultList.Add(Passives.GetCustomPassive("Foolhardy_PA"));
                resultList.Add(Passives.GetCustomPassive("Contagious_PA"));
                resultList.Add(Passives.GetCustomPassive("Lethargic_PA"));
            }
            if (AApocrypha.CrossMod.StewSpecimens)
            {
                resultList.Add(Passives.GetCustomPassive("StSpCauterizing_PA"));
                resultList.Add(Passives.GetCustomPassive("Skates_PA"));
                resultList.Add(Passives.GetCustomPassive("StSpOmniscient2_PA"));
            }
            foreach (BasePassiveAbilitySO passive in resultList) {Debug.Log($"Rift Passive Getter | loaded passive {passive._passiveName} ({passive.name})"); }
            return resultList.ToArray();
        }
    }
}
