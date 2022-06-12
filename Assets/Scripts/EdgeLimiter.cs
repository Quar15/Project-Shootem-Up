using UnityEngine;

public class EdgeLimiter : MonoBehaviour
{
    public float leftDelim;
    public float rightDelim;
    public float bottomDelim;
    public float topDelim;


    // Returns true whenever the given value fits inside this delimiter
    public bool IsInside(Vector3 value)
    {
        return value.x >= leftDelim && value.x <= rightDelim && value.z >= bottomDelim && value.z <= topDelim;
    }
       

    // Returns Vector adjusted to be inside the edges;
    public Vector3 Delimit(Vector3 value)
    {
        if (value.x < leftDelim)
        {
            value.x = leftDelim;
        } else if (value.x > rightDelim)
        {
            value.x = rightDelim;
        }

        if (value.z < bottomDelim)
        {
            value.z = bottomDelim;
        } else if (value.z > topDelim)
        {
            value.z = topDelim;
        }

        return value;
    }
}
