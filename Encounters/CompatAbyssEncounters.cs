using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Encounters
{
    public class CompatAbyssEncounters
    {
        public static void Add()
        {
            if (Abyss.Exists)
            {
                Debug.Log("AA Compat Encounters | Abyss Compat Loaded");
                AddTo abyssAdd = new AddTo(Abyss.H.YesMan.Med);
                abyssAdd.SimpleAddGroup(1, "YesMan_EN", 1, "MachineGnomes_EN");
                abyssAdd.SimpleAddGroup(1, "YesMan_EN", 1, "MachineGnomes_EN", 1, "Streetlight_EN");
            } 
        }
    }
}
