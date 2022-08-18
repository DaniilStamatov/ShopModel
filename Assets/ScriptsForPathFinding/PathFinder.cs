using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour, IPathFinder
{
    [SerializeField] private Rectangle[] _rectangles;
    [SerializeField] private Vector3[] _start;
    [SerializeField] private Vector3[] _end;

    private Edge[] edge = new Edge[2];
    private LineRenderer _line;
    private void Start()
    {
        GetEdge();
    }

    private void GetEdge()
    {

        for (int i = 0; i < edge.Length; i++)
        {
            edge[i] = new Edge(_rectangles[i], _rectangles[i + 1]);
            _start[i] = edge[i].Start;
            _end[i] = edge[i].End;
            GethPath(Vector3.zero, Vector3.zero, edge);
        }

    }

    public IEnumerable<Vector2> GethPath(Vector2 A, Vector2 C, IEnumerable<Edge> edges)
    {
        throw new System.NotImplementedException();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        foreach (var rectangle in _rectangles)
        {
            Gizmos.DrawLine(new Vector3(rectangle.Min.x, rectangle.Min.y, 0), new Vector3(rectangle.Max.x, rectangle.Min.y, 0));
            Gizmos.DrawLine(new Vector3(rectangle.Max.x, rectangle.Min.y, 0), new Vector3(rectangle.Max.x, rectangle.Max.y, 0));
            Gizmos.DrawLine(new Vector3(rectangle.Max.x, rectangle.Max.y, 0), new Vector3(rectangle.Min.x, rectangle.Max.y, 0));
            Gizmos.DrawLine(new Vector3(rectangle.Min.x, rectangle.Max.y, 0), new Vector3(rectangle.Min.x, rectangle.Min.y, 0));
        }
    }
}

[Serializable]
public struct Rectangle
{
    public Vector2 Min;
    public Vector2 Max;
    public Rectangle(Vector2 position, Vector2 size)
    {
        Min.x = position.x;
        Min.y = position.y;
        Max.x = size.x;
        Max.y = size.y;
    }

    public bool Contains(Vector2 point)
    {
        return point.x >= Min.x && point.x < Max.x && point.y >= Min.y && point.y < Max.y;
    }
}

public struct Edge
{
    public Rectangle First;
    public Rectangle Second;

    public Vector3 Start;
    public Vector3 End;

    public void GetDifference()
    {

    }
    public Edge(Rectangle rectangle1, Rectangle rectangle2)
    {
        First = rectangle1;
        Second = rectangle2;
        Start = Vector3.zero;
        End = Vector3.zero;

        if (rectangle1.Min.x < rectangle2.Min.x && rectangle1.Max.y == rectangle2.Min.y)
        {
            Start = rectangle2.Min;
            End = rectangle1.Max;
        }
        if (rectangle1.Min.x > rectangle2.Min.x && rectangle1.Max.y == rectangle2.Min.y)
        {
            Start = new Vector3(rectangle1.Min.x, rectangle1.Max.y);
            End = new Vector3(rectangle2.Max.x, rectangle2.Min.y);
            Vector3 allowable = Start - End;
        }
        if (rectangle1.Min.x > rectangle2.Min.x && rectangle1.Min.y == rectangle2.Max.y)
        {
            Start = rectangle1.Min;
            End = rectangle2.Max;
        }
        if (rectangle1.Min.x < rectangle2.Min.x && rectangle1.Min.y == rectangle2.Max.y)
        {
            Start = new Vector3(rectangle2.Min.x, rectangle2.Max.y);
            End = new Vector3(rectangle1.Max.x, rectangle1.Min.y);
        }

        if (rectangle1.Max.x == rectangle2.Min.x && rectangle2.Min.y > rectangle1.Min.y)
        {
            Start = rectangle1.Max;
            End = rectangle2.Min;
        }

        if (rectangle1.Max.x == rectangle2.Min.x && rectangle2.Min.y < rectangle1.Min.y)
        {
            Start = new Vector3(rectangle2.Min.x, rectangle2.Max.y);
            End = new Vector3(rectangle1.Max.x, rectangle1.Min.y);
        }

        if (rectangle1.Min.x == rectangle2.Max.x && rectangle2.Min.y > rectangle1.Min.y)
        {
            Start = new Vector3(rectangle1.Min.x, rectangle1.Max.y);
            End = new Vector3(rectangle2.Max.x, rectangle2.Min.y);
        }
        if (rectangle1.Min.x == rectangle2.Max.x && rectangle2.Min.y < rectangle1.Min.y)
        {
            Start = rectangle2.Max;
            End = rectangle1.Min;
        }
    
    }
}

public interface IPathFinder
{
    public IEnumerable<Vector2> GethPath(Vector2 A, Vector2 C, IEnumerable<Edge> edges);
}