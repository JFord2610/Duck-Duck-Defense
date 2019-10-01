using UnityEngine;

public class AttackRadius : MonoBehaviour
{
    int segments = 50;
    public float radius = 5;
    LineRenderer line;

    void Start()
    {
        line = gameObject.GetComponent<LineRenderer>();

        line.positionCount = segments + 1;
        line.useWorldSpace = false;
        CreatePoints();
    }

    void CreatePoints()
    {
        float x, y;

        float angle = 20f;

        for (int i = 0; i < (segments + 1); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
            y = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;

            line.SetPosition(i, new Vector3(x, y, -2));

            angle += (360f / segments);
        }
    }
}
