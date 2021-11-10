using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 80f;
    public float jumpSpeed = 7;
    //public AudioSource coinAudioSource;
    Rigidbody rb;
    bool jumpPressed = false;
    Collider coll;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        coll = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        WalkHandler();
        JumpHandler();
    }

    void WalkHandler()
    {
        float distance = walkSpeed * Time.deltaTime;
        float hAxis = Input.GetAxis("Horizontal");
        //print("haxis is " + hAxis);

        float vAxis = Input.GetAxis("Vertical");
        //print("vaxis is " + vAxis);

        Vector3 movement = new Vector3(hAxis * distance, 0f, vAxis * distance);

        Vector3 currPos = transform.position;

        Vector3 newPos = currPos + movement;

        rb.MovePosition(newPos);
    }
    void JumpHandler()
    {
        float jAxis = Input.GetAxis("Jump");
        bool isGrounded = CheckGrounded();
        if (jAxis > 0f)
        {
           
            if (!jumpPressed && isGrounded)
            {
                jumpPressed = true;

                // Jumping vector
                Vector3 jumpVector = new Vector3(0f, jumpSpeed, 0f);
                // Make the player jump by adding velocity
                rb.velocity = rb.velocity + jumpVector;
            }
        }
        else
        {
            jumpPressed = false;
        }
    }
    void OnTriggerEnter(Collider collider)
    {
        // Check if we ran into a coin
        if (collider.gameObject.tag == "coin")
        {
            print("Grabbing coin..");
           // coinAudioSource.Play();
            GameManager.instance.IncreaseScore(1);
            // Destroy coin
            Destroy(collider.gameObject);
        }
        else if (collider.gameObject.tag == "enemy")
        {
            // Game over
            print("game over");

            // Soon.. go to the game over scene
        }
    }
    bool CheckGrounded()
    {
        float sizeX = coll.bounds.size.x;
        float sizeY = coll.bounds.size.y;
        float sizeZ = coll.bounds.size.z;
       // print(sizeX + " " + sizeY + " " + sizeZ);
        Vector3 corner1 = transform.position + new Vector3(sizeX / 2, -sizeY / 2 + 0.01f, sizeZ / 2);
        Vector3 corner2 = transform.position + new Vector3(-sizeX / 2, -sizeY / 2 + 0.01f, sizeZ / 2);
        Vector3 corner3 = transform.position + new Vector3(sizeX / 2, -sizeY / 2 + 0.01f, -sizeZ / 2);
        Vector3 corner4 = transform.position + new Vector3(-sizeX / 2, -sizeY / 2 + 0.01f, -sizeZ / 2);
        Vector3 down = new Vector3(0, -1, 0);
        bool grounded1 = Physics.Raycast(corner1, down, 0.01f);
        bool grounded2 = Physics.Raycast(corner2, down, 0.01f);
        bool grounded3 = Physics.Raycast(corner3, down, 0.01f);
        bool grounded4 = Physics.Raycast(corner4, down, 0.01f);
        //Debug.DrawRay(corner1, down, Color.green);
        //Debug.DrawRay(corner2, down, Color.blue);
        //Debug.DrawRay(corner3, down, Color.red);
        //Debug.DrawRay(corner4, down, Color.yellow);

        return (grounded1 || grounded2 || grounded3 || grounded4);
    }
}
