using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Encounters
{
    public class BlemmiganEncounters
    {
        public static void Add()
        {
            // do not combine silver suckles and blemmigans - they are mortal enemies
            Portals.AddPortalSign("Uttershroom_Sign", ResourceLoader.LoadSprite("UttershroomPortal", new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);
            EnemyEncounter_API uttershroomMedium = new EnemyEncounter_API(0, Orph.H.Uttershroom.Med, "Uttershroom_Sign")
            {
                MusicEvent = "event:/AAMusic/FallenLondon/KhansHeart",
                RoarEvent = LoadedAssetsHandler.GetEnemyBundle(Garden.H.Sepulchre.Hard)._roarReference.roarEvent,
            };
            uttershroomMedium.SimpleAddEncounter(1, "UttershroomSpore_EN", 2, "Blemmigan_EN", 1, "SingingStone_EN");
            uttershroomMedium.SimpleAddEncounter(1, "UttershroomSpore_EN", 1, "Blemmigan_EN", 1, "MusicMan_EN");
            uttershroomMedium.SimpleAddEncounter(1, "UttershroomSpore_EN", 1, "Blemmigan_EN", 1, "Scrungie_EN");
            uttershroomMedium.SimpleAddEncounter(1, "UttershroomSpore_EN", 1, "Blemmigan_EN", 1, "MusicMan_EN", 1, HiddenBloatfinger.OrpheumRandom);
            uttershroomMedium.SimpleAddEncounter(1, "UttershroomSpore_EN", 1, "Blemmigan_EN", 1, "Scrungie_EN", 1, HiddenBloatfinger.OrpheumRandom);
            if (AApocrypha.CrossMod.EnemyPack)
            {
                uttershroomMedium.SimpleAddEncounter(1, "UttershroomSpore_EN", 1, "Blemmigan_EN", 1, "Neoplasm_EN");
                uttershroomMedium.SimpleAddEncounter(1, "UttershroomSpore_EN", 1, "Blemmigan_EN", 1, "Gizo_EN");
            }
            if (AApocrypha.CrossMod.MarmoEnemies)
            {
                uttershroomMedium.SimpleAddEncounter(1, "UttershroomSpore_EN", 2, "Blemmigan_EN", 1, "Gungrot_EN");
                uttershroomMedium.SimpleAddEncounter(1, "UttershroomSpore_EN", 1, "Blemmigan_EN", 1, Jumble.Unstable);
            }
            if (AApocrypha.CrossMod.GlitchsFreaks)
            {
                uttershroomMedium.SimpleAddEncounter(1, "UttershroomSpore_EN", 1, "Blemmigan_EN", 2, "Frostbite_EN");
            }
            if (AApocrypha.CrossMod.SaltEnemies)
            {
                uttershroomMedium.SimpleAddEncounter(1, "UttershroomSpore_EN", 2, "Blemmigan_EN", 1, Bots.Red);
                uttershroomMedium.SimpleAddEncounter(1, "UttershroomSpore_EN", 1, "Blemmigan_EN", 1, "Something_EN");
            }
            uttershroomMedium.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector(Orph.H.Uttershroom.Med, 12, ZoneType_GameIDs.Orpheum_Hard, BundleDifficulty.Medium); //12
        }
    }
}
