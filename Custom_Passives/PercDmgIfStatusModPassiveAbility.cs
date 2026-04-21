using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Custom_Passives
{
    public class PercDmgIfStatusModPassiveAbility : PercDmgModPassiveAbility
    {
        public string _statusID;

        public override void TriggerPassive(object sender, object args)
        {
            int finalPercentage = 0;

            if (args is DamageDealtValueChangeException context)
            {
                if (context.damagedUnit.ContainsStatusEffect(_statusID))
                {
                    finalPercentage = _percentageToModify;
                    //Debug.Log("BoostIfStatus | status found");
                }
                else
                {
                    finalPercentage = 0;
                    //Debug.Log("BoostIfStatus | status not found");
                }
            }

            //Debug.Log("BoostIfStatus | finalPercentage: " + finalPercentage);
            if (finalPercentage > 0)
            {
                if (_useSimpleInt)
                {
                    if (args is IntValueChangeException ex && !ex.Equals(null))
                    {
                        //Debug.Log("BoostIfStatus | finalPercentage: " + finalPercentage);
                        ex.AddModifier(new PercentageValueModifier(dmgDealt: false, finalPercentage, _doesIncrease));
                    }
                }
                else if (_useDealt)
                {
                    if (args is DamageDealtValueChangeException ex2 && !ex2.Equals(null))
                    {
                        //Debug.Log("BoostIfStatus | finalPercentage: " + finalPercentage);
                        ex2.AddModifier(new PercentageValueModifier(dmgDealt: true, finalPercentage, _doesIncrease));
                    }
                }
                else if (args is DamageReceivedValueChangeException ex3 && !ex3.Equals(null))
                {
                    //Debug.Log("BoostIfStatus | finalPercentage: " + finalPercentage);
                    ex3.AddModifier(new PercentageValueModifier(dmgDealt: false, finalPercentage, _doesIncrease));
                }
            }
        }
    }
}
