using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed;   //CHARACTER SPEED 
    
    private PlayerController _pc; 
    public GameObject player;
    public GameObject board; 
    

    public HBSCRIPT _hb;
    public bool isInArea;
    

    public Transform hoverBoardPoint;
    public Transform hoverBoardPoint2;


    // Start is called before the first frame update
    void Start()
    {
        isInArea = false; 
        speed = 10f;

        player = GameObject.FindGameObjectWithTag("MainPlayer");

       
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        movementDirection.Normalize();

        transform.Translate(movementDirection * speed * Time.deltaTime, Space.World);

        if (Input.GetKeyDown(KeyCode.E))

        {
            Debug.Log("TELEPORT");

            
            player.transform.position = hoverBoardPoint.transform.position;
            
            speed = 0f;
            _hb.turnTorque = 400;
            _hb.moveForce = 5000;
            _hb.multiplier = 3; 
            _hb.enabled = true; 
            
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "hbi")
        {
            
            Debug.Log("PLAYER IN HB AREA)");

        }
        else
        {
            return;
        }
    }
}

       
        

                






          



