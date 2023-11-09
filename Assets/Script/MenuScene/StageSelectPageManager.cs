using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageSelectPageManager : MonoBehaviour
{
    public static int STAGE_NUM;

    
    [NamedArrayAttribute(new string[] { "Stage1", "Stage2", "Stage3", "Stage4", "Stage5", "Stage6" })]
    [SerializeField]
    private Button[] stageButton;
    


    // Start is called before the first frame update
    void Start()
    {
        for(int i=0; i<stageButton.Length; i++)
        {
            int stage = i + 1;  // ˆê“xƒ[ƒJƒ‹•Ï”‚É‘ã“ü‚·‚é
            stageButton[i].onClick.AddListener(() => OnClickButton(stage));
        }
    }


    public void OnClickButton(int stage)
    {
        STAGE_NUM = stage;
        SceneManager.LoadScene("BreakBlock");
    }
    
}
