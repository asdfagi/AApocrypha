using System;
using System.Collections.Generic;
using System.Text;

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

            // Evasive - Skittish, but only if there is an opposing unit.
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

            // Adding to pool
            Passives.AddCustomPassiveToPool("Shy_PA", "Shy", shy);
            Passives.AddCustomPassiveToPool("Confrontational_PA", "Confrontational", confrontational);
            //Passives.AddCustomPassiveToPool("DecayAbandonedAltar_PA", "Decay", DecayAbandonedAltar);

            // Glossary entries
            GlossaryPassives AAShyInfo = new GlossaryPassives("Shy", "Upon performing an ability, this party member/enemy will move to the left or right if there is an enemy/party member opposing them.", ResourceLoader.LoadSprite("IconShy"));
            GlossaryPassives AAConfrontationalInfo = new GlossaryPassives("Confrontational", "Upon performing an ability, this party member/enemy will move to the left or right unless there is an enemy/party member opposing them.", ResourceLoader.LoadSprite("IconConfrontational"));
            GlossaryPassives AACopyThatInfo = new GlossaryPassives("Copy That", "At the start of combat, select a set amount of random party members. Copy one passive and two abilities (excluding Slap) from each selected party member onto this enemy. Change this enemy's health color to a combination of all selected party members' health colors.", ResourceLoader.LoadSprite("IconCopyThat"));

            LoadedDBsHandler.GlossaryDB.AddNewPassive(AAShyInfo);
            LoadedDBsHandler.GlossaryDB.AddNewPassive(AAConfrontationalInfo);
            LoadedDBsHandler.GlossaryDB.AddNewPassive(AACopyThatInfo);
        }

        // Copy That - The unhinged ability copying passive of the Simulacrum.
        private static readonly Dictionary<int, BasePassiveAbilitySO> GeneratedCopyThat = [];

        public static BasePassiveAbilitySO CopyThatGenerator(int amount)
        {
            return GetOrCreatePassive(GeneratedCopyThat, amount, delegate (int x)
            {
                PerformEffectPassiveAbility copythat = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
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
