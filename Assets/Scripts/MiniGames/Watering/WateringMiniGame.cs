using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringMiniGame : Minigame
{
   [SerializeField] HitPositionScrollBar hitPositionScrollBar;
    [SerializeField] Animator animator;
    [SerializeField] Transform[] hpDrops;
    [SerializeField] int amountOfRounds=5;

    public override void Init(int seconds,int score,int lives, Action onTimerDone = null,Action<Minigame, bool> OnGameEnded=null)
    {
        base.Init(seconds,score,lives,
            () =>
            {
                
                hitPositionScrollBar.init();
                onTimerDone?.Invoke();

            }, OnGameEnded);
    }
    
    public override int GetMiniGameType()
    {
        return 2;
    }

    private void OnEnable()
    {
        hitPositionScrollBar.playerPressedHit += PlayerTriedToScore;
    }
    private void OnDisable()
    {
        hitPositionScrollBar.playerPressedHit -= PlayerTriedToScore;
    }

    void PlayerTriedToScore(bool didHit)
    {
        Debug.Log($"player tried to hit and got {didHit}");
        if (!didHit)
        {
            UpdateVisualLives();
            ReduceLife();
        }
        else
        {
            animator.Play("Watering_On");
            StartCoroutine(TimerToStopWateringAnim());
            IncreaseScore(5);
        }
        amountOfRounds--;

        if (amountOfRounds == 0)
        {
            EndGame();
            return;
        }

        setNextLevelHitBar();
    }

    private void UpdateVisualLives()
    {
        hpDrops[lives-1].GetChild(0).gameObject.SetActive(true);
    }

    private IEnumerator TimerToStopWateringAnim()
    {
        yield return new WaitForSeconds(3);
        animator.Play("Watering_Off");

    }

    private void setNextLevelHitBar()
    {
      float randFreq =  UnityEngine.Random.Range(1, 5);
     
      float targetRandSize = UnityEngine.Random.Range(0.05f, 0.5f);
        float halfSize = targetRandSize/2;
        float targetRandYPos = UnityEngine.Random.Range(halfSize, 1- halfSize);
        hitPositionScrollBar.CreateNewBar(freq: randFreq,targetSize: targetRandSize,targetYPos: targetRandYPos);
    }

    public override void EndGame()
    {
        animator.Play("Watering_Off");
        base.EndGame();
       hitPositionScrollBar.CloseSelf();

        amountOfRounds = 5;
        foreach (Transform child in hpDrops)
        {
            child.GetChild(0).gameObject.SetActive(false);
        }
    }


    public override void CloseSelf()
    {
        base.CloseSelf();
    }
}
