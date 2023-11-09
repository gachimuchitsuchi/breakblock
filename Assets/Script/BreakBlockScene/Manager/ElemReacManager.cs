using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElemReacManager : MonoBehaviour
{
    public static ElemReacManager instance
    {
        get;
        private set;
    }

    [SerializeField]
    private Material frozenMaterial;

    [SerializeField]
    private GameObject overLoadedParticlePrefab;
    
    [SerializeField]
    private GameObject electroChargedParticlePrefab;

    [SerializeField]
    private GameObject meltParticlePrefab;

    [SerializeField]
    private GameObject superconductParticlePrefab;

    [SerializeField]
    private GameObject vaporizeParticlePrefab;


    private void Awake()
    {
        CreateInstance();
    }

    private void CreateInstance()
    {
        if(instance == null)
        {
            instance = this;
        }
    }


    /*ElementalReactions
    {
        None,
        //過負荷
        Overloaded,
        //感電
        ElectroCharged,
        //凍結
        Frozen,
        //溶解
        Melt,
        //超伝導
        Superconduct,
        //蒸発
        Vaporize
    }*/

    public void ElementalReaction(GameObject ball, GameObject block, ElementalReactions elemReac)
    {
        switch (elemReac)
        {
            case ElementalReactions.None:
                break;
            case ElementalReactions.Overloaded:
                Overloaded(block);
                break;
            case ElementalReactions.ElectroCharged:
                ElectroCharged(block);
                break;
            case ElementalReactions.Frozen:
                Frozen(block);
                break;
            case ElementalReactions.Melt:
                Melt(block);
                break;
            case ElementalReactions.Superconduct:
                Superconduct(ball, block);
                break;
            case ElementalReactions.Vaporize:
                Vaporize(block);
                break;
            default:
                Debug.Log("エラー");
                break;
        }
    }

    //過負荷反応
    public void Overloaded(GameObject block)
    {
        Collider[] hits = Physics.OverlapSphere(block.transform.position, 1.5f);

        //Debug.Log($"検出されたコライダーの数: {hits.Length}");

        foreach (var hit in hits)
        {
            //Debug.Log($"検出されたオブジェクト {hit.name}");
            if (hit.tag == "Block")
            {
                GameObject explosion = Instantiate(overLoadedParticlePrefab, hit.gameObject.transform.position, Quaternion.identity);
                explosion.GetComponent<ParticleSystem>().Play();
                Destroy(explosion, 1f);
                Destroy(hit.gameObject);
            }
        }
    }


    //感電反応
    public void ElectroCharged(GameObject block)
    {
        Collider[] hits = Physics.OverlapSphere(block.transform.position, 2.0f);

        //Debug.Log($"検出されたコライダーの数: {hits.Length}");

        foreach (var hit in hits)
        {
            //Debug.Log($"検出されたオブジェクト {hit.name}");
            if(hit.tag == "Block")
            {
                if (hit.GetComponent<Block>().blockElement == Elements.Hydro)
                {
                    GameObject thunder = Instantiate(electroChargedParticlePrefab, hit.gameObject.transform.position, Quaternion.identity);
                    thunder.GetComponent<ParticleSystem>().Play();
                    Destroy(thunder, 1f);
                    //1～2の間でダメージ数を取得
                    int rnd = Random.Range(1, 3);
                    hit.GetComponent<Block>().BlockToDamage(rnd);

                }
            }
        }
    }


    //凍結反応
    public void Frozen(GameObject block)
    {
        Block b = block.GetComponent<Block>();
        //2次元配列nowMapの配置位置を取得
        int x = b.row;
        int y = b.col;

        block.GetComponent<Renderer>().material = frozenMaterial;
        //ブロックの子のマテリアルをfrozenに変更
        ChangeChildrenColor(block);

        b.isFrozen = true;
        b.blockElement = Elements.None;

        //深さ優先探索
        for (int dx = -1; dx <= 1; dx++)
        {
            for (int dy = -1; dy <= 1; dy++)
            {
                //x方向にdx,y方向にdy移動した場所を(nx,ny)とする
                int nx = x + dx;
                int ny = y + dy;

                //nx,nyがブロックマップの範囲内かどうか
                if (0 <= nx && nx < StageMapInfo.STAGE_ROW && 0 <= ny && ny < StageMapInfo.STAGE_COL)
                {
                    //nowMap[x][y]がnullでなく水ブロックかどうか
                    if (BlockMap.nowMap[nx, ny] != null && BlockMap.nowMap[nx, ny].GetComponent<Block>().blockElement == Elements.Hydro)
                    {
                        Frozen(BlockMap.nowMap[nx, ny]);
                    }
                }
            }
        }
    }

    public void BreakFrozen(GameObject block)
    {
        Block b = block.GetComponent<Block>();
        int x = b.row;
        int y = b.col;

        BlockMap.nowMap[x, y] = null;
        Destroy(block);

        for (int dx = -1; dx <= 1; dx++)
        {
            for (int dy = -1; dy <= 1; dy++)
            {
                //x方向にdx,y方向にdy移動した場所を(nx,ny)とする
                int nx = x + dx;
                int ny = y + dy;

                //nx,nyがブロックマップの範囲内かどうかとnowMap[x][y]が凍結ブロックかどうかを判定
                if (0 <= nx && nx < StageMapInfo.STAGE_ROW && 0 <= ny && ny < StageMapInfo.STAGE_COL)
                {
                    if (BlockMap.nowMap[nx, ny] != null && BlockMap.nowMap[nx, ny].GetComponent<Block>().isFrozen == true)
                    {
                        BreakFrozen(BlockMap.nowMap[nx, ny]);
                    }
                }
            }
        }
    }



    //溶解反応
    public void Melt(GameObject block)
    {
        GameObject steam = Instantiate(meltParticlePrefab, block.transform.position, Quaternion.identity);
        steam.GetComponent<ParticleSystem>().Play();
        Destroy(steam, 2f);

        block.GetComponent<Block>().BlockToDamage(2);
    }


    //超電導反応
    public void Superconduct(GameObject ball, GameObject block)
    {
        GameObject explosion = Instantiate(superconductParticlePrefab, block.transform.position, Quaternion.identity);
        explosion.GetComponent<ParticleSystem>().Play();
        Destroy(explosion, 1f);

        Destroy(block);
        //ボールを超伝導(貫通)モードに変更
        ball.GetComponent<Ball>().ChangeToSuperMode();
    }

    //蒸発反応
    public void Vaporize(GameObject block)
    {
        GameObject splash = Instantiate(vaporizeParticlePrefab, block.transform.position, Quaternion.identity);
        splash.GetComponent<ParticleSystem>().Play();
        Destroy(splash, 2f);

        block.GetComponent<Block>().BlockToDamage(2);
    }

    private void ChangeChildrenColor(GameObject obj)
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
            ob.GetComponent<Renderer>().material = frozenMaterial;
        }
    }
}
