using System;
using System.Collections.Generic;
using System.Text;
using static A_Apocrypha.Encounters.Orph.H;

namespace A_Apocrypha.Encounters
{
    public class CompatOrpheumEncounters
    {
        public static void Add()
        {
            Debug.Log("AA Compat Encounters | Orpheum Compat Loaded");
            /*}
            public static void Post()
            {*/
            AddTo orphAdd = new AddTo(Orph.H.MusicMan.Easy);
            orphAdd.SimpleAddGroup(1, "MusicMan_EN", 1, "Acolyte_EN");

            orphAdd = new AddTo(Orph.H.Scrungie.Med);
            orphAdd.SimpleAddGroup(2, "Scrungie_EN", 1, HiddenBloatfinger.OrpheumRandom);
            orphAdd.SimpleAddGroup(2, "Scrungie_EN", 1, HiddenBloatfinger.OrpheumRandom);
            orphAdd.SimpleAddGroup(2, "Scrungie_EN", 1, "MusicMan_EN", 1, HiddenBloatfinger.OrpheumRandom);

            if (AApocrypha.CrossMod.pigmentRainbow)
            {
                orphAdd = new AddTo(Orph.H.Scrungie.Med);
                orphAdd.SimpleAddGroup(2, "Scrungie_EN", 1, Jumble.Rainbow, 1, Jumble.Red);
            }
            if (AApocrypha.CrossMod.GlitchsFreaks)
            {
                orphAdd = new AddTo(Orph.H.Frostbite.Med);
                orphAdd.SimpleAddGroup(3, Frostbites.Normal, 1, HiddenBloatfinger.OrpheumRandom);
                orphAdd.SimpleAddGroup(3, Frostbites.Normal, 1, HiddenBloatfinger.OrpheumRandom);
                orphAdd.SimpleAddGroup(2, Frostbites.Normal, 2, "Blemmigan_EN");
            }
            if (AApocrypha.CrossMod.SaltEnemies)
            {
                orphAdd = new AddTo(Orph.H.Maw.Med);
                orphAdd.SimpleAddGroup(1, "Maw_EN", 2, "Acolyte_EN");
                orphAdd.SimpleAddGroup(1, "Maw_EN", 1, "MusicMan_EN", 1, Spoggle.PurpleRedSplit);
                orphAdd.SimpleAddGroup(1, "Maw_EN", 1, "MusicMan_EN", 1, Spoggle.YellowBlueSplit);
            }
            if (AApocrypha.CrossMod.Mythos)
            {
                orphAdd = new AddTo("StarVampireMedium");
                orphAdd.SimpleAddGroup(1, "StarVampire_EN", 1, "MusicMan_EN", 1, "SingingStone_EN", 1, HiddenBloatfinger.OrpheumRandom);
                orphAdd.SimpleAddGroup(1, "StarVampire_EN", 1, "MusicMan_EN", 1, "SingingStone_EN", 1, HiddenBloatfinger.OrpheumRandom);
                orphAdd.SimpleAddGroup(1, "StarVampire_EN", 2, "MusicMan_EN", 1, HiddenBloatfinger.OrpheumRandom);
                orphAdd.SimpleAddGroup(1, "StarVampire_EN", 1, "MusicMan_EN", 2, "Blemmigan_EN");
            }
            if (AApocrypha.CrossMod.BismuthBoiler)
            {
                orphAdd = new AddTo(Orph.H.Feaster.Med);
                orphAdd.SimpleAddGroup(3, "FerrousFeaster_EN", 1, HiddenBloatfinger.OrpheumRandom);
                orphAdd.SimpleAddGroup(3, "FerrousFeaster_EN", 1, HiddenBloatfinger.OrpheumRandom);
                orphAdd.SimpleAddGroup(2, "FerrousFeaster_EN", 1, "AluminumAlchemist_EN", 1, Spoggle.BlueYellowSplit);
                orphAdd.SimpleAddGroup(2, "FerrousFeaster_EN", 1, "ArgonAccelerator_EN", 1, Spoggle.PurpleRedSplit);
            }
        }
    }
}
