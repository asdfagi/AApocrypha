using BrutalAPI;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace A_Apocrypha.CustomEffects
{
    public class MultiTargetting : BaseCombatTargettingSO
    {
        public BaseCombatTargettingSO first;
        public BaseCombatTargettingSO second;
        public override bool AreTargetAllies => first.AreTargetAllies && second.AreTargetAllies;
        public override bool AreTargetSlots => first.AreTargetSlots && second.AreTargetSlots;
        public override TargetSlotInfo[] GetTargets(SlotsCombat slots, int casterSlotID, bool isCasterCharacter)
        {
            TargetSlotInfo[] one = first.GetTargets(slots, casterSlotID, isCasterCharacter);
            TargetSlotInfo[] two = second.GetTargets(slots, casterSlotID, isCasterCharacter);
            TargetSlotInfo[] ret = new TargetSlotInfo[one.Length + two.Length];
            Array.Copy(one, ret, one.Length);
            Array.Copy(two, 0, ret, one.Length, two.Length);
            return ret;
        }

        public static MultiTargetting Create(BaseCombatTargettingSO first, BaseCombatTargettingSO second)
        {
            MultiTargetting ret = ScriptableObject.CreateInstance<MultiTargetting>();
            ret.first = first;
            ret.second = second;
            return ret;
        }
    }
    public static class Slots
    {
        public static BaseCombatTargettingSO Self => Targeting.Slot_SelfSlot;
        public static BaseCombatTargettingSO SelfSlots => Targeting.Slot_SelfAll;
        public static BaseCombatTargettingSO Front => Targeting.Slot_Front;
        public static BaseCombatTargettingSO Sides => Targeting.Slot_AllySides;
        public static BaseCombatTargettingSO Left => Targeting.Slot_OpponentLeft;
        public static BaseCombatTargettingSO Right => Targeting.Slot_OpponentRight;
        public static BaseCombatTargettingSO LeftRight => Targeting.Slot_OpponentSides;
        public static BaseCombatTargettingSO FrontLeftRight => Targeting.Slot_FrontAndSides;

        public static BaseCombatTargettingSO AllyLeft => Targeting.Slot_AllyLeft;
        public static BaseCombatTargettingSO AllyRight => Targeting.Slot_AllyRight;
        public static BaseCombatTargettingSO AllySides => Targeting.Slot_AllySides;
        public static BaseCombatTargettingSO OtherAllies => Targeting.Unit_OtherAllies;

        public static BaseCombatTargettingSO LeftRightWrap => Targeting.GenerateSlotTarget([-4, -1, 1, 4],false);
        public static BaseCombatTargettingSO FarLeft => Targeting.GenerateSlotTarget([-2], false);
        public static BaseCombatTargettingSO FarRight => Targeting.GenerateSlotTarget([2], false);
        public static BaseCombatTargettingSO FarSides => Targeting.GenerateSlotTarget([-2, 2], false);
        public static BaseCombatTargettingSO SlotTarget(int[] ints, bool allies, bool getAllSelf = false) => Targeting.GenerateSlotTarget(ints, allies, getAllSelf);
    }
}
