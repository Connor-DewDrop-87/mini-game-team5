using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using MiniGameCollection;

public class Team5Player1 : MonoBehaviour
{
    //Global variables - any function in this script can access them
    //public means any other script can acccess it, and it's
    //visible in the editor!
    public float moveSpeed;

    // Establishing dashing mechanic
    public float dashSpeed = 10f;
    public float dashTime = 2f;
    public float dashCooldown = 2f;
    private bool canDash = true;
    public TMP_Text DashCooldownText;

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
        // Get joystick input from ArcadeInput
        Vector2 movement = ArcadeInput.Player1.Joystick8Way;

        // Move Player1
        transform.Translate(movement * moveSpeed * Time.deltaTime);

        // Dash Input
        if (ArcadeInput.Player1.Action1.Pressed && canDash)
        {
            StartCoroutine(DashMechanic());
            DashCooldownText.text = $"Dash Cooldown: {dashCooldown}";
        }
    }

    IEnumerator DashMechanic()
    {
        canDash = false;

        // Store original movement speed
        float originalSpeed = moveSpeed;

        // Increase movement speed
        moveSpeed = dashSpeed;

        // Wait 2 sec
        yield return new WaitForSeconds(dashTime);

        // Restore original speed
        moveSpeed = originalSpeed;

        // Wait for cooldown
        yield return new WaitForSeconds(dashCooldown);

        // Allow dash to be used again
        canDash = true;
    }

}
