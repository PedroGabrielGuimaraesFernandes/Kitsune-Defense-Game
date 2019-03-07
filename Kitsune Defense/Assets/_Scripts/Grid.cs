using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [Header("Size x Size")]
    public float size = 1;
    [Header("Definem o Tamanho do Grid")]
    public int numPointsX = 1;
    public int numPointsY = 1;
    public int numPointsZ = 1;

    public Vector3 GetNearestPointOnGrid(Vector3 position)
    {
        position -= transform.position;

        int xCount = Mathf.RoundToInt(position.x / size);
        int yCount = Mathf.RoundToInt(position.y / size);
        int zCount = Mathf.RoundToInt(position.z / size);

        Vector3 result = new Vector3(xCount * size, yCount * size, zCount * size);

        result += transform.position;

        return result;
    }

    private void OnDrawGizmos()
    {
        /*Gizmos.color = Color.yellow;
        for (float x = 0 + transform.position.x; x  < numPointsX + transform.position.x; x += size)
        {
            for (float z  = 0 + transform.position.z; z  < numPointsZ + transform.position.z; z += size)
            {
                var point = GetNearestPointOnGrid(new Vector3(x, 0, z));
                Gizmos.DrawSphere(point, 0.1f);
            }
        }*/

        Gizmos.color = Color.green;
        for (float x = 0 + transform.position.x; x < numPointsX + transform.position.x; x += size)
        {
            for (float y = 0 + transform.position.y; y < numPointsY + transform.position.y; y += size)
            {
                for (float z = 0 + transform.position.z; z < numPointsZ + transform.position.z; z += size)
                {
                    var point = GetNearestPointOnGrid(new Vector3(x, y, z));
                    Gizmos.DrawSphere(point, 0.1f);
                }
            }
        }
    }

    /*Gizmos.color = Color.yellow;
        for (float x = 0 + transform.position.x; x<distanceX + transform.position.z; ++ x)
        {
            for (float z  = 0 + transform.position.z; z<distanceZ + transform.position.z; ++z)
            {
                var point = GetNearestPointOnGrid(new Vector3(x, 0, z));
    Gizmos.DrawSphere(point, 0.1f);
            }
        }
    }*/
}
