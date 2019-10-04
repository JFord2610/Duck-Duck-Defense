using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using UnityEditor.Animations;

[RequireComponent(typeof(BaseTowerType))]
public class TowerController : MonoBehaviour
{
    [Tooltip("Unique name of the tower used for identification")]
    [SerializeField] string towerName = "";

    #region Tower Stats
    [Header("Tower Stats")]
    public TowerStats towerStats = null;
    [Tooltip("Damage by the tower to enemies")]
    [SerializeField] float _damage = 0;
    [Tooltip("Effective range of tower")]
    [SerializeField] float _attackRange = 0;
    [Tooltip("Speed of the towers action call measured in attacks per second")]
    [SerializeField] float _attackSpeed = 0;
    [Tooltip("Default targeting property of tower")]
    [SerializeField] ETargetingType _targetingType = ETargetingType.Closest;
    #endregion

    #region Tower Visuals
    [Space]
    [Header("Visuals")]
    public TowerVisuals towerVisuals = null;
    [Tooltip("Factor of growth when hovered overwith mouse")]
    [SerializeField] float scaleFactor = 0.15f;
    [Tooltip("Sprite of the tower")]
    [SerializeField] Sprite _sprite = null;
    [Tooltip("Animator of the sprite")]
    [SerializeField] AnimatorController _animController = null;
    #endregion

    [HideInInspector]
    public bool alive = false;

    public bool colliding
    {
        get { return collisions > 0; }
    }
    private int collisions = 0;
    bool selected = false;

    GameManagerScript gameManager = null;
    TowerGUIManager towerGUIManager = null;
    SpriteRenderer attackRadius = null;
    Transform spriteTransform = null;
    BaseTowerType towerType = null;
    Animator anim = null;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        towerGUIManager = GameObject.Find("TowerGUIManager").GetComponent<TowerGUIManager>();
        attackRadius = transform.GetChild(2).gameObject.GetComponent<SpriteRenderer>();
        spriteTransform = transform.GetChild(0).transform;
        anim = spriteTransform.gameObject.GetComponent<Animator>();
        towerType = gameObject.GetComponent<BaseTowerType>();
        attackRadius.size = new Vector2(_attackRange * 2, _attackRange * 2);
        anim.speed = 1 / _attackSpeed;
    }

    private void FixedUpdate()
    {
        if (alive)
            towerType.Action();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collisions++;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        collisions--;
    }

    private void OnMouseDown()
    {
        selected = true;
        towerGUIManager.TowerClicked(towerName, towerStats, transform.position);
    }

    private void OnMouseEnter()
    {
        if (!alive) return;
        StartCoroutine("ScaleUp");
        attackRadius.gameObject.SetActive(true);
    }
    private void OnMouseExit()
    {
        if (!alive) return;
        StartCoroutine("ScaleDown");
        attackRadius.gameObject.SetActive(false);
    }
    IEnumerator ScaleUp()
    {
        for (int i = 0; i < 5; i++)
        {
            spriteTransform.localScale += new Vector3(scaleFactor / 5, scaleFactor / 5, 0.0f);
            yield return new WaitForSeconds(0.005f);
        }
    }
    IEnumerator ScaleDown()
    {
        for (int i = 0; i < 5; i++)
        {
            spriteTransform.localScale -= new Vector3(scaleFactor / 5, scaleFactor / 5, 0.0f);
            yield return new WaitForSeconds(0.005f);
        }
    }

    public List<BaseGoose> GetNearbyGeese()
    {
        List<BaseGoose> geese = new List<BaseGoose>();

        foreach (GameObject g in gameManager.geese)
        {
            float dst = (g.transform.position - transform.position).magnitude;
            if (dst <= _attackRange)
                geese.Add(g.GetComponent<BaseGoose>());
        }

        return geese;
    }
    public BaseGoose GetClosestGoose()
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
    public BaseGoose GetFirstGoose()
    {
        BaseGoose goose = null;
        Vector3 pos = transform.position;

        List<BaseGoose> geese = GetNearbyGeese();
        foreach (BaseGoose g in geese)
        {
            Vector3 goosePos = g.transform.position;
            if ((pos - goosePos).magnitude < _attackRange)
            {
                goose = g;
                break;
            }
        }
        return goose;
    }
    public BaseGoose GetLastGoose()
    {
        BaseGoose goose = null;
        Vector3 pos = transform.position;
        List<BaseGoose> geese = GetNearbyGeese();
        geese.Reverse();
        foreach (BaseGoose g in geese)
        {
            Vector3 goosePos = g.transform.position;
            if ((pos - goosePos).magnitude < _attackRange)
            {
                goose = g;
                break;
            }
        }
        return goose;
    }

    public void RotateToPoint(Vector3 p)
    {
        Vector3 dir = p - spriteTransform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        spriteTransform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }

    private void OnValidate()
    {
        if (towerStats != null)
        {
            towerStats.damage = _damage;
            towerStats.attackSpeed = _attackSpeed;
            towerStats.attackRange = _attackRange;
            towerStats.targetingType = _targetingType;
        }
        if (towerVisuals != null)
        {
            towerVisuals.sprite = _sprite;
            towerVisuals.animController = _animController;
        }
    }
}
