using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsectInvaders : Minigame
{
        private List<Insect> insectList;
        private List<Vector3> insectSpawnLocations;
        private int insectSpawnDelaySeconds;

        [SerializeField] private GameObject playerObject;

        [SerializeField] private Insect insectPrefab;

        public override void Init(int seconds)
        {
                base.Init(seconds);
                playerObject.SetActive(true);
        }

        public IEnumerator SpawnInsectsCoroutine()
        {
                
                
                yield return null;
        }

        private void SpawnInsect()
        {
                int locationIndex = Random.Range(0, insectSpawnLocations.Count - 1);
                Instantiate(insectPrefab, insectSpawnLocations[locationIndex], Quaternion.identity);
        }

        public void EndGame()
        {
                playerObject.SetActive(false);
        }
}
