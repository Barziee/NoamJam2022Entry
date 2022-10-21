using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPositionScrollBar : MonoBehaviour
{
    [SerializeField] GameObject arrowGO;
    [SerializeField] int arrowMovementSpeed;
    [SerializeField] Sprite arrowSprite;
    [SerializeField] float frequency=5f;
    [SerializeField] float magnitude=100f;
    [SerializeField] float offset=0;

    Vector3 arrowStartPosition;

    private void Start()
    {
        arrowStartPosition= arrowGO.transform.localPosition;
    }
     
    public void init(int speed,Sprite arrowVisualSprite,float freq,float mag,float off)
    {
        if (speed > 0)
            arrowMovementSpeed = speed;

        if (freq > 0)
            frequency = freq;
        if (mag > 0)
            magnitude = mag;
        if (off > 0)
            offset = off;

        if (arrowVisualSprite != null)
            arrowSprite = arrowVisualSprite;
    } 


    // Update is called once per frame
    void Update()
    {
        moveArrow();

        if (Input.GetMouseButtonDown(0))
        {
            ShootRayCast();
        }
           
    }

    private void ShootRayCast()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up);

        // If it hits something...
        if (hit.collider != null)
        {
            Debug.Log(hit.collider.name);
        }
    }

    void moveArrow()
    {
        float sinFuncValue = Mathf.Sin(Time.time * frequency + offset);

        arrowGO.transform.localPosition = arrowStartPosition + transform.up * sinFuncValue * magnitude;
    }
}
