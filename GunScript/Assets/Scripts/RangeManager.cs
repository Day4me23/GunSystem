using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RangeManager : MonoBehaviour
{
    public Text text;
    public Text timerText;
    public Text playerScore;
    public GameObject playerPrefab;
    public GameObject reference;
    public static RangeManager instance;
    public float timer;
    public int points = 0;
    private bool inMiniGame = false;
    private bool miniGameRunning = false;

    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        float minutes = Mathf.FloorToInt(timer / 60);
        float seconds = Mathf.FloorToInt(timer % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        playerScore.text = points.ToString();
        if (inMiniGame)
        {
            if (!miniGameRunning)
            {
                text.text = "Press F to begin the minigame.";
                if (Input.GetKeyDown(KeyCode.F))
                {
                    miniGameRunning = true;
                    timer = 30;
                    points = 0;
                    reference = Instantiate(playerPrefab, new Vector3(Random.Range(5, 21), 2.5f, Random.Range(-2, -18)), Quaternion.identity);
                }
            }
            else
            {
                text.text = "";
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    timer = 0;
                    Destroy(reference);
                    miniGameRunning = false;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            inMiniGame = true;
        }
    }

    public void IncreaseScore()
    {
        points++;
    }
}
