<<<<<<< HEAD
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void PlayAgain()
    {
        Invoke("ClearData", 0.1f);
    }

    private void ClearData()
    {
        Box.ClearLists();
        Coin.ClearList();
        Enemy.ClearList();
        Turtle.ClearLists();
        PlayerManage.Clear();
        SceneManager.LoadScene("Game");
    }

    public void GoHome()
    {
        SceneManager.LoadScene("Menu");
    }

    public static void SaveCoins()
    {
        int currentCoinsAmmount = EncryptedPlayerPrefs.GetInt("coinsAmmount");

        EncryptedPlayerPrefs.SetInt("coinsAmmount", currentCoinsAmmount += PlayerManage.GetPlayerPoints());

        int currentHighScore = EncryptedPlayerPrefs.GetInt("highScore");

        if (currentHighScore < PlayerManage.GetPlayerPoints()) EncryptedPlayerPrefs.SetInt("highScore", PlayerManage.GetPlayerPoints());

        Debug.Log(EncryptedPlayerPrefs.GetInt("coinsAmmount"));
    }
}
=======
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void PlayAgain()
    {
        Invoke("ClearData", 0.1f);
    }

    private void ClearData()
    {
        Box.ClearLists();
        Coin.ClearList();
        Enemy.ClearList();
        Turtle.ClearLists();
        PlayerManage.Clear();
        SceneManager.LoadScene("Game");
    }

    public void GoHome()
    {
        SceneManager.LoadScene("Menu");
    }

    public static void SaveCoins()
    {
        int currentCoinsAmmount = EncryptedPlayerPrefs.GetInt("coinsAmmount");

        EncryptedPlayerPrefs.SetInt("coinsAmmount", currentCoinsAmmount += PlayerManage.GetPlayerPoints());

        Debug.Log(EncryptedPlayerPrefs.GetInt("coinsAmmount"));
    }
}
>>>>>>> ca61e779ba700dbc0df8a95b287f6ea4b74a5e89
