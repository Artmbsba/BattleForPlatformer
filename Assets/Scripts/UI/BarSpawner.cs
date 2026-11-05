using UnityEngine;

public class BarSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _playerBarPrefab;
    [SerializeField] private GameObject _enemyBarPrefab;

    public static BarSpawner Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void CreatePlayerBar(Health health, Transform target)
    {
        CreateBar(_playerBarPrefab, health, target);
    }

    public void CreateEnemyBar(Health health, Transform target)
    {
        CreateBar(_enemyBarPrefab, health, target);
    }

    private void CreateBar(GameObject barPrefab, Health health, Transform target)
    {
        GameObject bar = Instantiate(barPrefab);

        if (bar.TryGetComponent(out BarFollow follow))
            follow.Setup(target);

        if (bar.TryGetComponent(out ViewHPBar hpBar))
            hpBar.Setup(health);
    }
}
