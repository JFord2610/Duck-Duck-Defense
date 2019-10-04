using UnityEngine;
public class UnitFactory : MonoBehaviour
{
    [SerializeField]
    private GameObject goose = null;
    [SerializeField]
    private GameObject tower = null;
    [SerializeField]
    private GameManagerScript gameManager = null;

    public GameObject SpawnGoose(float _speed, float _health, int _lifeWorth, int _goldWorth, Color _color)
    {
        GameObject g = Instantiate(goose);
        BaseGoose s = goose.GetComponent<BaseGoose>();
        g.GetComponentInChildren<SpriteRenderer>().color = _color;
        s.speed = _speed;
        s.health = _health;
        s.lifeWorth = _lifeWorth;
        s.goldWorth = _goldWorth;
        gameManager.geese.Add(g);
        return g;
    }

    public GameObject SpawnTower()
    {
        GameObject d = Instantiate(tower);
        return d;
    }
}
