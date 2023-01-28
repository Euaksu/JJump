using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class directions : MonoBehaviour
{
    Vector2 inputVec;

    //움직임 스피드
    public float MoveSpeed = 3.0f;

    //점프 스피드 (올라갈때-내려갈때-강화될때)
    public float JumpUpSpeed = 0.4f;
    public float JumpDownSpeed = 0.5f;
    public float JumpRFSpeed = 0.7f;

    //점프로 이동하는 거리
    public float JumpNMDist = 0.5f;
    public float JumpRFDist = 2.0f;

    //점프 플래그
    bool isJumping;
    bool isJumpRF;
    int JumpStep;

    //점프 횟수 제한
    int JumpCnt = 0;

    Rigidbody2D rigid;
    Animator anim;

    //시간 지연 위한 변수
    float timer;
    public float waitingTimeAtTop = 0.4f;
    public float waitingTimeAtMiddle = 0.4f;

    //점프 과정_ 이동 타겟 설정
    Vector2 jumpStartPos;
    Vector2 targetPositionUp1;
    Vector2 targetPositionUp2;
    Vector2 targetPositionDown1;
    Vector2 targetPositionDown2;
    Vector2 targetPositionRF;

    //대각선 움직임 제어 플래그
    bool isHorizonMove;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        //타이머 초기화
        timer = 0.0f;
    }

    void Update()
    {
        //이동
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");


        //대각선 움직임 제어용 체크
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


        //애니메이션
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


        //점프
        if (Input.GetButtonDown("Jump") && !isJumping && !isJumpRF)
        {
            jumpStartPos = new Vector2(transform.position.x, transform.position.y);
            isJumping = true;
            isJumpRF = false;
            JumpStep = 1;

            //점프 횟수
            JumpCnt = 0;

            //위치 지정
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

                    //점프 강화 판별
                    if (JumpCnt >= 2)
                        isJumpRF = true;

                    JumpStep = 3;
                    timer = 0.0f;
                }
            }
            if (isJumpRF)
            {
                Debug.Log("점프 강화 이동이요~");
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
                    Debug.Log("일반 점프 이동이요~");
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
        //이동
        Vector2 nextVec = inputVec.normalized * MoveSpeed * Time.fixedDeltaTime;
        if (isHorizonMove)
            nextVec.y = 0;
        else
            nextVec.x = 0;
        rigid.MovePosition(rigid.position + nextVec);

    }


}
