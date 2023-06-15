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
    string[] interactTags = { "Journal", "Coldrun", "Chemical" };
    public int interactTag = 0;
    public int chemicals = 0;
    public bool isBlocking = false;
    public bool isAttacking = false;
    public bool detectedEnemy = false;
    public bool isDead = false;
    float attackCooldown = 0.0f;
    public bool showChemicals = false;
    public bool canExit = false;
    

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        coldronController = GameObject.Find("Coldrun").GetComponent<ColdronController>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        animator.SetBool("isRunning", false);
        healthBar = GameObject.Find("HealthBar").GetComponent<HealthBar>();

        //Set camera position
        camOffset = new Vector3(0.00f, 3.28f, -4.82f);
        cam.transform.position = transform.position + camOffset;

    }

    // Update is called once per frame
    void Update()
    {
        healthBar.SetHealth(health);


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


        if(health <= 0)
        {
            animator.SetBool("isDead", true);
            healthBar.SetHealth(health);
            health = 0.0f;
            isDead = true;
        }
        
        //move camera
        cam.transform.position = transform.position + camOffset;

        Block();

        Attack();
          
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

    public void Attack()
    {
        attackCooldown -= Time.deltaTime;

        if (Input.GetKey(KeyCode.R) && attackCooldown <= 0.0f)
        {
            attackCooldown = 0.75f;

            isAttacking = true;
            animator.SetTrigger("Attack");

            if (detectedEnemy)
            {
                enemyController.takeDamage(10.0f);
            }

        }

        if (Input.GetKeyUp(KeyCode.R))
        {
            isAttacking = false;

        }

        if(attackCooldown <= 0.0f)
        {
            attackCooldown = 0.0f;
        }

    }

    public void Block()
    {
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
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
    }

    private void OnTriggerEnter(Collider other)
    {
        for(int i = 0; i < interactTags.Length; i++)
        {
            if(other.tag == interactTags[i])
            {
                interactTag = i;
            }
        }

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
            if (chemicals == 5)
            {
                canExit = false;
            }
        }

    }

}
