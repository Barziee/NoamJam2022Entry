using System;
using UnityEngine;

public class Spritzer : MonoBehaviour
{
    [SerializeField] 
    private float speed = 500f;
    
    [SerializeField] 
    private Transform muzzle;
    
    [SerializeField] 
    private float coolDownTime = 0.5f;

    [SerializeField] 
    private WaterBullet bulletPrefab;

    private float shootTimer;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            // move left
            transform.Translate(-speed * Time.deltaTime, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            // move right
            transform.Translate(speed * Time.deltaTime, 0, 0);
        }

        shootTimer += Time.deltaTime;
        if (shootTimer > coolDownTime && Input.GetKey(KeyCode.Space))
        {
            shootTimer = 0f;

            Instantiate(bulletPrefab, muzzle.position, Quaternion.identity);
            // play sfx water shoot
        }
    }
}
