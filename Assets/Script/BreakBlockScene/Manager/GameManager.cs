using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance
    {
        get; private set;
    }

    [NamedArrayAttribute(new string[] { "1", "2", "3", "4", "5", "6" })]
    [SerializeField]
    private Material[] floorMaterial;

    BlockMap bm;
    [SerializeField]
    private GameObject blockMap;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject floors;

    [SerializeField]
    private GameObject shotPos;

    [SerializeField]
    private GameObject clearUI;

    [SerializeField]
    private GameObject gameOverUI;

    [SerializeField]
    private GameObject pauseUI;

    [SerializeField]
    private GameObject ballPrefab;

    [SerializeField]
    private GameObject moveTitleButton;

    [SerializeField]
    private GameObject moveStageSelectButton;

    [SerializeField]
    private TextMeshProUGUI lifeText;

    private int life;

    private int ballCnt;
    
    private int blockCnt;
    
    private int stageNum;


    private void Awake()
    {
        CreateInstance();
        bm = blockMap.GetComponent<BlockMap>();
        InitGameInfo();
    }

    // Start is called before the first frame update
    void Start()
    {
        moveTitleButton.GetComponent<Button>().onClick.AddListener(MoveTitlePage);
        moveStageSelectButton.GetComponent<Button>().onClick.AddListener(MoveStageSelectPage);
        //ステージとボールを作成
        CreateBreakBlock();
    }

    // Update is called once per frame
    void Update()
    {
        if(ballCnt <= 0)
        {
            ballCnt = 0;
            Debug.Log("ww");
            CreateBall(Player.instance.GetPlayerElem());
        }

        if(blockCnt == 0)
        {
            blockCnt = -1;
            GameClear();
        }

        if(life == 0)
        {
            life = -1;
            GameOver();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            Pause();
        }
    }

    private void CreateInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void InitGameInfo()
    {
        stageNum = StageSelectPageManager.STAGE_NUM;
        life = 3;
        ballCnt = 0;
        blockCnt = 0;
    }

    private void CreateBreakBlock()
    {
        bm.CreateStage(stageNum);
        ChangeFloorMaterial(stageNum);
        CreateBall(Elements.None);

        //初期位置
        player.transform.position = new Vector3(0, 0.5f, -8);

        lifeText.text = "LIFE" + " " + life;
    }

    public void CreateBall(Elements elem)
    {
        Transform parent = shotPos.transform;

        //配置するposを指定
        Vector3 placePos = shotPos.transform.position;

        // 配置する回転角を設定
        Quaternion q = new Quaternion();
        q = Quaternion.identity;

        GameObject ball = Instantiate(ballPrefab, placePos, q, parent);
        ball.GetComponent<Ball>().ChangeBallElement(elem);
        ballCnt += 1;
    }

    private void ChangeFloorMaterial(int stage)
    {
        if(stage == 0)
        {
            return;
        }

        Transform children = floors.GetComponentInChildren<Transform>();
        //子要素がいなければ終了
        if (children.childCount == 0)
        {
            return;
        }
        foreach (Transform ob in children)
        {
            //色を変更
            ob.GetComponent<Renderer>().material = floorMaterial[stage - 1];
        }
    }

    public void DecreaseLife()
    {
        life -= 1;
        lifeText.text = "LIFE" + " " + life; 
    }

    public void DecreaseBallCnt()
    {
        ballCnt -= 1;
    }

    public void IncreaseBlockCnt()
    {
        blockCnt += 1;
    }

    public void DecreaseBlockCnt()
    {
        blockCnt -= 1;
    }

    private void Pause()
    {
        if (!pauseUI.activeSelf)
        {
            PlayerStop();
            AllBallStop();
        }
        else
        {
            PlayerStart();
            AllBallStart(); 
        }

        pauseUI.SetActive(!pauseUI.activeSelf);
        moveTitleButton.SetActive(!moveTitleButton.activeSelf);
    }

    private void GameClear()
    {
        clearUI.SetActive(true);
        moveStageSelectButton.SetActive(true);

        PlayerStop();
        AllBallDestroy();
    }

    private void GameOver()
    {
        gameOverUI.SetActive(true);
        moveStageSelectButton.SetActive(true);

        PlayerStop();
        AllBallDestroy();
    }

    private void PlayerStart()
    {
        player.GetComponent<MovePlayer>().enabled = true;
        player.GetComponent<ShotBall>().enabled = true;
        player.GetComponent<ChangeElement>().enabled = true;
    }

    private void PlayerStop()
    {
        player.GetComponent<MovePlayer>().enabled = false;
        player.GetComponent<ShotBall>().enabled = false;
        player.GetComponent<ChangeElement>().enabled = false;
    }

    public void MoveTitlePage()
    {
        MenuSceneManager.goPage = MenuSceneManager.Pages.Title;
        SceneManager.LoadScene("Menu");
    }

    public void MoveStageSelectPage()
    {
        MenuSceneManager.goPage = MenuSceneManager.Pages.StageSelect;
        SceneManager.LoadScene("Menu");
    }


    private void AllBallStop()
    {
        GameObject[] balls;
        balls = GameObject.FindGameObjectsWithTag("Ball");

        if (balls.Length == 0) return;

        for (int i = 0; i < balls.Length; i++)
        {
            balls[i].GetComponent<Ball>().StopBall();
        }
    }

    private void AllBallStart()
    {
        GameObject[] balls;
        balls = GameObject.FindGameObjectsWithTag("Ball");

        if (balls.Length == 0) return;

        for (int i = 0; i < balls.Length; i++)
        {
            balls[i].GetComponent<Ball>().StartBall();
        }
    }

    private void AllBallDestroy()
    {
        GameObject[] balls;
        balls = GameObject.FindGameObjectsWithTag("Ball");

        if (balls.Length == 0) return;

        for (int i=0; i < balls.Length; i++)
        {
            Destroy(balls[i]);
        }
    }
}
