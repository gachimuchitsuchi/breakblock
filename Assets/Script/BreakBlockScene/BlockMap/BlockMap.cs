using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlockMap : MonoBehaviour
{
    [SerializeField]
    GameObject blockPrefab;

    //block�I�u�W�F�N�g�Ɏg�p����}�e���A��
    [NamedArrayAttribute(new string[] { "None", "Pyro", "Hydro", "Cryo", "Electro" })]
    [SerializeField]
    Material[] elementMaterial;

    StageMapInfo mapInfoClass = new StageMapInfo();
    private int mapRow = StageMapInfo.STAGE_ROW;
    private int mapCol = StageMapInfo.STAGE_COL;
    
    //���f����Frozen�Ɏg�p�B�@�Q�[�����̃u���b�N�̔Ֆʏ�������
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

    // �X�e�[�W�쐬
    public void CreateStage(int stage)
    {
        if (stage == 0)
        {
            MenuSceneManager.goPage = MenuSceneManager.Pages.Title;
            SceneManager.LoadScene("Menu");
            return;
        }

        Transform parent = this.transform;

        // �z�u������W��ݒ�
        Vector3 placePosition = new Vector3(-9, 0.5f, 9);
        // ������������W��ݒ�
        Vector3 initPosition = placePosition;

        // �z�u�����]�p��ݒ�
        Quaternion q = new Quaternion();
        q = Quaternion.identity;

        // �u���b�N�S�폜
        GameObject[] clones = GameObject.FindGameObjectsWithTag("Block");
        foreach (GameObject clone in clones)
        {
            Destroy(clone);
        }

        // �z�u
        for (int i = 0; i < mapRow; i++)
        {
            placePosition.x = initPosition.x;
            for (int j = 0; j < mapCol; j++)
            {
                int item = mapInfoClass.stageMap[stage - 1, i, j];
                if (item != 5)
                {
                    // �u���b�N�̕���
                    GameObject block = Instantiate(blockPrefab, placePosition, q, parent);
                    // �N���A����Ɏg�p����u���b�N�̌�
                    GameManager.instance.IncreaseBlockCnt();
                    // �u���b�N���g��row��col��ێ�
                    block.GetComponent<Block>().row = i;
                    block.GetComponent<Block>().col = j;
                    // nowMap��block������
                    nowMap[i, j] = block;
                    // stageMap�̔ԍ��ɂ��F�ύX
                    Renderer r = block.GetComponent<Renderer>();
                    r.material = elementMaterial[item];
                    ChangeChildrenColor(block, item);
                    // ���f�̌���
                    block.GetComponent<Block>().blockElement = (Elements)item;
                    // stageMap�̔ԍ��Ŗ��O�𐶐�
                    //block.name = "Block_" + item.ToString();
                }
                //���̃u���b�N��z�u����x���W
                placePosition.x += blockPrefab.transform.localScale.x + 0.01f;
            }
            //���s�̃u���b�N��z�u����z���W
            placePosition.z -= blockPrefab.transform.localScale.z + 0.01f;
        }
    }

    private void ChangeChildrenColor(GameObject obj, int item)
    {
        Transform children = obj.GetComponentInChildren<Transform>();
        //�q�v�f�����Ȃ���ΏI��
        if (children.childCount == 0)
        {
            return;
        }
        foreach (Transform ob in children)
        {
            //�F��ύX
            ob.GetComponent<Renderer>().material = elementMaterial[item];
        }
    }
}
