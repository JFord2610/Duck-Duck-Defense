using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    Path path;
    PathManager pathManager;
    BaseGoose goose;
    
    [SerializeField]
    int currentPathIndex;

    private void Awake()
    {
        goose = GetComponent<BaseGoose>();
        pathManager = GameObject.Find("PathHolder").GetComponent<PathManager>();
        path = pathManager.path;
        currentPathIndex = 0;
    }

    public void StartPath()
    {
        goose.moving = true;
    }

    private void FixedUpdate()
    {
        if(goose.moving)
        {
            Vector3 goosePos = transform.position;
            Vector3 wayPoint = path.WayPoints[currentPathIndex];
            Vector3 v0 = wayPoint - transform.position; //Vector from goose to waypoint
            Vector3 v1 = v0.normalized * Time.deltaTime * goose.speed; //Translation vector for goose

            Vector3 translationVector = v1;

            //If it is moving past the waypoint
            if(Mathf.Round(v1.magnitude * 100) >= Mathf.Round(v0.magnitude * 100)) // Round to nearest hundredth
            {
                currentPathIndex++;

                if(currentPathIndex > path.WayPoints.Length - 1)
                {
                    goose.Die();
                    return;
                }

                Vector3 nextWaypoint = path.WayPoints[currentPathIndex];
                RotateToPoint(nextWaypoint); //Turn to next point

                //Move towards the next point, however far the goose would have moved past previous point
                Vector3 v2 = nextWaypoint - wayPoint;
                Vector3 v3 = v2.normalized * (v1.magnitude - v0.magnitude);

                translationVector += v3; // Add new vector to translation vector
            }

            //move goose
            transform.Translate(translationVector);
        }
    }

    private void RotateToPoint(Vector3 p)
    {
        //To Do: Rotate goose
    }

}
