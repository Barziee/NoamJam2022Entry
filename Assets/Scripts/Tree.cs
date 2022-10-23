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
}
