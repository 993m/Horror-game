using System.Collections;
using UnityEngine;
using UnityEngine.AI;
public class NavigationAI : MonoBehaviour
{
    Vector3 destinationPoint;
    public float walkRange = 200;
    bool walkPointSet = false;
    NavMeshAgent agent;
    public float runningSpeed = 6;
    public float walkingSpeed = 4;

    public GameObject player;
    public LayerMask playerLayer;
    public float sightRange = 40;
    public float attackRange = 2.5f;
    bool playerInSightRange, playerInAttackRange;

    Animator animator;
    RespawnOrKillPlayer playerScript;
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = GameObject.Find("Player");
        playerScript = player.GetComponent<RespawnOrKillPlayer>();
    }

    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);

        if (!playerInAttackRange && !playerInSightRange) Patrol();
        if (!playerInAttackRange && playerInSightRange) Chase();
        if (playerInAttackRange) Attack();
    }

    void Patrol()
    {
        if (!walkPointSet)
        {
            animator.SetFloat("Speed", 0);
            SearchForDestination();
        }
        else
        {
            agent.SetDestination(destinationPoint);
            if(agent.pathPending == false)
            {
                agent.speed = walkingSpeed;
                animator.SetFloat("Speed", 0.2f);
            }
        }    
        
        if (Vector3.Distance(transform.position, destinationPoint) < 10) walkPointSet = false;
    }

    void SearchForDestination()
    {
        Vector3 randomPoint = Random.insideUnitSphere * walkRange + transform.position;

        NavMeshHit hit;

        if(NavMesh.SamplePosition(randomPoint, out hit, walkRange, NavMesh.AllAreas))
        {
            destinationPoint = hit.position;
            walkPointSet = true;
        }
    }

    void Chase()
    {
        NavMeshPath path = new NavMeshPath();
        if(agent.CalculatePath(player.transform.position, path))
        {
            agent.SetPath(path);
            agent.speed = runningSpeed;
            animator.SetFloat("Speed", 1);
        }
    }

    void Attack()
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("2HAttack"))
        {
            animator.SetTrigger("Attack");
        }

        agent.SetDestination(transform.position);
        StartCoroutine(playerScript.attackPlayer());
    }
}