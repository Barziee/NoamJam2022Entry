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

    private Tree currentTree;
    private int currentTreeScore = 0;

    private int treesFixed = 0;

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

        currentTree = FindObjectOfType<Tree>();
    }


    public void MiniGameClicked(Minigame miniGame)
    {
        if (isInGame == false)
        {
            int gameType = miniGame.GetMiniGameType();

            switch (gameType)
            {
                case 1:
                    if (currentTree.playedBranch)
                        return;
                    break;
                case 2:
                    if (currentTree.playedWater)
                        return;
                    break;
                case 3:
                    if (currentTree.playedInsect)
                        return;
                    break;
                default:
                    break;
            }
            
            //barzie anim
            SoundManager.Instance.PlayAudioEffectOnce(openMiniGame);
            miniGame.gameObject.SetActive(true);
        
            isInGame = true;
            miniGame.Init(TIMER_COUNTDOWN_SECONDS,0, PLAYER_MINIGAME_LIVES,OnCountdownTimerEnded, OnMiniGameEnded);    
        }
        
    }

    void OnCountdownTimerEnded() { }

    void OnMiniGameEnded(Minigame miniGame, bool win)
    {
        int gameType = miniGame.GetMiniGameType();

        switch (gameType)
        {
            case 1:
                currentTree.playedBranch = true;
                break;
            case 2:
                currentTree.playedWater = true;
                break;
            case 3:
                currentTree.playedInsect = true;
                break;
            default:
                break;
        }
        
        miniGame.CloseSelf();
        isInGame = false;
        if (!win)
        { 
            // player lost life
            currentLives--;
            if (currentLives == 0)
            {
                // game over   
            }
        }
        else
        {
            int victoryType = gameType;
            // player won minigame
            currentTree.IncreaseScore();
            if (currentTree.GetScore() == Tree.MAX_SCORE)
                victoryType = 4;
            switch (victoryType)
            {
                case 1:
                    currentTree.BranchGameWin();
                    break;
                case 2:
                    currentTree.WaterGameWin();
                    break;
                case 3:
                    currentTree.InsectGameWin();
                    break;
                case 4:
                    currentTree.TreeFullyHealed();
                    break;
                default:
                    break;
            }
        }

        if (currentTree.playedAllGames())
        {
            // get new tree;
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
