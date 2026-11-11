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

    //Rigidbody2D is a component type variable.
    private Rigidbody2D rb2d;

    //Unity Animator
    private Animator anim;

    private SpriteRenderer sr;

    public TagSystemManager tagManager;

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
        // Get joystick input from ArcadeInput
        Vector2 movement = ArcadeInput.Player1.Joystick8Way;

        // Move Player2
        transform.Translate(movement * moveSpeed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player2"))
        {
            tagManager.SwitchTagger();
        }
    }
}
