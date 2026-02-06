using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomEffects
{
    // thanks millie
    public class ConsumePigmentSharingCasterHealthColorEffect : EffectSO
    {
        public bool consumeAll = true;

        public ManaColorSO eatme = Pigments.Purple;
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            JumpAnimationInformation jumpInfo = stats.GenerateUnitJumpInformation(caster.ID, caster.IsUnitCharacter);
            string manaConsumedSound = stats.audioController.manaConsumedSound;
            //Debug.Log("beginning anna molly thing");
            exitAmount = 0;
            List<ManaBarSlot> slots = [];
            List<EffectInfo> todo = [];
            if (stats.MainManaBar.EmptySlotsCount == stats.MainManaBar.ManaSlotCount)
            {
                return false;
            }
            int fuck = 0;
            foreach (ManaBarSlot slot in stats.MainManaBar.ManaBarSlots)
            {
                if (!slot.IsEmpty && fuck < 10)
                {
                    if (slot.ManaColor.SharesPigmentColor(eatme))
                    {
                        //Debug.Log("added slot");
                        slots.Add(slot);
                    }
                }
                if (fuck >= 10)
                {
                    break;
                }
            }
            foreach (ManaBarSlot slot in slots)
            {
                //Debug.Log($"Contains Pigment Eater | registered slot contents: {slot.ManaColor.name}");
            }
            if (slots.Count > 0)
            {
                int counter = 0;
                foreach (ManaBarSlot toeat in slots)
                {
                    if (counter >= entryVariable && !consumeAll) { break; }
                    Debug.Log(toeat.ManaColor.ToString());
                    //foreach (KeyValuePair<string, ManaColorSO> kvp in LoadedDBsHandler.PigmentDB._PigmentPool)
                    //{
                    //    Debug.Log("Shares " + kvp.Key + "? Program says: " + toeat.ManaColor.SharesPigmentColor(kvp.Value).ToString());
                    //}
                    bool flag = false;


                    if (toeat.ManaColor.SharesPigmentColor(eatme))
                    {
                        //Debug.Log($"Contains Pigment Eater | mmm tasty {toeat.ManaColor.name}");
                        flag = true;
                    }

                    
                    if (flag)
                    {
                        stats.MainManaBar.ConsumeAmountMana(toeat.ManaColor, 1, jumpInfo, manaConsumedSound);
                        exitAmount++;
                        counter++;
                        //Debug.Log("EXIT VALUE UP!");
                    }
                }
            }

            return exitAmount > 0;
        }
    }
}
