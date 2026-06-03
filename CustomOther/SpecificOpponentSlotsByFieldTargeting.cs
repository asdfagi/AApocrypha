using System;
using System.Collections.Generic;
using System.Text;
using BrutalAPI;

namespace A_Apocrypha.CustomOther
{
    public class SpecificOpponentSlotsByFieldTargeting : BaseCombatTargettingSO
    {
        public string _fieldEffectID;
        public bool targetUnitAllySlots;
        public bool getAllUnitSelfSlots;
        public bool _inverted = false;

        public override bool AreTargetAllies => targetUnitAllySlots;
        public override bool AreTargetSlots => true;

        public override TargetSlotInfo[] GetTargets(SlotsCombat slots, int casterSlotID, bool isCasterCharacter)
        {
            if (_fieldEffectID == null)
                return [];

            var enemies = CombatManager.Instance._stats.EnemiesOnField;
            var chars = CombatManager.Instance._stats.CharactersOnField;
            var res = new List<TargetSlotInfo>();

            if (isCasterCharacter)
            {
                foreach (CombatSlot enemySlot in slots.EnemySlots)
                {
                    if (!enemySlot.ContainsFieldEffect(_fieldEffectID) && !_inverted)
                        continue;

                    if (enemySlot.ContainsFieldEffect(_fieldEffectID) && _inverted)
                        continue;

                    if (!targetUnitAllySlots) { res.Add(enemySlot.TargetSlotInformation); }
                    else if (getAllUnitSelfSlots) { res.Add(slots.CharacterSlots[enemySlot.SlotID].TargetSlotInformation); }
                }
            }
            else
            {
                foreach (CombatSlot charSlot in slots.CharacterSlots)
                {
                    if (!charSlot.ContainsFieldEffect(_fieldEffectID) && !_inverted)
                        continue;

                    if (charSlot.ContainsFieldEffect(_fieldEffectID) && _inverted)
                        continue;

                    if (!targetUnitAllySlots) { res.Add(charSlot.TargetSlotInformation); }
                    else if (getAllUnitSelfSlots) { res.Add(slots.EnemySlots[charSlot.SlotID].TargetSlotInformation); }
                }
            }

            return [.. res];
        }
    }
}
