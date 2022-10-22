using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SessionManager : MonoBehaviour
{
    [SerializeField] AudioClip openMiniGame;
    [SerializeField] private HorizontalLayoutGroup heartsGroup;

    const int TIMER_COUNTDOWN_SECONDS=3;
    const int PLAYER_MINIGAME_LIVES = 3;

    private int currentLives = PLAYER_MINIGAME_LIVES;
    
    private bool isInGame = false;

    // Start is called before the first frame update
    void Start()
    {
        SoundManager.Instance.MasterVolume = 1;
        SoundManager.Instance.MusicVolume = 1;
        SoundManager.Instance.EffectsVolume = 1;

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void MiniGameClicked(Minigame miniGame)
    {
        if (isInGame == false)
        {
            //barzie anim
            SoundManager.Instance.PlayAudioEffectOnce(openMiniGame);
            miniGame.gameObject.SetActive(true);
        
            isInGame = true;
            miniGame.Init(TIMER_COUNTDOWN_SECONDS,0, PLAYER_MINIGAME_LIVES,OnCountdownTimerEnded, OnMiniGameEnded);    
        }
        
    }

    void OnCountdownTimerEnded() { }

    void OnMiniGameEnded(Minigame miniGame, int lives)
    {
         miniGame.CloseSelf();
         isInGame = false;
         if (lives != currentLives)
         {
            // player lost life
            int difference = currentLives - lives;
            currentLives = lives;
            LoseLife(difference);
            
            if (currentLives == 0)
            {
                // game over   
             }
         }
    }

    private void LoseLife(int livesLost)
    {
         
    }

    public void OnExitButtonClick()
    {
        Application.Quit();
    }
}
