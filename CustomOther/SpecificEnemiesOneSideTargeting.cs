using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomOther
{
    // Directly taken from the Hell Island Fell github repository
    public class SpecificEnemiesOneSideTargeting : BaseCombatTargettingSO
    {
        public string[] _enemies;
        public int[] slotOffsets;
        public bool targetUnitAllySlots;
        public bool getAllUnitSelfSlots;
        public bool _right = false;

        public override bool AreTargetAllies => targetUnitAllySlots;
        public override bool AreTargetSlots => true;

        public override TargetSlotInfo[] GetTargets(SlotsCombat slots, int casterSlotID, bool isCasterCharacter)
        {
            if (_enemies == null || slotOffsets == null)
                return [];

            var chars = CombatManager.Instance._stats.EnemiesOnField;
            var res = new List<TargetSlotInfo>();

            foreach (var ch in chars.Values)
            {
                if (ch == null || ch.Enemy == null)
                    continue;

                var id = ch.Enemy.name;
                if (string.IsNullOrEmpty(id) || Array.IndexOf(_enemies, id) < 0)
                    continue;

                var chSID = ch.SlotID;
                if (_right && chSID < casterSlotID) { continue; }
                if (!_right && chSID > casterSlotID) { continue; }

                var chIsCharacter = ch.IsUnitCharacter;

                foreach (var offs in slotOffsets)
                {
                    if (offs == 0)
                    {
                        if (!targetUnitAllySlots)
                        {
                            res.AddRange(slots.GetFrontOpponentSlotTargets(chSID, chIsCharacter));
                            continue;
                        }
                        else if (getAllUnitSelfSlots)
                        {
                            res.AddRange(slots.GetAllSelfSlots(chSID, chIsCharacter));
                            continue;
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
