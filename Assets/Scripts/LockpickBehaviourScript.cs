using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockpickBehaviourScript : MonoBehaviour
{
    float pickRotation;
    float lockRotation;
    public bool isUnlocked = false;
    public bool isFailed = false;
    bool isShaking = false;
    [SerializeField]
    public float pickSpeed = 1f;
    public float lockRotationSpeed = 2f;
    public float lockRetentionSpeed = 1f;
    public float leniency = 0.1f;
    public float tensionRate = 1f;
    public float timeLimit = 90;
    public int totalPicks = 5;

    Animator animator;
    float targetPos;
    float tension = 0f;

    public float curTime;
    public string lockDifficulty;
    public int curPicks;
    public PlayerBehaviourScript player;
    public int failedReason = 0;
    
    float MaxOffset
    {
        get { return 1f - Mathf.Abs(targetPos - PickRotation) + leniency; } 
    }
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }


    public void Init(int difficulty)
    {
        Reset();

        switch(difficulty)
        {
            case 0:
                {
                    lockDifficulty = "Easy";
                    leniency = 0.4f;
                    break;
                }
            case 1:
                {
                    lockDifficulty = "Medium";
                    leniency = 0.2f;
                    break;
                }
            case 2:
                {
                    lockDifficulty = "Hard";
                    leniency = 0.1f;
                    break;
                }
            case 3:
                {
                    lockDifficulty = "Expert";
                    leniency = 0.05f;
                    break;
                }
            case 4:
                {
                    lockDifficulty = "Legendary";
                    leniency = 0.01f;
                    break;
                }
            default:
                {
                    lockDifficulty = "Invalid";
                    leniency = 0.2f;
                    print("Invalid difficulty");
                    break;
                }
        }
        targetPos = Random.Range(0f,1f);

        curTime = timeLimit;
        curPicks = totalPicks;
        isFailed = false;
        isUnlocked = false;
    }

    public float PickRotation
    {
        get { return pickRotation;  }
        set
        {
            pickRotation = value;
            pickRotation = Mathf.Clamp(pickRotation, 0f, 1f);
        }
    }
    public float LockRotation
    {
        get { return lockRotation; }
        set
        {
            lockRotation = value;
            lockRotation = Mathf.Clamp(lockRotation, 0f, MaxOffset);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerBehaviourScript>();
        Init(Mathf.Clamp(Mathf.RoundToInt(Random.Range(0,5)) - player.playerSkill,0,5));
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isUnlocked)
        { 


            return;
        }

        if(isFailed)
        {

            return;
        }

        if (Input.GetAxisRaw("Horizontal") == 0)
        {
            Pick();
        }
        
        Shaking();
        Cylinder();
        UpdateAnimator();

        curTime -= Time.deltaTime;
        if (curTime <= 0)
        {
            print("Time's up!");
            failedReason = 1;
            isFailed = true;

        }
        if (curPicks <= 0)
        {
            failedReason = 2;
            print("No Picks");
            isFailed = true;
        }
    }

    public void Reset()
    {
        LockRotation = 0;
        PickRotation = 0;
        tension = 0f;
        isUnlocked = false;
        
    }

    private void Shaking()
    {
        isShaking = MaxOffset - LockRotation < 0.03f;
        if(isShaking)
        {
            tension += Time.deltaTime * tensionRate;
            if(tension > 1f)
            {
                PickBreak();
                tension = 0f;
            }
        }
    }

    private void PickBreak()
    {
        print("Broke pick");
        curPicks--;
        Reset();
    }

    private void Cylinder()
    {
        LockRotation -= lockRetentionSpeed * Time.deltaTime;
        LockRotation += Mathf.Abs(Input.GetAxisRaw("Horizontal") * Time.deltaTime * lockRotationSpeed);
        if (LockRotation >= 0.999f)
        {
            Unlock();
        }
    }

    private void Unlock()
    {
        isUnlocked = true;
        print("Unlocked!");

    }


    private void Pick()
    {
        PickRotation += Input.GetAxisRaw("Mouse X") * Time.deltaTime * pickSpeed;
    }

    private void UpdateAnimator()
    {
        animator.SetFloat("PickRotation", pickRotation);
        animator.SetFloat("LockRotation", lockRotation);
        animator.SetBool("Shake", isShaking);
    }
}
