using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Route : MonoBehaviour
{
    [SerializeField] private MovementType _movementType;
    public MovementType GetMovementType() { return _movementType; }
    [Tooltip("Auto filled, can be manually filled for gizmo draw.")]
    [SerializeField] private Transform[] _routePoints;
    public Transform[] GetRoutePoints() { return _routePoints; }
    [SerializeField] private float _speed = 0.5f;
    public float GetRouteSpeed() { return _speed; }

    private Vector2 gizmosPosition;

    private void Awake() 
    {
        List<Transform> routePointList = new List<Transform>();
        foreach (Transform child in transform)
            routePointList.Add(child);
        _routePoints = routePointList.ToArray();
    }

    private void OnDrawGizmos() 
    {
        switch (_movementType)
        {
            case MovementType.LERP:
                DrawLerpRoute();
                break;
            case MovementType.BEZIER:
                DrawBezierRoute();
                break;
            default:
                break;
        }
    }

    private void DrawLerpRoute()
    {
        if(_routePoints.Length < 2)
            return;

        for(int i=0; i < _routePoints.Length - 1; i++)
        {
            Gizmos.DrawLine(_routePoints[i].position, _routePoints[i+1].position);
        }

        Gizmos.DrawLine(_routePoints[_routePoints.Length-2].position, _routePoints[_routePoints.Length-1].position);
    }

    private void DrawBezierRoute()
    {
        List<Vector3> routePointsPositions = new List<Vector3>();
        foreach (var cp in _routePoints)
        {
            routePointsPositions.Add(new Vector3(cp.position.x, cp.position.z, cp.position.y));
        }

        if(routePointsPositions.Count < 4)
            return;

        for(float t=0; t<=1; t+= 0.05f)
        {
            gizmosPosition = Mathf.Pow(1 - t, 3) * routePointsPositions[0] +
                3 * Mathf.Pow(1 - t, 2) * t * routePointsPositions[1] +
                3 * (1 - t) * Mathf.Pow(t, 2) * routePointsPositions[2] +
                Mathf.Pow(t, 3) * routePointsPositions[3];

            Gizmos.DrawSphere(new Vector3(gizmosPosition.x, _routePoints[0].position.y, gizmosPosition.y), 0.25f);
        }

        Gizmos.DrawLine(_routePoints[0].position, _routePoints[1].position);
        Gizmos.DrawLine(_routePoints[2].position, _routePoints[3].position);
    }
}
