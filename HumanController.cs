using UnityEngine;
using System.Collections;

public class HumanController : MonoBehaviour {

    public enum TTT
    {
        idle,
        lum,
        walk_1,
        walk_2
    }
    public TTT typeAnim;
    public Animator _animator;

    public GameObject _bag;
    public GameObject _hand;
    public GameObject _tood;
	
	void Start ()
    {
        typeAnim = TTT.idle;
        _animator = gameObject.GetComponent<Animator>();
	}
	
	
	void Update ()
    {
	    switch(typeAnim)
        {
            case TTT.idle:
                _animator.SetBool("Idle", true);
                _animator.SetBool("Walk", false);
                _animator.SetBool("Lum", false);

                _hand.SetActive(false);
                _bag.SetActive(false);
                _tood.SetActive(true);

                break;

            case TTT.walk_1:
                _animator.SetBool("Idle", false);
                _animator.SetBool("Walk", true);
                _animator.SetBool("Lum", false);

                _hand.SetActive(false);
                _bag.SetActive(false);
                _tood.SetActive(true);

                break;

            case TTT.walk_2:
                _animator.SetBool("Idle", false);
                _animator.SetBool("Walk", true);
                _animator.SetBool("Lum", false);

                _hand.SetActive(false);
                _bag.SetActive(true);
                _tood.SetActive(true);

                break;

            case TTT.lum:
                _animator.SetBool("Idle", false);
                _animator.SetBool("Walk", false);
                _animator.SetBool("Lum", true);

                _hand.SetActive(false);
                _bag.SetActive(true);
                _tood.SetActive(false);

                break;
        }
	}
}
