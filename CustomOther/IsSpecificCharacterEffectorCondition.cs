using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomOther
{
    public class IsSpecificCharacterEffectorCondition : EffectorConditionSO
    {
        public bool _passIfTrue;
        public string _character = "";

        public override bool MeetCondition(IEffectorChecks effector, object args)
        {
            if (!effector.IsUnitCharacter) { return !_passIfTrue; }
            CharacterCombat ch = effector as CharacterCombat;
            return _passIfTrue == (ch.Character.entityID == _character);
        }
    }
}
