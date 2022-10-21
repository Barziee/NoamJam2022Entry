using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class InsectInvaders : Minigame
{
        public static InsectInvaders Instance;
        
        [SerializeField] private GameObject insectPrefab;
        [SerializeField] private List<Transform> insectSpawnLocations;
        private List<Insect> insectList;
        private List<Transform> availableInsectSpawnLocations;
        private int insectSpawnDelaySeconds = 1;

        [SerializeField] private GameObject spritzerPrefab;
        [SerializeField] private Transform spritzerStartLocation;
        [SerializeField] private GameObject spritzerBulletSpawn;

        private Spritzer spriterObject;

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
                GameObject spritzerObj = Instantiate(spritzerPrefab, spritzerStartLocation);
                spritzerObj.gameObject.SetActive(true);
                spriterObject = spritzerObj.GetComponent<Spritzer>();
                spriterObject.SetBulletSpawn(spritzerBulletSpawn);
                insectList = new List<Insect>();
                availableInsectSpawnLocations = new List<Transform>(insectSpawnLocations);
                
                base.Init(seconds, score, lives, () =>
                {
                        spritzerObj.GetComponent<Spritzer>().SetCanShoot(true);
                        StartCoroutine(SpawnInsectsCoroutine());
                },
                        null);
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
                        GameObject insect = Instantiate(insectPrefab, availableInsectSpawnLocations[locationIndex]);
                        availableInsectSpawnLocations.RemoveAt(locationIndex);
                        insectList.Add(insect.GetComponent<Insect>()); 
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
                spriterObject.DestroySelf();
                foreach (Insect insect in insectList)
                {
                        if(insect)
                                insect.DestroySelf();
                }
                DestroySelf();
        }
}
