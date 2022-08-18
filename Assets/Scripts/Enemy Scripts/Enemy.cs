using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
   [SerializeField] private float speed;
   [SerializeField] private float stoppingDistance;
   [SerializeField] private float retreatDistance;
   [SerializeField] private float restDistance;

   private Player player;

   [SerializeField] int minLevel;
   [SerializeField] int maxLevel;
   public int enemyLevel;
   [SerializeField] private Text enemyLevelText;

   [Header("HP")]
   public float enemyHitpoints;

   [SerializeField] private Image hpImage;
   [SerializeField] private Image hpEffectImage;

   private float maxHP;
   private readonly float hurtSpeed = 0.005f;

   private void Start()
   {
      player = FindObjectOfType<Player>();

      enemyLevel = Random.Range(minLevel, maxLevel + 1);
      enemyHitpoints = enemyLevel * 4;

      maxHP = enemyHitpoints;
   }

   private void LateUpdate()
   {
      if (!player.gameOver)
         EnemyMoving();
   }

   private void Update()
   {
      SetHPBar();

      enemyLevelText.text = "LVL: " + enemyLevel;
   }

   private void EnemyMoving()
   {
      float distanceFromPlayer = Vector2.Distance(player.transform.position, transform.position);
      if (distanceFromPlayer < stoppingDistance && distanceFromPlayer > restDistance)
      {
         transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
      }

      else if (distanceFromPlayer < restDistance && distanceFromPlayer > retreatDistance)
      {
         transform.position = transform.position;
      }

      else if (distanceFromPlayer < retreatDistance)
      {
         transform.position = Vector2.MoveTowards(transform.position, player.transform.position, -speed * Time.deltaTime);
      }
   }

   private void SetHPBar()
   {
      hpImage.fillAmount = enemyHitpoints / maxHP;

      if (hpEffectImage.fillAmount > hpImage.fillAmount)
         hpEffectImage.fillAmount -= hurtSpeed;
      else
         hpEffectImage.fillAmount = hpImage.fillAmount;
   }
}
