using System.Collections.Generic;
using UnityEngine;

public class LineDrawer : MonoBehaviour
{
    private LineRenderer line;

    private Vector3 P0;
    private Vector3 P1;
    private Vector3 P2;
    private Vector3 P3;

    public void Init(Vector3 vector)
    {
        line = GetComponent<LineRenderer>();
        P3 = vector;
        CalcPoints();
        UpdateLine();
    }

    private void CalcPoints()
    {
        P0 = transform.position;
        P1 = new Vector3(P0.x, P3.y, 0.1f);
        P2 = new Vector3(P3.x, P0.y, 0.1f);
    }

    private void UpdateLine()
    {
        int sigmentsNumber = 21;

        List<Vector3> points = new();

        for (int i = 0; i < sigmentsNumber + 1; i++)
        {
            float paremeter = (float)i / sigmentsNumber;
            Vector3 point = Bezier.GetPoint(P0, P1, P2, P3, paremeter);
            points.Add(point);
        }

        line.SetPositions(points.ToArray());
    }
}