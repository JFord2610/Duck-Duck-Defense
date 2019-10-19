using UnityEngine;
public class UnitFactory : MonoBehaviour
{
    [SerializeField] private GameObject goose = null;
    [SerializeField] private GameObject tower = null;
    [SerializeField] private GameManagerScript gameManager = null;
    [SerializeField] private Transform enemySpawnPoint = null;

    public GameObject SpawnGoose(GooseInfo info)
    {
        GameObject g = Instantiate(goose);
        g.transform.position = enemySpawnPoint.position + new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f), 0);
        g.GetComponent<Rigidbody2D>().rotation = -90;
        g.GetComponent<SpriteRenderer>().sprite = info.sprite;
        BaseGoose s = goose.GetComponent<BaseGoose>();
        s.stats = info.stats.CreateCopy();
        gameManager.geese.Add(g);
        s.Init();
        return g;
    }

    public GameObject SpawnTower(TowerInfo info)
    {
        GameObject t = Instantiate(tower);
        TowerController tc = t.GetComponent<TowerController>();
        tc.towerInfo = info;
        gameManager.towers.Add(t);
        return t;
    }
}
