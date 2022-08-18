using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSetting : MonoBehaviour
{
   private static readonly string backgroundPref = "BackgroundPref";
   private static readonly string soundEffectsPref = "SoundEffectsPref";
   private float backgroundFloat, soundEffectsFloat;
   public AudioSource backgroundAudio;
   public AudioSource[] soundEffectsAudio;

   private void Awake()
   {
      ContinueSettings();
   }

   private void ContinueSettings()
   {
      backgroundFloat = PlayerPrefs.GetFloat(backgroundPref);
      soundEffectsFloat = PlayerPrefs.GetFloat(soundEffectsPref);

      backgroundAudio.volume = backgroundFloat;

      for (int i = 0; i < soundEffectsAudio.Length; i++)
      {
         soundEffectsAudio[i].volume = soundEffectsFloat;
      }
   }
}
