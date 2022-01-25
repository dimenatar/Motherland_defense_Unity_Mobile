using UnityEngine;

public class Gym : Tower
{
    [SerializeField] private float _spawnTime;
    [SerializeField] private Transform _spawnPoint;

    [Header("Set up hero")]
    [SerializeField] private CharacterData _characterData;
    [SerializeField] private Transform basePointToMove;
    [SerializeField] private float arrivalToPointRange;

    private ViewPanel _viewPanel;
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
        hero.GetComponent<Hero>().InitializeHero(_characterData ,basePointToMove, arrivalToPointRange, _viewPanel);
        hero.GetComponent<Hero>().OnDied += SpawnHeroAfterTime;
    }

    private void Start()
    {
        _viewPanel = transform.parent.GetComponent<ViewPanel>();
        SpawnHero();
    }
}
