using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGoose : MonoBehaviour
{
    #region stat fields
    public GooseStats stats;
    [SerializeField] private float _currentHealth = 0;
    [SerializeField] private float _maxHealth = 0;
    [SerializeField] private float _speed = 0;
    [SerializeField] private int _lifeWorth = 0;
    [SerializeField] private int _goldWorth = 0;

    public float CurrentHealth
    {
        get { return _currentHealth; }
        set
        {
            if (value > _maxHealth)
                _currentHealth = _maxHealth;
            else if (value <= 0)
                Die(false);
            else
                _currentHealth = value;
        }
    }
    public float MaxHealth
    {
        get { return _maxHealth; }
        set
        {
            if (value > _maxHealth)
            {
                CurrentHealth += value - _maxHealth;
                _maxHealth = value;
            }
            else if (value < _maxHealth)
            {
                _maxHealth = value;
                if (CurrentHealth > _maxHealth)
                    CurrentHealth = _maxHealth;
            }
        }
    }
    public float Speed
    {
        get { return _speed; }
        set { _speed = value; }
    }
    public int LifeWorth
    {
        get { return _lifeWorth; }
        set { _lifeWorth = value; }
    }
    public int GoldWorth
    {
        get { return _goldWorth; }
        set { _goldWorth = value; }
    }
    #endregion

    public bool moving = false;

    GameManagerScript gameManager = null;
    UnitController unitController = null;
    List<GooseModifier> modifiers = null;

    public Vector3 Position { get { return transform.position; } }

    private void Awake()
    {
        modifiers = new List<GooseModifier>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
    }
    public void Init()
    {
        _maxHealth = stats.maxHealth;
        _currentHealth = _maxHealth;
        _speed = stats.speed;
        _lifeWorth = stats.lifeWorth;
        _goldWorth = stats.goldWorth;
        moving = false;
    }
    
    internal void AddModifier(GooseModifier mod)
    {
        if ((mod.modType & EModifierType.Stats) == EModifierType.Stats)
        {
            MaxHealth += mod.health;
            MaxHealth += stats.maxHealth * mod.percentHealth;
            Speed += mod.speed;
            Speed += stats.speed * mod.percentSpeed;
            LifeWorth += mod.lifeWorth;
            GoldWorth += mod.goldWorth;
            GoldWorth += Mathf.RoundToInt(stats.goldWorth * mod.percentGoldWorth);
        }
        if ((mod.modType & EModifierType.Action) == EModifierType.Action)
        {
            gameObject.AddComponent(System.Type.GetType(mod.action));
        }
        if ((mod.modType & EModifierType.Visual) == EModifierType.Visual)
        {
            //Do visual stuff
        }
        modifiers.Add(mod);
    }

    internal void RemoveModifier(GooseModifier mod)
    {
        if ((mod.modType & EModifierType.Stats) == EModifierType.Stats)
        {
            MaxHealth -= mod.health;
            MaxHealth -= stats.maxHealth * mod.percentHealth;
            Speed -= mod.speed;
            Speed -= stats.speed * mod.percentSpeed;
            LifeWorth -= mod.lifeWorth;
            GoldWorth -= mod.goldWorth;
            GoldWorth -= Mathf.RoundToInt(stats.goldWorth * mod.percentGoldWorth);
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

    public void Damage(float damage)
    {
        CurrentHealth -= damage;
    }

    public void Die(bool endOfPath)
    {
        if (!endOfPath)
            gameManager.player.AddMoney(_goldWorth);
        if (endOfPath)
            gameManager.player.Damage(_lifeWorth);
        gameManager.geese.Remove(gameObject);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "End")
            Die(true);
    }
}
