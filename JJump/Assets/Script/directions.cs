using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class directions : MonoBehaviour
{
    Vector2 inputVec;

    //������ ���ǵ�
    public float MoveSpeed = 3.0f;

    //���� ���ǵ� (�ö󰥶�-��������-��ȭ�ɶ�)
    public float JumpUpSpeed = 0.4f;
    public float JumpDownSpeed = 0.5f;
    public float JumpRFSpeed = 0.7f;

    //������ �̵��ϴ� �Ÿ�
    public float JumpNMDist = 0.5f;
    public float JumpRFDist = 2.0f;

    //���� �÷���
    bool isJumping;
    bool isJumpRF;
    int JumpStep;

    //���� Ƚ�� ����
    int JumpCnt = 0;

    Rigidbody2D rigid;
    Animator anim;

    //�ð� ���� ���� ����
    float timer;
    public float waitingTimeAtTop = 0.4f;
    public float waitingTimeAtMiddle = 0.4f;

    //���� ����_ �̵� Ÿ�� ����
    Vector2 jumpStartPos;
    Vector2 targetPositionUp1;
    Vector2 targetPositionUp2;
    Vector2 targetPositionDown1;
    Vector2 targetPositionDown2;
    Vector2 targetPositionRF;

    //�밢�� ������ ���� �÷���
    bool isHorizonMove;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        //Ÿ�̸� �ʱ�ȭ
        timer = 0.0f;
    }

    void Update()
    {
        //�̵�
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");


        //�밢�� ������ ����� üũ
        bool hDown = Input.GetButtonDown("Horizontal");
        bool vDown = Input.GetButtonDown("Vertical");
        bool hUp = Input.GetButtonUp("Horizontal");
        bool vUp = Input.GetButtonUp("Vertical");
        if (hDown)
            isHorizonMove = true;
        else if (vDown)
            isHorizonMove = false;
        else if (vUp || hUp)
            isHorizonMove = inputVec.x != 0;


        //�ִϸ��̼�
        if (anim.GetInteger("hAxisRaw") != inputVec.x)
        {
            anim.SetBool("isChange", true);
            anim.SetInteger("hAxisRaw", (int)inputVec.x);
        }
        else if (anim.GetInteger("vAxisRaw") != inputVec.y)
        {
            anim.SetBool("isChange", true);
            anim.SetInteger("vAxisRaw", (int)inputVec.y);
        }
        else
            anim.SetBool("isChange", false);


        //����
        if (Input.GetButtonDown("Jump") && !isJumping && !isJumpRF)
        {
            jumpStartPos = new Vector2(transform.position.x, transform.position.y);
            isJumping = true;
            isJumpRF = false;
            JumpStep = 1;

            //���� Ƚ��
            JumpCnt = 0;

            //��ġ ����
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Dplayer_down_idle") ||
                anim.GetCurrentAnimatorStateInfo(0).IsName("Dplayer_down_walk"))
            {
                targetPositionUp1 = new Vector2(jumpStartPos.x, jumpStartPos.y - JumpNMDist);
                targetPositionUp2 = new Vector2(jumpStartPos.x, jumpStartPos.y - JumpNMDist * 2);
                targetPositionDown1 = new Vector2(targetPositionUp2.x, targetPositionUp2.y - JumpNMDist);
                targetPositionDown2 = new Vector2(targetPositionUp2.x, targetPositionUp2.y - JumpNMDist * 2);
                targetPositionRF = new Vector2(targetPositionUp2.x, jumpStartPos.y - JumpRFDist);
            }
            else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Dplayer_up_idle") ||
                anim.GetCurrentAnimatorStateInfo(0).IsName("Dplayer_up_walk"))
            {
                targetPositionUp1 = new Vector2(jumpStartPos.x, jumpStartPos.y + JumpNMDist);
                targetPositionUp2 = new Vector2(jumpStartPos.x, jumpStartPos.y + JumpNMDist * 2);
                targetPositionDown1 = new Vector2(targetPositionUp2.x, targetPositionUp2.y + JumpNMDist);
                targetPositionDown2 = new Vector2(targetPositionUp2.x, targetPositionUp2.y + JumpNMDist * 2);
                targetPositionRF = new Vector2(targetPositionUp2.x, jumpStartPos.y + JumpRFDist);
            }
            else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Dplayer_right_idle") ||
                anim.GetCurrentAnimatorStateInfo(0).IsName("Dplayer_right_walk"))
            {
                targetPositionUp1 = new Vector2(jumpStartPos.x + JumpNMDist, jumpStartPos.y + JumpNMDist);
                targetPositionUp2 = new Vector2(jumpStartPos.x + JumpNMDist * 2, jumpStartPos.y + JumpNMDist * 2);
                targetPositionDown1 = new Vector2(targetPositionUp2.x + JumpNMDist, targetPositionUp2.y - JumpNMDist);
                targetPositionDown2 = new Vector2(targetPositionUp2.x + JumpNMDist * 2, targetPositionUp2.y - JumpNMDist * 2);
                targetPositionRF = new Vector2(targetPositionUp2.x + JumpRFDist, jumpStartPos.y);
            }
            else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Dplayer_left_idle") ||
                anim.GetCurrentAnimatorStateInfo(0).IsName("Dplayer_left_walk"))
            {
                targetPositionUp1 = new Vector2(jumpStartPos.x - JumpNMDist, jumpStartPos.y + JumpNMDist);
                targetPositionUp2 = new Vector2(jumpStartPos.x - JumpNMDist * 2, jumpStartPos.y + JumpNMDist * 2);
                targetPositionDown1 = new Vector2(targetPositionUp2.x - JumpNMDist, targetPositionUp2.y - JumpNMDist);
                targetPositionDown2 = new Vector2(targetPositionUp2.x - JumpNMDist * 2, targetPositionUp2.y - JumpNMDist * 2);
                targetPositionRF = new Vector2(targetPositionUp2.x - JumpRFDist, jumpStartPos.y);
            
            }
                
        }

        if (isJumping)
        {
            if (JumpStep == 1)
            {
                transform.position = Vector2.MoveTowards(gameObject.transform.position, targetPositionUp1, 0.1f * JumpUpSpeed);
                timer += Time.deltaTime;
                Vector3 targetPos = new Vector3(targetPositionUp1.x, targetPositionUp1.y, 0);

                if (Input.GetButtonDown("Jump"))
                    JumpCnt++;


                if (timer > waitingTimeAtMiddle && gameObject.transform.position == targetPos)
                {
                    if (Input.GetButtonDown("Jump"))
                        JumpCnt++;

                    JumpStep = 2;
                    timer = 0.0f;
                }
            }
            if (JumpStep == 2)
            {
                transform.position = Vector2.MoveTowards(gameObject.transform.position, targetPositionUp2, 0.1f * JumpUpSpeed);
                timer += Time.deltaTime;
                Vector3 targetPos = new Vector3(targetPositionUp2.x, targetPositionUp2.y, 0);
                if (Input.GetButtonDown("Jump"))
                    JumpCnt++;

                if (timer > waitingTimeAtTop && gameObject.transform.position == targetPos)
                {
                    if (Input.GetButtonDown("Jump"))
                        JumpCnt++;

                    //���� ��ȭ �Ǻ�
                    if (JumpCnt >= 2)
                        isJumpRF = true;

                    JumpStep = 3;
                    timer = 0.0f;
                }
            }
            if (isJumpRF)
            {
                Debug.Log("���� ��ȭ �̵��̿�~");
                transform.position = Vector2.MoveTowards(gameObject.transform.position, targetPositionRF, 0.1f * JumpRFSpeed);
                Vector3 targetPos = new Vector3(targetPositionRF.x, targetPositionRF.y, 0);
                if (gameObject.transform.position == targetPos)
                {
                    isJumping = isJumpRF = false;
                }

            }
            else
            {
                if (JumpStep == 3)
                {
                    Debug.Log("�Ϲ� ���� �̵��̿�~");
                    transform.position = Vector2.MoveTowards(gameObject.transform.position, targetPositionDown1, 0.1f * JumpDownSpeed);
                    timer += Time.deltaTime;
                    Vector3 targetPos = new Vector3(targetPositionDown1.x, targetPositionDown1.y, 0);
                    if (timer > waitingTimeAtMiddle && gameObject.transform.position == targetPos)
                    {
                        JumpStep = 4;
                        timer = 0.0f;
                    }
                }
                if (JumpStep == 4)
                {
                    transform.position = Vector2.MoveTowards(gameObject.transform.position, targetPositionDown2, 0.1f * JumpDownSpeed);
                    Vector3 targetPos = new Vector3(targetPositionDown2.x, targetPositionDown2.y, 0);
                    if (gameObject.transform.position == targetPos)
                    {
                        isJumping = false;
                    }
                }
            }
        }

    }

    private void FixedUpdate()
    {
        //�̵�
        Vector2 nextVec = inputVec.normalized * MoveSpeed * Time.fixedDeltaTime;
        if (isHorizonMove)
            nextVec.y = 0;
        else
            nextVec.x = 0;
        rigid.MovePosition(rigid.position + nextVec);

    }


}
