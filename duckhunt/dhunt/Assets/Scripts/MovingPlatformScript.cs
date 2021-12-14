using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformScript : MonoBehaviour
{
    private Vector3 nodePos1;
    private Vector3 nodePos2;
    private List<Vector3> nodePositions = new List<Vector3> { };
    public int numNodes=2; //you must tell it how many nodes the platform has to move between.
    public int goalNode=1; //first node we will move towards
    private Vector3 goalPos;
    private Vector3 curPos;
    private bool reverse = false;
    public float speed = 5f;
    private bool impulsed = false;
    Vector3 STATIONARY = new Vector3(0, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        print("numNodes is " + numNodes);
        //populate list
        for(int i = 0; i < numNodes; i++)
        {
            string nodeName = "node" + i;
            GameObject node = GameObject.Find(nodeName);
            //print("trying to find " + nodeName + ", got " + node);
            Vector3 thisNodePos = node.transform.position;
            //print("positions are x:" + thisNodePos.x + ", y:" + thisNodePos.y + ", z:" + thisNodePos.z);
            nodePositions.Add(thisNodePos);
        }

        goalPos = nodePositions[goalNode];
    }

   
    void FixedUpdate()
    {
        curPos = transform.position;
        // How much we are moving
        float maxMovement = speed * Time.deltaTime;
        // Check if we're close enough to that node
        //print("the distance between the positions is " + Vector3.Distance(curPos, goalPos));
        if (transform.position == goalPos)
        {
            print("yes");
            impulsed = false;
            if (reverse)
            {
                // Move to the next node
                if (goalNode == 0)
                {
                    goalNode++;
                    reverse = false;
                }
                else
                {
                    goalNode--;
                }
                goalPos = nodePositions[goalNode];
            }
            else
            {
                // Move to the next node
                if (goalNode == nodePositions.Count - 1)
                {
                    goalNode--;
                    reverse = true;
                }
                else
                {
                    goalNode++;
                    
                }
                goalPos = nodePositions[goalNode];
            }
        }

        // If it can move further, move

        // Move the object
        StartCoroutine(Vector3LerpCoroutine(gameObject, goalPos, speed));
       

        
    }
    IEnumerator Vector3LerpCoroutine(GameObject obj, Vector3 target, float speed)
    {
        Vector3 startPosition = obj.transform.position;
        float time = 0f;

        while (obj.transform.position != target)
        {
            obj.transform.position = Vector3.Lerp(startPosition, target, (time / Vector3.Distance(startPosition, target)) * speed);
            time += Time.deltaTime;
            yield return null;
        }
    }
}
