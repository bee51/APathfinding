using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathRequestManager : MonoBehaviour
{
    private Queue<PathRequest> _pathRequestsQue=new Queue<PathRequest>();
    private PathRequest _currentPathRequest;
    private Pathfinding _pathFinding;
    private bool _isProcessPath;
    public static PathRequestManager instance;

    private void Awake()
    {
        instance = this;
        _pathFinding = GetComponent<Pathfinding>();
    }


    public static void RequestPath(Vector3 pathStart, Vector3 pathEnd, Action<Vector3[], bool> callBack)
    {
        PathRequest newRequest = new PathRequest(pathStart, pathEnd, callBack);
        instance._pathRequestsQue.Enqueue(newRequest);
        instance.TryProcessNext();
    }

    public void FinishedProcessingPath(Vector3[] path ,bool success)
    {
        _currentPathRequest.callBack(path, success);
        _isProcessPath = false;
        TryProcessNext();
    }
    private void TryProcessNext()
    {
        if (!_isProcessPath&& _pathRequestsQue.Count>0)
        {
            _currentPathRequest = _pathRequestsQue.Dequeue();
            _isProcessPath = true;
            _pathFinding.StartFindPath(_currentPathRequest.pathStart, _currentPathRequest.pathEnd);
        }
    }
}

public struct PathRequest
{
    
    public Vector3 pathStart;
    public Vector3 pathEnd;
    public Action<Vector3[], bool> callBack;
    
    public  PathRequest (Vector3 pathStart, Vector3 pathEnd, Action<Vector3[], bool> callBack)
    {
        this.pathStart = pathStart;
        this.pathEnd = pathEnd;
        this.callBack = callBack;
    }

}
