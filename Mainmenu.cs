using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;
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
    private GameObject skinsMenuFreeCoins;
    [SerializeField]
    private GameObject skinsMenuCoinsAmmount;

    [SerializeField]
    private GameObject optionsMenuButton0;
    [SerializeField]
    private GameObject optionsMenuButton1;
    [SerializeField]
    private GameObject optionsMenuButton2;
    [SerializeField]
    private GameObject optionsMenuSlider0;
    [SerializeField]
    private GameObject optionsMenuSlider1;
    [SerializeField]
    private GameObject optionsMenuToggle;
    [SerializeField]
    private GameObject creditsMenu;
    [SerializeField]
    private GameObject BackgroundSound;

    [SerializeField]
    private GameObject coinText;

    [SerializeField]
    private GameObject highScoreText;

    AsyncOperation async;

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GoOptions()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);

        int currentRefreshRate = PlayerPrefs.GetInt("refreshRateSetting");

        optionsMenuSlider0.GetComponent<Slider>().value = PlayerPrefs.GetFloat("musicSetting");
        optionsMenuSlider1.GetComponent<Slider>().value = PlayerPrefs.GetFloat("effectsSetting");

        if(PlayerPrefs.GetInt("touchPad") == 0) optionsMenuToggle.GetComponent<Toggle>().isOn = false;
        else optionsMenuToggle.GetComponent<Toggle>().isOn = true;

    }

    public void OptionsSelectButton0()
    {
        PlayerPrefs.SetInt("refreshRateSetting", 31);

        Application.targetFrameRate = 31;
    }

    public void OptionsSelectButton1()
    {
        PlayerPrefs.SetInt("refreshRateSetting", 61);

        Application.targetFrameRate = 61;
    }

    public void OptionsSelectButton2()
    {
        PlayerPrefs.SetInt("refreshRateSetting", 121);

        Application.targetFrameRate = 121;
    }

    public void BackFromOptions()
    {
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }

    public void SaveOptions()
    {
        PlayerPrefs.SetFloat("musicSetting", optionsMenuSlider0.GetComponent<Slider>().value);
        PlayerPrefs.SetFloat("effectsSetting", optionsMenuSlider1.GetComponent<Slider>().value);

        if(optionsMenuToggle.GetComponent<Toggle>().isOn == false) PlayerPrefs.SetInt("touchPad", 0);
        else PlayerPrefs.SetInt("touchPad", 1);

        BackgroundSound.GetComponent<AudioSource>().volume = optionsMenuSlider0.GetComponent<Slider>().value;
    }

    public void BackFromCredits()
    {
        creditsMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void GoSkins()
    {
        mainMenu.SetActive(false);
        skinsMenu.SetActive(true);

        if(EncryptedPlayerPrefs.GetInt("isCharacterBought" + CharacterSelection.GetSelectedCharacter()) == 1)
        {
            skinsMenuBack.SetActive(true);
            skinsMenuBuy.SetActive(false);
            skinsMenuFreeCoins.SetActive(false);
        } else
        {
            skinsMenuBack.SetActive(false);
            skinsMenuBuy.SetActive(true);
            skinsMenuFreeCoins.SetActive(true);
            skinsMenuBuy.GetComponentInChildren<TextMeshProUGUI>().text = skinsMenu.GetComponent<CharacterSelection>().GetPrices(CharacterSelection.GetSelectedCharacter()).ToString();
        }

        skinsMenuCoinsAmmount.GetComponent<TextMeshProUGUI>().text = EncryptedPlayerPrefs.GetInt("coinsAmmount").ToString();
        skinsMenu.GetComponent<CharacterSelection>().characters[CharacterSelection.GetSelectedCharacter()].SetActive(true);
    }

    public void GoCredits()
    {
        optionsMenu.SetActive(false);
        creditsMenu.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private void Start()
    {
        Screen.SetResolution(1920, 1080, false);

        var audioClip0 = Resources.Load<AudioClip>("Sounds/coin_collect");
        var audioClip1 = Resources.Load<AudioClip>("Sounds/jump");
        var texture = Resources.Load<Texture2D>("Textures/texture01");


        EncryptedPlayerPrefs.keys = new string[5];
        EncryptedPlayerPrefs.keys[0] = "127ee06e3ea41f6598f9fbea933d25d661614c461e91504ef1f488273913e206";
        EncryptedPlayerPrefs.keys[1] = "4a8c553b7fce0e25909707b24db4ae52c0fa0dc439d666b8387e995221108424";
        EncryptedPlayerPrefs.keys[2] = "1cd5a87bc2fd7585d37deaff34ec5374f8d2fee68327819660d3ea8d2023fa9f";
        EncryptedPlayerPrefs.keys[3] = "ebf25c933e017fbcc7fca47c7cbddd409c31fc5c66789bb2184320a3c548e9a7";
        EncryptedPlayerPrefs.keys[4] = "ddc94b0a69d9e608220cc5123d7eaccce92fcdb1db1409cc441e92c56cf7c774";

        

        if (!EncryptedPlayerPrefs.HasKey("coinsAmmount"))
        {
            EncryptedPlayerPrefs.SetInt("coinsAmmount", 900000);
            EncryptedPlayerPrefs.SetInt("highScore", 0);
            EncryptedPlayerPrefs.SetInt("selectedCharacter", 0);
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
            PlayerPrefs.SetInt("refreshRateSetting", 60);
            PlayerPrefs.SetInt("touchPad", 0);
            PlayerPrefs.SetFloat("musicSetting", 0.5f);
            PlayerPrefs.SetFloat("effectsSetting", 0.5f);
        }

        coinText.GetComponent<TextMeshProUGUI>().text = EncryptedPlayerPrefs.GetInt("coinsAmmount").ToString();

        highScoreText.GetComponent<TextMeshProUGUI>().text = EncryptedPlayerPrefs.GetInt("highScore").ToString();

        Application.targetFrameRate = PlayerPrefs.GetInt("refreshRateSetting");

        BackgroundSound.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("musicSetting");
    }
}
