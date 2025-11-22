using FMOD;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using static MythosFriends.Effectsa.TbazTargeting.MirrorTargetting;
using static UnityEngine.GraphicsBuffer;

namespace MythosFriends.Effectsa
{
    // thanks Tairbaz
    public static class TbazTargeting
    {


        public static BaseCombatTargettingSO Mirror(bool allies)
        {
            MirrorTargetting mirror = ScriptableObject.CreateInstance<MirrorTargetting>();
            mirror.getAllies = allies;
            return mirror;
        }

        public static BaseCombatTargettingSO Farthest(bool allies)
        {
            FarthestTile mirror = ScriptableObject.CreateInstance<FarthestTile>();
            mirror.getAllies = allies;
            return mirror;
        }


        public class MirrorTargetting : BaseCombatTargettingSO
        {
            public bool getAllies;

            public bool ignoreCastSlot = true;

            public override bool AreTargetAllies => getAllies;

            public override bool AreTargetSlots => true;

            public override TargetSlotInfo[] GetTargets(SlotsCombat slots, int casterSlotID, bool isCasterCharacter)
            {
                List<TargetSlotInfo> targets = new List<TargetSlotInfo>();
                CombatSlot mirror = null;
                if ((isCasterCharacter && getAllies) || (!isCasterCharacter && !getAllies))
                {
                    foreach (CombatSlot slot in slots.CharacterSlots)
                    {

                        if (casterSlotID == 4)
                        {
                            mirror = slots.CharacterSlots[0];

                        }
                        else if (casterSlotID == 3)
                        {
                            mirror = slots.CharacterSlots[1];

                        }
                        else if (casterSlotID == 2)
                        {
                            mirror = slots.CharacterSlots[casterSlotID];

                        }
                        else if (casterSlotID == 1)
                        {
                            mirror = slots.CharacterSlots[3];

                        }
                        else if (casterSlotID == 0)
                        {
                            mirror = slots.CharacterSlots[4];

                        }
                        else
                        {
                            mirror = slots.CharacterSlots[casterSlotID];
                        }


                    }
                }
                else
                {
                    foreach (CombatSlot slot in slots.EnemySlots)
                    {
                        if (casterSlotID == 4)
                        {
                            mirror = slots.EnemySlots[0];

                        }
                        else if (casterSlotID == 3)
                        {
                            mirror = slots.EnemySlots[1];

                        }
                        else if (casterSlotID == 2)
                        {
                            mirror = slots.EnemySlots[casterSlotID];

                        }
                        else if (casterSlotID == 1)
                        {
                            mirror = slots.EnemySlots[3];

                        }
                        else if (casterSlotID == 0)
                        {
                            mirror = slots.EnemySlots[4];

                        }
                        else
                        {
                            mirror = slots.EnemySlots[casterSlotID];
                        }
                    }
                }
                if (mirror != null)
                {
                    targets.Add(mirror.TargetSlotInformation);
                }
                return targets.ToArray();
            }


            public class FarthestTile : BaseCombatTargettingSO
            {
                public bool getAllies;

                public bool ignoreCastSlot = true;

                public override bool AreTargetAllies => getAllies;

                public override bool AreTargetSlots => true;

                public override TargetSlotInfo[] GetTargets(SlotsCombat slots, int casterSlotID, bool isCasterCharacter)
                {
                    List<TargetSlotInfo> targets = new List<TargetSlotInfo>();
                    CombatSlot mirror = null;
                    CombatSlot mirror2 = null;
                    if ((isCasterCharacter && getAllies) || (!isCasterCharacter && !getAllies))
                    {
                        foreach (CombatSlot slot in slots.CharacterSlots)
                        {

                            if (casterSlotID == 0 || casterSlotID == 1)
                            {
                                mirror = slots.CharacterSlots[4];
                              

                            }
                            else if (casterSlotID == 3 || casterSlotID == 4)
                            {
                                mirror = slots.CharacterSlots[0];
                             

                            }

                            else
                            {
                                mirror = slots.CharacterSlots[0];
                                mirror2 = slots.CharacterSlots[4];
                              
                            }


                        }
                    }
                    else
                    {
                        foreach (CombatSlot slot in slots.EnemySlots)
                        {
                            if (casterSlotID == 0 || casterSlotID == 1)
                            {
                                mirror = slots.EnemySlots[4];
                          

                            }
                            else if (casterSlotID == 3 || casterSlotID == 4)
                            {
                                mirror = slots.EnemySlots[0];
                               

                            }

                            else
                            {
                                mirror = slots.EnemySlots[0];
                                mirror2 = slots.EnemySlots[4];
                               
                            }
                        }
                    }
                    if (mirror != null)
                    {
                        targets.Add(mirror.TargetSlotInformation);
                    }
                    if (mirror2 != null)
                    {
                        targets.Add(mirror2.TargetSlotInformation);
                    }
                    return targets.ToArray();
                }
            }

           
            

        }

    }
}
