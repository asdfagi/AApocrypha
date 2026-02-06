using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Encounters
{
    public class CompatGardenEncounters
    {
        public static void Add()
        {
            Debug.Log("AA Compat Encounters | Garden Compat Loaded");
            /*}
            public static void Post()
            {*/
            AddTo gardenAdd = new AddTo(Garden.H.Minister.Med);
            gardenAdd.SimpleAddGroup(1, Enemies.Minister, 2, "MachineGnomes_EN");
            if (AApocrypha.CrossMod.GlitchsFreaks)
            {
                gardenAdd = new AddTo("ChaliceMed");
                gardenAdd.SimpleAddGroup(1, "GodsChalice_EN", 2, "MachineGnomes_EN");
            }
            if (AApocrypha.CrossMod.EnemyPack)
            {
                gardenAdd = new AddTo("MetatronHard");
                gardenAdd.SimpleAddGroup(1, "Metatron_EN", 2, "MachineGnomes_EN");
                
                gardenAdd = new AddTo("PsychopompHard");
                gardenAdd.SimpleAddGroup(1, "Psychopomp_EN", 2, "MachineGnomes_EN");
            }
            if (AApocrypha.CrossMod.IntoTheAbyss)
            {
                gardenAdd = new AddTo("H_Zone03_Tanehineri_Easy_EnemyBundle");
                gardenAdd.SimpleAddGroup(2, "Tanehineri_EN", 2, "MachineGnomes_EN");

                gardenAdd = new AddTo(Garden.H.WRK.Med);
                gardenAdd.SimpleAddGroup(1, "WRK_EN", 2, "MachineGnomes_EN");
                gardenAdd.SimpleAddGroup(2, "WRK_EN", 2, "MachineGnomes_EN");
                gardenAdd.SimpleAddGroup(1, "Tanehineri_EN", 1, "MachineGnomes_EN", 1, "SomeoneSister_EN");
                
                gardenAdd = new AddTo(Garden.H.Kcolclock.Hard);
                gardenAdd.SimpleAddGroup(1, "Kcolclock_EN", 1, Logos.Red);
                gardenAdd.SimpleAddGroup(1, "Kcolclock_EN", 1, Logos.Blue);
                gardenAdd.SimpleAddGroup(1, "Kcolclock_EN", 1, "SomeoneSister_EN", 1, "NooneSister_EN");
                
                gardenAdd = new AddTo("H_Zone03_Butterfly_Medium_EnemyBundle");
                gardenAdd.SimpleAddGroup(2, "ButterflyEffect_EN", 1, "SomeoneSister_EN");
                gardenAdd.SimpleAddGroup(2, "ButterflyEffect_EN", 2, "MachineGnomes_EN");
                
                gardenAdd = new AddTo("H_Zone03_Plato_Hard_EnemyBundle");
                gardenAdd.SimpleAddGroup(1, "Plato_EN", 1, Logos.Purple);
            }
            if (AApocrypha.CrossMod.SaltEnemies)
            {
                gardenAdd = new AddTo(Garden.H.EvilDog.Med);
                gardenAdd.SimpleAddGroup(2, "EvilDog_EN", 1, "SomeoneSister_EN");
                gardenAdd.SimpleAddGroup(3, "EvilDog_EN", 1, "SomeoneSister_EN");

                gardenAdd = new AddTo(Garden.H.Stoplight.Med);
                gardenAdd.SimpleAddGroup(1, "Stoplight_EN", 2, "SomeoneSister_EN");
                gardenAdd.SimpleAddGroup(1, "Stoplight_EN", 2, "MachineGnomes_EN");

                gardenAdd = new AddTo(Garden.H.Satyr.Med);
                gardenAdd.SimpleAddGroup(1, "Satyr_EN", 2, "MachineGnomes_EN");
            }
            if (AApocrypha.CrossMod.StewSpecimens) 
            {
                /*List<RandomEnemyGroup> bardMedium = new List<RandomEnemyGroup>(((RandomEnemyBundleSO)LoadedAssetsHandler.GetEnemyBundle("H_Zone03_Bard_Medium_EnemyBundle"))._enemyBundles)
                {
                    new([
                        "TravellingBard_G_EN",
                        "MachineGnomes_EN",
                        "MachineGnomes_EN",
                    ]),
                };
                ((RandomEnemyBundleSO)LoadedAssetsHandler.GetEnemyBundle("H_Zone03_Bard_Medium_EnemyBundle"))._enemyBundles = bardMedium;*/
            }
        }
    }
}
