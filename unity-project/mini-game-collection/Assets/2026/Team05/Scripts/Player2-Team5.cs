using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using MiniGameCollection;

public class Player2 : MonoBehaviour
{
    //Global variables - any function in this script can access them
    //public means any other script can acccess it, and it's
    //visible in the editor!
    public float moveSpeed;

    //Rigidbody2D is a component type variable.
    private Rigidbody2D rb2d;

    //Unity Animator
    private Animator anim;

    private SpriteRenderer sr;

    public MiniGameManager minigameManager;

    // Moving Blocks
    GameObject moveableBlocks;
    bool top = false;
    bool right = false;
    public float maxCoolDown = 5;
    float currentCoolDown = 0;
    GameObject cooldownUI;
    // Start is called before the first frame update
    void Start()
    {
        //Initialize the rb2d variable
        rb2d = GetComponent<Rigidbody2D>();

        //Initialize Animator
        anim = GetComponent<Animator>();

        sr = GetComponent<SpriteRenderer>();

        // Get Moveable Block Object
        moveableBlocks = GameObject.Find("MoveableBlock");
        cooldownUI = GameObject.Find("RechargeBarTagger");
        MoveBlock();
    }

    // Move Player2 (the tagger)
    void Update()
    {
        HandleMovement();

        // Press Button 1 to move the Block to the Opposite Horizontal Side
        if (ArcadeInput.Player2.Action1.Pressed && currentCoolDown==0)
        {
            top = !top;
            MoveBlock();
            // Wait 5 seconds before using a button again
            currentCoolDown = maxCoolDown;
        }
        // Press Button 2 to move the Block to the Opposite Vertical Side
        if (ArcadeInput.Player2.Action2.Pressed && currentCoolDown == 0)
        {
            right = !right;
            MoveBlock();
            // Wait 5 seconds before using a button again
            currentCoolDown = maxCoolDown;
        }
        // Reduce Cooldown if the button was used
        if (currentCoolDown>0)
        {
            currentCoolDown -= Time.deltaTime;
            UpdateCoolDownUI();
            // Go to 0 if the CoolDown goes under
            if (currentCoolDown < 0)
            {
                currentCoolDown = 0;
            }
        }

    }
    
    // Handle player input and movement
    void HandleMovement()
    {
        // Return early if game isn't even running yet
        if (minigameManager && minigameManager.State != MiniGameManagerState.TimerRunning) return;

        // Get joystick input from ArcadeInput
        Vector2 movement = new Vector2(-ArcadeInput.Player2.AxisY, ArcadeInput.Player2.AxisX); ;

        // Move Player2
        transform.Translate(movement * moveSpeed * Time.deltaTime);
    }

    void MoveBlock()
    {
        Vector2 changePosition = new Vector2(0, 0);
        if (top==true)
        {
            changePosition.y = 4;
            if (right==true)
            {
                changePosition.x = 7;
            }
            else
            {
                changePosition.x = -7;
            }
        }
        else
        {
            changePosition.y = -4;
            if (right == true)
            {
                changePosition.x = 7;
            }
            else
            {
                changePosition.x = -7;
            }
        }

        moveableBlocks.transform.position = changePosition;
    }

    void UpdateCoolDownUI()
    {
        float cooldownpercent = ((maxCoolDown-currentCoolDown)/maxCoolDown);
        cooldownUI.transform.localScale = new Vector3(1, cooldownpercent, 1);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player1"))
        {
            // Set Player 2 as Winner
            minigameManager.Winner = MiniGameWinner.Player2;
            minigameManager.StopGame();
        }
    }
}
