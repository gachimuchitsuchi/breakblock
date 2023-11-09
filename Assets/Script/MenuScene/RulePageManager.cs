using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RulePageManager : MonoBehaviour
{
    private GameObject currentPage;

    [SerializeField]
    private GameObject page1;

    [SerializeField]
    private GameObject page2;

    [SerializeField]
    private GameObject page3;

    [SerializeField]
    private GameObject page4;

    [SerializeField]
    private GameObject page5;

    [SerializeField]
    private TextMeshProUGUI pagetitleText;

    [SerializeField]
    private GameObject nextButton;

    [SerializeField]
    private GameObject backButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        SetAllPagesActive(false);
        ShowPage1();
    }

    private void SetAllPagesActive(bool active)
    {
        page1.SetActive(active);
        page2.SetActive(active);
        page3.SetActive(active);
        page4.SetActive(active);
        page5.SetActive(active);
    }

    public void ShowPage1()
    {
        currentPage?.SetActive(false);
        currentPage = page1;
        currentPage?.SetActive(true);

        pagetitleText.text = "ÉLÅ[ê‡ñæ";

        nextButton.SetActive(true);
        nextButton.GetComponent<Button>().onClick.RemoveAllListeners();
        nextButton.GetComponent<Button>().onClick.AddListener(ShowPage2);

        backButton.SetActive(false);
    }

    public void ShowPage2()
    {
        currentPage?.SetActive(false);
        currentPage = page2;
        currentPage?.SetActive(true);

        pagetitleText.text = "å≥ëf";

        nextButton.SetActive(true);
        nextButton.GetComponent<Button>().onClick.RemoveAllListeners();
        nextButton.GetComponent<Button>().onClick.AddListener(ShowPage3);

        backButton.SetActive(true);
        backButton.GetComponent<Button>().onClick.RemoveAllListeners();
        backButton.GetComponent<Button>().onClick.AddListener(ShowPage1);
    }

    public void ShowPage3()
    {
        currentPage?.SetActive(false);
        currentPage = page3;
        currentPage?.SetActive(true);

        pagetitleText.text = "å≥ëfîΩâû";

        nextButton.SetActive(true);
        nextButton.GetComponent<Button>().onClick.RemoveAllListeners();
        nextButton.GetComponent<Button>().onClick.AddListener(ShowPage4);

        backButton.SetActive(true);
        backButton.GetComponent<Button>().onClick.RemoveAllListeners();
        backButton.GetComponent<Button>().onClick.AddListener(ShowPage2);
    }

    public void ShowPage4()
    {
        currentPage?.SetActive(false);
        currentPage = page4;
        currentPage?.SetActive(true);

        pagetitleText.text = "å≥ëfîΩâû";

        nextButton.SetActive(true);
        nextButton.GetComponent<Button>().onClick.RemoveAllListeners();
        nextButton.GetComponent<Button>().onClick.AddListener(ShowPage5);

        backButton.SetActive(true);
        backButton.GetComponent<Button>().onClick.RemoveAllListeners();
        backButton.GetComponent<Button>().onClick.AddListener(ShowPage3);
    }

    public void ShowPage5()
    {
        currentPage?.SetActive(false);
        currentPage = page5;
        currentPage?.SetActive(true);

        pagetitleText.text = "";

        nextButton.SetActive(false);

        backButton.SetActive(true);
        backButton.GetComponent<Button>().onClick.RemoveAllListeners();
        backButton.GetComponent<Button>().onClick.AddListener(ShowPage4);
    }
}
