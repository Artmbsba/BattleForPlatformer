public class EnemyDeath : Death
{
     protected override void OnDied()
    {
        Destroy(gameObject);
    }
}