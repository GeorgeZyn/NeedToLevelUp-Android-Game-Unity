using UnityEngine;

public class ObjectsSpawn : MonoBehaviour
{
   [SerializeField] private int maxObjects;
   [SerializeField] private GameObject[] objectPrefab;

   private float posXLimit = 46;
   private float posYLimit = 37;

   private void Awake()
   {
      RandomObject(maxObjects);
   }

   public void RandomObject(int countObjects)
   {
      for (int i = 0; i < countObjects; i++)
      {
         float posX = Random.Range(-posXLimit, posXLimit);
         float posY = Random.Range(-posYLimit, posYLimit);

         int chanceObjectAppearing = Random.Range(0, 101);

         if(chanceObjectAppearing <= 40)
            Instantiate(objectPrefab[0], new Vector2(posX, posY), Quaternion.identity);
         if (chanceObjectAppearing > 40 && chanceObjectAppearing <= 65)
            Instantiate(objectPrefab[1], new Vector2(posX, posY), Quaternion.identity);
         if (chanceObjectAppearing > 65 && chanceObjectAppearing <= 89)
            Instantiate(objectPrefab[2], new Vector2(posX, posY), Quaternion.identity);
         if (chanceObjectAppearing > 89)
            Instantiate(objectPrefab[3], new Vector2(posX, posY), Quaternion.identity);
      }
   }
}
