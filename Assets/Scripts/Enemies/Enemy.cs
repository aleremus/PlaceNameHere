using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField, Range(0, 1000)] float agroRadius;
    [SerializeField, Range(0, 1234567)] int maxHealth;
    [SerializeField, Range(1, 1000)] float speed;
    [SerializeField, Range(1, 100)] int damage;

    public HealthBar healthBar;
    private EnemySpawnManager location;
    private NavMeshAgent meshAgent;

    private float currentHealth;
    private bool isAgroed;
    private bool canAttack;

    private Vector3 movementDirection;

    private Rigidbody enemyRigidbody;
    private GameObject player;
    private PlayerCharacterstics playerCharacterstics;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxAndCurrentHeath(maxHealth);

        isAgroed = false;
        canAttack = true;

        enemyRigidbody = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        location = GameObject.Find("SpiderSpawnZone").GetComponent<EnemySpawnManager>();
        playerCharacterstics = player.GetComponent<PlayerCharacterstics>();
        meshAgent = GetComponent<NavMeshAgent>();

        StartCoroutine(FindRandomDirection());

    }

    // Update is called once per frame
    void Update()
    {
        healthBar.SetCurrentHealth(currentHealth);

        if (player.gameObject == null)
        {
            if (!isAgroed)
                return;
            StartCoroutine(FindRandomDirection());
            isAgroed = false;
        }

        float distanceToPlayer = (player.transform.position - transform.position).magnitude;
        if (isAgroed)
        {
            movementDirection = player.transform.position;
            if(distanceToPlayer <= 2 && canAttack)
            {
                canAttack = false;
                CloseRangeAttack(damage);
                StartCoroutine(AttackCoolDown());
                
            }
            if (distanceToPlayer > agroRadius * 3)
            {
                isAgroed = false;
                StartCoroutine(FindRandomDirection());
            }
        }
        else
        {
            if (distanceToPlayer < agroRadius)
            {
                StopCoroutine(FindRandomDirection());
                isAgroed = true;
            }
        }

    }

    private void FixedUpdate()
    {
        meshAgent.SetDestination(movementDirection);
    }

    IEnumerator FindRandomDirection()
    {
        while(!isAgroed)
        {
            movementDirection = location.GenerateRandomPointInArea();
            yield return new WaitForSeconds(5);
        }
    }

    IEnumerator AttackCoolDown()
    {
        float previousSpeed = speed;
        speed = 0;
        yield return new WaitForSeconds(2);
        canAttack = true;
        speed = previousSpeed;
    }

    public void DealDamage(int damage)
    {
        isAgroed = true;
        StopCoroutine("FindRandomDirection");

        currentHealth -= damage;

        healthBar.SetCurrentHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        location.amountOfEnemies--;
        Destroy(gameObject);
    }

    private void CloseRangeAttack(int damage)
    {
        playerCharacterstics.DealDamage(damage);
    }
}
