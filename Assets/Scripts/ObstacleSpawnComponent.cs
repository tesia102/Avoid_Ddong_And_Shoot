using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawnComponent : MonoBehaviour
{
    public GameObject ObstaclePrefab;       // ���� ������Ʈ�� ��Ƶ� ����
    public float spawnRate = 0.0f;          // ���� ������Ʈ�� �����ϴ� ����
    public float minRate = 1.0f;            // ���� ������Ʈ ���� ������ ���� ���� ��    
    public float maxRate = 4.0f;            // ���� ������Ʈ ���� ������ ���� ���� ��

    public float spawnY = 0.0f;             // ���� ������Ʈ ���� y�� ��ġ
    public float spawnMinX = 0.0f;          // ���� ������Ʈ ���� x�� ��ġ. ���� ���� ��.
    public float spawnMaxX = 0.0f;          // ���� ������Ʈ ���� x�� ��ġ. ���� ������ ��.

    private void Start()
    {
        StartCoroutine(SpawnCoroutine());
        // �ݺ��ؼ� Spawn�Լ��� ȣ��� �� �ֵ��� �ڷ�ƾ �Լ��� �ۼ��Ͽ� ȣ��.
    }

    void Spawn()
    {
        float x = Random.Range(spawnMinX, spawnMaxX);       // x�� min~max ������ ���� �� ����
        Instantiate(ObstaclePrefab, new Vector3(x, spawnY, 0), Quaternion.identity);
        // �ش� ��ġ�� ���ؿ�����Ʈ ����
    }

    IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            spawnRate = Random.Range(minRate, maxRate);     // ������ ���� �����̸� ����
            yield return new WaitForSeconds(spawnRate);     // ������ �ð���ŭ ���
            Spawn();
        }
    }

}
