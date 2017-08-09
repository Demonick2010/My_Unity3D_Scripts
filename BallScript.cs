using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    private bool ballIsActive = false;
    private Vector3 ballPosition;
    private Vector2 ballInitialForce;

    [SerializeField]
    private float force_x = 100.0f;
    [SerializeField]
    private float force_y = 300.0f;

    public GameObject playerObject;

    public AudioClip hitSound;
    	
	void Start ()
    {
        ballInitialForce = new Vector2(force_x, force_y); // Создаём силу

        ballIsActive = false; // Переводим в неактивное состояние

        ballPosition = transform.position; // Запоминаем позицию
	}
	
	
	void Update ()
    {
		if (Input.GetKeyDown(KeyCode.Space) == true) // Проверка нажатия пробела
        {
            if (!ballIsActive) // Проверка состояния шарика
            {
                gameObject.GetComponent<Rigidbody2D>().AddForce(ballInitialForce); // Применяем силу

                ballIsActive = !ballIsActive; // Задаём активное состояние
            }
        }

        if (!ballIsActive && playerObject != null)
        {
            ballPosition.x = playerObject.transform.position.x; // Задаём новую позицию

            transform.position = ballPosition; // Устанавливаем новую позицию
        }

        if (ballIsActive && transform.position.y < -6)
        {
            ballIsActive = !ballIsActive;

            ballPosition.x = playerObject.transform.position.x;
            ballPosition.y = -4.38f;
            transform.position = ballPosition; // Устанавливаем новую позицию

            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

            playerObject.SendMessage("TakeLife");
        }
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (ballIsActive)
        {
            gameObject.GetComponent<AudioSource>().PlayOneShot(hitSound);
        }
    }
}
