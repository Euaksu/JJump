using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    Vector2 inputVec;
    public float speed = 3.0f;
    public Vector2 targetPosition1;
    public Vector2 targetPosition2;
    public float FirstJumpSpeed = 0.4f;
    public float SecondJumpSpeed = 0.5f;
    bool isJumping;

    Rigidbody2D rigid;
    Animator anim;

    //�ð� ���� ���� ����
    float timer;
    public float waitingTime = 0.15f;

    //�밢�� ������ �����ϱ�
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
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
            anim.SetBool("IsWalking", true);

        else
            anim.SetBool("IsWalking", false);


        //����
        if (Input.GetButtonDown("Jump"))
        {
            isJumping = true;
            targetPosition1 = new Vector2(transform.position.x + 0.5f, transform.position.y + 0.5f);
            targetPosition2 = new Vector2(transform.position.x + 1.0f, transform.position.y);
        }

        if (isJumping)
        {
            transform.position = Vector2.MoveTowards(gameObject.transform.position, targetPosition1, 0.1f * FirstJumpSpeed);
            timer += Time.deltaTime;
            if (timer > waitingTime)
            {
                transform.position = Vector2.MoveTowards(gameObject.transform.position, targetPosition2, 0.1f * SecondJumpSpeed);
                Vector3 Pos2 = new Vector3(targetPosition2.x, targetPosition2.y, 0);
                if (gameObject.transform.position == Pos2)
                {
                    timer = 0.0f;
                    isJumping = false;

                }

            }
        }

    }

    private void FixedUpdate()
    {
        //�̵�
        Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime;
        if (isHorizonMove)
            nextVec.y = 0;
        else
            nextVec.x = 0;
        rigid.MovePosition(rigid.position + nextVec);

    }


}
