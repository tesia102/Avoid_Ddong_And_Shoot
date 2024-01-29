using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;             // ���� ������Ʈ�� �ִ� Rigidbody2D ������Ʈ�� ��Ƶ� ����.
    public float speed = 1.0f;  // ĳ������ ���ǵ带 ������ ����.

    Animator anim;              // ���� ������Ʈ�� �ִ� Animator ������Ʈ�� ��Ƶ� ����.

    bool isDeath = false;       // ���� ���¸� üũ�� bool ����.

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
                    //Debug.Log("���� �� ���� 30�� �Ѿ���");
                    canShoot++;
                    //Debug.Log($"canShoot ={canShoot}");
                    avoidCount = 0;
                    //Debug.Log($"avoidCount = {avoidCount}");
                }
            }
            //Debug.Log($"canShoot = {canShoot}\n avoidCount = {avoidCount}");
        }
    }

    //public Action<int> onAvoidCountChange;     // ���� �ٴڿ� ��� Ƚ���� ������� �����ϴ� ��������Ʈ

    public int canShoot = 1;
    const int maxCanShoot = 2;
    const int minCanShoot = 0;

    /// <summary>
    /// �Ѿ� ������
    /// </summary>
    public GameObject bulletPrefab;
    public GameObject firePosition;

    //Transform[] fireTransforms;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // ���� ������Ʈ�� �ִ� Rigidbody2D ������Ʈ�� rb������ �Ҵ�.

        anim = GetComponent<Animator>();
        // ���� ������Ʈ�� �ִ� Animator ������Ʈ�� anim ������ �Ҵ�.

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
            return;   // ���� ���¶�� �Լ��� ��������
        }
        Move();
    }

    /// <summary>
    /// �Ѿ��� �߻��ϴ� �Լ�
    /// </summary>
    void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        bullet.transform.position = firePosition.transform.position;
    }

    private void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");   // Ű �Է� A�� D �Ǵ� ����Ű <- , -> �� �Է¿� ���� -1 ~ 1�� ��ȯ.
                                                    // Ű �Է��� ���ٸ� 0�� ��ȯ�Ѵ�.
        rb.velocity = new Vector2(x * speed, 0);    // rb.velocity�� ���� �ӵ��� ����. �¿�θ� �̵��ϱ⿡
                                                    // y ���� 0�̴�.

        anim.SetBool("isMove", x != 0 ? true : false);  // x�� ���� 0�� �ƴ϶�� true, �´ٸ� false�� �����Ѵ�.
                                                        // �� ���� ���� Animator�� �߰��� isMove�� ���� ��ȭ��.

        if (x != 0 && x != transform.localScale.x)
            transform.localScale = new Vector3((float)(x * 0.7), (float)0.7, (float)0.7);
        // �̵� ���⿡ ���� �̹����� ������ �� �ֵ��� x�࿡ x������ ���� ����.

    }

    public void Death()
    {
        isDeath = true;             // ���� ���·� ����
        GameManager.i.GameOver();   // ���� �Ŵ����� ���� ���� �Լ� ȣ��

        anim.SetTrigger("Death");   // ���� �ִϸ��̼� �÷���
        Destroy(gameObject, 1.0f);  // 1�� �� ������Ʈ ����
    }
}
