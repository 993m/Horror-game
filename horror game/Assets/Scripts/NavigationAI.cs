using UnityEngine;
using UnityEngine.AI;
public class NavigationAI : MonoBehaviour
{
    Vector3 destinationPoint;
    public float walkRange = 500;
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
        playerScript = player.GetComponent<RespawnOrKillPlayer>();
    }

    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);

        if (!playerInAttackRange && !playerInSightRange) Patrol();
        if (!playerInAttackRange && playerInSightRange) Chase();
        if (playerInAttackRange)  Attack();
    }

    void Patrol()
    { 
        if (!walkPointSet)
        {
            agent.speed = 0;
            animator.SetFloat("Speed", 0);
            SearchForDestination();
        }
        if (walkPointSet)
        {
            agent.speed = walkingSpeed;
            animator.SetFloat("Speed", 0.1f);
            agent.SetDestination(destinationPoint);
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
        agent.speed = runningSpeed;
        animator.SetFloat("Speed", 0.6f);
        agent.SetDestination(player.transform.position);
    }

    void Attack()
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("2HAttack"))
        {
            animator.SetTrigger("Attack");
        }

        agent.SetDestination(transform.position);
        playerScript.attackPlayer();
    }
}