using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomOther
{
    public class GameInt_GenericCondition : GenericDataConditionSO
    {
        public string m_IntID;

        public bool _PassIfTrue = true;

        public int _operand = 0; //0 is ==, 1 is >=, 2 is <

        public int _comparator = 0;

        public override bool MeetsGameCondition(IGameCheckData game, IInGameRunMinimalData run)
        {
            int data = game.GetIntData(m_IntID);
            switch(_operand)
            {
                case 0:
                    return _PassIfTrue == (data == _comparator);
                case 1:
                    return _PassIfTrue == (data >= _comparator);
                case 2:
                    return _PassIfTrue == (data < _comparator);
                default:
                    return false;
            }
        }

        public static GameInt_GenericCondition GenerateIntCondition(string id, bool passTrue, int operand, int comparator)
        {
            GameInt_GenericCondition output = ScriptableObject.CreateInstance<GameInt_GenericCondition>();
            output.m_IntID = id;
            output._PassIfTrue = passTrue;
            output._operand = operand;
            output._comparator = comparator;
            return output;
        }
    }
}
