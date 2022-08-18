using UnityEngine;
using System.Collections;

public class EnemyShoot : MonoBehaviour
{
   private Player player;

   [SerializeField] private Transform[] firePoint;
   [SerializeField] private GameObject[] guns;
   [SerializeField] private GameObject enemyBulletPrefab;
   private AudioSource shootSound;

   [SerializeField] private float bulletForce = 10f;
   [SerializeField] private float timeReload = 1f;
   private float shootDistance = 19;
   public int damage;
   private float timeStamp;

   private void Start()
   {
      player = FindObjectOfType<Player>();
      shootSound = GameObject.Find("Shoot").GetComponent<AudioSource>();
   }

   private void Update()
   {
      EnemyRotate();
      DistanceShooting();
   }

   private void EnemyRotate()
   {
      Vector3 direction = player.transform.position - transform.position;
      float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
      transform.rotation = Quaternion.Euler(0f, 0f, angle - 90);
   }

   private void DistanceShooting()
   {

      float distanceFromPlayer = Vector2.Distance(player.transform.position, transform.position);
      if (distanceFromPlayer <= shootDistance)
      {
         if(!player.gameOver)
            Shooting();
      }
   }

   private void Shooting()
   {
      if (timeStamp <= Time.time)
      {
         shootSound.Play();
         for (int i = 0; i < firePoint.Length; i++)
         {
            GameObject bulletEnemy = Instantiate(enemyBulletPrefab, firePoint[i].position, Quaternion.identity);
            Rigidbody2D rbBullet = bulletEnemy.GetComponent<Rigidbody2D>();
            rbBullet.AddForce(firePoint[i].up * bulletForce, ForceMode2D.Impulse);

            guns[i].transform.position -= guns[i].transform.up * 0.06f;
            StartCoroutine(ShootAnimation(i));

            timeStamp = Time.time + timeReload;

            Destroy(bulletEnemy.gameObject, 0.5f);
         }
      }
   }

   IEnumerator ShootAnimation(int i)
   {
      yield return new WaitForSeconds(0.03f);
      guns[i].transform.position += guns[i].transform.up * 0.02f;
      yield return new WaitForSeconds(0.03f);
      guns[i].transform.position += guns[i].transform.up * 0.02f;
      yield return new WaitForSeconds(0.03f);
      guns[i].transform.position += guns[i].transform.up * 0.02f;
   }
}
