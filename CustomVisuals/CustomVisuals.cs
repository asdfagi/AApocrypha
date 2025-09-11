using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Animations
{
    public class CustomVisuals
    {
        public static AttackVisualsSO TestCannonVisualsSO;
        public static AttackVisualsSO GazeVisualsSO;
        public static AttackVisualsSO StarfallVisualsSO;
        public static AttackVisualsSO StaticVisualsSO;
        public static AttackVisualsSO StaticColorVisualsSO;

        public static void Add()
        {
            TestCannonVisualsSO = ScriptableObject.CreateInstance<AttackVisualsSO>();
            TestCannonVisualsSO.audioReference = "event:/Combat/CBT_MSC_ATK_ENM_Spawn";
            TestCannonVisualsSO.isAnimationFullScreen = false;
            TestCannonVisualsSO.animation = AApocrypha.assetBundle.LoadAsset<AnimationClip>("Assets/Apocrypha_Animations/TestCannonAnim.anim");

            GazeVisualsSO = ScriptableObject.CreateInstance<AttackVisualsSO>();
            GazeVisualsSO.audioReference = "event:/Characters/Enemies/DLC_01/TaMaGoa/CHR_ENM_TaMaGoa_Dmg";
            GazeVisualsSO.isAnimationFullScreen = false;
            GazeVisualsSO.animation = AApocrypha.assetBundle.LoadAsset<AnimationClip>("Assets/Apocrypha_Animations/GazeAnim.anim");

            StarfallVisualsSO = ScriptableObject.CreateInstance<AttackVisualsSO>();
            StarfallVisualsSO.audioReference = "event:/AASFX/Starfall_SFX";
            StarfallVisualsSO.isAnimationFullScreen = false;
            StarfallVisualsSO.animation = AApocrypha.assetBundle.LoadAsset<AnimationClip>("Assets/Apocrypha_Animations/StarfallAnim.anim");

            StaticVisualsSO = ScriptableObject.CreateInstance<AttackVisualsSO>();
            StaticVisualsSO.audioReference = "event:/AASFX/Static_SFX";
            StaticVisualsSO.isAnimationFullScreen = false;
            StaticVisualsSO.animation = AApocrypha.assetBundle.LoadAsset<AnimationClip>("Assets/Apocrypha_Animations/StaticAnim.anim");

            StaticColorVisualsSO = ScriptableObject.CreateInstance<AttackVisualsSO>();
            StaticColorVisualsSO.audioReference = "event:/AASFX/Static_SFX";
            StaticColorVisualsSO.isAnimationFullScreen = false;
            StaticColorVisualsSO.animation = AApocrypha.assetBundle.LoadAsset<AnimationClip>("Assets/Apocrypha_Animations/StaticColorAnim.anim");
        }
    }
}
