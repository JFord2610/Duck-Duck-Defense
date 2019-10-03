using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public abstract class BaseTower : MonoBehaviour
{
    public float damage = 0, attackRange = 0, attackSpeed = 0;
    [SerializeField] float scaleFactor = 0.15f;

    protected bool onCooldown = false;
    protected bool hovering = false;
    public bool alive = false;
    public ETargetingType targetingType = ETargetingType.Closest;

    public bool colliding
    {
        get { return collisions > 0; }
    }
    private int collisions = 0;

    protected GameManagerScript gameManager = null;
    protected GameObject attackRadiusObj = null;
    protected Transform spriteTransform = null;
    protected SpriteRenderer sprite = null;
    protected Animator anim = null;

    private void Start()
    {
        Init();
    }

    protected virtual void Init()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        spriteTransform = transform.GetChild(0);
        sprite = spriteTransform.gameObject.GetComponent<SpriteRenderer>();
        anim = spriteTransform.gameObject.GetComponent<Animator>();
        anim.speed = 1 / attackSpeed;
        attackRadiusObj = transform.GetChild(2).gameObject;
        attackRadiusObj.GetComponent<SpriteRenderer>().size = new Vector2(attackRange * 2, attackRange * 2);
    }

    private void Update()
    {
        if (alive)
            Action();
    }

    protected abstract void Action();

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(1 / attackSpeed);
        onCooldown = false;
    }

    protected List<BaseGoose> GetNearbyGeese()
    {
        List<BaseGoose> geese = new List<BaseGoose>();

        foreach (GameObject g in gameManager.geese)
        {
            float dst = (g.transform.position - transform.position).magnitude;
            if (dst <= attackRange)
                geese.Add(g.GetComponent<BaseGoose>());
        }

        return geese;
    }

    protected BaseGoose GetClosestGoose()
    {
        BaseGoose goose = null;
        Vector3 pos = transform.position;
        float dst = int.MaxValue;

        List<BaseGoose> geese = GetNearbyGeese();
        foreach (BaseGoose g in geese)
        {
            Vector3 goosePos = g.transform.position;
            float mag = (goosePos - pos).magnitude;
            if (mag < dst)
            {
                dst = mag;
                goose = g;
            }
        }

        return goose;
    }

    protected BaseGoose GetFirstGoose()
    {
        BaseGoose goose = null;
        Vector3 pos = transform.position;

        List<BaseGoose> geese = GetNearbyGeese();
        foreach (BaseGoose g in geese)
        {
            Vector3 goosePos = g.transform.position;
            if ((pos - goosePos).magnitude < attackRange)
            {
                goose = g;
                break;
            }
        }
        return goose;
    }

    protected BaseGoose GetLastGoose()
    {
        BaseGoose goose = null;
        Vector3 pos = transform.position;
        List<BaseGoose> geese = GetNearbyGeese();
        geese.Reverse();
        foreach (BaseGoose g in geese)
        {
            Vector3 goosePos = g.transform.position;
            if ((pos - goosePos).magnitude < attackRange)
            {
                goose = g;
                break;
            }
        }
        return goose;
    }

    protected void RotateToPoint(Vector3 p)
    {
        Vector3 dir = p - spriteTransform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        spriteTransform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collisions++;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collisions--;
    }

    private void OnMouseEnter()
    {
        if (!alive) return;
        hovering = true;
        StartCoroutine("ScaleUp");
        attackRadiusObj.SetActive(true);
    }
    IEnumerator ScaleUp()
    {
        for (int i = 0; i < 5; i++)
        {
            spriteTransform.localScale += new Vector3(scaleFactor / 5, scaleFactor / 5, 0.0f);
            yield return new WaitForSeconds(0.005f);
        }
    }

    private void OnMouseExit()
    {
        if (!alive) return;
        hovering = false;
        StartCoroutine("ScaleDown");
        attackRadiusObj.SetActive(false);
    }
    IEnumerator ScaleDown()
    {
        for (int i = 0; i < 5; i++)
        {
            spriteTransform.localScale -= new Vector3(scaleFactor / 5, scaleFactor / 5, 0.0f);
            yield return new WaitForSeconds(0.005f);
        }
    }
}
