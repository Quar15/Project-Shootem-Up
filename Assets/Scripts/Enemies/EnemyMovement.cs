using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovementType {NONE, BEZIER, LERP};

public class EnemyMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform[] _routePoints;
    public void SetRoutePoints(Transform[] routePoints) { _routePoints = routePoints; }

    [Header("Settings")]
    [SerializeField] private float _speed = 0.5f;
    public void SetSpeed(float speed) { _speed = speed; }
    public MovementType movementType;

    private bool movementStarted;
    private float t;
    private int currCheckpointIndex;
    private Vector3 startingPosition;

    private void Awake()
    {
        InitMovementSystem();
    }

    public void InitMovementSystem()
    {
        StopAllCoroutines();
        currCheckpointIndex = 0;
        t = 0f;
        movementStarted = false;
    }

    private void Update()
    {
        switch (movementType)
        {
            case MovementType.BEZIER:
                if(!movementStarted)
                    StartCoroutine(BezierCurveMoveTo(_routePoints));
                break;
            case MovementType.LERP:
                LerpMoveTo(_routePoints);
                break;
            default:
                break;
        }
    }

    private void LerpMoveTo(Transform[] checkpointTransforms)
    {
        if(currCheckpointIndex >= checkpointTransforms.Length)
            return;

        if(!movementStarted)
        {
            t = Time.time;
            startingPosition = transform.position;
            movementStarted = true;
        }

        float fractionOfJourney = (((Time.time - t) * _speed) / Vector3.Distance(startingPosition, checkpointTransforms[currCheckpointIndex].position));
        
        transform.position = Vector3.Lerp(startingPosition, checkpointTransforms[currCheckpointIndex].position, fractionOfJourney);
        transform.LookAt(checkpointTransforms[currCheckpointIndex].position);
        if(fractionOfJourney > 0.99)
        {
            currCheckpointIndex++;
            movementStarted = false;
        }
            
    }

    private IEnumerator BezierCurveMoveTo(Transform[] checkpointTransforms)
    {
        movementStarted = true;

        List<Vector3> controlPointsPositions = new List<Vector3>();
        foreach (var ct in checkpointTransforms)
        {
            controlPointsPositions.Add(new Vector3(ct.position.x, ct.position.z, ct.position.y));
        }

        while (t < 1)
        {
            t += Time.deltaTime * _speed;

            Vector2 calcPosition = Mathf.Pow(1 - t, 3) * controlPointsPositions[0] +
                3 * Mathf.Pow(1 - t, 2) * t * controlPointsPositions[1] +
                3 * (1 - t) * Mathf.Pow(t, 2) * controlPointsPositions[2] +
                Mathf.Pow(t, 3) * controlPointsPositions[3];

            Vector3 targetPosition = new Vector3(calcPosition.x, transform.position.y, calcPosition.y);

            transform.LookAt(targetPosition);
            transform.position = targetPosition;
            yield return new WaitForEndOfFrame();
        }
    }
}
