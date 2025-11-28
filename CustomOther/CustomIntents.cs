using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomOther
{
    public class CustomIntents
    {
        public static void Add()
        {
            IntentInfoBasic CopyThatIntent = new()
            {
                _color = Color.white,
                _sprite = ResourceLoader.LoadSprite("IconCopyThat"),
            };
            LoadedDBsHandler.IntentDB.AddNewBasicIntent("Passive_CopyThat", CopyThatIntent);

            IntentInfoBasic RemCopyThatIntent = new()
            {
                _color = Color.grey,
                _sprite = ResourceLoader.LoadSprite("IconCopyThat"),
            };
            LoadedDBsHandler.IntentDB.AddNewBasicIntent("Rem_Passive_CopyThat", RemCopyThatIntent);

            IntentInfoBasic RemConfusionIntent = new()
            {
                _color = Color.grey,
                _sprite = Passives.Confusion.passiveIcon,
            };
            LoadedDBsHandler.IntentDB.AddNewBasicIntent("Rem_Passive_Confusion", RemConfusionIntent);

            IntentInfoBasic RemSkittishIntent = new()
            {
                _color = Color.grey,
                _sprite = Passives.Skittish.passiveIcon,
            };
            LoadedDBsHandler.IntentDB.AddNewBasicIntent("Rem_Passive_Skittish", RemSkittishIntent);
            // Tairbaz
            IntentInfoBasic ColophonPigmentIntent = new()
            {
                _color = Color.white,
                _sprite = ResourceLoader.LoadSprite("IconColoIntent"),
            };
            LoadedDBsHandler.IntentDB.AddNewBasicIntent("AA_Pigment_Transform", ColophonPigmentIntent);

            IntentInfoBasic MultiIntent = new()
            {
                _color = Color.white,
                _sprite = ResourceLoader.LoadSprite("IconMultIntent"),
            };
            LoadedDBsHandler.IntentDB.AddNewBasicIntent("AA_Multi", MultiIntent);
            // MillieAmp
            IntentInfoBasic Multi2Intent = new()
            {
                _color = Color.white,
                _sprite = ResourceLoader.LoadSprite("intent_x2"),
            };
            LoadedDBsHandler.IntentDB.AddNewBasicIntent("AA_Multi2", Multi2Intent);
            // MillieAmp
            IntentInfoBasic Multi3Intent = new()
            {
                _color = Color.white,
                _sprite = ResourceLoader.LoadSprite("intent_x3"),
            };
            LoadedDBsHandler.IntentDB.AddNewBasicIntent("AA_Multi3", Multi3Intent);
            // MillieAmp
            IntentInfoBasic Multi4Intent = new()
            {
                _color = Color.white,
                _sprite = ResourceLoader.LoadSprite("intent_x4"),
            };
            LoadedDBsHandler.IntentDB.AddNewBasicIntent("AA_Multi4", Multi4Intent);
            // MillieAmp
            IntentInfoBasic Multi5Intent = new()
            {
                _color = Color.white,
                _sprite = ResourceLoader.LoadSprite("intent_x5"),
            };
            LoadedDBsHandler.IntentDB.AddNewBasicIntent("AA_Multi5", Multi5Intent);
            // MillieAmp
            IntentInfoBasic NothingIntent = new()
            {
                _color = Color.white,
                _sprite = ResourceLoader.LoadSprite("doNothing_intent"),
            };
            LoadedDBsHandler.IntentDB.AddNewBasicIntent("AA_Nothing", NothingIntent);
        }
    }
}
