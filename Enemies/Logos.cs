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

            redlogos.AddPassives([Passives.MultiAttack3, Passives.InfernoGenerator(1), Passives.GetCustomPassive("MadeOfFire_PA"), Passives.GetCustomPassive("AA_CondenseRed_PA"), CustomPassives.AltAttacksGenerator([tonguesextrared, placeextra, becomeextra])]);
            bluelogos.AddPassives([Passives.MultiAttack3, Passives.InfernoGenerator(1), Passives.GetCustomPassive("MadeOfFire_PA"), Passives.GetCustomPassive("AA_CondenseBlue_PA"), CustomPassives.AltAttacksGenerator([tonguesextrablue, lifeextra, burdensextra])]);
            yellowlogos.AddPassives([Passives.MultiAttack3, Passives.InfernoGenerator(1), Passives.GetCustomPassive("MadeOfFire_PA"), Passives.GetCustomPassive("AA_CondenseYellow_PA"), CustomPassives.AltAttacksGenerator([tonguesextrayellow, rotextra, peerextra])]);
            purplelogos.AddPassives([Passives.MultiAttack3, Passives.GetCustomPassive("BlackTears_2_PA"), Passives.GetCustomPassive("MadeOfFire_PA"), Passives.GetCustomPassive("AA_CondensePurple_PA"), CustomPassives.AltAttacksGenerator([alchemicalextra, royalextra, languageextra])]);

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

            if (AApocrypha.CrossMod.UndivineComedy && LoadedDBsHandler.PigmentDB.GetPigment("Broken") != null)
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

                CasterInOneEdgeCheckEffect CheckLeft = ScriptableObject.CreateInstance<CasterInOneEdgeCheckEffect>();
                CheckLeft._right = false;

                CasterInOneEdgeCheckEffect CheckRight = ScriptableObject.CreateInstance<CasterInOneEdgeCheckEffect>();
                CheckRight._right = true;

                EffectSO PigmentBreaker = LoadedAssetsHandler.GetCharacterAbility("Defrag_1_A").effects[3].effect;

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
                        Effects.GenerateEffect(PigmentBreaker),
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

                blacklogos.AddPassives([Passives.MultiAttack3, Passives.GetCustomPassive("Snowstorm_1_PA"), Passives.GetCustomPassive("Antifreeze_PA"), Passives.GetCustomPassive("Fragile_PA"), CustomPassives.AltAttacksGenerator([wordsextra, colorsextra, nothingextra])]);

                blacklogos.AddEnemyAbilities(
                [
                    turnback,
                    turnforth,
                ]);

                blacklogos.AddEnemy(false, false, false);
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
