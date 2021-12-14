using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickToThingsScript : MonoBehaviour
{
    private GameObject target = null;
    private Vector3 offset;
    void Start()
    {
    }
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            print("hello");
            col.transform.SetParent(gameObject.transform,true);
        }
        
    }
    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.transform.SetParent(null);
            print("goodbye");
        }
    }

        // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
        
    }
}
