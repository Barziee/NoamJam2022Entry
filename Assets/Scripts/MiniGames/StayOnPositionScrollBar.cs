using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StayOnPositionScrollBar : MonoBehaviour
{
    [SerializeField] Transform topBounds;
    [SerializeField] Transform bottomBounds;


    [SerializeField] Scrollbar stayOnPosScroller;
    [SerializeField] float smoothMotion;
    [SerializeField] float timerRandomizer=3;

    [Header("Target")]
    float targetWantedPosition;
    float stayInSingleSpotTimer;
    float targetSpeed;
    float targetNewRandXVal;
    private RectTransform stayOnPosScrollerRectTransform;
    private float pullScrollerYHeight;

    [Header("Player")]
    [SerializeField] RectTransform playerRectTransform;
    [SerializeField] float playerGravity=0.5f;
    [SerializeField] float playerSpeed=0.1f;


    private float playerPullVelocity;
    float playerWantedPosition;



    // Start is called before the first frame update
    void Start()
    {
        stayOnPosScrollerRectTransform = stayOnPosScroller.GetComponent<RectTransform>();
        pullScrollerYHeight = stayOnPosScrollerRectTransform.sizeDelta.y;

        Init();
    }

    public void Init()
    {
        CanPlayerMove = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MoveTarget();
        PlayerMove();
        

   void MoveTarget()
    {
        stayInSingleSpotTimer -= Time.deltaTime;
        if (stayInSingleSpotTimer <= 0)
        {
            //get new pos
            //reset timer
            stayInSingleSpotTimer= Random.value* timerRandomizer;
            targetNewRandXVal = Random.value;
        }
            targetWantedPosition = Mathf.SmoothDamp(stayOnPosScroller.value, targetNewRandXVal, ref targetSpeed, smoothMotion);
        stayOnPosScroller.value = Mathf.Lerp(0, 1, targetWantedPosition);
    }
}

    private void PlayerMove()
    {
        if (Input.GetMouseButton(0) && CanPlayerMove)
        {
            playerPullVelocity += playerSpeed * Time.deltaTime;
        }
        playerPullVelocity -= playerGravity * Time.deltaTime;

        playerWantedPosition += playerPullVelocity;

        playerWantedPosition = Mathf.Clamp(playerWantedPosition, playerRectTransform.sizeDelta.x / 2, stayOnPosScrollerRectTransform.sizeDelta.x- playerRectTransform.sizeDelta.x / 2);

        float newXPos = Mathf.Lerp(0, 1600, playerWantedPosition);



        playerRectTransform.anchoredPosition = new Vector3(
        newXPos,
        playerRectTransform.position.y,
        playerRectTransform.position.z);

      /*  playerWantedPosition += playerPullVelocity;

        float playerTopBound = pullScrollerYHeight / 2 - playerRectTransform.sizeDelta.y / 2;
        float plyerBottomBound =playerRectTransform.sizeDelta.y / 2;

       
        playerWantedPosition = Mathf.Clamp(playerWantedPosition, plyerBottomBound, playerTopBound);

        playerRectTransform.localPosition = Vector3.Lerp(topBounds.position, bottomBounds.position, playerWantedPosition);
*/
    
    }

    public bool CanPlayerMove { get; set; } = false;

}
