using System;
using System.Collections.Generic;
using System.Text;
using A_Apocrypha.CustomOther;
using MythosFriends.Effectsa;
using static A_Apocrypha.Encounters.Abyss.H;

namespace A_Apocrypha.Enemies
{
    public class Guests
    {
        public static void Add()
        {
            Enemy guestsmall = new Enemy("Tangled Guests", "TangledGuests_EN")
            {
                Health = 7,
                HealthColor = Pigments.Yellow,
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("GuestsTimeline", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("GuestsDead", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("GuestsOverworld", new Vector2(0.5f, 0f), 32),
                DamageSound = LoadedAssetsHandler.GetEnemy("SilverSuckle_EN").damageSound,
                DeathSound = LoadedAssetsHandler.GetEnemy("SilverSuckle_EN").deathSound,
                //UnitTypes = ["Empyrean"], //still need to think of a name for the sunless skies tag - it's certainly not neathy
            };
            guestsmall.PrepareEnemyPrefab("Assets/Apocrypha_Enemies/Guests_Enemy/Guests_Enemy.prefab", AApocrypha.assetBundle, AApocrypha.assetBundle.LoadAsset<GameObject>("Assets/Apocrypha_Enemies/Guests_Enemy/Guests_Giblets.prefab").GetComponent<ParticleSystem>());
            guestsmall.AddPassives([Passives.Slippery, Passives.GetCustomPassive("AA_Mucous_1_PA")]);

            DamageWithStatusBonusEffect MucusDamage = ScriptableObject.CreateInstance<DamageWithStatusBonusEffect>();
            MucusDamage._bonusAmount = 1;
            MucusDamage._bonusStacking = true;
            MucusDamage._status = StatusField.GetCustomStatusEffect("Mucus_ID");

            LeftOrRightToOpposeEnemyChanceForNextEffect NavigatorOpposing = ScriptableObject.CreateInstance<LeftOrRightToOpposeEnemyChanceForNextEffect>();
            NavigatorOpposing._inverted = false;

            SwapToOneSideEffect Left = ScriptableObject.CreateInstance<SwapToOneSideEffect>();
            Left._swapRight = false;

            SwapToOneSideEffect Right = ScriptableObject.CreateInstance<SwapToOneSideEffect>();
            Right._swapRight = true;

            Ability nostalgia = new Ability("Nostalgia", "AApocrypha_GuestNostalgia_A")
            {
                Description = "Move to the Left or Right, prioritizing opposed spaces, if not Opposing a party member. Deal Almost No damage to the Opposing party member.",
                Cost = [Pigments.Yellow],
                Visuals = Visuals.Innocence,
                AnimationTarget = Targeting.Slot_SelfSlot,
                Effects = [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<CheckHasUnitEffect>(), 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(NavigatorOpposing, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 1)),
                    Effects.GenerateEffect(Right, 1, Targeting.Slot_SelfSlot, Effects.CheckMultiplePreviousEffectsCondition([true, false], [1, 2])),
                    Effects.GenerateEffect(Left, 1, Targeting.Slot_SelfSlot, Effects.CheckMultiplePreviousEffectsCondition([false, false], [2, 3])),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 1, Targeting.Slot_Front),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.Normal,
            };
            nostalgia.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Misc_Hidden)]);
            nostalgia.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Swap_Sides)]);
            nostalgia.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Misc), nameof(IntentType_GameIDs.Damage_1_2)]);

            PerformEffectViaSubaction swapsub = ScriptableObject.CreateInstance<PerformEffectViaSubaction>();
            swapsub.effects = [Effects.GenerateEffect(ScriptableObject.CreateInstance<SwapToSidesEffect>(), 1, Targeting.Slot_SelfSlot)];

            Ability uninvited = new Ability("Uninvited", "AApocrypha_GuestUninvited_A")
            {
                Description = "Move to the Left or Right three times.",
                Cost = [Pigments.Yellow],
                Visuals = Visuals.Wriggle,
                AnimationTarget = Targeting.Slot_SelfSlot,
                Effects = [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<SwapToSidesEffect>(), 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(swapsub),
                    Effects.GenerateEffect(swapsub),
                ],
                Rarity = Rarity.Uncommon,
                Priority = Priority.Normal,
            };
            uninvited.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Swap_Sides), nameof(IntentType_GameIDs.Swap_Sides), nameof(IntentType_GameIDs.Swap_Sides)]);

            CheckHasUnitWithIDsEffect FindPartners = ScriptableObject.CreateInstance<CheckHasUnitWithIDsEffect>();
            FindPartners._ids = [guestsmall.enemy.name];

            SpawnEnemyAnywhereEffect WormUp = ScriptableObject.CreateInstance<SpawnEnemyAnywhereEffect>();
            WormUp.enemy = guestsmall.enemy;
            WormUp.givesExperience = true;
            WormUp._spawnTypeID = CombatType_GameIDs.Spawn_Basic.ToString();

            AnimationVisualsEffect WrigglersAnim = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            WrigglersAnim._visuals = Visuals.WrigglingWrath;
            WrigglersAnim._animationTarget = Targeting.Slot_SelfSlot;

            AnimationVisualsEffect CryAnim = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            CryAnim._visuals = Visuals.Cry;
            CryAnim._animationTarget = Targeting.Slot_SelfSlot;

            Ability entanglement = new Ability("Entanglement", "AApocrypha_GuestEntanglement_A")
            {
                Description = "If there are any other Tangled Guests in combat, produce another cluster of Tangled Guests.",
                Cost = [Pigments.Yellow],
                //Visuals = Visuals.WrigglingWrath,
                //AnimationTarget = Targeting.Slot_SelfSlot,
                Effects = [
                    Effects.GenerateEffect(FindPartners, 1, Targeting.Unit_OtherAllies),
                    Effects.GenerateEffect(WrigglersAnim, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(WormUp, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 2)),
                    Effects.GenerateEffect(CryAnim, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 3)),
                ],
                Rarity = Rarity.Rare,
                Priority = Priority.Normal,
            };
            entanglement.AddIntentsToTarget(Targeting.Unit_OtherAllies, [nameof(IntentType_GameIDs.Misc)]);
            entanglement.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Other_Spawn)]);

            guestsmall.AddEnemyAbilities([
                nostalgia.GenerateEnemyAbility(true),
                uninvited.GenerateEnemyAbility(true),
                entanglement.GenerateEnemyAbility(true),
            ]);
            guestsmall.AddEnemy(true, true, true);
            //LoadedAssetsHandler.GetEnemy("TangledGuests_EN").enemyTemplate = LoadedAssetsHandler.GetEnemy("Keko_EN").enemyTemplate;

            Enemy guestbig = new Enemy("Guest Colony", "GuestColony_EN")
            {
                Health = 28,
                HealthColor = Pigments.Yellow,
                Size = 2,
                CombatSprite = ResourceLoader.LoadSprite("GuestsBigTimeline", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("GuestsBigDead", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("GuestsBigOverworld", new Vector2(0.5f, 0f), 32),
                DamageSound = LoadedAssetsHandler.GetEnemy("SilverSuckle_EN").damageSound,
                DeathSound = LoadedAssetsHandler.GetEnemy("SilverSuckle_EN").deathSound,
                //UnitTypes = ["Empyrean"], //still need to think of a name for the sunless skies tag - it's certainly not neathy
            };
            guestbig.PrepareEnemyPrefab("Assets/Apocrypha_Enemies/Guests_Enemy/GuestsBig_Enemy.prefab", AApocrypha.assetBundle, AApocrypha.assetBundle.LoadAsset<GameObject>("Assets/Apocrypha_Enemies/Guests_Enemy/GuestsBig_Giblets.prefab").GetComponent<ParticleSystem>());

            ReturnValueComparatorEffectorCondition SevenOrMore = ScriptableObject.CreateInstance<ReturnValueComparatorEffectorCondition>();
            SevenOrMore._lessThan = false;
            SevenOrMore._comparator = 7;

            PerformEffectPassiveAbility deploymentGuests = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
            deploymentGuests.name = "AA_DeploymentGuests_PA";
            deploymentGuests._passiveName = "Deployment (7)";
            deploymentGuests.m_PassiveID = "DeploymentGuests";
            deploymentGuests.passiveIcon = ResourceLoader.LoadSprite("IconDeployment");
            deploymentGuests._characterDescription = "You have Guests aboard.";
            deploymentGuests._enemyDescription = "On taking 7 or more damage, attempt to summon a cluster of Tangled Guests.";
            deploymentGuests.doesPassiveTriggerInformationPanel = true;
            deploymentGuests._triggerOn = [TriggerCalls.OnDirectDamaged];
            deploymentGuests.conditions = [SevenOrMore];
            deploymentGuests.effects =
            [
                Effects.GenerateEffect(WormUp, 1, Targeting.Slot_SelfSlot),
            ];
            Passives.AddCustomPassiveToPool("AA_DeploymentGuests_PA", "Deployment (7)", deploymentGuests);

            guestbig.AddPassives([Passives.GetCustomPassive("AA_DeploymentGuests_PA"), Passives.DecayGenerator(guestsmall.enemy)]);

            QueueTimelineAbilityByNameEffect QueueNostalgia = ScriptableObject.CreateInstance<QueueTimelineAbilityByNameEffect>();
            QueueNostalgia._abilityName = "Nostalgia";

            QueueTimelineAbilityByNameEffect QueueUninvited = ScriptableObject.CreateInstance<QueueTimelineAbilityByNameEffect>();
            QueueUninvited._abilityName = "Uninvited";

            SpecificEnemiesTargeting AllGuests = ScriptableObject.CreateInstance<SpecificEnemiesTargeting>();
            AllGuests._enemies = ["TangledGuests_EN"];
            AllGuests.targetUnitAllySlots = true;
            AllGuests.slotOffsets = [0];

            RandomTargetPerformEffectViaSubaction QueueAGuestNostalgia = ScriptableObject.CreateInstance<RandomTargetPerformEffectViaSubaction>();
            QueueAGuestNostalgia.effects =
            [
                Effects.GenerateEffect(QueueNostalgia, 1, Targeting.Slot_SelfSlot),
            ];

            RandomTargetPerformEffectViaSubaction QueueAGuestUninvited = ScriptableObject.CreateInstance<RandomTargetPerformEffectViaSubaction>();
            QueueAGuestUninvited.effects =
            [
                Effects.GenerateEffect(QueueUninvited, 1, Targeting.Slot_SelfSlot),
            ];

            Ability makenostalgia = new Ability("Inspire Nostalgia", "AApocrypha_GuestMakeNostalgia_A")
            {
                Description = "Force a random cluster of Tangled Guests to queue \"Nostalgia\".",
                Cost = [Pigments.Yellow],
                Visuals = Visuals.Innocence,
                AnimationTarget = AllGuests,
                Effects = [
                    Effects.GenerateEffect(QueueAGuestNostalgia, 1, AllGuests),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.Fast,
            };
            makenostalgia.AddIntentsToTarget(AllGuests, [nameof(IntentType_GameIDs.Misc_Additional)]);

            Ability makeuninvited = new Ability("Writhing Reverence", "AApocrypha_GuestMakeUninvited_A")
            {
                Description = "Force a random cluster of Tangled Guests to queue \"Uninvited\".",
                Cost = [Pigments.Yellow],
                Visuals = Visuals.Wriggle,
                AnimationTarget = AllGuests,
                Effects = [
                    Effects.GenerateEffect(QueueAGuestUninvited, 1, AllGuests),
                ],
                Rarity = Rarity.Uncommon,
                Priority = Priority.Fast,
            };
            makeuninvited.AddIntentsToTarget(AllGuests, [nameof(IntentType_GameIDs.Misc_Additional)]);

            StatusEffect_Apply_Effect Mucusify = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            Mucusify._Status = StatusField.GetCustomStatusEffect("Mucus_ID");

            AnimationVisualsEffect GloopAnim = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            GloopAnim._visuals = Visuals.OilSlicked;
            GloopAnim._animationTarget = Targeting.GenerateSlotTarget([-4, -3, -2, -1, 1, 2, 3, 4], false);

            Ability emergence = new Ability("Sodden Emergence", "AApocrypha_GuestSoddenEmergence_A")
            {
                Description = "Deal an Agonizing amount of damage to this enemy and apply 1 Mucus to all party members not Opposing this enemy.",
                Cost = [Pigments.Yellow],
                Visuals = Visuals.Mitosis,
                AnimationTarget = Targeting.Slot_SelfAll,
                Effects = [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 7, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(GloopAnim),
                    Effects.GenerateEffect(Mucusify, 1, Targeting.GenerateSlotTarget([-4, -3, -2, -1, 1, 2, 3, 4], false)),
                ],
                Rarity = Rarity.Uncommon,
                Priority = Priority.Fast,
            };
            emergence.AddIntentsToTarget(Targeting.Slot_SelfAll, [nameof(IntentType_GameIDs.Damage_7_10)]);
            emergence.AddIntentsToTarget(Targeting.GenerateSlotTarget([-4, -3, -2, -1, 1, 2, 3, 4], false), ["Status_Mucus"]);

            guestbig.AddEnemyAbilities([
                makenostalgia.GenerateEnemyAbility(false),
                makeuninvited.GenerateEnemyAbility(false),
                emergence.GenerateEnemyAbility(true),
            ]);
            guestbig.AddEnemy(true, true, false);
            //LoadedAssetsHandler.GetEnemy("GuestColony_EN").enemyTemplate = LoadedAssetsHandler.GetEnemy("Kekastle_EN").enemyTemplate;
        }
    }
}
