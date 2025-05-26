using UnityEngine;
using UnityEngine.AI;
using System;

public class EnemyAI : MonoBehaviour
{
    [Header("Patrol Settings")]
    public Transform[] patrolPoints;
    private int currentPatrolIndex = 0;
    private NavMeshAgent agent;

    [Header("Detection Settings")]
    public Transform player;
    public float detectionRange = 10f;
    public float shootingRange = 8f;

    [Header("Attack Settings")]
    public float fireRate = 1f; 
    public int damagePerShot = 10;
    private float nextFireTime = 0f;
    private bool isPlayerDetected = false;

    [Header("Health Settings")]
    public int maxHealth = 100;
    private int currentHealth;

    private Animator animator;

    public static event Action enemydead;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;

        if (patrolPoints.Length > 0)
        {
            agent.destination = patrolPoints[currentPatrolIndex].position;
        }
    }

    void Update()
    {
        if (currentHealth <= 0) return; 

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            isPlayerDetected = true;
        }
        else
        {
            isPlayerDetected = false;
        }

        if (isPlayerDetected)
        {
            HandleCombat(distanceToPlayer);
        }
        else
        {
            Patrol();
        }
    }

    void Patrol()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
            agent.destination = patrolPoints[currentPatrolIndex].position;
        }

        if (animator != null)
        {
            animator.SetBool("isWalking", true); 
        }
    }

    void HandleCombat(float distanceToPlayer)
    {
        agent.SetDestination(transform.position); 

        if (animator != null)
        {
            animator.SetBool("isWalking", false); 
            animator.SetTrigger("Shoot"); 
        }

        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z)); 

        if (distanceToPlayer <= shootingRange && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + (1f / fireRate);
        }
    }

    void Shoot()
    {
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damagePerShot);
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log("Enemy Health: "+ currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        enemydead?.Invoke();
        agent.enabled = false; 
        Destroy(gameObject); 
    }
}
