﻿using System;
using System.Collections;
using UnityEngine;

public class Spritzer : MonoBehaviour
{
    [SerializeField] 
    private float speed = 400f;
    
    [SerializeField] 
    private GameObject nuzzle;
    
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
            Transform transform1;
            (transform1 = transform).Translate(-speed * Time.deltaTime, 0, 0);
            Vector3 position = transform1.localPosition;
            position.x = Mathf.Clamp(position.x, -590.0f, 590.0f);
            transform.localPosition = position;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            // move right
            Transform transform1;
            (transform1 = transform).Translate(speed * Time.deltaTime, 0, 0);
            Vector3 position = transform1.localPosition;
            position.x = Mathf.Clamp(position.x, -590.0f, 590.0f);
            transform.localPosition = position;
        }

        shootTimer += Time.deltaTime;
        if (canShoot && shootTimer > coolDownTime && Input.GetKey(KeyCode.Space))
        {
            shootTimer = 0f;

            Instantiate(bulletPrefab, nuzzle.transform.position, Quaternion.identity, bulletSpawn.transform);
            StartCoroutine(NuzzlePress());
            // play sfx water shoot
        }
    }
    
    internal void DestroySelf()
    {
        //gameObject.SetActive(false);
        Destroy(gameObject);
    }

    public void SetBulletSpawn(GameObject bulletSpawnObject)
    {
        bulletSpawn = bulletSpawnObject;
    }

    public void SetCanShoot(bool set)
    {
        canShoot = set;
    }

    private IEnumerator NuzzlePress()
    {
        for (int i = 0; i < 5; i++)
        {
            nuzzle.transform.Translate(500 *Time.deltaTime * Vector2.left);
            
            yield return new WaitForSeconds(.05f);
        }
        
        for (int i = 0; i < 10; i++)
        {
            nuzzle.transform.Translate(250 *Time.deltaTime * Vector2.right);
            
            yield return new WaitForSeconds(.05f);
        }
    }
}
