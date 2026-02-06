using System;
using System.Collections.Generic;
using System.Text;
using BrutalAPI;

namespace A_Apocrypha.CustomEffects
{
    public class ConsumeAllContainsColorManaEffect : EffectSO
    {
        public ManaColorSO _consumeMana;

        public bool _includeGenerator;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            List<ManaColorSO> manaList = new List<ManaColorSO>();
            foreach (ManaBarSlot manaSlot in stats.MainManaBar.ManaBarSlots)
            {
                UnityEngine.Debug.Log($"color: {manaSlot?.ManaColor}");
                if (manaSlot.ManaColor != null)
                {
                    if (manaSlot.ManaColor.SharesPigmentColor(_consumeMana) && !manaList.Contains(manaSlot.ManaColor))
                    {
                        manaList.Add(manaSlot.ManaColor);
                    }
                }
            }
            foreach (ManaColorSO igment in manaList) { Debug.Log($"pigment list | {igment.name}"); }

            JumpAnimationInformation jumpInfo = stats.GenerateUnitJumpInformation(caster.ID, caster.IsUnitCharacter);
            string manaConsumedSound = stats.audioController.manaConsumedSound;
            exitAmount = 0;
            foreach (ManaColorSO color in manaList)
            {
                Debug.Log($"pigment list | processing {color.name}");
                exitAmount += stats.MainManaBar.ConsumeAllManaColor(color, jumpInfo, manaConsumedSound);
                if (_includeGenerator)
                {
                    exitAmount += stats.YellowManaBar.ConsumeAllManaColor(color, jumpInfo, manaConsumedSound);
                }
            }

            return exitAmount > 0;
        }
    }
}
