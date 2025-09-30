using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomOther
{
    // Directly taken from the Hell Island Fell github repository
    public class SpecificPartyMembersTargeting : BaseCombatTargettingSO
    {
        public string[] _characters;
        public int[] slotOffsets;
        public bool targetUnitAllySlots; // interpreted in reverse here, don't worry too much about it
        public bool getAllUnitSelfSlots;

        public override bool AreTargetAllies => targetUnitAllySlots;
        public override bool AreTargetSlots => true;

        public override TargetSlotInfo[] GetTargets(SlotsCombat slots, int casterSlotID, bool isCasterCharacter)
        {
            if (_characters == null || slotOffsets == null)
                return [];

            var chars = CombatManager.Instance._stats.CharactersOnField;
            var res = new List<TargetSlotInfo>();

            foreach (var ch in chars.Values)
            {
                if (ch == null || ch.Character == null)
                    continue;

                var id = ch.Character.name;
                Debug.Log(id);
                if (string.IsNullOrEmpty(id) || Array.IndexOf(_characters, id) < 0)
                    continue;

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

            return [.. res];
        }
    }
}
