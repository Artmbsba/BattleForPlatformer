using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : Death
{
    protected override void OnDied()
    {
        Debug.Log("Игрок умер!");

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}