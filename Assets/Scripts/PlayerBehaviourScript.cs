using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviourScript : MonoBehaviour
{
    public int playerSkill = 0;
    public string skillString;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        switch (playerSkill)
        {
            case 0:
                {
                    skillString = "Beginner";
                    break;
                }
            case 1:
                {
                    skillString = "Novice";
                    break;
                }
            case 2:
                {
                    skillString = "Intermediate";
                    break;
                }
            case 3:
                {
                    skillString = "Expert";
                    break;
                }
            case 4:
                {
                    skillString = "Legend";
                    break;
                }
            default:
                {
                    skillString = "Invalid";
                    break;
                }
        }
    }
}
