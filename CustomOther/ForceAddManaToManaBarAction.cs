using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomOther
{
    public class ForceAddManaToManaBarAction : IImmediateAction
    {
        public ManaColorSO _mana;

        public int _amount;

        public bool _isGeneratorCharacter;

        public int _id;

        public ForceAddManaToManaBarAction(ManaColorSO mana, int amount, bool isGeneratorCharacter, int id)
        {
            _mana = mana;
            _amount = amount;
            _isGeneratorCharacter = isGeneratorCharacter;
            _id = id;
        }

        public void Execute(CombatStats stats)
        {
            if (_mana != null)
            {
                JumpAnimationInformation jumpInfo = stats.GenerateUnitJumpInformation(_id, _isGeneratorCharacter);
                stats.MainManaBar.AddManaAmount(_mana, _amount, jumpInfo);
            }
        }
    }
}
