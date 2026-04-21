using System;
using System.Collections.Generic;
using System.Text;
using A_Apocrypha.Assets;
using A_Apocrypha.CustomOther;

namespace A_Apocrypha.Enemies
{
    public class PhobiaEnemies
    {
        public static void Add()
        {
            MutedMusicHandlerEffect MuteToggleOn = ScriptableObject.CreateInstance<MutedMusicHandlerEffect>();
            MuteToggleOn.Add = true;

            MutedMusicHandlerEffect MuteToggleReset = ScriptableObject.CreateInstance<MutedMusicHandlerEffect>();
            MuteToggleReset.ResetEffect = true;

            SetScreenFadeEffect BlackoutOn = ScriptableObject.CreateInstance<SetScreenFadeEffect>();
            BlackoutOn._fadeToBlack = true;

            SetScreenFadeEffect BlackoutOff = ScriptableObject.CreateInstance<SetScreenFadeEffect>();
            BlackoutOff._fadeToBlack = false;

            PlayOneShotEffect CutOutSFX = ScriptableObject.CreateInstance<PlayOneShotEffect>();
            CutOutSFX.soundPath = "event:/AASFX/CutOut_SFX";

            PlayOneShotEffect CutInSFX = ScriptableObject.CreateInstance<PlayOneShotEffect>();
            CutInSFX.soundPath = "event:/AASFX/CutIn_SFX";

            PerformEffectViaSubaction BlackoutOffDelay = ScriptableObject.CreateInstance<PerformEffectViaSubaction>();
            BlackoutOffDelay.effects = [
                Effects.GenerateEffect(MuteToggleReset, 0),
                Effects.GenerateEffect(CutInSFX, 0),
                Effects.GenerateEffect(BlackoutOff, 0),
            ];

            AnimationVisualsEffect DelayAnim = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            DelayAnim._visuals = CustomVisuals.Nothing;
            DelayAnim._animationTarget = Targeting.Slot_SelfSlot;

            AnimationVisualsEffect DelayScreamAnim = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            DelayScreamAnim._visuals = CustomVisuals.NothingScream;
            DelayScreamAnim._animationTarget = Targeting.Slot_SelfSlot;

            PerformEffectViaSubaction BlackoutOffDelay2 = ScriptableObject.CreateInstance<PerformEffectViaSubaction>();
            BlackoutOffDelay2.effects = [
                Effects.GenerateEffect(DelayAnim, 0),
                Effects.GenerateEffect(BlackoutOffDelay, 0),
            ];

            PerformEffectViaSubaction BlackOutOffDelayReroll = ScriptableObject.CreateInstance<PerformEffectViaSubaction>();
            BlackOutOffDelayReroll.effects = 
            [
                Effects.GenerateEffect(ScriptableObject.CreateInstance<RerollCombatEnvEffect>()),
                Effects.GenerateEffect(BlackoutOffDelay, 0),
            ];

            QueueTimelineAbilityByNameEffect QueueSwitch = ScriptableObject.CreateInstance<QueueTimelineAbilityByNameEffect>();
            QueueSwitch._abilityName = "Recontextualize";

            string redID = ColorUtility.ToHtmlStringRGB(Color.red);
            int phobiaHealth = 50;
            ManaColorSO phobiaHealthColor = Pigments.Red;
            string hurtSound = "event:/AAEnemy/Phobias/PhobiasRoar";
            string deathSound = "event:/AASFX/Nothing_SFX";

            PerformDoubleEffectPassiveAbility blackoutPassive = ScriptableObject.CreateInstance<PerformDoubleEffectPassiveAbility>();
            blackoutPassive.name = "AA_Blackout_PA";
            blackoutPassive._passiveName = "Blackout";
            blackoutPassive.m_PassiveID = "Blackout";
            blackoutPassive.passiveIcon = ResourceLoader.LoadSprite("IconBlackout");
            blackoutPassive._characterDescription = "This party member's actions are too horrifying to be witnessed.";
            blackoutPassive._enemyDescription = "This enemy's actions are too horrifying to be witnessed.";
            blackoutPassive._triggerOn = [TriggerCalls.OnDamaged];
            blackoutPassive.doesPassiveTriggerInformationPanel = false;
            blackoutPassive.effects = [
                Effects.GenerateEffect(BlackoutOn, 0, Targeting.Slot_SelfSlot, ScriptableObject.CreateInstance<FuckingDeadEffectCondition>()),
                Effects.GenerateEffect(MuteToggleOn, 0, Targeting.Slot_SelfSlot, ScriptableObject.CreateInstance<FuckingDeadEffectCondition>()),
                Effects.GenerateEffect(CutOutSFX, 0, Targeting.Slot_SelfSlot, ScriptableObject.CreateInstance<FuckingDeadEffectCondition>()),
            ];
            blackoutPassive._secondTriggerOn = [TriggerCalls.OnDeath];
            blackoutPassive._secondDoesPerformPopUp = false;
            blackoutPassive._secondEffects = [Effects.GenerateEffect(BlackoutOffDelay2)];

            PerformEffectPassiveAbility patientPassive = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
            patientPassive.name = "AA_PatientPhobias_PA";
            patientPassive._passiveName = "Recontextualize";
            patientPassive.m_PassiveID = "Patient";
            patientPassive.passiveIcon = ResourceLoader.LoadSprite("IconPatient");
            patientPassive._characterDescription = "party members can't queue into the timeline, ya know - maybe it could work like windup, though...";
            patientPassive._enemyDescription = "When the player turn ends, this enemy queues the ability \"Recontextualize\".";
            patientPassive._triggerOn = [TriggerCalls.OnPlayerTurnEnd_ForEnemy];
            patientPassive.effects = [
                Effects.GenerateEffect(QueueSwitch, 1, Targeting.Slot_SelfSlot),
            ];

            Enemy fearoffear = new Enemy("<color=#" + redID + ">Phobophobia</color>", "Phobia_Phobias_EN")
            {
                Health = phobiaHealth,
                HealthColor = phobiaHealthColor,
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("PhobiaVerticalTimeline", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("AnomalyDead", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("PhobiaVerticalTimeline", new Vector2(0.5f, 0f), 32),
                DamageSound = hurtSound,
                DeathSound = deathSound,
                UnitTypes = ["Phobia"],
            };
            fearoffear.PrepareEnemyPrefab("Assets/Apocrypha_Enemies/Phobias_Enemy/Phobophobia_Enemy.prefab", AApocrypha.assetBundle, AApocrypha.assetBundle.LoadAsset<GameObject>("Assets/Apocrypha_Enemies/Phobias_Enemy/Phobia_Giblets.prefab").GetComponent<ParticleSystem>());
            
            Enemy fearofbeingwatched = new Enemy("<color=#" + redID + ">Scopophobia</color>", "Phobia_Eyes_EN")
            {
                Health = phobiaHealth,
                HealthColor = phobiaHealthColor,
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("ScopophobiaTimeline", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("AnomalyDead", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("ScopophobiaTimeline", new Vector2(0.5f, 0f), 32),
                DamageSound = hurtSound,
                DeathSound = deathSound,
                UnitTypes = ["Phobia"],
            };
            fearofbeingwatched.PrepareEnemyPrefab("Assets/Apocrypha_Enemies/Phobias_Enemy/Scopophobia_Enemy.prefab", AApocrypha.assetBundle, AApocrypha.assetBundle.LoadAsset<GameObject>("Assets/Apocrypha_Enemies/Phobias_Enemy/Phobia_Giblets.prefab").GetComponent<ParticleSystem>());

            Enemy fearofwords = new Enemy("<color=#" + redID + ">Logophobia</color>", "Phobia_Words_EN")
            {
                Health = phobiaHealth,
                HealthColor = phobiaHealthColor,
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("LogophobiaTimeline", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("AnomalyDead", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("LogophobiaTimeline", new Vector2(0.5f, 0f), 32),
                DamageSound = hurtSound,
                DeathSound = deathSound,
                UnitTypes = ["Phobia"],
            };
            fearofwords.PrepareEnemyPrefab("Assets/Apocrypha_Enemies/Phobias_Enemy/Logophobia_Enemy.prefab", AApocrypha.assetBundle, AApocrypha.assetBundle.LoadAsset<GameObject>("Assets/Apocrypha_Enemies/Phobias_Enemy/Phobia_Giblets.prefab").GetComponent<ParticleSystem>());

            Enemy fearofthedark = new Enemy("<color=#" + redID + ">Nyctophobia</color>", "Phobia_Darkness_EN")
            {
                Health = phobiaHealth,
                HealthColor = phobiaHealthColor,
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("NyctophobiaTimeline", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("AnomalyDead", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("NyctophobiaTimeline", new Vector2(0.5f, 0f), 32),
                DamageSound = hurtSound,
                DeathSound = deathSound,
                UnitTypes = ["Phobia"],
            };
            fearofthedark.PrepareEnemyPrefab("Assets/Apocrypha_Enemies/Phobias_Enemy/Nyctophobia_Enemy.prefab", AApocrypha.assetBundle, AApocrypha.assetBundle.LoadAsset<GameObject>("Assets/Apocrypha_Enemies/Phobias_Enemy/Phobia_Giblets.prefab").GetComponent<ParticleSystem>());
            
            Enemy fearofdeath = new Enemy("<color=#" + redID + ">Thanatophobia</color>", "Phobia_Death_EN")
            {
                Health = phobiaHealth,
                HealthColor = phobiaHealthColor,
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("NecrophobiaTimeline", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("AnomalyDead", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("NecrophobiaTimeline", new Vector2(0.5f, 0f), 32),
                DamageSound = hurtSound,
                DeathSound = deathSound,
                UnitTypes = ["Phobia"],
            };
            fearofdeath.PrepareEnemyPrefab("Assets/Apocrypha_Enemies/Phobias_Enemy/Necrophobia_Enemy.prefab", AApocrypha.assetBundle, AApocrypha.assetBundle.LoadAsset<GameObject>("Assets/Apocrypha_Enemies/Phobias_Enemy/Phobia_Giblets.prefab").GetComponent<ParticleSystem>());
            
            Enemy rarefearofdeathfake = new Enemy("<color=#" + redID + ">Thanatophobia</color>", "Phobia_Rare_DeathFake_EN")
            {
                Health = phobiaHealth,
                HealthColor = phobiaHealthColor,
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("NecrophobiaTimeline", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("AnomalyDead", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("NecrophobiaTimeline", new Vector2(0.5f, 0f), 32),
                DamageSound = hurtSound,
                DeathSound = deathSound,
                UnitTypes = ["Phobia"],
            };
            rarefearofdeathfake.PrepareEnemyPrefab("Assets/Apocrypha_Enemies/Phobias_Enemy/FakeoutPhobia_Enemy.prefab", AApocrypha.assetBundle, AApocrypha.assetBundle.LoadAsset<GameObject>("Assets/Apocrypha_Enemies/Phobias_Enemy/Phobia_Giblets.prefab").GetComponent<ParticleSystem>());
            
            Enemy rarefearoflongwords = new Enemy("<color=#" + redID + ">Hippopotomonstrosesquipedaliophobia</color>", "Phobia_Rare_LongWords_EN")
            {
                Health = phobiaHealth,
                HealthColor = phobiaHealthColor,
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("LogophobiaTimeline", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("AnomalyDead", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("LogophobiaTimeline", new Vector2(0.5f, 0f), 32),
                DamageSound = hurtSound,
                DeathSound = deathSound,
                UnitTypes = ["Phobia"],
            };
            rarefearoflongwords.PrepareEnemyPrefab("Assets/Apocrypha_Enemies/Phobias_Enemy/LongWordPhobia_Enemy.prefab", AApocrypha.assetBundle, AApocrypha.assetBundle.LoadAsset<GameObject>("Assets/Apocrypha_Enemies/Phobias_Enemy/Phobia_Giblets.prefab").GetComponent<ParticleSystem>());
            
            CasterRandomTransformationNotCasterWithRarePoolEffect PhobiaReroll = ScriptableObject.CreateInstance<CasterRandomTransformationNotCasterWithRarePoolEffect>();
            PhobiaReroll._maintainMaxHealth = false;
            PhobiaReroll._fullyHeal = false;
            PhobiaReroll._currentToMaxHealth = false;
            PhobiaReroll._maintainTimelineAbilities = true;
            PhobiaReroll._rarityPercentage = 99;
            PhobiaReroll._possibleTransformations =
            [
                new TransformOption(fearofbeingwatched.enemy),
                new TransformOption(fearofwords.enemy),
                new TransformOption(fearofthedark.enemy),
                new TransformOption(fearofdeath.enemy),
            ];
            PhobiaReroll._possibleRareTransformations =
            [
                new TransformOption(rarefearofdeathfake.enemy),
                new TransformOption(rarefearoflongwords.enemy),
            ];

            TargetPerformEffectViaSubaction RerollThem = ScriptableObject.CreateInstance<TargetPerformEffectViaSubaction>();
            RerollThem.effects = [Effects.GenerateEffect(PhobiaReroll)];

            SpecificEnemiesTargeting AllOtherPhobias = ScriptableObject.CreateInstance<SpecificEnemiesTargeting>();
            AllOtherPhobias.slotOffsets = [0];
            AllOtherPhobias.targetUnitAllySlots = true;
            AllOtherPhobias._excludeCaster = true;
            AllOtherPhobias._enemies =
            [
                "Phobia_Phobias_EN",
                "Phobia_Eyes_EN",
                "Phobia_Words_EN",
                "Phobia_Darkness_EN",
                "Phobia_Death_EN",
                "Phobia_Rare_DeathFake_EN",
                "Phobia_Rare_LongWords_EN",
            ];

            SpecificEnemiesTargeting AllPhobias = ScriptableObject.CreateInstance<SpecificEnemiesTargeting>();
            AllPhobias.slotOffsets = [0];
            AllPhobias.targetUnitAllySlots = true;
            AllPhobias._excludeCaster = false;
            AllPhobias._enemies =
            [
                "Phobia_Phobias_EN",
                "Phobia_Eyes_EN",
                "Phobia_Words_EN",
                "Phobia_Darkness_EN",
                "Phobia_Death_EN",
                "Phobia_Rare_DeathFake_EN",
                "Phobia_Rare_LongWords_EN",
            ];

            Ability rerollAbility = new Ability("Recontextualize", "AApocrypha_PhobiasReroll_A")
            {
                Description = "Transform into a random other Phobia, maintaining current health." +
                "\n<color=#" + redID + ">\"The only constant is fear.\"</color>",
                Cost = [Pigments.Red, Pigments.Red, Pigments.Red],
                //Visuals = CustomVisuals.Nothing,
                //AnimationTarget = Targeting.Slot_SelfSlot,
                Effects =
                [
                    Effects.GenerateEffect(BlackoutOn, 0),
                    Effects.GenerateEffect(MuteToggleOn, 0),
                    Effects.GenerateEffect(CutOutSFX, 0),
                    Effects.GenerateEffect(PhobiaReroll),
                    Effects.GenerateEffect(DelayAnim),
                    Effects.GenerateEffect(BlackOutOffDelayReroll, 0),
                ],
                Rarity = Rarity.ImpossibleNoReroll,
                Priority = Priority.CreateAndAddCustomPriorityToPool("AA_PhobiasStupendouslySlow", -10),
            };
            rerollAbility.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.PA_Confusion)]);

            PerformEffectPassiveAbility transformPassive = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
            transformPassive.name = "AA_HitTransformPhobias_PA";
            transformPassive._passiveName = "Overdose";
            transformPassive.m_PassiveID = "HitTransformPhobias";
            transformPassive.passiveIcon = ResourceLoader.LoadSprite("OverdosePassive");
            transformPassive._characterDescription = "On being directly damaged, no";
            transformPassive._enemyDescription = "On being directly damaged, transform into a random other Phobia.";
            transformPassive._triggerOn = [TriggerCalls.OnDirectDamaged];
            transformPassive.conditions = [ScriptableObject.CreateInstance<UnitAliveEffectorCondition>()];
            transformPassive.effects = [
                    Effects.GenerateEffect(BlackoutOn, 0),
                    Effects.GenerateEffect(MuteToggleOn, 0),
                    Effects.GenerateEffect(CutOutSFX, 0),
                    Effects.GenerateEffect(PhobiaReroll),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<ReloadTimelineEffect>()),
                    Effects.GenerateEffect(BlackOutOffDelayReroll, 0),
            ];

            Ability phobiasFiller = new Ability("Anticipation", "AApocrypha_PhobiaPhobiasSorryNothing_A")
            {
                Description = "<color=#" + redID + ">\"You have nothing to fear but fear itself.\"</color>",
                Cost = [Pigments.Red, Pigments.Red, Pigments.Red],
                //Visuals = CustomVisuals.Nothing,
                //AnimationTarget = Targeting.Slot_Front,
                Effects =
                [
                    Effects.GenerateEffect(BlackoutOn, 0),
                    Effects.GenerateEffect(MuteToggleOn, 0),
                    Effects.GenerateEffect(CutOutSFX, 0),
                    Effects.GenerateEffect(DelayAnim),
                    Effects.GenerateEffect(BlackoutOffDelay, 0),
                ],
                Rarity = Rarity.ExtremelyCommon,
                Priority = Priority.Normal,
            };
            phobiasFiller.AddIntentsToTarget(Targeting.Slot_SelfSlot, ["AA_Nothing"]);

            AddPassiveEffect Gouge = ScriptableObject.CreateInstance<AddPassiveEffect>();
            Gouge._passiveToAdd = Passives.GetCustomPassive("Gouged_PA");

            GenerateColorManaPerTargetEffect TheyMakeBlue = ScriptableObject.CreateInstance<GenerateColorManaPerTargetEffect>();
            TheyMakeBlue.mana = Pigments.Blue;

            CheckPassiveAbilityEffect IsInanimate = ScriptableObject.CreateInstance<CheckPassiveAbilityEffect>();
            IsInanimate.m_PassiveID = Passives.Inanimate.m_PassiveID;

            StatusEffect_Apply_Effect RupturedApply = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            RupturedApply._Status = StatusField.Ruptured;

            Ability lookerEyeSteal = new Ability("Stop Staring", "AApocrypha_PhobiaEyesEyeSteal_A")
            {
                Description = "Deal an Agonizing amount of damage to the Opposing party member, apply 2 Ruptured to them and attempt to gouge out their eyes." +
                "\nIf their eyes were successfully removed, produce 3 blue pigment." +
                "\n<color=#" + redID + ">\"Stop it! Stop looking at me!!\"</color>",
                Cost = [Pigments.Red, Pigments.Red, Pigments.Red],
                //Visuals = CustomVisuals.Nothing,
                //AnimationTarget = Targeting.Slot_Front,
                Effects =
                [
                    Effects.GenerateEffect(BlackoutOn, 0),
                    Effects.GenerateEffect(MuteToggleOn, 0),
                    Effects.GenerateEffect(CutOutSFX, 0),
                    Effects.GenerateEffect(DelayScreamAnim),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 10, Targeting.Slot_Front),
                    Effects.GenerateEffect(RupturedApply, 2, Targeting.Slot_Front),
                    Effects.GenerateEffect(BlackoutOffDelay, 0),
                    Effects.GenerateEffect(IsInanimate, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(Gouge, 1, Targeting.Slot_Front, Effects.CheckPreviousEffectCondition(false, 1)),
                    Effects.GenerateEffect(TheyMakeBlue, 3, Targeting.Slot_Front, Effects.CheckPreviousEffectCondition(true, 1)),
                ],
                Rarity = Rarity.ExtremelyCommon,
                Priority = Priority.Normal,
            };
            lookerEyeSteal.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Damage_7_10), "AA_AddPassive", nameof(IntentType_GameIDs.Mana_Generate)]);

            StatusEffect_Apply_Effect CursedApply = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            CursedApply._Status = StatusField.Cursed;

            StatusEffect_ApplyByPrevious_Effect ScarsApplyBy = ScriptableObject.CreateInstance<StatusEffect_ApplyByPrevious_Effect>();
            ScarsApplyBy._Status = StatusField.Scars;

            Ability deathMemento = new Ability("Memento Mori", "AApocrypha_PhobiaDeathMementoMori_A")
            {
                Description = "Curse the Opposing party member." +
                "\nConsume up to 5 pigment of this enemy's health color and apply an equivalent amount of Scars to the Opposing party member." +
                "\n<color=#" + redID + ">\"Remember that you will die.\"</color>",
                Cost = [Pigments.Red, Pigments.Red, Pigments.Red],
                //Visuals = CustomVisuals.Nothing,
                //AnimationTarget = Targeting.Slot_Front,
                Effects =
                [
                    Effects.GenerateEffect(BlackoutOn, 0),
                    Effects.GenerateEffect(MuteToggleOn, 0),
                    Effects.GenerateEffect(CutOutSFX, 0),
                    Effects.GenerateEffect(DelayAnim),
                    Effects.GenerateEffect(CursedApply, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<ConsumeCasterColorManaEffect>(), 5, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(ScarsApplyBy, 1, Targeting.Slot_Front, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(BlackoutOffDelay, 0),
                ],
                Rarity = Rarity.ExtremelyCommon,
                Priority = Priority.Normal,
            };
            deathMemento.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Status_Cursed)]);
            deathMemento.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Mana_Consume)]);
            deathMemento.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Status_Scars)]);

            Ability wordsCutting = new Ability("Cutting Remarks", "AApocrypha_PhobiaWordsCuttingWords_A")
            {
                Description = "Deal a Little damage to the Left, Right and Opposing party members and make each of them generate 2 pigment of this enemy's health color." +
                "\n<color=#" + redID + ">\"...But words hurt most of all.\"</color>",
                Cost = [Pigments.Red, Pigments.Red, Pigments.Red],
                //Visuals = CustomVisuals.Nothing,
                //AnimationTarget = Targeting.Slot_Front,
                Effects =
                [
                    Effects.GenerateEffect(BlackoutOn, 0),
                    Effects.GenerateEffect(MuteToggleOn, 0),
                    Effects.GenerateEffect(CutOutSFX, 0),
                    Effects.GenerateEffect(DelayAnim),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 2, Targeting.Slot_FrontAndSides),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<GenerateCasterHealthColorManaPerTargetEffect>(), 2, Targeting.Slot_FrontAndSides),
                    Effects.GenerateEffect(BlackoutOffDelay, 0),
                ],
                Rarity = Rarity.ExtremelyCommon,
                Priority = Priority.Fast,
            };
            wordsCutting.AddIntentsToTarget(Targeting.Slot_FrontAndSides, [nameof(IntentType_GameIDs.Damage_1_2), nameof(IntentType_GameIDs.Mana_Generate)]);

            AddPassiveEffect Confuse = ScriptableObject.CreateInstance<AddPassiveEffect>();
            Confuse._passiveToAdd = Passives.Confusion;

            Ability darkHiding = new Ability("Lurker in Darkness", "AApocrypha_PhobiaDarknessHide_A")
            {
                Description = "Attempt to consume 5 pigment of this enemy's health color." +
                "\nIf this succeeds, apply Confusion to the Opposing party member and shuffle the positions of all Phobias." +
                "\n<color=#" + redID + ">\"Imagine the horrors that could lurk within your very home...\"</color>",
                Cost = [Pigments.Red, Pigments.Red, Pigments.Red],
                //Visuals = CustomVisuals.Nothing,
                //AnimationTarget = Targeting.Slot_Front,
                Effects =
                [
                    Effects.GenerateEffect(BlackoutOn, 0),
                    Effects.GenerateEffect(MuteToggleOn, 0),
                    Effects.GenerateEffect(CutOutSFX, 0),
                    Effects.GenerateEffect(DelayAnim),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<ConsumeCasterColorReturnIfAllEffect>(), 5, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(Confuse, 1, Targeting.Slot_Front, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<ShufflePositionsAmongTargetsEffect>(), 1, AllPhobias, Effects.CheckPreviousEffectCondition(true, 2)),
                    Effects.GenerateEffect(BlackoutOffDelay, 0),
                ],
                Rarity = Rarity.ExtremelyCommon,
                Priority = Priority.VeryFast,
            };
            darkHiding.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Mana_Consume), nameof(IntentType_GameIDs.Misc_Hidden)]);
            darkHiding.AddIntentsToTarget(Targeting.Slot_Front, ["AA_AddPassive"]);
            darkHiding.AddIntentsToTarget(Targeting.Unit_AllAllySlots, [nameof(IntentType_GameIDs.Swap_Mass)]);

            Ability rareDeathFakeout = new Ability("Unfounded Paranoia", "AApocrypha_PhobiaFakeDeathHahaGottem_A")
            {
                Description = "<color=#" + redID + ">\"Deal a Mortal amount of damage to all party members.\"</color>",
                Cost = [Pigments.Red, Pigments.Red, Pigments.Red],
                //Visuals = CustomVisuals.Nothing,
                //AnimationTarget = Targeting.Slot_Front,
                Effects =
                [
                    Effects.GenerateEffect(BlackoutOn, 0),
                    Effects.GenerateEffect(MuteToggleOn, 0),
                    Effects.GenerateEffect(CutOutSFX, 0),
                    Effects.GenerateEffect(DelayAnim),
                    Effects.GenerateEffect(DelayAnim),
                    Effects.GenerateEffect(DelayAnim),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<FleeTargetEffect>(), 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(BlackoutOffDelay, 0),
                ],
                Rarity = Rarity.ExtremelyCommon,
                Priority = Priority.Normal,
            };
            rareDeathFakeout.AddIntentsToTarget(Targeting.Unit_AllOpponents, [nameof(IntentType_GameIDs.Damage_21)]);

            Ability rareWordsMany = new Ability("Titin", "AApocrypha_PhobiaLongWordsOneDamage_A")
            {
                Description = "When this enemy begins performing this ability, it will try to check if the position that is opposite to this enemy is occupied by a party member present in Nowak's travelling party or summoned forth by other means, such as treasures or other enemies, whose health is greater than zero." +
                "\nIf a party member that fulfills all of the aforementioned criteria is present opposite this particular enemy after it has begun performing this ability, this enemy will then alter the numerical value of the health of the aforementioned opposing party member such that the difference between its current and original health values is equivalent to the first positive integer.",// +
                //"\n<color=#" + redID + ">\"Deal a Mortal amount of damage to all party members.\"</color>",
                Cost = [Pigments.Red, Pigments.Red, Pigments.Red],
                //Visuals = CustomVisuals.Nothing,
                //AnimationTarget = Targeting.Slot_Front,
                Effects =
                [
                    Effects.GenerateEffect(BlackoutOn, 0),
                    Effects.GenerateEffect(MuteToggleOn, 0),
                    Effects.GenerateEffect(CutOutSFX, 0),
                    Effects.GenerateEffect(DelayAnim),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(BlackoutOffDelay, 0),
                ],
                Rarity = Rarity.ExtremelyCommon,
                Priority = Priority.Fast,
            };
            rareWordsMany.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Misc_Hidden), nameof(IntentType_GameIDs.Misc_Hidden), nameof(IntentType_GameIDs.Misc_Hidden)]);

            DamageEffect DamageByPreviousKill = ScriptableObject.CreateInstance<DamageEffect>();
            DamageByPreviousKill._usePreviousExitValue = true;
            DamageByPreviousKill._returnKillAsSuccess = true;

            TryUnlockAchievementEffect ScrabbledAchievement = ScriptableObject.CreateInstance<TryUnlockAchievementEffect>();
            ScrabbledAchievement._unlockID = "TragedyPhobophobiaLongWords";

            Ability rareScrabbled = new Ability("Oxyphenbutazone", "AApocrypha_PhobiaLongWordsScrabbled_A")
            {
                Description = "Deal an amount of damage equal to the Scrabble score of the Opposing party member's name to the Opposing party member." +
                "If this damage kills, deal an amount of damage equal to the Scrabble score of this enemy's name to this enemy." +
                "\n<color=#" + redID + ">\"Lost? Check the Glossary.\"</color>",
                Cost = [Pigments.Red, Pigments.Red, Pigments.Red],
                //Visuals = CustomVisuals.Nothing,
                //AnimationTarget = Targeting.Slot_Front,
                Effects =
                [
                    Effects.GenerateEffect(BlackoutOn, 0),
                    Effects.GenerateEffect(MuteToggleOn, 0),
                    Effects.GenerateEffect(CutOutSFX, 0),
                    Effects.GenerateEffect(DelayAnim),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<ReturnTargetsScrabbleScoreEffect>(), 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(DamageByPreviousKill, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<ReturnTargetsScrabbleScoreEffect>(), 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(DamageByPreviousKill, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 2)),
                    Effects.GenerateEffect(ScrabbledAchievement, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 3)),
                    Effects.GenerateEffect(BlackoutOffDelay, 0),
                ],
                Rarity = Rarity.ExtremelyCommon,
                Priority = Priority.Fast,
            };
            rareScrabbled.AddIntentsToTarget(Targeting.Slot_Front, ["AA_Damage_Scrabble"]);
            rareScrabbled.AddIntentsToTarget(Targeting.Slot_SelfSlot, ["AA_Damage_Scrabble"]);

            BasePassiveAbilitySO[] phobiaPassives = [transformPassive, patientPassive, blackoutPassive];

            fearoffear.AddPassives(phobiaPassives);
            fearoffear.AddEnemyAbilities([
                phobiasFiller,
                rerollAbility,
            ]);
            fearoffear.AddEnemy(true, false, true);

            fearofbeingwatched.AddPassives(phobiaPassives);
            fearofbeingwatched.AddEnemyAbilities([
                lookerEyeSteal,
                rerollAbility,
            ]);
            fearofbeingwatched.AddEnemy(false, false, false);

            fearofwords.AddPassives(phobiaPassives);
            fearofwords.AddEnemyAbilities([
                wordsCutting,
                rerollAbility,
            ]);
            fearofwords.AddEnemy(false, false, false);

            fearofthedark.AddPassives(phobiaPassives);
            fearofthedark.AddEnemyAbilities([
                darkHiding,
                rerollAbility,
            ]);
            fearofthedark.AddEnemy(false, false, false);

            fearofdeath.AddPassives(phobiaPassives);
            fearofdeath.AddEnemyAbilities([
                deathMemento,
                rerollAbility,
            ]);
            fearofdeath.AddEnemy(false, false, false);

            rarefearofdeathfake.AddPassives(phobiaPassives);
            rarefearofdeathfake.AddEnemyAbilities([
                rareDeathFakeout,
                rerollAbility,
            ]);
            rarefearofdeathfake.AddEnemy(false, false, false);

            rarefearoflongwords.AddPassives(phobiaPassives);
            rarefearoflongwords.AddEnemyAbilities([
                rareScrabbled,
                rerollAbility,    
            ]);
            rarefearoflongwords.AddEnemy(false, false, false);
        }
    }
}
