using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Animations
{
    public class ITAVisuals
    {
        public static AttackVisualsSO Explode;
        public static AttackVisualsSO Stank;
        public static void Add()
        {
            Explode = ScriptableObject.CreateInstance<AttackVisualsSO>();
            Explode.audioReference = "event:/AASFX/ITA/Explode";
            Explode.isAnimationFullScreen = false;
            Explode.animation = AApocrypha.assetBundle.LoadAsset<AnimationClip>("Assets/Apocrypha_Animations/Explode.anim");

            Stank = ScriptableObject.CreateInstance<AttackVisualsSO>();
            Stank.audioReference = "event:/AASFX/ITA/Stank";
            Stank.isAnimationFullScreen = false;
            Stank.animation = AApocrypha.assetBundle.LoadAsset<AnimationClip>("Assets/Apocrypha_Animations/Stank.anim");
        }
    }
}
