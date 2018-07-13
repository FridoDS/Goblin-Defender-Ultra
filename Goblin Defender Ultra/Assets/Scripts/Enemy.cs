using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using System.Linq;

public class Enemy : MonoBehaviour {


    public List<Transform> targets; // each transform of fort wall
    public float attackDistance; // raycast distance for stop moving, then start attacking
    [SerializeField] private float attackAmount; // enemy damage to fort
    [SerializeField] private float enemyHealthPoint; // enemy HP

    private Fort fort; //fort
    private Animator animator;
    private AICharacterControl aicc;
    private int layerMask; // building layermask (fort)
    private Spawner spawner;
    private bool isAlive = true;


	// Use this for initialization
	void Start () {
        fort = GameObject.FindObjectOfType<Fort>().GetComponent<Fort>();
        spawner = GameObject.FindObjectOfType<Spawner>().GetComponent<Spawner>();
        animator = GetComponent<Animator>();
        layerMask = LayerMask.GetMask("Building");

        List<Transform> targets_ = GameObject.FindGameObjectWithTag("Wall").GetComponentsInChildren<Transform>().ToList();
        targets_.RemoveAt(0);
        foreach (Transform target in targets_)
        {
            if (target.tag == "Target")
            {
                targets.Add(target.GetComponentInChildren<Transform>());

            }
        }

        aicc = GetComponent<AICharacterControl>();
        aicc.SetTarget(targets[Random.Range(0, targets.Count - 1)]);
    }
	
	// Update is called once per frame
	void Update () {
        if (isAlive)
        {
            detectWall();
            Death();
        }
        

	}

    private void Attack()
    {
        //Enemy will attack the wall when raycast detected the wall/collider
        fort.reduceHP(attackAmount);

    }

    private void Death()
    {
        if (enemyHealthPoint <= 0)
        {
            aicc.agent.Stop();
            animator.SetTrigger("Death");
            spawner.CurrentEnemiesAmount -= 1;
            fort.addScore(1);
            isAlive = false;

        }
    }

    public void reduceHP(float amount)
    {
        enemyHealthPoint -= amount;
    }

    private void detectWall() // detect wall using raycast
    {
        RaycastHit hit;

        // if wall detected in certain distance, stop moving toward target and animate attack
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, attackDistance, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            aicc.agent.Stop();
            animator.SetBool("Attack", true);
            //hit.
            //Debug.Log("Did Hit");
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * attackDistance, Color.white);
            //Debug.Log("Did not Hit");
        }
    }
}
