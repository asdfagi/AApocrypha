using System;
using System.Collections.Generic;
using System.Text;
using A_Apocrypha.Assets;
using A_Apocrypha.Custom_Passives;
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

            ForceGenerateColorManaEffect GiveGreyPigment = ScriptableObject.CreateInstance<ForceGenerateColorManaEffect>();
            GiveGreyPigment.mana = Pigments.Grey;

            // Stored Values
            UnitStoreData_LocTooltip_ModIntSO targeterValue = ScriptableObject.CreateInstance<UnitStoreData_LocTooltip_ModIntSO>();
            targeterValue.m_Text = "Target Slot: {0}";
            targeterValue._UnitStoreDataID = "TargeterStoredValue";
            targeterValue.m_TextColor = Color.red;
            targeterValue.m_CompareDataToThis = 0;
            targeterValue.m_ShowIfDataIsOver = true;
            LoadedDBsHandler.MiscDB.AddNewUnitStoreData("TargeterStoredValue", targeterValue);

            UnitStoreData_CombatAbilitySO repositoryValue = ScriptableObject.CreateInstance<UnitStoreData_CombatAbilitySO>();
            repositoryValue.m_Text = "Repository: {0}";
            repositoryValue._UnitStoreDataID = "RepositoryStoredValue";
            repositoryValue.m_TextColor = Color.yellow;
            LoadedDBsHandler.MiscDB.AddNewUnitStoreData("RepositoryStoredValue", repositoryValue);

            UnitStoreData_AnalysisTooltip_ModIntSO naudizAnalysisTracker = ScriptableObject.CreateInstance<UnitStoreData_AnalysisTooltip_ModIntSO>();
            naudizAnalysisTracker.m_Text = "Target: {0}";
            naudizAnalysisTracker._UnitStoreDataID = "NaudizCurrentStoredValue";
            naudizAnalysisTracker.m_TextColor = Color.cyan;
            LoadedDBsHandler.MiscDB.AddNewUnitStoreData("NaudizCurrentStoredValue", naudizAnalysisTracker);

            UnitStoreData_ModIntSO naudizKillTracker = ScriptableObject.CreateInstance<UnitStoreData_ModIntSO>();
            naudizKillTracker.m_Text = "Kills: {0}";
            naudizKillTracker._UnitStoreDataID = "NaudizKillStoredValue";
            naudizKillTracker.m_TextColor = Color.red;
            naudizKillTracker.m_CompareDataToThis = -1;
            naudizKillTracker.m_ShowIfDataIsOver = true;
            LoadedDBsHandler.MiscDB.AddNewUnitStoreData("NaudizKillStoredValue", naudizKillTracker);

            UnitStoreData_ModIntSO dogmaTracker = ScriptableObject.CreateInstance<UnitStoreData_ModIntSO>();
            dogmaTracker.m_Text = "Dogma: {0}";
            dogmaTracker._UnitStoreDataID = "DogmaStoredValue";
            dogmaTracker.m_TextColor = new Color32(200, 200, 200, 255);
            dogmaTracker.m_CompareDataToThis = -1;
            dogmaTracker.m_ShowIfDataIsOver = true;
            LoadedDBsHandler.MiscDB.AddNewUnitStoreData("DogmaStoredValue", dogmaTracker);

            UnitStoreData_ModIntSO alterTracker = ScriptableObject.CreateInstance<UnitStoreData_ModIntSO>();
            alterTracker.m_Text = "Alter: {0}";
            alterTracker._UnitStoreDataID = "AlterStoredValue";
            alterTracker.m_TextColor = new Color32(200, 0, 200, 255);
            alterTracker.m_CompareDataToThis = -1;
            alterTracker.m_ShowIfDataIsOver = true;
            LoadedDBsHandler.MiscDB.AddNewUnitStoreData("AlterStoredValue", alterTracker);

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

            ModifyRunIntDataEffect GnomeCounter = ScriptableObject.CreateInstance<ModifyRunIntDataEffect>();
            GnomeCounter._additive = true;
            GnomeCounter._data = "GnomeDeathCounter";

            IsCharacterEffectorCondition IsCharacter = ScriptableObject.CreateInstance<IsCharacterEffectorCondition>();
            IsCharacter._passIfTrue = true;

            TryUnlockAchievementEffect GnomeHatAchievement = ScriptableObject.CreateInstance<TryUnlockAchievementEffect>();
            GnomeHatAchievement._unlockID = "ComedyGnomeDeath";

            RunIntDataComparatorEffect GnomeCheck = ScriptableObject.CreateInstance<RunIntDataComparatorEffect>();
            GnomeCheck._data = "GnomeDeathCounter";
            GnomeCheck._lessThan = false;

            // Gnome - A passive with no proper effect, serves as flavour and targetting assistant and handles an achievement.
            PerformEffectPassiveAbility gnomePassive = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
            gnomePassive.name = "Gnome_PA";
            gnomePassive._passiveName = "Gnome";
            gnomePassive.m_PassiveID = "Gnome";
            gnomePassive.passiveIcon = ResourceLoader.LoadSprite("IconGnome");
            gnomePassive._characterDescription = "This party member is a gnome.";
            gnomePassive._enemyDescription = "This enemy is one or more gnomes.";
            gnomePassive._triggerOn = [TriggerCalls.OnDeath];
            gnomePassive.conditions = [IsCharacter];
            gnomePassive.doesPassiveTriggerInformationPanel = false;
            gnomePassive.effects =
            [
                Effects.GenerateEffect(GnomeCounter, 1),
                Effects.GenerateEffect(GnomeCheck, 5),
                Effects.GenerateEffect(GnomeHatAchievement, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 1)),
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
                        Effects.GenerateEffect(randomize, 1, Targeting.Slot_SelfSlot, ScriptableObject.CreateInstance<FuckingAliveEffectCondition>())
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
            jumpy._triggerOn = [TriggerCalls.OnDirectDamaged, TriggerCalls.OnAbilityUsed];

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
                        Effects.GenerateEffect(hyperrandomize, 1, Targeting.Slot_SelfSlot, ScriptableObject.CreateInstance<FuckingAliveEffectCondition>())
            ];
            hypercolors._triggerOn =
            [
                        TriggerCalls.OnDamaged,
                        TriggerCalls.OnAbilityUsed,
                        TriggerCalls.OnCombatStart,
            ];

            // Gouged - Missing an eye! Reduces damage dealt by 25%. also fucks up your name
            GougedPassiveAbility gougedPassive = ScriptableObject.CreateInstance<GougedPassiveAbility>();
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
            dryPassive._characterDescription = "This party member is immune to damage from Ruptured. Ruptured does not decrease when this party member moves.";
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

            PerformEffectPassiveAbility greyBlooded = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
            greyBlooded.name = "GreyBlooded_1_PA";
            greyBlooded._passiveName = "Grey-Blooded (1)";
            greyBlooded.m_PassiveID = "PigmentBlooded";
            greyBlooded.passiveIcon = ResourceLoader.LoadSprite("IconStonebloodGrey");
            greyBlooded._characterDescription = "Upon receiving direct damage this party member produces 1 additional Grey pigment.";
            greyBlooded._enemyDescription = "Upon receiving direct damage this enemy produces 1 additional Grey pigment.";
            greyBlooded._triggerOn = [TriggerCalls.OnDirectDamaged];
            greyBlooded.doesPassiveTriggerInformationPanel = true;
            greyBlooded.effects =
            [
                Effects.GenerateEffect(GiveGreyPigment, 1, Targeting.Slot_SelfSlot),
            ]; 
            
            GenerateColorsByListManaEffect GiveRandomPigment = ScriptableObject.CreateInstance<GenerateColorsByListManaEffect>();
            GiveRandomPigment._manaColors = [Pigments.Red, Pigments.Red, Pigments.Blue, Pigments.Blue, Pigments.Yellow, Pigments.Yellow, Pigments.Purple];

            PerformEffectPassiveAbility randomBlooded2 = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
            randomBlooded2.name = "Random4Blooded_2_PA";
            randomBlooded2._passiveName = "Random-Blooded (2)";
            randomBlooded2.m_PassiveID = "PigmentBlooded";
            randomBlooded2.passiveIcon = ResourceLoader.LoadSprite("IconStonebloodPrimary");
            randomBlooded2._characterDescription = "Upon receiving direct damage this party member produces 2 additional pigment of the four basic colors.";
            randomBlooded2._enemyDescription = "Upon receiving direct damage this enemy produces 2 additional pigment of the four basic colors.";
            randomBlooded2._triggerOn = [TriggerCalls.OnDirectDamaged];
            randomBlooded2.doesPassiveTriggerInformationPanel = true;
            randomBlooded2.effects =
            [
                Effects.GenerateEffect(GiveRandomPigment, 2, Targeting.Slot_SelfSlot),
            ];

            if (LoadedDBsHandler.PigmentDB.GetPigment("Broken") != null)
            {
                GenerateColorManaEffect GiveBrokenPigment = ScriptableObject.CreateInstance<GenerateColorManaEffect>();
                GiveBrokenPigment.mana = LoadedDBsHandler.PigmentDB.GetPigment("Broken");

                PerformEffectPassiveAbility brokenBlooded = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
                brokenBlooded.name = "BrokenBlooded_1_PA";
                brokenBlooded._passiveName = "Broken-Blooded (1)";
                brokenBlooded.m_PassiveID = "PigmentBlooded";
                brokenBlooded.passiveIcon = ResourceLoader.LoadSprite("IconStonebloodBroken");
                brokenBlooded._characterDescription = "Upon receiving direct damage this party member produces 1 additional Broken pigment, a mostly inert pigment that shatters on overflow.";
                brokenBlooded._enemyDescription = "Upon receiving direct damage this enemy produces 1 additional Broken pigment, a mostly inert pigment that shatters on overflow.";
                brokenBlooded._triggerOn = [TriggerCalls.OnDirectDamaged];
                brokenBlooded.doesPassiveTriggerInformationPanel = true;
                brokenBlooded.effects =
                [
                    Effects.GenerateEffect(GiveBrokenPigment, 1, Targeting.Slot_SelfSlot),
                ];

                CheckerPassivePoolAdd("BrokenBlooded_1_PA", "Broken-Blooded (1)", brokenBlooded);
            }

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

            GenerateColorsByListManaFillPigmentBarEffect AllBaseColorsLessPurple = ScriptableObject.CreateInstance<GenerateColorsByListManaFillPigmentBarEffect>();
            AllBaseColorsLessPurple._manaColors = [Pigments.Red, Pigments.Red, Pigments.Blue, Pigments.Blue, Pigments.Yellow, Pigments.Yellow, Pigments.Purple];

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

            PerformEffectPassiveAbility condenseRandomPrimaryLessPurple = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
            condenseRandomPrimaryLessPurple.name = "AA_CondensePrimaryLessPurple_PA";
            condenseRandomPrimaryLessPurple._passiveName = "Condense (Random)";
            condenseRandomPrimaryLessPurple.m_PassiveID = "Condense";
            condenseRandomPrimaryLessPurple.passiveIcon = ResourceLoader.LoadSprite("Condense_passive_primary");
            condenseRandomPrimaryLessPurple._characterDescription = "Upon death, fill the pigment bar with random pigment of the four basic colors.";
            condenseRandomPrimaryLessPurple._enemyDescription = condenseRandomPrimaryLessPurple._characterDescription;
            condenseRandomPrimaryLessPurple._triggerOn = [TriggerCalls.OnDeath];
            condenseRandomPrimaryLessPurple.doesPassiveTriggerInformationPanel = true;
            condenseRandomPrimaryLessPurple.effects =
            [
                Effects.GenerateEffect(AllBaseColorsLessPurple, 1, Targeting.Slot_SelfSlot),
            ];

            if (LoadedDBsHandler.PigmentDB.GetPigment("Broken") != null)
            {
                GenerateColorManaFillPigmentBarEffect AllBroken = ScriptableObject.CreateInstance<GenerateColorManaFillPigmentBarEffect>();
                AllBroken.mana = LoadedDBsHandler.PigmentDB.GetPigment("Broken");

                PerformEffectPassiveAbility condenseBroken = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
                condenseBroken.name = "AA_CondenseBroken_PA";
                condenseBroken._passiveName = "Condense (Broken)";
                condenseBroken.m_PassiveID = "Condense";
                condenseBroken.passiveIcon = ResourceLoader.LoadSprite("Condense_passive_broken");
                condenseBroken._characterDescription = "Upon death, fill the pigment bar with Broken pigment, a mostly inert pigment that shatters on overflow.";
                condenseBroken._enemyDescription = condenseBroken._characterDescription;
                condenseBroken._triggerOn = [TriggerCalls.OnDeath];
                condenseBroken.doesPassiveTriggerInformationPanel = true;
                condenseBroken.effects =
                [
                    Effects.GenerateEffect(AllBroken, 1, Targeting.Slot_SelfSlot),
                ];

                Passives.AddCustomPassiveToPool("AA_CondenseBroken_PA", "Condense (Broken)", condenseBroken);
            };

            if (LoadedDBsHandler.PigmentDB.GetPigment("Iridescent") != null)
            {
                GenerateColorManaFillPigmentBarEffect AllIridescent = ScriptableObject.CreateInstance<GenerateColorManaFillPigmentBarEffect>();
                AllIridescent.mana = LoadedDBsHandler.PigmentDB.GetPigment("Iridescent");

                PerformEffectPassiveAbility condenseIridescent = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
                condenseIridescent.name = "AA_CondenseIridescent_PA";
                condenseIridescent._passiveName = "Condense (Iridescent)";
                condenseIridescent.m_PassiveID = "Condense";
                condenseIridescent.passiveIcon = ResourceLoader.LoadSprite("Condense_passive_iridescent");
                condenseIridescent._characterDescription = "Upon death, fill the pigment bar with Iridescent pigment, which transforms into random pigment at the start of your next turn.";
                condenseIridescent._enemyDescription = condenseIridescent._characterDescription;
                condenseIridescent._triggerOn = [TriggerCalls.OnDeath];
                condenseIridescent.doesPassiveTriggerInformationPanel = true;
                condenseIridescent.effects =
                [
                    Effects.GenerateEffect(AllIridescent, 1, Targeting.Slot_SelfSlot),
                ];

                Passives.AddCustomPassiveToPool("AA_CondenseIridescent_PA", "Condense (Iridescent)", condenseIridescent);
            }

            if (LoadedDBsHandler.PigmentDB.GetPigment("Clusterfuck") != null)
            {
                GenerateColorsByListManaFillPigmentBarEffect AllClusterfuck = ScriptableObject.CreateInstance<GenerateColorsByListManaFillPigmentBarEffect>();
                AllClusterfuck._manaColors = [LoadedDBsHandler.PigmentDB.GetPigment("Clusterfuck")];

                PerformEffectPassiveAbility condenseClusterfuck = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
                condenseClusterfuck.name = "AA_CondenseClusterfuck_PA";
                condenseClusterfuck._passiveName = "Condense (Clusterfuck)";
                condenseClusterfuck.m_PassiveID = "Condense";
                condenseClusterfuck.passiveIcon = ResourceLoader.LoadSprite("Condense_passive_clusterfuck");
                condenseClusterfuck._characterDescription = "Upon death, fill the pigment bar with entirely random pigment, including split and esoteric pigment colors.";
                condenseClusterfuck._enemyDescription = condenseClusterfuck._characterDescription;
                condenseClusterfuck._triggerOn = [TriggerCalls.OnDeath];
                condenseClusterfuck.doesPassiveTriggerInformationPanel = true;
                condenseClusterfuck.effects =
                [
                    Effects.GenerateEffect(AllClusterfuck, 1, Targeting.Slot_SelfSlot),
                ];

                Passives.AddCustomPassiveToPool("AA_CondenseClusterfuck_PA", "Condense (Clusterfuck)", condenseClusterfuck);
            }

            if (LoadedDBsHandler.PigmentDB.GetPigment("EntropicBase") != null)
            {
                GenerateColorManaFillPigmentBarEffect AllEntropic = ScriptableObject.CreateInstance<GenerateColorManaFillPigmentBarEffect>();
                AllEntropic.mana = LoadedDBsHandler.PigmentDB.GetPigment("EntropicBase");

                PerformEffectPassiveAbility condenseEntropic = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
                condenseEntropic.name = "AA_CondenseEntropic_PA";
                condenseEntropic._passiveName = "Condense (Entropic)";
                condenseEntropic.m_PassiveID = "Condense";
                condenseEntropic.passiveIcon = ResourceLoader.LoadSprite("Condense_passive_entropic");
                condenseEntropic._characterDescription = "Upon death, fill the pigment bar with Entropic pigment, which transforms into random pigment upon anything performing an ability.";
                condenseEntropic._enemyDescription = condenseEntropic._characterDescription;
                condenseEntropic._triggerOn = [TriggerCalls.OnDeath];
                condenseEntropic.doesPassiveTriggerInformationPanel = true;
                condenseEntropic.effects =
                [
                    Effects.GenerateEffect(AllEntropic, 1, Targeting.Slot_SelfSlot),
                ];

                Passives.AddCustomPassiveToPool("AA_CondenseEntropic_PA", "Condense (Entropic)", condenseEntropic);
            }

            if (LoadedDBsHandler.PigmentDB.GetPigment("White") != null)
            {
                GenerateColorManaFillPigmentBarEffect AllWhite = ScriptableObject.CreateInstance<GenerateColorManaFillPigmentBarEffect>();
                AllWhite.mana = LoadedDBsHandler.PigmentDB.GetPigment("White");

                PerformEffectPassiveAbility condenseWhite = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
                condenseWhite.name = "AA_CondenseEntropic_PA";
                condenseWhite._passiveName = "Condense (Entropic)";
                condenseWhite.m_PassiveID = "Condense";
                condenseWhite.passiveIcon = ResourceLoader.LoadSprite("Condense_passive_white");
                condenseWhite._characterDescription = "Upon death, fill the pigment bar with universal White pigment.";
                condenseWhite._enemyDescription = condenseWhite._characterDescription;
                condenseWhite._triggerOn = [TriggerCalls.OnDeath];
                condenseWhite.doesPassiveTriggerInformationPanel = true;
                condenseWhite.effects =
                [
                    Effects.GenerateEffect(AllWhite, 1, Targeting.Slot_SelfSlot),
                ];

                Passives.AddCustomPassiveToPool("AA_CondenseWhite_PA", "Condense (White)", condenseWhite);
            }

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

            FieldEffect_Apply_Effect HoarfrostApply = ScriptableObject.CreateInstance<FieldEffect_Apply_Effect>();
            HoarfrostApply._Field = StatusField.GetCustomFieldEffect("Hoarfrost_ID");

            // Snowstorm - Hoarfrost's inferno equivalent
            PerformEffectPassiveAbility snowstorm1 = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
            snowstorm1.name = "Snowstorm_1_PA";
            snowstorm1._passiveName = "Snowstorm (1)";
            snowstorm1.m_PassiveID = "Snowstorm";
            snowstorm1.passiveIcon = ResourceLoader.LoadSprite("IconHoarfrostMirrored");
            snowstorm1._characterDescription = "On turn start this party member applies 1 Hoarfrost to its current position.";
            snowstorm1._enemyDescription = "On turn start this enemy applies 1 Hoarfrost to its current position.";
            snowstorm1._triggerOn = [TriggerCalls.OnTurnStart];
            snowstorm1.doesPassiveTriggerInformationPanel = true;
            snowstorm1.effects =
            [
                Effects.GenerateEffect(HoarfrostApply, 1, Targeting.Slot_SelfSlot),
            ];

            // Antifreeze - Hoarfrost equivalent of Made Of Fire
            DamageTypeImmunityPassiveAbility iceproofPassive = ScriptableObject.CreateInstance<DamageTypeImmunityPassiveAbility>();
            iceproofPassive.name = "Antifreeze_PA";
            iceproofPassive._passiveName = "Antifreeze";
            iceproofPassive.m_PassiveID = "Antifreeze";
            iceproofPassive.passiveIcon = ResourceLoader.LoadSprite("IconIceskull");
            iceproofPassive._characterDescription = "This party member is unaffected by Hoarfrost and immune to frost damage.";
            iceproofPassive._enemyDescription = "This enemy is unaffected by Hoarfrost and immune to frost damage.";
            iceproofPassive.doesPassiveTriggerInformationPanel = false;
            iceproofPassive._triggerOn = [TriggerCalls.OnBeingDamaged];
            iceproofPassive._damageType = "AA_Frost_Damage";
            // again, field effect immunity is handled by the field effect

            if (LoadedDBsHandler.PigmentDB.GetPigment("Broken") != null)
            {
                // Fragile - broken pigment-related passive from Undivine Comedy (thanks WolfaCola)
                PerformDoubleEffectPassiveAbility Fragile = ScriptableObject.CreateInstance<PerformDoubleEffectPassiveAbility>();
                Fragile.name = "AA_Fragile_PA";
                Fragile._passiveName = "Fragile";
                Fragile.m_PassiveID = "Fragile";
                Fragile.passiveIcon = ResourceLoader.LoadSprite("Fragile");
                Fragile._characterDescription = "This party member's health will be Broken upon taking direct damage.\nThis party member also shatters all Broken pigment upon death.\nBroken Pigment naturally shatters upon overflow.";
                Fragile._enemyDescription = "This enemy's health will be Broken upon taking direct damage.\nThis enemy also shatters all Broken pigment upon death.\nBroken Pigment naturally shatters upon overflow.";

                ChangeToRandomHealthColorEffect setBroken = ScriptableObject.CreateInstance<ChangeToRandomHealthColorEffect>();
                setBroken._healthColors = [LoadedDBsHandler.PigmentDB.GetPigment("Broken")];

                Fragile._triggerOn = [TriggerCalls.OnDirectDamaged];
                Fragile._secondTriggerOn = [TriggerCalls.OnDeath];
                Fragile.effects =
                [
                    Effects.GenerateEffect(setBroken, 1, Targeting.Slot_SelfSlot),
                ];
                Fragile._secondEffects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<ShatterBrokenPigmentEffect>()),
                ];
                if (!LoadedDBsHandler.PassiveDB._PassivesPool.Contains("Fragile_PA")) { Passives.AddCustomPassiveToPool("Fragile_PA", "Fragile", Fragile); }
            }

            // Gadsby - joke passive
            GadsbyPassiveAbility gadsbyPassiv = ScriptableObject.CreateInstance<GadsbyPassiveAbility>();
            gadsbyPassiv.name = "Gadsby_PA";
            gadsbyPassiv._passiveName = "Gadsby";
            gadsbyPassiv.m_PassiveID = "Gadsby";
            gadsbyPassiv.passiveIcon = ResourceLoader.LoadSprite("IconNoE");
            gadsbyPassiv._characterDescription = "This party m_mb_r do_s not lik_ th_ l_tt_r _.";
            gadsbyPassiv._enemyDescription = "This _n_my do_s not lik_ th_ l_tt_r _.";
            gadsbyPassiv._triggerOn = [TriggerCalls.OnBeforeCombatStart];
            gadsbyPassiv.doesPassiveTriggerInformationPanel = false;
            gadsbyPassiv.effects =
            [
                //Effects.GenerateEffect(ScriptableObject.CreateInstance<CasterGadsbyEffect>(), 1, Targeting.Slot_SelfSlot),
            ];

            // Jolly - Joke(r) Passive
            JollyPassiveAbility jollyPassive = ScriptableObject.CreateInstance<JollyPassiveAbility>();
            jollyPassive.name = "JollyJoker_PA";
            jollyPassive._passiveName = "Jolly";
            jollyPassive.m_PassiveID = "Jolly";
            jollyPassive.passiveIcon = ResourceLoader.LoadSprite("IconJolly");
            jollyPassive._characterDescription = "This party member is feeling rather jolly.";
            jollyPassive._enemyDescription = "This enemy is feeling rather jolly.";
            jollyPassive.doesPassiveTriggerInformationPanel = false;

            // Tortured - convert indirect damage taken to direct damage (stew's specimens)
            ConvertIndirectToDirectPassiveAbility torturedPassive = ScriptableObject.CreateInstance<ConvertIndirectToDirectPassiveAbility>();
            torturedPassive.name = "Tortured_PA";
            torturedPassive._passiveName = "Tortured";
            torturedPassive.m_PassiveID = "Tortured";
            torturedPassive.passiveIcon = ResourceLoader.LoadSprite("IconTortured");
            torturedPassive._characterDescription = "Indirect damage taken is instead direct.";
            torturedPassive._enemyDescription = torturedPassive._characterDescription;
            torturedPassive.doesPassiveTriggerInformationPanel = false;
            torturedPassive._triggerOn = [TriggerCalls.OnBeingDamaged];

            if (LoadedDBsHandler.StatusFieldDB._FieldEffects.ContainsKey("Gravity_ID"))
            {
                // Fixed Point - Gravity Immunity passive (once I figure out how to do that lmao)
                PerformEffectPassiveAbility nograv = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
                nograv.name = "AA_FixedPoint_PA";
                nograv._passiveName = "Fixed Point";
                nograv.m_PassiveID = "FixedPoint";
                nograv.passiveIcon = ResourceLoader.LoadSprite("IconFixedPoint");
                nograv._characterDescription = "This party member is unaffected by Gravity.";
                nograv._enemyDescription = "This enemy is unaffected by Gravity.";
                nograv._triggerOn = [];
                nograv.doesPassiveTriggerInformationPanel = false;
                nograv.effects =
                [

                ];
                Passives.AddCustomPassiveToPool("AA_FixedPoint_PA", "Fixed Point", nograv);
            }

            if (!LoadedDBsHandler.PassiveDB._PassivesPool.Contains("Thrombophilia_PA"))
            {
                RemoveAmountStatusEffectEffect RupturedDown = ScriptableObject.CreateInstance<RemoveAmountStatusEffectEffect>();
                RupturedDown.statusId = StatusField.Ruptured.StatusID;
                RupturedDown.entryAsPercentage = false;

                PerformEffectPassiveAbility thrombo = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
                thrombo.name = "AA_FixedPoint_PA";
                thrombo._passiveName = "Fixed Point";
                thrombo.m_PassiveID = "FixedPoint";
                thrombo.passiveIcon = ResourceLoader.LoadSprite("IconThrombophilia");
                thrombo._characterDescription = "This party member will lose 1 extra Ruptured upon moving.";
                thrombo._enemyDescription = "This enemy will lose 1 Ruptured upon moving.";
                thrombo._triggerOn = [TriggerCalls.OnMoved];
                thrombo.doesPassiveTriggerInformationPanel = false;
                thrombo.effects =
                [
                    Effects.GenerateEffect(RupturedDown, 1, Targeting.Slot_SelfSlot),
                ];
                Passives.AddCustomPassiveToPool("Thrombophilia_PA", "Thrombophilia", thrombo);
            }

            RerollNumberPigmentFromSetColorEffect PurpleDilute = ScriptableObject.CreateInstance<RerollNumberPigmentFromSetColorEffect>();
            PurpleDilute._targetMana = Pigments.Purple;
            PurpleDilute._resultMana = [Pigments.PurpleRed, Pigments.PurpleBlue, Pigments.PurpleYellow];

            PerformEffectPassiveAbility pigmentTransformPurple1 = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
            pigmentTransformPurple1.name = "AA_Dilution_Purple1_PA";
            pigmentTransformPurple1._passiveName = "Dilution (Purple, 1)";
            pigmentTransformPurple1.m_PassiveID = "Dilution";
            pigmentTransformPurple1.passiveIcon = ResourceLoader.LoadSprite("IconPigmentTransformPurple");
            pigmentTransformPurple1._characterDescription = "On being directly damaged, split a random pigment color into 1 Purple pigment in the pigment bar.";
            pigmentTransformPurple1._enemyDescription = pigmentTransformPurple1._characterDescription;
            pigmentTransformPurple1._triggerOn = [TriggerCalls.OnDirectDamaged];
            pigmentTransformPurple1.doesPassiveTriggerInformationPanel = true;
            pigmentTransformPurple1.effects =
            [
                Effects.GenerateEffect(PurpleDilute, 1),
            ];

            // Zelator - Hexed damage bonus passive
            PercDmgIfStatusModPassiveAbility zelator = ScriptableObject.CreateInstance<PercDmgIfStatusModPassiveAbility>();
            zelator.name = "Zelator_PA";
            zelator._passiveName = "Zelator";
            zelator.m_PassiveID = "Zelator";
            zelator.passiveIcon = ResourceLoader.LoadSprite("IconCometGaze");
            zelator._characterDescription = "This party member deals 50% more damage to Hexed targets.";
            zelator._enemyDescription = "This enemy deals 50% more damage to Hexed targets.";
            zelator.doesPassiveTriggerInformationPanel = true;
            zelator._triggerOn = [TriggerCalls.OnWillApplyDamage];
            zelator._doesIncrease = true;
            zelator._useDealt = true;
            zelator._useSimpleInt = false;
            zelator._percentageToModify = 50;
            zelator._statusID = "Hexed_ID";

            // Contaminant - Aggregate color manipulation passive
            PerformEffectPassiveAbility contaminant1 = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
            contaminant1.name = "AA_Contaminant1_PA";
            contaminant1._passiveName = "Contaminant (1)";
            contaminant1.m_PassiveID = "Contaminant";
            contaminant1.passiveIcon = ResourceLoader.LoadSprite("IconContaminant");
            contaminant1._characterDescription = "On being directly damaged, split this party member's health color into 1 random pigment in the pigment bar.";
            contaminant1._enemyDescription = "On being directly damaged, split this enemy's health color into 1 random pigment in the pigment bar.";
            contaminant1._triggerOn = [TriggerCalls.OnDirectDamaged];
            contaminant1.doesPassiveTriggerInformationPanel = true;
            contaminant1.effects =
            [
                Effects.GenerateEffect(ScriptableObject.CreateInstance<PutrefyNumberPigmentCasterHealthColorEffect>(), 1),
            ];

            // Adding to pool
            CheckerPassivePoolAdd("Shy_PA", "Shy", shy);
            CheckerPassivePoolAdd("Confrontational_PA", "Confrontational", confrontational);
            CheckerPassivePoolAdd("Gnome_PA", "Gnome", gnomePassive);
            CheckerPassivePoolAdd("AA_FreeWilled_PA", "Free-Willed", freeWillPassive);
            CheckerPassivePoolAdd("AA_Heterochromia_PA", "Heterochromia", colors);
            CheckerPassivePoolAdd("Omnichromia_PA", "Omnichromia", hypercolors);
            CheckerPassivePoolAdd("AA_TornApart_PA", "Torn Apart", tornApart);
            CheckerPassivePoolAdd("AA_Jumpy_PA", "Jumpy", jumpy);
            CheckerPassivePoolAdd("Gouged_PA", "Gouged", gougedPassive);
            CheckerPassivePoolAdd("MadeOfFire_PA", "Made Of Fire", fireproofPassive);
            CheckerPassivePoolAdd("DriedOut_PA", "Dried Out", dryPassive);
            CheckerPassivePoolAdd("AA_Condense_PA", "Condense", condensePassive);
            CheckerPassivePoolAdd("AA_CondenseRed_PA", "Condense (Red)", condenseRed);
            CheckerPassivePoolAdd("AA_CondenseBlue_PA", "Condense (Blue)", condenseBlue);
            CheckerPassivePoolAdd("AA_CondenseYellow_PA", "Condense (Yellow)", condenseYellow);
            CheckerPassivePoolAdd("AA_CondensePurple_PA", "Condense (Purple)", condensePurple);
            CheckerPassivePoolAdd("AA_CondensePrimaryLessPurple_PA", "Condense (Random)", condenseRandomPrimaryLessPurple);
            CheckerPassivePoolAdd("RedBlooded_1_PA", "Red-Blooded (1)", redBlooded);
            CheckerPassivePoolAdd("BlueBlooded_1_PA", "Blue-Blooded (1)", blueBlooded);
            CheckerPassivePoolAdd("YellowBlooded_1_PA", "Yellow-Blooded (1)", yellowBlooded);
            CheckerPassivePoolAdd("PurpleBlooded_1_PA", "Purple-Blooded (1)", purpleBlooded);
            CheckerPassivePoolAdd("GreyBlooded_1_PA", "Grey-Blooded (1)", greyBlooded);
            CheckerPassivePoolAdd("BlackTears_2_PA", "Black Tears (2)", blackTears);
            CheckerPassivePoolAdd("Snowstorm_1_PA", "Snowstorm (1)", snowstorm1);
            CheckerPassivePoolAdd("Antifreeze_PA", "Antifreeze", iceproofPassive);
            CheckerPassivePoolAdd("Gadsby_PA", "Gadsby", gadsbyPassiv);
            CheckerPassivePoolAdd("JollyJoker_PA", "Jolly", jollyPassive);
            CheckerPassivePoolAdd("Tortured_PA", "Tortured", torturedPassive);
            CheckerPassivePoolAdd("AA_Dilution_Purple1_PA", "Dilution (Purple, 1)", pigmentTransformPurple1);
            CheckerPassivePoolAdd("Zelator_PA", "Zelator", zelator);
            CheckerPassivePoolAdd("AA_Contaminant1_PA", "Contaminant (1)", contaminant1);
            CheckerPassivePoolAdd("Random4Blooded_2_PA", "Random-Blooded (2)", randomBlooded2);

            // Glossary entries
            GlossaryPassives AAShyInfo = new GlossaryPassives("Shy", "Upon performing an ability, this party member/enemy will move to the left or right, prioritizing unopposed spaces, if there is an enemy/party member opposing them.", ResourceLoader.LoadSprite("IconShy"));
            GlossaryPassives AAConfrontationalInfo = new GlossaryPassives("Confrontational", "Upon performing an ability, this party member/enemy will move to the left or right, prioritizing opposed spaces, unless there is an enemy/party member opposing them.", ResourceLoader.LoadSprite("IconConfrontational"));
            GlossaryPassives AACopyThatInfo = new GlossaryPassives("Copy That", "At the start of combat, select a set amount of random party members. Copy one passive and two abilities (excluding Slap) from each selected party member onto this enemy. Change this enemy's health color to a combination of all selected party members' health colors.", ResourceLoader.LoadSprite("IconCopyThat"));
            GlossaryPassives AAGnomeInfo = new GlossaryPassives("Gnome", "This unit is one or more gnomes.", ResourceLoader.LoadSprite("IconGnome"));
            GlossaryPassives AAOmnichromiaInfo = new GlossaryPassives("Omnichromia", "Upon receiving any kind of damage or performing an ability, randomize this unit's health colour. This includes unusual and split pigment.", ResourceLoader.LoadSprite("IconOmnichromia"));
            GlossaryPassives AAGougedInfo = new GlossaryPassives("Gouged", "This unit is missing an eye, their reduced accuracy decreasing damage dealt by 25%.", ResourceLoader.LoadSprite("IconGouged"));
            GlossaryPassives AAMadeOfFireInfo = new GlossaryPassives("Made Of Fire", "This unit is unaffected by Fire and immune to fire damage.", ResourceLoader.LoadSprite("IconFireskull"));
            GlossaryPassives AADriedOutInfo = new GlossaryPassives("Dried Out", "This unit is immune to damage from Ruptured.", ResourceLoader.LoadSprite("IconDriedOut"));
            GlossaryPassives AAMercurialInfo = new GlossaryPassives("Mercurial", "At the end of the round, if a certain condition is met, this enemy transforms into a different enemy.", ResourceLoader.LoadSprite("IconTransformPassive"));
            GlossaryPassives AAPigmentBloodedInfo = new GlossaryPassives("Pigment-Blooded", "Upon receiving direct damage this party member/enemy produces additional pigment of a specific color.", ResourceLoader.LoadSprite("IconStonebloodPrimary"));
            GlossaryPassives AATargeterInfo = new GlossaryPassives("Targeter", "At the start of combat and at the end of the round, this enemy will remember the position of the party member with the highest current health.", ResourceLoader.LoadSprite("IconTargeter"));
            GlossaryPassives AADeploymentInfo = new GlossaryPassives("Deployment", "Dealing damage to this enemy greater than a certain threshold will cause it to attempt to summon a specific enemy.", ResourceLoader.LoadSprite("IconDeployment")); // icon sprited by MillieAmp
            GlossaryPassives AACondenseInfo = new GlossaryPassives("Condense", "Upon death, fill the pigment bar with pigment of this party member/enemy's health color. Specific versions of Condense can create specific pigment colors.", ResourceLoader.LoadSprite("Condense_passive"));
            GlossaryPassives AABlackTearsInfo = new GlossaryPassives("Black Tears", "On moving, this unit gains a certain amount of Oil Slicked.", ResourceLoader.LoadSprite("IconBlackTears"));
            GlossaryPassives AASnowstormInfo = new GlossaryPassives("Snowstorm", "On turn start, this party member/enemy applies a certain amount of Hoarfrost to their current position.", ResourceLoader.LoadSprite("IconHoarfrostMirrored"));
            GlossaryPassives AAAntifreezeInfo = new GlossaryPassives("Antifreeze", "This unit is unaffected by Hoarfrost and immune to frost damage.", ResourceLoader.LoadSprite("IconIceskull"));
            GlossaryPassives AAPatientInfo = new GlossaryPassives("Patient", "When the player turn ends, this enemy queues a specific ability.", ResourceLoader.LoadSprite("IconPatient"));
            GlossaryPassives AADilutionInfo = new GlossaryPassives("Dilution", "On being directly damaged, split random pigment colors into a set amount of pigments of a specific color in the pigment bar.", ResourceLoader.LoadSprite("IconPigmentTransformPrimary"));
            GlossaryPassives AAContaminantInfo = new GlossaryPassives("Contaminant", "On being directly damaged, split this party member/enemy's health color into a set amount of random pigment in the pigment bar.", ResourceLoader.LoadSprite("IconContaminant"));
            GlossaryPassives AAZelatorInfo = new GlossaryPassives("Zelator", "This party member/enemy deals 50% more damage to Hexed targets.", ResourceLoader.LoadSprite("IconCometGaze"));

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
            LoadedDBsHandler.GlossaryDB.AddNewPassive(AASnowstormInfo);
            LoadedDBsHandler.GlossaryDB.AddNewPassive(AAAntifreezeInfo);
            LoadedDBsHandler.GlossaryDB.AddNewPassive(AAPatientInfo);
            LoadedDBsHandler.GlossaryDB.AddNewPassive(AADilutionInfo);
            LoadedDBsHandler.GlossaryDB.AddNewPassive(AAContaminantInfo);
            LoadedDBsHandler.GlossaryDB.AddNewPassive(AAZelatorInfo);

            if (!AApocrypha.CrossMod.StewSpecimens)
            {
                GlossaryPassives AAFreeWillInfo = new GlossaryPassives("Free-Willed", "This party member acts of their own free will, but can still be manually moved.", ResourceLoader.LoadSprite("IconStewSpecimensFreeWill"));
                LoadedDBsHandler.GlossaryDB.AddNewPassive(AAFreeWillInfo);
                //GlossaryPassives AATorturedInfo = new GlossaryPassives("Tortured", "All Indirect Damage this unit takes becomes Direct.", ResourceLoader.LoadSprite("IconTortured"));
                //LoadedDBsHandler.GlossaryDB.AddNewPassive(AATorturedInfo);
            }

            if (!AApocrypha.CrossMod.SaltEnemies)
            {
                GlossaryPassives AAHeterochromiaInfo = new GlossaryPassives("Heterochromia", "Upon receiving any kind of damage, randomize this unit's health colour.", ResourceLoader.LoadSprite("IconHemochromia"));
                LoadedDBsHandler.GlossaryDB.AddNewPassive(AAHeterochromiaInfo);
                GlossaryPassives AAJumpyInfo = new GlossaryPassives("Jumpy", "Upon being damaged, move to a random position. Upon performing an ability, move to a random position.", ResourceLoader.LoadSprite("IconJumpy"));
                LoadedDBsHandler.GlossaryDB.AddNewPassive(AAJumpyInfo);
                GlossaryPassives AATornApartInfo = new GlossaryPassives("Torn Apart", "This unit is permanently Gutted.", StatusField.Gutted._EffectInfo.icon);
                LoadedDBsHandler.GlossaryDB.AddNewPassive(AATornApartInfo);
                GlossaryPassives AAOverdoseInfo = new GlossaryPassives("Overdose", "On being directly damaged, transform into a random other party member/enemy from a selection of party members/enemies.", ResourceLoader.LoadSprite("OverdosePassive"));
                LoadedDBsHandler.GlossaryDB.AddNewPassive(AAOverdoseInfo);
            }

            GlossaryPassives BonusSuiteInfo = new GlossaryPassives("Bonus Suite", "This enemy will perform one of a list of moves as a bonus action each turn.", ResourceLoader.LoadSprite("IconBonusSuite"));
            LoadedDBsHandler.GlossaryDB.AddNewPassive(BonusSuiteInfo);

            if (LoadedDBsHandler.StatusFieldDB._FieldEffects.ContainsKey("Gravity_ID"))
            {
                GlossaryPassives NoGravInfo = new GlossaryPassives("Fixed Point", "This party member/enemy is unaffected by Gravity.", ResourceLoader.LoadSprite("IconFixedPoint"));
                LoadedDBsHandler.GlossaryDB.AddNewPassive(NoGravInfo);
            }
        }

        public static void CheckerPassivePoolAdd(string id, string name, BasePassiveAbilitySO passive)
        {
            if (!LoadedDBsHandler.PassiveDB._PassivesPool.Contains(id))
            {
                Debug.Log($"Passives | adding passive {name} ({id})");
                Passives.AddCustomPassiveToPool(id, name, passive);
            }
            else
            {
                Debug.Log($"Passives | passive with id {id} already registered! skipping...");
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

            PassiveLockingEffect PureLock = ScriptableObject.CreateInstance<PassiveLockingEffect>();
            PureLock._lock = true;
            PureLock.m_PassiveIDs = [Passives.Pure.m_PassiveID];

            PassiveLockingEffect PureUnlock = ScriptableObject.CreateInstance<PassiveLockingEffect>();
            PureUnlock._lock = false;
            PureUnlock.m_PassiveIDs = [Passives.Pure.m_PassiveID];

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
                    Effects.GenerateEffect(PureLock),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<CopyThatEffect>(), x, Targeting.Unit_AllOpponents),
                    Effects.GenerateEffect(PureUnlock),
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

        // Bonus Suite: Multiple Bonus Attack Passive to replace Alt Attacks
        public static BasePassiveAbilitySO BonusSuiteGenerator(List<ExtraAbilityInfo> bonusAbilities)
        {
            List<string> names = [];
            List<ExtraAbilityInfo> weights = [];
            foreach (ExtraAbilityInfo ability in bonusAbilities)
            {
                if (ability == null || ability.ability == null)
                {
                    Debug.Log("Bonus ability " + ability.ability.name + " does not exist.");
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

            BonusSuitePassiveAbility bonusSuitePassiveAbility = ScriptableObject.CreateInstance<BonusSuitePassiveAbility>();

            bonusSuitePassiveAbility.name = "BonusSuite_" + string.Join("_", names) + "_PA";
            bonusSuitePassiveAbility.m_PassiveID = "BonusSuite_" + string.Join("_", names);
            bonusSuitePassiveAbility._passiveName = "Bonus Suite";
            bonusSuitePassiveAbility._characterDescription = "This passive is not meant for party members.";
            bonusSuitePassiveAbility._enemyDescription = "This enemy will perform one of the following moves as a bonus action each turn:" + "\n" + string.Join("\n", names);
            bonusSuitePassiveAbility.passiveIcon = ResourceLoader.LoadSprite("IconBonusSuite");
            bonusSuitePassiveAbility._triggerOn = [TriggerCalls.ExtraAdditionalAttacks];
            bonusSuitePassiveAbility.conditions = [];
            bonusSuitePassiveAbility.doesPassiveTriggerInformationPanel = false;
            bonusSuitePassiveAbility.specialStoredData = null;
            bonusSuitePassiveAbility._suiteAbilities = weights;
            return bonusSuitePassiveAbility;
        }

        private static TValue GetOrCreatePassive<TKey, TValue>(IDictionary<TKey, TValue> readFrom, TKey key, Func<TKey, TValue> create)
        {
            if (readFrom.TryGetValue(key, out TValue value))
            {
                return value;
            }

            return readFrom[key] = create(key);
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
