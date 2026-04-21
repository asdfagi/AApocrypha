using System;
using System.Collections.Generic;
using System.Text;
using Tools;

namespace A_Apocrypha.CustomOther
{
    public class UnitStoreData_AnalysisTooltip_ModIntSO : UnitStoreData_BasicSO
    {
        public string m_LocID = "";

        public string m_Text = "";

        public Color m_TextColor = Color.black;

        public override bool TryGetUnitStoreDataToolTip(UnitStoreDataHolder holder, out string result)
        {
            bool flag = holder.m_MainString != null;
            if (flag)
            {
                result = GenerateString(holder.m_MainString);
            }
            else
            {
                result = "None";
            }
            return flag;
        }

        public string GenerateString(string name)
        {
            string text = m_Text;

            string text2 = string.Format(text, name);
            string text3 = ColorUtility.ToHtmlStringRGB(m_TextColor);
            string text4 = "<color=#" + text3 + ">";
            string text5 = "</color>";
            return text4 + text2 + text5;
        }
    }
}
