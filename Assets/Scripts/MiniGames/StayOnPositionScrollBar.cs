using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StayOnPositionScrollBar : MonoBehaviour
{
    [SerializeField] float targetSpeed=5;
    [SerializeField] Scrollbar stayOnPosScroller;
    [SerializeField] float smoothMotion;

    float timerRandomizer;

    float wantedPosition;
    float stayInSingleSpotTimer;

    float targetNewRandYVal;
    private RectTransform stayOnPosScrollerRectTransform;

    // Start is called before the first frame update
    void Start()
    {
        stayOnPosScrollerRectTransform = stayOnPosScroller.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveTarget();
    }

   void MoveTarget()
    {
        stayInSingleSpotTimer -= Time.deltaTime;
        if (stayInSingleSpotTimer <= 0)
        {
            //get new pos
            //reset timer
            stayInSingleSpotTimer= Random.value* timerRandomizer;
            targetNewRandYVal = Random.value;
        }
        wantedPosition = Mathf.SmoothDamp(stayOnPosScroller.value, targetNewRandYVal, ref targetSpeed, smoothMotion);
        stayOnPosScroller.value = Mathf.Lerp(0, 1, wantedPosition);
    }
}
