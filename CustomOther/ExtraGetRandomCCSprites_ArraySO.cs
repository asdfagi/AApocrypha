using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomOther
{
    public class ExtraGetRandomCCSprites_ArraySO : ExtraCharacterCombatSpritesSO
    {
        public Sprite[] _frontSprite;

        public Sprite[] _backSprite;

        public string _DefaultID = "";

        public string _SpecialID = "";

        public bool _doesLoop;

        public override ExtraSpriteOptions GetSpriteOptions(string extraSpriteID)
        {
            if (extraSpriteID == _DefaultID)
            {
                return ExtraSpriteOptions.UseDefault;
            }

            if (extraSpriteID == _SpecialID)
            {
                return ExtraSpriteOptions.UseSpecial;
            }

            return ExtraSpriteOptions.DoNothing;
        }

        public override int TryGetSpecialSprites(int specialID, out Sprite front, out Sprite back)
        {
            int num = Mathf.Min(_frontSprite.Length, _backSprite.Length);
            if (num == 0)
            {
                front = null;
                back = null;
                return 0;
            }

            if (specialID >= num)
            {
                specialID = num - 1;
            }

            front = _frontSprite[specialID];
            back = _backSprite[specialID];
            specialID = UnityEngine.Random.Range(0, num);

            return specialID;
        }
    }
}
