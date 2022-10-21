using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitPositionScrollBar : MonoBehaviour
{
    [SerializeField] GameObject arrowGO;
    [SerializeField] Scrollbar targetScroll;
    [SerializeField] Sprite arrowSprite;
    [SerializeField] float frequency=5f;
    [SerializeField] float magnitude=100f;
    [SerializeField] float offset=0;

    Vector3 arrowStartPosition;

    public Action<bool> playerPressedHit;

    private void Start()
    {
        arrowStartPosition= arrowGO.transform.localPosition;
    }
     
    public void init(Sprite arrowVisualSprite=null,float freq=0,float mag=0,float off=0,float targetYPos=0, float targetSize=0)
    {
        if (freq > 0)
            frequency = freq;
        if (mag > 0)
            magnitude = mag;
        if (off > 0)
            offset = off;

        if (arrowVisualSprite != null)
            arrowSprite = arrowVisualSprite;

        if (targetYPos > 0)
            targetScroll.value = targetYPos;

        if (targetSize > 0)
            targetScroll.size = targetSize;



        ShouldArrowMove = true;
    }

    internal void CloseSelf()
    {
        ShouldArrowMove = false;
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
        RaycastHit2D hit = Physics2D.Raycast(arrowGO.transform.position,Vector2.left);
        Debug.DrawRay(arrowGO.transform.position, Vector3.left- arrowGO.transform.position, Color.green);
        // If it hits something...

        playerPressedHit?.Invoke(hit.collider != null && hit.collider.name == "Target");
    }

    void moveArrow()
    {
        if (ShouldArrowMove)
        {
            float sinFuncValue = Mathf.Sin(Time.time * frequency + offset);
            arrowGO.transform.localPosition = arrowStartPosition + transform.up * sinFuncValue * magnitude;
        }
    
    }

    public bool ShouldArrowMove { get; set; } = false;
    

    
}
