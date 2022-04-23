using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;

public class Mainmenu : MonoBehaviour
{
    [SerializeField]
    private GameObject mainMenu;
    [SerializeField]
    private GameObject optionsMenu;
    [SerializeField]
    private GameObject skinsMenu;
    [SerializeField]
    private GameObject skinsMenuBack;
    [SerializeField]
    private GameObject skinsMenuBuy;
    [SerializeField]
    private GameObject skinsMenuBuyCoin;

    [SerializeField]
    private GameObject coinText;

    public void PlayGame()
    {
        Invoke("ClearData", 0.2f);
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

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GoOptions()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void GoSkings()
    {
        mainMenu.SetActive(false);
        skinsMenu.SetActive(true);

        if(EncryptedPlayerPrefs.GetInt("isCharacterBought" + CharacterSelection.GetSelectedCharacter()) == 1)
        {
            skinsMenuBack.SetActive(true);
            skinsMenuBuy.SetActive(false);
            skinsMenuBuyCoin.SetActive(false);
        } else
        {
            skinsMenuBack.SetActive(false);
            skinsMenuBuy.SetActive(true);
            skinsMenuBuyCoin.SetActive(true);
            skinsMenuBuy.GetComponentInChildren<TextMeshProUGUI>().text = skinsMenu.GetComponent<CharacterSelection>().GetPrices(CharacterSelection.GetSelectedCharacter()).ToString();
        }

        skinsMenu.GetComponent<CharacterSelection>().characters[CharacterSelection.GetSelectedCharacter()].SetActive(true);
    }

    private void Start()
    {
        EncryptedPlayerPrefs.keys = new string[5];
        EncryptedPlayerPrefs.keys[0] = "127ee06e3ea41f6598f9fbea933d25d661614c461e91504ef1f488273913e206";
        EncryptedPlayerPrefs.keys[1] = "4a8c553b7fce0e25909707b24db4ae52c0fa0dc439d666b8387e995221108424";
        EncryptedPlayerPrefs.keys[2] = "1cd5a87bc2fd7585d37deaff34ec5374f8d2fee68327819660d3ea8d2023fa9f";
        EncryptedPlayerPrefs.keys[3] = "ebf25c933e017fbcc7fca47c7cbddd409c31fc5c66789bb2184320a3c548e9a7";
        EncryptedPlayerPrefs.keys[4] = "ddc94b0a69d9e608220cc5123d7eaccce92fcdb1db1409cc441e92c56cf7c774";

        if (!EncryptedPlayerPrefs.HasKey("coinsAmmount"))
        {
            EncryptedPlayerPrefs.SetInt("coinsAmmount", 0);
        }

        if (!EncryptedPlayerPrefs.HasKey("selectedCharacter"))
        {
            EncryptedPlayerPrefs.SetInt("selectedCharacter", 0);
        }

        if (!EncryptedPlayerPrefs.HasKey("isCharacterBought0"))
        {
            EncryptedPlayerPrefs.SetInt("isCharacterBought0", 1);
            EncryptedPlayerPrefs.SetInt("isCharacterBought1", 0);
            EncryptedPlayerPrefs.SetInt("isCharacterBought2", 0);
            EncryptedPlayerPrefs.SetInt("isCharacterBought3", 0);
            EncryptedPlayerPrefs.SetInt("isCharacterBought4", 0);
            EncryptedPlayerPrefs.SetInt("isCharacterBought5", 0);
            EncryptedPlayerPrefs.SetInt("isCharacterBought6", 0);
            EncryptedPlayerPrefs.SetInt("isCharacterBought7", 0);
            EncryptedPlayerPrefs.SetInt("isCharacterBought8", 0);
            EncryptedPlayerPrefs.SetInt("isCharacterBought9", 0);
            EncryptedPlayerPrefs.SetInt("isCharacterBought10", 0);
        }


        coinText.GetComponent<TextMeshProUGUI>().text = EncryptedPlayerPrefs.GetInt("coinsAmmount").ToString();
    }
}
