using System;
using UnityEngine;


public class Insect : MonoBehaviour
{
        public void OnCollisionEnter2D(Collision2D col)
        {
                if (col.gameObject.GetComponent<WaterBullet>())
                {
                        // hit insect with water
                }
        }
}
