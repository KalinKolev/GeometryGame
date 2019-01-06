using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SpawnBullets : MonoBehaviour
{

    public GameObject[] bulletTypes;


    public int misses=0;
    public int score=0;
    public int missesUntilGameover = 3;

    public Text scoreDisplay;
    public Text missesDisplay;
    public Text highscoreDisplay;

    public GameObject gameOverScreen;
    public Text gameOverTextScore;

    public bool isOver = false;

    int loadScore = 0;

    void Update()
    {
        scoreDisplay.text = "Score: " + score;
        missesDisplay.text = "Misses " + misses + "/" + missesUntilGameover;

        if (!isOver)
        {
            if (Input.GetKeyDown("1"))
            {
                GameObject bullet = Instantiate(bulletTypes[0], transform.position, Quaternion.identity);
                bullet.GetComponent<BulletMovement>().bulletType = "Circle";
            }
            if (Input.GetKeyDown("2"))
            {
                GameObject bullet = Instantiate(bulletTypes[1], transform.position, Quaternion.identity);
                bullet.GetComponent<BulletMovement>().bulletType = "Square";
                Destroy(bullet, 5f);
            }
            if (Input.GetKeyDown("3"))
            {
                GameObject bullet = Instantiate(bulletTypes[2], transform.position, Quaternion.identity);
                bullet.GetComponent<BulletMovement>().bulletType = "Triangle";
                Destroy(bullet, 5f);
            }
        }
        else
        {
            if (Input.GetKeyDown("space"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }

            if (loadScore == 0)
            {
                StartCoroutine(ManageScore());
                loadScore++;
            }
        }

        if (Input.GetKeyDown("escape"))
        {
            Application.Quit();
        }
    }

    IEnumerator ManageScore()
    {      
        int currentScoreToDisplay = 0;
        while (currentScoreToDisplay < score)
        {
            currentScoreToDisplay++;
            gameOverTextScore.text = "SCORE: " + currentScoreToDisplay;

            yield return new WaitForSeconds(.05f);            
        }

        if (score > PlayerPrefs.GetInt("GHS", 0)) PlayerPrefs.SetInt("GHS", score);

        currentScoreToDisplay = 0;
        while (currentScoreToDisplay < PlayerPrefs.GetInt("GHS",0))
        {
            currentScoreToDisplay++;
           highscoreDisplay.text = "HIGHSCORE: " + currentScoreToDisplay;

            yield return new WaitForSeconds(.05f);
        }
    }
}
