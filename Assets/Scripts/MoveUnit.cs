using System;
using System.Collections;
using UnityEngine;

public class MoveUnit : MonoBehaviour
{
    public Transform target;
    public float speed = 1;
    public int targetIndex;
    private Vector3[] _path;
    private void Start()
    {
        PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
    }

    public void OnPathFound(Vector3[] newPath, bool pathSuccesful)
    {
        if (pathSuccesful)
        {
            _path = newPath;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }

    IEnumerator FollowPath()
    {
        Vector3 currentWayPoint = _path[0];
        while (true)
        {
            if (transform.position == currentWayPoint)
            {
                targetIndex++;
                if (targetIndex >= _path.Length)
                {
                    yield break;
                }

                currentWayPoint = _path[targetIndex];
                
            }

            transform.position = Vector3.MoveTowards(transform.position, currentWayPoint, speed);
            yield return null;
        }
    }
}