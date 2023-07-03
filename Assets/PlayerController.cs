using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed;   //CHARACTER SPEED 
    
    private PlayerController _pc; 
    public Transform player;

    public GameObject hb; 

    private HBSCRIPT _hb;
    public bool isInArea;
    

    public Transform hoverBoardPoint;
    public Transform hoverBoardPoint2;


    // Start is called before the first frame update
    void Start()
    {
        isInArea = false; 
        speed = 10f;


        _hb = GetComponent<HBSCRIPT>(); 
        

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
            
            player.transform.position = hoverBoardPoint.transform.position;

            Debug.Log("TELEPORT");

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


