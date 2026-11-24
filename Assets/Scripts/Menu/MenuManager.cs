using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Scene settings")]
    public string cena;

    [Header("Extra screen")]
    public GameObject controlScreen;
    public GameObject ruleScreen;
    public void PlayGame()
    {
        SceneManager.LoadScene(cena); // carrega a cena que o jogo vai rodar
    }

    public void OpenControls()
    {
        controlScreen.SetActive(true);
    }

    public void CloseControls()
    {
        controlScreen.SetActive(false);
    }

    public void OpenRules()
    {
        ruleScreen.SetActive(true);
    }

    public void CloseRules()
    {
        ruleScreen.SetActive(false);
    }
}
