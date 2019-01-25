using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWaypoints : MonoBehaviour
{

    public List<Transform> waypoints;

    int currentWaypointIndex;

    public Transform getCurrentWaypoint()
    {
        return waypoints[currentWaypointIndex];
    }

    public Transform goUp()
    {
        if (currentWaypointIndex != 0)
        {
            currentWaypointIndex--;
        }

        return waypoints[currentWaypointIndex];

    }
    public Transform goDown()
    {
        if (currentWaypointIndex+1 != waypoints.Count)
        {
            currentWaypointIndex++;
        }

        return waypoints[currentWaypointIndex];

    }



}
