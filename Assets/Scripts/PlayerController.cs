using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    GameManager gameManager;
    Rigidbody rb;
    Animator animator;
    Camera cam;
    EnemyController enemyController;
    ColdronController coldronController;
    HealthBar healthBar;

    Vector3 movementDirection;
    Quaternion playerRotation;
    Vector3 camOffset;

    float horizontalMovement;
    float verticalMovement;
    public float Speed;
    public float rotationSpeed;

    public float health = 100.0f;
    float attackCooldown = 0.0f;

    public bool isBlocking = false;
    public bool detectedEnemy = false;
    public bool isDead = false;
    public bool canExit = false;
    

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();

        coldronController = GameObject.Find("Coldrun").GetComponent<ColdronController>();
        healthBar = GameObject.Find("HealthBar").GetComponent<HealthBar>();

        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        //Set camera position
        camOffset = new Vector3(0.00f, 3.28f, -4.82f);
        cam.transform.position = transform.position + camOffset;

    }

    // Update is called once per frame
    void Update()
    {
        Move();

        //update health
        healthBar.SetHealth(health);

        //atacking
        attackCooldown -= Time.deltaTime;

        if (Input.GetKey(KeyCode.R) && attackCooldown <= 0.0f)
        {
            Attack();
        }

        //blocking
        if (Input.GetKeyDown(KeyCode.F))
        {
            isBlocking = true;
            animator.SetBool("isBlocking", true);

        }

        if (Input.GetKeyUp(KeyCode.F))
        {
            isBlocking = false;
            animator.SetBool("isBlocking", false);

        }

        if (Input.GetKey(KeyCode.E) && canExit)
        {
            SceneManager.LoadScene("Town");
        }

    }

    private void FixedUpdate()
    {
        transform.Translate(movementDirection * Speed * Time.deltaTime, Space.World);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, playerRotation, rotationSpeed * Time.deltaTime);

    }

    public void Die()
    {
        animator.SetBool("isDead", true);
        healthBar.SetHealth(health);
        health = 0.0f;
        isDead = true;
        
    }

    public void Move()
    {
        //move camera
        cam.transform.position = transform.position + camOffset;

        if (!isDead)
        {
            horizontalMovement = Input.GetAxisRaw("Horizontal");
            verticalMovement = Input.GetAxisRaw("Vertical");

            movementDirection = new Vector3(horizontalMovement, 0f, verticalMovement);
            movementDirection.Normalize();

            if (movementDirection != Vector3.zero)
            {
                playerRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            }

            if (movementDirection != Vector3.zero)
            {
                animator.SetBool("isRunning", true);
            }
            else
            {
                animator.SetBool("isRunning", false);

            }
        }
    }

    public void Attack()
    {

        attackCooldown = 0.75f;

        animator.SetTrigger("Attack");

        if (detectedEnemy)
        {
            enemyController.takeDamage(10.0f);
            if(enemyController.health <= 0.0f)
            {
                enemyController.Die();
            }
        }


        if(attackCooldown <= 0.0f)
        {
            attackCooldown = 0.0f;
        }

    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject.CompareTag("Enemy"))
        {
            enemyController = other.gameObject.GetComponent<EnemyController>();
            detectedEnemy = true;
        }

        if (other.gameObject.CompareTag("Exit"))
        {

            if(coldronController.madePotion)
            {
                canExit = true;
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            detectedEnemy = false;
        }

        if (other.gameObject.CompareTag("Exit"))
        {
            
        }

    }

}
