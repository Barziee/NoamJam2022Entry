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
                InsectInvaders.Instance.InsectMissedAtLocation(transform, this);
                DestroySelf();
        }

        internal void DestroySelf()
        {
                gameObject.SetActive(false);
                Destroy(this);
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
                Debug.Log("collision insect");
                if (other.gameObject.GetComponent<WaterBullet>())
                {
                        // hit insect with water
                        DestroySelf();
                }
        }
}
