using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointFollower : MonoBehaviour
{

    [SerializeField] GameObject[] wayPoints;
    private int currentWayPointIndex = 0;
    [SerializeField] private float speed = 2f;


    private void Update()
    {
        if (Vector2.Distance(wayPoints[currentWayPointIndex].transform.position,transform.position) < 0.1f)
        {
            WayPointIndexProcess();
        }

        transform.position = Vector2.MoveTowards(transform.position, wayPoints[currentWayPointIndex].transform.position, Time.deltaTime * speed);

    }

    private void WayPointIndexProcess()
    {
        currentWayPointIndex++;
        if(currentWayPointIndex >= wayPoints.Length)
        {
            currentWayPointIndex = 0;
        }
    }


}
