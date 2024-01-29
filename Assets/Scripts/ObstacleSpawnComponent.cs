using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawnComponent : MonoBehaviour
{
    public GameObject ObstaclePrefab;       // 방해 오브젝트를 담아둘 변수
    public float spawnRate = 0.0f;          // 방해 오브젝트를 생성하는 간격
    public float minRate = 1.0f;            // 방해 오브젝트 생성 간격의 제일 낮은 값    
    public float maxRate = 4.0f;            // 방해 오브젝트 생성 간격의 제일 높은 값

    public float spawnY = 0.0f;             // 방해 오브젝트 생성 y축 위치
    public float spawnMinX = 0.0f;          // 방해 오브젝트 생성 x축 위치. 제일 왼쪽 값.
    public float spawnMaxX = 0.0f;          // 방해 오브젝트 생성 x축 위치. 제일 오른쪽 값.

    private void Start()
    {
        StartCoroutine(SpawnCoroutine());
        // 반복해서 Spawn함수가 호출될 수 있도록 코루틴 함수로 작성하여 호출.
    }

    void Spawn()
    {
        float x = Random.Range(spawnMinX, spawnMaxX);       // x축 min~max 사이의 임의 값 리턴
        Instantiate(ObstaclePrefab, new Vector3(x, spawnY, 0), Quaternion.identity);
        // 해당 위치로 방해오브젝트 생성
    }

    IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            spawnRate = Random.Range(minRate, maxRate);     // 임의의 생성 딜레이를 설정
            yield return new WaitForSeconds(spawnRate);     // 딜레이 시간만큼 대기
            Spawn();
        }
    }

}
