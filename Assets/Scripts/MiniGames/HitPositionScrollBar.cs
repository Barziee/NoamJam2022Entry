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
    [SerializeField] float magnitude;
    [SerializeField] float offset=0;
    [SerializeField] BoxCollider2D targetCollider;

    Vector3 arrowStartPosition;
    private RectTransform scrollerRectTransform;

    public Action<bool> playerPressedHit;

    private void Start()
    {
        arrowStartPosition= arrowGO.transform.localPosition;
        magnitude = GetComponent<RectTransform>().sizeDelta.y / 2;
        scrollerRectTransform = targetScroll.GetComponent<RectTransform>();
    }
     
    public void init(Sprite arrowVisualSprite=null,float freq=0,float off=0,float targetYPos=0, float targetSize=0)
    {
        if (freq > 0)
            frequency = freq;
        if (off > 0)
            offset = off;

        if (arrowVisualSprite != null)
            arrowSprite = arrowVisualSprite;

        if (targetYPos > 0)
            targetScroll.value = targetYPos;

        if (targetSize > 0)
        {
            targetScroll.size = targetSize;
            targetCollider.size =new Vector2(targetCollider.size.x,targetSize* scrollerRectTransform.sizeDelta.y);
        }



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
