using System;
using System.Collections.Generic;
using System.Text;
using A_Apocrypha.CustomOther;
using UnityEngine.Assertions.Must;

namespace A_Apocrypha.Enemies
{
    public class CageGarden
    {
        public static void Add()
        {
            QueueTimelineAbilityByNameEffect QueueNoMore = ScriptableObject.CreateInstance<QueueTimelineAbilityByNameEffect>();
            QueueNoMore._abilityName = "No more...";

            TurnCountComparatorEffectorCondition Patient3Condition = ScriptableObject.CreateInstance<TurnCountComparatorEffectorCondition>();
            Patient3Condition._lessThan = false;
            Patient3Condition._comparator = 3;

            CasterStoredValueSetEffect PatientSet = ScriptableObject.CreateInstance<CasterStoredValueSetEffect>();
            PatientSet._valueName = "PatientTurnCountStoredValue";

            PerformEffectPassiveAbility patientPassive = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
            patientPassive.name = "AA_Patient3CageGarden_PA";
            patientPassive._passiveName = "No more... (3)";
            patientPassive.m_PassiveID = "Patient";
            patientPassive.passiveIcon = ResourceLoader.LoadSprite("IconPatient");
            patientPassive._characterDescription = "bees in your head";
            patientPassive._enemyDescription = "When the player turn ends, if the turn count is 3 or higher, this enemy queues the ability \"No more...\".";
            patientPassive._triggerOn = [TriggerCalls.OnPlayerTurnEnd_ForEnemy];
            patientPassive.doesPassiveTriggerInformationPanel = true;
            patientPassive.conditions = [Patient3Condition];
            patientPassive.effects = [
                Effects.GenerateEffect(QueueNoMore, 1, Targeting.Slot_SelfSlot),
            ];
            patientPassive.specialStoredData = UnitStoreData.GetCustom_UnitStoreData("PatientTurnCountStoredValue");

            ExtraLootEffect Treasure = ScriptableObject.CreateInstance<ExtraLootEffect>();
            Treasure._isTreasure = true;
            Treasure._getLocked = false;

            GainPlayerCurrencyEffect FourteenDollars = ScriptableObject.CreateInstance<GainPlayerCurrencyEffect>();
            FourteenDollars._gainForPlayer = true;
            FourteenDollars._usePreviousExitValue = false;

            PerformEffectPassiveAbility lootHandler = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
            lootHandler.name = "AA_LootHandlerCageGarden_PA";
            lootHandler._passiveName = "";
            lootHandler.m_PassiveID = "LootHandlerCageGarden";
            lootHandler.passiveIcon = ResourceLoader.LoadSprite("AnomalyDead");
            lootHandler._characterDescription = "???????";
            lootHandler._enemyDescription = "When this enemy dies, it produces 1 Treasure item and 14 Coins." +
                "\nI couldn't think of something better, so boom! Invisible passive.";
            lootHandler._triggerOn = [TriggerCalls.OnDeath];
            lootHandler.doesPassiveTriggerInformationPanel = false;
            lootHandler.conditions = [ScriptableObject.CreateInstance<NotWitheringDeathCondition>()];
            lootHandler.effects = [
                Effects.GenerateEffect(Treasure, 1, Targeting.Slot_SelfSlot),
                Effects.GenerateEffect(FourteenDollars, 14, Targeting.Slot_SelfSlot),
            ];

            Enemy gaolerhoney = new Enemy("Dream of a Cage-Garden", "CageGarden_Item_EN")
            {
                Health = 40,
                HealthColor = Pigments.Red,
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("UnlockDeathmatchKneynsberg", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("AnomalyDead", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("AnomalyDead", new Vector2(0.5f, 0f), 32),
                DamageSound = LoadedAssetsHandler.GetCharacter("Gospel_CH").damageSound,
                DeathSound = LoadedAssetsHandler.GetCharacter("Gospel_CH").deathSound,
                UnitTypes = ["Parabolan"],
                CombatEnterEffects = [Effects.GenerateEffect(PatientSet, 3, Targeting.Slot_SelfSlot)],
            };

            Enemy gaolerhoneyactual = new Enemy("Dream of a Cage-Garden", "CageGarden_EN")
            {
                Health = 40,
                HealthColor = Pigments.Red,
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("UnlockDeathmatchKneynsberg", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("AnomalyDead", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("AnomalyDead", new Vector2(0.5f, 0f), 32),
                DamageSound = LoadedAssetsHandler.GetCharacter("Gospel_CH").damageSound,
                DeathSound = LoadedAssetsHandler.GetCharacter("Gospel_CH").deathSound,
                UnitTypes = ["Parabolan"],
            };
            gaolerhoney.PrepareEnemyPrefab("Assets/Apocrypha_Enemies/CageGarden_Enemy/CageGarden_Enemy.prefab", AApocrypha.assetBundle, AApocrypha.assetBundle.LoadAsset<GameObject>("Assets/Apocrypha_Enemies/CageGarden_Enemy/CageGarden_Giblets.prefab").GetComponent<ParticleSystem>());
            gaolerhoney.AddPassives([Passives.Pure, Passives.Forgetful, patientPassive, Passives.Withering, lootHandler]);
            gaolerhoneyactual.AddPassives([Passives.Pure, Passives.Forgetful, Passives.Withering]);

            PigmentAmountCheckEffect CasterHealthCheck = ScriptableObject.CreateInstance<PigmentAmountCheckEffect>();
            CasterHealthCheck._useCasterHealthColor = true;
            CasterHealthCheck._contains = false;

            StatusEffect_Apply_Effect ScarsToRand = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            ScarsToRand._Status = StatusField.Scars;
            ScarsToRand._JustOneRandomTarget = true;

            StatusEffect_ApplyWithRandomDistribution_Effect ScarsRand = ScriptableObject.CreateInstance<StatusEffect_ApplyWithRandomDistribution_Effect>();
            ScarsRand.status = StatusField.Scars;
            ScarsRand.usePrevious = true;

            FieldEffect_Apply_Effect Shieldadd = ScriptableObject.CreateInstance<FieldEffect_Apply_Effect>();
            Shieldadd._Field = StatusField.Shield;

            RandomTargetPerformEffectViaSubaction ShieldThyselves = ScriptableObject.CreateInstance<RandomTargetPerformEffectViaSubaction>();
            ShieldThyselves.effects = [Effects.GenerateEffect(Shieldadd, 2, Targeting.Slot_SelfAll)];

            PerformEffectXTimesViaSubaction ScarsRepeat = ScriptableObject.CreateInstance<PerformEffectXTimesViaSubaction>();
            ScarsRepeat.effects = [
                Effects.GenerateEffect(ScarsToRand, 1, Targeting.Slot_SelfSlot),
                Effects.GenerateEffect(ScarsToRand, 1, Targeting.Unit_AllOpponents),
            ];
            ScarsRepeat.usePreviousExit = true;

            GenerateCasterHealthManaEffect bleed = ScriptableObject.CreateInstance<GenerateCasterHealthManaEffect>();

            ConsumeCasterColorManaEffect bleedEater = ScriptableObject.CreateInstance<ConsumeCasterColorManaEffect>();

            PerformEffectXTimesViaSubaction SupRepeat = ScriptableObject.CreateInstance<PerformEffectXTimesViaSubaction>();
            SupRepeat.effects = [
                Effects.GenerateEffect(bleedEater, 1, Targeting.Slot_SelfSlot),
                Effects.GenerateEffect(ShieldThyselves, 1, Targeting.Unit_AllAllies),
            ];
            SupRepeat.usePreviousExit = true;

            Ability crawl = new Ability("I feel them crawling in my head", "AApocrypha_CageGardenCrawl_A")
            {
                Description = "For each pigment in the pigment tray of this enemy's health color, apply 1 Scar to this enemy and to a random party member. If this fails, produce 5 pigment of this enemy's health color.",
                Cost = [],
                Effects =
                [
                    Effects.GenerateEffect(CasterHealthCheck, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(ScarsRepeat, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(bleed, 5, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 2)),
                ],
                Rarity = Rarity.ExtremelyCommon,
                Priority = Priority.Fast,
            };
            crawl.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Status_Scars)]);
            crawl.AddIntentsToTarget(Targeting.Unit_AllOpponents, [nameof(IntentType_GameIDs.Status_Scars)]);
            crawl.AddIntentsToTarget(Targeting.Slot_SelfSlot, ["AA_MultiX", nameof(IntentType_GameIDs.Mana_Modify)]);
            crawl.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Misc_Hidden), nameof(IntentType_GameIDs.Mana_Generate)]);

            Ability sup = new Ability("I feel them supping on my tears", "AApocrypha_CageGardenSup_A")
            {
                Description = "For each pigment in the pigment tray of this enemy's health color, consume it and apply 2 Shield to a random occupied enemy position. If this fails, produce 5 pigment of this enemy's health color.",
                Cost = [],
                Effects =
                [
                    Effects.GenerateEffect(CasterHealthCheck, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(SupRepeat, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(bleed, 5, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 2)),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.Fast,
            };
            sup.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Mana_Consume)]);
            sup.AddIntentsToTarget(Targeting.Unit_AllAllies, [nameof(IntentType_GameIDs.Field_Shield)]);
            sup.AddIntentsToTarget(Targeting.Slot_SelfSlot, ["AA_MultiX", nameof(IntentType_GameIDs.Mana_Modify)]);
            sup.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Misc_Hidden), nameof(IntentType_GameIDs.Mana_Generate)]);

            Ability tear = new Ability("I feel them tearing me apart", "AApocrypha_CageGardenTear_A")
            {
                Description = "Deal an Agonizing amount of damage to this enemy, then randomly distribute Scars to all party members equal to the damage dealt and Heal this enemy, even if this damage would have killed it.",
                Cost = [],
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 10, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(ScarsRand, 1, Targeting.Unit_AllOpponents, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<HealEffect>(), 10, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 2)),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.Fast,
            };
            tear.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Damage_7_10)]);
            tear.AddIntentsToTarget(Targeting.Unit_AllOpponents, [nameof(IntentType_GameIDs.Status_Scars)]);
            tear.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Heal_5_10)]);

            Ability nomore = new Ability("No more...", "AApocrypha_CageGardenNoMore_A")
            {
                Description = "The dream ends.",
                Cost = [],
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<FleeTargetEffect>(), 1, Targeting.Slot_SelfSlot),
                ],
                Rarity = Rarity.ImpossibleNoReroll,
                Priority = Priority.Normal,
            };
            nomore.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.PA_Fleeting)]);

            gaolerhoney.AddEnemyAbilities([
                crawl.GenerateEnemyAbility(true),
                sup.GenerateEnemyAbility(true),
                tear.GenerateEnemyAbility(true),
                nomore.GenerateEnemyAbility(false),
            ]);

            gaolerhoneyactual.AddEnemyAbilities([
                crawl.GenerateEnemyAbility(true),
                sup.GenerateEnemyAbility(true),
                tear.GenerateEnemyAbility(true),
            ]);

            gaolerhoney.AddEnemy(false, false, false);
            gaolerhoneyactual.AddEnemy(true, false, false);
            //LoadedAssetsHandler.GetEnemy("CageGarden_Item_EN").enemyTemplate = LoadedAssetsHandler.GetEnemy("Mung_EN").enemyTemplate;
            LoadedAssetsHandler.GetEnemy("CageGarden_EN").enemyTemplate = LoadedAssetsHandler.GetEnemy("CageGarden_Item_EN").enemyTemplate;
        }
        public class NotWitheringDeathCondition : EffectorConditionSO
        {
            public override bool MeetCondition(IEffectorChecks effector, object args)
            {
                if (args is DeathReference reffe && reffe.witheringDeath == false) return true;
                return false;
            }
        }
    }
}
