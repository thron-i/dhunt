using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XFollowY : MonoBehaviour
{
    public string FollowedByName = null;
    private GameObject FollowedBy;
    
    // Start is called before the first frame update
    void Start()
    {
        if (FollowedByName != null)
        {
            FollowedBy = GameObject.Find(FollowedByName);
        }
       // FollowedBy.transform.SetParent(transform);
    }

    // Update is called once per frame
    void Update()
    {
        //try
        //{
        //    FollowedBy.transform = transform;
        //}
        //catch (NullReferenceException e)
        //{

        //}
       // FollowedBy.transform.SetParent(transform);
    }
}
