using UnityEngine;
using System.Collections;

public class UnitFactory : MonoBehaviour
{
    [SerializeField]
    private GameObject goose;
    [SerializeField]
    private GameObject duck;
    [SerializeField]
    private GameManagerScript gameManager;

    public GameObject SpawnGeese(float _speed, float _health)
    {
        GameObject g = Instantiate(goose);
        GeeseScript s = goose.GetComponent<GeeseScript>();
        s.speed = _speed;
        s.health = _health;
        gameManager.geese.Add(g);
        return g;
    }
    public GameObject SpawnDuck()
    {
        GameObject d = Instantiate(duck);
        return d;
    }
}
