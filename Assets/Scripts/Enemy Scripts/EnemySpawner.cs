using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
   [SerializeField] private GameObject[] enemyPrefabs;
   private LevelSystem levelSystem;

   private float posXLimit = 46;
   private float posYLimit = 37;

   private void Awake()
   {
      levelSystem = FindObjectOfType<LevelSystem>();
      StartSpawn();
   }

   private void StartSpawn()
   {
      for (int i = 0; i < 7; i++)
      {
         float posX = Random.Range(-posXLimit, posXLimit);
         float posY = Random.Range(-posYLimit, posYLimit);

         Instantiate(enemyPrefabs[0], new Vector2(posX, posY), Quaternion.identity);
      }
   }

   public void SpawnEnemy()
   {
      float posX = Random.Range(-posXLimit, posXLimit);
      float posY = Random.Range(-posYLimit, posYLimit);

      if (levelSystem.level >= 5 && levelSystem.level < 14)
         Instantiate(enemyPrefabs[Random.Range(1,3)], new Vector2(posX, posY), Quaternion.identity);
      else if (levelSystem.level >= 15 && levelSystem.level < 34)
         Instantiate(enemyPrefabs[Random.Range(3, 7)], new Vector2(posX, posY), Quaternion.identity);
      else if (levelSystem.level >= 35)
         Instantiate(enemyPrefabs[Random.Range(7, 15)], new Vector2(posX, posY), Quaternion.identity);
      else
         Instantiate(enemyPrefabs[0], new Vector2(posX, posY), Quaternion.identity);
   }
}
