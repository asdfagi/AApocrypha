global using A_Apocrypha.Animations;
global using A_Apocrypha.Custom_Passives;
global using A_Apocrypha.CustomEffects;
global using A_Apocrypha.Encounters;
global using A_Apocrypha.Enemies;
global using A_Apocrypha.Fools;
global using BepInEx;
global using BrutalAPI;
global using UnityEngine;
using System.Collections.Generic;
using A_Apocrypha.CustomOther;
using A_Apocrypha.CustomStatusField;
using A_Apocrypha.Items;
using BepInEx.Bootstrap;
using HarmonyLib;

namespace A_Apocrypha
{
    [BepInPlugin("asdfagi.A_Apocrypha", "asdfagi's Abominable Apocrypha", "0.2.0")]
    [BepInDependency("BrutalOrchestra.BrutalAPI", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("Tairbaz.ColophonConundrum", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("TairbazPeep.EnemyPack", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("AnimatedGlitch.GlitchsFreaks", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("000.saltenemies", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("Dui_Mauris_Football.Hell_Island_Fell", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("millieamp.intoTheAbyss", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("Stew.STEWS_SPECIMENS", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("Devron.UnluckyGuys", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("AnimatedGlitch.NumerousLads", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("embercoral.embercoralsMonsterMixtape", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("AnimatedGlitch.Siren", BepInDependency.DependencyFlags.SoftDependency)]
    //[BepInDependency("Marmo.Sasha", BepInDependency.DependencyFlags.SoftDependency)]

    public class AApocrypha : BaseUnityPlugin
    {
        public static AssetBundle assetBundle;
        public static class CrossMod
        {
            public static bool Colophons = false;
            public static bool EnemyPack = false;
            public static bool GlitchsFreaks = false;
            public static bool SaltEnemies = false;
            public static bool HellIslandFell = false;
            public static bool IntoTheAbyss = false;
            public static bool StewSpecimens = false;
            public static bool Siren = false;
            public static bool pigmentGilded = false;
            public static bool pigmentRainbow = false;
            public static bool pigmentPeppermint = false;
            public static bool pigmentPink = false;
            public static void Check()
            {
                foreach (var plugin in Chainloader.PluginInfos)
                {
                    var metadata = plugin.Value.Metadata;

                    if (metadata.GUID == "Tairbaz.ColophonConundrum") { Colophons = true; }
                    if (metadata.GUID == "TairbazPeep.EnemyPack") { EnemyPack = true; }
                    if (metadata.GUID == "AnimatedGlitch.GlitchsFreaks") { GlitchsFreaks = true; }
                    if (metadata.GUID == "000.saltenemies") { SaltEnemies = true; }
                    if (metadata.GUID == "Dui_Mauris_Football.Hell_Island_Fell") { HellIslandFell = true; }
                    if (metadata.GUID == "millieamp.intoTheAbyss") { IntoTheAbyss = true; }
                    if (metadata.GUID == "Stew.STEWS_SPECIMENS") { StewSpecimens = true; }
                    if (metadata.GUID == "AnimatedGlitch.Siren") { Siren = true; }
                    if (metadata.GUID == "AnimatedGlitch.NumerousLads") { pigmentGilded = true; }
                    if (metadata.GUID == "Devron.UnluckyGuys") { pigmentRainbow = true; }
                    if (metadata.GUID == "embercoral.embercoralsMonsterMixtape") { pigmentPeppermint = true; }
                    //if (metadata.GUID == "Marmo.Sasha") { pigmentPink = true; }
                }
                if (Colophons) { Debug.Log("hello colophons"); }
                if (EnemyPack) { Debug.Log("hello packed enemies"); }
                if (GlitchsFreaks) { Debug.Log("hello freaks of glitch"); }
                if (SaltEnemies) { Debug.Log("hello salt enemies"); }
                if (HellIslandFell) { Debug.Log("hello fallen hell island"); }
                if (IntoTheAbyss) { Debug.Log("hello abyss"); }
                if (StewSpecimens) { Debug.Log("hello specimens of stew"); }
                if (Siren) { Debug.Log("hello the siren"); }
                if (pigmentGilded && LoadedDBsHandler.PigmentDB.GetPigment("Gilded") != null)
                {
                    Debug.Log("hello gilded pigment from numerous lads");
                }
                if (pigmentRainbow && LoadedDBsHandler.PigmentDB.GetPigment("Rainbow") != null)
                {
                    Debug.Log("hello rainbow pigment from unlucky guys");
                }
                if (pigmentPeppermint && LoadedDBsHandler.PigmentDB.GetPigment("Peppermint") != null)
                {
                    Debug.Log("hello peppermint pigment from embercoral mixter monstape");
                }
                /*if (pigmentPink && LoadedDBsHandler.PigmentDB.GetPigment("Pink") != null)
                {
                    Debug.Log("hello pink pigment from sasha");
                }*/
            }
        }
        public void Awake()
        {
            Logger.LogInfo("Asdfagi's Abominable Apocrypha activating...");

            var harmony = new Harmony("asdfagi.A_Apocrypha");
            harmony.PatchAll();

            //Asset Bundles
            assetBundle = AssetBundle.LoadFromMemory(ResourceLoader.ResourceBinary("aapocrypha_assetbundle"));

            //Intents
            CustomIntents.Add();
            
            //Crossmod Check
            CrossMod.Check();

            //Custom Animations
            CustomVisuals.Add();

            //Status & Field Effects
            CustomStatus.Add();

            //Passives
            CustomPassives.Add();

            // ITEMS & ACHIEVEMENTS
            // Miniboss Unlocks
            HyperdimensionalPearl.Add();
            // Comedies
            HumanHeart.Add();

            //Characters
            //TestCharacter.Add();
            GnomeCharacter.Add();

            //Enemies
            //Far Shore
            Macerator.Add();
            Acolyte.Add();
            Asterism.Add();
            SandSifter.Add();
            TearDrinker.Add();
            FungusColumn.Add();
            //Orpheum
            UnboundAnomaly.Add();
            EncasedAnomaly.Add();
            SharpenedAnomaly.Add();
            SculptorBird.Add();
            //AnomalyMiniboss.Add();
            Rift.Add();
            Bloatfinger.Add();
            //Garden
            Simulacrum.Add();
            MachineGnomes.Add();
            //Unclassified, Multiple
            CustomSpoggles.Add();
            CustomJumbleGuts.Add();
            Logos.Add();

            //Encounters
            //TestEncounters.Add();
            //Far Shore
            MaceratorEncounters.Add();
            AcolyteFarShoreEncounters.Add();
            AsterismEncounters.Add();
            SandSifterEncounters.Add();
            TearDrinkerEncounters.Add();
            FungusColumnEncounters.Add();
            CompatFarShoreEncounters.Add();
            //Orpheum
            UnboundAnomalyEncounters.Add();
            EncasedAnomalyEncounters.Add();
            SharpenedAnomalyEncounters.Add();
            SculptorBirdEncounters.Add();
            CellularSpoggleEncounters.Add();
            DevotedSpoggleEncounters.Add();
            BloatfingerEncounters.Add();
            if (CrossMod.pigmentRainbow)
            {
                CoruscatingJumbleGutsEncounters.Add();
            }
            CompatOrpheumEncounters.Add();
            //Siren
            if (CrossMod.Siren)
            {
                SculptorBirdSirenEncounters.Add();
                CompatSirenEncounters.Add();
            }
            //Garden
            SimulacrumEncounters.Add();
            MachineGnomesEncounters.Add();
            RedLogosEncounters.Add();
            CompatGardenEncounters.Add();
            //Minibosses
            RiftEncounters.Add();
            //AnomalyMinibossEncounters.Add();

            Logger.LogInfo("Asdfagi's Abominable Apocrypha activated.");
        }
    }
}
