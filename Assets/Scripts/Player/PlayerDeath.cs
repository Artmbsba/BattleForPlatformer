using UnityEngine.SceneManagement;

public class PlayerDeath : Death
{
    protected override void OnDied()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}