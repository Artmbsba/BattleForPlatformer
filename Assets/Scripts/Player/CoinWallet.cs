using UnityEngine;

public class CoinWallet : MonoBehaviour
{
    private int _coinCount;

    public int CoinCount => _coinCount;

    public void AddCoin(Coin coin)
    {
        _coinCount++;

        Debug.Log("Вы подобрали монетку");
        Debug.Log($"Баланс: {CoinCount}");
    }
}