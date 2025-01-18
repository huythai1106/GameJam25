using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ParadoxGameStudio
{
    [CreateAssetMenu(menuName = "SoundObject")]
    public class SoundObject : ScriptableObject
    {
        public AudioClip[] allSoundEffect;
        public Sound[] allSoundEffectByName;
        public SoundList[] allSoundEffectListByName;
        public AudioClip[] bgClip;
    }
}
