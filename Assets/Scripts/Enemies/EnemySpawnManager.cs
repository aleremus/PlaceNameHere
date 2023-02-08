using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemySpawnManager : MonoBehaviour
{
    private Vector3 spawningArea;
    [SerializeField, Range(1, 10)] public int spawningInterval;

    private float nextSpawnIn = 4;

    public TMPro.TMP_Text difficultyText;
    public GameObject enemyPrefab;

    public int amountOfEnemies = 0;
    // Start is called before the first frame update
    void Start()
    {
        spawningArea = new Vector3(transform.localScale.x, 0, transform.localScale.z) / 2;
        StartCoroutine(SpawnRandomly());
        difficultyText.text = "" + GetDifficulty();
    }

    
    IEnumerator SpawnRandomly()
    {
        while (true)
        {
            yield return new WaitForSeconds(nextSpawnIn);
            nextSpawnIn = Random.Range(spawningInterval - .5f, spawningInterval + .5f);//  * FindObjectsOfType<Enemy>().Length;

            Vector2 randomPosInCircle = Random.insideUnitCircle;


            Vector3 randomPosition = transform.position + GenerateRandomPointInArea();

            if (amountOfEnemies < GetDifficulty() * GetDifficulty())
            {
                Instantiate(enemyPrefab, randomPosition, transform.rotation);
                amountOfEnemies++;
            }
        }
    }

    public Vector3 GenerateRandomPointInArea()
    {
        Vector3 point = Vector3.zero;

        Vector2 posInCircle = Random.insideUnitCircle;

        point.x = posInCircle.x * spawningArea.x;
        point.z = posInCircle.y * spawningArea.z;

        return point;
    }

    public void IncreaseDifficulty()
    {
        spawningInterval -= 1;
        if (spawningInterval <= 1)
            spawningInterval = 1;
        difficultyText.text = "" + GetDifficulty();
    }

    public void DecreaseDifficulty()
    {
        spawningInterval += 1;
        if (spawningInterval >= 11)
            spawningInterval = 10;
        difficultyText.text = "" + GetDifficulty();
    }

    private int GetDifficulty()
    {
        return 11 - spawningInterval;
    }
}
