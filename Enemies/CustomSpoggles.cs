using System;
using System.Collections.Generic;
using System.Text;
using A_Apocrypha.CustomOther;

namespace A_Apocrypha.Enemies
{
    public class CustomSpoggles
    {
        public static void Add()
        {
            ConsumeRandomButCasterHealthManaEffect ConsumeNotHealth = ScriptableObject.CreateInstance<ConsumeRandomButCasterHealthManaEffect>();

            PreviousEffectCondition PreviousTrue = ScriptableObject.CreateInstance<PreviousEffectCondition>();
            PreviousTrue.wasSuccessful = true;

            PreviousEffectCondition PreviousFalse = ScriptableObject.CreateInstance<PreviousEffectCondition>();
            PreviousFalse.wasSuccessful = false;

            Ability gnaw = new Ability("Gnaw", "AApocrypha_SpoggleGnaw_A")
            {
                Description = "Deals a Painful amount of damage to the Left and Right party members.\nThis enemy consumes 2 Pigment not of this enemy's health colour.",
                Cost = [],
                Visuals = Visuals.Gnaw,
                AnimationTarget = Targeting.Slot_OpponentSides,
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 4, Targeting.Slot_OpponentSides),
                    Effects.GenerateEffect(ConsumeNotHealth, 2, Targeting.Slot_SelfSlot),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.Normal,
            };
            gnaw.AddIntentsToTarget(Targeting.Slot_OpponentSides, [nameof(IntentType_GameIDs.Damage_3_6)]);
            gnaw.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Mana_Consume)]);

            Ability siphon = new Ability("Siphon", "AApocrypha_SpoggleSiphon_A")
            {
                Description = "This enemy consumes 3 Pigment not of this enemy's health colour.",
                Cost = [],
                Visuals = Visuals.Leech,
                AnimationTarget = Targeting.Slot_SelfSlot,
                Effects =
                [
                    Effects.GenerateEffect(ConsumeNotHealth, 3, Targeting.Slot_SelfSlot),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.Normal,
            };
            siphon.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Mana_Consume)]);

            Ability evaporate = new Ability("Evaporate", "AApocrypha_SpoggleEvaporate_A")
            {
                Description = "This enemy consumes 6 Pigment not of this enemy's health colour.",
                Cost = [],
                Visuals = Visuals.Leech,
                AnimationTarget = Targeting.Slot_SelfSlot,
                Effects =
                [
                    Effects.GenerateEffect(ConsumeNotHealth, 6, Targeting.Slot_SelfSlot),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.Normal,
            };
            evaporate.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Mana_Consume)]);

            Enemy devotedSpoggle = new Enemy("Devoted Spoggle", "DevotedSpoggle_EN")
            {
                Health = 25,
                HealthColor = Pigments.RedPurple,
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("DevotedSpoggleTimeline", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("DevotedSpoggleDead", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("DevotedSpoggleTimeline", new Vector2(0.5f, 0f), 32),
                DamageSound = LoadedAssetsHandler.GetEnemy("Spoggle_Resonant_EN").damageSound,
                DeathSound = LoadedAssetsHandler.GetEnemy("Spoggle_Resonant_EN").deathSound,
            };
            devotedSpoggle.PrepareEnemyPrefab("Assets/Apocrypha_Enemies/DevotedSpoggle_Enemy/DevotedSpoggle_Enemy.prefab", AApocrypha.assetBundle, AApocrypha.assetBundle.LoadAsset<GameObject>("Assets/Apocrypha_Enemies/DevotedSpoggle_Enemy/DevotedSpoggle_Giblets.prefab").GetComponent<ParticleSystem>());
            devotedSpoggle.AddPassives([Passives.Pure, Passives.Absorb, Passives.Skittish, Passives.Catalyst]);

            StatusEffect_Apply_Effect LinkedApply = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            LinkedApply._Status = StatusField.Linked;

            StatusEffect_Apply_Effect ScarsApply = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            ScarsApply._Status = StatusField.Scars;

            SpecificUnitsByStatusTargeting AllLinkedUnits = ScriptableObject.CreateInstance<SpecificUnitsByStatusTargeting>();
            AllLinkedUnits._status = StatusField.Linked;
            AllLinkedUnits.targetUnitAllySlots = true;
            AllLinkedUnits.slotOffsets = [0];

            AnimationVisualsEffect ScarAnim = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            ScarAnim._visuals = Visuals.Lullaby;
            ScarAnim._animationTarget = AllLinkedUnits;

            Ability drinkoursuffering = new Ability("Drink Our Suffering", "AApocrypha_DrinkOurSuffering_A")
            {
                Description = "Apply 2 Linked to the highest health party member.",
                Cost = [],
                Visuals = Visuals.Genesis,
                AnimationTarget = Targeting.GenerateUnitTarget_Specific_Health(false, false, false, false),
                Effects =
                [
                    Effects.GenerateEffect(LinkedApply, 2, Targeting.GenerateUnitTarget_Specific_Health(false, false, false, false)),
                ],
                Rarity = Rarity.Uncommon,
                Priority = Priority.Normal,
            };
            drinkoursuffering.AddIntentsToTarget(Targeting.GenerateUnitTarget_Specific_Health(false, false, false, false), [nameof(IntentType_GameIDs.Status_Linked)]);

            Ability torturouswhispers = new Ability("Torturous Whispers", "AApocrypha_TorturousWhispers_A")
            {
                Description = "Deal a Little damage to this enemy. Apply 1 Scar to All Linked units.",
                Cost = [],
                Visuals = Visuals.Flay,
                AnimationTarget = Targeting.Slot_SelfSlot,
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 2, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(ScarAnim, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(ScarsApply, 1, AllLinkedUnits),
                ],
                Rarity = Rarity.Uncommon,
                Priority = Priority.Normal,
            };
            torturouswhispers.AddIntentsToTarget(AllLinkedUnits, [nameof(IntentType_GameIDs.Status_Scars)]);
            torturouswhispers.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Damage_1_2)]);

            devotedSpoggle.AddEnemyAbilities(
                [
                    gnaw,
                    siphon,
                    drinkoursuffering,
                    torturouswhispers,
                ]);
            devotedSpoggle.AddEnemy(true, true, false);

            Enemy cellSpoggle = new Enemy("Cellular Spoggle", "CellularSpoggle_EN")
            {
                Health = 25,
                HealthColor = Pigments.BlueYellow,
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("CellularSpoggleTimeline", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("CellularSpoggleDead", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("CellularSpoggleTimeline", new Vector2(0.5f, 0f), 32),
                DamageSound = LoadedAssetsHandler.GetEnemy("Spoggle_Writhing_EN").damageSound,
                DeathSound = LoadedAssetsHandler.GetEnemy("Spoggle_Writhing_EN").deathSound,
            };
            cellSpoggle.PrepareEnemyPrefab("Assets/Apocrypha_Enemies/BacterialSpoggle_Enemy/BacterialSpoggle_Enemy.prefab", AApocrypha.assetBundle, AApocrypha.assetBundle.LoadAsset<GameObject>("Assets/Apocrypha_Enemies/BacterialSpoggle_Enemy/BacterialSpoggle_Giblets.prefab").GetComponent<ParticleSystem>());
            cellSpoggle.AddPassives([Passives.Pure, Passives.Absorb, Passives.Skittish]);

            StatusEffect_Apply_Effect GuttedApply = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            GuttedApply._Status = StatusField.Gutted;

            SpecificEnemiesTargeting OpposingCells = ScriptableObject.CreateInstance<SpecificEnemiesTargeting>();
            OpposingCells._enemies = ["CellularSpoggle_EN"];
            OpposingCells.targetUnitAllySlots = false;
            OpposingCells.slotOffsets = [0];

            CheckHasMaxHealthEffect HealthIsMax = ScriptableObject.CreateInstance<CheckHasMaxHealthEffect>();

            ChangeMaxHealthEffect PercentageReduceMaxHealth = ScriptableObject.CreateInstance<ChangeMaxHealthEffect>();
            PercentageReduceMaxHealth._entryAsPercentage = true;
            PercentageReduceMaxHealth._increase = false;

            SpawnEnemyAnywhereWithHealthByPreviousEffect Mitose = ScriptableObject.CreateInstance<SpawnEnemyAnywhereWithHealthByPreviousEffect>();
            Mitose.enemy = cellSpoggle.enemy;
            Mitose._spawnTypeID = CombatType_GameIDs.Spawn_Basic.ToString();

            Ability catabolize = new Ability("Catabolize", "AApocrypha_Catabolize_A")
            {
                Description = "Apply 2 Gutted to this enemy.\nDeal a Little damage to the Left and Right enemies and Slightly Heal this enemy for each enemy damaged.\nApply 1 Scar to this enemy.",
                Cost = [],
                Visuals = Visuals.Melt,
                AnimationTarget = Targeting.Slot_AllySides,
                Effects =
                [
                    Effects.GenerateEffect(GuttedApply, 2, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 2, Targeting.Slot_AllyLeft),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<HealEffect>(), 2, Targeting.Slot_SelfSlot, PreviousTrue),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 2, Targeting.Slot_AllyRight),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<HealEffect>(), 2, Targeting.Slot_SelfSlot, PreviousTrue),
                    Effects.GenerateEffect(ScarsApply, 1, Targeting.Slot_SelfSlot),
                ],
                Rarity = Rarity.Uncommon,
                Priority = Priority.Normal,
            };
            catabolize.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Status_Gutted)]);
            catabolize.AddIntentsToTarget(Targeting.Slot_AllySides, [nameof(IntentType_GameIDs.Damage_1_2)]);
            catabolize.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Heal_1_4)]);
            catabolize.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Status_Scars)]);

            Ability mitosis = new Ability("Mitosis", "AApocrypha_Mitosis_A")
            {
                Description = "If this enemy is at full health, deal a Painful amount of damage to all party members opposing a Cellular Spoggle.\nReduce this enemy's maximum health to its current health, then halve this enemy's maximum health. Spawn a clone of this enemy with maximum health equal to the amount this enemy lost.",
                Cost = [],
                Visuals = Visuals.Mitosis,
                AnimationTarget = Targeting.Slot_SelfSlot,
                Effects =
                [
                    Effects.GenerateEffect(HealthIsMax, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 4, OpposingCells, PreviousTrue),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<ChangeMaxHealthByCurrentHealthEffect>(), 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(PercentageReduceMaxHealth, 50, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(Mitose, 1, Targeting.Slot_SelfSlot),
                ],
                Rarity = Rarity.Uncommon,
                Priority = Priority.Normal,
            };
            mitosis.AddIntentsToTarget(OpposingCells, [nameof(IntentType_GameIDs.Damage_3_6)]);
            mitosis.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Other_MaxHealth_Alt)]);
            mitosis.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Other_Spawn)]);

            cellSpoggle.AddEnemyAbilities(
                [
                    gnaw,
                    siphon,
                    catabolize,
                    mitosis,
                ]);
            cellSpoggle.AddEnemy(true, true, false);
        }
    }
}
