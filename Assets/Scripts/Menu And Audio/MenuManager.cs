using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
   [SerializeField] private Animator transition;
   [SerializeField] private GameObject panelSetiings;
   [SerializeField] private AudioSource buttonSound;

   [SerializeField] private Toggle effectsToggle;
   [SerializeField] private Toggle bloomToggle;

   private int highLevel;
   private int effectsReference;
   private int bloomReference;

   [SerializeField] private Text highLevelText;

   private void Start()
   {
      highLevel = PlayerPrefs.GetInt("HighLevel");
      highLevelText.text = highLevel.ToString();

      EffectsCheck();
      BloomCheck();
   }

   private void EffectsCheck()
   {
      effectsReference = PlayerPrefs.GetInt("Effects", 1);
      effectsToggle.isOn = effectsReference == 1;
   }

   private void BloomCheck()
   {
      bloomReference = PlayerPrefs.GetInt("Bloom", 1);
      bloomToggle.isOn = bloomReference == 1;
   }

   public void PlayButton()
   {
      buttonSound.Play();
      StartCoroutine(LoadGame());
   }

   public void SettingButton()
   {
      buttonSound.Play();
      if (!panelSetiings.activeSelf)
         panelSetiings.SetActive(true);
      else
      {
         StartCoroutine(CloseSettingsPanel());
      }
   }

   public void EffectsToggle(bool value)
   {
      int getNum = value ? 1 : 0;
      PlayerPrefs.SetInt("Effects", getNum);
   }

   public void BloomToggle(bool value)
   {
      int getNum = value ? 1 : 0;
      PlayerPrefs.SetInt("Bloom", getNum);
   }

   IEnumerator LoadGame()
   {
      transition.SetTrigger("Start");
      yield return new WaitForSeconds(1);
      SceneManager.LoadScene("GameScene");
   }

   IEnumerator CloseSettingsPanel()
   {
      panelSetiings.GetComponent<Animator>().SetTrigger("Close");
      yield return new WaitForSeconds(0.7f);
      panelSetiings.SetActive(false);
   }
}
