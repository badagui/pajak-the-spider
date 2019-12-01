using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPathPlanner : MonoBehaviour
{
    public float PathPlanningTimeStep = 0.02f;

    [SerializeField]
    private Transform jumpStartPos;

    [SerializeField]
    private LayerMask groundLayer;

    /// <summary>
    /// Uses current velocity from rigidbody.
    /// </summary>
    public List<Vector3> GetJumpPathPoints(Rigidbody2D rigidbody2D)
    {
        List<Vector3> points = new List<Vector3>();

        for (int i = 0; i < 60; i++)
        {
            float time = i * PathPlanningTimeStep;
            points.Add(new Vector3(rigidbody2D.velocity.x * time,
                                    rigidbody2D.velocity.y * time + Physics2D.gravity.y * rigidbody2D.gravityScale * time * time * 0.5f,
                                    0) + jumpStartPos.position);

            if (Physics2D.OverlapPoint(points[i], groundLayer) != null)
            {
                break;
            }
        }
        return points;
    }

    public List<Vector3> GetJumpPathPoints(Rigidbody2D rigidbody2D, Vector2 velocity)
    {
        List<Vector3> points = new List<Vector3>();

        for (int i = 0; i < 60; i++)
        {
            float time = i * PathPlanningTimeStep;
            points.Add(new Vector3( velocity.x * time,
                                    velocity.y * time + Physics2D.gravity.y * rigidbody2D.gravityScale * time * time * 0.5f,
                                    0) + jumpStartPos.position);

            if (Physics2D.OverlapPoint(points[i], groundLayer) != null)
            {
                break;
            }
        }
        return points;
    }

    public void RenderLine(LineRenderer lineRenderer, List<Vector3> points)
    {
        int nPointsToUseLimit = 15;
        int nPointsToUse = points.Count - 2; // dont use first 2
        nPointsToUse = nPointsToUse > nPointsToUseLimit ? nPointsToUseLimit : nPointsToUse;

        lineRenderer.positionCount = nPointsToUse;
        points = points.GetRange(2, nPointsToUse);
        lineRenderer.SetPositions(points.ToArray());
    }
}
