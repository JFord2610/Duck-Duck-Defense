using UnityEngine;
using System.Collections;

public class UnitFactory : MonoBehaviour
{
    public GameObject Goose;
    public GameObject Duck;
    public GameManagerScript gameManager;

    public GameObject SpawnGeese(float _speed, float _health)
    {
        GameObject g = Instantiate(Goose);
        GeeseScript s = Goose.GetComponent<GeeseScript>();
        //s.turnThreshold = 0.3f; //+ Random.Range(-0.08f, 0.08f);
        s.speed = _speed;
        s.health = _health;
        gameManager.geese.Add(g);
        return g;
    }
    public GameObject SpawnDuck()
    {
        GameObject d = Instantiate(Duck);
        return d;
    }
}
