using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomOther
{
    public class CasterStoredValueComparatorEffectorCondition : EffectorConditionSO
    {
        public string m_unitStoredDataID = "";
        public int _comparator;
        public bool _lessThan = false;
        public override bool MeetCondition(IEffectorChecks effector, object args)
        {
            if (effector is IUnit caster)
            {
                int value = caster.SimpleGetStoredValue(m_unitStoredDataID);
                if (_lessThan)
                {
                    return value < _comparator;
                }
                else
                {
                    return value >= _comparator;
                }
            }
            return false;
        }
    }
}
