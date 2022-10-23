using UnityEngine;


public class Tree : MonoBehaviour
{
        [SerializeField] GameObject damagedTreeObject;
        [SerializeField] GameObject waterGameDamageObject;
        [SerializeField] GameObject branchGameDamageObject;
        [SerializeField] GameObject sprayGameDamageObject;

        [SerializeField] private GameObject fullyHealedTreeObject;

        public bool playedWater = false;
        public bool playedBranch = false;
        public bool playedInsect = false;

        private int score = 0;
        public static int MAX_SCORE = 3;

        public void WaterGameWin()
        {
                waterGameDamageObject.gameObject.SetActive(false);
        }

        public void BranchGameWin()
        {
                branchGameDamageObject.gameObject.SetActive(false);
        }
        
        public void InsectGameWin()
        {
                sprayGameDamageObject.gameObject.SetActive(false);
        }

        public void TreeFullyHealed()
        {
                damagedTreeObject.gameObject.SetActive(false);
                fullyHealedTreeObject.gameObject.SetActive(true);
        }

        public bool playedAllGames()
        {
                return playedBranch && playedInsect && playedWater;
        }

        public void IncreaseScore()
        {
                score++;
        }

        public int GetScore()
        {
                return score;
        }
}
