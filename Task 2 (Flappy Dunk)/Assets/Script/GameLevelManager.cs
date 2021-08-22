using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLevelManager : MonoBehaviour
{
    [SerializeField]
    int score;
    
    [SerializeField]
    int combo;

    public int scoreToIncreaseRingRotation;
    public int rotateIncrementPerThreshold = 10;

    int scoreTreshold;
    int difficultyLevelThreshold;

    public Text comboText;
    public Text scoreText;

    public Animator background;
    public Animator border;

    AudioManager audioManager;

    GameObject ball;

    public GameObject gameOverIcon;

    bool isGameOver;

    private void Start()
    {
        FindObjectOfType<Spawner>().increaseRingCollectionIndex();
        difficultyLevelThreshold = 0;
        score = 0;
        combo = 1;
        ball = GameObject.FindGameObjectWithTag("Player");
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void Update()
    {
        //increase max radius each time score reach certain point
        if (scoreTreshold > scoreToIncreaseRingRotation) {
            scoreTreshold = 0;
            difficultyLevelThreshold += 1;
            FindObjectOfType<Spawner>().increaseRotation(rotateIncrementPerThreshold);
        }

        //increase max prefab index each time score reach certain point
        if (difficultyLevelThreshold > 5) {
            difficultyLevelThreshold = 0;
            FindObjectOfType<Spawner>().increaseRingCollectionIndex();
        }

        scoreText.text = score.ToString();

        if (combo > 1)
        {
            comboText.text = "SWIFT!!!" +"\n"+"x" + combo.ToString();
        }
        else {
            comboText.text = "";
        }
    }

    //add score depending on combo
    public void AddScore() {
        if (combo > 1)
        {
            audioManager.Play("ScoreCombo");
        }
        else {
            audioManager.Play("ScoreNormal");
        }

        score += combo;

        scoreTreshold += combo;
    }

    //add combo
    public void AddCombo(int comboToAdd) {
        combo += comboToAdd;
    }

    //reset combo back to 1
    public void RestCombo() {
        combo = 1;
    }

    //func to make game over
    public void GameOver() {
        if (!isGameOver) {
            gameOverIcon.SetActive(true);
            isGameOver = true;
            audioManager.Play("Whistle");

            //background.SetTrigger("Stop");
            border.SetTrigger("Stop");
            FindObjectOfType<Spawner>().stopMovement();

            ball.GetComponent<BBall>().enabled = false;
            ball.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        }
      
    }
}
