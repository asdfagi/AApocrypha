using System;
using System.Collections.Generic;
using System.Text;
using TMPro;

namespace A_Apocrypha.DamageTypes
{
    public class FrostDamage
    {
        public static void Add()
        {
            var damageId = "AA_Frost_Damage";

            LoadedDBsHandler.CombatDB.AddNewSound(damageId, "event:/AASFX/Damage_Frost");
            TMP_ColorGradient frostGradient = ScriptableObject.CreateInstance<TMP_ColorGradient>();
            frostGradient.topLeft = new Color32(177, 225, 235, 255);
            frostGradient.topRight = new Color(140, 180, 188, 255);
            frostGradient.bottomLeft = new Color(140, 180, 188, 255);
            frostGradient.bottomRight = new Color(177, 225, 235, 255);

            LoadedDBsHandler.CombatDB.AddNewTextColor(damageId, frostGradient);
        }
    }
}
