using UnityEngine;
using UnityEngine.UI;

public class LevelSystem : MonoBehaviour
{
   public int level;
   private int highLevel;
   private float currentXp;
   private float requiredXp;

   private float lerpTime;
   private float delayTime;
   private int effectsOptionGet;

   [Header("UI")]
   [SerializeField] private Image frontXpBar;
   [SerializeField] private Image backXpBar;
   [SerializeField] private Text LVLText;
   [SerializeField] private AudioSource levelUpSound;
   [SerializeField] private ParticleSystem levelUpParticle;

   private void Awake()
   {
      frontXpBar.fillAmount = 0;
      backXpBar.fillAmount = 0;
      requiredXp = CalculateRequiredXp();
      LVLText.text = "LVL: " + level;
      highLevel = PlayerPrefs.GetInt("HighLevel");
      effectsOptionGet = PlayerPrefs.GetInt("Effects", 1);
   }

   private void Update()
   {
      UpdateXpUI();
      if (currentXp >= requiredXp)
         LevelUP();
   }

   public void HighLevel()
   {
      if(level > highLevel)
      {
         PlayerPrefs.SetInt("HighLevel", level);
      }
   }

   public void UpdateXpUI()
   {
      float xpFraction = currentXp / requiredXp;
      float FXP = frontXpBar.fillAmount;
      if (FXP < xpFraction)
      {
         delayTime += Time.deltaTime;
         backXpBar.fillAmount = xpFraction;
         if (delayTime > 0.2f)
         {
            lerpTime += Time.deltaTime;
            float percentComplete = lerpTime / 4f;
            frontXpBar.fillAmount = Mathf.Lerp(FXP, backXpBar.fillAmount, percentComplete);
         }
      }
   }

   public void GainExperienceFlatRate(float xpGained)
   {
      currentXp += xpGained;
      lerpTime = 0f;
      delayTime = 0f;
   }

   /* public void GainExperienceScalable(float xpGained, int passedLevel)
   {
      if (passedLevel < level)
      {
         float multiplier = 1 + (level - passedLevel) * 0.1f;
         currentXp += xpGained * multiplier;
      }
      else
      {
         currentXp += xpGained;
      }
      lerpTime = 0f;
      delayTime = 0f;
   } */

   public void LevelUP()
   {
      if (effectsOptionGet == 1)
         Instantiate(levelUpParticle, LVLText.transform.position, levelUpParticle.transform.rotation);
      levelUpSound.Play();
      LVLText.GetComponent<Animator>().SetTrigger("LvlUP");
      level++;
      frontXpBar.fillAmount = 0f;
      backXpBar.fillAmount = 0f;
      currentXp = Mathf.RoundToInt(currentXp - requiredXp);
      requiredXp = CalculateRequiredXp();
      LVLText.text = "LVL: " + level;
      GetComponent<Player>().IncreaseHealth(level);
   }

   private int CalculateRequiredXp()
   {
      int solveForRequiredXp = 0;
      for (int levelCycle = 1; levelCycle <= level; levelCycle++)
      {
         solveForRequiredXp += (int)Mathf.Floor(levelCycle + 200 * Mathf.Pow(2, levelCycle / 14));
      }

      return solveForRequiredXp / 4;
   }
}
