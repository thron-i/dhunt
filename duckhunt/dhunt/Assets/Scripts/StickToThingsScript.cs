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
    void OnTriggerEnter(Collider col)
    {
        col.transform.SetParent(transform);
    }
    void OnTriggerExit(Collider col)
    {
        col.transform.SetParent(null);
    }

        // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
        
    }
}
