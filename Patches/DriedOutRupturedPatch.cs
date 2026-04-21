using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;

namespace A_Apocrypha.Patches
{
    [HarmonyPatch]
    public class DriedOutRupturedPatch
    {
        [HarmonyPatch(typeof(RupturedSE_SO), nameof(OnFireFE_SO.OnEventCall_01))]
        [HarmonyPrefix]
        public static bool EarlyReturnIfDriedOutPatch(FieldEffect_Holder holder, object sender, object args)
        {
            IUnit unit = sender as IUnit;
            if (unit.ContainsPassiveAbility("DriedOut"))
            {
                Debug.Log("Dried Out Ruptured Blocker Patch | Dried Out detected! interrupting action early");
                return false;
            }
            return true;
        }
    }
}
