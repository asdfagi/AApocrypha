using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomOther
{
    public class UnitStoreData_CombatAbilitySO : UnitStoreData_BasicSO
    {
        public string m_LocID = "";

        public string m_Text = "";

        public Color m_TextColor = Color.black;

        public override bool TryGetUnitStoreDataToolTip(UnitStoreDataHolder holder, out string result)
        {
            bool flag = holder.m_ObjectData is CombatAbility;
            if (flag)
            {
                result = GenerateString(holder.m_ObjectData as CombatAbility);
            } else
            {
                result = "Unknown";
            }
            return flag;
        }

        public string GenerateString(CombatAbility ability)
        {
            string text = m_Text;
            string name = ability.ability._abilityName;
            string text2 = string.Format(text, name);
            string text3 = ColorUtility.ToHtmlStringRGB(m_TextColor);
            string text4 = "<color=#" + text3 + ">";
            string text5 = "</color>";
            return text4 + text2 + text5;
        }
    }
}
