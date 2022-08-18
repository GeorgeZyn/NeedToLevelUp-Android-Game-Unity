using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBullet : MonoBehaviour
{
   private ObjectsSpawn objectsSpawn;
   private EnemyShoot enemyShoot;
   private AudioSource hitSound;
   private int effectsOptionGet;

   [SerializeField] private ParticleSystem[] ObjectHitParticle;
   [SerializeField] private ParticleSystem hitBulletsParticle;

   private void Start()
   {
      objectsSpawn = FindObjectOfType<ObjectsSpawn>();
      enemyShoot = FindObjectOfType<EnemyShoot>();
      hitSound = GameObject.Find("Hit").GetComponent<AudioSource>();
      effectsOptionGet = PlayerPrefs.GetInt("Effects", 1);
   }

   private void OnCollisionEnter2D(Collision2D collision)
   {
      hitSound.Play();
      Object obj = collision.gameObject.GetComponent<Object>();
      Player player = collision.gameObject.GetComponent<Player>();
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

         obj.hitpoints -= enemyShoot.damage;
         Destroy(gameObject);
         if (obj.hitpoints <= 0)
         {
            Destroy(obj.gameObject);
            objectsSpawn.RandomObject(1);
         }
      }

      else if (player)
      {
         if (effectsOptionGet == 1)
            Instantiate(hitBulletsParticle, transform.position, hitBulletsParticle.transform.rotation);
         player.playerHealth -= enemyShoot.damage;
         player.lerpTime = 0;
         Destroy(gameObject);
      }
   }
}
