using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using UnityEditor.Animations;
using System;

[RequireComponent(typeof(BaseAction))]
public class TowerController : MonoBehaviour
{
    public TowerInfo towerInfo = null;

    [Tooltip("Unique name of the tower used for identification")]
    [SerializeField] string towerName = "";

    #region Tower Stats
    [Header("Tower Stats")]
    public TowerStats towerStats = null;
    [Tooltip("Damage by the tower to enemies")]
    [SerializeField] private float _damage = 0;
    [Tooltip("Effective range of tower")]
    [SerializeField] private float _attackRange = 0;
    [Tooltip("Speed of the towers action call measured in attacks per second")]
    [SerializeField] private float _attackSpeed = 0;
    [Tooltip("Default targeting property of tower")]
    [SerializeField] private ETargetingType _targetingType = ETargetingType.Closest;

    public float Damage
    {
        get { return towerInfo.towerStats.damage; }
        set
        {
            towerInfo.towerStats.damage = value;
            _damage = value;
        }
    }
    public float AttackRange
    {
        get { return towerInfo.towerStats.attackRange; }
        set
        {
            towerInfo.towerStats.attackRange = value;
            _attackRange = value;
        }
    }
    public float AttackSpeed
    {
        get { return towerInfo.towerStats.attackSpeed; }
        set
        {
            towerInfo.towerStats.attackSpeed = value;
            _attackSpeed = value;
        }
    }
    public ETargetingType TargetingType
    {
        get { return towerInfo.towerStats.targetingType; }
        set
        {
            towerInfo.towerStats.targetingType = value;
            _targetingType = value;
        }
    }
    #endregion

    #region ProjectileInfo
    public ProjectileInfo projInfo = null;
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

    public UpgradeTree upgradeTree;
    public List<Modifier> modifiers;

    internal bool alive = false;
    internal bool selected = false;
    internal bool isHovered = false;

    internal bool colliding
    {
        get { return collisions > 0; }
    }
    private int collisions = 0;

    GameManagerScript gameManager = null;
    TowerGUIManager towerGUIManager = null;
    SpriteRenderer attackRadius = null;
    SpriteRenderer spriteRenderer = null;
    Animator anim = null;
    internal BaseAction action = null;

    private void Start()
    {
        towerInfo = towerInfo.CreateCopy();
        upgradeTree = towerInfo.upgradeTree;
        projInfo = towerInfo.projectileInfo;
        towerStats = towerInfo.towerStats;
        projInfo = towerInfo.projectileInfo;
        spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        attackRadius = transform.GetChild(2).gameObject.GetComponent<SpriteRenderer>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        towerGUIManager = GameObject.Find("TowerGUIManager").GetComponent<TowerGUIManager>();
        anim = spriteRenderer.gameObject.GetComponent<Animator>();
        attackRadius.transform.localScale = new Vector2(AttackRange * 2, AttackRange * 2);
        anim.speed = 1 / AttackSpeed;
        action = (BaseAction)gameObject.AddComponent(System.Type.GetType(towerInfo.action));
        spriteRenderer.sprite = towerInfo.towerVisuals.sprite;
        anim.runtimeAnimatorController = towerInfo.towerVisuals.animController;
    }

    private void FixedUpdate()
    {
        if (alive)
            action.Action();
    }

    internal void AddModifier(Modifier mod)
    {
        if ((mod.modType & EModifierType.Stats) == EModifierType.Stats)
        {
            Damage += mod.damage;
            AttackRange += mod.attackRange;
            AttackSpeed += mod.attackSpeed;
            if(mod.projectileInfo)
            {
                projInfo.speed += mod.projectileInfo.speed;
                projInfo.bounces += mod.projectileInfo.bounces;
                projInfo.pierce += mod.projectileInfo.pierce;
                projInfo.speed += mod.projectileInfo.timeBeforeDeath;
            }
        }
        if ((mod.modType & EModifierType.Action) == EModifierType.Action)
        {
            gameObject.AddComponent(Type.GetType(mod.action));
        }
        if ((mod.modType & EModifierType.Visual) == EModifierType.Visual)
        {
            //Do visual stuff
        }
        modifiers.Add(mod);
    }

    internal void RemoveModifier(Modifier mod)
    {
        if ((mod.modType & EModifierType.Stats) == EModifierType.Stats)
        {
            Damage -= mod.damage;
            AttackRange -= mod.attackRange;
            AttackSpeed -= mod.attackSpeed;
            if (mod.projectileInfo)
            {
                projInfo.speed -= mod.projectileInfo.speed;
                projInfo.bounces -= mod.projectileInfo.bounces;
                projInfo.pierce -= mod.projectileInfo.pierce;
                projInfo.speed -= mod.projectileInfo.timeBeforeDeath;
            }
        }
        if ((mod.modType & EModifierType.Action) == EModifierType.Action)
        {
            Destroy(gameObject.GetComponent(mod.action));
        }
        if ((mod.modType & EModifierType.Visual) == EModifierType.Visual)
        {
            //Do visual stuff
        }
        modifiers.Remove(mod);
    }

    internal void Upgrade(int line)
    {
        TowerUpgrade upgrade = upgradeTree.GetNextInLine(line);
        if (upgrade == null)
            return;
        AddModifier(upgrade.modifier);
        upgrade.hasUpgrade = true;

        attackRadius.size = new Vector2(AttackRange * 2, AttackRange * 2);
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
        towerGUIManager.TowerClicked(this, upgradeTree, towerStats, transform.position);
    }

    private void OnMouseEnter()
    {
        if (!alive) return;
        isHovered = true;
        StartCoroutine("ScaleUp");
        attackRadius.gameObject.SetActive(true);
    }
    private void OnMouseExit()
    {
        if (!alive) return;
        isHovered = false;
        StartCoroutine("ScaleDown");
        attackRadius.gameObject.SetActive(false);
    }
    IEnumerator ScaleUp()
    {
        for (int i = 0; i < 5; i++)
        {
            spriteRenderer.transform.localScale += new Vector3(scaleFactor / 5, scaleFactor / 5, 0.0f);
            yield return new WaitForSeconds(0.005f);
        }
    }
    IEnumerator ScaleDown()
    {
        for (int i = 0; i < 5; i++)
        {
            spriteRenderer.transform.localScale -= new Vector3(scaleFactor / 5, scaleFactor / 5, 0.0f);
            yield return new WaitForSeconds(0.005f);
        }
    }

    internal List<BaseGoose> GetNearbyGeese()
    {
        List<BaseGoose> geese = new List<BaseGoose>();

        foreach (GameObject g in gameManager.geese)
        {
            float dst = (g.transform.position - transform.position).magnitude;
            if (dst <= AttackRange)
                geese.Add(g.GetComponent<BaseGoose>());
        }

        return geese;
    }
    internal BaseGoose GetClosestGoose()
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
    internal BaseGoose GetFirstGoose()
    {
        BaseGoose goose = null;
        Vector3 pos = transform.position;

        List<BaseGoose> geese = GetNearbyGeese();
        foreach (BaseGoose g in geese)
        {
            Vector3 goosePos = g.transform.position;
            if ((pos - goosePos).magnitude < AttackRange)
            {
                goose = g;
                break;
            }
        }
        return goose;
    }
    internal BaseGoose GetLastGoose()
    {
        BaseGoose goose = null;
        Vector3 pos = transform.position;
        List<BaseGoose> geese = GetNearbyGeese();
        geese.Reverse();
        foreach (BaseGoose g in geese)
        {
            Vector3 goosePos = g.transform.position;
            if ((pos - goosePos).magnitude < AttackRange)
            {
                goose = g;
                break;
            }
        }
        return goose;
    }

    internal void RotateToPoint(Vector3 p)
    {
        Vector3 dir = p - spriteRenderer.transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        spriteRenderer.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }

    private void OnValidate()
    {
        if (towerStats != null)
        {
            towerStats.damage = Damage;
            towerStats.attackSpeed = AttackSpeed;
            towerStats.attackRange = AttackRange;
            towerStats.targetingType = TargetingType;
        }
        if (towerVisuals != null)
        {
            towerVisuals.sprite = _sprite;
            towerVisuals.animController = _animController;
        }
    }
}
