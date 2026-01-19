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
        public static AttackVisualsSO MicrowaveVisualsSO;
        public static AttackVisualsSO CranesHeavenVisualsSO;
        public static AttackVisualsSO Whispers;
        public static AttackVisualsSO Nothing;

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

            MicrowaveVisualsSO = ScriptableObject.CreateInstance<AttackVisualsSO>();
            MicrowaveVisualsSO.audioReference = "event:/AASFX/Microwave_SFX";
            MicrowaveVisualsSO.isAnimationFullScreen = false;
            MicrowaveVisualsSO.animation = AApocrypha.assetBundle.LoadAsset<AnimationClip>("Assets/Apocrypha_Animations/MicrowaveAnim.anim");

            CranesHeavenVisualsSO = ScriptableObject.CreateInstance<AttackVisualsSO>();
            CranesHeavenVisualsSO.audioReference = "event:/Characters/Player/Cranes/CHR_PLR_Cranes_Dx";
            CranesHeavenVisualsSO.isAnimationFullScreen = false;
            CranesHeavenVisualsSO.animation = AApocrypha.assetBundle.LoadAsset<AnimationClip>("Assets/Apocrypha_Animations/CranesSavingTheRunAnim.anim");

            Whispers = ScriptableObject.CreateInstance<AttackVisualsSO>();
            Whispers.audioReference = "event:/AASFX/Whispers_SFX";
            Whispers.isAnimationFullScreen = false;
            Whispers.animation = AApocrypha.assetBundle.LoadAsset<AnimationClip>("Assets/Apocrypha_Animations/WhispersAnim.anim");

            Nothing = ScriptableObject.CreateInstance<AttackVisualsSO>();
            Nothing.audioReference = "event:/AASFX/Nothing_SFX";
            Nothing.isAnimationFullScreen = false;
            Nothing.animation = AApocrypha.assetBundle.LoadAsset<AnimationClip>("Assets/Apocrypha_Animations/NothingAnim.anim");
        }
    }
}
