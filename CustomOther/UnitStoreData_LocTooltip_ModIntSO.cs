using System;
using System.Collections.Generic;
using System.Text;
using Tools;

namespace A_Apocrypha.CustomOther
{
    public class UnitStoreData_LocTooltip_ModIntSO : UnitStoreData_BasicSO
    {
        public string m_LocID = "";

        public string m_Text = "";

        public Color m_TextColor = Color.black;

        public int m_CompareDataToThis;

        public bool m_ShowIfDataIsOver = true;

        public bool reduceByOne = true;

        public override bool TryGetUnitStoreDataToolTip(UnitStoreDataHolder holder, out string result)
        {
            bool flag = (m_ShowIfDataIsOver ? (holder.m_MainData > m_CompareDataToThis) : (holder.m_MainData < m_CompareDataToThis));
            result = (flag ? GenerateString(holder.m_MainData) : "");
            return flag;
        }

        public string GenerateString(int value)
        {
            string text = m_Text;
            int modValue = value;
            if (reduceByOne) { modValue = modValue - 1; }
            string location = "";
            switch (modValue)
            {
                case 0:
                    location = "Far Left";
                    break;
                case 1:
                    location = "Left";
                    break;
                case 2:
                    location = "Center";
                    break;
                case 3:
                    location = "Right";
                    break;
                case 4:
                    location = "Far Right";
                    break;
                default:
                    location = "Out Of Bounds";
                    break;
            }

            string text2 = string.Format(text, location);
            string text3 = ColorUtility.ToHtmlStringRGB(m_TextColor);
            string text4 = "<color=#" + text3 + ">";
            string text5 = "</color>";
            return text4 + text2 + text5;
        }
    }
}
