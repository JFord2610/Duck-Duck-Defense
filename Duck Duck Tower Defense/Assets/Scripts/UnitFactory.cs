using UnityEngine;
public class UnitFactory : MonoBehaviour
{
    [SerializeField]
    private GameObject goose = null;
    [SerializeField]
    private GameObject duck = null;
    [SerializeField]
    private GameManagerScript gameManager = null;

    public GameObject SpawnGoose(float _speed, float _health, int _lifeWorth, int _goldWorth)
    {
        GameObject g = Instantiate(goose);
        GeeseScript s = goose.GetComponent<GeeseScript>();
        s.speed = _speed;
        s.health = _health;
        s.lifeWorth = _lifeWorth;
        s.goldWorth = _goldWorth;
        gameManager.geese.Add(g);
        return g;
    }

    public GameObject SpawnDuck()
    {
        GameObject d = Instantiate(duck);
        return d;
    }
}
