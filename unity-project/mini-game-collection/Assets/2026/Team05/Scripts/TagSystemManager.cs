using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Data;

public class TagSystemManager : MonoBehaviour
{
    public int whoisit = 0;
    public TMP_Text Player1Status;
    public TMP_Text Player2Status;

    private bool tagStatus = true;
    private float cooldown = 1f;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void SwitchTagger()
    {
        if (!tagStatus)
            return;

        whoisit = whoisit == 0 ? 1 : 0;

        UpdateStatus();

        StartCoroutine(TagCooldown());
    }

    // Update is called once per frame
    void UpdateStatus()
    {
        if (whoisit == 0)
        {
            Player1Status.text = "Runner";
            Player2Status.text = "Tagger";
        }
        else
        {
            Player2Status.text = "Runner";
            Player1Status.text = "Tagger";
        }
    }

    IEnumerator TagCooldown()
    {
        tagStatus = false;

        yield return new WaitForSeconds(cooldown);

        tagStatus = true;
    }
}