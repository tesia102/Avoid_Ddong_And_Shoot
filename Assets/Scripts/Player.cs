using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;             // 같은 오브젝트에 있는 Rigidbody2D 컴포넌트를 담아둘 변수.
    public float speed = 1.0f;  // 캐릭터의 스피드를 제어할 변수.

    Animator anim;              // 같은 오브젝트에 있는 Animator 컴포넌트를 담아둘 변수.

    bool isDeath = false;       // 죽은 상태를 체크할 bool 변수.

    int avoidCount = 0;

    public int AvoidCount
    {
        get => avoidCount;
        set
        {
            if (avoidCount != value)
            {
                avoidCount = value;
                if (avoidCount > 50)
                {
                    //Debug.Log("피한 똥 갯수 30개 넘었음");
                    canShoot++;
                    //Debug.Log($"canShoot ={canShoot}");
                    avoidCount = 0;
                    //Debug.Log($"avoidCount = {avoidCount}");
                }
            }
            //Debug.Log($"canShoot = {canShoot}\n avoidCount = {avoidCount}");
        }
    }

    //public Action<int> onAvoidCountChange;     // 똥이 바닥에 닿는 횟수가 변경됨을 저장하는 델리게이트

    public int canShoot = 1;
    const int maxCanShoot = 2;
    const int minCanShoot = 0;

    /// <summary>
    /// 총알 프리팹
    /// </summary>
    public GameObject bulletPrefab;
    public GameObject firePosition;

    //Transform[] fireTransforms;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // 같은 오브젝트에 있는 Rigidbody2D 컴포넌트를 rb변수에 할당.

        anim = GetComponent<Animator>();
        // 같은 오브젝트에 있는 Animator 컴포넌트를 anim 변수에 할당.

    }

    // Update is called once per frame
    void Update()
    {
        if (canShoot > maxCanShoot)
        {
            canShoot = maxCanShoot;
            //Debug.Log($"canShoot ={canShoot}");
        }
        if (canShoot > minCanShoot && Input.GetKeyDown(KeyCode.Space))
        {
            //GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            //bullet.transform.position = firePosition.transform.position;
            Fire();
            canShoot--;
        }
        if (isDeath)
        {
            return;   // 죽음 상태라면 함수를 빠져나감
        }
        Move();
    }

    /// <summary>
    /// 총알을 발사하는 함수
    /// </summary>
    void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        bullet.transform.position = firePosition.transform.position;
    }

    private void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");   // 키 입력 A와 D 또는 방향키 <- , -> 의 입력에 따라 -1 ~ 1을 반환.
                                                    // 키 입력이 없다면 0을 반환한다.
        rb.velocity = new Vector2(x * speed, 0);    // rb.velocity를 통해 속도를 적용. 좌우로만 이동하기에
                                                    // y 축은 0이다.

        anim.SetBool("isMove", x != 0 ? true : false);  // x의 값이 0이 아니라면 true, 맞다면 false를 리턴한다.
                                                        // 이 값에 따라 Animator에 추가한 isMove의 값이 변화됨.

        if (x != 0 && x != transform.localScale.x)
            transform.localScale = new Vector3((float)(x * 0.7), (float)0.7, (float)0.7);
        // 이동 방향에 따라 이미지를 반전할 수 있도록 x축에 x변수의 값을 대입.

    }

    public void Death()
    {
        isDeath = true;             // 죽은 상태로 변경
        GameManager.i.GameOver();   // 게임 매니저의 게임 오버 함수 호출

        anim.SetTrigger("Death");   // 죽음 애니메이션 플레이
        Destroy(gameObject, 1.0f);  // 1초 뒤 오브젝트 삭제
    }
}
