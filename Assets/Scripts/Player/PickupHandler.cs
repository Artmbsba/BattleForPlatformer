using UnityEngine;

[RequireComponent(typeof(CollectibleDetector))]
[RequireComponent (typeof(CoinWallet))]
public class PickupHandler: MonoBehaviour
{
    [SerializeField] private Health _health;

    private CollectibleDetector _detector;
    private CoinWallet _wallet;

    private void Awake()
    {
        _detector = GetComponent<CollectibleDetector>();
        _wallet = GetComponent<CoinWallet>();
    }

    private void OnEnable()
    {
        _detector.CollectibleDetected += OnCollectibleDetected;
    }

    private void OnDisable()
    {
        _detector.CollectibleDetected -= OnCollectibleDetected;
    }

    private void OnCollectibleDetected(ICollectible collectible)
    {
        switch (collectible)
        {
            case Coin coin:
                OnCoinDetected(coin);
                break;

            case MedKit medKit:
                OnMedKitDetected(medKit);
                break;
        }
    }

    private void OnMedKitDetected(MedKit medKit)
    {
        bool ishealed = _health.Heal(medKit.HealthUnits);

        Debug.Log($"Çהמנמגüÿ: {_health.CurrentHealth}");

        if (ishealed)
            medKit.Collect();
    }

    private void OnCoinDetected(Coin coin)
    {
        _wallet.AddCoin(coin);

        coin.Collect();
    }
}