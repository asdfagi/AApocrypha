using System;
using System.Collections.Generic;
using System.Text;
using A_Apocrypha.CustomOther;

namespace A_Apocrypha.Enemies
{
    public class Logos
    {
        public static void Add()
        {
            Enemy redlogos = new Enemy("Crimson Logos", "CrimsonLogos_EN")
            {
                Health = 40,
                HealthColor = Pigments.Red,
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("LogosTimelineRed", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("AnomalyDead", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("LogosTimelineRed", new Vector2(0.5f, 0f), 32),
                DamageSound = "event:/Characters/Enemies/DLC_01/ChoirBoy/CHR_ENM_ChoirBoy_Dmg",
                DeathSound = "event:/Characters/Enemies/DLC_01/ChoirBoy/CHR_ENM_ChoirBoy_Dth",
                UnitTypes = ["Neathy", "Logos"],
            };
            redlogos.PrepareEnemyPrefab("Assets/Apocrypha_Enemies/Logos_Enemy/Logos_Enemy.prefab", AApocrypha.assetBundle, null);

            Enemy bluelogos = new Enemy("Cerulean Logos", "CeruleanLogos_EN")
            {
                Health = 40,
                HealthColor = Pigments.Blue,
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("LogosTimelineBlue", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("AnomalyDead", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("LogosTimelineBlue", new Vector2(0.5f, 0f), 32),
                DamageSound = "event:/Characters/Enemies/DLC_01/ChoirBoy/CHR_ENM_ChoirBoy_Dmg",
                DeathSound = "event:/Characters/Enemies/DLC_01/ChoirBoy/CHR_ENM_ChoirBoy_Dth",
                UnitTypes = ["Neathy", "Logos"],
            };
            bluelogos.PrepareEnemyPrefab("Assets/Apocrypha_Enemies/Logos_Enemy/LogosBlue_Enemy.prefab", AApocrypha.assetBundle, null);

            Enemy yellowlogos = new Enemy("Aureate Logos", "AureateLogos_EN")
            {
                Health = 40,
                HealthColor = Pigments.Yellow,
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("LogosTimelineYellow", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("AnomalyDead", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("LogosTimelineYellow", new Vector2(0.5f, 0f), 32),
                DamageSound = "event:/Characters/Enemies/DLC_01/ChoirBoy/CHR_ENM_ChoirBoy_Dmg",
                DeathSound = "event:/Characters/Enemies/DLC_01/ChoirBoy/CHR_ENM_ChoirBoy_Dth",
                UnitTypes = ["Neathy", "Logos"],
            };
            yellowlogos.PrepareEnemyPrefab("Assets/Apocrypha_Enemies/Logos_Enemy/LogosYellow_Enemy.prefab", AApocrypha.assetBundle, null);

            Enemy purplelogos = new Enemy("Regent Logos", "RegentLogos_EN")
            {
                Health = 40,
                HealthColor = Pigments.Purple,
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("LogosTimelinePurple", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("AnomalyDead", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("LogosTimelinePurple", new Vector2(0.5f, 0f), 32),
                DamageSound = "event:/Characters/Enemies/DLC_01/ChoirBoy/CHR_ENM_ChoirBoy_Dmg",
                DeathSound = "event:/Characters/Enemies/DLC_01/ChoirBoy/CHR_ENM_ChoirBoy_Dth",
                UnitTypes = ["Neathy", "Logos"],
            };
            purplelogos.PrepareEnemyPrefab("Assets/Apocrypha_Enemies/Logos_Enemy/LogosPurple_Enemy.prefab", AApocrypha.assetBundle, null);

            RegentLogosMusicHandlerEffect MusicReset = ScriptableObject.CreateInstance<RegentLogosMusicHandlerEffect>();
            MusicReset.ResetEffect = true;

            RegentLogosMusicHandlerEffect MusicToggleOn = ScriptableObject.CreateInstance<RegentLogosMusicHandlerEffect>();
            MusicToggleOn.Add = true;

            RegentLogosMusicHandlerEffect MusicToggleOff = ScriptableObject.CreateInstance<RegentLogosMusicHandlerEffect>();
            MusicToggleOff.Add = false;

            purplelogos.CombatEnterEffects = [
                Effects.GenerateEffect(MusicReset),
                Effects.GenerateEffect(MusicToggleOn),
            ];
            purplelogos.CombatExitEffects = [Effects.GenerateEffect(MusicToggleOff)];

            DirectDeathEffect Obliterate = ScriptableObject.CreateInstance<DirectDeathEffect>();
            Obliterate._obliterationDeath = true;

            FieldEffect_Apply_Effect ApplyFire = ScriptableObject.CreateInstance<FieldEffect_Apply_Effect>();
            ApplyFire._Field = StatusField.OnFire;

            FieldEffect_Apply_Effect ApplyRandomFire = ScriptableObject.CreateInstance<FieldEffect_Apply_Effect>();
            ApplyRandomFire._Field = StatusField.OnFire;
            ApplyRandomFire._UseRandomBetweenPrevious = true;

            RemoveFieldEffectEffect RemoveFire = ScriptableObject.CreateInstance<RemoveFieldEffectEffect>();
            RemoveFire._field = StatusField.OnFire;

            FieldEffect_Apply_Effect ApplyConstricted = ScriptableObject.CreateInstance<FieldEffect_Apply_Effect>();
            ApplyConstricted._Field = StatusField.Constricted;

            StatusEffect_Apply_Effect OilApply = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            OilApply._Status = StatusField.OilSlicked;

            StatusEffect_ApplyByPrevious_Effect OilByPrevious = ScriptableObject.CreateInstance<StatusEffect_ApplyByPrevious_Effect>();
            OilByPrevious._Status = StatusField.OilSlicked;

            RemoveStatusEffectEffect OilRemove = ScriptableObject.CreateInstance<RemoveStatusEffectEffect>();
            OilRemove._status = StatusField.OilSlicked;

            StatusEffect_ApplyByPrevious_Effect IrradiatedByPrevious = ScriptableObject.CreateInstance<StatusEffect_ApplyByPrevious_Effect>();
            IrradiatedByPrevious._Status = StatusField.GetCustomStatusEffect("Irradiated_ID");

            SpecificOpponentsByFieldTargeting OpponentsInFire = ScriptableObject.CreateInstance<SpecificOpponentsByFieldTargeting>();
            OpponentsInFire._fieldEffectID = StatusField.OnFire._FieldID;
            OpponentsInFire.targetUnitAllySlots = false;
            OpponentsInFire.slotOffsets = [0];

            SpecificOpponentsByFieldTargeting OneOpponentInFire = ScriptableObject.CreateInstance<SpecificOpponentsByFieldTargeting>();
            OneOpponentInFire._fieldEffectID = StatusField.OnFire._FieldID;
            OneOpponentInFire.targetUnitAllySlots = false;
            OneOpponentInFire.slotOffsets = [0];
            OneOpponentInFire.oneOfTargets = true;

            SwapToOneSideEffect SwapLeft = ScriptableObject.CreateInstance<SwapToOneSideEffect>();
            SwapLeft._swapRight = false;

            SwapToOneSideEffect SwapRight = ScriptableObject.CreateInstance<SwapToOneSideEffect>();
            SwapRight._swapRight = true;

            AnimationVisualsEffect TorchAnim = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            TorchAnim._visuals = Visuals.Torched;
            TorchAnim._animationTarget = Targeting.Slot_Front;

            AnimationVisualsEffect TorchAnim2 = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            TorchAnim2._visuals = Visuals.Torched;
            TorchAnim2._animationTarget = Targeting.Slot_FrontAndSides;

            AnimationVisualsEffect OilAnim = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            OilAnim._visuals = Visuals.OilSlicked;
            OilAnim._animationTarget = Targeting.Slot_Front;

            DamageEffect DamageByPrevious = ScriptableObject.CreateInstance<DamageEffect>();
            DamageByPrevious._usePreviousExitValue = true;

            DamageOnDoubleCascadeEffect DamageCascadeByPrevious = ScriptableObject.CreateInstance<DamageOnDoubleCascadeEffect>();
            DamageCascadeByPrevious._usePreviousExitValue = true;
            DamageCascadeByPrevious._cascadeIsIndirect = true;
            DamageCascadeByPrevious._decreaseAsPercentage = true;
            DamageCascadeByPrevious._cascadeDecrease = 75;
            DamageCascadeByPrevious._returnKillAsSuccess = true;

            PreviousEffectCondition PreviousTrue = ScriptableObject.CreateInstance<PreviousEffectCondition>();
            PreviousTrue.wasSuccessful = true;

            PreviousEffectCondition PreviousFalse = ScriptableObject.CreateInstance<PreviousEffectCondition>();
            PreviousFalse.wasSuccessful = false;

            FieldEffect_Apply_Effect ShieldByPrevious = ScriptableObject.CreateInstance<FieldEffect_Apply_Effect>();
            ShieldByPrevious._Field = StatusField.Shield;
            ShieldByPrevious._UsePreviousExitValueAsMultiplier = true;

            FieldEffect_ApplyWithStatusBonus_Effect ShieldWithOilBonus = ScriptableObject.CreateInstance<FieldEffect_ApplyWithStatusBonus_Effect>();
            ShieldWithOilBonus._Field = StatusField.Shield;
            ShieldWithOilBonus._Status = StatusField.OilSlicked;
            ShieldWithOilBonus._bonusStacking = true;
            ShieldWithOilBonus._bonusAmount = 1;

            FieldEffect_Apply_Effect FireByPrevious = ScriptableObject.CreateInstance<FieldEffect_Apply_Effect>();
            FireByPrevious._Field = StatusField.OnFire;
            FireByPrevious._UsePreviousExitValueAsMultiplier = true;

            FieldEffect_ApplyWithRandomDistribution_Effect FireByPrevious2 = ScriptableObject.CreateInstance<FieldEffect_ApplyWithRandomDistribution_Effect>();
            FireByPrevious2.field = StatusField.OnFire;
            FireByPrevious2.usePrevious = true;

            AllTargeting AllUnitsTargeting = ScriptableObject.CreateInstance<AllTargeting>();
            AllUnitsTargeting._units = true;

            Targetting_ByUnit_Side_Specific_Status OilAllyTargeting = ScriptableObject.CreateInstance<Targetting_ByUnit_Side_Specific_Status>();
            OilAllyTargeting.m_SpecificStatus = [StatusField.OilSlicked];
            OilAllyTargeting.getAllies = true;
            OilAllyTargeting.getAllUnitSlots = true;
            OilAllyTargeting.ignoreCastSlot = false;

            DamageOfTypeEffect FireDamageIndirect = ScriptableObject.CreateInstance<DamageOfTypeEffect>();
            FireDamageIndirect._indirect = true;
            FireDamageIndirect._DamageTypeID = CombatType_GameIDs.Dmg_Fire.ToString();

            DamageOfTypePigmentControlEffect FireDamageDirect = ScriptableObject.CreateInstance<DamageOfTypePigmentControlEffect>();
            FireDamageDirect._indirect = false;
            FireDamageDirect._DamageTypeID = CombatType_GameIDs.Dmg_Fire.ToString();
            FireDamageDirect._producePigment = false;

            DamageAdvancedWithCasterStatusBonusEffect FireDamageCasterOilBoosted = ScriptableObject.CreateInstance<DamageAdvancedWithCasterStatusBonusEffect>();
            FireDamageCasterOilBoosted._indirect = true;
            FireDamageCasterOilBoosted._bonusAmount = 1;
            FireDamageCasterOilBoosted._bonusStacking = true;
            FireDamageCasterOilBoosted._producePigment = false;
            FireDamageCasterOilBoosted._status = StatusField.OilSlicked;
            FireDamageCasterOilBoosted._DamageTypeID = CombatType_GameIDs.Dmg_Fire.ToString();

            StatusEffectCheckerEffect HasOil = ScriptableObject.CreateInstance<StatusEffectCheckerEffect>();
            HasOil._status = StatusField.OilSlicked;

            SpecificAlliesByPassiveTargeting MadeOfFireAllies = ScriptableObject.CreateInstance<SpecificAlliesByPassiveTargeting>();
            MadeOfFireAllies.slotOffsets = [0];
            MadeOfFireAllies.targetUnitAllySlots = true;
            MadeOfFireAllies._passive = Passives.GetCustomPassive("MadeOfFire_PA");

            ConsumeAllColorManaEffect EatPurple = ScriptableObject.CreateInstance<ConsumeAllColorManaEffect>();
            EatPurple._consumeMana = Pigments.Purple;

            SpecificAlliesByUnitTypeTargeting NotLogoi = ScriptableObject.CreateInstance<SpecificAlliesByUnitTypeTargeting>();
            NotLogoi._unitTypes = ["Logos"];
            NotLogoi.blacklist = true;
            NotLogoi.slotOffsets = [0];
            NotLogoi.targetUnitAllySlots = true;

            RemovePassiveEffect UnFire = ScriptableObject.CreateInstance<RemovePassiveEffect>();
            UnFire.m_PassiveID = "MadeOfFire";

            AddPassiveEffect ReFire = ScriptableObject.CreateInstance<AddPassiveEffect>();
            ReFire._passiveToAdd = Passives.GetCustomPassive("MadeOfFire_PA");

            AnimationVisualsEffect PeerAnim = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            PeerAnim._visuals = Visuals.Conductor;
            PeerAnim._animationTarget = Targeting.Slot_SelfSlot;

            StatusEffect_Apply_Effect SpotlightApply = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            SpotlightApply._Status = StatusField.Spotlight;

            RandomTargetPerformEffectViaSubaction PeerSubaction = ScriptableObject.CreateInstance<RandomTargetPerformEffectViaSubaction>();
            PeerSubaction.effects =
            [
                Effects.GenerateEffect(PeerAnim),
                Effects.GenerateEffect(ReFire, 1, Targeting.Slot_SelfSlot),
                Effects.GenerateEffect(SpotlightApply, 1, Targeting.Slot_SelfSlot),
            ];

            CheckPassiveAbilityEffect IsFireproof = ScriptableObject.CreateInstance<CheckPassiveAbilityEffect>();
            IsFireproof.m_PassiveID = "MadeOfFire";

            StatusEffect_ApplyByPrevious_Effect HalfOil = ScriptableObject.CreateInstance<StatusEffect_ApplyByPrevious_Effect>();
            HalfOil._Status = StatusField.OilSlicked;
            HalfOil._entryVariableAsPercentage = true;

            Ability westwheel = new Ability("Turn To The West", "AApocrypha_LogosLeft_A")
            {
                Description = "Move Left, then apply 1 Fire to the newly Opposing position.",
                Cost = [Pigments.Grey, Pigments.Red],
                Effects =
                [
                    Effects.GenerateEffect(SwapLeft, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(TorchAnim, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(ApplyFire, 1, Targeting.Slot_Front),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.VeryFast,
            };
            westwheel.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Swap_Left)]);
            westwheel.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Field_Fire)]);

            Ability eastwheel = new Ability("Turn To The East", "AApocrypha_LogosRight_A")
            {
                Description = "Move Right, then apply 1 Fire to the newly Opposing position.",
                Cost = [Pigments.Red, Pigments.Grey],
                Effects =
                [
                    Effects.GenerateEffect(SwapRight, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(TorchAnim, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(ApplyFire, 1, Targeting.Slot_Front),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.VeryFast,
            };
            eastwheel.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Swap_Right)]);
            eastwheel.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Field_Fire)]);

            Ability westflow = new Ability("Flow To The West", "AApocrypha_LogosLeftOil_A")
            {
                Description = "Move Left. If this enemy swapped positions with another, they gain 1 Oil Slicked.\nApply 1 Oil Slicked to the Opposing party member.",
                Cost = [Pigments.Grey, Pigments.Red],
                Effects =
                [
                    Effects.GenerateEffect(SwapLeft, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(OilApply, 1, Targeting.Slot_AllyRight, PreviousTrue),
                    Effects.GenerateEffect(OilAnim, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(OilApply, 1, Targeting.Slot_Front),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.VeryFast,
            };
            westflow.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Swap_Left)]);
            westflow.AddIntentsToTarget(Targeting.Slot_AllyRight, [nameof(IntentType_GameIDs.Status_OilSlicked)]);
            westflow.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Status_OilSlicked)]);

            Ability eastflow = new Ability("Flow To The East", "AApocrypha_LogosRightOil_A")
            {
                Description = "Move Right. If this enemy swapped positions with another, they gain 1 Oil Slicked.\nApply 1 Oil Slicked to the Opposing party member.",
                Cost = [Pigments.Red, Pigments.Grey],
                Effects =
                [
                    Effects.GenerateEffect(SwapRight, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(OilApply, 1, Targeting.Slot_AllyLeft, PreviousTrue),
                    Effects.GenerateEffect(OilAnim, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(OilApply, 1, Targeting.Slot_Front),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.VeryFast,
            };
            eastflow.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Swap_Right)]);
            eastflow.AddIntentsToTarget(Targeting.Slot_AllyLeft, [nameof(IntentType_GameIDs.Status_OilSlicked)]);
            eastflow.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Status_OilSlicked)]);

            Ability tonguesoffire = new Ability("Tongues Of Fire", "AApocrypha_FireTongues_A")
            {
                Description = "Deal a Painful amount of damage to All party members standing in Fire. Apply 0-2 Fire to the Left, Right and Opposing spaces.",
                Cost = [Pigments.Red, Pigments.Grey, Pigments.Red],
                Visuals = Visuals.Torched,
                AnimationTarget = OpponentsInFire,
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 5, OpponentsInFire),
                    Effects.GenerateEffect(TorchAnim2, 1, Targeting.Slot_FrontAndSides),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<ExtraVariableForNextEffect>(), 0),
                    Effects.GenerateEffect(ApplyRandomFire, 2, Targeting.Slot_FrontAndSides),
                ],
                Rarity = Rarity.Impossible,
                Priority = Priority.Fast,
            };
            tonguesoffire.AddIntentsToTarget(OpponentsInFire, [nameof(IntentType_GameIDs.Damage_3_6)]);
            tonguesoffire.AddIntentsToTarget(Targeting.Slot_FrontAndSides, [nameof(IntentType_GameIDs.Field_Fire)]);

            Ability everythinginplace = new Ability("Everything In Its Place", "AApocrypha_EverythingInPlace_A")
            {
                Description = "Apply 1 Fire to all party members standing in Fire. Apply 1 Constricted to a random party member standing in Fire.",
                Cost = [Pigments.Grey, Pigments.Red, Pigments.Red],
                Visuals = Visuals.Torched,
                AnimationTarget = OpponentsInFire,
                Effects =
                [
                    Effects.GenerateEffect(ApplyFire, 1, OpponentsInFire),
                    Effects.GenerateEffect(ApplyConstricted, 1, OneOpponentInFire),
                ],
                Rarity = Rarity.Impossible,
                Priority = Priority.Fast,
            };
            everythinginplace.AddIntentsToTarget(OpponentsInFire, [nameof(IntentType_GameIDs.Field_Fire)]);
            everythinginplace.AddIntentsToTarget(OpponentsInFire, [nameof(IntentType_GameIDs.Field_Constricted)]);

            Ability becomefire = new Ability("To Become Fire", "AApocrypha_BecomeFire_A")
            {
                Description = "Remove all Fire from the Left, Opposing and Right positions. Deal damage to the Opposing party member equal to twice the amount of Fire removed. Damage cascades indirectly to the Left and Right with a 25% falloff.\nIf this damage kills, remove all Fire from All party member positions.",
                Cost = [Pigments.Red, Pigments.Red, Pigments.Red, Pigments.Red],
                Visuals = Visuals.Conductor,
                AnimationTarget = Targeting.Slot_Front,
                Effects =
                [
                    Effects.GenerateEffect(RemoveFire, 1, Targeting.Slot_FrontAndSides),
                    Effects.GenerateEffect(DamageCascadeByPrevious, 2, Targeting.GenerateSlotTarget([0, -1, 1, -2, 2, -3, 3, -4, 4], false)),
                    Effects.GenerateEffect(RemoveFire, 1, Targeting.Slot_OpponentAllSlots, PreviousTrue),
                ],
                Rarity = Rarity.Impossible,
                Priority = Priority.Fast,
            };
            becomefire.AddIntentsToTarget(Targeting.Slot_FrontAndSides, [nameof(IntentType_GameIDs.Rem_Field_Fire)]);
            becomefire.AddIntentsToTarget(Targeting.Slot_OpponentAllLefts, [nameof(IntentType_GameIDs.Swap_Left)]);
            becomefire.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Damage_7_10)]);
            becomefire.AddIntentsToTarget(Targeting.Slot_OpponentAllRights, [nameof(IntentType_GameIDs.Swap_Right)]);

            Ability lifepreserved = new Ability("Life Preserved", "AApocrypha_LifePreserved_A")
            {
                Description = "Convert all Fire on All allied positions to three times the amount of Shield.",
                Cost = [Pigments.Blue, Pigments.Blue, Pigments.Blue, Pigments.Blue],
                Visuals = Visuals.Shield,
                AnimationTarget = Targeting.Slot_AllyAllSlots,
                Effects =
                [
                    Effects.GenerateEffect(RemoveFire, 1, Targeting.GenerateGenericTarget([0], true)),
                    Effects.GenerateEffect(ShieldByPrevious, 3, Targeting.GenerateGenericTarget([0], true)),
                    Effects.GenerateEffect(RemoveFire, 1, Targeting.GenerateGenericTarget([1], true)),
                    Effects.GenerateEffect(ShieldByPrevious, 3, Targeting.GenerateGenericTarget([1], true)),
                    Effects.GenerateEffect(RemoveFire, 1, Targeting.GenerateGenericTarget([2], true)),
                    Effects.GenerateEffect(ShieldByPrevious, 3, Targeting.GenerateGenericTarget([2], true)),
                    Effects.GenerateEffect(RemoveFire, 1, Targeting.GenerateGenericTarget([3], true)),
                    Effects.GenerateEffect(ShieldByPrevious, 3, Targeting.GenerateGenericTarget([3], true)),
                    Effects.GenerateEffect(RemoveFire, 1, Targeting.GenerateGenericTarget([4], true)),
                    Effects.GenerateEffect(ShieldByPrevious, 3, Targeting.GenerateGenericTarget([4], true)),
                ],
                Rarity = Rarity.Impossible,
                Priority = Priority.Fast,
            };
            lifepreserved.AddIntentsToTarget(Targeting.Slot_AllyAllSlots, [nameof(IntentType_GameIDs.Rem_Field_Fire), nameof(IntentType_GameIDs.Field_Shield)]);

            Ability exchangeburdens = new Ability("An Exchange Of Burdens", "AApocrypha_ExchangeBurdens_A")
            {
                Description = "Remove all Fire from All positions and apply it to this enemy.\nRemove all Fire from this enemy's position and distribute it randomly to All occupied positions.",
                Cost = [Pigments.Red, Pigments.Red, Pigments.Red, Pigments.Red],
                Visuals = Visuals.Conductor,
                AnimationTarget = Targeting.Slot_Front,
                Effects =
                [
                    Effects.GenerateEffect(RemoveFire, 1, Targeting.Slot_AllyAllSlots),
                    Effects.GenerateEffect(FireByPrevious, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(RemoveFire, 1, Targeting.Slot_OpponentAllSlots),
                    Effects.GenerateEffect(FireByPrevious, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(RemoveFire, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(FireByPrevious2, 1, AllUnitsTargeting),
                ],
                Rarity = Rarity.Impossible,
                Priority = Priority.Fast,
            };
            exchangeburdens.AddIntentsToTarget(Targeting.Slot_AllyAllSlots, [nameof(IntentType_GameIDs.Rem_Field_Fire), nameof(IntentType_GameIDs.Field_Fire)]);
            exchangeburdens.AddIntentsToTarget(Targeting.Slot_OpponentAllSlots, [nameof(IntentType_GameIDs.Rem_Field_Fire), nameof(IntentType_GameIDs.Field_Fire)]);

            Ability rotandobliteration = new Ability("Between Rot And Obliteration", "AApocrypha_RotAndObliteration_A")
            {
                Description = "Remove all Fire from All party member positions and apply Irradiated to each party member equal to the amount of Fire removed from their position.",
                Cost = [Pigments.Yellow, Pigments.Yellow, Pigments.Yellow, Pigments.Yellow],
                Visuals = CustomVisuals.MicrowaveVisualsSO,
                AnimationTarget = Targeting.Slot_OpponentAllSlots,
                Effects =
                [
                    Effects.GenerateEffect(RemoveFire, 1, Targeting.GenerateGenericTarget([0], false)),
                    Effects.GenerateEffect(IrradiatedByPrevious, 1, Targeting.GenerateGenericTarget([0], false)),
                    Effects.GenerateEffect(RemoveFire, 1, Targeting.GenerateGenericTarget([1], false)),
                    Effects.GenerateEffect(IrradiatedByPrevious, 1, Targeting.GenerateGenericTarget([1], false)),
                    Effects.GenerateEffect(RemoveFire, 1, Targeting.GenerateGenericTarget([2], false)),
                    Effects.GenerateEffect(IrradiatedByPrevious, 1, Targeting.GenerateGenericTarget([2], false)),
                    Effects.GenerateEffect(RemoveFire, 1, Targeting.GenerateGenericTarget([3], false)),
                    Effects.GenerateEffect(IrradiatedByPrevious, 1, Targeting.GenerateGenericTarget([3], false)),
                    Effects.GenerateEffect(RemoveFire, 1, Targeting.GenerateGenericTarget([4], false)),
                    Effects.GenerateEffect(IrradiatedByPrevious, 1, Targeting.GenerateGenericTarget([4], false)),
                ],
                Rarity = Rarity.Impossible,
                Priority = Priority.Fast,
            };
            rotandobliteration.AddIntentsToTarget(Targeting.Slot_OpponentAllSlots, [nameof(IntentType_GameIDs.Rem_Field_Fire), "Status_Irradiated"]);

            Ability recognitionpeer = new Ability("Recognition Of A Peer", "AApocrypha_RecognitionPeer_A")
            {
                Description = "Remove Made Of Fire from All enemies that are not Logoi.\nApply Made Of Fire and Spotlight to a random non-Logos enemy. If there are no valid targets, apply Spotlight to this enemy.",
                Cost = [Pigments.Yellow, Pigments.Yellow, Pigments.Yellow, Pigments.Yellow],
                Effects =
                [
                    Effects.GenerateEffect(UnFire, 1, NotLogoi),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<CheckHasUnitEffect>(), 1, NotLogoi),
                    Effects.GenerateEffect(PeerAnim, 1, Targeting.Slot_SelfSlot, PreviousGenerator(false, 1)),
                    Effects.GenerateEffect(SpotlightApply, 1, Targeting.Slot_SelfSlot, PreviousGenerator(false, 2)),
                    Effects.GenerateEffect(PeerSubaction, 1, NotLogoi, PreviousGenerator(true, 3)),
                ],
                Rarity = Rarity.Impossible,
                Priority = Priority.Fast,
            };
            recognitionpeer.AddIntentsToTarget(NotLogoi, ["Rem_Passive_MadeOfFire", "Passive_MadeOfFire", nameof(IntentType_GameIDs.Status_Spotlight)]);
            recognitionpeer.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Misc_Hidden), nameof(IntentType_GameIDs.Status_Spotlight)]);

            Ability alchemicalregent = new Ability("Alchemical Regent", "AApocrypha_AlchemicalRegent_A")
            {
                Description = "Deal a Barely Painful amount of fire damage to the Opposing party member. If the Opposing party member was Oil Slicked, deal Almost No fire damage to the Left and Right party members. The damage dealt by this ability is increased by the amount of Oil Slicked on this enemy.\nRemove half of this enemy's Oil Slicked.",
                Cost = [Pigments.Purple, Pigments.Purple, Pigments.Purple, Pigments.Purple],
                Visuals = Visuals.Pyre,
                AnimationTarget = Targeting.Slot_Front,
                Effects =
                [
                    Effects.GenerateEffect(FireDamageCasterOilBoosted, 3, Targeting.Slot_Front),
                    Effects.GenerateEffect(HasOil, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(FireDamageCasterOilBoosted, 1, Targeting.Slot_OpponentSides, PreviousTrue),
                    Effects.GenerateEffect(OilRemove, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(HalfOil, 50, Targeting.Slot_SelfSlot, PreviousTrue),
                ],
                Rarity = Rarity.Impossible,
                Priority = Priority.Fast,
            };
            alchemicalregent.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Damage_3_6), nameof(IntentType_GameIDs.Misc_Hidden)]);
            alchemicalregent.AddIntentsToTarget(Targeting.Slot_OpponentSides, [nameof(IntentType_GameIDs.Damage_1_2)]);
            alchemicalregent.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Rem_Status_OilSlicked)]);

            Ability royalblood = new Ability("Of Royal Blood", "AApocrypha_RoyalBlood_A")
            {
                Description = "Apply Shield to all occupied enemy positions equal to how many stacks of Oil Slicked they have.\nRemove half of this enemy's Oil Slicked.",
                Cost = [Pigments.Purple, Pigments.Purple, Pigments.Purple, Pigments.Purple],
                Visuals = Visuals.Shield,
                AnimationTarget = OilAllyTargeting,
                Effects =
                [
                    Effects.GenerateEffect(ShieldWithOilBonus, 0, OilAllyTargeting),
                    Effects.GenerateEffect(OilRemove, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(HalfOil, 50, Targeting.Slot_SelfSlot, PreviousTrue),
                ],
                Rarity = Rarity.Impossible,
                Priority = Priority.Fast,
            };
            royalblood.AddIntentsToTarget(OilAllyTargeting, [nameof(IntentType_GameIDs.Field_Shield)]);
            royalblood.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Rem_Status_OilSlicked)]);

            Ability butcheredlanguage = new Ability("Butchered Language", "AApocrypha_ButcheredLanguage_A")
            {
                Description = "Consume all Purple Pigment. Apply Fire to all spaces occupied by a unit Made of Fire equal to the consumed pigment.\nConvert all Fire on this enemy's position into an equal amount of Oil Slicked.",
                Cost = [Pigments.Purple, Pigments.Purple, Pigments.Purple, Pigments.Purple],
                Visuals = Visuals.Pyre,
                AnimationTarget = MadeOfFireAllies,
                Effects =
                [
                    Effects.GenerateEffect(EatPurple, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(FireByPrevious, 1, MadeOfFireAllies),
                    Effects.GenerateEffect(RemoveFire, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(OilByPrevious, 1, Targeting.Slot_SelfSlot),
                ],
                Rarity = Rarity.Impossible,
                Priority = Priority.Fast,
            };
            butcheredlanguage.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Mana_Consume)]);
            butcheredlanguage.AddIntentsToTarget(MadeOfFireAllies, [nameof(IntentType_GameIDs.Field_Fire)]);

            ExtraAbilityInfo tonguesextrared = new()
            {
                ability = tonguesoffire.GenerateEnemyAbility().ability,
                rarity = Rarity.Uncommon,
            };

            ExtraAbilityInfo tonguesextrablue = new()
            {
                ability = tonguesoffire.GenerateEnemyAbility().ability,
                rarity = Rarity.Uncommon,
            };

            ExtraAbilityInfo tonguesextrayellow = new()
            {
                ability = tonguesoffire.GenerateEnemyAbility().ability,
                rarity = Rarity.Uncommon,
            };

            ExtraAbilityInfo placeextra = new()
            {
                ability = everythinginplace.GenerateEnemyAbility().ability,
                rarity = Rarity.Uncommon,
            };

            ExtraAbilityInfo becomeextra = new()
            {
                ability = becomefire.GenerateEnemyAbility().ability,
                rarity = Rarity.Uncommon,
            };

            ExtraAbilityInfo lifeextra = new()
            {
                ability = lifepreserved.GenerateEnemyAbility().ability,
                rarity = Rarity.Uncommon,
            };

            ExtraAbilityInfo burdensextra = new()
            {
                ability = exchangeburdens.GenerateEnemyAbility().ability,
                rarity = Rarity.Uncommon,
            };

            ExtraAbilityInfo rotextra = new()
            {
                ability = rotandobliteration.GenerateEnemyAbility().ability,
                rarity = Rarity.Uncommon,
            };

            ExtraAbilityInfo peerextra = new()
            {
                ability = recognitionpeer.GenerateEnemyAbility().ability,
                rarity = Rarity.Uncommon,
            };

            ExtraAbilityInfo royalextra = new()
            {
                ability = royalblood.GenerateEnemyAbility().ability,
                rarity = Rarity.Uncommon,
            };

            ExtraAbilityInfo alchemicalextra = new()
            {
                ability = alchemicalregent.GenerateEnemyAbility().ability,
                rarity = Rarity.Uncommon,
            };

            ExtraAbilityInfo languageextra = new()
            {
                ability = butcheredlanguage.GenerateEnemyAbility().ability,
                rarity = Rarity.Uncommon,
            };

            redlogos.AddPassives([Passives.MultiAttack3, Passives.InfernoGenerator(1), Passives.GetCustomPassive("MadeOfFire_PA"), Passives.GetCustomPassive("AA_CondenseRed_PA"), CustomPassives.BonusSuiteGenerator([tonguesextrared, placeextra, becomeextra])]);
            bluelogos.AddPassives([Passives.MultiAttack3, Passives.InfernoGenerator(1), Passives.GetCustomPassive("MadeOfFire_PA"), Passives.GetCustomPassive("AA_CondenseBlue_PA"), CustomPassives.BonusSuiteGenerator([tonguesextrablue, lifeextra, burdensextra])]);
            yellowlogos.AddPassives([Passives.MultiAttack3, Passives.InfernoGenerator(1), Passives.GetCustomPassive("MadeOfFire_PA"), Passives.GetCustomPassive("AA_CondenseYellow_PA"), CustomPassives.BonusSuiteGenerator([tonguesextrayellow, rotextra, peerextra])]);
            purplelogos.AddPassives([Passives.MultiAttack3, Passives.GetCustomPassive("BlackTears_2_PA"), Passives.GetCustomPassive("MadeOfFire_PA"), Passives.GetCustomPassive("AA_CondensePurple_PA"), CustomPassives.BonusSuiteGenerator([alchemicalextra, royalextra, languageextra])]);

            redlogos.AddEnemyAbilities(
            [
                westwheel,
                eastwheel,
            ]);

            bluelogos.AddEnemyAbilities(
            [
                westwheel,
                eastwheel,
            ]);

            yellowlogos.AddEnemyAbilities(
            [
                westwheel,
                eastwheel,
            ]);

            purplelogos.AddEnemyAbilities(
            [
                westflow,
                eastflow,
            ]);

            redlogos.AddEnemy(true, true, false);
            bluelogos.AddEnemy(true, true, false);
            yellowlogos.AddEnemy(true, true, false);
            purplelogos.AddEnemy(true, true, false);

            CasterInOneEdgeCheckEffect CheckLeft = ScriptableObject.CreateInstance<CasterInOneEdgeCheckEffect>();
            CheckLeft._right = false;

            CasterInOneEdgeCheckEffect CheckRight = ScriptableObject.CreateInstance<CasterInOneEdgeCheckEffect>();
            CheckRight._right = true;

            if (LoadedDBsHandler.PigmentDB.GetPigment("Broken") != null)
            {
                Enemy blacklogos = new Enemy("Discordant Logos", "DiscordantLogos_EN")
                {
                    Health = 40,
                    HealthColor = LoadedDBsHandler.PigmentDB.GetPigment("Broken"),
                    Size = 1,
                    CombatSprite = ResourceLoader.LoadSprite("LogosTimelineBlack", new Vector2(0.5f, 0f), 32),
                    OverworldDeadSprite = ResourceLoader.LoadSprite("AnomalyDead", new Vector2(0.5f, 0f), 32),
                    OverworldAliveSprite = ResourceLoader.LoadSprite("LogosTimelineBlack", new Vector2(0.5f, 0f), 32),
                    DamageSound = "event:/AAEnemy/LogosDisco/LogosDiscoHurt",
                    DeathSound = "event:/AAEnemy/LogosDisco/LogosDiscoDeath",
                    UnitTypes = ["Neathy", "Logos"],
                    CombatEnterEffects = [Effects.GenerateEffect(ScriptableObject.CreateInstance<CasterCapitalizeNameEffect>())],
                };
                blacklogos.PrepareEnemyPrefab("Assets/Apocrypha_Enemies/Logos_Enemy/LogosBlack_Enemy.prefab", AApocrypha.assetBundle, null);

                FieldEffect_Apply_Effect HoarfrostApply = ScriptableObject.CreateInstance<FieldEffect_Apply_Effect>();
                HoarfrostApply._Field = StatusField.GetCustomFieldEffect("Hoarfrost_ID");

                RemoveFieldEffectEffect HoarfrostRemove = ScriptableObject.CreateInstance<RemoveFieldEffectEffect>();
                HoarfrostRemove._field = StatusField.GetCustomFieldEffect("Hoarfrost_ID");

                AnimationVisualsEffect NothingAnim = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
                NothingAnim._visuals = CustomVisuals.Nothing;
                NothingAnim._animationTarget = Targeting.Slot_Front;

                DamageOfTypeAdditivePreviousEffect FrostDamageAdditiveBonus = ScriptableObject.CreateInstance<DamageOfTypeAdditivePreviousEffect>();
                FrostDamageAdditiveBonus._DamageTypeID = "AA_Frost_Damage";
                FrostDamageAdditiveBonus._usePreviousExitValue = true;
                FrostDamageAdditiveBonus._bonusAmount = 2;

                FieldEffect_ApplyWithRandomDistribution_Effect HoarfrostDistributeByPrevious = ScriptableObject.CreateInstance<FieldEffect_ApplyWithRandomDistribution_Effect>();
                HoarfrostDistributeByPrevious.field = StatusField.GetCustomFieldEffect("Hoarfrost_ID");
                HoarfrostDistributeByPrevious.usePrevious = true;

                Ability turnback = new Ability("NOT CLOCKWISE", "AApocrypha_LogosLeftDisco_A")
                {
                    Description = "Do not move Right, then do not apply 1 Hoarfrost to the newly Opposing position. This ability does not assume that the grip loops around.\n\"THE MONARCH DOES NOT SPEAK THE TRUTH\"",
                    Cost = [Pigments.Grey, LoadedDBsHandler.PigmentDB.GetPigment("Broken")],
                    Effects =
                    [
                        Effects.GenerateEffect(SwapLeft, 1, Targeting.Slot_SelfSlot),
                        Effects.GenerateEffect(CheckLeft, 1, Targeting.Slot_SelfSlot, PreviousFalse),
                        Effects.GenerateEffect(ScriptableObject.CreateInstance<MirrorPositionEffect>(), 1, Targeting.Slot_SelfSlot, Effects.CheckMultiplePreviousEffectsCondition([true, false], [1, 2])),
                        Effects.GenerateEffect(NothingAnim, 1, Targeting.Slot_Front),
                        Effects.GenerateEffect(HoarfrostApply, 1, Targeting.Slot_Front),
                    ],
                    Rarity = Rarity.Common,
                    Priority = Priority.VeryFast,
                };
                turnback.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Swap_Right)]);
                turnback.AddIntentsToTarget(Targeting.Slot_Front, ["Rem_Field_Hoarfrost"]);

                Ability turnforth = new Ability("NOT COUNTERCLOCKWISE", "AApocrypha_LogosRightDisco_A")
                {
                    Description = "Do not move Left, then do not apply 1 Hoarfrost to the newly Opposing position. This ability does not assume that the grip loops around.\n\"LEFT IS NOT LEFT, RIGHT IS NOT RIGHT\"",
                    Cost = [LoadedDBsHandler.PigmentDB.GetPigment("Broken"), Pigments.Grey],
                    Effects =
                    [
                        Effects.GenerateEffect(SwapRight, 1, Targeting.Slot_SelfSlot),
                        Effects.GenerateEffect(CheckRight, 1, Targeting.Slot_SelfSlot, PreviousFalse),
                        Effects.GenerateEffect(ScriptableObject.CreateInstance<MirrorPositionEffect>(), 1, Targeting.Slot_SelfSlot, Effects.CheckMultiplePreviousEffectsCondition([true, false], [1, 2])),
                        Effects.GenerateEffect(NothingAnim, 1, Targeting.Slot_Front),
                        Effects.GenerateEffect(HoarfrostApply, 1, Targeting.Slot_Front),
                    ],
                    Rarity = Rarity.Common,
                    Priority = Priority.VeryFast,
                };
                turnforth.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Swap_Left)]);
                turnforth.AddIntentsToTarget(Targeting.Slot_Front, ["Rem_Field_Hoarfrost"]);

                Ability nowordsspoken = new Ability("NO WORDS SHALL BE SPOKEN", "AApocrypha_NoWordsSpoken_A")
                {
                    Description = "Do not remove all Hoarfrost from the Opposing position, then do not deal a Painful amount of frost damage to the Left and Right party members. This damage is not increased by two for each stack of Hoarfrost removed. This ability does not assume that the grid loops around.",
                    Cost = [LoadedDBsHandler.PigmentDB.GetPigment("Broken"), LoadedDBsHandler.PigmentDB.GetPigment("Broken"), LoadedDBsHandler.PigmentDB.GetPigment("Broken"), LoadedDBsHandler.PigmentDB.GetPigment("Broken")],
                    Visuals = CustomVisuals.Whispers,
                    AnimationTarget = Targeting.GenerateSlotTarget([-1, 1, -4, 4], false),
                    Effects =
                    [
                        Effects.GenerateEffect(HoarfrostRemove, 1, Targeting.Slot_Front),
                        Effects.GenerateEffect(FrostDamageAdditiveBonus, 5, Targeting.GenerateSlotTarget([-1, 1, -4, 4], false)),
                    ],
                    Rarity = Rarity.Impossible,
                    Priority = Priority.Fast,
                };
                nowordsspoken.AddIntentsToTarget(Targeting.Slot_Front, ["Field_Hoarfrost"]);
                nowordsspoken.AddIntentsToTarget(Targeting.GenerateSlotTarget([-1, 1, -4, 4], false), [nameof(IntentType_GameIDs.Misc_Hidden), nameof(IntentType_GameIDs.Damage_7_10)]);

                Ability nocolorseen = new Ability("NO COLORS SHALL BE SEEN", "AApocrypha_NoColorsSeen_A")
                {
                    Description = "Do not attempt to break 4 random pigment. Do not randomly distribute an amount of Hoarfrost equal to twice the amount of pigment broken to all occupied party member positions.",
                    Cost = [LoadedDBsHandler.PigmentDB.GetPigment("Broken"), LoadedDBsHandler.PigmentDB.GetPigment("Broken"), LoadedDBsHandler.PigmentDB.GetPigment("Broken"), LoadedDBsHandler.PigmentDB.GetPigment("Broken")],
                    Visuals = ITAVisuals.Wind,
                    AnimationTarget = Targeting.Unit_AllOpponentSlots,
                    Effects =
                    [
                        Effects.GenerateEffect(ScriptableObject.CreateInstance<RandomizeNumberPigmentCasterHealthColorEffect>(), 4, Targeting.Slot_SelfSlot),
                        Effects.GenerateEffect(HoarfrostDistributeByPrevious, 2, Targeting.Slot_OpponentAllSlots),
                    ],
                    Rarity = Rarity.Impossible,
                    Priority = Priority.Fast,
                };
                nocolorseen.AddIntentsToTarget(Targeting.Slot_SelfSlot, ["AA_Pigment_Transform"]);
                nocolorseen.AddIntentsToTarget(Targeting.Slot_OpponentAllSlots, ["Rem_Field_Hoarfrost"]);

                Ability nothingbe = new Ability("NO THING SHALL BE", "AApocrypha_NoThingShallBe_A")
                {
                    Description = "Do not shatter all broken pigment in the pigment bar. Do not deal damage to the Opposing party member equal to twice the amount of pigment shattered. Damage does not cascade indirectly to the Left and Right with a 25% falloff.\nIf no pigment is shattered, do not attempt to break 4 random pigment.",
                    Cost = [LoadedDBsHandler.PigmentDB.GetPigment("Broken"), LoadedDBsHandler.PigmentDB.GetPigment("Broken"), LoadedDBsHandler.PigmentDB.GetPigment("Broken"), LoadedDBsHandler.PigmentDB.GetPigment("Broken")],
                    Visuals = CustomVisuals.GazeVisualsSO,
                    AnimationTarget = Targeting.Slot_SelfSlot,
                    Effects =
                    [
                        Effects.GenerateEffect(ScriptableObject.CreateInstance<ShatterBrokenPigmentEffect>()),
                        Effects.GenerateEffect(DamageCascadeByPrevious, 2, Targeting.GenerateSlotTarget([0, -1, 1, -2, 2, -3, 3, -4, 4], false)),
                        Effects.GenerateEffect(ScriptableObject.CreateInstance<RandomizeNumberPigmentCasterHealthColorEffect>(), 4, Targeting.Slot_SelfSlot, PreviousGenerator(false, 2)),
                    ],
                    Rarity = Rarity.Impossible,
                    Priority = Priority.Fast,
                };
                nothingbe.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Misc)]);
                nothingbe.AddIntentsToTarget(Targeting.Slot_OpponentAllLefts, [nameof(IntentType_GameIDs.Swap_Left)]);
                nothingbe.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Damage_7_10)]);
                nothingbe.AddIntentsToTarget(Targeting.Slot_OpponentAllRights, [nameof(IntentType_GameIDs.Swap_Right)]);
                nothingbe.AddIntentsToTarget(Targeting.Slot_SelfSlot, ["AA_Pigment_Transform"]);

                ExtraAbilityInfo wordsextra = new()
                {
                    ability = nowordsspoken.GenerateEnemyAbility().ability,
                    rarity = Rarity.Uncommon,
                };

                ExtraAbilityInfo colorsextra = new()
                {
                    ability = nocolorseen.GenerateEnemyAbility().ability,
                    rarity = Rarity.Uncommon,
                };

                ExtraAbilityInfo nothingextra = new()
                {
                    ability = nothingbe.GenerateEnemyAbility().ability,
                    rarity = Rarity.Uncommon,
                };

                blacklogos.AddPassives([Passives.MultiAttack3, Passives.GetCustomPassive("Snowstorm_1_PA"), Passives.GetCustomPassive("Antifreeze_PA"), Passives.GetCustomPassive("Fragile_PA"), CustomPassives.BonusSuiteGenerator([wordsextra, colorsextra, nothingextra])]);

                blacklogos.AddEnemyAbilities(
                [
                    turnback,
                    turnforth,
                ]);

                blacklogos.AddEnemy(false, false, false);
            }

            if (AApocrypha.CrossMod.IntoTheAbyss && Abyss.Exists)
            {
                //Debug.Log("Orguis | anims");
                AttackVisualsSO GlitchVisuals = LoadedAssetsHandler.GetCharacterAbility("SamDefrag_A").visuals;

                AnimationVisualsEffect GlitchAnim = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
                GlitchAnim._visuals = GlitchVisuals;
                GlitchAnim._animationTarget = Targeting.Slot_Front;

                AttackVisualsSO ResonateVisuals = LoadedAssetsHandler.GetCharacterAbility("GenevievePeace1_A").visuals;

                AnimationVisualsEffect ResonateAnim = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
                ResonateAnim._visuals = ResonateVisuals;
                ResonateAnim._animationTarget = Targeting.Slot_Front;

                AnimationVisualsEffect PendLAnim = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
                PendLAnim._visuals = ITAVisuals.PendulumL;
                PendLAnim._animationTarget = Targeting.Slot_Front;

                AnimationVisualsEffect PendRAnim = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
                PendRAnim._visuals = ITAVisuals.PendulumR;
                PendRAnim._animationTarget = Targeting.Slot_Front;

                AnimationVisualsEffect LookAnim = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
                LookAnim._visuals = Visuals.Providence;
                LookAnim._animationTarget = Targeting.Slot_Front;

                AnimationVisualsEffect BeamAnim = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
                BeamAnim._visuals = Visuals.Excommunicate;
                BeamAnim._animationTarget = Targeting.Slot_Front;

                RemovePassiveEffect UnFree = ScriptableObject.CreateInstance<RemovePassiveEffect>();
                UnFree.m_PassiveID = "FreeWilled";

                AddPassiveEffect ReFree = ScriptableObject.CreateInstance<AddPassiveEffect>();
                ReFree._passiveToAdd = Passives.GetCustomPassive("AA_FreeWilled_PA");

                PassivePopUpOnTargetEffect FreePopup = ScriptableObject.CreateInstance<PassivePopUpOnTargetEffect>();
                FreePopup._isUnitCharacter = true;
                FreePopup._sprite = "IconStewSpecimensFreeWill";
                FreePopup._name = "Free-Willed";

                //Debug.Log("Orguis | iridescent start");
                if (LoadedDBsHandler.PigmentDB.GetPigment("Iridescent") != null && LoadedDBsHandler.StatusFieldDB._FieldEffects.ContainsKey("Resonance_ID"))
                {
                    Enemy iridescentOrguis = new Enemy("Ephialtes Orguis", "EphialtesOrguis_EN")
                    {
                        Health = 40,
                        HealthColor = LoadedDBsHandler.PigmentDB.GetPigment("Iridescent"),
                        Size = 1,
                        CombatSprite = ResourceLoader.LoadSprite("OrguisTimelineIridescent", new Vector2(0.5f, 0f), 32),
                        OverworldDeadSprite = ResourceLoader.LoadSprite("AnomalyDead", new Vector2(0.5f, 0f), 32),
                        OverworldAliveSprite = ResourceLoader.LoadSprite("OrguisTimelineIridescent", new Vector2(0.5f, 0f), 32),
                        DamageSound = "event:/AAEnemy/LogosDisco/LogosDiscoHurt",
                        DeathSound = "event:/AAEnemy/LogosDisco/LogosDiscoDeath",
                        UnitTypes = ["Logos"],
                    };
                    //Debug.Log("Orguis | prefab");
                    iridescentOrguis.PrepareEnemyPrefab("Assets/Apocrypha_Enemies/Logos_Enemy/OrguisIridescent_Enemy.prefab", AApocrypha.assetBundle, null);

                    FieldEffect_Apply_Effect ResonanceApply = ScriptableObject.CreateInstance<FieldEffect_Apply_Effect>();
                    ResonanceApply._Field = StatusField.GetCustomFieldEffect("Resonance_ID");

                    //Debug.Log("Orguis | euphony");
                    PerformEffectPassiveAbility euphony2 = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
                    euphony2.name = "AA_Euphony2_PA";
                    euphony2._passiveName = "Euphony (2)";
                    euphony2.m_PassiveID = "Euphony";
                    euphony2.passiveIcon = Passives.GetCustomPassive("Euphony2_PA").passiveIcon;
                    euphony2._characterDescription = "On turn start this party member applies 2 Resonance to its current position.";
                    euphony2._enemyDescription = "On turn start this enemy applies 2 Resonance to its current position.";
                    euphony2._triggerOn = [TriggerCalls.OnTurnStart];
                    euphony2.doesPassiveTriggerInformationPanel = true;
                    euphony2.effects =
                    [
                        Effects.GenerateEffect(ResonanceApply, 2, Targeting.Slot_SelfSlot),
                    ];
                    Passives.AddCustomPassiveToPool("AA_Euphony2_PA", "Euphony (2)", euphony2);

                    RemoveFieldEffectEffect RemoveResonance = ScriptableObject.CreateInstance<RemoveFieldEffectEffect>();
                    RemoveResonance._field = StatusField.GetCustomFieldEffect("Resonance_ID");

                    FieldEffect_Apply_Effect ResonanceByPrevious = ScriptableObject.CreateInstance<FieldEffect_Apply_Effect>();
                    ResonanceByPrevious._Field = StatusField.GetCustomFieldEffect("Resonance_ID");
                    ResonanceByPrevious._UsePreviousExitValueAsMultiplier = true;

                    FieldEffect_ApplyWithRandomDistribution_Effect ResonanceByPrevious2 = ScriptableObject.CreateInstance<FieldEffect_ApplyWithRandomDistribution_Effect>();
                    ResonanceByPrevious2.field = StatusField.GetCustomFieldEffect("Resonance_ID");
                    ResonanceByPrevious2.usePrevious = true;

                    //Debug.Log("Orguis | abilities 1");
                    Ability orguisiridleft = new Ability("Visionary", "AApocrypha_OrguisIridLeft_A")
                    {
                        Description = "Move Left, then apply 2 Resonance to the newly Opposing position. This ability assumes that the grid wraps around.",
                        Cost = [Pigments.Grey, LoadedDBsHandler.PigmentDB.GetPigment("Iridescent")],
                        Effects =
                        [
                            Effects.GenerateEffect(SwapLeft, 1, Targeting.Slot_SelfSlot),
                            Effects.GenerateEffect(CheckLeft, 1, Targeting.Slot_SelfSlot, PreviousFalse),
                            Effects.GenerateEffect(ScriptableObject.CreateInstance<MirrorPositionEffect>(), 1, Targeting.Slot_SelfSlot, Effects.CheckMultiplePreviousEffectsCondition([true, false], [1, 2])),
                            Effects.GenerateEffect(ResonateAnim, 1, Targeting.Slot_Front),
                            Effects.GenerateEffect(ResonanceApply, 2, Targeting.Slot_Front),
                        ],
                        Rarity = Rarity.Common,
                        Priority = Priority.VeryFast,
                    };
                    orguisiridleft.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Swap_Left)]);
                    orguisiridleft.AddIntentsToTarget(Targeting.Slot_Front, ["Field_Resonance"]);

                    //Debug.Log("Orguis | abilities 2");
                    Ability orguisiridright = new Ability("Luminary", "AApocrypha_OrguisIridRight_A")
                    {
                        Description = "Move Right, then apply 2 Resonance to the newly Opposing position. This ability assumes that the grid wraps around.",
                        Cost = [LoadedDBsHandler.PigmentDB.GetPigment("Iridescent"), Pigments.Grey],
                        Effects =
                        [
                            Effects.GenerateEffect(SwapRight, 1, Targeting.Slot_SelfSlot),
                            Effects.GenerateEffect(CheckRight, 1, Targeting.Slot_SelfSlot, PreviousFalse),
                            Effects.GenerateEffect(ScriptableObject.CreateInstance<MirrorPositionEffect>(), 1, Targeting.Slot_SelfSlot, Effects.CheckMultiplePreviousEffectsCondition([true, false], [1, 2])),
                            Effects.GenerateEffect(ResonateAnim, 1, Targeting.Slot_Front),
                            Effects.GenerateEffect(ResonanceApply, 2, Targeting.Slot_Front),
                        ],
                        Rarity = Rarity.Common,
                        Priority = Priority.VeryFast,
                    };
                    orguisiridright.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Swap_Right)]);
                    orguisiridright.AddIntentsToTarget(Targeting.Slot_Front, ["Field_Resonance"]);

                    //Debug.Log("Orguis | abilities 3 | stat");
                    StatusEffect_ApplyWithRandomDistribution_Effect DisjuncterRandom = ScriptableObject.CreateInstance<StatusEffect_ApplyWithRandomDistribution_Effect>();
                    DisjuncterRandom.status = StatusField.GetCustomStatusEffect("Disjunct_ID");
                    DisjuncterRandom.usePrevious = true;

                    //Debug.Log("Orguis | abilities 4");
                    Ability promise = new Ability("I Will It, Promise Greatness", "AApocrypha_OrguisPromiseGreatnessIrid_A")
                    {
                        Description = "Heal the Opposing party member, then deal damage to them equal to twice the amount of effective healing and grant them Free Will. If they already had Free Will, remove it when performing this ability.",
                        Cost = [LoadedDBsHandler.PigmentDB.GetPigment("Iridescent"), LoadedDBsHandler.PigmentDB.GetPigment("Iridescent"), LoadedDBsHandler.PigmentDB.GetPigment("Iridescent"), LoadedDBsHandler.PigmentDB.GetPigment("Iridescent")],
                        Visuals = CustomVisuals.Whispers,
                        AnimationTarget = Targeting.Slot_Front,
                        Effects =
                        [
                            Effects.GenerateEffect(ScriptableObject.CreateInstance<HealEffect>(), 5, Targeting.Slot_Front),
                            Effects.GenerateEffect(DamageByPrevious, 2, Targeting.Slot_Front, Effects.CheckPreviousEffectCondition(true, 1)),
                            Effects.GenerateEffect(ReFree, 1, Targeting.Slot_Front),
                            Effects.GenerateEffect(FreePopup, 1, Targeting.Slot_Front, Effects.CheckPreviousEffectCondition(true, 1)),
                            Effects.GenerateEffect(UnFree, 1, Targeting.Slot_Front, Effects.CheckPreviousEffectCondition(false, 2)),
                        ],
                        Rarity = Rarity.Impossible,
                        Priority = Priority.Fast,
                    };
                    promise.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Heal_5_10), nameof(IntentType_GameIDs.Damage_7_10), "AA_AddPassive", "AA_RemPassive"]);

                    //Debug.Log("Orguis | abilities 3 | ability");
                    Ability terrify = new Ability("I Will It, Terrify Them", "AApocrypha_OrguisTerrifyThem_A")
                    {
                        Description = "Deal damage to the Opposing party member equal to their missing health, then apply the damage dealt as Disjunct spread across all other party members.",
                        Cost = [LoadedDBsHandler.PigmentDB.GetPigment("Iridescent"), LoadedDBsHandler.PigmentDB.GetPigment("Iridescent"), LoadedDBsHandler.PigmentDB.GetPigment("Iridescent"), LoadedDBsHandler.PigmentDB.GetPigment("Iridescent")],
                        Visuals = CustomVisuals.Whispers,
                        AnimationTarget = Targeting.Slot_Front,
                        Effects =
                        [
                            Effects.GenerateEffect(ScriptableObject.CreateInstance<MissingHealthCheckEffect>(), 1, Targeting.Slot_Front),
                            Effects.GenerateEffect(DamageByPrevious, 1, Targeting.Slot_Front, Effects.CheckPreviousEffectCondition(true, 1)),
                            Effects.GenerateEffect(DisjuncterRandom, 1, Targeting.Slot_OpponentAllSlots, Effects.CheckPreviousEffectCondition(true, 1)),
                        ],
                        Rarity = Rarity.Impossible,
                        Priority = Priority.Fast,
                    };
                    //Debug.Log("Orguis | abilities 3 | intents");
                    terrify.AddIntentsToTarget(Targeting.Slot_Front, ["Damage_Prop"]);
                    terrify.AddIntentsToTarget(Targeting.GenerateSlotTarget([-4, -3, -2, -1, 1, 2, 3, 4], false), ["Status_Disjunct"]);

                    //Debug.Log("Orguis | abilities 5");
                    Ability breakreality = new Ability("I Will It, Break Reality", "AApocrypha_OrguisBreakReality_A")
                    {
                        Description = "Remove all Resonance from All positions and apply it to this enemy.\nRemove all Resonance from this enemy's position and distribute it randomly to All occupied positions.",
                        Cost = [LoadedDBsHandler.PigmentDB.GetPigment("Iridescent"), LoadedDBsHandler.PigmentDB.GetPigment("Iridescent"), LoadedDBsHandler.PigmentDB.GetPigment("Iridescent"), LoadedDBsHandler.PigmentDB.GetPigment("Iridescent")],
                        Visuals = GlitchVisuals,
                        AnimationTarget = AllUnitsTargeting,
                        Effects =
                        [
                            Effects.GenerateEffect(RemoveResonance, 1, Targeting.Slot_AllyAllSlots),
                            Effects.GenerateEffect(ResonanceByPrevious, 1, Targeting.Slot_SelfSlot),
                            Effects.GenerateEffect(RemoveResonance, 1, Targeting.Slot_OpponentAllSlots),
                            Effects.GenerateEffect(ResonanceByPrevious, 1, Targeting.Slot_SelfSlot),
                            Effects.GenerateEffect(RemoveResonance, 1, Targeting.Slot_SelfSlot),
                            Effects.GenerateEffect(ResonanceByPrevious2, 1, AllUnitsTargeting),
                        ],
                        Rarity = Rarity.Impossible,
                        Priority = Priority.Fast,
                    };
                    breakreality.AddIntentsToTarget(Targeting.Slot_AllyAllSlots, ["Rem_Field_Resonance", "Field_Resonance"]);
                    breakreality.AddIntentsToTarget(Targeting.Slot_OpponentAllSlots, ["Rem_Field_Resonance", "Field_Resonance"]);

                    //Debug.Log("Orguis | setup");
                    ExtraAbilityInfo promiseextra = new()
                    {
                        ability = promise.GenerateEnemyAbility().ability,
                        rarity = Rarity.Uncommon,
                    };

                    ExtraAbilityInfo terrifyextra = new()
                    {
                        ability = terrify.GenerateEnemyAbility().ability,
                        rarity = Rarity.Uncommon,
                    };

                    ExtraAbilityInfo realityextra = new()
                    {
                        ability = breakreality.GenerateEnemyAbility().ability,
                        rarity = Rarity.Uncommon,
                    };

                    iridescentOrguis.AddPassives([Passives.MultiAttack3, Passives.GetCustomPassive("AA_Euphony2_PA"), Passives.GetCustomPassive("AA_CondenseIridescent_PA"), CustomPassives.BonusSuiteGenerator([promiseextra, terrifyextra, realityextra])]);

                    iridescentOrguis.AddEnemyAbilities(
                    [
                        orguisiridleft,
                        orguisiridright,
                    ]);

                    //Debug.Log("Orguis | add");
                    iridescentOrguis.AddEnemy(true, false, false);
                }

                if (LoadedDBsHandler.PigmentDB.GetPigment("Clusterfuck") != null && LoadedDBsHandler.StatusFieldDB._FieldEffects.ContainsKey("Segfault_ID"))
                {
                    Enemy clusterfuckOrguis = new Enemy("Apatelos Orguis", "ApatelosOrguis_EN")
                    {
                        Health = 40,
                        HealthColor = LoadedDBsHandler.PigmentDB.GetPigment("Clusterfuck"),
                        Size = 1,
                        CombatSprite = ResourceLoader.LoadSprite("OrguisTimelineClusterfuck", new Vector2(0.5f, 0f), 32),
                        OverworldDeadSprite = ResourceLoader.LoadSprite("AnomalyDead", new Vector2(0.5f, 0f), 32),
                        OverworldAliveSprite = ResourceLoader.LoadSprite("OrguisTimelineClusterfuck", new Vector2(0.5f, 0f), 32),
                        DamageSound = "event:/AAEnemy/LogosDisco/LogosDiscoHurt",
                        DeathSound = "event:/AAEnemy/LogosDisco/LogosDiscoDeath",
                        UnitTypes = ["Logos"],
                    };
                    clusterfuckOrguis.PrepareEnemyPrefab("Assets/Apocrypha_Enemies/Logos_Enemy/OrguisClusterfuck_Enemy.prefab", AApocrypha.assetBundle, null);

                    FieldEffect_Apply_Effect SegfaultApply = ScriptableObject.CreateInstance<FieldEffect_Apply_Effect>();
                    SegfaultApply._Field = StatusField.GetCustomFieldEffect("Segfault_ID");

                    //Debug.Log("Orguis | euphony");
                    PerformEffectPassiveAbility fragmentation1 = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
                    fragmentation1.name = "AA_Fragmentation1_PA";
                    fragmentation1._passiveName = "Fragmentation (1)";
                    fragmentation1.m_PassiveID = "Fragmentation";
                    fragmentation1.passiveIcon = ResourceLoader.LoadSprite("IconFragmentation");
                    fragmentation1._characterDescription = "On turn start this party member applies 1 Segfault to its current position.";
                    fragmentation1._enemyDescription = "On turn start this enemy applies 1 Segfault to its current position.";
                    fragmentation1._triggerOn = [TriggerCalls.OnTurnStart];
                    fragmentation1.doesPassiveTriggerInformationPanel = true;
                    fragmentation1.effects =
                    [
                        Effects.GenerateEffect(SegfaultApply, 1, Targeting.Slot_SelfSlot),
                    ];
                    Passives.AddCustomPassiveToPool("AA_Fragmentation1_PA", "Fragmentation (1)", fragmentation1);

                    RemoveFieldEffectEffect RemoveSegfault = ScriptableObject.CreateInstance<RemoveFieldEffectEffect>();
                    RemoveSegfault._field = StatusField.GetCustomFieldEffect("Segfault_ID");

                    FieldEffect_Apply_Effect SegfaultByPrevious = ScriptableObject.CreateInstance<FieldEffect_Apply_Effect>();
                    SegfaultByPrevious._Field = StatusField.GetCustomFieldEffect("Segfault_ID");
                    SegfaultByPrevious._UsePreviousExitValueAsMultiplier = true;

                    AnimationVisualsEffect GlitchAnim1 = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
                    GlitchAnim1._visuals = GlitchVisuals;
                    GlitchAnim1._animationTarget = Targeting.Slot_OpponentSides;

                    AnimationVisualsEffect GlitchAnim2 = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
                    GlitchAnim2._visuals = GlitchVisuals;
                    GlitchAnim2._animationTarget = Targeting.Slot_OpponentFarSides;

                    AnimationVisualsEffect GlitchAnim3 = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
                    GlitchAnim3._visuals = GlitchVisuals;
                    GlitchAnim3._animationTarget = Targeting.GenerateSlotTarget([-3, 3], false);

                    AnimationVisualsEffect GlitchAnim4 = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
                    GlitchAnim4._visuals = GlitchVisuals;
                    GlitchAnim4._animationTarget = Targeting.GenerateSlotTarget([-4, 4], false);

                    Ability orguisclusterleft = new Ability("eDetdmne", "AApocrypha_OrguisClusterLeft_A") //Demented
                    {
                        Description = "Move Left, then apply 1 Segfault to the newly Opposing position. This ability assumes that the grid wraps around.",
                        Cost = [Pigments.Grey, LoadedDBsHandler.PigmentDB.GetPigment("Clusterfuck")],
                        Effects =
                        [
                            Effects.GenerateEffect(SwapLeft, 1, Targeting.Slot_SelfSlot),
                            Effects.GenerateEffect(CheckLeft, 1, Targeting.Slot_SelfSlot, PreviousFalse),
                            Effects.GenerateEffect(ScriptableObject.CreateInstance<MirrorPositionEffect>(), 1, Targeting.Slot_SelfSlot, Effects.CheckMultiplePreviousEffectsCondition([true, false], [1, 2])),
                            Effects.GenerateEffect(GlitchAnim, 1, Targeting.Slot_Front),
                            Effects.GenerateEffect(SegfaultApply, 1, Targeting.Slot_Front),
                        ],
                        Rarity = Rarity.Common,
                        Priority = Priority.VeryFast,
                    };
                    orguisclusterleft.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Swap_Left)]);
                    orguisclusterleft.AddIntentsToTarget(Targeting.Slot_Front, ["Field_Segfault"]);

                    //Debug.Log("Orguis | abilities 2");
                    Ability orguisclusterright = new Ability("liouseDir", "AApocrypha_OrguisClusterRight_A") //Delirious
                    {
                        Description = "Move Right, then apply 1 Segfault to the newly Opposing position. This ability assumes that the grid wraps around.",
                        Cost = [LoadedDBsHandler.PigmentDB.GetPigment("Clusterfuck"), Pigments.Grey],
                        Effects =
                        [
                            Effects.GenerateEffect(SwapRight, 1, Targeting.Slot_SelfSlot),
                            Effects.GenerateEffect(CheckRight, 1, Targeting.Slot_SelfSlot, PreviousFalse),
                            Effects.GenerateEffect(ScriptableObject.CreateInstance<MirrorPositionEffect>(), 1, Targeting.Slot_SelfSlot, Effects.CheckMultiplePreviousEffectsCondition([true, false], [1, 2])),
                            Effects.GenerateEffect(GlitchAnim, 1, Targeting.Slot_Front),
                            Effects.GenerateEffect(SegfaultApply, 1, Targeting.Slot_Front),
                        ],
                        Rarity = Rarity.Common,
                        Priority = Priority.VeryFast,
                    };
                    orguisclusterright.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Swap_Right)]);
                    orguisclusterright.AddIntentsToTarget(Targeting.Slot_Front, ["Field_Segfault"]);

                    DamageEffect ShieldPiercing = ScriptableObject.CreateInstance<DamageEffect>();
                    ShieldPiercing._ignoreShield = true;

                    HealEffect HealByPrevious = ScriptableObject.CreateInstance<HealEffect>();
                    HealByPrevious.usePreviousExitValue = true;

                    FieldEffectCheckEffect SegfaultCheck = ScriptableObject.CreateInstance<FieldEffectCheckEffect>();
                    SegfaultCheck._fields = [StatusField.GetCustomFieldEffect("Segfault_ID")];

                    Ability promise = new Ability("WlI itlI, srePem isasorenGt", "AApocrypha_OrguisPromiseGreatnessCluster_A")
                    {
                        Description = "Heal the Opposing party member, then deal damage to them equal to twice the amount of effective healing and grant them Free Will. If they already had Free Will, remove it when performing this ability.",
                        Cost = [LoadedDBsHandler.PigmentDB.GetPigment("Clusterfuck"), LoadedDBsHandler.PigmentDB.GetPigment("Clusterfuck"), LoadedDBsHandler.PigmentDB.GetPigment("Clusterfuck"), LoadedDBsHandler.PigmentDB.GetPigment("Clusterfuck")],
                        Visuals = CustomVisuals.Whispers,
                        AnimationTarget = Targeting.Slot_Front,
                        Effects =
                        [
                            Effects.GenerateEffect(ScriptableObject.CreateInstance<HealEffect>(), 5, Targeting.Slot_Front),
                            Effects.GenerateEffect(DamageByPrevious, 2, Targeting.Slot_Front, Effects.CheckPreviousEffectCondition(true, 1)),
                            Effects.GenerateEffect(ReFree, 1, Targeting.Slot_Front),
                            Effects.GenerateEffect(FreePopup, 1, Targeting.Slot_Front, Effects.CheckPreviousEffectCondition(true, 1)),
                            Effects.GenerateEffect(UnFree, 1, Targeting.Slot_Front, Effects.CheckPreviousEffectCondition(false, 2)),
                        ],
                        Rarity = Rarity.Impossible,
                        Priority = Priority.Fast,
                    };
                    promise.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Heal_5_10), nameof(IntentType_GameIDs.Damage_7_10), "AA_AddPassive", "AA_RemPassive"]);

                    Ability overwrite = new Ability("WlI itlI, etevti rODawra", "AApocrypha_OrguisOverwriteData_A")
                    {
                        Description = "Attempt to deal a Little damage to the Opposing slot. Damage cascades across empty spaces to the Left and Right, doubling until it hits a party member.",
                        Cost = [LoadedDBsHandler.PigmentDB.GetPigment("Clusterfuck"), LoadedDBsHandler.PigmentDB.GetPigment("Clusterfuck"), LoadedDBsHandler.PigmentDB.GetPigment("Clusterfuck"), LoadedDBsHandler.PigmentDB.GetPigment("Clusterfuck")],
                        Effects =
                        [
                            Effects.GenerateEffect(ScriptableObject.CreateInstance<CheckHasUnitEffect>(), 1, Targeting.Slot_Front),
                            Effects.GenerateEffect(GlitchAnim, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 1)),
                            Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 2, Targeting.Slot_Front, Effects.CheckPreviousEffectCondition(true, 2)),
                            Effects.GenerateEffect(ScriptableObject.CreateInstance<CheckHasUnitEffect>(), 1, Targeting.Slot_OpponentSides, Effects.CheckPreviousEffectCondition(false, 3)),
                            Effects.GenerateEffect(GlitchAnim1, 1, Targeting.Slot_SelfSlot, Effects.CheckMultiplePreviousEffectsCondition([true, false], [1, 4])),
                            Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 4, Targeting.Slot_OpponentSides, Effects.CheckMultiplePreviousEffectsCondition([true, false], [2, 5])),
                            Effects.GenerateEffect(ScriptableObject.CreateInstance<CheckHasUnitEffect>(), 1, Targeting.Slot_OpponentFarSides, Effects.CheckMultiplePreviousEffectsCondition([false, false], [3, 6])),
                            Effects.GenerateEffect(GlitchAnim2, 1, Targeting.Slot_SelfSlot, Effects.CheckMultiplePreviousEffectsCondition([true, false, false], [1, 4, 7])),
                            Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 8, Targeting.Slot_OpponentFarSides, Effects.CheckMultiplePreviousEffectsCondition([true, false, false], [2, 5, 8])),
                            Effects.GenerateEffect(ScriptableObject.CreateInstance<CheckHasUnitEffect>(), 1, Targeting.GenerateSlotTarget([-3, 3], false), Effects.CheckMultiplePreviousEffectsCondition([false, false, false], [3, 6, 9])),
                            Effects.GenerateEffect(GlitchAnim3, 1, Targeting.Slot_SelfSlot, Effects.CheckMultiplePreviousEffectsCondition([true, false, false, false], [1, 4, 7, 10])),
                            Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 16, Targeting.GenerateSlotTarget([-3, 3], false), Effects.CheckMultiplePreviousEffectsCondition([true, false, false, false], [2, 5, 8, 11])),
                            Effects.GenerateEffect(ScriptableObject.CreateInstance<CheckHasUnitEffect>(), 1, Targeting.GenerateSlotTarget([-4, 4], false), Effects.CheckMultiplePreviousEffectsCondition([false, false, false, false], [3, 6, 9, 12])),
                            Effects.GenerateEffect(GlitchAnim4, 1, Targeting.Slot_SelfSlot, Effects.CheckMultiplePreviousEffectsCondition([true, false, false, false, false], [1, 4, 7, 10, 13])),
                            Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 32, Targeting.GenerateSlotTarget([-4, 4], false), Effects.CheckMultiplePreviousEffectsCondition([true, false, false, false, false], [2, 5, 8, 11, 14])),
                        ],
                        Rarity = Rarity.Impossible,
                        Priority = Priority.Fast,
                    };
                    overwrite.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Damage_1_2)]);
                    overwrite.AddIntentsToTarget(Targeting.Slot_OpponentSides, [nameof(IntentType_GameIDs.Damage_3_6)]);
                    overwrite.AddIntentsToTarget(Targeting.Slot_OpponentFarSides, [nameof(IntentType_GameIDs.Damage_7_10)]);
                    overwrite.AddIntentsToTarget(Targeting.GenerateSlotTarget([-3, 3], false), [nameof(IntentType_GameIDs.Damage_16_20)]);
                    overwrite.AddIntentsToTarget(Targeting.GenerateSlotTarget([-4, 4], false), [nameof(IntentType_GameIDs.Damage_21)]);

                    TargetPerformEffectViaSubaction InspireSubaction = ScriptableObject.CreateInstance<TargetPerformEffectViaSubaction>();
                    InspireSubaction.effects =
                    [
                        Effects.GenerateEffect(ShieldPiercing, 1, Targeting.Slot_SelfSlot),
                        Effects.GenerateEffect(HealByPrevious, 2, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 1)),
                    ];

                    PerformEffectViaSubaction OmNomNom = ScriptableObject.CreateInstance<PerformEffectViaSubaction>();
                    OmNomNom.effects = [Effects.GenerateEffect(ScriptableObject.CreateInstance<ConsumeRandomManaEffect>(), 5, Targeting.Slot_SelfSlot)];

                    Ability inspire = new Ability("WlI itlI, snerInM daspise", "AApocrypha_OrguisInspireMadness_A")
                    {
                        Description = "Double all Segfault on all positions. Make all enemies deal 1 shield-piercing direct damage to themselves, then heal themselves for twice the damage dealt." +
                        "\nConsume 5 pigment.",
                        Cost = [LoadedDBsHandler.PigmentDB.GetPigment("Clusterfuck"), LoadedDBsHandler.PigmentDB.GetPigment("Clusterfuck"), LoadedDBsHandler.PigmentDB.GetPigment("Clusterfuck"), LoadedDBsHandler.PigmentDB.GetPigment("Clusterfuck")],
                        Visuals = GlitchVisuals,
                        AnimationTarget = Targeting.Slot_SelfSlot,
                        Effects =
                        [
                            Effects.GenerateEffect(RemoveSegfault, 1, Targeting.GenerateGenericTarget([0], true)),
                            Effects.GenerateEffect(SegfaultByPrevious, 2, Targeting.GenerateGenericTarget([0], true), Effects.CheckPreviousEffectCondition(true, 1)),
                            Effects.GenerateEffect(RemoveSegfault, 1, Targeting.GenerateGenericTarget([1], true)),
                            Effects.GenerateEffect(SegfaultByPrevious, 2, Targeting.GenerateGenericTarget([1], true), Effects.CheckPreviousEffectCondition(true, 1)),
                            Effects.GenerateEffect(RemoveSegfault, 1, Targeting.GenerateGenericTarget([2], true)),
                            Effects.GenerateEffect(SegfaultByPrevious, 2, Targeting.GenerateGenericTarget([2], true), Effects.CheckPreviousEffectCondition(true, 1)),
                            Effects.GenerateEffect(RemoveSegfault, 1, Targeting.GenerateGenericTarget([3], true)),
                            Effects.GenerateEffect(SegfaultByPrevious, 2, Targeting.GenerateGenericTarget([3], true), Effects.CheckPreviousEffectCondition(true, 1)),
                            Effects.GenerateEffect(RemoveSegfault, 1, Targeting.GenerateGenericTarget([4], true)),
                            Effects.GenerateEffect(SegfaultByPrevious, 2, Targeting.GenerateGenericTarget([4], true), Effects.CheckPreviousEffectCondition(true, 1)),
                            Effects.GenerateEffect(RemoveSegfault, 1, Targeting.GenerateGenericTarget([0], false)),
                            Effects.GenerateEffect(SegfaultByPrevious, 2, Targeting.GenerateGenericTarget([0], false), Effects.CheckPreviousEffectCondition(true, 1)),
                            Effects.GenerateEffect(RemoveSegfault, 1, Targeting.GenerateGenericTarget([1], false)),
                            Effects.GenerateEffect(SegfaultByPrevious, 2, Targeting.GenerateGenericTarget([1], false), Effects.CheckPreviousEffectCondition(true, 1)),
                            Effects.GenerateEffect(RemoveSegfault, 1, Targeting.GenerateGenericTarget([2], false)),
                            Effects.GenerateEffect(SegfaultByPrevious, 2, Targeting.GenerateGenericTarget([2], false), Effects.CheckPreviousEffectCondition(true, 1)),
                            Effects.GenerateEffect(RemoveSegfault, 1, Targeting.GenerateGenericTarget([3], false)),
                            Effects.GenerateEffect(SegfaultByPrevious, 2, Targeting.GenerateGenericTarget([3], false), Effects.CheckPreviousEffectCondition(true, 1)),
                            Effects.GenerateEffect(RemoveSegfault, 1, Targeting.GenerateGenericTarget([4], false)),
                            Effects.GenerateEffect(SegfaultByPrevious, 2, Targeting.GenerateGenericTarget([4], false), Effects.CheckPreviousEffectCondition(true, 1)),
                            Effects.GenerateEffect(InspireSubaction, 1, Targeting.Unit_AllAllies),
                            Effects.GenerateEffect(OmNomNom),
                        ],
                        Rarity = Rarity.Impossible,
                        Priority = Priority.Fast,
                    };
                    inspire.AddIntentsToTarget(Targeting.GenerateSlotTarget([-4, -3, -2, -1, 0, 1, 2, 3, 4], true), ["Field_Segfault"]);
                    inspire.AddIntentsToTarget(Targeting.GenerateSlotTarget([-4, -3, -2, -1, 0, 1, 2, 3, 4], false), ["Field_Segfault"]);
                    inspire.AddIntentsToTarget(Targeting.Unit_AllAllies, [nameof(IntentType_GameIDs.Damage_1_2), nameof(IntentType_GameIDs.Heal_1_4)]);

                    ExtraAbilityInfo promiseextra = new()
                    {
                        ability = promise.GenerateEnemyAbility().ability,
                        rarity = Rarity.Uncommon,
                    };

                    ExtraAbilityInfo overwriteextra = new()
                    {
                        ability = overwrite.GenerateEnemyAbility().ability,
                        rarity = Rarity.Uncommon,
                    };

                    ExtraAbilityInfo inspireextra = new()
                    {
                        ability = inspire.GenerateEnemyAbility().ability,
                        rarity = Rarity.Uncommon,
                    };

                    clusterfuckOrguis.AddPassives([Passives.MultiAttack3, Passives.GetCustomPassive("AA_Fragmentation1_PA"), Passives.GetCustomPassive("AA_CondenseClusterfuck_PA"), CustomPassives.BonusSuiteGenerator([promiseextra, overwriteextra, inspireextra])]);

                    clusterfuckOrguis.AddEnemyAbilities([
                        orguisclusterleft.GenerateEnemyAbility(true),
                        orguisclusterright.GenerateEnemyAbility(true),
                    ]);

                    clusterfuckOrguis.AddEnemy(true, false, false);
                }

                if (LoadedDBsHandler.PigmentDB.GetPigment("EntropicBase") != null && LoadedDBsHandler.StatusFieldDB._FieldEffects.ContainsKey("Gravity_ID"))
                {
                    Enemy entropicOrguis = new Enemy("Panoptic Orguis", "PanopticOrguis_EN")
                    {
                        Health = 40,
                        HealthColor = LoadedDBsHandler.PigmentDB.GetPigment("EntropicBase"),
                        Size = 1,
                        CombatSprite = ResourceLoader.LoadSprite("OrguisTimelineEntropic", new Vector2(0.5f, 0f), 32),
                        OverworldDeadSprite = ResourceLoader.LoadSprite("AnomalyDead", new Vector2(0.5f, 0f), 32),
                        OverworldAliveSprite = ResourceLoader.LoadSprite("OrguisTimelineEntropic", new Vector2(0.5f, 0f), 32),
                        DamageSound = "event:/AAEnemy/LogosDisco/LogosDiscoHurt",
                        DeathSound = "event:/AAEnemy/LogosDisco/LogosDiscoDeath",
                        UnitTypes = ["Logos"],
                    };

                    entropicOrguis.PrepareEnemyPrefab("Assets/Apocrypha_Enemies/Logos_Enemy/OrguisEntropic_Enemy.prefab", AApocrypha.assetBundle, null);

                    FieldEffect_Apply_Effect GravityApply = ScriptableObject.CreateInstance<FieldEffect_Apply_Effect>();
                    GravityApply._Field = StatusField.GetCustomFieldEffect("Gravity_ID");

                    //Debug.Log("Orguis | euphony");
                    PerformEffectPassiveAbility accretion1 = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
                    accretion1.name = "AA_Accretion1_PA";
                    accretion1._passiveName = "Accretion (1)";
                    accretion1.m_PassiveID = "Accretion";
                    accretion1.passiveIcon = StatusField.GetCustomFieldEffect("Gravity_ID")._EffectInfo.icon;
                    accretion1._characterDescription = "On turn start this party member applies 1 Gravity to its current position.";
                    accretion1._enemyDescription = "On turn start this enemy applies 1 Gravity to its current position.";
                    accretion1._triggerOn = [TriggerCalls.OnTurnStart];
                    accretion1.doesPassiveTriggerInformationPanel = true;
                    accretion1.effects =
                    [
                        Effects.GenerateEffect(GravityApply, 1, Targeting.Slot_SelfSlot),
                    ];
                    Passives.AddCustomPassiveToPool("AA_Accretion1_PA", "Accretion (1)", accretion1);

                    RemoveFieldEffectEffect RemoveGravity = ScriptableObject.CreateInstance<RemoveFieldEffectEffect>();
                    RemoveGravity._field = StatusField.GetCustomFieldEffect("Gravity_ID");

                    FieldEffect_Apply_Effect GravityByPrevious = ScriptableObject.CreateInstance<FieldEffect_Apply_Effect>();
                    GravityByPrevious._Field = StatusField.GetCustomFieldEffect("Gravity_ID");
                    GravityByPrevious._UsePreviousExitValueAsMultiplier = true;

                    Ability orguisentropicleft = new Ability("Conspiracy", "AApocrypha_OrguisEntropicLeft_A")
                    {
                        Description = "Move Left, then apply 1 Gravity to the newly Opposing position. This ability assumes that the grid wraps around.",
                        Cost = [Pigments.Grey, LoadedDBsHandler.PigmentDB.GetPigment("EntropicBase")],
                        Effects =
                        [
                            Effects.GenerateEffect(SwapLeft, 1, Targeting.Slot_SelfSlot),
                            Effects.GenerateEffect(CheckLeft, 1, Targeting.Slot_SelfSlot, PreviousFalse),
                            Effects.GenerateEffect(ScriptableObject.CreateInstance<MirrorPositionEffect>(), 1, Targeting.Slot_SelfSlot, Effects.CheckMultiplePreviousEffectsCondition([true, false], [1, 2])),
                            Effects.GenerateEffect(LookAnim, 1, Targeting.Slot_Front),
                            Effects.GenerateEffect(GravityApply, 1, Targeting.Slot_Front),
                        ],
                        Rarity = Rarity.Common,
                        Priority = Priority.VeryFast,
                    };
                    orguisentropicleft.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Swap_Left)]);
                    orguisentropicleft.AddIntentsToTarget(Targeting.Slot_Front, ["Field_Gravity"]);

                    //Debug.Log("Orguis | abilities 2");
                    Ability orguisentropicright = new Ability("Controversy", "AApocrypha_OrguisEntropicRight_A")
                    {
                        Description = "Move Right, then apply 1 Gravity to the newly Opposing position. This ability assumes that the grid wraps around.",
                        Cost = [LoadedDBsHandler.PigmentDB.GetPigment("EntropicBase"), Pigments.Grey],
                        Effects =
                        [
                            Effects.GenerateEffect(SwapRight, 1, Targeting.Slot_SelfSlot),
                            Effects.GenerateEffect(CheckRight, 1, Targeting.Slot_SelfSlot, PreviousFalse),
                            Effects.GenerateEffect(ScriptableObject.CreateInstance<MirrorPositionEffect>(), 1, Targeting.Slot_SelfSlot, Effects.CheckMultiplePreviousEffectsCondition([true, false], [1, 2])),
                            Effects.GenerateEffect(LookAnim, 1, Targeting.Slot_Front),
                            Effects.GenerateEffect(GravityApply, 1, Targeting.Slot_Front),
                        ],
                        Rarity = Rarity.Common,
                        Priority = Priority.VeryFast,
                    };
                    orguisentropicright.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Swap_Right)]);
                    orguisentropicright.AddIntentsToTarget(Targeting.Slot_Front, ["Field_Gravity"]);

                    Ability promise = new Ability("I Will It, Promise Greatness", "AApocrypha_OrguisPromiseGreatnessEntropic_A")
                    {
                        Description = "Heal the Opposing party member, then deal damage to them equal to twice the amount of effective healing and grant them Free Will. If they already had Free Will, remove it when performing this ability.",
                        Cost = [LoadedDBsHandler.PigmentDB.GetPigment("EntropicBase"), LoadedDBsHandler.PigmentDB.GetPigment("EntropicBase"), LoadedDBsHandler.PigmentDB.GetPigment("EntropicBase"), LoadedDBsHandler.PigmentDB.GetPigment("EntropicBase")],
                        Visuals = CustomVisuals.Whispers,
                        AnimationTarget = Targeting.Slot_Front,
                        Effects =
                        [
                            Effects.GenerateEffect(ScriptableObject.CreateInstance<HealEffect>(), 5, Targeting.Slot_Front),
                            Effects.GenerateEffect(DamageByPrevious, 2, Targeting.Slot_Front, Effects.CheckPreviousEffectCondition(true, 1)),
                            Effects.GenerateEffect(ReFree, 1, Targeting.Slot_Front),
                            Effects.GenerateEffect(FreePopup, 1, Targeting.Slot_Front, Effects.CheckPreviousEffectCondition(true, 1)),
                            Effects.GenerateEffect(UnFree, 1, Targeting.Slot_Front, Effects.CheckPreviousEffectCondition(false, 2)),
                        ],
                        Rarity = Rarity.Impossible,
                        Priority = Priority.Fast,
                    };
                    promise.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Heal_5_10), nameof(IntentType_GameIDs.Damage_7_10), "AA_AddPassive", "AA_RemPassive"]);

                    ExtraAbilityInfo promiseextra = new()
                    {
                        ability = promise.GenerateEnemyAbility().ability,
                        rarity = Rarity.Uncommon,
                    };

                    entropicOrguis.AddPassives([Passives.MultiAttack3, Passives.GetCustomPassive("AA_Accretion1_PA"), Passives.GetCustomPassive("FixedPoint_PA"), Passives.GetCustomPassive("AA_CondenseEntropic_PA"), CustomPassives.BonusSuiteGenerator([promiseextra])]);

                    entropicOrguis.AddEnemyAbilities([
                        orguisentropicleft.GenerateEnemyAbility(true),
                        orguisentropicright.GenerateEnemyAbility(true),
                    ]);

                    entropicOrguis.AddEnemy(false, false, false);
                }

                if (LoadedDBsHandler.PigmentDB.GetPigment("White") != null && LoadedDBsHandler.StatusFieldDB._FieldEffects.ContainsKey("Absolution_ID") && LoadedDBsHandler.StatusFieldDB._StatusEffects.ContainsKey("Atrophy_ID"))
                {
                    Enemy whiteOrguis = new Enemy("Hamalat Orguis", "HamalatOrguis_EN")
                    {
                        Health = 40,
                        HealthColor = LoadedDBsHandler.PigmentDB.GetPigment("White"),
                        Size = 1,
                        CombatSprite = ResourceLoader.LoadSprite("OrguisTimelineBase", new Vector2(0.5f, 0f), 32),
                        OverworldDeadSprite = ResourceLoader.LoadSprite("AnomalyDead", new Vector2(0.5f, 0f), 32),
                        OverworldAliveSprite = ResourceLoader.LoadSprite("OrguisTimelineBase", new Vector2(0.5f, 0f), 32),
                        DamageSound = "event:/AAEnemy/LogosDisco/LogosDiscoHurt",
                        DeathSound = "event:/AAEnemy/LogosDisco/LogosDiscoDeath",
                        UnitTypes = ["Logos"],
                    };

                    whiteOrguis.PrepareEnemyPrefab("Assets/Apocrypha_Enemies/Logos_Enemy/OrguisWhite_Enemy.prefab", AApocrypha.assetBundle, null);

                    FieldEffect_Apply_Effect AbsolutionApply = ScriptableObject.CreateInstance<FieldEffect_Apply_Effect>();
                    AbsolutionApply._Field = StatusField.GetCustomFieldEffect("Absolution_ID");

                    RemoveFieldEffectEffect RemoveAbsolution = ScriptableObject.CreateInstance<RemoveFieldEffectEffect>();
                    RemoveAbsolution._field = StatusField.GetCustomFieldEffect("Absolution_ID");

                    FieldEffect_Apply_Effect AbsolutionByPrevious = ScriptableObject.CreateInstance<FieldEffect_Apply_Effect>();
                    AbsolutionByPrevious._Field = StatusField.GetCustomFieldEffect("Absolution_ID");
                    AbsolutionByPrevious._UsePreviousExitValueAsMultiplier = true; 

                    FieldEffect_ApplyWithEvenDistributionAllSlots_Effect AbsolutionEvenSpreadByPrevious = ScriptableObject.CreateInstance<FieldEffect_ApplyWithEvenDistributionAllSlots_Effect>();
                    AbsolutionEvenSpreadByPrevious.field = StatusField.GetCustomFieldEffect("Absolution_ID");
                    AbsolutionEvenSpreadByPrevious.usePrevious = true;
                    AbsolutionEvenSpreadByPrevious._includeCaster = false;

                    FieldEffect_ApplyWithEvenDistributionAllSlots_Effect AbsolutionEvenSpreadByPreviousCast = ScriptableObject.CreateInstance<FieldEffect_ApplyWithEvenDistributionAllSlots_Effect>();
                    AbsolutionEvenSpreadByPreviousCast.field = StatusField.GetCustomFieldEffect("Absolution_ID");
                    AbsolutionEvenSpreadByPreviousCast.usePrevious = true;
                    AbsolutionEvenSpreadByPreviousCast._includeCaster = true;

                    RemoveAmountStatusEffectEffect HalveAtrophy = ScriptableObject.CreateInstance<RemoveAmountStatusEffectEffect>();
                    HalveAtrophy.statusId = "Atrophy_ID";
                    HalveAtrophy.entryAsPercentage = true;

                    Ability orguiswhiteleft = new Ability("Widdershins", "AApocrypha_OrguisWhiteLeft_A")
                    {
                        Description = "Move Left, then eject half of this unit's Atrophy as Absolution spread between its position and the Opposing position. This ability assumes that the grid wraps around.",
                        Cost = [Pigments.Grey, LoadedDBsHandler.PigmentDB.GetPigment("White")],
                        Effects =
                        [
                            Effects.GenerateEffect(SwapLeft, 1, Targeting.Slot_SelfSlot),
                            Effects.GenerateEffect(CheckLeft, 1, Targeting.Slot_SelfSlot, PreviousFalse),
                            Effects.GenerateEffect(ScriptableObject.CreateInstance<MirrorPositionEffect>(), 1, Targeting.Slot_SelfSlot, Effects.CheckMultiplePreviousEffectsCondition([true, false], [1, 2])),
                            Effects.GenerateEffect(BeamAnim, 1, Targeting.Slot_Front),
                            Effects.GenerateEffect(HalveAtrophy, 50, Targeting.Slot_SelfSlot),
                            Effects.GenerateEffect(AbsolutionEvenSpreadByPreviousCast, 1, Targeting.Slot_Front),
                        ],
                        Rarity = Rarity.Common,
                        Priority = Priority.VeryFast,
                    };
                    orguiswhiteleft.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Swap_Left)]);
                    orguiswhiteleft.AddIntentsToTarget(Targeting.Slot_Front, ["Field_Absolution"]);
                    orguiswhiteleft.AddIntentsToTarget(Targeting.Slot_SelfSlot, ["Field_Absolution"]);

                    Ability orguiswhiteright = new Ability("Deosil", "AApocrypha_OrguisWhiteRight_A")
                    {
                        Description = "Move Right, then eject half of this unit's Atrophy as Absolution spread between its position and the Opposing position. This ability assumes that the grid wraps around.",
                        Cost = [LoadedDBsHandler.PigmentDB.GetPigment("White"), Pigments.Grey],
                        Effects =
                        [
                            Effects.GenerateEffect(SwapRight, 1, Targeting.Slot_SelfSlot),
                            Effects.GenerateEffect(CheckRight, 1, Targeting.Slot_SelfSlot, PreviousFalse),
                            Effects.GenerateEffect(ScriptableObject.CreateInstance<MirrorPositionEffect>(), 1, Targeting.Slot_SelfSlot, Effects.CheckMultiplePreviousEffectsCondition([true, false], [1, 2])),
                            Effects.GenerateEffect(BeamAnim, 1, Targeting.Slot_Front),
                            Effects.GenerateEffect(HalveAtrophy, 50, Targeting.Slot_SelfSlot),
                            Effects.GenerateEffect(AbsolutionEvenSpreadByPreviousCast, 1, Targeting.Slot_Front),
                        ],
                        Rarity = Rarity.Common,
                        Priority = Priority.VeryFast,
                    };
                    orguiswhiteright.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Swap_Right)]);
                    orguiswhiteright.AddIntentsToTarget(Targeting.Slot_Front, ["Field_Absolution"]);
                    orguiswhiteright.AddIntentsToTarget(Targeting.Slot_SelfSlot, ["Field_Absolution"]);

                    AddPassiveEffect Revelate = ScriptableObject.CreateInstance<AddPassiveEffect>();
                    Revelate._passiveToAdd = Passives.GetCustomPassive("Revelator_PA");

                    RemovePassiveEffect Irrevelate = ScriptableObject.CreateInstance<RemovePassiveEffect>();
                    Irrevelate.m_PassiveID = Passives.GetCustomPassive("Revelator_PA").m_PassiveID;

                    Ability promise = new Ability("I Will It, Promise Greatness?", "AApocrypha_OrguisPromiseGreatnessWhite_A")
                    {
                        Description = "Grant the Opposing party member Revelator until this ability concludes. Greatly heal the Opposing party member, then deal damage to them and this unit equal to the amount of effective healing and grant them Free Will. If they already had Free Will, remove it when performing this ability.",
                        Cost = [LoadedDBsHandler.PigmentDB.GetPigment("White"), LoadedDBsHandler.PigmentDB.GetPigment("White"), LoadedDBsHandler.PigmentDB.GetPigment("White"), LoadedDBsHandler.PigmentDB.GetPigment("White")],
                        Visuals = CustomVisuals.Whispers,
                        AnimationTarget = Targeting.Slot_Front,
                        Effects =
                        [
                            Effects.GenerateEffect(Revelate, 1, Targeting.Slot_Front),
                            Effects.GenerateEffect(ScriptableObject.CreateInstance<HealEffect>(), 10, Targeting.Slot_Front),
                            Effects.GenerateEffect(DamageByPrevious, 1, Targeting.Slot_Front, Effects.CheckPreviousEffectCondition(true, 1)),
                            Effects.GenerateEffect(ReFree, 1, Targeting.Slot_Front),
                            Effects.GenerateEffect(FreePopup, 1, Targeting.Slot_Front, Effects.CheckPreviousEffectCondition(true, 1)),
                            Effects.GenerateEffect(UnFree, 1, Targeting.Slot_Front, Effects.CheckPreviousEffectCondition(false, 2)),
                            Effects.GenerateEffect(Irrevelate, 1, Targeting.Slot_Front, Effects.CheckPreviousEffectCondition(true, 6)),
                        ],
                        Rarity = Rarity.Impossible,
                        Priority = Priority.Fast,
                    };
                    promise.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Heal_5_10), nameof(IntentType_GameIDs.Damage_7_10), "AA_AddPassive", "AA_RemPassive"]);

                    Ability angels = new Ability("I Will It, Waking Angels", "AApocrypha_OrguisWakingAngelsWhite_A")
                    {
                        Description = "Remove all Absolution from this enemy's position and distribute it evenly between the Left and Right party member positions." +
                        "\nRemove all Absolution from the enemy side, then apply it to this enemy's position.",
                        Cost = [LoadedDBsHandler.PigmentDB.GetPigment("White"), LoadedDBsHandler.PigmentDB.GetPigment("White"), LoadedDBsHandler.PigmentDB.GetPigment("White"), LoadedDBsHandler.PigmentDB.GetPigment("White")],
                        Visuals = Visuals.Excommunicate,
                        AnimationTarget = Targeting.Slot_OpponentSides,
                        Effects =
                        [
                            Effects.GenerateEffect(RemoveAbsolution, 1, Targeting.Slot_SelfSlot),
                            Effects.GenerateEffect(AbsolutionEvenSpreadByPrevious, 1, Targeting.Slot_OpponentSides),
                            Effects.GenerateEffect(RemoveAbsolution, 1, Targeting.Slot_AllyAllSlots),
                            Effects.GenerateEffect(AbsolutionByPrevious, 1, Targeting.Slot_SelfSlot),
                        ],
                        Rarity = Rarity.Impossible,
                        Priority = Priority.Fast,
                    };
                    angels.AddIntentsToTarget(Targeting.Slot_SelfSlot, ["Rem_Field_Absolution"]);
                    angels.AddIntentsToTarget(Targeting.Slot_OpponentSides, ["Field_Absolution"]);
                    angels.AddIntentsToTarget(Targeting.Slot_AllyAllSlots, ["Rem_Field_Absolution"]);
                    angels.AddIntentsToTarget(Targeting.Slot_SelfSlot, ["Field_Absolution"]);

                    FieldEffect_Apply_Effect CrosshairsByPrevious = ScriptableObject.CreateInstance<FieldEffect_Apply_Effect>();
                    CrosshairsByPrevious._Field = StatusField.GetCustomFieldEffect("Crosshairs_ID");
                    CrosshairsByPrevious._UsePreviousExitValueAsMultiplier = true;

                    Ability manna = new Ability("I Will It, Manna Driver", "AApocrypha_OrguisMannaDriverWhite_A")
                    {
                        Description = "Convert all Absolution on all other enemy positions to an equivalent amount of Shield." +
                        "\nConvert all Absolution on all party member positions and this enemy's position to an equivalent amount of Crosshairs.",
                        Cost = [LoadedDBsHandler.PigmentDB.GetPigment("White"), LoadedDBsHandler.PigmentDB.GetPigment("White"), LoadedDBsHandler.PigmentDB.GetPigment("White"), LoadedDBsHandler.PigmentDB.GetPigment("White")],
                        Visuals = Visuals.Excommunicate,
                        AnimationTarget = AllUnitsTargeting,
                        Effects =
                        [
                            Effects.GenerateEffect(RemoveAbsolution, 1, Targeting.GenerateSlotTarget([-4], true)),
                            Effects.GenerateEffect(ShieldByPrevious, 1, Targeting.GenerateSlotTarget([-4], true)),
                            Effects.GenerateEffect(RemoveAbsolution, 1, Targeting.GenerateSlotTarget([-3], true)),
                            Effects.GenerateEffect(ShieldByPrevious, 1, Targeting.GenerateSlotTarget([-3], true)),
                            Effects.GenerateEffect(RemoveAbsolution, 1, Targeting.GenerateSlotTarget([-2], true)),
                            Effects.GenerateEffect(ShieldByPrevious, 1, Targeting.GenerateSlotTarget([-2], true)),
                            Effects.GenerateEffect(RemoveAbsolution, 1, Targeting.GenerateSlotTarget([-1], true)),
                            Effects.GenerateEffect(ShieldByPrevious, 1, Targeting.GenerateSlotTarget([-1], true)),
                            Effects.GenerateEffect(RemoveAbsolution, 1, Targeting.GenerateSlotTarget([1], true)),
                            Effects.GenerateEffect(ShieldByPrevious, 1, Targeting.GenerateSlotTarget([1], true)),
                            Effects.GenerateEffect(RemoveAbsolution, 1, Targeting.GenerateSlotTarget([2], true)),
                            Effects.GenerateEffect(ShieldByPrevious, 1, Targeting.GenerateSlotTarget([2], true)),
                            Effects.GenerateEffect(RemoveAbsolution, 1, Targeting.GenerateSlotTarget([3], true)),
                            Effects.GenerateEffect(ShieldByPrevious, 1, Targeting.GenerateSlotTarget([3], true)),
                            Effects.GenerateEffect(RemoveAbsolution, 1, Targeting.GenerateSlotTarget([4], true)),
                            Effects.GenerateEffect(ShieldByPrevious, 1, Targeting.GenerateSlotTarget([4], true)),
                            Effects.GenerateEffect(RemoveAbsolution, 1, Targeting.GenerateGenericTarget([0], false)),
                            Effects.GenerateEffect(CrosshairsByPrevious, 1, Targeting.GenerateGenericTarget([0], false)),
                            Effects.GenerateEffect(RemoveAbsolution, 1, Targeting.GenerateGenericTarget([1], false)),
                            Effects.GenerateEffect(CrosshairsByPrevious, 1, Targeting.GenerateGenericTarget([1], false)),
                            Effects.GenerateEffect(RemoveAbsolution, 1, Targeting.GenerateGenericTarget([2], false)),
                            Effects.GenerateEffect(CrosshairsByPrevious, 1, Targeting.GenerateGenericTarget([2], false)),
                            Effects.GenerateEffect(RemoveAbsolution, 1, Targeting.GenerateGenericTarget([3], false)),
                            Effects.GenerateEffect(CrosshairsByPrevious, 1, Targeting.GenerateGenericTarget([3], false)),
                            Effects.GenerateEffect(RemoveAbsolution, 1, Targeting.GenerateGenericTarget([4], false)),
                            Effects.GenerateEffect(CrosshairsByPrevious, 1, Targeting.GenerateGenericTarget([4], false)),
                            Effects.GenerateEffect(RemoveAbsolution, 1, Targeting.Slot_SelfSlot),
                            Effects.GenerateEffect(CrosshairsByPrevious, 1, Targeting.Slot_SelfSlot),
                        ],
                        Rarity = Rarity.Impossible,
                        Priority = Priority.Fast,
                    };
                    manna.AddIntentsToTarget(Targeting.GenerateSlotTarget([-4, -3, -2, -1, 0, 1, 2, 3, 4], true), ["Rem_Field_Absolution"]);
                    manna.AddIntentsToTarget(Targeting.GenerateSlotTarget([-4, -3, -2, -1, 1, 2, 3, 4], true), [nameof(IntentType_GameIDs.Field_Shield)]);
                    manna.AddIntentsToTarget(Targeting.GenerateSlotTarget([-4, -3, -2, -1, 0, 1, 2, 3, 4], false), ["Rem_Field_Absolution", "Field_Crosshairs"]);
                    manna.AddIntentsToTarget(Targeting.Slot_SelfSlot, ["Field_Crosshairs"]);

                    ExtraAbilityInfo promiseextra = new()
                    {
                        ability = promise.GenerateEnemyAbility().ability,
                        rarity = Rarity.Uncommon,
                    };

                    ExtraAbilityInfo angelsextra = new()
                    {
                        ability = angels.GenerateEnemyAbility().ability,
                        rarity = Rarity.Uncommon,
                    };

                    ExtraAbilityInfo mannaextra = new()
                    {
                        ability = manna.GenerateEnemyAbility().ability,
                        rarity = Rarity.Uncommon,
                    };

                    whiteOrguis.AddPassives([Passives.MultiAttack3, Passives.GetCustomPassive("Erasure_PA"), Passives.GetCustomPassive("AA_CondenseWhite_PA"), CustomPassives.BonusSuiteGenerator([promiseextra, angelsextra, mannaextra])]);

                    whiteOrguis.AddEnemyAbilities([
                        orguiswhiteleft.GenerateEnemyAbility(true),
                        orguiswhiteright.GenerateEnemyAbility(true),
                    ]);

                    whiteOrguis.AddEnemy(false, false, false);
                }
            }
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
