using UnityEngine;

public class Bullet : MonoBehaviour
{
   private PlayerProperties playerProperties;
   private LevelSystem levelSystem;
   private ObjectsSpawn objectsSpawn;
   private Player player;
   private EnemySpawner enemySpawner;
   private AudioSource hitSound;
   private int effectsOptionGet;

   [SerializeField] private ParticleSystem[] ObjectHitParticle;
   [SerializeField] private ParticleSystem hitBulletsParticle;
   [SerializeField] private ParticleSystem deathEnemyParticle;

   private void Awake()
   {
      playerProperties = FindObjectOfType<PlayerProperties>();
      enemySpawner = FindObjectOfType<EnemySpawner>();
      levelSystem = FindObjectOfType<LevelSystem>();
      objectsSpawn = FindObjectOfType<ObjectsSpawn>();
      player = FindObjectOfType<Player>();
      hitSound = GameObject.Find("Hit").GetComponent<AudioSource>();
      effectsOptionGet = PlayerPrefs.GetInt("Effects", 1);
   }

   private void OnCollisionEnter2D(Collision2D collision)
   {
      hitSound.Play();
      if (collision.gameObject.CompareTag("EnemyBullet"))
      {
         if(effectsOptionGet == 1)
            Instantiate(hitBulletsParticle, transform.position, hitBulletsParticle.transform.rotation);
         Destroy(gameObject);
         Destroy(collision.gameObject);
      }

      Object obj = collision.gameObject.GetComponent<Object>();
      Enemy enemyHP = collision.gameObject.GetComponent<Enemy>();
      if (obj)
      {
         if (effectsOptionGet == 1)
         {
            if (obj.healValue == 40)
               Instantiate(ObjectHitParticle[0], transform.position, ObjectHitParticle[0].transform.rotation);
            if (obj.healValue == 30)
               Instantiate(ObjectHitParticle[1], transform.position, ObjectHitParticle[1].transform.rotation);
            if (obj.healValue == 20)
               Instantiate(ObjectHitParticle[2], transform.position, ObjectHitParticle[2].transform.rotation);
            if (obj.healValue == 10)
               Instantiate(ObjectHitParticle[3], transform.position, ObjectHitParticle[3].transform.rotation);
         }
         obj.hitpoints -= playerProperties.damage;
         Destroy(gameObject);
         if(obj.hitpoints <= 0)
         {
            levelSystem.GainExperienceFlatRate(obj.givesExperience);
            player.playerHealth += player.maxHealth / obj.healValue;
            player.lerpTime = 0;
            Destroy(obj.gameObject);
            objectsSpawn.RandomObject(1);
         }
      }

      else if (enemyHP)
      {
         if (effectsOptionGet == 1)
            Instantiate(hitBulletsParticle, transform.position, hitBulletsParticle.transform.rotation);
         enemyHP.enemyHitpoints -= playerProperties.damage;
         Destroy(gameObject);
         if (enemyHP.enemyHitpoints <= 0)
         {
            if (effectsOptionGet == 1)
               Instantiate(deathEnemyParticle, enemyHP.transform.position, deathEnemyParticle.transform.rotation);
            levelSystem.GainExperienceFlatRate(enemyHP.enemyLevel * 50);
            enemySpawner.SpawnEnemy();
            Destroy(enemyHP.gameObject);
         }
      }
   }
}
