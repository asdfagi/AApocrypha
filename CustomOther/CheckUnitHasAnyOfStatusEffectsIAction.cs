using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomOther
{
    public class CheckUnitHasAnyOfStatusEffectsIAction : IImmediateAction
    {
        public BooleanReference _result;

        public string[] _StatusIDs;

        public IUnit _target;

        public CheckUnitHasAnyOfStatusEffectsIAction(BooleanReference result, string[] statusIDs, IUnit target)
        {
            _result = result;
            _StatusIDs = statusIDs;
            _target = target;
        }

        public void Execute(CombatStats stats)
        {
            foreach (string statusID in _StatusIDs)
            {
                if (_target.ContainsStatusEffect(statusID))
                {
                    _result.value = true;
                    return;
                }
            }

            _result.value = false;
        }
    }
}
