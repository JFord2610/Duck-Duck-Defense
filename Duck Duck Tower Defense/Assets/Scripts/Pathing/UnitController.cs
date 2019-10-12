using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    [SerializeField] PathManagerScript pathManager = null;
    [SerializeField] Rigidbody2D rb2d = null;
    public float speed = 5;

    private void Start()
    {
        pathManager = GameObject.Find("PathManager").GetComponent<PathManagerScript>();
    }

    private void FixedUpdate()
    {
        Node n = pathManager.GetNodeFromWorldPoint(transform.position);
        rb2d.velocity = Vector2.Lerp(rb2d.velocity, n.vector * speed, Time.deltaTime * 7);
        float angle = Mathf.Atan2(n.vector.y, n.vector.x) * Mathf.Rad2Deg - 90;
        rb2d.rotation = Mathf.LerpAngle(rb2d.rotation, angle, Time.deltaTime * 7);
    }
}
