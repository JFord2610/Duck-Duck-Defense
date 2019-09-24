using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHolder : MonoBehaviour
{
    public GameManagerScript gameManager;

    public Text lifeText;
    public Text moneyText;

    [SerializeField]
    private int _life;
    [SerializeField]
    private int _money;
    public int life
    {
        get {
            return _life;
        }
        set
        {
            _life = value;
            lifeText.text = $"Life: {_life}";
        }
    }
    public int money
    {
        get {
            return _money;
        }
        set {
            _money = value;
            moneyText.text = $"Money: {_money}";
        }
    }

    public void AddMoney(int m)
    {
        if (gameManager.playerBusy) return;
        money += m;
    }

    public void TakeMoney(int m)
    {
        if (gameManager.playerBusy) return;
        money -= m;
    }

    public void Heal(int l)
    {
        if (gameManager.playerBusy) return;
        life += l;
    }

    public void Damage(int damage)
    {
        life -= damage;
    }
}
