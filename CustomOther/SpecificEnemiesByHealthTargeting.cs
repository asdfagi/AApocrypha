using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomOther
{
    // Directly taken from the Hell Island Fell github repository
    public class SpecificEnemiesByHealthTargeting : BaseCombatTargettingSO
    {
        public string[] _enemies;
        public int[] slotOffsets;
        public bool targetUnitAllySlots;
        public bool getAllUnitSelfSlots;
        public bool blacklist = false;
        public bool _lowest = true;
        public bool _ignoreFullHealth = true;

        public override bool AreTargetAllies => targetUnitAllySlots;
        public override bool AreTargetSlots => true;

        public override TargetSlotInfo[] GetTargets(SlotsCombat slots, int casterSlotID, bool isCasterCharacter)
        {
            if (_enemies == null || slotOffsets == null)
                return [];

            var chars = CombatManager.Instance._stats.EnemiesOnField;
            var res = new List<TargetSlotInfo>();
            var healthThreshold = 0;

            var filteredChars = new Dictionary<int, EnemyCombat>();

            if (_lowest)
            {
                foreach (var ch in chars.Values)
                {
                    if (ch == null || ch.Enemy == null)
                        continue;

                    var id = ch.Enemy.name;
                    if (string.IsNullOrEmpty(id))
                        continue;
                    if (blacklist == false && Array.IndexOf(_enemies, id) < 0)
                        continue;
                    if (blacklist == true && Array.IndexOf(_enemies, id) >= 0)
                        continue;

                    if (ch.CurrentHealth <= 0) { continue; }

                    if (ch.CurrentHealth >= ch.MaximumHealth && _ignoreFullHealth) { continue; }

                    if (healthThreshold == 0)
                    {
                        healthThreshold = ch.CurrentHealth;
                        continue;
                    }

                    if (healthThreshold <= ch.CurrentHealth)
                    {
                        continue;
                    }

                    if (healthThreshold > ch.CurrentHealth)
                    {
                        healthThreshold = ch.CurrentHealth;
                        continue;
                    }
                }
            }

            if (healthThreshold <= 0)
            {
                return [];
            }

            int filterIndex = 0;
            foreach (var ch in chars.Values)
            {
                if (ch.CurrentHealth <= healthThreshold)
                {
                    filteredChars.Add(filterIndex, ch);
                    filterIndex++;
                }
            }

            foreach (var ch in filteredChars.Values)
            {
                if (ch == null || ch.Enemy == null)
                    continue;

                var id = ch.Enemy.name;
                if (string.IsNullOrEmpty(id))
                    continue;
                if (blacklist == false && Array.IndexOf(_enemies, id) < 0)
                    continue;
                if (blacklist == true && Array.IndexOf(_enemies, id) >= 0)
                    continue;

                var chSID = ch.SlotID;
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
