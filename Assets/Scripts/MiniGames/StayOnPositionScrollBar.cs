using System;
using UnityEngine;
using UnityEngine.UI;

public class StayOnPositionScrollBar : MonoBehaviour
{
  
    [SerializeField] Scrollbar stayOnPosScroller;  // first scroller, where we have both the running target and player.
    [SerializeField] Scrollbar fillMeter;
    [SerializeField] float smoothMotion;
    [SerializeField] float timerRandomizer = 3;

    [Header("Target")]
    float targetWantedPosition;
    float stayInSingleSpotTimer;
    float targetSpeed;
    float targetNewRandXVal;
    private RectTransform stayOnPosScrollerRectTransform;
    private float pullScrollerYHeight;

    [Header("Player")]
    [SerializeField] RectTransform playerRectTransform;
    [SerializeField] float playerGravity = 0.5f;
    [SerializeField] float playerSpeed = 0.1f;
    [SerializeField] TriggerDetector triggerDetector;

    private float playerPullVelocity;
    float playerWantedPosition;

    bool isPlayerOnTarget;

    public Action<bool> OnRoundEnd;
    private bool finishedCounting;

    public void Init()
    {
        stayOnPosScrollerRectTransform = stayOnPosScroller.GetComponent<RectTransform>();
        pullScrollerYHeight = stayOnPosScrollerRectTransform.sizeDelta.y;
        triggerDetector.PlayerOnTarget += IsPlayerOnTarget;
        fillMeter.size = 0.5f;
  finishedCounting = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (finishedCounting)
        {
            MoveTarget();
            PlayerMove();
            UpdateFillMeter();
        }
    }

    void MoveTarget()
    {
        stayInSingleSpotTimer -= Time.deltaTime;
        if (stayInSingleSpotTimer <= 0)
        {
            //get new pos
            //reset timer
            stayInSingleSpotTimer = UnityEngine.Random.value * timerRandomizer;
            targetNewRandXVal = UnityEngine.Random.value;
        }
        targetWantedPosition = Mathf.SmoothDamp(stayOnPosScroller.value, targetNewRandXVal, ref targetSpeed, smoothMotion);
        stayOnPosScroller.value = Mathf.Lerp(0, 1, targetWantedPosition);
    }

    private void PlayerMove()
    {
        if (Input.GetMouseButton(0))
        {
            playerPullVelocity += playerSpeed * Time.deltaTime;
        }
        playerPullVelocity -= playerGravity * Time.deltaTime;

        playerWantedPosition += playerPullVelocity;

        playerWantedPosition = Mathf.Clamp(playerWantedPosition, playerRectTransform.sizeDelta.x / 2, stayOnPosScrollerRectTransform.sizeDelta.x - playerRectTransform.sizeDelta.x / 2);

        float newXPos = Mathf.Lerp(0, 1600, playerWantedPosition / 1600);

        if (playerRectTransform.anchoredPosition.x == playerRectTransform.sizeDelta.x / 2 && playerPullVelocity <= 0
            || playerRectTransform.anchoredPosition.x == stayOnPosScrollerRectTransform.sizeDelta.x && playerPullVelocity > 0)
            playerPullVelocity = 0;

        playerRectTransform.anchoredPosition = new Vector3(
        newXPos,
        0,
        0);
    }

    void IsPlayerOnTarget(bool isOnTarget)
    {
        isPlayerOnTarget = isOnTarget;
    }


  void  UpdateFillMeter()
    {
        if (isPlayerOnTarget)
        {
            if (fillMeter.size > 0.9f)
                OnRoundEnd?.Invoke(true);

                if (fillMeter.size <1)
            fillMeter.size += 0.12f * Time.deltaTime;
        }
        else
        {
            if(fillMeter.size<0.1f)
                OnRoundEnd?.Invoke(false);


            if (fillMeter.size > 0)
            fillMeter.size -= 0.2f * Time.deltaTime;
        }

    }

    public void ResetSelf()
    {
        fillMeter.size = 0.5f;
    }

    public void CloseSelf()
    {
        fillMeter.size = 0.5f;
        triggerDetector.PlayerOnTarget -= IsPlayerOnTarget;
        finishedCounting = false;
    }

}
