using UnityEngine;
using System.Collections;

public class PlayerProperties : MonoBehaviour
{
   private Player player;
   private Joystick rotateJoystick;
   [SerializeField] private Transform[] firePoint;
   [SerializeField] private GameObject[] guns;
   [SerializeField] private GameObject bulletPrefab;
   private AudioSource shootSound;

   [SerializeField] private float bulletForce = 20f;
   [SerializeField] private float timeReload = 1f;
   private float timeStamp;

   public int damage;

   private void Start()
   {
      rotateJoystick = GameObject.Find("Background Joystick Rotate").GetComponent<Joystick>();
      shootSound = GameObject.Find("Shoot").GetComponent<AudioSource>();
      player = FindObjectOfType<Player>();
   }

   private void Update()
   {
      if (!player.gameOver)
         Shooting();
   }

   private void Shooting()
   {
      if (rotateJoystick.joystickVector != Vector2.zero)
      {
         if (timeStamp <= Time.time)
         {
            shootSound.Play();
            for (int i = 0; i < firePoint.Length; i++)
            {
               GameObject bullet = Instantiate(bulletPrefab, firePoint[i].position, firePoint[i].rotation);
               Rigidbody2D rbBullet = bullet.GetComponent<Rigidbody2D>();
               rbBullet.AddForce(firePoint[i].up * bulletForce, ForceMode2D.Impulse);

               guns[i].transform.position -= guns[i].transform.up * 0.06f;
               StartCoroutine(ShootAnimation(i));

               timeStamp = Time.time + timeReload;

               Destroy(bullet.gameObject, 0.5f);
            }
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
