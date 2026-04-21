using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomEffects
{
    public class RerollNumberPigmentFromSetColorEffect : EffectSO
    {
        public ManaColorSO _targetMana;
        public List<ManaColorSO> _resultMana = [];
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            //this._mana = caster.HealthColor;
            List<int> list = new List<int>();
            ManaBarSlot[] manaBarSlots = stats.MainManaBar.ManaBarSlots;
            for (int i = 0; i < manaBarSlots.Length; i++)
            {
                ManaBarSlot manaBarSlot = manaBarSlots[i];
                bool flag = manaBarSlot != null && !manaBarSlot.IsEmpty && manaBarSlot.ManaColor == _targetMana;
                bool flag2 = flag;
                if (flag2)
                {
                    list.Add(i);
                }
            }
            List<int> list2 = new List<int>();
            List<ManaColorSO> list3 = new List<ManaColorSO>();
            while (list.Count > 0 && list2.Count < entryVariable)
            {
                int index = UnityEngine.Random.Range(0, list.Count);
                int index2 = UnityEngine.Random.Range(0, _resultMana.Count);
                list2.Add(list[index]);
                stats.MainManaBar.ManaBarSlots[list[index]].SetMana(_resultMana[index2]);
                list.RemoveAt(index);
                list3.Add(_resultMana[index2]);
                exitAmount++;
            }
            bool flag3 = list2.Count > 0;
            bool flag4 = flag3;
            if (flag4)
            {
                CombatManager instance = CombatManager.Instance;
                ManaBarType id = stats.MainManaBar.ID;
                List<int> list4 = new List<int>();
                foreach (int item in list2)
                {
                    list4.Add(item);
                }
                int[] array = list4.ToArray();
                List<ManaColorSO> list5 = new List<ManaColorSO>();
                foreach (ManaColorSO item2 in list3)
                {
                    list5.Add(item2);
                }
                instance.AddUIAction(new ModifyManaSlotsUIAction(id, array, list5.ToArray()));
            }
            return exitAmount > 0;
        }
    }
}
