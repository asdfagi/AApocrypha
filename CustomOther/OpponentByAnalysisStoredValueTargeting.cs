using System;
using System.Collections.Generic;
using System.Text;
using BrutalAPI;

namespace A_Apocrypha.CustomOther
{
    // Directly taken from the Hell Island Fell github repository
    public class OpponentByAnalysisStoredValueTargeting : BaseCombatTargettingSO
    {
        public string _storedValueID;
        public bool targetUnitAllySlots; // interpreted in reverse here, don't worry too much about it
        public bool getAllUnitSelfSlots;
        public int[] _modifiers = [0];

        public override bool AreTargetAllies => targetUnitAllySlots;
        public override bool AreTargetSlots => false;

        public override TargetSlotInfo[] GetTargets(SlotsCombat slots, int casterSlotID, bool isCasterCharacter)
        {
            if (_storedValueID == null)
                return [];

            var enemies = CombatManager.Instance._stats.EnemiesOnField;
            var chars = CombatManager.Instance._stats.CharactersOnField;
            IUnit caster = null;
            if (isCasterCharacter)
            {
                foreach (var ch in chars.Values)
                {
                    if (ch.SlotID == casterSlotID)
                    {
                        caster = ch;
                        break;
                    }
                }
            }
            else
            {
                foreach (var en in enemies.Values)
                {
                    if (en.SlotID == casterSlotID)
                    {
                        caster = en;
                        break;
                    }
                }
            }
            var res = new List<TargetSlotInfo>();
            var targetSlotID = -1;

            caster.TryGetStoredData(_storedValueID, out var targetIDValue);
            if (isCasterCharacter)
            {
                foreach (EnemyCombat enemy in enemies.Values)
                {
                    if (enemy.ID == targetIDValue.m_MainData)
                    {
                        targetSlotID = enemy.SlotID;
                        break;
                    }
                }
            }
            else
            {
                foreach (CharacterCombat character in chars.Values)
                {
                    if (character.ID == targetIDValue.m_MainData)
                    {
                        targetSlotID = character.SlotID;
                        break;
                    }
                }
            }

            if (targetSlotID < 0 || targetSlotID > 4) { return []; }

            foreach (int modifier in _modifiers)
            {
                if (targetSlotID + modifier > 4 || targetSlotID + modifier < 0) { continue; }
                if (targetUnitAllySlots)
                {
                    res.Add(slots.GetAllySlotTarget(targetSlotID + modifier, 0, isCasterCharacter));
                }
                else
                {
                    res.Add(slots.GetOpponentSlotTarget(targetSlotID + modifier, 0, isCasterCharacter));
                }
            }
            return [.. res];
        }
    }
}
