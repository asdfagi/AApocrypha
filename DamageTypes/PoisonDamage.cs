using System;
using System.Collections.Generic;
using System.Text;
using TMPro;

namespace A_Apocrypha.DamageTypes
{
    public class PoisonDamage
    {
        public static void Add()
        {
            var damageId = "AA_Poison_Damage";

            LoadedDBsHandler.CombatDB.AddNewSound(damageId, "event:/Combat/StatusEffects/SE_Ruptured_Trg");
            TMP_ColorGradient poisonGradient = ScriptableObject.CreateInstance<TMP_ColorGradient>();
            poisonGradient.topLeft = new Color(0.33f, 0.5f, 0.08f);
            poisonGradient.topRight = new Color(0.11f, 0.4f, 0.08f);
            poisonGradient.bottomLeft = new Color(0.11f, 0.4f, 0.08f);
            poisonGradient.bottomRight = new Color(0.33f, 0.5f, 0.08f);

            LoadedDBsHandler.CombatDB.AddNewTextColor(damageId, poisonGradient);
        }
    }
}
