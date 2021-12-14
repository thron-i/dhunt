using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 3f; //walkspeed is also the maxspeed a player can move.
    
    public float jumpSpeed = 7;
    //public AudioSource coinAudioSource;
    float maxSqrMag;
    Rigidbody rb;
    bool jumpPressed = false;
    Collider coll;


    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        maxSqrMag = walkSpeed * walkSpeed;
        print("maxsqrmag: " + maxSqrMag);
        coll = GetComponent<Collider>();
    }

    // Update is called once per frame
    private void Update()
    {

    }
    private void FixedUpdate()
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
        //TODO: https://www.youtube.com/watch?v=YMwwYO1naCg
        //maybe recode the movement again to behave more like this, try messing around with normalising vectors to prevent speeds going too high.
        //atm the movement is normalised, but jumping is janky because it normalises velocity when you jump
        Vector3 curVelo = new Vector3(hAxis * walkSpeed, rb.velocity.y, vAxis * walkSpeed);
        //curVelo.x = hAxis * walkSpeed;
       // curVelo.z = vAxis * walkSpeed;

        // we get rb's current y vel because we don't wanna touch it, but we do want to be able to instantly make the player move in a direction on the z and x axes
        


        rb.velocity = curVelo;//clamp velocity so we don't go faster when holding two directions :)))
        float tempYvel = rb.velocity.y;
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        if (rb.velocity.sqrMagnitude> maxSqrMag)
        {
            //print("too fast bro");
            rb.velocity = rb.velocity.normalized * walkSpeed;
           
        }
        rb.velocity = new Vector3(rb.velocity.x, tempYvel, rb.velocity.z);
        //print("magnitude of velocity is " + rb.velocity.sqrMagnitude);
        //Vector3 currPos = transform.position;
        //
        //Vector3 newPos = currPos + movement;
        // transform.position = newPos;
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
        print("bababooey");
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

    //private void OnTriggerStay(Collider coll)
    //{
    //    GameObject target = coll.gameObject;
    //    if (target.tag == "platform")
    //    {
    //        Vector3 offset = target.transform.position - transform.position;
    //        transform.parent = coll.transform;
    //        target.transform.position = transform.position + offset;
    //    }

    //}

    //private void OnTriggerExit(Collider collider)
    //{
    //    if (collider.gameObject.tag == "platform")
    //    {
    //        transform.parent = null;
    //    }
    //}

    bool CheckGrounded()
    {
        float sizeX = coll.bounds.size.x;
        float sizeY = coll.bounds.size.y;
        float sizeZ = coll.bounds.size.z;
       // print(sizeX + " " + sizeY + " " + sizeZ);
        Vector3 corner1 = transform.position + new Vector3(sizeX / 2, -sizeY / 2 + 0.005f, sizeZ / 2);
        Vector3 corner2 = transform.position + new Vector3(-sizeX / 2, -sizeY / 2 + 0.005f, sizeZ / 2);
        Vector3 corner3 = transform.position + new Vector3(sizeX / 2, -sizeY / 2 + 0.005f, -sizeZ / 2);
        Vector3 corner4 = transform.position + new Vector3(-sizeX / 2, -sizeY / 2 + 0.005f, -sizeZ / 2);
        Vector3 down = new Vector3(0, -0.25f, 0);
        bool grounded1 = Physics.Raycast(corner1, down, 0.01f);
        bool grounded2 = Physics.Raycast(corner2, down, 0.01f);
        bool grounded3 = Physics.Raycast(corner3, down, 0.01f);
        bool grounded4 = Physics.Raycast(corner4, down, 0.01f);
        Debug.DrawRay(corner1, down, Color.green);
        Debug.DrawRay(corner2, down, Color.blue);
        Debug.DrawRay(corner3, down, Color.red);
        Debug.DrawRay(corner4, down, Color.yellow);


        print(grounded1 + " " + grounded2 + " " + grounded3 + " " + grounded4);
        return (grounded1 || grounded2 || grounded3 || grounded4);
    }
}
