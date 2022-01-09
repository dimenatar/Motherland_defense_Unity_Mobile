using UnityEngine;

public class Gym : MonoBehaviour
{
    [SerializeField] private float _spawnTime;
    [SerializeField] private Transform _spawnPoint;
    
    [Header("Set up hero")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float health;
    [SerializeField] private float attackDelay;
    [SerializeField] private Transform basePointToMove;
    [SerializeField] private float arrivalToPointRange;
    [SerializeField] private int damage;

    public void SpawnHero()
    {
        SetUpHero(InstantiateHero());
    }
    public void SpawnHeroAfterTime()
    {
        Invoke(nameof(SpawnHero), _spawnTime);
    }

    private GameObject InstantiateHero()
    {
        return Instantiate(Resources.Load<GameObject>("HeroPrefab"), _spawnPoint.position, transform.rotation);
    }

    private void SetUpHero(GameObject hero)
    {
        hero.transform.position = _spawnPoint.position;
        hero.SetActive(true);
        hero.GetComponent<Hero>().InitializeHero(moveSpeed, health, attackDelay, basePointToMove, arrivalToPointRange, damage);
        hero.GetComponent<Hero>().OnDied += SpawnHeroAfterTime;
    }

    private void Start()
    {
        SpawnHero();
    }
}
