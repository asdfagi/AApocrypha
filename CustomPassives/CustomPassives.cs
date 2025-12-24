using System;
using System.Collections.Generic;
using System.Text;
using A_Apocrypha.CustomOther;
using HarmonyLib;
using UnityEngine;
using static A_Apocrypha.Enemies.Bloatfinger;

namespace A_Apocrypha.Custom_Passives
{
    public class CustomPassives
    {
        public static void Add()
        {
            // Movers

            SwapToSidesEffect LeftOrRight = ScriptableObject.CreateInstance<SwapToSidesEffect>();

            SwapToOneSideEffect Left = ScriptableObject.CreateInstance<SwapToOneSideEffect>();
            Left._swapRight = false;

            SwapToOneSideEffect Right = ScriptableObject.CreateInstance<SwapToOneSideEffect>();
            Right._swapRight = true;

            PreviousEffectCondition PreviousTrue = ScriptableObject.CreateInstance<PreviousEffectCondition>();
            PreviousTrue.wasSuccessful = true;

            PreviousEffectCondition PreviousFalse = ScriptableObject.CreateInstance<PreviousEffectCondition>();
            PreviousFalse.wasSuccessful = false;

            LeftOrRightToOpposeEnemyChanceForNextEffect NavigatorNotOpposing = ScriptableObject.CreateInstance<LeftOrRightToOpposeEnemyChanceForNextEffect>();
            NavigatorNotOpposing._inverted = true;

            LeftOrRightToOpposeEnemyChanceForNextEffect NavigatorOpposing = ScriptableObject.CreateInstance<LeftOrRightToOpposeEnemyChanceForNextEffect>();
            NavigatorOpposing._inverted = false;

            GenerateColorManaEffect GiveRedPigment = ScriptableObject.CreateInstance<GenerateColorManaEffect>();
            GiveRedPigment.mana = Pigments.Red;

            GenerateColorManaEffect GiveBluePigment = ScriptableObject.CreateInstance<GenerateColorManaEffect>();
            GiveBluePigment.mana = Pigments.Blue;

            GenerateColorManaEffect GiveYellowPigment = ScriptableObject.CreateInstance<GenerateColorManaEffect>();
            GiveYellowPigment.mana = Pigments.Yellow;

            GenerateColorManaEffect GivePurplePigment = ScriptableObject.CreateInstance<GenerateColorManaEffect>();
            GivePurplePigment.mana = Pigments.Purple;

            // Stored Values
            UnitStoreData_LocTooltip_ModIntSO targeterValue = ScriptableObject.CreateInstance<UnitStoreData_LocTooltip_ModIntSO>();
            targeterValue.m_Text = "Target Slot: {0}";
            targeterValue._UnitStoreDataID = "TargeterStoredValue";
            targeterValue.m_TextColor = Color.red;
            targeterValue.m_CompareDataToThis = 0;
            targeterValue.m_ShowIfDataIsOver = true;
            LoadedDBsHandler.MiscDB.AddNewUnitStoreData("TargeterStoredValue", targeterValue);

            // Shy - Skittish, but only if there is an opposing unit.
            PerformEffectPassiveAbility shy = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
            shy.name = "Shy_PA";
            shy._passiveName = "Shy";
            shy.m_PassiveID = "Shy";
            shy.passiveIcon = ResourceLoader.LoadSprite("IconShy");
            shy._characterDescription = "Upon performing an ability, this party member will move to the left or right, prioritizing unopposed spaces, if there is an enemy opposing them.";
            shy._enemyDescription = "Upon performing an ability, this enemy will move to the left or right, prioritizing unopposed spaces, if there is a party member opposing them.";
            shy._triggerOn = [TriggerCalls.OnAbilityUsed];
            shy.effects =
            [
                Effects.GenerateEffect(ScriptableObject.CreateInstance<CheckHasUnitEffect>(), 1, Targeting.Slot_Front),
                Effects.GenerateEffect(NavigatorNotOpposing, 1, Targeting.Slot_SelfSlot, PreviousTrue),
                Effects.GenerateEffect(Right, 1, Targeting.Slot_SelfSlot, Effects.CheckMultiplePreviousEffectsCondition([true, true], [1, 2])),
                Effects.GenerateEffect(Left, 1, Targeting.Slot_SelfSlot, Effects.CheckMultiplePreviousEffectsCondition([false, true], [2, 3])),
            ];

            // Confrontational - Skittish, but only if there is NOT an opposing unit.
            PerformEffectPassiveAbility confrontational = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
            confrontational.name = "Confrontational_PA";
            confrontational._passiveName = "Confrontational";
            confrontational.m_PassiveID = "Confrontational";
            confrontational.passiveIcon = ResourceLoader.LoadSprite("IconConfrontational");
            confrontational._characterDescription = "Upon performing an ability, this party member will move to the left or right, prioritizing, opposed spaces, unless there is an enemy opposing them.";
            confrontational._enemyDescription = "Upon performing an ability, this enemy will move to the left or right, prioritizing, opposed spaces, unless there is a party member opposing them.";
            confrontational._triggerOn = [TriggerCalls.OnAbilityUsed];
            confrontational.effects =
            [
                Effects.GenerateEffect(ScriptableObject.CreateInstance<CheckHasUnitEffect>(), 1, Targeting.Slot_Front),
                Effects.GenerateEffect(NavigatorOpposing, 1, Targeting.Slot_SelfSlot, PreviousFalse),
                Effects.GenerateEffect(Right, 1, Targeting.Slot_SelfSlot, Effects.CheckMultiplePreviousEffectsCondition([true, false], [1, 2])),
                Effects.GenerateEffect(Left, 1, Targeting.Slot_SelfSlot, Effects.CheckMultiplePreviousEffectsCondition([false, false], [2, 3])),
            ];

            // Decay (Abandoned Altar) - Decay variant for the Anomaly Miniboss including center position and a unique description.
            /*SpawnEnemyInSpecificSlotEffect SpawnAnomalyMiniboss = ScriptableObject.CreateInstance<SpawnEnemyInSpecificSlotEffect>();
            SpawnAnomalyMiniboss.enemy = AnomalyMiniboss.aanomaly_miniboss.enemy;
            SpawnAnomalyMiniboss._spawnTypeID = CombatType_GameIDs.Spawn_Basic.ToString();
            SpawnAnomalyMiniboss.spawnSlot = 2;

            PerformEffectPassiveAbility DecayAbandonedAltar = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
            DecayAbandonedAltar.m_PassiveID = Passives.Example_Decay_MudLung.m_PassiveID;
            DecayAbandonedAltar.passiveIcon = Passives.Example_Decay_MudLung.passiveIcon;
            DecayAbandonedAltar._characterDescription = "Not meant for party members.";
            DecayAbandonedAltar._enemyDescription = "Upon death the shell crumbles.";
            DecayAbandonedAltar.effects = [Effects.GenerateEffect(SpawnAnomalyMiniboss, 1)];
            DecayAbandonedAltar._triggerOn = [TriggerCalls.OnDeath];*/

            // Gnome - A passive with no proper effect, serves as both flavour and targetting assistant.
            PerformEffectPassiveAbility gnomePassive = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
            gnomePassive.name = "Gnome_PA";
            gnomePassive._passiveName = "Gnome";
            gnomePassive.m_PassiveID = "Gnome";
            gnomePassive.passiveIcon = ResourceLoader.LoadSprite("IconGnome");
            gnomePassive._characterDescription = "This party member is a gnome.";
            gnomePassive._enemyDescription = "This enemy is one or more gnomes.";
            gnomePassive._triggerOn = [TriggerCalls.TimelineEndReached];
            gnomePassive.doesPassiveTriggerInformationPanel = false;
            gnomePassive.effects =
            [
                Effects.GenerateEffect(ScriptableObject.CreateInstance<ExtraVariableForNextEffect>(), 1, Targeting.Slot_SelfSlot),
            ];

            // Free-Willed - A passive that works similarly to the Wheel of Fortune item - copied from Stew's Specimens
            PerformRandomAbilityEffect performRandomAbilityEffect = ScriptableObject.CreateInstance<PerformRandomAbilityEffect>();

            RefreshAbilityUseEffect refreshAbilityUseEffect = ScriptableObject.CreateInstance<RefreshAbilityUseEffect>();
            refreshAbilityUseEffect._doesExhaustInstead = true;

            PerformDoubleEffectPassiveAbility freeWillPassive = ScriptableObject.CreateInstance<PerformDoubleEffectPassiveAbility>();
            freeWillPassive.name = "AA_FreeWilled_PA";
            freeWillPassive._passiveName = "Free-Willed";
            freeWillPassive.m_PassiveID = "FreeWilled";
            freeWillPassive._characterDescription = "This party member acts of their own free will, but can still be manually moved.";
            freeWillPassive._enemyDescription = "Enemies already have free will. What did you expect would happen?";
            freeWillPassive._triggerOn = [TriggerCalls.OnTurnFinished];
            freeWillPassive.effects =
            [
                Effects.GenerateEffect(performRandomAbilityEffect, 1, Targeting.Slot_SelfSlot, null),
                Effects.GenerateEffect(refreshAbilityUseEffect, 1, Targeting.Slot_SelfSlot, null)
            ];
            freeWillPassive._secondTriggerOn = [TriggerCalls.OnTurnStart];
            freeWillPassive._secondEffects =
            [
                Effects.GenerateEffect(refreshAbilityUseEffect, 1, Targeting.Slot_SelfSlot, null)
            ];
            freeWillPassive.passiveIcon = ResourceLoader.LoadSprite("IconStewSpecimensFreeWill", null, 32, null);

            // Heterochromia - Essentially Four-Faced but with any damage.
            PerformEffectPassiveAbility colors = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
            colors.name = "AA_Heterochromia_PA";
            colors._passiveName = "Heterochromia";
            colors.m_PassiveID = "Heterochromia";
            colors.passiveIcon = ResourceLoader.LoadSprite("IconHemochromia");
            colors._enemyDescription = "Upon receiving any kind of damage, randomize this enemy's health colour.";
            colors._characterDescription = "Upon receiving any kind of damage, randomize this party member's health colour.";
            ChangeToRandomHealthColorEffect randomize = ScriptableObject.CreateInstance<ChangeToRandomHealthColorEffect>();
            randomize._healthColors = 
            [
                        Pigments.Blue,
                        Pigments.Red,
                        Pigments.Yellow,
                        Pigments.Purple
            ];
            colors.effects =
            [
                        Effects.GenerateEffect(randomize, 1, Targeting.Slot_SelfSlot)
            ];
            colors._triggerOn = 
            [
                        TriggerCalls.OnDamaged
            ];

            // Torn Apart - Gutted Restrictor.
            StatusEffectPassiveAbility tornApart = ScriptableObject.CreateInstance<StatusEffectPassiveAbility>();
            tornApart.name = "AA_TornApart_PA";
            tornApart._passiveName = "Torn Apart";
            tornApart.m_PassiveID = "TornApart";
            tornApart.passiveIcon = StatusField.Gutted._EffectInfo.icon;
            tornApart._enemyDescription = "This enemy is permanently Gutted.";
            tornApart._characterDescription = "This party member is permanently Gutted.";
            tornApart._Status = StatusField.Gutted;

            // Jumpy - Erratic movement passive from Salt Enemies
            PerformEffectPassiveAbility jumpy = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
            jumpy.name = "AA_Jumpy_PA";
            jumpy._passiveName = "Jumpy";
            jumpy.m_PassiveID = "Jumpy_PA";
            jumpy.passiveIcon = ResourceLoader.LoadSprite("IconJumpy");
            jumpy._characterDescription = "Upon being damaged, move to a random position. Upon performing an ability, move to a random position.";
            jumpy._enemyDescription = "Upon being damaged, move to a random position. Upon performing an ability, move to a random position.";
            jumpy.doesPassiveTriggerInformationPanel = true;
            jumpy.effects =
            [
                Effects.GenerateEffect(ScriptableObject.CreateInstance<SwapToRandomZoneEffect>(), 1, Targeting.GenerateSlotTarget(new int[9] { -4, -3, -2, -1, 0, 1, 2, 3, 4 }, true)),
            ];
            jumpy._triggerOn = [ TriggerCalls.OnDirectDamaged, TriggerCalls.OnAbilityUsed ];

            // Omnichromia - Heterochromia with additional crazy.
            PerformEffectPassiveAbility hypercolors = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
            hypercolors.name = "Omnichromia_PA";
            hypercolors._passiveName = "Omnichromia";
            hypercolors.m_PassiveID = "Omnichromia";
            hypercolors.passiveIcon = ResourceLoader.LoadSprite("IconOmnichromia");
            hypercolors._enemyDescription = "Upon receiving any kind of damage or performing an ability, randomize this enemy's health colour. This includes unusual and split pigment.";
            hypercolors._characterDescription = "Upon receiving any kind of damage or performing an ability, randomize this party member's health colour. This includes unusual and split pigment.";
            ChangeToRandomHealthColorEffect hyperrandomize = ScriptableObject.CreateInstance<ChangeToRandomHealthColorEffect>();
            List<ManaColorSO> omniColorsList = new List<ManaColorSO>();
            omniColorsList.AddRange([
                Pigments.Red,
                Pigments.Red,
                Pigments.Blue,
                Pigments.Blue,
                Pigments.Yellow,
                Pigments.Yellow,
                Pigments.Purple,
                Pigments.Purple,
                Pigments.RedBlue,
                Pigments.BlueRed,
                Pigments.YellowRed,
                Pigments.YellowBlue,
                Pigments.RedPurple,
                Pigments.BluePurple,
                Pigments.YellowPurple,
                Pigments.Grey,
                Pigments.SplitPigment(Pigments.Red, Pigments.Blue, Pigments.Yellow),
                Pigments.SplitPigment(Pigments.Red, Pigments.Blue, Pigments.Purple),
                Pigments.SplitPigment(Pigments.Yellow, Pigments.Purple, Pigments.Red),
                Pigments.SplitPigment(Pigments.Yellow, Pigments.Purple, Pigments.Blue),
                Pigments.SplitPigment(Pigments.Red, Pigments.Blue, Pigments.Yellow, Pigments.Purple),
            ]);
            if (AApocrypha.CrossMod.IntoTheAbyss)
            {
                Debug.Log("Omnichromia - Into The Abyss pigments loaded");
                omniColorsList.Add(LoadedDBsHandler.PigmentDB.GetPigment("Iridescent"));
                omniColorsList.Add(LoadedDBsHandler.PigmentDB.GetPigment("Clusterfuck"));
                omniColorsList.Add(LoadedDBsHandler.PigmentDB.GetPigment("EntropicBase"));
            }
            if (AApocrypha.CrossMod.pigmentGilded)
            {
                omniColorsList.Add(LoadedDBsHandler.PigmentDB.GetPigment("Gilded"));
                Debug.Log("Omnichromia - Gilded pigment loaded");
            }
            if (AApocrypha.CrossMod.pigmentRainbow)
            {
                omniColorsList.Add(LoadedDBsHandler.PigmentDB.GetPigment("Rainbow"));
                Debug.Log("Omnichromia - Rainbow pigment loaded");
            }
            if (AApocrypha.CrossMod.pigmentPeppermint)
            {
                omniColorsList.Add(LoadedDBsHandler.PigmentDB.GetPigment("Peppermint"));
                Debug.Log("Omnichromia - Peppermint pigment loaded");
            }
            if (AApocrypha.CrossMod.pigmentPink)
            {
                omniColorsList.Add(LoadedDBsHandler.PigmentDB.GetPigment("Pink"));
                Debug.Log("Omnichromia - Pink pigment loaded");
            }
            hyperrandomize._healthColors = omniColorsList.ToArray();
            hypercolors.effects =
            [
                        Effects.GenerateEffect(hyperrandomize, 1, Targeting.Slot_SelfSlot)
            ];
            hypercolors._triggerOn =
            [
                        TriggerCalls.OnDamaged,
                        TriggerCalls.OnAbilityUsed,
                        TriggerCalls.OnCombatStart,
            ];

            // Gouged - Missing an eye! Reduces damage dealt by 25%.
            PercDmgModPassiveAbility gougedPassive = ScriptableObject.CreateInstance<PercDmgModPassiveAbility>();
            gougedPassive.name = "Gouged_PA";
            gougedPassive._passiveName = "Gouged";
            gougedPassive.m_PassiveID = "Gouged";
            gougedPassive.passiveIcon = ResourceLoader.LoadSprite("IconGouged");
            gougedPassive._characterDescription = "This party member is missing an eye, their reduced accuracy decreasing damage dealt by 25%.";
            gougedPassive._enemyDescription = "This enemy is missing an eye, their reduced accuracy decreasing damage dealt by 25%.";
            gougedPassive.doesPassiveTriggerInformationPanel = true;
            gougedPassive._triggerOn = [TriggerCalls.OnWillApplyDamage];
            gougedPassive._doesIncrease = false;
            gougedPassive._useDealt = true;
            gougedPassive._useSimpleInt = false;
            gougedPassive._percentageToModify = 25;

            // Made of Fire - simple fire immunity passive
            DamageTypeImmunityPassiveAbility fireproofPassive = ScriptableObject.CreateInstance<DamageTypeImmunityPassiveAbility>();
            fireproofPassive.name = "MadeOfFire_PA";
            fireproofPassive._passiveName = "Made Of Fire";
            fireproofPassive.m_PassiveID = "MadeOfFire";
            fireproofPassive.passiveIcon = ResourceLoader.LoadSprite("IconFireskull");
            fireproofPassive._characterDescription = "This party member is unaffected by Fire and immune to fire damage.";
            fireproofPassive._enemyDescription = "This enemy is unaffected by Fire and immune to fire damage.";
            fireproofPassive.doesPassiveTriggerInformationPanel = false;
            fireproofPassive._triggerOn = [TriggerCalls.OnBeingDamaged];
            fireproofPassive._damageType = CombatType_GameIDs.Dmg_Fire.ToString();
            // the passive itself only handles the damage immunity - fire is prevented from activating by Patches/FireBlockerPatch.cs

            // Dried Out - ruptured *damage* immunity passive
            DamageTypeImmunityPassiveAbility dryPassive = ScriptableObject.CreateInstance<DamageTypeImmunityPassiveAbility>();
            dryPassive.name = "DriedOut_PA";
            dryPassive._passiveName = "Dried Out";
            dryPassive.m_PassiveID = "DriedOut";
            dryPassive.passiveIcon = ResourceLoader.LoadSprite("IconDriedOut");
            dryPassive._characterDescription = "This party member is immune to damage from Ruptured.";
            dryPassive._enemyDescription = "This enemy is immune to damage from Ruptured.";
            dryPassive.doesPassiveTriggerInformationPanel = false;
            dryPassive._triggerOn = [TriggerCalls.OnBeingDamaged];
            dryPassive._damageType = CombatType_GameIDs.Dmg_Ruptured.ToString();

            // Pigment-Blooded - some pigment when hit passives

            PerformEffectPassiveAbility redBlooded = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
            redBlooded.name = "RedBlooded_1_PA";
            redBlooded._passiveName = "Red-Blooded (1)";
            redBlooded.m_PassiveID = "PigmentBlooded";
            redBlooded.passiveIcon = ResourceLoader.LoadSprite("IconStonebloodRed");
            redBlooded._characterDescription = "Upon receiving direct damage this party member produces 1 additional Red pigment.";
            redBlooded._enemyDescription = "Upon receiving direct damage this enemy produces 1 additional Red pigment.";
            redBlooded._triggerOn = [TriggerCalls.OnDirectDamaged];
            redBlooded.doesPassiveTriggerInformationPanel = true;
            redBlooded.effects =
            [
                Effects.GenerateEffect(GiveRedPigment, 1, Targeting.Slot_SelfSlot),
            ];

            PerformEffectPassiveAbility blueBlooded = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
            blueBlooded.name = "BlueBlooded_1_PA";
            blueBlooded._passiveName = "Blue-Blooded (1)";
            blueBlooded.m_PassiveID = "PigmentBlooded";
            blueBlooded.passiveIcon = ResourceLoader.LoadSprite("IconStonebloodBlue");
            blueBlooded._characterDescription = "Upon receiving direct damage this party member produces 1 additional Blue pigment.";
            blueBlooded._enemyDescription = "Upon receiving direct damage this enemy produces 1 additional Blue pigment.";
            blueBlooded._triggerOn = [TriggerCalls.OnDirectDamaged];
            blueBlooded.doesPassiveTriggerInformationPanel = true;
            blueBlooded.effects =
            [
                Effects.GenerateEffect(GiveBluePigment, 1, Targeting.Slot_SelfSlot),
            ];

            PerformEffectPassiveAbility yellowBlooded = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
            yellowBlooded.name = "YellowBlooded_1_PA";
            yellowBlooded._passiveName = "Yellow-Blooded (1)";
            yellowBlooded.m_PassiveID = "PigmentBlooded";
            yellowBlooded.passiveIcon = ResourceLoader.LoadSprite("IconStonebloodYellow");
            yellowBlooded._characterDescription = "Upon receiving direct damage this party member produces 1 additional Yellow pigment.";
            yellowBlooded._enemyDescription = "Upon receiving direct damage this enemy produces 1 additional Yellow pigment.";
            yellowBlooded._triggerOn = [TriggerCalls.OnDirectDamaged];
            yellowBlooded.doesPassiveTriggerInformationPanel = true;
            yellowBlooded.effects =
            [
                Effects.GenerateEffect(GiveYellowPigment, 1, Targeting.Slot_SelfSlot),
            ];

            PerformEffectPassiveAbility purpleBlooded = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
            purpleBlooded.name = "PurpleBlooded_1_PA";
            purpleBlooded._passiveName = "Purple-Blooded (1)";
            purpleBlooded.m_PassiveID = "PigmentBlooded";
            purpleBlooded.passiveIcon = ResourceLoader.LoadSprite("IconStonebloodPurple");
            purpleBlooded._characterDescription = "Upon receiving direct damage this party member produces 1 additional Purple pigment.";
            purpleBlooded._enemyDescription = "Upon receiving direct damage this enemy produces 1 additional Purple pigment.";
            purpleBlooded._triggerOn = [TriggerCalls.OnDirectDamaged];
            purpleBlooded.doesPassiveTriggerInformationPanel = true;
            purpleBlooded.effects =
            [
                Effects.GenerateEffect(GivePurplePigment, 1, Targeting.Slot_SelfSlot),
            ];

            // Condense - humor passive that fills pigment bare (thanks wavetamer)
            PerformEffectPassiveAbility condensePassive = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
            condensePassive.name = "AA_Condense_PA";
            condensePassive._passiveName = "Condense";
            condensePassive.m_PassiveID = "Condense";
            condensePassive.passiveIcon = ResourceLoader.LoadSprite("Condense_passive");
            condensePassive._characterDescription = "Upon death, fill the pigment bar with pigment of this party member's health color.";
            condensePassive._enemyDescription = "Upon death, fill the pigment bar with pigment of this enemy's health color.";
            condensePassive.doesPassiveTriggerInformationPanel = true;
            condensePassive.effects =
            [
                Effects.GenerateEffect(ScriptableObject.CreateInstance<GenerateCasterHealthManaFillPigmentBarEffect>(), 1, Targeting.Slot_SelfSlot),
            ];
            condensePassive._triggerOn = [TriggerCalls.OnDeath];

            GenerateColorManaFillPigmentBarEffect AllRed = ScriptableObject.CreateInstance<GenerateColorManaFillPigmentBarEffect>();
            AllRed.mana = Pigments.Red;

            GenerateColorManaFillPigmentBarEffect AllBlue = ScriptableObject.CreateInstance<GenerateColorManaFillPigmentBarEffect>();
            AllBlue.mana = Pigments.Blue;

            GenerateColorManaFillPigmentBarEffect AllYellow = ScriptableObject.CreateInstance<GenerateColorManaFillPigmentBarEffect>();
            AllYellow.mana = Pigments.Yellow;

            GenerateColorManaFillPigmentBarEffect AllPurple = ScriptableObject.CreateInstance<GenerateColorManaFillPigmentBarEffect>();
            AllPurple.mana = Pigments.Purple;

            PerformEffectPassiveAbility condenseRed = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
            condenseRed.name = "AA_CondenseRed_PA";
            condenseRed._passiveName = "Condense (Red)";
            condenseRed.m_PassiveID = "Condense";
            condenseRed.passiveIcon = ResourceLoader.LoadSprite("Condense_passive_red");
            condenseRed._characterDescription = "Upon death, fill the pigment bar with Red pigment.";
            condenseRed._enemyDescription = condenseRed._characterDescription;
            condenseRed._triggerOn = [TriggerCalls.OnDeath];
            condenseRed.doesPassiveTriggerInformationPanel = true;
            condenseRed.effects =
            [
                Effects.GenerateEffect(AllRed, 1, Targeting.Slot_SelfSlot),
            ];

            PerformEffectPassiveAbility condenseBlue = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
            condenseBlue.name = "AA_CondenseBlue_PA";
            condenseBlue._passiveName = "Condense (Blue)";
            condenseBlue.m_PassiveID = "Condense";
            condenseBlue.passiveIcon = ResourceLoader.LoadSprite("Condense_passive_blue");
            condenseBlue._characterDescription = "Upon death, fill the pigment bar with Blue pigment.";
            condenseBlue._enemyDescription = condenseBlue._characterDescription;
            condenseBlue._triggerOn = [TriggerCalls.OnDeath];
            condenseBlue.doesPassiveTriggerInformationPanel = true;
            condenseBlue.effects =
            [
                Effects.GenerateEffect(AllBlue, 1, Targeting.Slot_SelfSlot),
            ];

            PerformEffectPassiveAbility condenseYellow = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
            condenseYellow.name = "AA_CondenseYellow_PA";
            condenseYellow._passiveName = "Condense (Yellow)";
            condenseYellow.m_PassiveID = "Condense";
            condenseYellow.passiveIcon = ResourceLoader.LoadSprite("Condense_passive_yellow");
            condenseYellow._characterDescription = "Upon death, fill the pigment bar with Yellow pigment.";
            condenseYellow._enemyDescription = condenseYellow._characterDescription;
            condenseYellow._triggerOn = [TriggerCalls.OnDeath];
            condenseYellow.doesPassiveTriggerInformationPanel = true;
            condenseYellow.effects =
            [
                Effects.GenerateEffect(AllYellow, 1, Targeting.Slot_SelfSlot),
            ];

            PerformEffectPassiveAbility condensePurple = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
            condensePurple.name = "AA_CondensePurple_PA";
            condensePurple._passiveName = "Condense (Purple)";
            condensePurple.m_PassiveID = "Condense";
            condensePurple.passiveIcon = ResourceLoader.LoadSprite("Condense_passive_purple");
            condensePurple._characterDescription = "Upon death, fill the pigment bar with Purple pigment.";
            condensePurple._enemyDescription = condensePurple._characterDescription;
            condensePurple._triggerOn = [TriggerCalls.OnDeath];
            condensePurple.doesPassiveTriggerInformationPanel = true;
            condensePurple.effects =
            [
                Effects.GenerateEffect(AllPurple, 1, Targeting.Slot_SelfSlot),
            ];

            StatusEffect_Apply_Effect OilApply = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            OilApply._Status = StatusField.OilSlicked;

            // Black Tears - oil slicked on movement passive (regent logos)
            PerformEffectPassiveAbility blackTears = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
            blackTears.name = "BlackTears_2_PA";
            blackTears._passiveName = "Black Tears (2)";
            blackTears.m_PassiveID = "BlackTears";
            blackTears.passiveIcon = ResourceLoader.LoadSprite("IconBlackTears");
            blackTears._characterDescription = "On moving, this party member gains 2 Oil Slicked.";
            blackTears._enemyDescription = "On moving, this enemy gains 2 Oil Slicked.";
            blackTears._triggerOn = [TriggerCalls.OnMoved];
            blackTears.doesPassiveTriggerInformationPanel = true;
            blackTears.effects =
            [
                Effects.GenerateEffect(OilApply, 2, Targeting.Slot_SelfSlot),
            ];

            // Adding to pool
            Passives.AddCustomPassiveToPool("Shy_PA", "Shy", shy);
            Passives.AddCustomPassiveToPool("Confrontational_PA", "Confrontational", confrontational);
            //Passives.AddCustomPassiveToPool("DecayAbandonedAltar_PA", "Decay", DecayAbandonedAltar);
            Passives.AddCustomPassiveToPool("Gnome_PA", "Gnome", gnomePassive);
            Passives.AddCustomPassiveToPool("AA_FreeWilled_PA", "Free-Willed", freeWillPassive);
            Passives.AddCustomPassiveToPool("AA_Heterochromia_PA", "Heterochromia", colors);
            Passives.AddCustomPassiveToPool("Omnichromia_PA", "Omnichromia", hypercolors);
            Passives.AddCustomPassiveToPool("AA_TornApart_PA", "Torn Apart", tornApart);
            Passives.AddCustomPassiveToPool("AA_Jumpy_PA", "Jumpy", jumpy);
            Passives.AddCustomPassiveToPool("Gouged_PA", "Gouged", gougedPassive);
            Passives.AddCustomPassiveToPool("MadeOfFire_PA", "Made Of Fire", fireproofPassive);
            Passives.AddCustomPassiveToPool("DriedOut_PA", "Dried Out", dryPassive);
            Passives.AddCustomPassiveToPool("AA_Condense_PA", "Condense", condensePassive);
            Passives.AddCustomPassiveToPool("AA_CondenseRed_PA", "Condense (Red)", condenseRed);
            Passives.AddCustomPassiveToPool("AA_CondenseBlue_PA", "Condense (Blue)", condenseBlue);
            Passives.AddCustomPassiveToPool("AA_CondenseYellow_PA", "Condense (Yellow)", condenseYellow);
            Passives.AddCustomPassiveToPool("AA_CondensePurple_PA", "Condense (Purple)", condensePurple);
            Passives.AddCustomPassiveToPool("RedBlooded_1_PA", "Red-Blooded (1)", redBlooded);
            Passives.AddCustomPassiveToPool("BlueBlooded_1_PA", "Blue-Blooded (1)", blueBlooded);
            Passives.AddCustomPassiveToPool("YellowBlooded_1_PA", "Yellow-Blooded (1)", yellowBlooded);
            Passives.AddCustomPassiveToPool("PurpleBlooded_1_PA", "Purple-Blooded (1)", purpleBlooded);
            Passives.AddCustomPassiveToPool("BlackTears_2_PA", "Black Tears (2)", blackTears);

            // Glossary entries
            GlossaryPassives AAShyInfo = new GlossaryPassives("Shy", "Upon performing an ability, this party member/enemy will move to the left or right, prioritizing unopposed spaces, if there is an enemy/party member opposing them.", ResourceLoader.LoadSprite("IconShy"));
            GlossaryPassives AAConfrontationalInfo = new GlossaryPassives("Confrontational", "Upon performing an ability, this party member/enemy will move to the left or right, prioritizing opposed spaces, unless there is an enemy/party member opposing them.", ResourceLoader.LoadSprite("IconConfrontational"));
            GlossaryPassives AACopyThatInfo = new GlossaryPassives("Copy That", "At the start of combat, select a set amount of random party members. Copy one passive and two abilities (excluding Slap) from each selected party member onto this enemy. Change this enemy's health color to a combination of all selected party members' health colors.", ResourceLoader.LoadSprite("IconCopyThat"));
            GlossaryPassives AAGnomeInfo = new GlossaryPassives("Gnome", "This unit is one or more gnomes.", ResourceLoader.LoadSprite("IconGnome"));
            GlossaryPassives AAOmnichromiaInfo = new GlossaryPassives("Omnichromia", "Upon receiving any kind of damage or performing an ability, randomize this unit's health colour. This includes unusual and split pigment.", ResourceLoader.LoadSprite("IconOmnichromia"));
            GlossaryPassives AAGougedInfo = new GlossaryPassives("Gouged", "This unit is missing an eye, their reduced accuracy decreasing damage dealt by 25%.", ResourceLoader.LoadSprite("IconGouged"));
            GlossaryPassives AAMadeOfFireInfo = new GlossaryPassives("Made Of Fire", "This unit is immune to fire damage.", ResourceLoader.LoadSprite("IconFireskull"));
            GlossaryPassives AADriedOutInfo = new GlossaryPassives("Dried Out", "This unit is immune to damage from Ruptured.", ResourceLoader.LoadSprite("IconDriedOut"));
            GlossaryPassives AAMercurialInfo = new GlossaryPassives("Mercurial", "At the end of the timeline, if a certain condition is met, this enemy transforms into a different enemy.", ResourceLoader.LoadSprite("IconTransformPassive"));
            GlossaryPassives AAPigmentBloodedInfo = new GlossaryPassives("Pigment-Blooded", "Upon receiving direct damage this party member/enemy produces additional pigment of a specific color.", ResourceLoader.LoadSprite("IconStonebloodPrimary"));
            GlossaryPassives AATargeterInfo = new GlossaryPassives("Targeter", "At the start of combat and at the end of the timeline, this enemy will remember the position of the party member with the highest current health.", ResourceLoader.LoadSprite("IconTargeter"));
            GlossaryPassives AADeploymentInfo = new GlossaryPassives("Deployment", "Dealing damage to this enemy greater than a certain threshold will cause it to attempt to summon a specific enemy.", ResourceLoader.LoadSprite("IconDeployment")); // icon sprited by MillieAmp
            GlossaryPassives AACondenseInfo = new GlossaryPassives("Condense", "Upon death, fill the pigment bar with pigment of this party member/enemy's health color.", ResourceLoader.LoadSprite("Condense_passive"));
            GlossaryPassives AABlackTearsInfo = new GlossaryPassives("Black Tears", "On moving, this unit gains a certain amount of Oil Slicked.", ResourceLoader.LoadSprite("IconBlackTears"));

            LoadedDBsHandler.GlossaryDB.AddNewPassive(AAShyInfo);
            LoadedDBsHandler.GlossaryDB.AddNewPassive(AAConfrontationalInfo);
            LoadedDBsHandler.GlossaryDB.AddNewPassive(AACopyThatInfo);
            LoadedDBsHandler.GlossaryDB.AddNewPassive(AAGnomeInfo);
            LoadedDBsHandler.GlossaryDB.AddNewPassive(AAOmnichromiaInfo);
            LoadedDBsHandler.GlossaryDB.AddNewPassive(AAGougedInfo);
            LoadedDBsHandler.GlossaryDB.AddNewPassive(AAMadeOfFireInfo);
            LoadedDBsHandler.GlossaryDB.AddNewPassive(AADriedOutInfo);
            LoadedDBsHandler.GlossaryDB.AddNewPassive(AAMercurialInfo);
            LoadedDBsHandler.GlossaryDB.AddNewPassive(AAPigmentBloodedInfo);
            LoadedDBsHandler.GlossaryDB.AddNewPassive(AATargeterInfo);
            LoadedDBsHandler.GlossaryDB.AddNewPassive(AADeploymentInfo);
            LoadedDBsHandler.GlossaryDB.AddNewPassive(AACondenseInfo);
            LoadedDBsHandler.GlossaryDB.AddNewPassive(AABlackTearsInfo);

            if (!AApocrypha.CrossMod.StewSpecimens)
            {
                GlossaryPassives AAFreeWillInfo = new GlossaryPassives("Free-Willed", "This party member acts of their own free will, but can still be manually moved.", ResourceLoader.LoadSprite("IconStewSpecimensFreeWill"));
                LoadedDBsHandler.GlossaryDB.AddNewPassive(AAFreeWillInfo);
            }

            if (!AApocrypha.CrossMod.SaltEnemies)
            {
                GlossaryPassives AAHeterochromiaInfo = new GlossaryPassives("Heterochromia", "Upon receiving any kind of damage, randomize this unit's health colour.", ResourceLoader.LoadSprite("IconHemochromia"));
                LoadedDBsHandler.GlossaryDB.AddNewPassive(AAHeterochromiaInfo);
                GlossaryPassives AAJumpyInfo = new GlossaryPassives("Jumpy", "Upon being damaged, move to a random position. Upon performing an ability, move to a random position.", ResourceLoader.LoadSprite("IconJumpy"));
                LoadedDBsHandler.GlossaryDB.AddNewPassive(AAJumpyInfo);
                GlossaryPassives AATornApartInfo = new GlossaryPassives("Torn Apart", "This unit is permanently Gutted.", StatusField.Gutted._EffectInfo.icon);
                LoadedDBsHandler.GlossaryDB.AddNewPassive(AATornApartInfo);
            }

            if (!AApocrypha.CrossMod.HellIslandFell)
            {
                GlossaryPassives AltAttacksInfo = new GlossaryPassives("Alt Attacks", "This enemy will perform an additional ability each turn, this ability is randomly selected from a given set", ResourceLoader.LoadSprite("PassiveAltAttacks"));
                LoadedDBsHandler.GlossaryDB.AddNewPassive(AltAttacksInfo);
            }
        }

        // Copy That - The unhinged ability copying passive of the Simulacrum. Also handles an achievement.
        private static readonly Dictionary<int, BasePassiveAbilitySO> GeneratedCopyThat = [];

        public static BasePassiveAbilitySO CopyThatGenerator(int amount)
        {
            TryUnlockAchievementEffect SimulacrumAchievement = ScriptableObject.CreateInstance<TryUnlockAchievementEffect>();
            SimulacrumAchievement._unlockID = "ComedySimulacrumKillSelf";

            PreviousEffectCondition PreviousFalse = ScriptableObject.CreateInstance<PreviousEffectCondition>();
            PreviousFalse.wasSuccessful = false;

            return GetOrCreatePassive(GeneratedCopyThat, amount, delegate (int x)
            {
                PerformDoubleEffectPassiveAbility copythat = ScriptableObject.CreateInstance<PerformDoubleEffectPassiveAbility>();
                copythat.name = $"CopyThat_{x}_PA";
                copythat.m_PassiveID = "CopyThat_PA";
                copythat._passiveName = $"Copy That ({x})";
                copythat.passiveIcon = ResourceLoader.LoadSprite("IconCopyThat");
                copythat._characterDescription = "Not designed for party members, something might break HARD. Maybe someday...";
                copythat._enemyDescription = $"At the start of combat, select {x} random party members. Copy one passive and two abilities (excluding Slap) from each selected party member onto this enemy. Change this enemy's health color to a combination of all selected party members' health colors.";
                copythat._triggerOn = [TriggerCalls.OnBeforeCombatStart];
                copythat.effects = [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<CopyThatEffect>(), x, Targeting.Unit_AllOpponents),
                ];
                copythat._secondTriggerOn = [TriggerCalls.OnDeath];
                copythat._secondPerformConditions = [ScriptableObject.CreateInstance<IsSelfDeathCondition>()];
                copythat._secondEffects = [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<CheckIsPlayerTurnEffect>(), 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(SimulacrumAchievement, 1, Targeting.Slot_SelfSlot, PreviousFalse)
                ];
                copythat.doesPassiveTriggerInformationPanel = true;
                copythat._secondDoesPerformPopUp = false;
                return copythat;
            });
        }

        // Masochism (X): Alternate version of numbered Masochism that uses the number as a to-hit threshold instead of the amount of actions queued
        // (partially taken from/"inspired by" the Ruinful Revelry github)
        private static readonly Dictionary<int, BasePassiveAbilitySO> GeneratedThresholdMasochism = [];

        public static BasePassiveAbilitySO ThresholdMasochismGenerator(int amount)
        {
            ReturnValueComparatorEffectorCondition AmountOrHigher = ScriptableObject.CreateInstance<ReturnValueComparatorEffectorCondition>();
            AmountOrHigher._comparator = amount;
            AmountOrHigher._lessThan = false;

            return GetOrCreatePassive(GeneratedThresholdMasochism, amount, delegate (int x)
            {
                PerformEffectPassiveAbility masochismalt = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
                masochismalt.name = $"AA_Threshold_Masochism_{x}_PA";
                masochismalt.m_PassiveID = "AA_Threshold_Masochism";
                masochismalt._passiveName = $"Masochism ({x})";
                masochismalt.passiveIcon = Passives.Masochism1.passiveIcon;
                masochismalt._characterDescription = $"When this party member takes damage equal to or greater than {x}, refresh their movement and abilities.";
                masochismalt._enemyDescription = $"When this enemy takes damage equal to or greather than {x}, they will add an additional action to the timeline.";
                masochismalt._triggerOn = [TriggerCalls.OnDamaged];
                masochismalt.conditions = [AmountOrHigher];
                masochismalt.effects = [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<CasterMasochismEffect>()),
                ];
                masochismalt.doesPassiveTriggerInformationPanel = true;
                return masochismalt;
            });
        }

        // Alt Attacks: multi-ability variant of Bonus Attack from Hell Island Fell
        public static BasePassiveAbilitySO AltAttacksGenerator(List<ExtraAbilityInfo> bonusAbilities)
        {
            List<string> names = [];
            List<ExtraAbilityInfo> weights = [];
            foreach (ExtraAbilityInfo ability in bonusAbilities)
            {
                if (ability == null || ability.ability == null)
                {
                    Debug.Log("Null alt ability: " + ability.ability.name);
                    return null;
                }
                names.Add(ability.ability._abilityName);
                for (int i = 0; i < ability.rarity.rarityValue; i++)
                {
                    weights.Add(ability);
                }
                if (ability.rarity.canBeRerolled)
                {
                    ability.rarity = Rarity.Impossible;
                } 
                else
                {
                    ability.rarity = Rarity.ImpossibleNoReroll;
                }
            }

            AltAttacksPassiveAbility altAttacksPassiveAbility = ScriptableObject.CreateInstance<AltAttacksPassiveAbility>();

            altAttacksPassiveAbility.name = string.Join("_", names) + "_PA";
            altAttacksPassiveAbility.m_PassiveID = string.Join("_", names);
            altAttacksPassiveAbility._passiveName = "Alt Attacks";
            altAttacksPassiveAbility._characterDescription = "This passive is not meant for party members.";
            altAttacksPassiveAbility._enemyDescription = "This enemy will perform an additional ability each turn, this ability is randomly selected from the following:" + "\n" + string.Join("\n", names);
            altAttacksPassiveAbility.passiveIcon = ResourceLoader.LoadSprite("PassiveAltAttacks");
            altAttacksPassiveAbility._triggerOn = [TriggerCalls.ExtraAdditionalAttacks];
            altAttacksPassiveAbility.conditions = [];
            altAttacksPassiveAbility.doesPassiveTriggerInformationPanel = false;
            altAttacksPassiveAbility.specialStoredData = null;
            altAttacksPassiveAbility._altAbilities = bonusAbilities;
            altAttacksPassiveAbility._weights = weights;
            return altAttacksPassiveAbility;
        }

        // from hell island fell (custom passives)
        private static TValue GetOrCreatePassive<TKey, TValue>(IDictionary<TKey, TValue> readFrom, TKey key, Func<TKey, TValue> create)
        {
            if (readFrom.TryGetValue(key, out TValue value))
            {
                return value;
            }

            return readFrom[key] = create(key);
        }
        static PreviousEffectCondition PreviousGenerator(bool wasTrue, int number)
        {
            PreviousEffectCondition previous = ScriptableObject.CreateInstance<PreviousEffectCondition>();
            previous.wasSuccessful = wasTrue;
            previous.previousAmount = number;
            return previous;
        }
    }
    public class IsSelfDeathCondition : EffectorConditionSO
    {
        public override bool MeetCondition(IEffectorChecks effector, object args)
        {
            if (args is DeathReference reffe && reffe.killer == effector) return true;
            return false;
        }
    }
}
