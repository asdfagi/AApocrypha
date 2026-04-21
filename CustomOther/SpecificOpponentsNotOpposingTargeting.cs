using System;
using System.Collections.Generic;
using System.Text;
using BrutalAPI;

namespace A_Apocrypha.CustomOther
{
    // Directly taken from the Hell Island Fell github repository
    public class SpecificOpponentsNotOpposingTargeting : BaseCombatTargettingSO
    {
        public int[] slotOffsets;
        public bool targetUnitAllySlots; // interpreted in reverse here, don't worry too much about it
        public bool getAllUnitSelfSlots;

        public override bool AreTargetAllies => targetUnitAllySlots;
        public override bool AreTargetSlots => false;

        public override TargetSlotInfo[] GetTargets(SlotsCombat slots, int casterSlotID, bool isCasterCharacter)
        {
            if (slotOffsets == null)
                return [];

            var enemies = CombatManager.Instance._stats.EnemiesOnField;
            var chars = CombatManager.Instance._stats.CharactersOnField;
            var res = new List<TargetSlotInfo>();
            List<int> checkedIDs = new List<int>();

            if (isCasterCharacter) 
            {
                foreach (var en in enemies.Values)
                {
                    if (en == null || en.Enemy == null)
                        continue;

                    if (en.SlotID == casterSlotID) { continue; }
                    if (checkedIDs.Contains(en.ID)) { continue; }

                    bool largeIsOpposing = false;
                    if (en.Size > 1)
                    {
                        for (int i = 0; i < en.Size; i++)
                        {
                            if (en.SlotID + i == casterSlotID)
                            {
                                largeIsOpposing = true;
                                break;
                            }
                        }
                    }
                    if (largeIsOpposing) { continue; }

                    var chSID = en.SlotID;
                    var chIsCharacter = en.IsUnitCharacter;
                    checkedIDs.Add(en.ID);

                    foreach (var offs in slotOffsets)
                    {
                        if (offs == 0)
                        {
                            if (isCasterCharacter)
                            {
                                if (!targetUnitAllySlots)
                                {
                                    res.AddRange([slots.GetAllSelfSlots(chSID, chIsCharacter).ToArray()[0]]);
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
                foreach (var ch in chars.Values)
                {
                    Debug.Log($"ch {ch} | ch.Character {ch.Character.name}");
                    if (ch == null || ch.Character == null)
                        continue;

                    if (ch.SlotID == casterSlotID) { continue; }
                    if (checkedIDs.Contains(ch.ID)) { continue; }

                    var chSID = ch.SlotID;
                    var chIsCharacter = ch.IsUnitCharacter;
                    checkedIDs.Add(ch.ID);

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
