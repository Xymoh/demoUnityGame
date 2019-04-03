using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public enum EnemyState
{
    PATROL,
    CHASE,
    ATTACK
}

public class EnemyControler : MonoBehaviour
{
    public float walkSpeed = 0.5f;
    public float runSpeed = 4f;
    public float chaseDistance = 7f;
    public float attackDistance = 1.8f;
    public float patrolRadiusMinimum = 20f;
    public float patrolRadiusMaximum = 60f;
    public float patrolForThisTime = 15f;
    public float waitBeforeAttack = 0.1f;
    public float chaseAfterAttackDistance = 2f;
    public GameObject attackPoint;

    [SerializeField]
    private AudioClip[] screamClip;

    [SerializeField]
    private AudioClip[] attackClips;

    private AudioSource audioSource;
    private EnemyAnimator enemyAnim;
    private NavMeshAgent navAgent;
    private EnemyState enemyState;
    private float currentChaseDistance;
    private float patrolTimer;
    private float attackTime;
    private Transform target;

    private void Awake()
    {
        enemyAnim = GetComponent<EnemyAnimator>();
        navAgent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();

        target = GameObject.FindWithTag("Player").transform;
    }

    void Start()
    {
        enemyState = EnemyState.PATROL;

        patrolTimer = patrolForThisTime;

        attackTime = waitBeforeAttack;

        currentChaseDistance = chaseDistance;
    }

    void Update()
    {
        if (enemyState == EnemyState.PATROL)
        {
            Patrol();
        }

        if (enemyState == EnemyState.CHASE)
        {
            Chase();
        }

        if (enemyState == EnemyState.ATTACK)
        {
            Attack();
        }
    }

    void Patrol()
    {
        navAgent.isStopped = false;
        navAgent.speed = walkSpeed;

        patrolTimer += Time.deltaTime;

        if (patrolTimer > patrolForThisTime)
        {
            SetNewRandomDestination();

            patrolTimer = 0f;
        }

        if (navAgent.velocity.sqrMagnitude > 0)
        {
            enemyAnim.Walk(true);
        }
        else
        {
            enemyAnim.Walk(false);
        }

        if (Vector3.Distance(transform.position, target.position) <= chaseDistance)
        {
            enemyAnim.Walk(false);

            enemyState = EnemyState.CHASE;

            PlayScreamSound();
        }
    }
   
    void Chase()
    {
        navAgent.isStopped = false;
        navAgent.speed = runSpeed;

        navAgent.SetDestination(target.position);

        if (navAgent.velocity.sqrMagnitude > 0)
        {
            enemyAnim.Run(true);
        }
        else
        {
            enemyAnim.Run(false);
        }

        if (Vector3.Distance(transform.position, target.position) <= attackDistance)
        {
            enemyAnim.Run(false);
            enemyAnim.Walk(false);
            enemyState = EnemyState.ATTACK;

            if (chaseDistance != currentChaseDistance)
            {
                chaseDistance = currentChaseDistance;
            }
        }
        else if (Vector3.Distance(transform.position, target.position) > chaseDistance)
        {
            enemyAnim.Run(false);
            enemyState = EnemyState.PATROL;

            patrolTimer = patrolForThisTime;

            if(chaseDistance != currentChaseDistance)
            {
                chaseDistance = currentChaseDistance;
            }
        }

    }

    void Attack()
    {
        navAgent.velocity = Vector3.zero;
        navAgent.isStopped = false;

        attackTime += Time.deltaTime;

        if (attackTime > waitBeforeAttack)
        {
            enemyAnim.Attack();

            attackTime = 0f;

            PlayAttackSound();
        }

        if (Vector3.Distance(transform.position, target.position) > attackDistance + chaseAfterAttackDistance)
        {
            enemyState = EnemyState.CHASE;
        }
    }

    private void SetNewRandomDestination()
    {
        float randRadius = Random.Range(patrolRadiusMinimum, patrolRadiusMaximum);

        Vector3 randDir = Random.insideUnitSphere * randRadius;

        randDir += transform.position;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDir, out navHit, randRadius, -1);

        navAgent.SetDestination(navHit.position);
    }

    void TurnOnAttackPoint()
    {
        attackPoint.SetActive(true);
    }

    void TurnOffAttackPoint()
    {
        if (attackPoint.activeInHierarchy)
        {
            attackPoint.SetActive(false);
        }
    }

    public EnemyState EnemyState
    {
        get; set;
    }

    public void PlayScreamSound()
    {
        audioSource.clip = screamClip[Random.Range(0, attackClips.Length)];
        audioSource.Play();
    }

    public void PlayAttackSound()
    {
        audioSource.clip = attackClips[Random.Range(0, attackClips.Length)];
        audioSource.Play();
    }
}
