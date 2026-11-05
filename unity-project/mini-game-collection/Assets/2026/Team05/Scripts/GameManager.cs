using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private float privateTimer = 0;
    public TMP_Text timerTextP1;
    public TMP_Text timerTextP2;

    // Update is called once per frame
    void Update()
    {
        privateTimer += Time.deltaTime;
        UpdateTimer();
        GameOver();
    }

    private void UpdateTimer()
    {
        int min = Mathf.FloorToInt(privateTimer / 60);
        int sec = Mathf.FloorToInt(privateTimer % 60);
        int mil = Mathf.FloorToInt(privateTimer * 100) % 100;
        timerTextP1.text = "Time: " + min + ":" + sec + "." + mil;
        timerTextP2.text = "Time: " + min + ":" + sec + "." + mil;
    }
    private void GameOver()
    {
        if (privateTimer == 120)
        {
            SceneManager.LoadScene("GameOver");
        }
    }

}