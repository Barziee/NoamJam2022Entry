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

                Init(3);
        }

        public override void Init(int seconds, Action onComplete = null)
        {
                Spritzer spritzerObj = Instantiate(spritzer, spritzerStartLocation);
                spritzerObj.SetBulletSpawn(spritzerBulletSpawn);
                insectList = new List<Insect>();
                availableInsectSpawnLocations = new List<Transform>(insectSpawnLocations);
                
                base.Init(seconds, () =>
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
                        yield return new WaitForSeconds(insectSpawnDelaySeconds);
                        SpawnInsect();
                        // play sound
                }
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
