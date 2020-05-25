using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pong {
    public static class AudioManager {
#if WINDOWS
        public static AudioEngine AudioEngine;
        public static WaveBank WaveBank;
        public static SoundBank SoundBank;
#elif ANDROID
        public static Dictionary<string, SoundEffect> SoundEffects = new Dictionary<string, SoundEffect>();
#endif

        public static void Initialize(ContentManager content) {
#if WINDOWS
            AudioEngine = new AudioEngine("Content/Sound/XACT/GameAudio.xgs");
            WaveBank = new WaveBank(AudioEngine, "Content/Sound/XACT/Wave Bank.xwb");
            SoundBank = new SoundBank(AudioEngine, "Content/Sound/XACT/Sound Bank.xsb");
#elif ANDROID
            SoundEffects.Add("Pad", content.Load<SoundEffect>("Sound/Pad"));
            SoundEffects.Add("Score", content.Load<SoundEffect>("Sound/Score"));
            SoundEffects.Add("Wall", content.Load<SoundEffect>("Sound/Wall"));
#endif
        }

        public static void PlayCue(string cueName) {
#if WINDOWS
            SoundBank.PlayCue(cueName);
#elif ANDROID
            SoundEffect soundEffect;
            SoundEffects.TryGetValue(cueName, out soundEffect);
            soundEffect.Play();
#endif
        }
    }
}
