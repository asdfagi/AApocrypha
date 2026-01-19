using A_Apocrypha.Animations;
using BrutalAPI;
using UnityEngine;

namespace A_Apocrypha.Fools //replace this with your mod's name. EX. "BRUTAL_ORCHESTRA_MOD", "MyBorchMod", etc
{
    public class TestCharacter // Will be called when adding character to main file. replace with your character's name
    {
        public static void Add()
        {
            Debug.LogWarning("Fools | Warning! TestCharacter.cs is enabled!");
            Character testCharacter = new Character("TestCharacterName", "TestCharacterName_CH") // replace "CHaracterName" with your character's name
            {
                HealthColor = Pigments.Purple,
                UsesBasicAbility = true, //set this to false if you don't want your fool to use slap
                UsesAllAbilities = false, //set this to true if you want your fool to use all their abilities (like Longliver, Gospel or Mordrake)
                MovesOnOverworld = true, //set this to false if you want your fool to move like Gospel or Leviat in the overworld
                //BasicAbility = basicAbility //if your character has a different basic ability than Slap, you need to define it above the character code (this block) to call it here
                FrontSprite = ResourceLoader.LoadSprite("TestCharacterFront", new Vector2(0.5f, 0f), 32), // Only needs Image file name as long as it's embedded, no .png/.file addition needed.
                BackSprite = ResourceLoader.LoadSprite("TestCharacterBack", new Vector2(0.5f, 0f), 32), 
                OverworldSprite = ResourceLoader.LoadSprite("TestCharacterOverworld", new Vector2(0.5f, 0f), 32), // Portrait and Overworld sprites are 64x64
                DamageSound = LoadedAssetsHandler.GetCharacter("Nowak_CH").damageSound, //character IDs end in _CH
                DeathSound = LoadedAssetsHandler.GetEnemy("Revola_EN").deathSound, //enemy IDs end in _EN
                DialogueSound = LoadedAssetsHandler.GetCharacter("Nowak_CH").dxSound, //.dxSound is for Dialogue sounds
                // Support - IgnoredAbilitiesForSupportBuilds
                IgnoredAbilitiesForDPSBuilds = [1], //For excluding abilities when game chooses fool loadout, not necessary for all fools
            };
            testCharacter.GenerateMenuCharacter(ResourceLoader.LoadSprite("TestCharacterMenu"), ResourceLoader.LoadSprite("TestCharacterLocked")); //Menu Locked and Unlocked sprites are 32x48.
            testCharacter.AddPassives([Passives.CatalystGenerator(103), Passives.Transfusion, Passives.Absorb, Passives.EssenceYellow, Passives.GetCustomPassive("JollyJoker_PA")]); // If you want a different existing passive at a different degree, most of them have a built-in generator.
            testCharacter.SetMenuCharacterAsFullDPS(); // Sets a Support/DPS bias for your fool. Used when your Fool is picked randomly by the game.
            // Support - .SetMenuCharacterAsFullSupport()

            //general format for effects is EffectToDo EffectName = ScriptableObject.CreateInstance<EffectToDo>();
            //List of SOME (not all) effects - https://brutalorchestramodding.miraheze.org/wiki/All_base_game_Effects

            DamageEffect DirectDamage = ScriptableObject.CreateInstance<DamageEffect>();

            DamageEffect IndirectDamage = ScriptableObject.CreateInstance<DamageEffect>(); //DamageEffect has multiple further definitions, including specifying Indirect Damage
            IndirectDamage._indirect = IndirectDamage;

            DamageEffect IndirectDamageByPrevious = ScriptableObject.CreateInstance<DamageEffect>(); //DamageEffect has multiple further definitions, including specifying Indirect Damage
            IndirectDamageByPrevious._indirect = IndirectDamage;
            IndirectDamageByPrevious._usePreviousExitValue = true;

            AddPassiveEffect addPassiveEffect = ScriptableObject.CreateInstance<AddPassiveEffect>(); 
            addPassiveEffect._passiveToAdd = Passives.FleetingGenerator(1);

            IntentInfoBasic fleetIntent = new IntentInfoBasic(); // some passives dont have intents by default, so you may have to create them yourself
            fleetIntent._color = Color.white;
            fleetIntent._sprite = Passives.Fleeting1.passiveIcon; //calls the sprite from the passive for the intent
            LoadedDBsHandler.IntentDB.AddNewBasicIntent("FleetingIntent", fleetIntent); //creates the actual intent to call in your move

            RefreshAbilityUseEffect refreshAbilityUseEffect = ScriptableObject.CreateInstance<RefreshAbilityUseEffect>();
            PercentageEffectCondition refreshOne = Effects.ChanceCondition(20); // chance rate of refreshing a fool's abilities.

            FieldEffect_Apply_Effect ShieldApply = ScriptableObject.CreateInstance<FieldEffect_Apply_Effect>();
            ShieldApply._Field = StatusField.Shield; //Both field and status effects are under StatusField now

            StatusEffect_Apply_Effect ScarsApply = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>(); //Sets up the general effect. Can't do anything without further definitions below.
            ScarsApply._Status = StatusField.Scars; //Defines what exact effect will be used. Even if the EffectName reads "ScarsApply", if it calls for StatusField.Rupture, it will apply Rupture.

            DamageOfTypeEffect LinkedDamage = ScriptableObject.CreateInstance<DamageOfTypeEffect>();
            LinkedDamage._DamageTypeID = CombatType_GameIDs.Dmg_Linked.ToString();

            AddPassiveEffect Gouge = ScriptableObject.CreateInstance<AddPassiveEffect>();
            Gouge._passiveToAdd = Passives.GetCustomPassive("Gouged_PA");

            AnimationVisualsEffect GougeAnim = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            GougeAnim._animationTarget = Targeting.Slot_Front;
            GougeAnim._visuals = Visuals.InvadeTheVeins;

            RemovePassiveEffect Ungouge = ScriptableObject.CreateInstance<RemovePassiveEffect>();
            Ungouge.m_PassiveID = "Gouged";

            //NOTES: Right Clicking an Effect (like DamageEffect) will tell you what additional definitions can be chosen for certain things, like Indirect Damage or Status Effects.
            //Typing things out manually is helpful, VS will bring up a menu of available items to enter. Use it to your advantage.

            //Ability 1. 4 different scales needed, 1 for each level.
            Ability ability0 = new Ability("Unleveled Ability", "AApocrypha_TestAbility_1_A")
            {
                Description = "Deal indirect damage to the Opposing enemy equal to the Scrabble score of their name.\nDeal 4 Linked damage to the Opposing enemy.\nRemove the Opposing enemy's eyes.",
                AbilitySprite = ResourceLoader.LoadSprite("AbilityIcon"),
                Cost = [Pigments.YellowRed, Pigments.Red],
                Visuals = CustomVisuals.Whispers, //Visuals are now under 'Visuals. '. List here: https://github.com/kondorriano/BrutalAPI/wiki/Visuals Visual Aid here: https://www.youtube.com/watch?v=YJsGBPA-OP0
                AnimationTarget = Targeting.Slot_Front,
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<ReturnTargetsScrabbleScoreEffect>(), 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(IndirectDamageByPrevious, 1, Targeting.Slot_Front), // In order, calls for (EffectToDo, #ToApply, Targeting)
                    Effects.GenerateEffect(LinkedDamage, 4, Targeting.Slot_Front),
                    Effects.GenerateEffect(GougeAnim),
                    Effects.GenerateEffect(Gouge, 1, Targeting.Slot_Front),
                ]
            };
            ability0.AddIntentsToTarget(Targeting.Slot_Front, ["AA_Damage_Scrabble", nameof(IntentType_GameIDs.Damage_3_6), nameof(IntentType_GameIDs.Misc)]); //Damage_#_# is parameters for damage calculations. Heal_#_# for heals. Ranges for all numbers between given #s. EX: 7,8,9,10.

            Ability ability1 = new Ability("Crazy Ability", "AApocrypha_TestAbility_2_A") //be sure to rename the ID of the ability too. the game will be unhappy if you don't
            {
                Description = "Deal indirect damage to the Opposing enemy equal to the Scrabble score of their name.\nDeal 6 Linked damage to the Opposing enemy.\nRemove the Opposing enemy's eyes.",
                AbilitySprite = ResourceLoader.LoadSprite("AbilityIcon"),
                Cost = [Pigments.YellowRed, Pigments.Red], //Split pigments are under one name now
                Visuals = CustomVisuals.Whispers,
                AnimationTarget = Targeting.Slot_Front,
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<ReturnTargetsScrabbleScoreEffect>(), 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(IndirectDamage, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(LinkedDamage, 6, Targeting.Slot_Front),
                    Effects.GenerateEffect(GougeAnim),
                    Effects.GenerateEffect(Gouge, 1, Targeting.Slot_Front),
                ]
            };
            ability1.AddIntentsToTarget(Targeting.Slot_Front, ["AA_Damage_Scrabble", nameof(IntentType_GameIDs.Damage_3_6), nameof(IntentType_GameIDs.Misc)]); //Good format to call multiple calculations.


            //Ability 2.
            Ability otherAbility0 = new Ability("The Other Ability", "AApocrypha_TestotherAbility_1_A")
            {
                Description = "Apply 5 Shield to this position.\nApply 1 Scar to the Left and Right enemies.\nRemove Gouged from the Opposing enemy.",
                AbilitySprite = ResourceLoader.LoadSprite("OtherAbilityIcon"),
                Cost = [Pigments.YellowPurple, Pigments.Blue],
                Visuals = CustomVisuals.GazeVisualsSO,
                AnimationTarget = Targeting.Slot_SelfSlot,
                Effects =
                [
                    Effects.GenerateEffect(ShieldApply, 5, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(ScarsApply, 1, Targeting.Slot_OpponentSides),
                    Effects.GenerateEffect(Ungouge, 1, Targeting.Slot_Front),
                ]
            };
            otherAbility0.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Field_Shield)]);
            otherAbility0.AddIntentsToTarget(Targeting.Slot_OpponentSides, [nameof(IntentType_GameIDs.Status_Scars)]); //Intents are added sequentially
            otherAbility0.AddIntentsToTarget(Targeting.Slot_Front, ["AA_RemPassive"]);

            Ability otherAbility1 = new Ability("The Other Ability", "AApocrypha_TestotherAbility_2_A")
            {
                Description = "Apply 10 Shield to this position.\nApply 1 Scar to the Left, Right, and Opposing enemies.\nRemove Gouged from the Opposing enemy.",
                AbilitySprite = ResourceLoader.LoadSprite("OtherAbilityIcon"),
                Cost = [Pigments.YellowPurple, Pigments.RedBlue],
                Visuals = CustomVisuals.GazeVisualsSO,
                AnimationTarget = Targeting.Slot_SelfSlot,
                Effects =
                [
                    Effects.GenerateEffect(ShieldApply, 10, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(ScarsApply, 1, Targeting.Slot_FrontAndSides),
                    Effects.GenerateEffect(Ungouge, 1, Targeting.Slot_Front),
                ]
            };
            otherAbility1.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Field_Shield)]);
            otherAbility1.AddIntentsToTarget(Targeting.Slot_FrontAndSides, [nameof(IntentType_GameIDs.Status_Scars)]);
            otherAbility1.AddIntentsToTarget(Targeting.Slot_Front, ["AA_RemPassive"]);

            //Ability 3.
            Ability thirdAbility0 = new Ability("Third Ability", "thirdAbility_1_A")
            {
                Description = "Deal 2 damage to the Opposing enemy.\nApply 1 Fleeting to this party member.",
                AbilitySprite = ResourceLoader.LoadSprite("ThirdAbilityIcon"),
                Cost = [Pigments.Blue],
                Visuals = Visuals.Wriggle,
                AnimationTarget = Targeting.Slot_Front,
                Effects =
                [
                    Effects.GenerateEffect(DirectDamage, 2, Targeting.Slot_Front),
                    Effects.GenerateEffect(addPassiveEffect, 1, Targeting.Slot_SelfSlot),
                ]
            };
            thirdAbility0.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Status_Spotlight)]);
            thirdAbility0.AddIntentsToTarget(Targeting.Slot_SelfSlot, new string[1] { "FleetingIntent" }); //method of writing custom intents

            //Ability 3.
            Ability thirdAbility1 = new Ability("Third Ability", "thirdAbility_2_A")
            {
                Description = "Deal 4 damage to the Opposing enemy.\nApply 1 Fleeting to this party member. 20% chance to refresh this Party Member.",
                AbilitySprite = ResourceLoader.LoadSprite("ThirdAbilityIcon"),
                Cost = [Pigments.Blue],
                Visuals = Visuals.Wriggle,
                AnimationTarget = Targeting.Slot_Front,
                Effects =
                [
                    Effects.GenerateEffect(DirectDamage, 4, Targeting.Slot_Front),
                    Effects.GenerateEffect(addPassiveEffect, 1, Targeting.Slot_SelfSlot, refreshOne), // refresh rates can be stuck nicely right after your Targeting.
                ]
            };
            thirdAbility1.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Status_Spotlight)]);
            thirdAbility1.AddIntentsToTarget(Targeting.Slot_SelfSlot, new string[1] { "FleetingIntent" }); //method of writing custom intents

            testCharacter.AddLevelData(5, new Ability[] { ability0, otherAbility0, thirdAbility0 }); //# (5) is health at level.
            testCharacter.AddLevelData(10, new Ability[] { ability0, otherAbility0, thirdAbility0 }); //Levels are added sequentially
            testCharacter.AddLevelData(15, new Ability[] { ability1, otherAbility1, thirdAbility1 });
            testCharacter.AddLevelData(20, new Ability[] { ability1, otherAbility1, thirdAbility1 });
            testCharacter.AddCharacter(true, true); //The first bool (true/false) is whether the fool is unlocked initially. The second is whether they show up in shops.
        }
    }
}