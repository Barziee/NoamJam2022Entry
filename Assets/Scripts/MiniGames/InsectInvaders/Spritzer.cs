using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spritzer : MonoBehaviour
{
    [SerializeField] 
    private float speed = 100f;
    
    [SerializeField] 
    private GameObject nuzzle;
    
    [SerializeField] 
    private float coolDownTime = 0.5f;

    [SerializeField] 
    private WaterBullet bulletPrefab;

    private List<WaterBullet> bulletsList = new List<WaterBullet>();
    
    public GameObject bulletSpawn;

    private float shootTimer;

    private bool canShoot = false;
    private float maxX = 590f; //710f;
    private float minX = -590f; //106f;
    private float multipleByPixels = 100f;
    
    private void Update()
    {
        float newX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        newX *= multipleByPixels;
        Vector3 pos = transform.localPosition;
        pos.x = Mathf.Clamp(newX, minX, maxX);
        transform.localPosition = pos;
        
        shootTimer += Time.deltaTime;
        if (canShoot && shootTimer > coolDownTime && Input.GetMouseButton(0))
        {
            shootTimer = 0f;
            StartCoroutine(NuzzlePress());
        }
    }
    
    internal void DestroySelf()
    {
        gameObject.SetActive(false);
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
        WaterBullet waterBulletObj = Instantiate(bulletPrefab, nuzzle.transform.position, Quaternion.identity, bulletSpawn.transform);
        bulletsList.Add(waterBulletObj);
        
        // play sfx water shoot
        
        for (int i = 0; i < 5; i++)
        {
            nuzzle.transform.Translate(50 * Time.deltaTime * Vector2.left);
            yield return new WaitForSeconds(.05f);
        }
        
        for (int i = 0; i < 10; i++)
        {
            nuzzle.transform.Translate(25 * Time.deltaTime * Vector2.right);
            yield return new WaitForSeconds(.05f);
        }
    }

    public void RemoveBulletFromList(WaterBullet bullet)
    {
        if(!bullet) return;
        if (bulletsList.Contains(bullet))
        {
            bulletsList.Remove(bullet);
        }
    }

    public List<WaterBullet> GetWaterBulletList()
    {
        return bulletsList;
    }

}
