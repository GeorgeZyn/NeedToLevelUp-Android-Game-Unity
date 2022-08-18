using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class GameManager : MonoBehaviour
{
   [SerializeField] private Animator close;
   [SerializeField] private GameObject pausePanel;
   [SerializeField] private AudioSource buttonSound;

   [SerializeField] private PostProcessVolume postProcessVolume;
   private Bloom bloom;

   private int bloomOptionGet;

   private void Start()
   {
      postProcessVolume.profile.TryGetSettings(out bloom);
      bloomOptionGet = PlayerPrefs.GetInt("Bloom", 1);
      bloom.active = bloomOptionGet == 1;
   }

   public void PauseButton()
   {
      buttonSound.Play();
      if (!pausePanel.activeSelf)
      {
         StartCoroutine(OpenPausePanel());
         
      }
      else
      {
         StartCoroutine(ClosePausePanel());
      }
   }

   IEnumerator ClosePausePanel()
   {
      Time.timeScale = 1;
      pausePanel.GetComponent<Animator>().SetTrigger("Close");
      yield return new WaitForSeconds(0.5f);
      pausePanel.SetActive(false);
   }

   IEnumerator OpenPausePanel()
   {
      pausePanel.SetActive(true);
      yield return new WaitForSeconds(0.5f);
      Time.timeScale = 0;
   }
}
