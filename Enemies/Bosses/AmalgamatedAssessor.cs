using System;
using System.Collections.Generic;
using System.Text;
using A_Apocrypha.CustomOther;

namespace A_Apocrypha.Enemies.Bosses
{
    public class AmalgamatedAssessor
    {
        public static void Add()
        {
            Enemy assessor = new Enemy("Amalgamated Assessor", "AmalgamatedAssessor_BOSS")
            {
                Health = 100,
                HealthColor = Pigments.Grey,
                Size = 2,
                CombatSprite = ResourceLoader.LoadSprite("AssessorTimeline", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("DuneThresherDead", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("DuneThresherTimelineWhy", new Vector2(0.5f, 0f), 32),
                DamageSound = "event:/AAEnemy/Assessor/AssessorHurt",
                DeathSound = "event:/AAEnemy/Assessor/AssessorDeath",
                UnitTypes = ["Robot"],
                AbilitySelector = ScriptableObject.CreateInstance<AbilitySelector_Assessor>(),
            };
            assessor.PrepareEnemyPrefab("Assets/Apocrypha_Enemies/Assessor_Boss/Assessor_Boss.prefab", AApocrypha.assetBundle, AApocrypha.assetBundle.LoadAsset<GameObject>("Assets/Apocrypha_Enemies/Assessor_Boss/Assessor_Giblets.prefab").GetComponent<ParticleSystem>());

            CasterEnemyVariantHandlerEffect ScrapVariantHandler = ScriptableObject.CreateInstance<CasterEnemyVariantHandlerEffect>();
            ScrapVariantHandler._variantNumber = 3;

            ChangeToRandomHealthColorEffect HealthRandom = ScriptableObject.CreateInstance<ChangeToRandomHealthColorEffect>();
            HealthRandom._healthColors = [Pigments.Red, Pigments.Red, Pigments.Blue, Pigments.Blue, Pigments.Yellow, Pigments.Yellow, Pigments.Purple];

            Enemy scraps = new Enemy("Shattered Scraps", "ShatteredScrapsSummon_EN")
            {
                Health = 12,
                HealthColor = Pigments.Grey,
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("ScrapSummonTimeline", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("SandSifterDead", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("SandSifterTimeline", new Vector2(0.5f, 0f), 32),
                DamageSound = "event:/AAEnemy/SandSifterHurt",
                DeathSound = "event:/AAEnemy/SandSifterDeath",
                UnitTypes = ["Robot"],
                CombatEnterEffects = [Effects.GenerateEffect(ScrapVariantHandler), Effects.GenerateEffect(HealthRandom, 1, Targeting.Slot_SelfSlot)],
            };
            scraps.PrepareEnemyPrefab("Assets/Apocrypha_Enemies/Assessor_Boss/AssessorScrap_Enemy.prefab", AApocrypha.assetBundle, null);

            SpecificEnemiesTargeting BossTarget = ScriptableObject.CreateInstance<SpecificEnemiesTargeting>();
            BossTarget._enemies = ["AmalgamatedAssessor_BOSS"];
            BossTarget.targetUnitAllySlots = true;
            BossTarget.slotOffsets = [0];

            SpecificEnemiesTargeting AllScraps = ScriptableObject.CreateInstance<SpecificEnemiesTargeting>();
            AllScraps._enemies = ["ShatteredScrapsSummon_EN"];
            AllScraps.targetUnitAllySlots = true;
            AllScraps.slotOffsets = [0];

            SpecificEnemiesTargeting OpposingAllScraps = ScriptableObject.CreateInstance<SpecificEnemiesTargeting>();
            OpposingAllScraps._enemies = ["ShatteredScrapsSummon_EN"];
            OpposingAllScraps.targetUnitAllySlots = false;
            OpposingAllScraps.slotOffsets = [0];

            SpecificEnemiesTargeting AllScrapsAndSelf = ScriptableObject.CreateInstance<SpecificEnemiesTargeting>();
            AllScrapsAndSelf._enemies = ["ShatteredScrapsSummon_EN", "AmalgamatedAssessor_BOSS"];
            AllScrapsAndSelf.targetUnitAllySlots = true;
            AllScrapsAndSelf.slotOffsets = [0];

            ExtraVariableForNext_SVEffect GetTargeterValue = ScriptableObject.CreateInstance<ExtraVariableForNext_SVEffect>();
            GetTargeterValue.m_unitStoredDataID = "TargeterStoredValue";

            FirstTargetExtraVariableForNext_SVEffect GetTargetTargeterValue = ScriptableObject.CreateInstance<FirstTargetExtraVariableForNext_SVEffect>();
            GetTargetTargeterValue.m_unitStoredDataID = "TargeterStoredValue";

            CasterStoreValueSetterAdvancedEffect SetTargeter = ScriptableObject.CreateInstance<CasterStoreValueSetterAdvancedEffect>();
            SetTargeter.m_unitStoredDataID = "TargeterStoredValue";
            SetTargeter._ignoreIfContains = false;
            SetTargeter.usePreviousExitValue = true;

            OpponentByStoredValueTargeting TargeterTargeting = ScriptableObject.CreateInstance<OpponentByStoredValueTargeting>();
            TargeterTargeting._storedValueID = "TargeterStoredValue";
            TargeterTargeting.targetUnitAllySlots = false;
            TargeterTargeting.reduceByOne = true;

            OpponentByStoredValueTargeting TargeterTargetingSides = ScriptableObject.CreateInstance<OpponentByStoredValueTargeting>();
            TargeterTargetingSides._storedValueID = "TargeterStoredValue";
            TargeterTargetingSides.targetUnitAllySlots = false;
            TargeterTargetingSides.reduceByOne = true;
            TargeterTargetingSides._modifiers = [-1, 1];

            OpponentByStoredValueTargeting TargeterTargetingGroup = ScriptableObject.CreateInstance<OpponentByStoredValueTargeting>();
            TargeterTargetingGroup._storedValueID = "TargeterStoredValue";
            TargeterTargetingGroup.targetUnitAllySlots = false;
            TargeterTargetingGroup.reduceByOne = true;
            TargeterTargetingGroup._modifiers = [-1, 0, 1];

            PassivePopUpOnTargetEffect TargeterPopup = ScriptableObject.CreateInstance<PassivePopUpOnTargetEffect>();
            TargeterPopup._name = "Targeter";
            TargeterPopup._sprite = "IconTargeter";
            TargeterPopup._isUnitCharacter = false;

            AnimationVisualsEffect TargetAnim = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            TargetAnim._visuals = Visuals.Poke;
            TargetAnim._animationTarget = TargeterTargeting;

            TargetPerformEffectViaSubaction InheritTargeter = ScriptableObject.CreateInstance<TargetPerformEffectViaSubaction>();
            InheritTargeter.effects = [
                Effects.GenerateEffect(TargeterPopup, 1, Targeting.Slot_SelfSlot),
                Effects.GenerateEffect(GetTargetTargeterValue, 1, BossTarget),
                Effects.GenerateEffect(SetTargeter, 1, Targeting.Slot_SelfSlot),
            ];

            PerformEffectPassiveAbility assessorTargeterPassive = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
            assessorTargeterPassive.name = "AA_TargeterAssessor_PA";
            assessorTargeterPassive._passiveName = "Targeter";
            assessorTargeterPassive.m_PassiveID = "Targeter";
            assessorTargeterPassive.passiveIcon = ResourceLoader.LoadSprite("IconTargeter");
            assessorTargeterPassive._characterDescription = "might not work, also what'd you even DO with this?";
            assessorTargeterPassive._enemyDescription = "At the start of combat and at the end of the timeline, this enemy will remember the position of the party member with the highest current health.";
            assessorTargeterPassive._triggerOn = [TriggerCalls.TimelineEndReached, TriggerCalls.OnCombatStart];
            assessorTargeterPassive.doesPassiveTriggerInformationPanel = true;
            assessorTargeterPassive.effects =
            [
                Effects.GenerateEffect(ScriptableObject.CreateInstance<TargetGetSlotEffect>(), 1, Targeting.GenerateUnitTarget_Specific_Health(false, false, false, false)),
                Effects.GenerateEffect(SetTargeter, 1, Targeting.Slot_SelfSlot),
                Effects.GenerateEffect(TargetAnim),
                Effects.GenerateEffect(InheritTargeter, 1, AllScraps),
            ];
            Passives.AddCustomPassiveToPool("AA_TargeterAssessor_PA", "Targeter", assessorTargeterPassive);

            PerformEffectPassiveAbility scrapsTargeterPassive = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
            scrapsTargeterPassive.name = "AA_TargeterAssessorScrap_PA";
            scrapsTargeterPassive._passiveName = "Targeter";
            scrapsTargeterPassive.m_PassiveID = "Targeter";
            scrapsTargeterPassive.passiveIcon = ResourceLoader.LoadSprite("IconTargeter");
            scrapsTargeterPassive._characterDescription = "literally does nothing";
            scrapsTargeterPassive._enemyDescription = "This enemy inherits its Targeted position from the Amalgamated Assessor.";
            Passives.AddCustomPassiveToPool("AA_TargeterAssessorScrap_PA", "Targeter", scrapsTargeterPassive);

            assessor.AddPassives([Passives.Skittish, Passives.Unstable, Passives.GetCustomPassive("AA_TargeterAssessor_PA")]);
            scraps.AddPassives([Passives.Inanimate, Passives.GetCustomPassive("AA_TargeterAssessorScrap_PA")]);

            RemovePassiveEffect TargeterWipe = ScriptableObject.CreateInstance<RemovePassiveEffect>();
            TargeterWipe.m_PassiveID = "Targeter";

            AddPassiveEffect TargeterAdd = ScriptableObject.CreateInstance<AddPassiveEffect>();
            TargeterAdd._passiveToAdd = Passives.GetCustomPassive("AA_TargeterDuneThresher_PA");

            TargetPerformEffectViaSubaction TargeterModify = ScriptableObject.CreateInstance<TargetPerformEffectViaSubaction>();
            TargeterModify.effects =
            [
                Effects.GenerateEffect(TargeterWipe, 1, Targeting.Slot_SelfSlot),
                Effects.GenerateEffect(TargeterAdd, 1, Targeting.Slot_SelfSlot),
            ];

            assessor.CombatExitEffects = [Effects.GenerateEffect(TargeterModify, 1, AllScraps)];

            StatusEffect_Apply_Effect ScarsApply = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            ScarsApply._Status = StatusField.Scars;

            AnimationVisualsEffect RecyclingAnim = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            RecyclingAnim._visuals = Visuals.Crush;
            RecyclingAnim._animationTarget = AllScraps;

            AddEnemyAbilityFromStoredValueEffect RepositoryDeathHandler = ScriptableObject.CreateInstance<AddEnemyAbilityFromStoredValueEffect>();
            RepositoryDeathHandler._storedValueID = "RepositoryStoredValue";

            PerformEffectPassiveAbility repositoryPassive = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
            repositoryPassive.name = "AA_RepositoryAssessor_PA";
            repositoryPassive._passiveName = "Repository";
            repositoryPassive.m_PassiveID = "Repository";
            repositoryPassive.passiveIcon = ResourceLoader.LoadSprite("passive_repository");
            repositoryPassive._characterDescription = "git commit -m \"Fix: fixed a server crash caused by Sniper trying to eat his gun\"";
            repositoryPassive._enemyDescription = "This enemy contains one of the Amalgamated Assessor's abilities, which will be returned to it when this enemy dies.";
            repositoryPassive._triggerOn = [TriggerCalls.OnDeath];
            repositoryPassive.doesPassiveTriggerInformationPanel = true;
            repositoryPassive.effects =
            [
                Effects.GenerateEffect(RepositoryDeathHandler, 10, BossTarget),
            ];
            Passives.AddCustomPassiveToPool("AA_RepositoryAssessor_PA", "Repository", repositoryPassive);

            AddEnemyAbilityFromCasterEffect PassToScraps = ScriptableObject.CreateInstance<AddEnemyAbilityFromCasterEffect>();
            PassToScraps._abilityBlacklist = ["AApocrypha_AutomatedAmputation_A", "Automated Amputation"];
            PassToScraps._removeFromCaster = true;

            PerformEffectViaSubaction PassToScrapsSubaction = ScriptableObject.CreateInstance<PerformEffectViaSubaction>();
            PassToScrapsSubaction.effects = [Effects.GenerateEffect(PassToScraps, 1, AllScraps)];

            AddPassiveEffect RepositoryAdd = ScriptableObject.CreateInstance<AddPassiveEffect>();
            RepositoryAdd._passiveToAdd = Passives.GetCustomPassive("AA_RepositoryAssessor_PA");

            CasterFirstAbilityToStoredValueSetterEffect RepositoryValueInit = ScriptableObject.CreateInstance<CasterFirstAbilityToStoredValueSetterEffect>();
            RepositoryValueInit.m_unitStoredDataID = "RepositoryStoredValue";
            RepositoryValueInit._ignoreIfContains = true;

            TargetPerformEffectViaSubaction RepositoryApplySubaction = ScriptableObject.CreateInstance<TargetPerformEffectViaSubaction>();
            RepositoryApplySubaction.effects =
            [
                Effects.GenerateEffect(RepositoryAdd, 1, Targeting.Slot_SelfSlot),
                Effects.GenerateEffect(RepositoryValueInit, 1, Targeting.Slot_SelfSlot, PreviousGenerator(true, 1)),
            ];

            SpawnEnemyAnywhereEffect ShedScraps = ScriptableObject.CreateInstance<SpawnEnemyAnywhereEffect>();
            ShedScraps.enemy = scraps.enemy;
            ShedScraps._spawnTypeID = CombatType_GameIDs.Spawn_Basic.ToString();

            PerformEffectViaSubaction AmputationSubactionContainer = ScriptableObject.CreateInstance<PerformEffectViaSubaction>();
            AmputationSubactionContainer.effects =
            [
                Effects.GenerateEffect(PassToScrapsSubaction),
                Effects.GenerateEffect(RepositoryApplySubaction, 1, AllScraps),
            ];

            TryUnlockAchievementEffect AssessorBonusUnlock = ScriptableObject.CreateInstance<TryUnlockAchievementEffect>();
            AssessorBonusUnlock._unlockID = "ComedyAssessorRecycling";

            Ability hydraulicpress = new Ability("Hydraulic Press", "AApocrypha_HydraulicPress_A")
            {
                Description = "Deal an Agonizing amount of damage to this enemy's Targeted position if it is not Opposing this enemy.",
                Cost = [Pigments.Grey, Pigments.Grey],
                Visuals = Visuals.Crush,
                AnimationTarget = TargeterTargeting,
                Effects =
                    [
                        Effects.GenerateEffect(GetTargeterValue, 1, Targeting.Slot_SelfSlot),
                        Effects.GenerateEffect(ScriptableObject.CreateInstance<TargetPreviousValueComparatorEffect>(), 1, Targeting.GenerateBigUnitSlotTarget([0], null)),
                        Effects.GenerateEffect(GetTargeterValue, 1, Targeting.Slot_SelfSlot),
                        Effects.GenerateEffect(ScriptableObject.CreateInstance<TargetPreviousValueComparatorEffect>(), 1, Targeting.GenerateBigUnitSlotTarget([1], null)),
                        Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 8, TargeterTargeting, Effects.CheckMultiplePreviousEffectsCondition([false, false], [1, 3]))
                    ],
                Rarity = Rarity.Common,
                Priority = Priority.Fast,
            };
            hydraulicpress.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Misc_Hidden)]);
            hydraulicpress.AddIntentsToTarget(TargeterTargeting, [nameof(IntentType_GameIDs.Damage_7_10)]);

            Ability ionizingfumes = new Ability("Ionizing Fumes", "AApocrypha_IonizingFumes_A")
            {
                Description = "Apply 1 Scar to the Opposing party members. Move all Opposing party members to the Left or Right.",
                Cost = [Pigments.Grey, Pigments.Grey],
                Visuals = ITAVisuals.Stank,
                AnimationTarget = Targeting.Slot_Front,
                Effects =
                    [
                        Effects.GenerateEffect(ScarsApply, 1, Targeting.Slot_Front),
                        Effects.GenerateEffect(ScriptableObject.CreateInstance<SwapToSidesEffect>(), 1, Targeting.Slot_Front),
                    ],
                Rarity = Rarity.Common,
                Priority = Priority.Fast,
            };
            ionizingfumes.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Status_Scars), nameof(IntentType_GameIDs.Swap_Sides)]);

            Ability frayedwires = new Ability("Frayed Wires", "AApocrypha_FrayedWires_A")
            {
                Description = "Deal a Painful amount of damage to the Left and Right party members. If either party member is in this enemy's Targeted position, the damage is instead Agonizing.",
                Cost = [Pigments.Grey, Pigments.Grey],
                Visuals = CustomVisuals.StaticVisualsSO,
                AnimationTarget = Targeting.Slot_OpponentSides,
                Effects =
                    [
                        Effects.GenerateEffect(GetTargeterValue, 1, Targeting.Slot_SelfSlot),
                        Effects.GenerateEffect(ScriptableObject.CreateInstance<TargetPreviousValueComparatorEffect>(), 1, Targeting.Slot_OpponentLeft),
                        Effects.GenerateEffect(GetTargeterValue, 1, Targeting.Slot_SelfSlot),
                        Effects.GenerateEffect(ScriptableObject.CreateInstance<TargetPreviousValueComparatorEffect>(), 1, Targeting.Slot_OpponentRight),
                        Effects.GenerateEffect(ScriptableObject.CreateInstance<ExtraVariableForNextEffect>(), 1, Targeting.Slot_SelfSlot, Effects.CheckMultiplePreviousEffectsCondition([false, false], [1, 3])),
                        Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 4, Targeting.Slot_OpponentSides, PreviousGenerator(true, 1)),
                        Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 8, Targeting.Slot_OpponentSides, PreviousGenerator(false, 2)),
                    ],
                Rarity = Rarity.Common,
                Priority = Priority.Fast,
            };
            frayedwires.AddIntentsToTarget(Targeting.Slot_OpponentSides, [nameof(IntentType_GameIDs.Damage_3_6)]);
            frayedwires.AddIntentsToTarget(TargeterTargeting, [nameof(IntentType_GameIDs.Misc_Hidden)]);
            frayedwires.AddIntentsToTarget(Targeting.Slot_OpponentSides, [nameof(IntentType_GameIDs.Damage_7_10)]);

            Ability factoryreset = new Ability("Factory Reset", "AApocrypha_FactoryReset_A")
            {
                Description = "Deal a Little damage to all party members Opposing Shattered Scraps. Destroy all Shattered Scraps.",
                Cost = [Pigments.Grey, Pigments.Grey],
                Visuals = ITAVisuals.Explode,
                AnimationTarget = OpposingAllScraps,
                Effects =
                    [
                        Effects.GenerateEffect(ScriptableObject.CreateInstance<CheckHasUnitThresholdEffect>(), 3, AllScraps),
                        Effects.GenerateEffect(AssessorBonusUnlock, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 1)),
                        Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 2, OpposingAllScraps),
                        Effects.GenerateEffect(RecyclingAnim),
                        Effects.GenerateEffect(ScriptableObject.CreateInstance<DirectDeathEffect>(), 1, AllScraps),
                    ],
                Rarity = Rarity.Rare,
                Priority = Priority.VerySlow,
            };
            factoryreset.AddIntentsToTarget(OpposingAllScraps, [nameof(IntentType_GameIDs.Damage_1_2)]);
            factoryreset.AddIntentsToTarget(AllScraps, [nameof(IntentType_GameIDs.Damage_Death)]);

            Ability automatedamputation = new Ability("Automated Amputation", "AApocrypha_AutomatedAmputation_A")
            {
                Description = "Shed a pile of Shattered Scraps. Shed an additional pile if there are no other enemies present.\nThen, for each pile of Shattered Scraps in combat without an ability, remove one of this enemy's abilities, excluding Automated Amputation, and pass it to the Shattered Scraps.",
                Cost = [Pigments.Grey, Pigments.Grey],
                Visuals = Visuals.Crush,
                AnimationTarget = Targeting.Slot_SelfAll,
                Effects =
                    [
                        Effects.GenerateEffect(ScriptableObject.CreateInstance<CheckHasUnitEffect>(), 1, AllScraps),
                        Effects.GenerateEffect(ShedScraps, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 1)),
                        Effects.GenerateEffect(ShedScraps, 1, Targeting.Slot_SelfSlot),
                        Effects.GenerateEffect(AmputationSubactionContainer),
                    ],
                Rarity = Rarity.Common,
                Priority = Priority.ExtremelySlow,
            };
            automatedamputation.AddIntentsToTarget(Targeting.Slot_SelfAll, [nameof(IntentType_GameIDs.Other_Spawn), nameof(IntentType_GameIDs.Misc)]);
            automatedamputation.AddIntentsToTarget(AllScraps, [nameof(IntentType_GameIDs.Misc)]);

            assessor.AddEnemyAbilities([
                automatedamputation.GenerateEnemyAbility(true),
                hydraulicpress.GenerateEnemyAbility(true),
                ionizingfumes.GenerateEnemyAbility(true),
                frayedwires.GenerateEnemyAbility(true),
                factoryreset.GenerateEnemyAbility(true),
            ]);

            assessor.AddEnemy(true, false, false);

            scraps.AddEnemy(false, false, false);

            BackwardsUnlockCompatibility.TryLockItemBehindAchievement("AssessorBoss_ACH", "ThresherEye_TW");
            UnlockableModData assessorBossUnlockData = new UnlockableModData("AmalgamatedAssessor_BOSS");
            assessorBossUnlockData.hasModdedAchievementUnlock = true;
            assessorBossUnlockData.moddedAchievementID = "AssessorBoss_ACH";
            assessorBossUnlockData.hasItemUnlock = true;
            assessorBossUnlockData.items = ["ThresherEye_TW"];

            ListedUnlockCheck assessorUnlockCheck = ScriptableObject.CreateInstance<ListedUnlockCheck>();
            assessorUnlockCheck.unlockID = "AmalgamatedAssessor_BOSS";
            assessorUnlockCheck.unlockData = assessorBossUnlockData;
            Unlocks.AddUnlock_BeatBoss(assessorUnlockCheck);

            ModdedAchievements assessorBossAchievement = new ModdedAchievements("The Contraption", "Destroy the Amalgamated Assessor.", ResourceLoader.LoadSprite("AchievementBossAssessor", null, 32, null), "AssessorBoss_ACH");
            assessorBossAchievement.AddNewAchievementToInGameCategory(AchievementCategoryIDs.BossesTitleLabel);

            string[] assessorTips = 
            [
                "Always keep an eye on where the Assessor's aiming. It starts at the position of the healthiest party member, but doesn't move with them.",
                "The more of those scrap heaps are on the field, the more dangerous everything gets. Don't let it get out of hand.",
                "It might be a bit dangerous, but why not keep some of those scrap heaps around next time? Sure they explode, but you need the pigment.", // thanks stoat
            ];
            BrutalAPI.Dialogues.AddCustom_GameOver_BossLines("Assessor_BOSS", assessorTips);
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
