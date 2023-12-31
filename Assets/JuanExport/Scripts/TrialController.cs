using UnityEngine;

public class TrialController : MonoBehaviour
{
    // Health / lives
    [Header("Health / Lives")]
    public int lives = 3;  // Number of lives the player has
    public int maxHealth = 3;  // Maximum health the player can have
    public float currentHealth = 3;  // Current health of the player

    // Attacks
    [Header("Attacks")]
    public float barkDamage;
    public float smashDamage;

    // Bones
    [Header("Bones")]
    public int boneCount = 0;  // Number of collected bones

    // Movement
    [Header("Movement")]
    public float moveSpeed = 5;  // Movement speed of the player
    public float jumpForce = 5f;  // Force applied when the player jumps
    public int maxJumps = 2;  // Maximum number of jumps the player can perform
    private int jumpsRemaining;  // Number of jumps remaining for the player

    // Rigidbody / Ground test
    [Header("Rigidbody / Ground Test")]
    private Rigidbody rb;  // Reference to the Rigidbody component of the player
    private bool isGrounded;  // Indicates if the player is currently grounded

    // Powerups
    [Header("Power Ups")]
    public float speedTimer;  // Timer for the speed power-up
    public float tripleJumpTimer;  // Timer for the triple jump power-up

    // Debuffs
    [Header("Debuffs")]
    public bool isSlowed = false;  // Indicates if the player is currently slowed down

    // Respawn point
    [Header("Respawn Point")]
    public Vector3 respawnPoint;  // Position where the player respawns after dying

    // Platforms
    [Header("Platforms")]
    

    //Animator
    public Animator animator;
    public float rotationSpeed;
    void Start()
    {
        // Get the Rigidbody component of the player
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        // Set jumps and health to max
        jumpsRemaining = maxJumps;
        currentHealth = maxHealth;
    }

    void Update()
    {
        // MOVEMENT
        // Input system movement
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical) * moveSpeed;
        rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);


        // Jump with ground checker
        if (Input.GetButtonDown("Jump") && (isGrounded || jumpsRemaining > 0))
        {
            animator.SetBool("IsJumping", true);

            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
            isGrounded = false;
            jumpsRemaining--;
            
        } else
        
        {
            if (isGrounded == true)
            {
                animator.SetBool("IsJumping", false);
                animator.SetBool("IsFalling", true);
                animator.SetBool("IsDoubleJumping", false); 
            }
            if (jumpsRemaining == 0)
            {
                animator.SetBool("IsDoubleJumping", true); 
            }
        }

        //CHARACTER ROTATION - JUAN

        if (movement != Vector3.zero) //CHARACTER ROTATION //Setting up the rotation for the character
        {
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime); //Specifying how I want the character to rotate
            animator.SetBool("IsMoving", true);
        }
        else
        {
            animator.SetBool("IsMoving", false);

        }

        //PUNCH ATTACK - JUAN
        if (Input.GetMouseButton(0))
        {
            Debug.Log("PUNCH ATTACK");
            animator.SetBool("IsAttacking", true); 
            
        } else
        {
            animator.SetBool("IsAttacking", false); 
        }







        //COMMENTED FOR NOW


            //// Turn the player depending on how they move
            //if (moveHorizontal < 0)
            //{
            //    transform.rotation = Quaternion.Euler(0f, 270f, 0f);
            //}
            //else if (moveHorizontal > 0)
            //{
            //    transform.rotation = Quaternion.Euler(0f, 90f, 0f);
            //}
            //else if (moveVertical < 0)
            //{
            //    transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            //}
            //else if (moveVertical > 0)
            //{
            //    transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            //}

            // POWER UPS
            // Triple jump powerup
            //tripleJumpTimer -= Time.deltaTime;
            //speedTimer -= Time.deltaTime;

            //if (tripleJumpTimer < 0)
            //{
            //    maxJumps = 2;
            //}

            //if (speedTimer < 0 && !isSlowed)
            //{
            //    moveSpeed = 5;
            //}
    }

    // HEALTH / LIVES
    // If health = 0
    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        if (lives >= 1)
        {
            if (currentHealth <= 0)
            {
                // Respawn
                Respawn();
            }
        }
        else
        {
            // End game
            Debug.Log("Lives = 0");
        }
    }

    // Collision stuff
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            // Reset jumps and set grounded true
            isGrounded = true;
            jumpsRemaining = maxJumps;
        }
    }

    // GAINING HEALTH METHOD
    public void GainHealth()
    {
        if (currentHealth < maxHealth)
        {
            currentHealth = currentHealth + 1;
        }
    }

    // TAKING DAMAGE METHOD
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }

    // RESPAWNING METHOD
    public void Respawn()
    {
        transform.position = respawnPoint;
        currentHealth = maxHealth;
        lives -= 1;
    }

    // SETTING THE SPAWN POINT METHOD
    public void SetSpawnPoint(Vector3 newPosition)
    {
        respawnPoint = newPosition;
    }

    // COLLECTING BONES METHOD
    public void Collectedbone()
    {
        boneCount = boneCount + 1;
    }

    // POWER UPS
    // Method that controls the triple jump power up
    //public void TripleJump()
    //{
    //    // Set timer to 10 seconds and max jumps to 3
    //    tripleJumpTimer = 10f;
    //    maxJumps = 3;
    //    jumpsRemaining = maxJumps;
    //}

    //// Method that controls the speed power up
    //public void SpeedPowerUp()
    //{
    //    speedTimer = 10f;
    //    moveSpeed = 8;
    //}

    
}