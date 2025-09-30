using System;
using System.Collections.Generic;
using System.Text;
using BrutalAPI;
using static UnityEngine.GraphicsBuffer;

namespace A_Apocrypha.CustomOther
{
    // Directly taken from the Hell Island Fell github repository
    public class PartyMembersByMostPassivesTargeting : BaseCombatTargettingSO
    {
        public int[] slotOffsets;
        public bool targetUnitAllySlots; // interpreted in reverse here, don't worry too much about it
        public bool getAllUnitSelfSlots;
        public bool oneOfTargets = false;

        public override bool AreTargetAllies => targetUnitAllySlots;
        public override bool AreTargetSlots => true;

        public override TargetSlotInfo[] GetTargets(SlotsCombat slots, int casterSlotID, bool isCasterCharacter)
        {
            if (slotOffsets == null)
                return [];

            var chars = CombatManager.Instance._stats.CharactersOnField;
            var res = new List<TargetSlotInfo>();
            var mostPassives = 0;
            var mostPassivesCharacters = new List<CharacterCombat>();

            foreach (var ch in chars.Values)
            {
                if (ch == null || ch.Character == null)
                    continue;

                var passives = ch.PassiveAbilities;
                if (passives.Count == 0)
                    continue;

                if (passives.Count > mostPassives || mostPassives == 0)
                {
                    mostPassivesCharacters.Clear();
                    mostPassives = passives.Count;
                    mostPassivesCharacters.Add(ch);
                    Debug.Log($"mostPassives {mostPassives} | mostPassivesCharacters.Count {mostPassivesCharacters.Count}");
                }
                else if (passives.Count == mostPassives)
                {
                    mostPassivesCharacters.Add(ch);
                    Debug.Log($"mostPassives {mostPassives} | mostPassivesCharacters.Count {mostPassivesCharacters.Count}");
                }
            }

            if (oneOfTargets)
            {
                while (mostPassivesCharacters.Count > 1)
                {
                    int randomIndex = UnityEngine.Random.Range(0, mostPassivesCharacters.Count);
                    mostPassivesCharacters.RemoveAt(randomIndex);
                }
            }

            foreach (var h_ch in mostPassivesCharacters)
            { 
                var chSID = h_ch.SlotID;
                var chIsCharacter = h_ch.IsUnitCharacter;

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
