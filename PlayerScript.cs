using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    private float playerSpeed; // Скорость перемещения
    private Vector3 playerPosition; // Позиция платформы

    [SerializeField]
    private float boundary;

    private int playerLives;
    private int playerPoints;

    public AudioClip pointSound;
    public AudioClip lifeSound;

    void Start()
    {
        playerPosition = gameObject.transform.position; // Получаем начальную позицию

        playerLives = 3;
        playerPoints = 0;
    }


    void Update()
    {
        playerPosition.x += Input.GetAxis("Horizontal") * playerSpeed;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        transform.position = playerPosition; // Обновляем позицию

        if (playerPosition.x < -boundary)
        {
            transform.position = new Vector3(-boundary, playerPosition.y, playerPosition.z);
        }

        if (playerPosition.x > boundary)
        {
            transform.position = new Vector3(boundary, playerPosition.y, playerPosition.z);
        }

        WinLose();
    }

    void AddPoints(int points)
    {
        playerPoints += points;

        gameObject.GetComponent<AudioSource>().PlayOneShot(pointSound);
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(5.0f, 3.0f, 200.0f, 200.0f), "Live's: " + playerLives + "  Score: " + playerPoints);
    }

    void TakeLife()
    {
        playerLives--;

        gameObject.GetComponent<AudioSource>().PlayOneShot(lifeSound);
    }

    void WinLose()
    {
        if (playerLives == 0)
        {
            SceneManager.LoadScene("Level_1");
        }

        if ((GameObject.FindGameObjectsWithTag("Block")).Length == 0)
        {
            if (SceneManager.GetActiveScene().name == "Level_1")
            {
                SceneManager.LoadScene("Level_2");
            }
            else
            {
                Application.Quit();
            }
        }
    }
}