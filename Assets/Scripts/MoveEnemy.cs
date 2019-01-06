using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemy : MonoBehaviour
{
    public float speed = 0.25f;
    public float scorePerSpeedUp = 10f;
    public float speedUpAfterScore = 0.01f;
    public float extraSpeed;

    public string enemyType;

    public int misses = 0;

    public int score = 0;

    private void Start()
    {
        extraSpeed = Mathf.Floor(GameObject.FindGameObjectWithTag("Player").GetComponent<SpawnBullets>().score / scorePerSpeedUp) * speedUpAfterScore;
        speed += extraSpeed;
    }

    void Update()
    {        
        print(speed);

        float posX = transform.position.x - speed;
        transform.position = new Vector3(posX, transform.position.y, transform.position.z);

        if (GameObject.FindGameObjectWithTag("Player").GetComponent<SpawnBullets>().misses >= GameObject.FindGameObjectWithTag("Player").GetComponent<SpawnBullets>().missesUntilGameover)
        {
            GameOver();            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Destroy(gameObject);
            GameOver();
        }
        else
        {
            if (enemyType == collision.GetComponent<BulletMovement>().bulletType)
            {
                Destroy(collision.gameObject);
                Destroy(gameObject);
                GameObject.FindGameObjectWithTag("Player").GetComponent<SpawnBullets>().score += 1;
                score++;
            }
            else if(enemyType != collision.GetComponent<BulletMovement>().bulletType)
            {                
                GameObject.FindGameObjectWithTag("Player").GetComponent<SpawnBullets>().misses += 1;
                misses++;
                Destroy(collision.gameObject);
            }
        }
    }

    void GameOver()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<SpawnBullets>().missesDisplay.enabled = false;
        GameObject.FindGameObjectWithTag("Player").GetComponent<SpawnBullets>().scoreDisplay.enabled = false;        

        GameObject.FindGameObjectWithTag("Player").GetComponent<SpawnBullets>().isOver = true;
        GameObject.FindGameObjectWithTag("Spawner").GetComponent<EnemySpawner>().enabled = false;

        GameObject.FindGameObjectWithTag("Player").GetComponent<SpawnBullets>().gameOverScreen.SetActive(true);

        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Circle"))
        {
            obj.GetComponent<MoveEnemy>().enabled = false;
            obj.GetComponent<Animator>().enabled = false;
        }
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Square"))
        {
            obj.GetComponent<MoveEnemy>().enabled = false;
            obj.GetComponent<Animator>().enabled = false;
        }
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Triangle"))
        {
            obj.GetComponent<MoveEnemy>().enabled = false;
            obj.GetComponent<Animator>().enabled = false;
        }
    }    
}
