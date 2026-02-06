using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Animations
{
    public class ITAVisuals
    {
        public static AttackVisualsSO Explode;
        public static AttackVisualsSO Stank;
        public static AttackVisualsSO Wind;
        public static AttackVisualsSO PendulumL;
        public static AttackVisualsSO PendulumR;
        public static AttackVisualsSO PendulumFinisher;
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

            Wind = ScriptableObject.CreateInstance<AttackVisualsSO>();
            Wind.audioReference = "event:/AASFX/ITA/Wind";
            Wind.isAnimationFullScreen = false;
            Wind.animation = AApocrypha.assetBundle.LoadAsset<AnimationClip>("Assets/Apocrypha_Animations/Wind.anim");

            PendulumL = ScriptableObject.CreateInstance<AttackVisualsSO>();
            PendulumL.audioReference = "event:/AASFX/ITA/PendulumLeft";
            PendulumL.isAnimationFullScreen = false;
            PendulumL.animation = AApocrypha.assetBundle.LoadAsset<AnimationClip>("Assets/IntoTheAbyssEnemies/PendulumLeft.anim");

            PendulumR = ScriptableObject.CreateInstance<AttackVisualsSO>();
            PendulumR.audioReference = "event:/AASFX/ITA/PendulumRight";
            PendulumR.isAnimationFullScreen = false;
            PendulumR.animation = AApocrypha.assetBundle.LoadAsset<AnimationClip>("Assets/IntoTheAbyssEnemies/PendulumRight.anim");

            PendulumFinisher = ScriptableObject.CreateInstance<AttackVisualsSO>();
            PendulumFinisher.audioReference = "event:/AASFX/ITA/PendulumFinisher";
            PendulumFinisher.isAnimationFullScreen = false;
            PendulumFinisher.animation = AApocrypha.assetBundle.LoadAsset<AnimationClip>("Assets/IntoTheAbyssEnemies/PendulumFinisher.anim");
        }
    }
}
