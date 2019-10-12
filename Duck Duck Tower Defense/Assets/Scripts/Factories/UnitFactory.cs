using UnityEngine;
public class UnitFactory : MonoBehaviour
{
    [SerializeField] private GameObject goose = null;
    [SerializeField] private GameObject tower = null;
    [SerializeField] private GameManagerScript gameManager = null;
    [SerializeField] private TowerInfo geraldInfo = null;
    [SerializeField] private Transform enemySpawnPoint = null;

    public GameObject SpawnGoose(float _speed, float _health, int _lifeWorth, int _goldWorth, Color _color)
    {
        GameObject g = Instantiate(goose);
        g.transform.position = enemySpawnPoint.position + new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f), 0);
        g.GetComponent<Rigidbody2D>().rotation = -90;
        BaseGoose s = goose.GetComponent<BaseGoose>();
        g.GetComponentInChildren<SpriteRenderer>().color = _color;
        g.GetComponent<UnitController>().speed = _speed;
        s.speed = _speed;
        s.health = _health;
        s.lifeWorth = _lifeWorth;
        s.goldWorth = _goldWorth;
        gameManager.geese.Add(g);
        return g;
    }

    public GameObject SpawnTower()
    {
        GameObject t = Instantiate(tower);
        TowerController tc = t.GetComponent<TowerController>();
        tc.towerInfo = geraldInfo;
        return t;
    }
}
