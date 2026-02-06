using System;
using System.Collections.Generic;
using System.Text;
using A_Apocrypha.CustomOther;

namespace A_Apocrypha.Custom_Passives
{
    public class ConvertIndirectToDirectPassiveAbility : BasePassiveAbilitySO
    {
        public bool _useIntReferenceResult = true;
        public EffectInfo[] effects;
        public override void TriggerPassive(object sender, object args)
        {
            //IPassiveEffector passiveEffector = sender as IPassiveEffector;
            DamageReceivedValueChangeException damage = args as DamageReceivedValueChangeException;
            bool flag = damage != null && !damage.Equals(null);
            if (flag) 
            { 
                if (!damage.directDamage)
                {
                    List<EffectInfo> effects = new List<EffectInfo>(2)
                    {
                        Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), damage.amount, Targeting.Slot_SelfSlot, null),
                    };
                    damage.AddModifier(new ConvertDamageToDirectModifier(effects, damage.damagedUnit));
                }
                else
                {
                    List<EffectInfo> effects = new List<EffectInfo>(1) {};
                    CombatManager.Instance.ProcessImmediateAction(new ImmediateEffectAction(effects.ToArray(), damage.damagedUnit, damage.amount), false);
                }
            }
        }
        public override bool IsPassiveImmediate
        {
            get
            {
                return true;
            }
        }
        public override bool DoesPassiveTrigger
        {
            get
            {
                return true;
            }
        }
        public override void OnPassiveConnected(IUnit unit)
        {
        }
        public override void OnPassiveDisconnected(IUnit unit)
        {
        }
    }
}
