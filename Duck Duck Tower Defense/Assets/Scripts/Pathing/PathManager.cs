using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class PathManager : MonoBehaviour
{
    public Path path;
    public GameObject point;

    public List<Point> wayPoints;
    List<GameObject> objects;

    private void Awake()
    {
        objects = new List<GameObject>();
        wayPoints = new List<Point>();
        for (int i = 0; i < path.WayPoints.Length; i++)
        {
            Vector3 w = path.WayPoints[i];
            GameObject g = Instantiate(point, transform);
            g.transform.position = w;
            g.GetComponentInChildren<MeshRenderer>().enabled = false;
            g.name = $"Point{i}";
            Point p = g.GetComponent<Point>();
            wayPoints.Add(p);
            objects.Add(g);
        }
    }

    // Unity editor stuff for visualizing path
    public bool generatePoints = false;
    public bool clearPoints = false;
    public bool showGizmos = false;
#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        if (clearPoints)
        {
            if (objects.Count != 0)
                foreach (GameObject obj in objects)
                {
                    DestroyImmediate(obj);
                }
            clearPoints = false;
            showGizmos = false;
        }
        if (generatePoints)
        {
            foreach (GameObject obj in objects)
            {
                DestroyImmediate(obj);
            }
            wayPoints = new List<Point>();
            for (int i = 0; i < path.WayPoints.Length; i++)
            {
                Vector3 w = path.WayPoints[i];
                GameObject g = Instantiate(point, transform);
                g.transform.position = w;
                g.name = $"Point{i}";
                Point p = g.GetComponent<Point>();
                wayPoints.Add(p);
                objects.Add(g);
            }
            generatePoints = false;
        }
        if (showGizmos)
        {
            for (int i = 0; i < wayPoints.Count; i++)
            {
                if (i != wayPoints.Count - 1)
                {
                    Vector3 p1 = wayPoints[i].transform.position;
                    Vector3 p2 = wayPoints[i + 1].transform.position;
                    Gizmos.DrawLine(p1, p2);
                }
            }
        }
    }
#endif
}
