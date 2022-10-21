using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class InsectInvaders : Minigame
{
        public static InsectInvaders Instance;
        
        [SerializeField] private Insect insectPrefab;
        [SerializeField] private List<Transform> insectSpawnLocations;
        private List<Insect> insectList;
        private List<Transform> availableInsectSpawnLocations;
        private int insectSpawnDelaySeconds = 1;

        [SerializeField] private Spritzer spritzer;
        [SerializeField] private Transform spritzerStartLocation;
        [SerializeField] private GameObject spritzerBulletSpawn;

        private bool finishedSpawning = false;

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

                Init(3, 0 ,3);
        }

        public override void Init(int seconds, int score, int lives,
                Action onComplete = null, Action onMiniGameEnded = null)
        {
                Spritzer spritzerObj = Instantiate(spritzer, spritzerStartLocation);
                spritzerObj.gameObject.SetActive(true);
                spritzerObj.SetBulletSpawn(spritzerBulletSpawn);
                insectList = new List<Insect>();
                availableInsectSpawnLocations = new List<Transform>(insectSpawnLocations);
                
                base.Init(seconds, score, lives, () =>
                {
                        spritzerObj.SetCanShoot(true);
                        StartCoroutine(SpawnInsectsCoroutine());
                });
        }

        public IEnumerator SpawnInsectsCoroutine()
        {
                int seconds = 3;
                for (int i = 0; i < seconds; i++)
                {
                        float diff = Random.Range(-0.5f, 0.5f);
                        yield return new WaitForSeconds(insectSpawnDelaySeconds + diff);
                        SpawnInsect();
                        // play sound
                }

                finishedSpawning = true;
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

        public void InsectKilledAtLocation(Transform location, Insect insect)
        {
                IncreaseScore(1);
                InsectRemovedFromLocation(location, insect);
        }
        
        public void InsectMissedAtLocation(Transform location, Insect insect)
        {
                ReduceLife();
                InsectRemovedFromLocation(location, insect);
        }

        private void InsectRemovedFromLocation(Transform location, Insect insect)
        {
                if (insectList.Contains(insect))
                        insectList.Remove(insect);
                
                if(insectSpawnLocations.Contains(location))
                        availableInsectSpawnLocations.Add(location);

                if (finishedSpawning && insectList.Count == 0)
                        EndGame();
        }

        public override void EndGame()
        {
                spritzer.DestroySelf();
                foreach (Insect insect in insectList)
                {
                        if(insect)
                                insect.DestroySelf();
                }
                DestroySelf();
        }
}
