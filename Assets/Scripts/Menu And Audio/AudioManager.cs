using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
   private static readonly string firstPlay = "FirstPlay";
   private static readonly string backgroundPref = "BackgroundPref";
   private static readonly string soundEffectsPref = "SoundEffectsPref";
   private int firstPlayInt;
   public Slider backgroundSlider, soundEffectsSlider;
   private float backgroundFloat, soundEffectsFloat;
   public AudioSource backgroundAudio;
   public AudioSource[] soundEffectsAudio;

   private void Start()
   {
      firstPlayInt = PlayerPrefs.GetInt(firstPlay);

      if (firstPlayInt == 0)
      {
         backgroundFloat = .25f;
         soundEffectsFloat = .50f;
         backgroundSlider.value = backgroundFloat;
         soundEffectsSlider.value = soundEffectsFloat;
         PlayerPrefs.SetFloat(backgroundPref, backgroundFloat);
         PlayerPrefs.SetFloat(soundEffectsPref, soundEffectsFloat);
         PlayerPrefs.SetInt(firstPlay, -1);
      }
      else
      {
         backgroundFloat = PlayerPrefs.GetFloat(backgroundPref);
         backgroundSlider.value = backgroundFloat;
         soundEffectsFloat = PlayerPrefs.GetFloat(soundEffectsPref);
         soundEffectsSlider.value = soundEffectsFloat;
      }
   }

   public void SaveSoundSettings()
   {
      PlayerPrefs.SetFloat(backgroundPref, backgroundSlider.value);
      PlayerPrefs.SetFloat(soundEffectsPref, soundEffectsSlider.value);
   }

   private void OnApplicationFocus(bool inFocus)
   {
      if (!inFocus)
      {
         SaveSoundSettings();
      }
   }

   public void UpdateSound()
   {
      backgroundAudio.volume = backgroundSlider.value;

      for (int i = 0; i < soundEffectsAudio.Length; i++)
      {
         soundEffectsAudio[i].volume = soundEffectsSlider.value;
      }
   }
}

