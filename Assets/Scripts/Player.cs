using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
   [SerializeField] private Joystick rotateJoystick;
   [SerializeField] private Joystick moveJoystick;

   private LevelSystem levelSystem;

   private float angle;
   private Rigidbody2D rb;

   [SerializeField] private float moveSpeed = 3f;

   public float playerHealth;
   public float lerpTime;
   public float maxHealth = 15f;
   private float chipSpeed = 2f;

   public bool gameOver = false;

   [SerializeField] private Image frontHealthBar;
   [SerializeField] private Image backHealthBar;
   [SerializeField] private Color healColor;
   [SerializeField] private Color damageColor;

   [SerializeField] private Animator transition;
   [SerializeField] private AudioSource gameOverSound;

   private void Start()
   {
      rb = GetComponent<Rigidbody2D>();
      levelSystem = FindObjectOfType<LevelSystem>();
      playerHealth = maxHealth;
   }

   private void Update()
   {
      playerHealth = Mathf.Clamp(playerHealth, 0, maxHealth);

      UpdateHealthUI();
      HealthCheck();
   }

   private void FixedUpdate()
   {
      if (!gameOver)
      {
         RotatePlayer();
         MovePlayer();
      }
   }

   private void UpdateHealthUI()
   {
      float fillF = frontHealthBar.fillAmount;
      float fillB = backHealthBar.fillAmount;
      float hFraction = playerHealth / maxHealth;

      if (fillB > hFraction)
      {
         frontHealthBar.fillAmount = hFraction;
         backHealthBar.color = damageColor;
         lerpTime += Time.deltaTime;
         float percentComplete = lerpTime / chipSpeed;
         percentComplete *= percentComplete;
         backHealthBar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
      }

      if (fillF < hFraction)
      {
         backHealthBar.color = healColor;
         backHealthBar.fillAmount = hFraction;
         lerpTime += Time.deltaTime;
         float percentComplete = lerpTime / chipSpeed;
         percentComplete *= percentComplete;
         frontHealthBar.fillAmount = Mathf.Lerp(fillF, backHealthBar.fillAmount, percentComplete);
      }
   }

   private void HealthCheck()
   {
      if (playerHealth <= 0)
      {
         gameOver = true;
         levelSystem.HighLevel();
         gameOverSound.Play();
         StartCoroutine(LoadGame());
      }
   }

   public void IncreaseHealth(int level)
   {
      maxHealth += (playerHealth * 0.012f) * ((100 - level) * 0.1f);
      playerHealth = maxHealth;
   }

   void MovePlayer()
   {
      rb.MovePosition(rb.position + moveJoystick.joystickVector * moveSpeed * Time.deltaTime);
   }

   void RotatePlayer()
   {
      if (rotateJoystick.joystickVector == Vector2.zero) return;
      angle = Mathf.Atan2(rotateJoystick.joystickVector.y, rotateJoystick.joystickVector.x) * Mathf.Rad2Deg;
      var lookRotation = Quaternion.Euler((angle - 90) * Vector3.forward);
      transform.rotation = lookRotation;
   }

   IEnumerator LoadGame()
   {
      yield return new WaitForSeconds(2);
      transition.SetTrigger("Start");
      yield return new WaitForSeconds(1);
      SceneManager.LoadScene("MenuScene");
   }
}
