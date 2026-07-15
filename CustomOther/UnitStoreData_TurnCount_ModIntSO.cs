using System;
using System.Collections.Generic;
using System.Text;
using Tools;

namespace A_Apocrypha.CustomOther
{
    public class UnitStoreData_TurnCount_ModIntSO : UnitStoreData_BasicSO
    {
        public string m_LocID = "";

        public string m_Text = "";

        public Color m_TextColor = Color.black;

        public int m_CompareDataToThis;

        public bool m_ShowIfDataIsOver = true;

        public override bool TryGetUnitStoreDataToolTip(UnitStoreDataHolder holder, out string result)
        {
            bool flag = (m_ShowIfDataIsOver ? (holder.m_MainData > m_CompareDataToThis) : (holder.m_MainData < m_CompareDataToThis));
            result = (flag ? GenerateString(holder.m_MainData) : "");
            return flag;
        }

        public string GenerateString(int value)
        {
            string text = m_Text;
            bool isPlayerTurn = CombatManager.Instance._stats.IsPlayerTurn;
            int turnCount = CombatManager.Instance._stats.TurnsPassed;
            int adjustedCount = isPlayerTurn ? turnCount + 1 : turnCount;
            string text2 = string.Format(text, adjustedCount);
            string text2er = "!";
            string text3 = ColorUtility.ToHtmlStringRGB(m_TextColor);
            string text4 = "<color=#" + text3 + ">";
            string text5 = "</color>";
            return adjustedCount >= value ? text4 + text2 + text2er + text5 : text4 + text2 + text5;
        }
    }
}
