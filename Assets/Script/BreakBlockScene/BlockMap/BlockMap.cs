using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlockMap : MonoBehaviour
{
    [SerializeField]
    GameObject blockPrefab;

    //blockオブジェクトに使用するマテリアル
    [NamedArrayAttribute(new string[] { "None", "Pyro", "Hydro", "Cryo", "Electro" })]
    [SerializeField]
    Material[] elementMaterial;

    StageMapInfo mapInfoClass = new StageMapInfo();
    private int mapRow = StageMapInfo.STAGE_ROW;
    private int mapCol = StageMapInfo.STAGE_COL;
    
    //元素反応Frozenに使用。　ゲーム中のブロックの盤面情報を入れる
    public static GameObject[,] nowMap = new GameObject[StageMapInfo.STAGE_ROW, StageMapInfo.STAGE_COL];

    private void Awake()
    {
        InitNowMap();
    }

    public void InitNowMap()
    {
        for (int i = 0; i < mapRow; i++)
        {
            for(int j=0; j< mapCol; j++)
            {
                nowMap[i, j] = null;
            }
        }
    }

    // ステージ作成
    public void CreateStage(int stage)
    {
        if (stage == 0)
        {
            MenuSceneManager.goPage = MenuSceneManager.Pages.Title;
            SceneManager.LoadScene("Menu");
            return;
        }

        Transform parent = this.transform;

        // 配置する座標を設定
        Vector3 placePosition = new Vector3(-9, 0.5f, 9);
        // 初期化する座標を設定
        Vector3 initPosition = placePosition;

        // 配置する回転角を設定
        Quaternion q = new Quaternion();
        q = Quaternion.identity;

        // ブロック全削除
        GameObject[] clones = GameObject.FindGameObjectsWithTag("Block");
        foreach (GameObject clone in clones)
        {
            Destroy(clone);
        }

        // 配置
        for (int i = 0; i < mapRow; i++)
        {
            placePosition.x = initPosition.x;
            for (int j = 0; j < mapCol; j++)
            {
                int item = mapInfoClass.stageMap[stage - 1, i, j];
                if (item != 5)
                {
                    // ブロックの複製
                    GameObject block = Instantiate(blockPrefab, placePosition, q, parent);
                    // クリア判定に使用するブロックの個数
                    GameManager.instance.IncreaseBlockCnt();
                    // ブロック自身のrowとcolを保持
                    block.GetComponent<Block>().row = i;
                    block.GetComponent<Block>().col = j;
                    // nowMapにblockを入れる
                    nowMap[i, j] = block;
                    // stageMapの番号により色変更
                    Renderer r = block.GetComponent<Renderer>();
                    r.material = elementMaterial[item];
                    ChangeChildrenColor(block, item);
                    // 元素の決定
                    block.GetComponent<Block>().blockElement = (Elements)item;
                    // stageMapの番号で名前を生成
                    //block.name = "Block_" + item.ToString();
                }
                //次のブロックを配置するx座標
                placePosition.x += blockPrefab.transform.localScale.x + 0.01f;
            }
            //次行のブロックを配置するz座標
            placePosition.z -= blockPrefab.transform.localScale.z + 0.01f;
        }
    }

    private void ChangeChildrenColor(GameObject obj, int item)
    {
        Transform children = obj.GetComponentInChildren<Transform>();
        //子要素がいなければ終了
        if (children.childCount == 0)
        {
            return;
        }
        foreach (Transform ob in children)
        {
            //色を変更
            ob.GetComponent<Renderer>().material = elementMaterial[item];
        }
    }
}
