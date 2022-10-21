using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class InsectInvaders : Minigame
{
        public static InsectInvaders Instance;
        
        private List<Insect> insectList;
        private List<Transform> insectSpawnLocations;
        private List<Transform> availableInsectSpawnLocations;
        private int insectSpawnDelaySeconds;

        [SerializeField] private GameObject playerObject;

        [SerializeField] private Insect insectPrefab;

        private void Awake()
        {
                if (Instance)
                {
                        Destroy(this);
                        return;
                }
                else
                {
                        Instance = this;
                }
        }

        public override void Init(int seconds)
        {
                base.Init(seconds);
                playerObject.SetActive(true);
                availableInsectSpawnLocations = new List<Transform>(insectSpawnLocations);
        }

        public IEnumerator SpawnInsectsCoroutine()
        {
                int seconds = 5;
                for (int i = 0; i < seconds; i++)
                { 
                        yield return new WaitForSeconds(insectSpawnDelaySeconds);
                        SpawnInsect();
                        // play sound
                }
                
                yield return null;
        }

        private void SpawnInsect()
        {
                if (availableInsectSpawnLocations.Count > 0)
                {
                        int locationIndex = Random.Range(0, availableInsectSpawnLocations.Count - 1);
                        Insect insect = Instantiate(insectPrefab, availableInsectSpawnLocations[locationIndex]);
                        availableInsectSpawnLocations.RemoveAt(locationIndex);
                        insectList.Add(insect); 
                }
        }

        public void InsectKilledAtLocation(Transform location)
        {
                IncreaseScore(1);
                InsectRemovedFromLocation(location);
        }
        
        public void InsectMissedAtLocation(Transform location)
        {
                ReduceLife();
                InsectRemovedFromLocation(location);
        }

        private void InsectRemovedFromLocation(Transform location)
        {
                if(insectSpawnLocations.Contains(location))
                        availableInsectSpawnLocations.Add(location);
        }

        public void EndGame()
        {
                playerObject.SetActive(false);
        }
}
