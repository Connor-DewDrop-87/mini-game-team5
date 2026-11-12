using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using MiniGameCollection;

public class Player1 : MonoBehaviour
{
    //Global variables - any function in this script can access them
    //public means any other script can acccess it, and it's
    //visible in the editor!
    public float moveSpeed;

    // Establishing dashing mechanic
    public float dashSpeed = 10f;
    float originalSpeed = 0;
    public float currentDashTime = 2f;
    public float dashTime = 2f;
    public float dashCooldown = 2f;
    private bool canDash = true;
    GameObject cooldownUI;
    // Minigame BS
    public MiniGameManager minigameManager;

    //Rigidbody2D is a component type variable.
    private Rigidbody2D rb2d;

    //Unity Animator
    private Animator anim;

    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        //Initialize the rb2d variable
        rb2d = GetComponent<Rigidbody2D>();

        //Initialize Animator
        anim = GetComponent<Animator>();

        sr = GetComponent<SpriteRenderer>();

        // Find CooldownUI
        cooldownUI = GameObject.Find("RechargeBarRunner");
    }

    // Move Player1 (the runner)
    void Update()
    {
        HandleMovement();
        if (ArcadeInput.Player1.Action1.Pressed && canDash==true)
        {
            StartCoroutine(DashMechanic());
        }
        ShowDashMeter();
    }

    // Handle player input and movement
    void HandleMovement()
    {
        // Return early if game isn't even running yet
        if (minigameManager && minigameManager.State != MiniGameManagerState.TimerRunning) return;
        
        // Get joystick input from ArcadeInput
        Vector2 movement = new Vector2(ArcadeInput.Player1.AxisY, -ArcadeInput.Player1.AxisX);

        // Move Player2
        transform.Translate(movement * moveSpeed * Time.deltaTime);
    }

    void ShowDashMeter()
    {
        // If the Player can Dash, the UI is handled here
        // Otherwise, it is based on the current dash time 
        if (canDash == true)
        {
            cooldownUI.transform.localScale = new Vector3(0.25f, 1, 1);
        }
        else
        {
            if (moveSpeed==originalSpeed) // When on Cooldown
            {
                currentDashTime += Time.deltaTime;
            }
            else // When Dashing
            {
                currentDashTime -= Time.deltaTime;
            }
            float dashPercent = (currentDashTime * 1f / dashTime * 1f);
            cooldownUI.transform.localScale = new Vector3(0.25f, dashPercent, 1);
        }
    }

    IEnumerator DashMechanic()
    {
        canDash = false;

        // Store original movement speed
        originalSpeed = moveSpeed;

        // Increase movement speed
        moveSpeed = dashSpeed;
        // Wait 2 sec
        
        yield return new WaitForSeconds(dashTime);

        // Restore original speed
        moveSpeed = originalSpeed;

        // Wait for cooldown
        currentDashTime += Time.deltaTime;
        yield return new WaitForSeconds(dashCooldown);

        // Allow dash to be used again
        canDash = true;
    }
}
