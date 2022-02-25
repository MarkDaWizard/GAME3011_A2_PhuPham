using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUIBehaviourScript : MonoBehaviour
{
    public PlayerBehaviourScript player;
    public GameObject gameUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDiff0ButtonClick()
    {
        player.playerSkill = 0;
        gameObject.SetActive(false);
        gameUI.SetActive(true);
    }
    public void OnDiff1ButtonClick()
    {
        player.playerSkill = 1;
        gameObject.SetActive(false);
        gameUI.SetActive(true);
    }
    public void OnDiff2ButtonClick()
    {
        player.playerSkill = 2;
        gameObject.SetActive(false);
        gameUI.SetActive(true);
    }
    public void OnDiff3ButtonClick()
    {
        player.playerSkill = 3;
        gameObject.SetActive(false);
        gameUI.SetActive(true);
    }
    public void OnDiff4ButtonClick()
    {
        player.playerSkill = 4;
        gameObject.SetActive(false);
        gameUI.SetActive(true);
    }
}
