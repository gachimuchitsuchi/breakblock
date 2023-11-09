using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitlePageManager : MonoBehaviour
{
    [SerializeField]
    private GameObject startButton;

    [SerializeField]
    private GameObject ruleButton;

    

    // Start is called before the first frame update
    void Start()
    {
        startButton.GetComponent<Button>().onClick.AddListener(MenuSceneManager.instance.ShowStageSelectPage);
        ruleButton.GetComponent<Button>().onClick.AddListener(MenuSceneManager.instance.ShowRulePage);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
