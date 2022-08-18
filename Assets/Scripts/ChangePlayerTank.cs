using UnityEngine;

public class ChangePlayerTank : MonoBehaviour
{
   [SerializeField] private Transform player;
   [SerializeField] private GameObject[] playerTankPrefabs;
   private LevelSystem lvl;
   private int developmentBranch;

   [Header("Panels")]
   [SerializeField] private GameObject Panel2;
   private bool Panel2Clicked = false;

   [SerializeField] private GameObject Panel3_1;
   private bool Panel3_1Clicked = false;

   [SerializeField] private GameObject Panel3_2;
   private bool Panel3_2Clicked = false;

   [SerializeField] private GameObject Panel4_1;
   private bool Panel4_1Clicked = false;

   [SerializeField] private GameObject Panel4_2;
   private bool Panel4_2Clicked = false;

   [SerializeField] private GameObject Panel4_3;
   private bool Panel4_3Clicked = false;

   [SerializeField] private GameObject Panel4_4;
   private bool Panel4_4Clicked = false;

   private void Start()
   {
      lvl = FindObjectOfType<Player>().GetComponent<LevelSystem>();
   }

   private void Update()
   {
      LevelCheck();
   }

   public void SetTank_2_1()
   {
      Panel2Clicked = true;
      SetPanel(0, Panel2);
      developmentBranch = 1;
   }

   public void SetTank_2_2()
   {
      Panel2Clicked = true;
      SetPanel(1, Panel2);
      developmentBranch = 2;
   }

   public void SetTank_3_1()
   {
      Panel3_1Clicked = true;
      SetPanel(2, Panel3_1);
      developmentBranch = 3;
   }

   public void SetTank_3_2()
   {
      Panel3_1Clicked = true;
      SetPanel(3, Panel3_1);
      developmentBranch = 4;
   }

   public void SetTank_3_3()
   {
      Panel3_2Clicked = true;
      SetPanel(4, Panel3_2);
      developmentBranch = 5;
   }

   public void SetTank_3_4()
   {
      Panel3_2Clicked = true;
      SetPanel(5, Panel3_2);
      developmentBranch = 6;
   }

   public void SetTank_4_1()
   {
      Panel4_1Clicked = true;
      SetPanel(6, Panel4_1);
   }

   public void SetTank_4_2()
   {
      Panel4_1Clicked = true;
      SetPanel(7, Panel4_1);
   }

   public void SetTank_4_3()
   {
      Panel4_2Clicked = true;
      SetPanel(8, Panel4_2);
   }

   public void SetTank_4_4()
   {
      Panel4_2Clicked = true;
      SetPanel(9, Panel4_2);
   }

   public void SetTank_4_5()
   {
      Panel4_3Clicked = true;
      SetPanel(10, Panel4_3);
   }

   public void SetTank_4_6()
   {
      Panel4_3Clicked = true;
      SetPanel(11, Panel4_3);
   }

   public void SetTank_4_7()
   {
      Panel4_4Clicked = true;
      SetPanel(12, Panel4_4);
   }

   public void SetTank_4_8()
   {
      Panel4_4Clicked = true;
      SetPanel(13, Panel4_4);
   }

   private void LevelCheck()
   {
      if (lvl.level >= 5 && Panel2Clicked == false)
         Panel2.SetActive(true);

      if (lvl.level >= 15 && Panel3_1Clicked == false && developmentBranch == 1)
         Panel3_1.SetActive(true);
      if (lvl.level >= 15 && Panel3_2Clicked == false && developmentBranch == 2)
         Panel3_2.SetActive(true);

      if (lvl.level >= 35 && Panel4_1Clicked == false && developmentBranch == 3)
         Panel4_1.SetActive(true);
      if (lvl.level >= 35 && Panel4_2Clicked == false && developmentBranch == 4)
         Panel4_2.SetActive(true);
      if (lvl.level >= 35 && Panel4_3Clicked == false && developmentBranch == 5)
         Panel4_3.SetActive(true);
      if (lvl.level >= 35 && Panel4_4Clicked == false && developmentBranch == 6)
         Panel4_4.SetActive(true);
   }

   private void SetPanel(int NumPref, GameObject Panel)
   {
      DeleteChildTank();
      Instantiate(playerTankPrefabs[NumPref], player.position, player.rotation, player);
      Panel.SetActive(!Panel.activeSelf);
   }

   private void DeleteChildTank()
   {
      foreach (Transform child in player)
         Destroy(child.gameObject);
   }
}
