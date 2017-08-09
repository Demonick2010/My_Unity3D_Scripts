using UnityEngine;
using System.Collections;

public class Human_01_Work : MonoBehaviour {

    public Vector3 home;
    public Vector3 target;
    public float distance;
    public Transform myTransform;

    public GameObject[] _mass;
    public float min;
    public float current;
    public int numberTarget;

    public bool findComplite = false;
    public bool WorkYes = false;
    public bool ResFull = false;

    public float timer;
    public NavMeshAgent agent;
	
	void Start ()
    {
        home = GameObject.FindGameObjectWithTag("CompleteUnit").transform.position;
        myTransform = gameObject.transform;
        agent = gameObject.GetComponent<NavMeshAgent>();
	}
	
	
	void Update ()
    {
	    if((findComplite == false) && (gameObject.tag == "CompleteUnit"))
        {
            FindWork();
            findComplite = true;
        }

        if((findComplite == true) && (WorkYes == true))
        {
            distance = Vector3.Distance(target, myTransform.position);

            if((distance >= 1f) && (ResFull == false))
            {
                agent.SetDestination(target);

                if(gameObject.GetComponent<HumanController>().typeAnim != HumanController.TTT.walk_1)
                {
                    gameObject.GetComponent<HumanController>().typeAnim = HumanController.TTT.walk_1;
                }
            }

            if((distance < 1f) && (ResFull == false))
            {
                timer += Time.deltaTime;
                if (timer > 5f)
                {
                    ResFull = true;

                    target = home;
                    distance = Vector3.Distance(target, myTransform.position);

                    timer = 0;

                    Destroy(_mass[numberTarget]);
                }

                myTransform.LookAt(target);

                    if(gameObject.GetComponent<HumanController>().typeAnim != HumanController.TTT.lum)
                    {
                        gameObject.GetComponent<HumanController>().typeAnim = HumanController.TTT.lum;
                    }
                }

                if ((distance >= 1f) && (ResFull == true))
                {
                    agent.SetDestination(target);

                    if (gameObject.GetComponent<HumanController>().typeAnim != HumanController.TTT.walk_2)
                    {
                        gameObject.GetComponent<HumanController>().typeAnim = HumanController.TTT.walk_2;
                    }
                }

                if ((distance < 1f) && (ResFull == true))
                {
                    ResFull = false;

                    FindWork();

                    distance = Vector3.Distance(target, myTransform.position);
                    agent.SetDestination(target);

                    if (gameObject.GetComponent<HumanController>().typeAnim != HumanController.TTT.idle)
                    {
                        gameObject.GetComponent<HumanController>().typeAnim = HumanController.TTT.idle;
                    }
                }
            }
        }
	


    void FindWork()
    {
        _mass = GameObject.FindGameObjectsWithTag("Tree");

        min = Vector3.Distance(gameObject.transform.position, _mass[0].transform.position);
        target = _mass[0].transform.position;
        numberTarget = 0;
        for (int i = 1; i < _mass.Length; i++)
        {
            current = Vector3.Distance(gameObject.transform.position, _mass[i].transform.position);
            if (current < min)
            {
                min = current;
                target = _mass[i].transform.position;
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawLine(gameObject.transform.position, home);
        Gizmos.DrawLine(gameObject.transform.position, target);
    }
}
