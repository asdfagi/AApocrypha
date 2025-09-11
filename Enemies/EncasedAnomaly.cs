using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Enemies
{
    public class EncasedAnomaly
    {
        public static void Add()
        {
            Enemy encasedanomaly = new Enemy("Encased Anomaly", "EncasedAnomaly_EN")
            {
                Health = 20,
                HealthColor = Pigments.Purple,
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("EncasedAnomalyTimeline", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("EncasedAnomalyDead", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("EncasedAnomalyTimeline", new Vector2(0.5f, 0f), 32),
                DamageSound = LoadedAssetsHandler.GetEnemy("TaMaGoa_EN").damageSound,
                DeathSound = LoadedAssetsHandler.GetEnemy("TaMaGoa_EN").damageSound,
                UnitTypes = ["AnomalyID"],
            };
            encasedanomaly.PrepareEnemyPrefab("Assets/Apocrypha_Enemies/EncasedAnomaly_Enemy/EncasedAnomaly_Enemy.prefab", AApocrypha.assetBundle, AApocrypha.assetBundle.LoadAsset<GameObject>("Assets/Apocrypha_Enemies/Anomaly_Enemy/AnomalyShell_Giblets.prefab").GetComponent<ParticleSystem>());
            encasedanomaly.AddPassives([Passives.Pure, Passives.GetCustomPassive("Shy_PA"), Passives.DecayGenerator(LoadedAssetsHandler.GetEnemy("UnboundAnomaly_EN"), 60)]);

            GenerateColorManaEffect GivePurplePigment = ScriptableObject.CreateInstance<GenerateColorManaEffect>();
            GivePurplePigment.mana = Pigments.Purple;

            ConsumeColorManaEffect ConsumePurplePigment = ScriptableObject.CreateInstance<ConsumeColorManaEffect>();
            ConsumePurplePigment.mana = Pigments.Purple;

            FieldEffect_Apply_Effect ShieldByPrevious = ScriptableObject.CreateInstance<FieldEffect_Apply_Effect>();
            ShieldByPrevious._Field = StatusField.Shield;
            ShieldByPrevious._UsePreviousExitValueAsMultiplier = true;

            RemoveFieldEffectEffect RemoveShield = ScriptableObject.CreateInstance<RemoveFieldEffectEffect>();
            RemoveShield._field = StatusField.Shield;

            FieldEffect_Apply_Effect ApplyShield = ScriptableObject.CreateInstance<FieldEffect_Apply_Effect>();
            ApplyShield._Field = StatusField.Shield;

            MassSwapZoneEffect Shuffle = ScriptableObject.CreateInstance<MassSwapZoneEffect>();

            PreviousEffectCondition PreviousTrue = ScriptableObject.CreateInstance<PreviousEffectCondition>();
            PreviousTrue.wasSuccessful = true;

            PreviousEffectCondition PreviousFalse = ScriptableObject.CreateInstance<PreviousEffectCondition>();
            PreviousFalse.wasSuccessful = false;

            Ability expelmatter = new Ability("Expel Matter", "AApocrypha_ExpelMatter_A")
            {
                Description = "Deals a Painful amount of damage to the Opposing party member and produces 2 Purple Pigment.\nAfterwards, if there is a party member opposing this enemy, applies 3 Shield to the Left and Right enemy positions.",
                Cost = [Pigments.Purple, Pigments.Purple],
                Visuals = Visuals.Bosch,
                AnimationTarget = Targeting.Slot_SelfSlot,
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 5, Targeting.Slot_Front),
                    Effects.GenerateEffect(GivePurplePigment, 2, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<CheckHasUnitEffect>(), 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(ApplyShield, 3, Targeting.Slot_AllySides, PreviousTrue),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.Normal,
            };
            expelmatter.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Damage_3_6)]);
            expelmatter.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Mana_Generate)]);
            expelmatter.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Misc_Hidden)]);
            expelmatter.AddIntentsToTarget(Targeting.Slot_AllySides, [nameof(IntentType_GameIDs.Field_Shield)]);

            Ability absorbmatter = new Ability("Absorb Matter", "AAPocrypha_AbsorbMatter_A")
            {
                Description = "Consumes up to 5 Purple Pigment. Applies Shield to this enemy's position equal to the amount of Pigment consumed.\nAfterwards, if there is a party member opposing this enemy, removes all Shield from this enemy's position and applies an equivalent amount of Shield to the Left and Right enemy positions.",
                Cost = [],
                Visuals = Visuals.Shield,
                AnimationTarget = Targeting.Slot_SelfSlot,
                Effects =
                [
                    Effects.GenerateEffect(ConsumePurplePigment, 5, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(ShieldByPrevious, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<CheckHasUnitEffect>(), 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(RemoveShield, 1, Targeting.Slot_SelfSlot, PreviousTrue),
                    Effects.GenerateEffect(ShieldByPrevious, 1, Targeting.Slot_AllySides, PreviousTrue),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.Normal,
            };
            absorbmatter.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Mana_Consume)]);
            absorbmatter.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Field_Shield)]);
            absorbmatter.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Misc_Hidden)]);
            absorbmatter.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Rem_Field_Shield)]);
            absorbmatter.AddIntentsToTarget(Targeting.Slot_AllySides, [nameof(IntentType_GameIDs.Field_Shield)]);

            Ability discordantgaze = new Ability("Discordant Gaze", "AApocrypha_DiscordantGaze_A")
            {
                Description = "Randomly moves all party members and enemies.",
                Cost = [Pigments.Purple, Pigments.Purple],
                Visuals = CustomVisuals.GazeVisualsSO,
                AnimationTarget = Targeting.Slot_SelfSlot,
                Effects =
                [
                    Effects.GenerateEffect(Shuffle, 1, Targeting.Slot_OpponentAllSlots),
                    Effects.GenerateEffect(Shuffle, 1, Targeting.Slot_AllyAllSlots),
                ],
                Rarity = Rarity.Uncommon,
                Priority = Priority.VeryFast
            };
            discordantgaze.AddIntentsToTarget(Targeting.Slot_OpponentAllSlots, [nameof(IntentType_GameIDs.Swap_Mass)]);
            discordantgaze.AddIntentsToTarget(Targeting.Slot_AllyAllSlots, [nameof(IntentType_GameIDs.Swap_Mass)]);

            encasedanomaly.AddEnemyAbilities(
                [
                    expelmatter,
                    absorbmatter,
                    discordantgaze,
                    UnboundAnomaly.anomalytears,
                ]);
            encasedanomaly.AddEnemy(true, true, true);
        }
    }
}
