using System;
using System.Collections.Generic;
using System.Text;
using A_Apocrypha.CustomOther;

namespace A_Apocrypha.Enemies
{
    public class MachineGnomesFriendly
    {
        public static void Add()
        {
            ChangeToRandomHealthColorEffect randomize = ScriptableObject.CreateInstance<ChangeToRandomHealthColorEffect>();
            randomize._healthColors =
            [
                Pigments.Blue,
                Pigments.Red,
                Pigments.Yellow,
                Pigments.Purple
            ];

            GainLootOneOfCustomCharactersEffect GnomeReward = ScriptableObject.CreateInstance<GainLootOneOfCustomCharactersEffect>();
            GnomeReward._characterCopies = ["Gnome_CH", "GnomePurple_CH", "GnomeBlue_CH", "GnomeGreen_CH"];
            GnomeReward._rank = 0;
            GnomeReward._nameAddition = new NameAdditionLocID();

            Enemy gnomes = new Enemy("Machine Gnomes", "MachineGnomes_Friendly_EN")
            {
                Health = 50,
                HealthColor = Pigments.Grey,
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("GnomesTimeline", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("AnomalyDead", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("GnomesOverworld", new Vector2(0.5f, 0f), 32),
                DamageSound = "event:/AAEnemy/GnomesHurt",
                DeathSound = "event:/AAEnemy/GnomesDeath",
                CombatEnterEffects =
                [
                    Effects.GenerateEffect(randomize, 1, Targeting.Slot_SelfSlot),
                ],
                CombatExitEffects = 
                [
                    Effects.GenerateEffect(GnomeReward, 1, Targeting.Slot_SelfSlot),    
                ],
            };

            DamageEffect IndirectDamage = ScriptableObject.CreateInstance<DamageEffect>();
            IndirectDamage._indirect = true;

            SpawnEnemyAnywhereWithHealthByPreviousEffect GnomeSpawn = ScriptableObject.CreateInstance<SpawnEnemyAnywhereWithHealthByPreviousEffect>();
            GnomeSpawn.enemy = gnomes.enemy;
            GnomeSpawn._spawnTypeID = CombatType_GameIDs.Spawn_Basic.ToString();

            SpecificUnitsByPassiveTargeting AllGnomes = ScriptableObject.CreateInstance<SpecificUnitsByPassiveTargeting>();
            AllGnomes._passive = Passives.GetCustomPassive("Gnome_PA");
            AllGnomes.targetUnitAllySlots = true;
            AllGnomes.slotOffsets = [0];

            PreviousEffectCondition PreviousTrue = ScriptableObject.CreateInstance<PreviousEffectCondition>();
            PreviousTrue.wasSuccessful = true;

            PreviousEffectCondition PreviousFalse = ScriptableObject.CreateInstance<PreviousEffectCondition>();
            PreviousFalse.wasSuccessful = false;

            CopyAndSpawnOneOfCustomCharactersAnywhereEffect GnomePartyJoin = ScriptableObject.CreateInstance<CopyAndSpawnOneOfCustomCharactersAnywhereEffect>();
            GnomePartyJoin._characterCopies = ["Gnome_CH", "GnomePurple_CH", "GnomeBlue_CH", "GnomeGreen_CH"];
            GnomePartyJoin._permanentSpawn = false;
            GnomePartyJoin._rank = 0;
            GnomePartyJoin._usePreviousAsHealth = false;
            GnomePartyJoin._extraModifiers = [];
            GnomePartyJoin._nameAddition = new NameAdditionLocID();

            ExtraVariableForNextEffect Blank = ScriptableObject.CreateInstance<ExtraVariableForNextEffect>();

            CheckIsAliveMultiplyByEntryOrPreviousEffect IsUnitPassPrevious = ScriptableObject.CreateInstance<CheckIsAliveMultiplyByEntryOrPreviousEffect>();
            IsUnitPassPrevious._usePreviousExitValue = true;

            Ability splitgroup = new Ability("Split the Group", "AApocrypha_SplitGroup_Friendly_A")
            {
                Description = "Deal a Painful amount of indirect damage to this enemy. If the damage did not kill, spawn a horde of Machine Gnomes with maximum health equal to twice the damage dealt.",
                Cost = [],
                Visuals = CustomVisuals.StaticColorVisualsSO,
                AnimationTarget = Targeting.Slot_SelfSlot,
                Effects =
                [
                    Effects.GenerateEffect(IndirectDamage, 5, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(IsUnitPassPrevious, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(GnomeSpawn, 2, Targeting.Slot_SelfSlot, PreviousTrue),
                ],
                Rarity = Rarity.Uncommon,
                Priority = Priority.Normal,
            };
            splitgroup.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Damage_3_6), nameof(IntentType_GameIDs.Other_Spawn)]);

            Ability dance = new Ability("Dance, Dance!", "AApocrypha_Dance_Friendly_A")
            {
                Description = "Move All gnomes to the Left or Right.",
                Cost = [],
                Visuals = Visuals.Tango,
                AnimationTarget = AllGnomes,
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<SwapToSidesEffect>(), 1, AllGnomes),
                ],
                Rarity = Rarity.Uncommon,
                Priority = Priority.Normal,
            };
            dance.AddIntentsToTarget(AllGnomes, [nameof(IntentType_GameIDs.Swap_Sides)]);

            CheckPassiveAbilityEffect IsThisAGnome = ScriptableObject.CreateInstance<CheckPassiveAbilityEffect>();
            IsThisAGnome.m_PassiveID = "Gnome";

            StatusEffect_ApplyRandomFromList_Effect statusApply = ScriptableObject.CreateInstance<StatusEffect_ApplyRandomFromList_Effect>();
            statusApply._Statuses = [StatusField.Ruptured, StatusField.OilSlicked, StatusField.Cursed];
            statusApply._ApplyToFirstUnit = false;
            statusApply._JustOneRandomTarget = false;

            Ability playwiththem = new Ability("Play With Them", "AApocrypha_PlayWithThem_Friendly_A")
            {
                Description = "Deal 2 damage to the Left and Right non-Gnome enemies and apply 1 Ruptured, 1 Oil Slicked or Cursed to them.",
                Cost = [],
                Visuals = CustomVisuals.StaticColorVisualsSO,
                AnimationTarget = Targeting.Slot_AllySides,
                Effects =
                [
                    Effects.GenerateEffect(IsThisAGnome, 1, Targeting.Slot_AllyLeft),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 2, Targeting.Slot_AllyLeft, Effects.CheckPreviousEffectCondition(false, 1)),
                    Effects.GenerateEffect(statusApply, 1, Targeting.Slot_AllyLeft, Effects.CheckPreviousEffectCondition(false, 2)),
                    Effects.GenerateEffect(IsThisAGnome, 1, Targeting.Slot_AllyRight),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 2, Targeting.Slot_AllyRight, Effects.CheckPreviousEffectCondition(false, 1)),
                    Effects.GenerateEffect(statusApply, 1, Targeting.Slot_AllyRight, Effects.CheckPreviousEffectCondition(false, 2)),
                ],
                Rarity = Rarity.Uncommon,
                Priority = Priority.Normal,
            };
            playwiththem.AddIntentsToTarget(Targeting.Slot_AllySides, [nameof(IntentType_GameIDs.Damage_1_2), nameof(IntentType_GameIDs.Status_Ruptured), nameof(IntentType_GameIDs.Status_OilSlicked), nameof(IntentType_GameIDs.Status_Cursed)]);

            gnomes.AddPassives([Passives.Slippery, Passives.GetCustomPassive("AA_Heterochromia_PA"), Passives.GetCustomPassive("Gnome_PA"), Passives.Withering]);

            gnomes.AddEnemyAbilities(
            [
                splitgroup.GenerateEnemyAbility(false),
                dance.GenerateEnemyAbility(false),
                playwiththem.GenerateEnemyAbility(false),
            ]);

            gnomes.AddEnemy(true, false, false);
            LoadedAssetsHandler.GetEnemy("MachineGnomes_Friendly_EN").enemyTemplate = LoadedAssetsHandler.GetEnemy("MachineGnomes_EN").enemyTemplate;
        }
    }
}
