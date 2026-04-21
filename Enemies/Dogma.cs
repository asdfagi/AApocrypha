using System;
using System.Collections.Generic;
using System.Text;
using A_Apocrypha.CustomOther;

namespace A_Apocrypha.Enemies
{
    public class Dogma
    {
        public static void Add()
        {
            DogmaMusicHandlerEffect MusicToggleReset = ScriptableObject.CreateInstance<DogmaMusicHandlerEffect>();
            MusicToggleReset.ResetEffect = true;

            DogmaMusicHandlerEffect MusicTogglePlus = ScriptableObject.CreateInstance<DogmaMusicHandlerEffect>();
            MusicTogglePlus.Add = true;

            DogmaMusicHandlerEffect MusicToggleMinus = ScriptableObject.CreateInstance<DogmaMusicHandlerEffect>();
            MusicToggleMinus.Add = false;

            SetCasterAnimationParameterEffect DogmaAdd = ScriptableObject.CreateInstance<SetCasterAnimationParameterEffect>();
            DogmaAdd._parameterName = "Dogma";
            DogmaAdd._parameterValue = 4;
            DogmaAdd._UsePrevious = true;

            SetCasterAnimationParameterEffect DogmaReset = ScriptableObject.CreateInstance<SetCasterAnimationParameterEffect>();
            DogmaReset._parameterName = "Dogma";
            DogmaReset._parameterValue = 0;
            DogmaReset._UsePrevious = false;

            CasterStoredValueChangeWithMaxEffect DogmaValueUp = ScriptableObject.CreateInstance<CasterStoredValueChangeWithMaxEffect>();
            DogmaValueUp.m_unitStoredDataID = "DogmaStoredValue";
            DogmaValueUp._minimumValue = 0;
            DogmaValueUp._maximumValue = 4;
            DogmaValueUp._exitValueIsChange = false;
            DogmaValueUp._increase = true;
            DogmaValueUp._randomBetweenPrevious = false;
            DogmaValueUp._usePreviousExitValue = false;

            CasterStoredValueChangeWithMaxEffect DogmaValueDown = ScriptableObject.CreateInstance<CasterStoredValueChangeWithMaxEffect>();
            DogmaValueDown.m_unitStoredDataID = "DogmaStoredValue";
            DogmaValueDown._minimumValue = 0;
            DogmaValueDown._maximumValue = 4;
            DogmaValueDown._exitValueIsChange = false;
            DogmaValueDown._increase = false;
            DogmaValueDown._randomBetweenPrevious = false;
            DogmaValueDown._usePreviousExitValue = false;

            CasterStoredValueSetEffect DogmaValueReset = ScriptableObject.CreateInstance<CasterStoredValueSetEffect>();
            DogmaValueReset._valueName = "DogmaStoredValue";

            GenerateColorsByListManaEffect AllBaseColors = ScriptableObject.CreateInstance<GenerateColorsByListManaEffect>();
            AllBaseColors._manaColors = [Pigments.Red, Pigments.Blue, Pigments.Yellow, Pigments.Purple];
            AllBaseColors.usePreviousExitValue = true;

            PreviousComparatorCheckEffect FivePlus = ScriptableObject.CreateInstance<PreviousComparatorCheckEffect>();
            FivePlus._atOrAbove = true;
            FivePlus._entryIsComparator = false;
            FivePlus._fixedComparator = 5;

            PreviousComparatorCheckEffect FourPlus = ScriptableObject.CreateInstance<PreviousComparatorCheckEffect>();
            FourPlus._atOrAbove = true;
            FourPlus._entryIsComparator = false;
            FourPlus._fixedComparator = 4;

            StatusEffect_Apply_Effect ScarsApply = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            ScarsApply._Status = StatusField.Scars;

            SpecificHealthColorCheckEffect IsBroken = ScriptableObject.CreateInstance<SpecificHealthColorCheckEffect>();
            IsBroken._color = LoadedDBsHandler.PigmentDB.GetPigment("Broken");

            SpecificHealthColorAllTargetsCheckEffect AllIsBroken = ScriptableObject.CreateInstance<SpecificHealthColorAllTargetsCheckEffect>();
            AllIsBroken._color = LoadedDBsHandler.PigmentDB.GetPigment("Broken");
            AllIsBroken._ignorePure = true;

            ChangeToRandomHealthColorEffect Break = ScriptableObject.CreateInstance<ChangeToRandomHealthColorEffect>();
            Break._healthColors = [LoadedDBsHandler.PigmentDB.GetPigment("Broken")];

            ChangeToRandomHealthColorEffect Mend = ScriptableObject.CreateInstance<ChangeToRandomHealthColorEffect>();
            Mend._healthColors = [Pigments.Red, Pigments.Blue, Pigments.Yellow, Pigments.Purple];

            GenerateColorManaEffect GiveBrokenPigment = ScriptableObject.CreateInstance<GenerateColorManaEffect>();
            GiveBrokenPigment.mana = LoadedDBsHandler.PigmentDB.GetPigment("Broken");

            GenerateColorManaEffect GiveRedPigment = ScriptableObject.CreateInstance<GenerateColorManaEffect>();
            GiveRedPigment.mana = Pigments.Red;

            GenerateColorManaEffect GiveBluePigment = ScriptableObject.CreateInstance<GenerateColorManaEffect>();
            GiveBluePigment.mana = Pigments.Blue;

            GenerateColorManaEffect GiveYellowPigment = ScriptableObject.CreateInstance<GenerateColorManaEffect>();
            GiveYellowPigment.mana = Pigments.Yellow;

            GenerateColorManaEffect GivePurplePigment = ScriptableObject.CreateInstance<GenerateColorManaEffect>();
            GivePurplePigment.mana = Pigments.Purple;

            ExtraVariableForNext_SVEffect DogmaGet = ScriptableObject.CreateInstance<ExtraVariableForNext_SVEffect>();
            DogmaGet.m_unitStoredDataID = "DogmaStoredValue";

            SpecificOpponentsByHealthColorTargeting partyBroken = ScriptableObject.CreateInstance<SpecificOpponentsByHealthColorTargeting>();
            partyBroken._color = LoadedDBsHandler.PigmentDB.GetPigment("Broken");
            partyBroken._contains = false;
            partyBroken.getAllUnitSelfSlots = false;
            partyBroken.targetUnitAllySlots = true;
            partyBroken.slotOffsets = [0];

            AnimationVisualsEffect SmiteBrokenAnim = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            SmiteBrokenAnim._animationTarget = partyBroken;
            SmiteBrokenAnim._visuals = Visuals.Excommunicate;

            PerformEffectViaSubaction mendSub = ScriptableObject.CreateInstance<PerformEffectViaSubaction>();
            mendSub.effects = [Effects.GenerateEffect(Mend, 1, Targeting.Unit_AllOpponents)];

            Enemy dogma = new Enemy("Tarnished Divinity", "TarnishedDivinity_EN")
            {
                Health = 200,
                HealthColor = LoadedDBsHandler.PigmentDB.GetPigment("Broken"),
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("DogmaTimeline", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("DogmaDead", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("DogmaOverworld", new Vector2(0.5f, 0f), 32),
                DamageSound = "event:/AAEnemy/Phobias/PhobiasRoar",
                DeathSound = "event:/AAEnemy/Phobias/DogmaDeath",
                UnitTypes = [],
                AbilitySelector = ScriptableObject.CreateInstance <AbilitySelector_NoDuplicate>(),
                CombatEnterEffects = [
                    Effects.GenerateEffect(DogmaValueReset, 0, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(MusicToggleReset),
                    Effects.GenerateEffect(DogmaAdd, 1, Targeting.Slot_SelfSlot),
                ],
            };
            dogma.PrepareEnemyPrefab("Assets/Apocrypha_Enemies/Dogma_Enemy/Dogma_Enemy.prefab", AApocrypha.assetBundle, AApocrypha.assetBundle.LoadAsset<GameObject>("Assets/Apocrypha_Enemies/Dogma_Enemy/Dogma_Giblets.prefab").GetComponent<ParticleSystem>());
            dogma.AddPassives([Passives.GetCustomPassive("Fragile_PA"), Passives.MultiAttack2, Passives.GetCustomPassive("Thrombophilia_PA"), Passives.Dying]);

            Ability worship = new Ability("Your Hollow Worship", "AApocrypha_DogmaAmplify_A")
            {
                Description = "Apply 1 Scar to this enemy." +
                "\nShatter all Broken pigment in the pigment tray. For each pigment shattered, produce one pigment of a random color. If 5 or more Pigment was shattered, increase Dogma by 1." +
                "\nIf all non-Pure party members have Broken health, deal a Painful amount of indirect damage to All party members with Broken health, then randomize the health color of all party members.",
                Cost = [Pigments.Grey],
                Visuals = Visuals.UglyOnTheInside,
                AnimationTarget = Targeting.Slot_SelfSlot,
                Effects =
                [
                    Effects.GenerateEffect(ScarsApply, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<ShatterBrokenPigmentEffect>()),
                    Effects.GenerateEffect(AllBaseColors, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(FivePlus, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(DogmaValueUp, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(MusicTogglePlus, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 2)),
                    Effects.GenerateEffect(DogmaAdd, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 3)),
                    Effects.GenerateEffect(AllIsBroken, 1, Targeting.Unit_AllOpponents),
                    Effects.GenerateEffect(SmiteBrokenAnim, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(BasicEffects.Indirect, 4, partyBroken, Effects.CheckPreviousEffectCondition(true, 2)),
                    Effects.GenerateEffect(mendSub, 1, Targeting.Unit_AllOpponents, Effects.CheckPreviousEffectCondition(true, 2)),
                ],
                Rarity = Rarity.ImpossibleNoReroll,
                Priority = Priority.CreateAndAddCustomPriorityToPool("DogmaLastPriority", -7),
            };
            worship.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Status_Scars), nameof(IntentType_GameIDs.Misc), nameof(IntentType_GameIDs.Mana_Generate)]);
            worship.AddIntentsToTarget(Targeting.Unit_AllOpponents, [nameof(IntentType_GameIDs.Misc_Hidden), nameof(IntentType_GameIDs.Damage_3_6), nameof(IntentType_GameIDs.Mana_Modify)]);

            ExtraAbilityInfo worshipextra = new()
            {
                ability = worship.GenerateEnemyAbility().ability,
                rarity = Rarity.Impossible,
            };
            dogma.AddPassive(Passives.BonusAttackGenerator(worshipextra));

            Ability bless0 = new Ability("Blessings Upon The First", "AApocrypha_DogmaBless0_A")
            {
                Description = "If there is a party member in the Leftmost slot, change their health color to Broken. If this fails, increase Dogma by 1 and generate 1 Broken pigment.",
                Cost = [Pigments.Grey],
                Visuals = Visuals.UglyOnTheInside,
                AnimationTarget = Targeting.GenerateGenericTarget([0], false),
                Effects =
                [
                    Effects.GenerateEffect(Break, 1, Targeting.GenerateGenericTarget([0], false)),
                    Effects.GenerateEffect(DogmaValueUp, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 1)),
                    Effects.GenerateEffect(MusicTogglePlus, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 2)),
                    Effects.GenerateEffect(DogmaAdd, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 3)),
                    Effects.GenerateEffect(GiveBrokenPigment, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 4)),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.ExtremelySlow,
            };
            bless0.AddIntentsToTarget(Targeting.GenerateGenericTarget([0], false), [nameof(IntentType_GameIDs.Mana_Modify), nameof(IntentType_GameIDs.Misc_Hidden)]);
            bless0.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Misc)]);

            Ability bless1 = new Ability("Blessings Upon The Second", "AApocrypha_DogmaBless1_A")
            {
                Description = "If there is a party member in the Center Left slot, change their health color to Broken. If this fails, increase Dogma by 1 and generate 1 Broken pigment.",
                Cost = [Pigments.Grey],
                Visuals = Visuals.UglyOnTheInside,
                AnimationTarget = Targeting.GenerateGenericTarget([1], false),
                Effects =
                [
                    Effects.GenerateEffect(Break, 1, Targeting.GenerateGenericTarget([1], false)),
                    Effects.GenerateEffect(DogmaValueUp, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 1)),
                    Effects.GenerateEffect(MusicTogglePlus, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 2)),
                    Effects.GenerateEffect(DogmaAdd, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 3)),
                    Effects.GenerateEffect(GiveBrokenPigment, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 4)),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.ExtremelySlow,
            };
            bless1.AddIntentsToTarget(Targeting.GenerateGenericTarget([1], false), [nameof(IntentType_GameIDs.Mana_Modify), nameof(IntentType_GameIDs.Misc_Hidden)]);
            bless1.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Misc)]);

            Ability bless2 = new Ability("Blessings Upon The Third", "AApocrypha_DogmaBless2_A")
            {
                Description = "If there is a party member in the Center slot, change their health color to Broken. If this fails, increase Dogma by 1 and generate 1 Broken pigment.",
                Cost = [Pigments.Grey],
                Visuals = Visuals.UglyOnTheInside,
                AnimationTarget = Targeting.GenerateGenericTarget([2], false),
                Effects =
                [
                    Effects.GenerateEffect(Break, 1, Targeting.GenerateGenericTarget([2], false)),
                    Effects.GenerateEffect(DogmaValueUp, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 1)),
                    Effects.GenerateEffect(MusicTogglePlus, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 2)),
                    Effects.GenerateEffect(DogmaAdd, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 3)),
                    Effects.GenerateEffect(GiveBrokenPigment, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 4)),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.ExtremelySlow,
            };
            bless2.AddIntentsToTarget(Targeting.GenerateGenericTarget([2], false), [nameof(IntentType_GameIDs.Mana_Modify), nameof(IntentType_GameIDs.Misc_Hidden)]);
            bless2.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Misc)]);

            Ability bless3 = new Ability("Blessings Upon The Fourth", "AApocrypha_DogmaBless3_A")
            {
                Description = "If there is a party member in the Center Right slot, change their health color to Broken. If this fails, increase Dogma by 1 and generate 1 Broken pigment.",
                Cost = [Pigments.Grey],
                Visuals = Visuals.UglyOnTheInside,
                AnimationTarget = Targeting.GenerateGenericTarget([3], false),
                Effects =
                [
                    Effects.GenerateEffect(Break, 1, Targeting.GenerateGenericTarget([3], false)),
                    Effects.GenerateEffect(DogmaValueUp, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 1)),
                    Effects.GenerateEffect(MusicTogglePlus, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 2)),
                    Effects.GenerateEffect(DogmaAdd, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 3)),
                    Effects.GenerateEffect(GiveBrokenPigment, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 4)),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.ExtremelySlow,
            };
            bless3.AddIntentsToTarget(Targeting.GenerateGenericTarget([3], false), [nameof(IntentType_GameIDs.Mana_Modify), nameof(IntentType_GameIDs.Misc_Hidden)]);
            bless3.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Misc)]);

            Ability bless4 = new Ability("Blessings Upon The Fifth", "AApocrypha_DogmaBless4_A")
            {
                Description = "If there is a party member in the Rightmost slot, change their health color to Broken. If this fails, increase Dogma by 1 and generate 1 Broken pigment.",
                Cost = [Pigments.Grey],
                Visuals = Visuals.UglyOnTheInside,
                AnimationTarget = Targeting.GenerateGenericTarget([4], false),
                Effects =
                [
                    Effects.GenerateEffect(Break, 1, Targeting.GenerateGenericTarget([4], false)),
                    Effects.GenerateEffect(DogmaValueUp, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 1)),
                    Effects.GenerateEffect(MusicTogglePlus, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 2)),
                    Effects.GenerateEffect(DogmaAdd, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 3)),
                    Effects.GenerateEffect(GiveBrokenPigment, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 4)),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.ExtremelySlow,
            };
            bless4.AddIntentsToTarget(Targeting.GenerateGenericTarget([4], false), [nameof(IntentType_GameIDs.Mana_Modify), nameof(IntentType_GameIDs.Misc_Hidden)]);
            bless4.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Misc)]);

            var priority5 = Priority.CreateAndAddCustomPriorityToPool("DogmaMidPriority", -5);

            PerformEffectViaSubaction DogmaResetHandler = ScriptableObject.CreateInstance<PerformEffectViaSubaction>();
            DogmaResetHandler.effects =
            [
                    Effects.GenerateEffect(DogmaValueDown, 4, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(MusicToggleReset, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(DogmaReset, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<MassSwapZoneEffect>(), 1, Targeting.Slot_AllyAllSlots),
            ];

            AnimationVisualsEffect WoundsAnim = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            WoundsAnim._visuals = Visuals.Genesis;
            WoundsAnim._animationTarget = Targeting.Slot_SelfSlot;

            Ability altwounds = new Ability("My Ancient Wounds", "AApocrypha_DogmaAltWounds_A")
            {
                Description = "If Dogma is at 4, remove all status effects from this enemy and apply them to all party members with Broken health, then reset Dogma to 0 and randomly move all enemies. Otherwise, generate 2 Red pigment.",
                Cost = [Pigments.Grey],
                Effects =
                [
                    Effects.GenerateEffect(DogmaGet, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(FourPlus, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(WoundsAnim, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<CasterMoveStatusToTargetsEffect>(), 1, partyBroken, Effects.CheckPreviousEffectCondition(true, 2)),
                    Effects.GenerateEffect(DogmaResetHandler, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 3)),
                    Effects.GenerateEffect(GiveRedPigment, 2, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 4)),
                ],
                Rarity = Rarity.Impossible,
                Priority = Priority.GetCustomPriority("DogmaMidPriority"),
                UnitStoreData = UnitStoreData.GetCustom_UnitStoreData("DogmaStoredValue"),
            };
            altwounds.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Misc_Hidden)]);
            altwounds.AddIntentsToTarget(partyBroken, [nameof(IntentType_GameIDs.Misc)]);
            altwounds.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Misc)]);
            altwounds.AddIntentsToTarget(Targeting.Unit_AllAllySlots, [nameof(IntentType_GameIDs.Swap_Mass)]);
            altwounds.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Mana_Generate)]);

            AnimationVisualsEffect SorrowAnim = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            SorrowAnim._visuals = Visuals.Excommunicate;
            SorrowAnim._animationTarget = Targeting.GenerateSlotTarget([-4, -1, 1, 4]);

            Ability altsorrow = new Ability("My Unending Sorrow", "AApocrypha_DogmaAltSorrow_A")
            {
                Description = "If Dogma is at 4, deal a Deadly amount of damage to the Left and Right party members, then reset Dogma to 0 and randomly move all enemies. Otherwise, generate 2 Blue pigment. This ability assumes the grid wraps around.",
                Cost = [Pigments.Grey],
                Effects =
                [
                    Effects.GenerateEffect(DogmaGet, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(FourPlus, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(SorrowAnim, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 11, Targeting.GenerateSlotTarget([-4, -1, 1, 4]), Effects.CheckPreviousEffectCondition(true, 2)),
                    Effects.GenerateEffect(DogmaResetHandler, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 3)),
                    Effects.GenerateEffect(GiveBluePigment, 2, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 4)),
                ],
                Rarity = Rarity.Impossible,
                Priority = Priority.GetCustomPriority("DogmaMidPriority"),
                UnitStoreData = UnitStoreData.GetCustom_UnitStoreData("DogmaStoredValue"),
            };
            altsorrow.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Misc_Hidden)]);
            altsorrow.AddIntentsToTarget(Targeting.GenerateSlotTarget([-4, -1, 1, 4]), [nameof(IntentType_GameIDs.Damage_11_15)]);
            altsorrow.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Misc)]);
            altsorrow.AddIntentsToTarget(Targeting.Unit_AllAllySlots, [nameof(IntentType_GameIDs.Swap_Mass)]);
            altsorrow.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Mana_Generate)]);

            AnimationVisualsEffect LightAnim = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            LightAnim._visuals = ITAVisuals.Wind;
            LightAnim._animationTarget = partyBroken;

            DamageOfTypeEffect FrostDamage = ScriptableObject.CreateInstance<DamageOfTypeEffect>();
            FrostDamage._DamageTypeID = "AA_Frost_Damage";

            Ability altlight = new Ability("My Extinguished Light", "AApocrypha_DogmaAltLight_A")
            {
                Description = "If Dogma is at 4, deal a Painful amount of frost damage to all party members with Broken health, then reset Dogma to 0 and randomly move all enemies. Otherwise, generate 2 Yellow pigment.",
                Cost = [Pigments.Grey],
                Effects =
                [
                    Effects.GenerateEffect(DogmaGet, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(FourPlus, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(LightAnim, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(FrostDamage, 4, partyBroken, Effects.CheckPreviousEffectCondition(true, 2)),
                    Effects.GenerateEffect(DogmaResetHandler, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 3)),
                    Effects.GenerateEffect(GiveYellowPigment, 2, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 4)),
                ],
                Rarity = Rarity.Impossible,
                Priority = Priority.GetCustomPriority("DogmaMidPriority"),
                UnitStoreData = UnitStoreData.GetCustom_UnitStoreData("DogmaStoredValue"),
            };
            altlight.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Misc_Hidden)]);
            altlight.AddIntentsToTarget(partyBroken, [nameof(IntentType_GameIDs.Damage_3_6)]);
            altlight.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Misc)]);
            altlight.AddIntentsToTarget(Targeting.Unit_AllAllySlots, [nameof(IntentType_GameIDs.Swap_Mass)]);
            altlight.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Mana_Generate)]);

            AnimationVisualsEffect WhispersAnim = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            WhispersAnim._visuals = Visuals.Genesis;
            WhispersAnim._animationTarget = Targeting.Slot_SelfSlot;

            StatusEffect_Apply_Effect CurseRandom = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            CurseRandom._JustOneRandomTarget = true;
            CurseRandom._Status = StatusField.Cursed;

            StatusEffect_Apply_Effect FrailApply = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            FrailApply._Status = StatusField.Frail;

            SpecificOpponentsByStatusTargeting CursedParty = ScriptableObject.CreateInstance<SpecificOpponentsByStatusTargeting>();
            CursedParty._status = StatusField.Cursed;
            CursedParty.getAllUnitSelfSlots = false;
            CursedParty.slotOffsets = [0];
            CursedParty.targetUnitAllySlots = true;

            Ability altwhispers = new Ability("My Dying Whispers", "AApocrypha_DogmaAltWhispers_A")
            {
                Description = "If Dogma is at 4, Curse a random party member with Broken health and apply 3 Frail to all Cursed party members, then reset Dogma to 0 and randomly move all enemies. Otherwise, generate 2 Purple pigment.",
                Cost = [Pigments.Grey],
                Effects =
                [
                    Effects.GenerateEffect(DogmaGet, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(FourPlus, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(WoundsAnim, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(CurseRandom, 1, partyBroken, Effects.CheckPreviousEffectCondition(true, 2)),
                    Effects.GenerateEffect(FrailApply, 3, CursedParty, Effects.CheckPreviousEffectCondition(true, 3)),
                    Effects.GenerateEffect(DogmaResetHandler, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 4)),
                    Effects.GenerateEffect(GivePurplePigment, 2, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 5)),
                ],
                Rarity = Rarity.Impossible,
                Priority = Priority.GetCustomPriority("DogmaMidPriority"),
                UnitStoreData = UnitStoreData.GetCustom_UnitStoreData("DogmaStoredValue"),
            };
            altwhispers.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Misc_Hidden)]);
            altwhispers.AddIntentsToTarget(partyBroken, [nameof(IntentType_GameIDs.Status_Cursed)]);
            altwhispers.AddIntentsToTarget(CursedParty, [nameof(IntentType_GameIDs.Status_Frail)]);
            altwhispers.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Misc)]);
            altwhispers.AddIntentsToTarget(Targeting.Unit_AllAllySlots, [nameof(IntentType_GameIDs.Swap_Mass)]);
            altwhispers.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Mana_Generate)]);

            dogma.AddEnemyAbilities([bless0, bless1, bless2, bless3, bless4]);

            ExtraAbilityInfo woundsextra = new()
            {
                ability = altwounds.GenerateEnemyAbility().ability,
                rarity = Rarity.AbsurdlyRare,
            };

            ExtraAbilityInfo sorrowextra = new()
            {
                ability = altsorrow.GenerateEnemyAbility().ability,
                rarity = Rarity.AbsurdlyRare,
            };

            ExtraAbilityInfo lightextra = new()
            {
                ability = altlight.GenerateEnemyAbility().ability,
                rarity = Rarity.AbsurdlyRare,
            };

            ExtraAbilityInfo whispersextra = new()
            {
                ability = altwhispers.GenerateEnemyAbility().ability,
                rarity = Rarity.AbsurdlyRare,
            };

            dogma.AddPassive(CustomPassives.BonusSuiteGenerator([woundsextra, sorrowextra, lightextra, whispersextra]));

            dogma.AddEnemy(true, false, false);
        }
    }
}
