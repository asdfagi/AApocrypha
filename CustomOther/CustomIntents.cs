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

            IntentInfoBasic ColophonPigmentIntent = new()
            {
                _color = Color.white,
                _sprite = ResourceLoader.LoadSprite("IconColoIntent"),
            };
            LoadedDBsHandler.IntentDB.AddNewBasicIntent("AA_Pigment_Transform", ColophonPigmentIntent);
        }
    }
}
