using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Encounters
{
    public class CompatFarShoreEncounters
    {
        public static void Add()
        {
            Debug.Log("AA Compat Encounters | Far Shore Compat Loaded");
            /*}
            public static void Post()
            {*/
            AddTo shoreAdd = new AddTo(Shore.H.MudLung.Easy);
            shoreAdd.SimpleAddGroup(1, "MudLung_EN", 1, "SandSifter_EN");

            shoreAdd = new AddTo(Shore.H.Flarb.Hard);
            shoreAdd.SimpleAddGroup(1, "Flarb_EN", 1, "SandSifter_EN");
            shoreAdd.SimpleAddGroup(1, "Flarb_EN", 1, "TearDrinker_EN", 1, "Flarblet_EN");
            shoreAdd.SimpleAddGroup(1, "Flarb_EN", 1, "Gammamite_EN");
            shoreAdd.SimpleAddGroup(1, "Flarb_EN", 1, Aggregates.Red);

            shoreAdd = new AddTo(Shore.H.FlaMinGoa.Med);
            shoreAdd.SimpleAddGroup(1, "FlaMinGoa_EN", 1, "MudLung_EN", 1, "FungusColumn_EN");
            shoreAdd.SimpleAddGroup(1, "FlaMinGoa_EN", 1, Enemies.Mungling, 1, "FungusColumn_EN");
            shoreAdd.SimpleAddGroup(1, "FlaMinGoa_EN", 1, "MudLung_EN", 1, "SandSifter_EN");
            shoreAdd.SimpleAddGroup(1, "FlaMinGoa_EN", 1, "Wringle_EN", 1, "SandSifter_EN");
            shoreAdd.SimpleAddGroup(1, "FlaMinGoa_EN", 1, Enemies.Mungling, 1, Aggregates.Red);
            shoreAdd.SimpleAddGroup(1, "FlaMinGoa_EN", 1, "MudLung_EN", 1, Aggregates.Purple);

            if (AApocrypha.CrossMod.HellIslandFell)
            {
                shoreAdd = new AddTo("H_Zone01_Draugr_Easy_EnemyBundle");
                shoreAdd.SimpleAddGroup(1, "Draugr_EN", 1, "FungusColumn_EN");
                shoreAdd.SimpleAddGroup(1, "Draugr_EN", 1, "SandSifter_EN");
            }
            if (AApocrypha.CrossMod.SaltEnemies)
            {
                shoreAdd = new AddTo(Shore.H.Ufo.Med);
                shoreAdd.SimpleAddGroup(1, "ToyUfo_EN", 1, Jumble.Yellow, 1, "SandSifter_EN");
                shoreAdd.SimpleAddGroup(1, "ToyUfo_EN", 1, "MudLung_EN", 1, "FungusColumn_EN");

                shoreAdd = new AddTo(Shore.H.Grave.Easy);
                shoreAdd.SimpleAddGroup(1, "NobodyGrave_EN", 1, "SandSifter_EN");
                shoreAdd.SimpleAddGroup(1, "NobodyGrave_EN", 1, "ToyUfo_EN", 1, "SandSifter_EN");
                shoreAdd.SimpleAddGroup(1, "NobodyGrave_EN", 2, "Acolyte_EN");
                shoreAdd.SimpleAddGroup(1, "NobodyGrave_EN", 1, Aggregates.Red);
                shoreAdd.SimpleAddGroup(1, "NobodyGrave_EN", 1, Aggregates.Purple);

                /*if (LoadedAssetsHandler.LoadEnemyBundle(Shore.H.Jabber.Med) != null)
                {
                    Debug.Log("yeay jaberwok");
                    shoreAdd = new AddTo(Shore.H.Jabber.Med);
                    shoreAdd.SimpleAddGroup(2, "Jabberwocky_EN", 1, "SandSifter_EN");
                }*/ //doesn't work
            }
            /*if (AApocrypha.CrossMod.StewSpecimens)
            {
                List<RandomEnemyGroup> bardMedium = new List<RandomEnemyGroup>(((RandomEnemyBundleSO)LoadedAssetsHandler.GetEnemyBundle("H_Zone01_Bard_Med_EnemyBundle"))._enemyBundles)
                {
                    new([
                       "TravellingBard_S_EN",
                       "TearDrinker_EN",
                       "TearDrinker_EN",
                    ]),
                };
                ((RandomEnemyBundleSO)LoadedAssetsHandler.GetEnemyBundle("H_Zone01_Bard_Med_EnemyBundle"))._enemyBundles = bardMedium;
            }*/

            if (AApocrypha.CrossMod.LeonLegion)
            {
                shoreAdd = new AddTo(Shore.H.Martian.Purple.Med);
                shoreAdd.SimpleAddGroup(1, Martians.Purple, 1, "SandSifter_EN");
                shoreAdd.SimpleAddGroup(1, Martians.Purple, 1, "FungusColumn_EN", 1, "Mung_EN");
                shoreAdd.SimpleAddGroup(1, Martians.Purple, 1, "Acolyte_EN");

                shoreAdd = new AddTo(Shore.H.Martian.Blue.Med);
                shoreAdd.SimpleAddGroup(1, Martians.Blue, 1, "SandSifter_EN");
                shoreAdd.SimpleAddGroup(1, Martians.Blue, 1, "MudLung_EN", 1, Aggregates.Purple);

                shoreAdd = new AddTo(Shore.H.Keklets.Med);
                shoreAdd.SimpleAddGroup(1, "Keklets_EN", 1, "Keko_EN", 1, "FungusColumn_EN");
            }
        }
    }
}
