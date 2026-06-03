using System;
using System.Collections.Generic;
using System.Text;
using BrutalAPI;

namespace A_Apocrypha.CustomOther
{
    public class HighestHealthAllyNotCasterByPassiveTargeting : BaseCombatTargettingSO
    {
        public BasePassiveAbilitySO _passive;
        public bool _blacklist = false;
        public int[] slotOffsets;
        public bool targetUnitAllySlots;
        public bool getAllUnitSelfSlots;

        public override bool AreTargetAllies => targetUnitAllySlots;
        public override bool AreTargetSlots => true;

        public override TargetSlotInfo[] GetTargets(SlotsCombat slots, int casterSlotID, bool isCasterCharacter)
        {
            if (_passive == null || slotOffsets == null)
                return [];

            var enemies = CombatManager.Instance._stats.EnemiesOnField;
            var chars = CombatManager.Instance._stats.CharactersOnField;
            var res = new List<TargetSlotInfo>();
            
            if (!isCasterCharacter)
            {
                List<EnemyCombat> filteredEnemies = [];
                int checkerHealth = 0;
                foreach (var en in enemies.Values)
                {
                    if (en == null || en.Enemy == null)
                        continue;

                    if (en.SlotID == casterSlotID) { continue; }

                    var passives = en.PassiveAbilities;

                    if (_blacklist)
                    {
                        if (en.ContainsPassiveAbility(_passive.m_PassiveID)) { continue; }
                    }
                    else
                    {
                        if (passives.Count == 0 || !en.ContainsPassiveAbility(_passive.m_PassiveID)) { continue; }
                    }

                    if (en.CurrentHealth > checkerHealth)
                    {
                        filteredEnemies = [en];
                        checkerHealth = en.CurrentHealth;
                    } else if (en.CurrentHealth == checkerHealth)
                    {
                        filteredEnemies.Add(en);
                    }
                }

                foreach (var en in filteredEnemies) {
                    var chSID = en.SlotID;
                    var chIsCharacter = en.IsUnitCharacter;

                    foreach (var offs in slotOffsets)
                    {
                        if (offs == 0)
                        {
                            if (isCasterCharacter)
                            {
                                if (!targetUnitAllySlots)
                                {
                                    res.AddRange(slots.GetAllSelfSlots(chSID, chIsCharacter));
                                    continue;
                                }
                                else if (getAllUnitSelfSlots)
                                {
                                    res.AddRange(slots.GetFrontOpponentSlotTargets(chSID, chIsCharacter));
                                    continue;
                                }
                            }
                            else
                            {
                                if (!targetUnitAllySlots)
                                {
                                    res.AddRange(slots.GetAllSelfSlots(chSID, chIsCharacter));
                                    continue;
                                }
                                else if (getAllUnitSelfSlots)
                                {
                                    res.AddRange(slots.GetFrontOpponentSlotTargets(chSID, chIsCharacter));
                                    continue;
                                }
                            }
                        }

                        var slot = targetUnitAllySlots ? slots.GetAllySlotTarget(chSID, offs, chIsCharacter) : slots.GetOpponentSlotTarget(chSID, offs, chIsCharacter);

                        if (slot == null)
                            continue;

                        res.Add(slot);
                    }
                }
            }
            else
            {
                List<CharacterCombat> filteredCharacters = [];
                int checkerHealth = 0;
                foreach (var ch in chars.Values)
                {
                    //Debug.Log($"ch {ch} | ch.Character {ch.Character.name}");
                    if (ch == null || ch.Character == null)
                        continue;

                    if (ch.SlotID == casterSlotID) { continue; }

                    var passives = ch.PassiveAbilities;

                    if (_blacklist)
                    {
                        if (ch.ContainsPassiveAbility(_passive.m_PassiveID)) { continue; }
                    }
                    else
                    {
                        if (passives.Count == 0 || !ch.ContainsPassiveAbility(_passive.m_PassiveID)) { continue; }
                    }

                    if (ch.CurrentHealth > checkerHealth)
                    {
                        filteredCharacters = [ch];
                        checkerHealth = ch.CurrentHealth;
                    }
                    else if (ch.CurrentHealth == checkerHealth)
                    {
                        filteredCharacters.Add(ch);
                    }
                }

                foreach (var ch in filteredCharacters)
                {
                    var chSID = ch.SlotID;
                    var chIsCharacter = ch.IsUnitCharacter;

                    foreach (var offs in slotOffsets)
                    {
                        if (offs == 0)
                        {
                            if (isCasterCharacter)
                            {
                                if (!targetUnitAllySlots)
                                {
                                    res.AddRange(slots.GetAllSelfSlots(chSID, chIsCharacter));
                                    continue;
                                }
                                else if (getAllUnitSelfSlots)
                                {
                                    res.AddRange(slots.GetFrontOpponentSlotTargets(chSID, chIsCharacter));
                                    continue;
                                }
                            }
                            else
                            {
                                if (!targetUnitAllySlots)
                                {
                                    res.AddRange(slots.GetAllSelfSlots(chSID, chIsCharacter));
                                    continue;
                                }
                                else if (getAllUnitSelfSlots)
                                {
                                    res.AddRange(slots.GetFrontOpponentSlotTargets(chSID, chIsCharacter));
                                    continue;
                                }
                            }
                        }

                        var slot = targetUnitAllySlots ? slots.GetAllySlotTarget(chSID, offs, chIsCharacter) : slots.GetOpponentSlotTarget(chSID, offs, chIsCharacter);

                        if (slot == null)
                            continue;

                        res.Add(slot);
                    }
                }
            }
            return [.. res];
        }
    }
}
