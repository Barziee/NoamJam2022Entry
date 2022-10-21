﻿using System;
using UnityEngine;

public class Spritzer : MonoBehaviour
{
    [SerializeField] 
    private float speed = 400f;
    
    [SerializeField] 
    private Transform muzzle;
    
    [SerializeField] 
    private float coolDownTime = 0.5f;

    [SerializeField] 
    private WaterBullet bulletPrefab;
    
    public GameObject bulletSpawn;

    private float shootTimer;

    private bool canShoot = false;
    
    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            // move left
            transform.Translate(-speed * Time.deltaTime, 0, 0);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            // move right
            transform.Translate(speed * Time.deltaTime, 0, 0);
        }

        shootTimer += Time.deltaTime;
        if (canShoot && shootTimer > coolDownTime && Input.GetKey(KeyCode.Space))
        {
            shootTimer = 0f;

            Instantiate(bulletPrefab, muzzle.position, Quaternion.identity, bulletSpawn.transform);
            // play sfx water shoot
        }
    }
    
    internal void DestroySelf()
    {
        gameObject.SetActive(false);
        DestroyImmediate(gameObject, true);
    }

    public void SetBulletSpawn(GameObject bulletSpawnObject)
    {
        bulletSpawn = bulletSpawnObject;
    }

    public void SetCanShoot(bool set)
    {
        canShoot = set;
    }
}
