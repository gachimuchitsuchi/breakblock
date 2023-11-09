using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuSceneManager : MonoBehaviour
{
    public static MenuSceneManager instance
    {
        get; private set;
    }

    private GameObject currentPage;

    [SerializeField]
    private GameObject titlePage;

    [SerializeField]
    private GameObject stageSelectPage;

    [SerializeField]
    private GameObject rulePage;

    [SerializeField]
    private GameObject homeButton;


    public enum Pages
    {
        Title,
        StageSelect,
        Rule
    }

    //breakblockシーンから移動するページを保持
    public static Pages goPage = Pages.Title;

    //public static Pages goPage;

    private void Awake()
    {
        CreateInstance();  
    }

    // Start is called before the first frame update
    void Start()
    {
        SetAllPagesActive(false);
        ShowPages(goPage);
    }

    private void CreateInstance()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void SetAllPagesActive(bool active)
    {
        titlePage.SetActive(active);
        stageSelectPage.SetActive(active);
        rulePage.SetActive(active);
    }

    public void ShowPages(Pages page)
    {
        switch (page)
        {
            case Pages.Title:
                ShowTitlePage();
                break;
            case Pages.StageSelect:
                ShowStageSelectPage();
                break;
            case Pages.Rule:
                ShowRulePage();
                break;
        }
    }


    public void ShowTitlePage()
    {
        currentPage?.SetActive(false);
        currentPage = titlePage;
        currentPage?.SetActive(true);

        homeButton.SetActive(false);
    }

    public void ShowStageSelectPage()
    {
        currentPage?.SetActive(false);
        currentPage = stageSelectPage;
        currentPage?.SetActive(true);

        homeButton.SetActive(true);
        homeButton.GetComponent<Button>().onClick.RemoveAllListeners();
        homeButton.GetComponent<Button>().onClick.AddListener(ShowTitlePage);
    }

    public void ShowRulePage()
    {
        currentPage?.SetActive(false);
        currentPage = rulePage;
        currentPage?.SetActive(true);

        homeButton.SetActive(true);
        homeButton.GetComponent<Button>().onClick.RemoveAllListeners();
        homeButton.GetComponent<Button>().onClick.AddListener(ShowTitlePage);
    }
}
