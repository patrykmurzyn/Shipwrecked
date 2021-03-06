using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterSelection : MonoBehaviour
{
    [SerializeField]
    private GameObject mainMenu;
    [SerializeField]
    private GameObject skinsMenu;

    [SerializeField]
    public GameObject[] characters;
    private static int selectedCharacter = 0;
    [SerializeField]
    private int[] prices;
    [SerializeField]
    private GameObject coinPanel;
    [SerializeField]
    private GameObject skinsMenuBack;
    [SerializeField]
    private GameObject skinsMenuBuy;
    [SerializeField]
    private GameObject skinsMenuBuyPrice;
    [SerializeField]
    private GameObject skinsMenuFreeCoins;
    [SerializeField]
    private GameObject coinText;

    public int GetPrices(int index)
    {
        return prices[index];
    }

    public static int GetSelectedCharacter()
    {
        return selectedCharacter;
    }

    public void NextCharacter()
    {
        characters[selectedCharacter].SetActive(false);
        selectedCharacter = (selectedCharacter + 1) % characters.Length;
        characters[selectedCharacter].SetActive(true);

        if (EncryptedPlayerPrefs.GetInt("isCharacterBought" + CharacterSelection.GetSelectedCharacter()) == 1)
        {
            skinsMenuBack.SetActive(true);
            skinsMenuBuy.SetActive(false);
            skinsMenuFreeCoins.SetActive(false);
        }
        else
        {
            skinsMenuBack.SetActive(false);
            skinsMenuBuy.SetActive(true);
            skinsMenuFreeCoins.SetActive(true);
            skinsMenuBuyPrice.GetComponent<TextMeshProUGUI>().text = prices[selectedCharacter].ToString();
        }
    }

    public void PreviousCharacter()
    {
        characters[selectedCharacter].SetActive(false);
        selectedCharacter--;
        if(selectedCharacter < 0)
        {
            selectedCharacter += characters.Length;
        }
        characters[selectedCharacter].SetActive(true);

        if (EncryptedPlayerPrefs.GetInt("isCharacterBought" + CharacterSelection.GetSelectedCharacter()) == 1)
        {
            skinsMenuBack.SetActive(true);
            skinsMenuBuy.SetActive(false);
            skinsMenuFreeCoins.SetActive(false);
        }
        else
        {
            skinsMenuBack.SetActive(false);
            skinsMenuBuy.SetActive(true);
            skinsMenuFreeCoins.SetActive(true);
            skinsMenuBuyPrice.GetComponent<TextMeshProUGUI>().text = prices[selectedCharacter].ToString();

        }
    }

    public void SaveSelection()
    {
        EncryptedPlayerPrefs.SetInt("selectedCharacter", selectedCharacter);
        skinsMenu.SetActive(false);
        characters[selectedCharacter].SetActive(false);
        mainMenu.SetActive(true);
    }

    public void PurchaseAttempt()
    {
        if(EncryptedPlayerPrefs.GetInt("coinsAmmount") >= prices[selectedCharacter])
        {
            int currentCoins = EncryptedPlayerPrefs.GetInt("coinsAmmount");

            EncryptedPlayerPrefs.SetInt("coinsAmmount", currentCoins  - prices[selectedCharacter]);
            EncryptedPlayerPrefs.SetInt("isCharacterBought" + selectedCharacter, 1);

            skinsMenuBack.SetActive(true);
            skinsMenuBuy.SetActive(false);
            skinsMenuFreeCoins.SetActive(false);
            coinText.GetComponent<TextMeshProUGUI>().text = (currentCoins - prices[selectedCharacter]).ToString();



        } else
        {
            coinPanel.GetComponent<Animation>().Play("PurchaseDecline");
            
        }
    }
}