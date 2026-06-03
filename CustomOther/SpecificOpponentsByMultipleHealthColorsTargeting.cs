using System;
using System.Collections.Generic;
using System.Text;
using BrutalAPI;
using TMPro;

namespace A_Apocrypha.CustomOther
{
    // Directly taken from the Hell Island Fell github repository
    public class SpecificOpponentsByMultipleHealthColorsTargeting : BaseCombatTargettingSO
    {
        public ManaColorSO[] _colors;
        public bool _contains = false;
        public int[] slotOffsets;
        public bool targetUnitAllySlots; // interpreted in reverse here, don't worry too much about it
        public bool getAllUnitSelfSlots;
        public bool blacklist = false;

        public override bool AreTargetAllies => !targetUnitAllySlots;
        public override bool AreTargetSlots => true;

        public override TargetSlotInfo[] GetTargets(SlotsCombat slots, int casterSlotID, bool isCasterCharacter)
        {
            if (_colors == null || slotOffsets == null || _colors.Length <= 0)
                return [];

            var enemies = CombatManager.Instance._stats.EnemiesOnField;
            var chars = CombatManager.Instance._stats.CharactersOnField;
            var res = new List<TargetSlotInfo>();

            if (isCasterCharacter) 
            {
                foreach (var en in enemies.Values)
                {
                    if (en == null || en.Enemy == null)
                        continue;

                    if (blacklist)
                    {
                        bool skip = false;
                        foreach (ManaColorSO _color in _colors)
                        {
                            if (_contains && en.HealthColor.SharesPigmentColor(_color)) { skip = true; }
                            if (!_contains && en.HealthColor == _color) { skip = true; }
                        }
                        if (skip) { continue; }
                    }
                    else
                    {
                        bool skip = false;
                        foreach (ManaColorSO _color in _colors)
                        {
                            if (_contains && !en.HealthColor.SharesPigmentColor(_color)) { skip = true; }
                            if (!_contains && en.HealthColor != _color) { skip = true; }
                        }
                        if (skip) { continue; }
                    }

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
                foreach (var ch in chars.Values)
                {
                    //Debug.Log($"ch {ch} | ch.Character {ch.Character.name}");
                    if (blacklist)
                    {
                        bool skip = false;
                        foreach (ManaColorSO _color in _colors)
                        {
                            if (_contains && ch.HealthColor.SharesPigmentColor(_color)) { skip = true; }
                            if (!_contains && ch.HealthColor == _color) { skip = true; }
                        }
                        if (skip) { continue; }
                    }
                    else
                    {
                        bool skip = false;
                        foreach (ManaColorSO _color in _colors)
                        {
                            if (_contains && !ch.HealthColor.SharesPigmentColor(_color)) { skip = true; }
                            if (!_contains && ch.HealthColor != _color) { skip = true; }
                        }
                        if (skip) { continue; }
                    }

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
