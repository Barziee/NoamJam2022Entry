using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SessionManager : MonoBehaviour
{
    [SerializeField] AudioClip openMiniGame;
    [SerializeField] private HorizontalLayoutGroup heartsGroup;
    private List<GameObject> heartsList;

    const int TIMER_COUNTDOWN_SECONDS=3;
    const int PLAYER_MINIGAME_LIVES = 3;

    private int currentLives = PLAYER_MINIGAME_LIVES;
    
    private bool isInGame = false;

    // Start is called before the first frame update
    void Start()
    {
        AssignHeartsList();
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
            LoseLives(difference);
            currentLives = lives;

            if (currentLives == 0)
            {
                // game over   
            }
         }
    }

    private void LoseLives(int livesLost)
    {
        for (int i = PLAYER_MINIGAME_LIVES - currentLives; i < livesLost; i++)
        {
            HeartBreak(heartsList[i]);
        }
    }

    private void HeartBreak(GameObject heart)
    {
        // transition heart to broken animation state
    }

    public void OnExitButtonClick()
    {
        Application.Quit();
    }

    private void AssignHeartsList()
    {
        List<Transform> tempList = heartsGroup.GetComponentsInChildren<Transform>().ToList();
        heartsList = new List<GameObject>();
        foreach (Transform t in tempList.Where(t => t.gameObject.GetComponent<Animator>()))
        {
            heartsList.Add(t.gameObject);
        }
    }
}
