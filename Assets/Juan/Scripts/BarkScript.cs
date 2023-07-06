using UnityEngine;

public class BarkScript : MonoBehaviour
{
    public float destroyTimer = 1;
    public PlayerControllerJuan playerController;
    public float damage;

    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerControllerJuan>();  // Find and assign the PlayerController component in the scene
        damage = playerController.barkDamage;
    }

    private void Awake()
    {
        Destroy(gameObject, destroyTimer);
    }

 
}