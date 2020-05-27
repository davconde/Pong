using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pong {
    public static class AudioManager {
        public static AudioEngine AudioEngine;
        public static WaveBank WaveBank;
        public static SoundBank SoundBank;

        public static void Initialize(ContentManager content) {
            AudioEngine = new AudioEngine("Content/Sound/GameAudio.xgs");
            WaveBank = new WaveBank(AudioEngine, "Content/Sound/Wave Bank.xwb");
            SoundBank = new SoundBank(AudioEngine, "Content/Sound/Sound Bank.xsb");
        }

        public static void PlayCue(string cueName) {
            SoundBank.PlayCue(cueName);
        }
    }
}
