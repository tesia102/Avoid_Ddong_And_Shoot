using UnityEngine;

public class Bullet : RecycleObject
{
    /// <summary>
    /// 총알의 이동 속도
    /// </summary>
    public float moveSpeed = 20.0f;

    public float lifeTime = 2.0f;

    protected override void OnEnable()
    {
        base.OnEnable();
        StartCoroutine(LifeOver(lifeTime));
    }

    private void Update()
    {
        transform.Translate(Time.deltaTime * moveSpeed * Vector2.up);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ddong"))
        {
            Destroy(gameObject);
        }
    }
}
