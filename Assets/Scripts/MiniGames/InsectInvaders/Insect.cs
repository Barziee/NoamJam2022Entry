using System;
using UnityEngine;


public class Insect : MonoBehaviour
{
        private int lifeTimeSeconds = 5;

        private void Awake()
        {
                Invoke(nameof(InsectSurvived), lifeTimeSeconds);
        }

        private void InsectSurvived()
        {
                InsectInvaders.Instance.InsectMissedAtLocation(transform);
                DestroySelf();
        }

        internal void DestroySelf()
        {
                gameObject.SetActive(false);
                Destroy(gameObject);
        }
        
        public void OnCollisionEnter2D(Collision2D col)
        {
                if (col.gameObject.GetComponent<WaterBullet>())
                {
                        // hit insect with water
                        DestroySelf();
                }
        }
}
