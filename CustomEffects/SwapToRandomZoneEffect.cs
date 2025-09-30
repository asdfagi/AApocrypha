using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomEffects
{
    // shamelessly taken from the Salt Enemies github repository
    public class SwapToRandomZoneEffect : EffectSO
    {

        public override bool PerformEffect(
          CombatStats stats,
          IUnit caster,
          TargetSlotInfo[] targets,
          bool areTargetSlots,
          int entryVariable,
          out int exitAmount)
        {
            exitAmount = 0;
            if (stats.combatSlots.UnitInSlotContainsFieldEffect(caster.SlotID, caster.IsUnitCharacter, StatusField_GameIDs.Constricted_ID.ToString()))
            {
                return false;
            }
            if (caster.CurrentHealth < 1)
            {
                return false;
            }
            foreach (TargetSlotInfo target in targets)
            {
                int secondSlotID = UnityEngine.Random.Range(0, 5);
                if (secondSlotID == 5)
                {
                    //Debug.Log("failed, ran again");
                    //Debug.Log("second Slot was out of bounds");
                    EffectInfo swapAgain = Effects.GenerateEffect(ScriptableObject.CreateInstance<SwapToRandomZoneEffect>(), 1, Targeting.GenerateSlotTarget(new int[9] { -4, -3, -2, -1, 0, 1, 2, 3, 4 }, true));
                    CombatManager.Instance.AddSubAction(new EffectAction(new EffectInfo[] { swapAgain }, caster));
                    return false;
                }
                if (secondSlotID == caster.SlotID)
                {
                    //Debug.Log("effect ran but second slot ID was caster Slot ID so it didn't move");
                    return exitAmount > 0;
                }
                if (secondSlotID != caster.SlotID)
                {
                    /*if (stats.combatSlots.CanEnemiesSwap(caster.SlotID, secondSlotID, out var firstSlotSwap, out var secondSlotSwap))
                    {*/
                    //Debug.Log("caster.SlotID:");
                    //Debug.Log(caster.SlotID);
                    //Debug.Log("secondSlotID:");
                    //Debug.Log(secondSlotID);
                    /*if (caster.SlotID != secondSlotSwap || secondSlotID != firstSlotSwap)
                    {
                        Debug.Log("failed, ran again");
                        if (caster.SlotID != secondSlotSwap)
                        {
                            Debug.Log("caster slot not equal second slot target");
                        }
                        if (secondSlotID != firstSlotSwap)
                        {
                            Debug.Log("target slot not equal caster slot target");
                        }
                        Effect swapAgain = Effects.GenerateEffect(ScriptableObject.CreateInstance<SwapToRandomZoneEffect>(), 1, new IntentType?(), Slots.Self);
                        CombatManager.Instance.AddSubAction(new EffectAction(ExtensionMethods.ToEffectInfoArray(new Effect[1] { swapAgain }), caster));
                        return false;
                    }*/
                    int thisTarget = 0;
                    for (int i = 0; i < targets.Length; i++)
                    {
                        if (targets[i].SlotID == secondSlotID)
                        {
                            thisTarget = i;
                            //Debug.Log("found it");
                        }
                    }
                    TargetSlotInfo unit2 = targets[thisTarget];
                    if (unit2.HasUnit)
                    {
                        //Debug.Log(unit2.Unit.SlotID);
                    }
                    if (!unit2.HasUnit)
                    {
                        //Debug.Log("empty slot");
                    }
                    bool hasRight = false;
                    for (int i = 0; i < targets.Length; i++)
                    {
                        if (targets[i].SlotID == caster.SlotID + 1)
                        {
                            //Debug.Log("found it");
                            hasRight = targets[i].HasUnit;
                        }
                    }
                    bool hasLeft = false;
                    for (int i = 0; i < targets.Length; i++)
                    {
                        if (targets[i].SlotID == caster.SlotID - 1)
                        {
                            //Debug.Log("found it");
                            hasLeft = targets[i].HasUnit;
                        }
                    }



                    /*if (secondSlotID > 0)
                        {
                            Debug.Log("second slot id isnt 0");
                            CombatSlot checkIf2Tile = new CombatSlot(secondSlotID - 1, false);
                            if (checkIf2Tile.HasUnit)
                            {
                                Debug.Log("has unit");
                                if (checkIf2Tile.Unit.Size > 1)
                                {
                                    Debug.Log("big enemy");
                                    Debug.Log(checkIf2Tile.Unit.Size);
                                    {
                                        unit2 = checkIf2Tile;
                                    }
                                }
                            }
                            Debug.Log("should have checked if 2 or bigger tile");
                        }*/
                    /*if (!stats.combatSlots.CanEnemiesSwap(caster.SlotID, secondSlotID, out var firstSlotSwap, out var secondSlotSwap))
                    {
                        Debug.Log("they can't swap for whatever reason, run it again.");
                        Debug.Log("caster slot id");
                        Debug.Log(caster.SlotID);
                        Debug.Log("caster moves to slot");
                        Debug.Log(firstSlotSwap);
                        Debug.Log("target slot id");
                        Debug.Log(secondSlotID);
                        Debug.Log("target unit moves to slot");
                        Debug.Log(secondSlotSwap);
                        Effect swap2 = Effects.GenerateEffect(ScriptableObject.CreateInstance<SwapToRandomZoneEffect>(), 1, new IntentType?(), Slots.SlotTarget(new int[9] {-4, -3, -2, -1, 0, 1, 2, 3, 4}, true));
                        CombatManager.Instance.AddSubAction(new EffectAction(ExtensionMethods.ToEffectInfoArray(new Effect[1] { swap2 }), caster));
                        return false;
                    }*/
                    //Debug.Log("can swap");
                    if (unit2.HasUnit == false)
                    {
                        //Debug.Log("slot is empty");
                        if (stats.combatSlots.CanEnemiesSwap(caster.SlotID, secondSlotID, out var firstSlotSwapA, out var secondSlotSwapA))
                        {
                            if (stats.combatSlots.SwapEnemies(caster.SlotID, firstSlotSwapA, secondSlotID, secondSlotSwapA))
                            {
                                //Debug.Log("moved!!");
                                exitAmount++;
                                return exitAmount > 0;
                            }
                        }
                    }
                    //Debug.Log("has unit");
                    if (caster.Size == 1 && unit2.Unit.Size == 1)
                    {
                        //Debug.Log("both are size 1");
                        if (stats.combatSlots.SwapEnemies(caster.SlotID, secondSlotID, secondSlotID, caster.SlotID))
                        {
                            //Debug.Log("moved!!");
                            exitAmount++;
                            return exitAmount > 0;
                        }
                    }
                    if (caster.Size == 1 && unit2.Unit.Size == 2)
                    {
                        //Debug.Log("caster is size 1, target is size 2");
                        if (caster.SlotID + 1 == unit2.Unit.SlotID)
                        {
                            //Debug.Log("2 size target is 1 right from caster");
                            //Debug.Log("second slot id:");
                            //Debug.Log(secondSlotID);
                            //Debug.Log("unit 2 slot id:");
                            //Debug.Log(unit2.Unit.SlotID);
                            if (secondSlotID == unit2.Unit.SlotID)
                            {
                                //Debug.Log("second slot id is same as unit 2 slot id");
                                if (stats.combatSlots.SwapEnemies(caster.SlotID, secondSlotID + 1, unit2.Unit.SlotID, caster.SlotID))
                                {
                                    //Debug.Log("moved!!");
                                    exitAmount++;
                                    return exitAmount > 0;
                                }
                            }
                            if (secondSlotID - 1 == unit2.Unit.SlotID)
                            {
                                //Debug.Log("second slot id +1 to unit 2 slot id");
                                if (stats.combatSlots.SwapEnemies(caster.SlotID, secondSlotID, unit2.Unit.SlotID, caster.SlotID))
                                {
                                    //Debug.Log("moved!!");
                                    exitAmount++;
                                    return exitAmount > 0;
                                }
                            }
                        }
                        if (caster.SlotID - 2 == unit2.Unit.SlotID)
                        {
                            //Debug.Log("2 size target is 1 left from caster");
                            //Debug.Log("second slot id:");
                            //Debug.Log(secondSlotID);
                            //Debug.Log("unit 2 slot id:");
                            //Debug.Log(unit2.Unit.SlotID);
                            if (secondSlotID == unit2.Unit.SlotID)
                            {
                                //Debug.Log("second slot id is same as unit 2 slot id");
                                if (stats.combatSlots.SwapEnemies(caster.SlotID, secondSlotID, unit2.Unit.SlotID, caster.SlotID - 1))
                                {
                                    //Debug.Log("moved!!");
                                    exitAmount++;
                                    return exitAmount > 0;
                                }
                            }
                            if (secondSlotID - 1 == unit2.Unit.SlotID)
                            {
                                //Debug.Log("second slot id +1 to unit 2 slot id");
                                if (stats.combatSlots.SwapEnemies(caster.SlotID, secondSlotID - 1, unit2.Unit.SlotID, caster.SlotID - 1))
                                {
                                    //Debug.Log("moved!!");
                                    exitAmount++;
                                    return exitAmount > 0;
                                }
                            }
                        }
                        //Debug.Log("caster is not next to 2 size target.");
                        if (caster.SlotID == 4)
                        {
                            //Debug.Log("caster is in rightmost slot");
                            //Debug.Log("second slot id:");
                            //Debug.Log(secondSlotID);
                            //Debug.Log("unit 2 slot id:");
                            //Debug.Log(unit2.Unit.SlotID);
                            if (hasLeft == false)
                            {
                                if (stats.combatSlots.SwapEnemies(caster.SlotID, unit2.Unit.SlotID + 1, unit2.Unit.SlotID, caster.SlotID - 1))
                                {
                                    if (hasLeft == true)
                                    {
                                        //Debug.Log("caster has unit to the left");
                                        stats.combatSlots.SwapEnemies(caster.SlotID - 1, unit2.Unit.SlotID, unit2.Unit.SlotID, caster.SlotID - 1);
                                    }
                                    //Debug.Log("moved!!");
                                    exitAmount++;
                                    return exitAmount > 0;
                                }
                            }
                        }
                        if (caster.SlotID == 0)
                        {
                            //Debug.Log("caster is in leftmost slot");
                            //Debug.Log("second slot id:");
                            //Debug.Log(secondSlotID);
                            //Debug.Log("unit 2 slot id:");
                            //Debug.Log(unit2.Unit.SlotID);
                            if (hasRight == false)
                            {
                                if (stats.combatSlots.SwapEnemies(caster.SlotID, unit2.Unit.SlotID, unit2.Unit.SlotID, caster.SlotID))
                                {
                                    if (hasRight == true)
                                    {
                                        //Debug.Log("caster has unit to the right");
                                        stats.combatSlots.SwapEnemies(caster.SlotID + 1, unit2.Unit.SlotID + 1, unit2.Unit.SlotID, caster.SlotID);
                                    }
                                    //Debug.Log("moved!!");
                                    exitAmount++;
                                    return exitAmount > 0;
                                }
                            }
                        }
                        if (caster.SlotID != 4 && caster.SlotID != 0)
                        {
                            //Debug.Log("caster is not in rightmost or leftmost slot.");
                            //Debug.Log("second slot id:");
                            //Debug.Log(secondSlotID);
                            //Debug.Log("unit 2 slot id:");
                            //Debug.Log(unit2.Unit.SlotID);
                            if (secondSlotID == unit2.Unit.SlotID)
                            {
                                //Debug.Log("target slot is same as unit slot");
                                if (hasRight == false)
                                {
                                    if (stats.combatSlots.SwapEnemies(caster.SlotID, secondSlotID, unit2.Unit.SlotID, caster.SlotID))
                                    {
                                        if (hasRight == true)
                                        {
                                            //Debug.Log("caster has unit to the right");
                                            stats.combatSlots.SwapEnemies(caster.SlotID + 1, secondSlotID + 1, secondSlotID, caster.SlotID);
                                        }
                                        //Debug.Log("moved!!");
                                        exitAmount++;
                                        return exitAmount > 0;
                                    }
                                }
                                else if (hasLeft == false)
                                {
                                    if (stats.combatSlots.SwapEnemies(caster.SlotID, secondSlotID, unit2.Unit.SlotID, caster.SlotID - 1))
                                    {
                                        //Debug.Log("moved!!");
                                        exitAmount++;
                                        return exitAmount > 0;
                                    }
                                }
                            }
                            if (secondSlotID + 1 == unit2.Unit.SlotID)
                            {
                                //Debug.Log("target slot is not same as unit slot");
                                if (hasLeft == false)
                                {
                                    if (stats.combatSlots.SwapEnemies(caster.SlotID, secondSlotID, secondSlotID - 1, caster.SlotID - 1))
                                    {
                                        if (hasLeft == true)
                                        {
                                            //Debug.Log("caster has unit to the left");
                                            stats.combatSlots.SwapEnemies(caster.SlotID - 1, unit2.Unit.SlotID, unit2.Unit.SlotID, caster.SlotID - 1);
                                        }
                                        //Debug.Log("moved!!");
                                        exitAmount++;
                                        return exitAmount > 0;
                                    }
                                }
                                else if (hasRight == false)
                                {
                                    if (stats.combatSlots.SwapEnemies(caster.SlotID, secondSlotID, secondSlotID - 1, caster.SlotID))
                                    {
                                        //Debug.Log("moved!!");
                                        exitAmount++;
                                        return exitAmount > 0;
                                    }
                                }
                            }
                        }
                    }
                    if (unit2.Unit.Size >= 3)
                    {
                        //Debug.Log("target unit is size 3 or greater, fuck this.");
                        //Debug.Log("Swap sides effect");
                        EffectInfo swapSides = Effects.GenerateEffect(ScriptableObject.CreateInstance<CustomSwapToSidesEffect>(), 1, Targeting.Slot_SelfAll);
                        CombatManager.Instance.AddSubAction(new EffectAction(new EffectInfo[] { swapSides }, caster));
                        exitAmount++;
                        return exitAmount > 0;
                    }


                    //Debug.Log("failed, ran again");
                    EffectInfo swapAgain = Effects.GenerateEffect(ScriptableObject.CreateInstance<SwapToRandomZoneEffect>(), 1, Targeting.GenerateSlotTarget(new int[9] { -4, -3, -2, -1, 0, 1, 2, 3, 4 }, true));
                    CombatManager.Instance.AddSubAction(new EffectAction(new EffectInfo[] { swapAgain }, caster));
                    return exitAmount > 0;

                }
            }
            return exitAmount > 0;
        }
    }
}
