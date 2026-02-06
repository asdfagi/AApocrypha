using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Fools
{
    internal class AnnaMollyTrackData : UnlockTrack_Data
    {
        public override string GetDescription(GameInformationHolder holder)
        {
            return "Unlock this party member by banishing a nexus of anomalous power.";
        }

        public override bool IsTrackDataAvailable(GameInformationHolder holder)
        {
            return true;
        }
    }
}
