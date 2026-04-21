using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomOther
{
    public class CustomPigments
    {
        public static void Add()
        {
            // Broken Pigment - junk pigment that breaks when overflow is triggered, credits to WolfaCola
            if (!LoadedDBsHandler.PigmentDB._PigmentPool.ContainsKey("Broken"))
            {
                Debug.Log("Pigments | No pigment with ID [Broken] found! Creating...");
                ManaColorSO brokenPigment = ScriptableObject.CreateInstance<ManaColorSO>();
                brokenPigment.canGenerateMana = true;
                brokenPigment.dealsCostDamage = true;
                brokenPigment.pigmentID = "Broken";
                brokenPigment.manaSprite = ResourceLoader.LoadSprite("BrokenMana", null, 32, null);
                brokenPigment.manaUsedSprite = ResourceLoader.LoadSprite("BrokenManaUsed", null, 32, null);
                brokenPigment.manaCostSelectedSprite = ResourceLoader.LoadSprite("BrokenManaCostSelected", null, 32, null);
                brokenPigment.manaCostSprite = ResourceLoader.LoadSprite("BrokenManaCostUnselected", null, 32, null);
                brokenPigment.manaSoundEvent = "event:/AASFX/BrokenPigmentGen";
                brokenPigment.healthSprite = ResourceLoader.LoadSprite("BrokenManaHealth", null, 32, null);
                brokenPigment.pigmentTypes =
                [
                    "Broken"
                ];
                Pigments.AddNewPigment("Broken", brokenPigment);
            }
            else
            {
                Debug.Log("Pigments | Pigment with ID [Broken] is already loaded!");
            }
        }
    }
}
