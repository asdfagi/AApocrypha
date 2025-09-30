using System;
using System.Collections.Generic;
using System.Text;
using A_Apocrypha.CustomOther;

namespace A_Apocrypha.Fools
{
    public class GnomeCharacter
    {
        public static void Add()
        {
            bool boolGnomeBasic = false;
            bool boolGnomeAllAbilities = true;
            bool boolGnomeMoves = true;
            string gnomeDamageSound = "event:/AAEnemy/GnomesHurt";
            string gnomeDeathSound = "event:/AAEnemy/GnomesDeath";
            string gnomeTalkSound = "event:/AAEnemy/GnomesRoar";
            List<string> gnomeTypes = ["Sandwich_Silly"];
            BasePassiveAbilitySO[] gnomePassives = [Passives.Slippery, Passives.GetCustomPassive("Gnome_PA"), Passives.GetCustomPassive("AA_FreeWilled_PA"), Passives.Withering];

            SpecificUnitsByPassiveTargeting AllGnomes = ScriptableObject.CreateInstance<SpecificUnitsByPassiveTargeting>();
            AllGnomes._passive = Passives.GetCustomPassive("Gnome_PA");
            AllGnomes.targetUnitAllySlots = true;
            AllGnomes.slotOffsets = [0];

            HealEffect HealByPrevious = ScriptableObject.CreateInstance<HealEffect>();
            HealByPrevious.usePreviousExitValue = true;

            ConsumeCasterColorManaEffect StealPigment = ScriptableObject.CreateInstance<ConsumeCasterColorManaEffect>();

            StatusEffect_Apply_Effect RupturedApply = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            RupturedApply._Status = StatusField.Ruptured;

            StatusEffect_Apply_Effect OilApply = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            OilApply._Status = StatusField.OilSlicked;

            StatusEffect_Apply_Effect FocusApply = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            FocusApply._Status = StatusField.Focused;

            Ability dance = new Ability("Dance!", "AApocrypha_Gnome_Dance_A")
            {
                Description = "Deal 1 damage to this party member.",
                Cost = [],
                Visuals = Visuals.Tango,
                AnimationTarget = Targeting.Slot_SelfSlot,
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 1, Targeting.Slot_SelfSlot),
                ],
                Rarity = Rarity.Uncommon,
                Priority = Priority.Normal,
            };
            dance.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Damage_1_2)]);

            Ability steal = new Ability("Steal!", "AApocrypha_Gnome_Steal_A")
            {
                Description = "Try to consume 3 Pigment of this party member's health color. Heal All Gnomes for twice the amount of Pigment consumed.",
                Cost = [],
                Visuals = Visuals.Leech,
                AnimationTarget = Targeting.Slot_SelfSlot,
                Effects =
                [
                    Effects.GenerateEffect(StealPigment, 3, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(HealByPrevious, 2, AllGnomes),
                ],
                Rarity = Rarity.Uncommon,
                Priority = Priority.Normal,
            };
            steal.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Mana_Consume)]);
            steal.AddIntentsToTarget(AllGnomes, [nameof(IntentType_GameIDs.Heal_5_10)]);

            Ability poke = new Ability("Poke!", "AApocrypha_Gnome_Poke_A")
            {
                Description = "Deal 2 damage to the Left and Right party members.",
                Cost = [],
                Visuals = Visuals.Poke,
                AnimationTarget = Targeting.Slot_AllySides,
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 2, Targeting.Slot_AllySides),
                ],
                Rarity = Rarity.Uncommon,
                Priority = Priority.Normal,
            };
            poke.AddIntentsToTarget(Targeting.Slot_AllySides, [nameof(IntentType_GameIDs.Damage_1_2)]);

            Ability bbreak = new Ability("Break!", "AApocrypha_Gnome_Break_A")
            {
                Description = "Apply 1 Ruptured to the Left and Right party members and Self.",
                Cost = [],
                Visuals = Visuals.Exsanguinate,
                AnimationTarget = Targeting.Slot_SelfAndSides,
                Effects =
                [
                    Effects.GenerateEffect(RupturedApply, 1, Targeting.Slot_SelfAndSides),
                ],
                Rarity = Rarity.Uncommon,
                Priority = Priority.Normal,
            };
            bbreak.AddIntentsToTarget(Targeting.Slot_SelfAndSides, [nameof(IntentType_GameIDs.Status_Ruptured)]);

            Ability spill = new Ability("Spill!", "AApocrypha_Gnome_Spill_A")
            {
                Description = "Apply 1 Oil Slicked to the Left and Right party members and Self.",
                Cost = [],
                Visuals = Visuals.OilSlicked,
                AnimationTarget = Targeting.Slot_SelfAndSides,
                Effects =
                [
                    Effects.GenerateEffect(OilApply, 1, Targeting.Slot_SelfAndSides),
                ],
                Rarity = Rarity.Uncommon,
                Priority = Priority.Normal,
            };
            spill.AddIntentsToTarget(Targeting.Slot_SelfAndSides, [nameof(IntentType_GameIDs.Status_OilSlicked)]);

            Ability focus = new Ability("Focus!", "AApocrypha_Gnome_Focus_A")
            {
                Description = "Apply Focused to the Left and Right party members and Self.",
                Cost = [],
                Visuals = Visuals.Bosch,
                AnimationTarget = Targeting.Slot_SelfAndSides,
                Effects =
                [
                    Effects.GenerateEffect(FocusApply, 1, Targeting.Slot_SelfAndSides),
                ],
                Rarity = Rarity.Uncommon,
                Priority = Priority.Normal,
            };
            focus.AddIntentsToTarget(Targeting.Slot_SelfAndSides, [nameof(IntentType_GameIDs.Status_Focused)]);

            Character gnomeCharacter = new Character("Gnome", "Gnome_CH")
            {
                HealthColor = Pigments.Red,
                UsesBasicAbility = boolGnomeBasic,
                UsesAllAbilities = boolGnomeAllAbilities,
                MovesOnOverworld = boolGnomeMoves,
                //BasicAbility = basicAbility //if your character has a different basic ability than Slap, you need to define it above the character code (this block) to call it here
                FrontSprite = ResourceLoader.LoadSprite("GnomeCharacterFront", new Vector2(0.5f, 0f), 32),
                BackSprite = ResourceLoader.LoadSprite("GnomeCharacterBack", new Vector2(0.5f, 0f), 32),
                OverworldSprite = ResourceLoader.LoadSprite("GnomeCharacterOverworld", new Vector2(0.5f, 0f), 32), // Portrait and Overworld sprites are 64x64
                DamageSound = gnomeDamageSound,
                DeathSound = gnomeDeathSound,
                DialogueSound = gnomeTalkSound,
                UnitTypes = gnomeTypes,
            };
            gnomeCharacter.AddPassives(gnomePassives);

            gnomeCharacter.AddLevelData(5, [dance, steal, poke]);
            gnomeCharacter.AddCharacter(true, true);

            Character gnomeCharacterPurple = new Character("Gnome", "GnomePurple_CH")
            {
                HealthColor = Pigments.Purple,
                UsesBasicAbility = boolGnomeBasic,
                UsesAllAbilities = boolGnomeAllAbilities,
                MovesOnOverworld = boolGnomeMoves,
                FrontSprite = ResourceLoader.LoadSprite("GnomeCharacterPurpleFront", new Vector2(0.5f, 0f), 32),
                BackSprite = ResourceLoader.LoadSprite("GnomeCharacterPurpleBack", new Vector2(0.5f, 0f), 32),
                OverworldSprite = ResourceLoader.LoadSprite("GnomeCharacterPurpleOverworld", new Vector2(0.5f, 0f), 32),
                DamageSound = gnomeDamageSound,
                DeathSound = gnomeDeathSound,
                DialogueSound = gnomeTalkSound,
                UnitTypes = gnomeTypes,
            };
            gnomeCharacterPurple.AddPassives(gnomePassives);

            gnomeCharacterPurple.AddLevelData(5, [dance, steal, focus]);
            gnomeCharacterPurple.AddCharacter(true, true);

            Character gnomeCharacterBlue = new Character("Gnome", "GnomeBlue_CH")
            {
                HealthColor = Pigments.Blue,
                UsesBasicAbility = boolGnomeBasic,
                UsesAllAbilities = boolGnomeAllAbilities,
                MovesOnOverworld = boolGnomeMoves,
                FrontSprite = ResourceLoader.LoadSprite("GnomeCharacterBlueFront", new Vector2(0.5f, 0f), 32),
                BackSprite = ResourceLoader.LoadSprite("GnomeCharacterBlueBack", new Vector2(0.5f, 0f), 32),
                OverworldSprite = ResourceLoader.LoadSprite("GnomeCharacterBlueOverworld", new Vector2(0.5f, 0f), 32),
                DamageSound = gnomeDamageSound,
                DeathSound = gnomeDeathSound,
                DialogueSound = gnomeTalkSound,
                UnitTypes = gnomeTypes,
            };
            gnomeCharacterBlue.AddPassives(gnomePassives);

            gnomeCharacterBlue.AddLevelData(5, [dance, steal, bbreak]);
            gnomeCharacterBlue.AddCharacter(true, true);

            Character gnomeCharacterGreen = new Character("Gnome", "GnomeGreen_CH")
            {
                HealthColor = Pigments.Yellow,
                UsesBasicAbility = boolGnomeBasic,
                UsesAllAbilities = boolGnomeAllAbilities,
                MovesOnOverworld = boolGnomeMoves,
                FrontSprite = ResourceLoader.LoadSprite("GnomeCharacterGreenFront", new Vector2(0.5f, 0f), 32),
                BackSprite = ResourceLoader.LoadSprite("GnomeCharacterGreenBack", new Vector2(0.5f, 0f), 32),
                OverworldSprite = ResourceLoader.LoadSprite("GnomeCharacterGreenOverworld", new Vector2(0.5f, 0f), 32),
                DamageSound = gnomeDamageSound,
                DeathSound = gnomeDeathSound,
                DialogueSound = gnomeTalkSound,
                UnitTypes = gnomeTypes,
            };
            gnomeCharacterGreen.AddPassives(gnomePassives);

            gnomeCharacterGreen.AddLevelData(5, [dance, steal, spill]);
            gnomeCharacterGreen.AddCharacter(true, true);
        }
    }
}
