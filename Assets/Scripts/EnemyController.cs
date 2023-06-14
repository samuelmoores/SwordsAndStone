using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Transform routeOne;
    public List<Transform> wayPoints;
    private NavMeshAgent agent;
    public Animator animator;
    public Transform Player;
    PlayerController playerController;
    ColdronController coldronController;

    public int locationIndex = 0;
    bool playerDetected = false;
    public float health = 100.0f;
    public bool isDead = false;
    float attackCoolDown = 2.6f;
    bool incrementingLocationIndex = true;
    bool attack = false;


    // Start is called before the first frame update
    void Start()
    {
        routeOne = GameObject.Find("PatrolRoute").GetComponent<Transform>();
        playerController = GameObject.Find("CastleGuard").GetComponent<PlayerController>();
        Player = GameObject.Find("CastleGuard").transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        coldronController = GameObject.Find("Coldrun").GetComponent<ColdronController>();

        InitializePatrolRoute();

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(attackCoolDown);

        if (!isDead && !playerController.isDead)
        {
            
            if(coldronController.cauldronUsed)
            {
                agent.destination = Player.position;
                agent.updateRotation = true;
            }else
            {
                if(agent.remainingDistance < 1.0f && !agent.pathPending)
                    MoveToNextPatrolLocation();
            }
            
            if (Vector3.Distance(transform.position, playerController.transform.position) < 1.5f && !playerController.isDead)
            {

                animator.SetBool("playerDetected", true);

                agent.speed = 0.0f;


                attackCoolDown -= Time.deltaTime;

                if(attackCoolDown > 1.2f && attackCoolDown < 1.4f && !playerController.isBlocking)
                {
                    playerController.TakeDamage(1.0f);
                }

                if(attackCoolDown <= 0.0f)
                {
                    attackCoolDown = 2.6f;
                }

            }else if(attackCoolDown < 2.6f && attackCoolDown >= 0.1f)
            {
                attackCoolDown -= Time.deltaTime;
                agent.speed = 0.0f;

            }
            else if(attackCoolDown <= 0.1f)
            {
                agent.speed = 1.0f;
                attackCoolDown = 2.6f;
                animator.SetBool("playerDetected", false);

            }

        }
        


        if (health <= 0)
        {
            health = 0.0f;
            isDead = true;
            animator.SetBool("isDead", true);
            Destroy(this.gameObject, 15.0f);

        }

        if(playerController.isDead)
        {
            playerDetected = false;
            animator.SetBool("playerDead", true);
        }

    }

    public void takeDamage(float damageAmount)
    {
        health -= damageAmount;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {

            if(!playerController.isDead && coldronController.cauldronUsed)
            {
                playerDetected = true;
            }
          
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerDetected = false;

        }
    }

    void InitializePatrolRoute()
    {
        locationIndex = 0;

        for(int i = 0; i < routeOne.childCount; i++)
        {
            wayPoints.Add(routeOne.GetChild(i).transform);

        }

        agent.speed = 1.0f;
        agent.destination = wayPoints[0].position;
    }

    void MoveToNextPatrolLocation()
    {
        if(incrementingLocationIndex)
        {
            locationIndex++;
            agent.destination = wayPoints[locationIndex].position;

            if(locationIndex == wayPoints.Count - 1)
            {
                incrementingLocationIndex = false;
            }

        }else
        {
            locationIndex--;
            agent.destination = wayPoints[locationIndex].position;
            if(locationIndex == 0)
            {
                incrementingLocationIndex = true;
            }
        }

    }



}
