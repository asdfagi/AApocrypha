using System;
using System.Collections.Generic;
using System.Text;
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
            shy.m_PassiveID = "Shy_PA";
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
            confrontational.m_PassiveID = "Confrontational_PA";
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
            gnomePassive.m_PassiveID = "Gnome_PA";
            gnomePassive.passiveIcon = ResourceLoader.LoadSprite("GnomesTimeline");
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
            freeWillPassive.m_PassiveID = "AA_FreeWilled_PA";
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

            // Heterochromia - Essentially Four-Faced.
            PerformEffectPassiveAbility colors = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
            colors.name = "AA_Heterochromia_PA";
            colors._passiveName = "Heterochromia";
            colors.m_PassiveID = "AA_Heterochromia_PA";
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

            // Adding to pool
            Passives.AddCustomPassiveToPool("Shy_PA", "Shy", shy);
            Passives.AddCustomPassiveToPool("Confrontational_PA", "Confrontational", confrontational);
            //Passives.AddCustomPassiveToPool("DecayAbandonedAltar_PA", "Decay", DecayAbandonedAltar);
            Passives.AddCustomPassiveToPool("Gnome_PA", "Gnome", gnomePassive);
            Passives.AddCustomPassiveToPool("AA_FreeWilled_PA", "Free-Willed", freeWillPassive);
            Passives.AddCustomPassiveToPool("AA_Heterochromia_PA", "Heterochromia", colors);

            // Glossary entries
            GlossaryPassives AAShyInfo = new GlossaryPassives("Shy", "Upon performing an ability, this party member/enemy will move to the left or right if there is an enemy/party member opposing them.", ResourceLoader.LoadSprite("IconShy"));
            GlossaryPassives AAConfrontationalInfo = new GlossaryPassives("Confrontational", "Upon performing an ability, this party member/enemy will move to the left or right unless there is an enemy/party member opposing them.", ResourceLoader.LoadSprite("IconConfrontational"));
            GlossaryPassives AACopyThatInfo = new GlossaryPassives("Copy That", "At the start of combat, select a set amount of random party members. Copy one passive and two abilities (excluding Slap) from each selected party member onto this enemy. Change this enemy's health color to a combination of all selected party members' health colors.", ResourceLoader.LoadSprite("IconCopyThat"));
            GlossaryPassives AAGnomeInfo = new GlossaryPassives("Gnome", "This unit is one or more gnomes.", ResourceLoader.LoadSprite("GnomesTimeline"));
            
            LoadedDBsHandler.GlossaryDB.AddNewPassive(AAShyInfo);
            LoadedDBsHandler.GlossaryDB.AddNewPassive(AAConfrontationalInfo);
            LoadedDBsHandler.GlossaryDB.AddNewPassive(AACopyThatInfo);
            LoadedDBsHandler.GlossaryDB.AddNewPassive(AAGnomeInfo);

            if (/*!AApocrypha.CrossMod.StewSpecimens*/true) // turns out the original Free-Willed doesn't have a glossary entry, so this adds one regardless
            {
                GlossaryPassives AAFreeWillInfo = new GlossaryPassives("Free-Willed", "This party member acts of their own free will, but can still be manually moved.", ResourceLoader.LoadSprite("IconStewSpecimensFreeWill"));
                LoadedDBsHandler.GlossaryDB.AddNewPassive(AAFreeWillInfo);
            }

            if (!AApocrypha.CrossMod.SaltEnemies)
            {
                GlossaryPassives AAHeterochromiaInfo = new GlossaryPassives("Heterochromia", "Upon receiving any kind of damage, randomize this unit's health colour.", ResourceLoader.LoadSprite("IconHemochromia"));
                LoadedDBsHandler.GlossaryDB.AddNewPassive(AAHeterochromiaInfo);
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
