using UnityEngine;
using UnityEngine.UI;

public class Object : MonoBehaviour
{
   public float hitpoints;
   public int givesExperience;
   public int healValue;

   [SerializeField] private float moveSpeed;

   [SerializeField] private Image hpImage;
   [SerializeField] private Image hpEffectImage;
   [SerializeField] private Transform obj;

   private float maxHP;
   private readonly float hurtSpeed = 0.005f;

   private int randomMove;
   private int randomRotate;
   private bool collisionCheck = true;

   private Rigidbody2D rb;

   private void Start()
   {
      rb = GetComponent<Rigidbody2D>();

      maxHP = hitpoints;

      randomMove = Random.Range(10, 101);
      randomRotate = Random.Range(-20, 21);
   }

   private void Update()
   {
      SetHPBar();
   }

   private void FixedUpdate()
   {
      MoveAndRotate();
   }

   private void SetHPBar()
   {
      hpImage.fillAmount = hitpoints / maxHP;

      if (hpEffectImage.fillAmount > hpImage.fillAmount)
         hpEffectImage.fillAmount -= hurtSpeed;
      else
         hpEffectImage.fillAmount = hpImage.fillAmount;
   }

   private void MoveAndRotate()
   {
      if (collisionCheck)
      {
         rb.MovePosition(rb.position + moveSpeed * Time.deltaTime * new Vector2(-(randomMove / 50), randomMove / 50));
      }
      obj.Rotate(0, 0, randomRotate * Time.deltaTime);
   }

   private void OnCollisionEnter2D(Collision2D collision)
   {
      if(!collision.gameObject.CompareTag("Object"))
         collisionCheck = false;
   }
}
