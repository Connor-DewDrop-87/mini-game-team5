using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
        }
    }
}
