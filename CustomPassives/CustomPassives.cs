using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;
using UnityEngine;

namespace A_Apocrypha.Custom_Passives
{
    public class CustomPassives
    {
        public static void Add()
        {
            // Movers

            SwapToSidesEffect LeftOrRight = ScriptableObject.CreateInstance<SwapToSidesEffect>();

            PreviousEffectCondition PreviousTrue = ScriptableObject.CreateInstance<PreviousEffectCondition>();
            PreviousTrue.wasSuccessful = true;

            PreviousEffectCondition PreviousFalse = ScriptableObject.CreateInstance<PreviousEffectCondition>();
            PreviousFalse.wasSuccessful = false;

            // Shy - Skittish, but only if there is an opposing unit.
            PerformEffectPassiveAbility shy = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
            shy.name = "Shy_PA";
            shy._passiveName = "Shy";
            shy.m_PassiveID = "Shy";
            shy.passiveIcon = ResourceLoader.LoadSprite("IconShy");
            shy._characterDescription = "Upon performing an ability, this party member will move to the left or right if there is an enemy opposing them.";
            shy._enemyDescription = "Upon performing an ability, this enemy will move to the left or right if there is a party member opposing them.";
            shy._triggerOn = [TriggerCalls.OnAbilityUsed];
            shy.effects =
            [
                Effects.GenerateEffect(ScriptableObject.CreateInstance<CheckHasUnitEffect>(), 1, Targeting.Slot_Front),
                Effects.GenerateEffect(LeftOrRight, 1, Targeting.Slot_SelfSlot, PreviousTrue),
            ];

            // Confrontational - Skittish, but only if there is NOT an opposing unit.
            PerformEffectPassiveAbility confrontational = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
            confrontational.name = "Confrontational_PA";
            confrontational._passiveName = "Confrontational";
            confrontational.m_PassiveID = "Confrontational";
            confrontational.passiveIcon = ResourceLoader.LoadSprite("IconConfrontational");
            confrontational._characterDescription = "Upon performing an ability, this party member will move to the left or right unless there is an enemy opposing them.";
            confrontational._enemyDescription = "Upon performing an ability, this enemy will move to the left or right unless there is a party member opposing them.";
            confrontational._triggerOn = [TriggerCalls.OnAbilityUsed];
            confrontational.effects =
            [
                Effects.GenerateEffect(ScriptableObject.CreateInstance<CheckHasUnitEffect>(), 1, Targeting.Slot_Front),
                Effects.GenerateEffect(LeftOrRight, 1, Targeting.Slot_SelfSlot, PreviousFalse),
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

            // Glossary entries
            GlossaryPassives AAShyInfo = new GlossaryPassives("Shy", "Upon performing an ability, this party member/enemy will move to the left or right if there is an enemy/party member opposing them.", ResourceLoader.LoadSprite("IconShy"));
            GlossaryPassives AAConfrontationalInfo = new GlossaryPassives("Confrontational", "Upon performing an ability, this party member/enemy will move to the left or right unless there is an enemy/party member opposing them.", ResourceLoader.LoadSprite("IconConfrontational"));
            GlossaryPassives AACopyThatInfo = new GlossaryPassives("Copy That", "At the start of combat, select a set amount of random party members. Copy one passive and two abilities (excluding Slap) from each selected party member onto this enemy. Change this enemy's health color to a combination of all selected party members' health colors.", ResourceLoader.LoadSprite("IconCopyThat"));
            GlossaryPassives AAGnomeInfo = new GlossaryPassives("Gnome", "This unit is one or more gnomes.", ResourceLoader.LoadSprite("IconGnome"));
            GlossaryPassives AAOmnichromiaInfo = new GlossaryPassives("Omnichromia", "Upon receiving any kind of damage or performing an ability, randomize this unit's health colour. This includes unusual and split pigment.", ResourceLoader.LoadSprite("IconOmnichromia"));
            GlossaryPassives AAGougedInfo = new GlossaryPassives("Gouged", "This unit is missing an eye, their reduced accuracy decreasing damage dealt by 25%.", ResourceLoader.LoadSprite("IconGouged"));

            LoadedDBsHandler.GlossaryDB.AddNewPassive(AAShyInfo);
            LoadedDBsHandler.GlossaryDB.AddNewPassive(AAConfrontationalInfo);
            LoadedDBsHandler.GlossaryDB.AddNewPassive(AACopyThatInfo);
            LoadedDBsHandler.GlossaryDB.AddNewPassive(AAGnomeInfo);
            LoadedDBsHandler.GlossaryDB.AddNewPassive(AAOmnichromiaInfo);
            LoadedDBsHandler.GlossaryDB.AddNewPassive(AAGougedInfo);

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
                copythat._secondEffects = [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<CheckIsPlayerTurnEffect>(), 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(SimulacrumAchievement, 1, Targeting.Slot_SelfSlot, PreviousFalse)
                ];
                copythat.doesPassiveTriggerInformationPanel = true;
                copythat._secondDoesPerformPopUp = false;
                return copythat;
            });
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
    }
}
