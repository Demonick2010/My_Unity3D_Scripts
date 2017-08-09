using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockScript : MonoBehaviour
{
    [SerializeField]
    private int hitsToKill;
    [SerializeField]
    private int points;
    private int numberOfHits;

    void Start ()
    {
        numberOfHits = 0;
	}
	
	void Update ()
    {
		
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            numberOfHits++;

            if(numberOfHits == hitsToKill)
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");

                player.SendMessage("AddPoints", points);

                Destroy(this.gameObject);
            }
        }
    }

    
}
