using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;

namespace A_Apocrypha.Patches
{
    /*[HarmonyPatch]
    public class GravityBlockerPatch
    {
        [HarmonyPatch(typeof(OnFireFE_SO), nameof(OnFireFE_SO.OnEventCall_03))]
        [HarmonyPrefix]
        public static bool EarlyReturnIfFixedPointPatch(FieldEffect_Holder holder, object sender, object args)
        {
            IUnit unit = sender as IUnit;
            if (unit.ContainsPassiveAbility("MadeOfFire"))
            {
                Debug.Log("Fire Blocker Patch | Made Of Fire detected! interrupting action early");
                return false;
            }
            return true;
        }
    }*/
}
