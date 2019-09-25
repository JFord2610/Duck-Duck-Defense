using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    public float damage;
    public float attackRange;
    public float attackSpeed;

    public Transform sprite;
    public GameManagerScript gameManager;

    private GeeseScript[] inRangeGeese;

    private bool active = true;
    private bool AttachedToMouse = false;
    private bool AbleToAttack = false;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
    }

    void Update()
    {
        if (AttachedToMouse)
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(pos.x, pos.y, 0.0f);
            if(Input.GetMouseButtonDown(0))
            {
                DeAttachFromMouse();
                AbleToAttack = true;
                gameManager.playerBusy = false;
            }
        }

        if (AbleToAttack)
        {
            inRangeGeese = findGeeseInRange();
            if (inRangeGeese.Length > 0)
            {
                GeeseScript closest = inRangeGeese[0];
                float closestDst = float.MaxValue;
                foreach (GeeseScript goose in inRangeGeese)
                {
                    float dst = (transform.position - goose.Position).magnitude;
                    if (dst < closestDst)
                    {
                        closest = goose;
                        closestDst = dst;
                    }
                }
                RotateToPoint(gameManager.geese[0].transform.position);
                if(active)
                {
                    AttackGoose(closest);
                    active = false;
                    StartCoroutine("AttackCooldown");
                }
            }
        }
    }

    public void AttachToMouse()
    {
        AttachedToMouse = true;
    }

    public void DeAttachFromMouse()
    {
        AttachedToMouse = false;
    }

    void AttackGoose(GeeseScript goose)
    {
        goose.health -= damage;
        if (goose.health <= 0)
            goose.Die();
    }

    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(1 / attackSpeed);
        active = true;
    }

    GeeseScript[] findGeeseInRange()
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();
        List<GeeseScript> geese = new List<GeeseScript>();
        foreach (GameObject g in gameManager.geese)
        {
            GeeseScript goose = g.GetComponent<GeeseScript>();
            if (goose == null) continue;

            if ((transform.position - goose.Position).magnitude < attackRange)
                geese.Add(goose);
        }
        return geese.ToArray();
    }

    void RotateToPoint(Vector3 p)
    {
        Vector3 dir = p - sprite.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        sprite.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }

    private void OnDrawGizmos()
    {
        //Handles.color = Color.red;
        //Handles.DrawWireDisc(transform.position, Vector3.back, attackRange);
        //Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
