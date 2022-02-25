using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameUIBehaviourScript : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI difficultyText;
    public TextMeshProUGUI picksText;
    public TextMeshProUGUI skillText;
    public TextMeshProUGUI gameOverText;
    public GameObject gameOverUI;
    public LockpickBehaviourScript lockpickScript;
    public PlayerBehaviourScript player;

    // Start is called before the first frame update
    void Start()
    {
        lockpickScript = FindObjectOfType<LockpickBehaviourScript>();
        //timerText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (lockpickScript != null)
        {
            timerText.text = "Remaining Time: " + Mathf.RoundToInt(lockpickScript.curTime);
            picksText.text = "Remaining Picks: " + lockpickScript.curPicks;
            skillText.text = "Skill: " + player.skillString;
            difficultyText.text = "Difficulty: " + lockpickScript.lockDifficulty;

            if(lockpickScript.isUnlocked || lockpickScript.isFailed)
            {
                gameOverUI.SetActive(true);
                if (lockpickScript.isUnlocked)
                    gameOverText.text = "UNLOCKED!";
                else if (lockpickScript.isFailed)
                {
                    if (lockpickScript.failedReason == 1)
                    {
                        gameOverText.text = "TIME'S UP!";
                    }
                    else if (lockpickScript.failedReason == 2)
                    {
                        gameOverText.text = "NO PICKS!";
                    }
                    else
                        return;
                }
            }
            else
                gameOverUI.SetActive(false);
        }
        else
            return;
    }

    public void OnExitButtonClick()
    {
        Application.Quit();
    }

    public void OnRetryButtonClick()
    {
        if (lockpickScript != null)
            lockpickScript.Init(Mathf.Clamp(Mathf.RoundToInt(Random.Range(0, 5)) - player.playerSkill, 0, 5));
        else
            return;
    }
}
