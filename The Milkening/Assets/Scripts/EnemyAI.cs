using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
public class EnemyAI : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    public float health;

    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    public float timeBetweenAttacks;
    bool alreadyAttacked;

    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    Animator animator;
    private Transform groundCheck;
    public bool isLunging;
    public Vector3 lungeDir;
    private Rigidbody rb;
    private float escapeTime = .1f;
    private float currentEscapeTime;
    private float lungeCoolDown = 1.5f;
    private float currentCoolDownTime;
    public float damage;
    public float damageCoolDown;
    public float damageTime;

    public GameObject milkSplash;
    public GameObject milkPuddle;
    public LayerMask floor;

    private void Awake()
    {
        animator = transform.GetChild(0).GetComponent<Animator>();
        player = GameObject.Find("Player").transform;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        groundCheck = transform.Find("Ground Check");
        rb = GetComponent<Rigidbody>();
        currentCoolDownTime = lungeCoolDown;
        damageTime = damageCoolDown;
    }

    public void Damage(float damage)
    {
        if (damageTime >= damageCoolDown)
        {
            health -= damage;
            damageTime = 0;
        }

        if (health <= 0)
            Die();
    }

    private void Update()
    {
        agent.enabled = true;
        currentCoolDownTime += Time.deltaTime;
        damageTime += Time.deltaTime;

        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        bool isGrounded = IsGrounded();

        if (!playerInSightRange && !playerInAttackRange && isGrounded && !isLunging) Patroling();
        if (playerInSightRange && !playerInAttackRange && isGrounded && !isLunging) ChasePlayer();
        if ((playerInAttackRange && playerInSightRange) || isLunging) AttackPlayer();
    }

    private void Die()
    {
        Instantiate(milkSplash, transform.position, Quaternion.identity);
        Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, floor);

        Vector3 point = hit.point;
        point.y += .01f;

        Instantiate(milkPuddle, point, Quaternion.Euler(90, 0, 0));

        Destroy(gameObject);
    }

    private void Patroling()
    {
        agent.enabled = true;
        animator.enabled = true;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

            
            

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }
    private void ChasePlayer()
    {
        agent.enabled = true;
        animator.enabled = true;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        agent.SetDestination(player.position);
    }
    private void AttackPlayer()
    {
        bool isGrounded = IsGrounded();
        Vector3 dir = (player.position - transform.position).normalized;
        agent.enabled = false;
        animator.enabled = false;

        if (isGrounded && currentCoolDownTime >= lungeCoolDown)
        {
            dir.y += .5f;

            rb.AddForce(dir * 250f);
            isLunging = true;
            currentEscapeTime = 0;
            currentCoolDownTime = 0;

            lungeDir = dir;
        }

        if (isLunging)
        {
            rb.angularVelocity = dir * 7;
            if (isGrounded && currentEscapeTime >= escapeTime)
                isLunging = false;
            currentEscapeTime += Time.deltaTime;
        }
    }
    private void LungeAttack()
    {
        Vector3 dir = (player.position - transform.position).normalized;
        agent.SetDestination(player.position + dir * 20f);
        agent.speed = 50f;
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
        animator.ResetTrigger("Attack");
        animator.SetTrigger("Walk");
        agent.speed = 6f;
    }
    public void TakeDamage(int damage)
    {
        animator.ResetTrigger("Walk");
        animator.ResetTrigger("Attack");
        animator.SetTrigger("Hurt");
        health -= damage;

        if (health <= 0) Invoke(nameof(DestroyEnemy), 0.5f);
    }
    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

    public bool ReachedDestinationOrGaveUp()
    {

        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    return true;
                }
            }
        }

        return false;
    }

    private bool IsGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, .15f, whatIsGround, QueryTriggerInteraction.Ignore);
    }
}
