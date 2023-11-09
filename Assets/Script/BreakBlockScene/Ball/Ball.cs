using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody rb;
    private Renderer r;

    [NamedArrayAttribute(new string[] { "None", "Pyro", "Hydro", "Cryo", "Electro" })]
    public Material[] elementMaterial;

    //���`��������̃}�e���A��
    [SerializeField]
    private Material superMaterial;

    [NamedArrayAttribute(new string[] { "None", "Pyro", "Hydro", "Cryo", "Electro" })]
    [SerializeField]
    private GameObject[] elementParticle;

    private GameObject currentParticle;

    private bool isSuperMode;

    public float ballSpeed = 2.0f;

    //Pause���Ɍ��̃X�s�[�h������ϐ�
    private Vector3 keepSpeed;

    [HideInInspector]
    public Elements ballElement;

    //���f�����@�@�@   �u���b�N
    //�@�@�@�@�@�@ ���@���@���@�X�@�d
    //          �� ���@���@���@���@��
    //�@�@�@�{�@�� ��  ���@���@�n�@��
    //      �b  �� ��  ���@���@���@��
    //�@�@�@���@�X ��  �n�@���@���@��
    //�@�@�@�@�@�d ��  �߁@���@���@��

    public ElementalReactions[,] blockElementalReaction = new ElementalReactions[5, 5]
    {
        {ElementalReactions.None, ElementalReactions.None,       ElementalReactions.None,           ElementalReactions.None,         ElementalReactions.None},
        {ElementalReactions.None, ElementalReactions.None,       ElementalReactions.Vaporize,       ElementalReactions.Melt,         ElementalReactions.Overloaded},
        {ElementalReactions.None, ElementalReactions.Vaporize,   ElementalReactions.None,           ElementalReactions.None,         ElementalReactions.None},
        {ElementalReactions.None, ElementalReactions.Melt,       ElementalReactions.Frozen,         ElementalReactions.None,         ElementalReactions.Superconduct},
        {ElementalReactions.None, ElementalReactions.Overloaded, ElementalReactions.ElectroCharged, ElementalReactions.Superconduct, ElementalReactions.None}
    };

    private void Awake()
    {
        rb = this.GetComponent<Rigidbody>();
        r = this.GetComponent<Renderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        isSuperMode = false;
        InitParticle();
    }

    // Update is called once per frame
    void Update()
    {
        if(this.gameObject.transform.position.z < -10.0f)
        {
            GameManager.instance.DecreaseBallCnt();
            GameManager.instance.DecreaseLife();
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (rb == null) return;
        //�@���x�N�g���̎擾
        Vector3 inNormal = other.transform.up;


        //�u���b�N�Ɠ��������Ƃ�
        if (other.gameObject.CompareTag("BlockPlane"))
        {
            //�e�I�u�W�F�N�g�̎擾
            GameObject parent = other.transform.parent.gameObject;

            if (parent.GetComponent<Block>().isFrozen)
            {
                ElemReacManager.instance.BreakFrozen(parent);
            }

            //���d�����[�h�̎��͔j�󏈗��̂�
            if (isSuperMode)
            {
                Destroy(parent);
                return;
            }
            else
            {
                //�{�[���ƃu���b�N�̌��f�������擾
                ElementalReactions reaction = blockElementalReaction[(int)ballElement, (int)parent.GetComponent<Block>().blockElement];
                //���f��������
                ElemReacManager.instance.ElementalReaction(this.gameObject, parent.gameObject, reaction);
                //���`������(�ђʏ���)�ȊO���˂���
                if(reaction != ElementalReactions.Superconduct)
                {
                    BallReflector(inNormal);
                }
            }
        }

        //�ǂƓ��������Ƃ�
        if (other.gameObject.CompareTag("WallPlane"))
        {
            if (isSuperMode)
            {
                isSuperMode = false;
                ChangeBallElement(Elements.Electro);
                ShowParticle();
            }
            BallReflector(inNormal);
        }


        //�v���C���[�Ɠ��������Ƃ�
        if (other.gameObject.CompareTag("PlayerPlane"))
        {
            if (isSuperMode)
            {
                isSuperMode = false;
            }

            if(Player.instance.GetPlayerElem() == Elements.Hydro && ballElement == Elements.Cryo)
            {
                GameManager.instance.CreateBall(Elements.Cryo);

                Destroy(this.gameObject);
            }

            ChangeBallElement(Player.instance.GetPlayerElem());
            ShowParticle();
            //Debug.Log(ballElement);
            BallReflector(inNormal);
        }
    }    

    public void ChangeBallElement(Elements elem)
    {
        ballElement = elem;
        r.material = elementMaterial[(int)elem];
    }

    private void InitParticle()
    {
        elementParticle[(int)Elements.None].SetActive(false);
        elementParticle[(int)Elements.Pyro].SetActive(false);
        elementParticle[(int)Elements.Hydro].SetActive(false);
        elementParticle[(int)Elements.Cryo].SetActive(false);
        elementParticle[(int)Elements.Electro].SetActive(false);
    }

    public void ShowParticle()
    {
        switch (ballElement)
        {
            case Elements.None:
                currentParticle?.SetActive(false);
                currentParticle = elementParticle[(int)Elements.None];
                currentParticle?.SetActive(true);
                break;

            case Elements.Pyro:
                currentParticle?.SetActive(false);
                currentParticle = elementParticle[(int)Elements.Pyro];
                currentParticle?.SetActive(true);
                break;

            case Elements.Hydro:
                currentParticle?.SetActive(false);
                currentParticle = elementParticle[(int)Elements.Hydro];
                currentParticle?.SetActive(true);
                break;

            case Elements.Cryo:
                currentParticle?.SetActive(false);
                currentParticle = elementParticle[(int)Elements.Cryo];
                currentParticle?.SetActive(true);
                break;

            case Elements.Electro:
                currentParticle?.SetActive(false);
                currentParticle = elementParticle[(int)Elements.Electro];
                currentParticle?.SetActive(true);
                break;
        }
    }

    public void StopBall()
    {
        keepSpeed = rb.velocity;
        rb.velocity = Vector3.zero;
    }

    public void StartBall()
    {
        rb.AddForce(keepSpeed, ForceMode.VelocityChange);
        keepSpeed = Vector3.zero;
    }

    //�{�[���𔽎˂��郁�\�b�h
    private void BallReflector(Vector3 inNormal)
    {
        //���˃x�N�g��(���x)
        Vector3 inDirection = rb.velocity;

        //�{�[���ƃu���b�N�v���[���̐ڐG�������Ĕ���łȂ�������
        if (inNormal.x != 0 && Mathf.Sign(inNormal.x) != Mathf.Sign(inDirection.x) || inNormal.z != 0 && Mathf.Sign(inNormal.z) != Mathf.Sign(inDirection.z))
        {
            //���˃x�N�g��(���x)
            Vector3 result = Vector3.Reflect(inDirection, inNormal);
            //���x�v�Z
            float horizontalSpeed = (float)Mathf.Sqrt(Mathf.Pow(rb.velocity.x, 2) + Mathf.Pow(rb.velocity.z, 2));
            if(horizontalSpeed < 4.0f)
            {
                //���˃x�N�g���𔽉f
                rb.velocity = result * 1.02f;
            }
            else
            {
                //���˃x�N�g���𔽉f
                rb.velocity = result;
            }
            
        }
    }

    public void ChangeToSuperMode()
    {
        isSuperMode = true;
        r.material = superMaterial;
    }
}
