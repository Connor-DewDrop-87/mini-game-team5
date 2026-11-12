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
    }

    // Move Player1 (the runner)
    void Update()
    {
        HandleMovement();
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
}
