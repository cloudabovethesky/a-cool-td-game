using System.Collections;
using UnityEngine;
using TMPro;

public class EnemySpawner : MonoBehaviour
{
    public bool waveEnd = false;
    public WaveData[] waves;

    public int currentWaveIndex;
    public int currentGroundIndex;

    public TMP_Text waveCountdownText;
    public float waveCountdown = 5f;

    public MainGameController mainGameController;

    public Transform[] waypoints;
    // Start is called before the first frame update
    void Start()
    {
        currentWaveIndex = 0;
        currentGroundIndex = 0;
        StartCoroutine(WaveCoroutine());
    }


    void Update()
    {
        if (waveCountdown > 0f)
        {
            waveCountdown -= Time.deltaTime;
            waveCountdownText.text = Mathf.RoundToInt(waveCountdown).ToString();
        }
        else if (!waveEnd)
        {
            waveCountdownText.text = "Wave " + (currentWaveIndex + 1).ToString();
        }
        else
        {
            waveCountdownText.text = "";
        }

        if (currentWaveIndex == waves.Length)
        {
            mainGameController.WinLevel();
            this.enabled = false;
        }
    }
    private IEnumerator WaveCoroutine()
    {

        while (currentWaveIndex < waves.Length)
        {

            WaveData wavetemp = waves[currentWaveIndex];

            waveCountdown = wavetemp.delayBeforeWave;

            yield return new WaitForSeconds(wavetemp.delayBeforeWave);

            while (currentGroundIndex < wavetemp.groups.Length)
            {
                EnemyGroup grouptemp = wavetemp.groups[currentGroundIndex];

                for (int i = 0; i < grouptemp.enemyCount; i++)
                {
                    if (MainGameController.instance.endGame)
                    {
                        yield break;
                    }

                    GameObject go = Instantiate(grouptemp.enemyPrefab, Vector3.zero, Quaternion.identity);
                    EnemyController enemy = go.GetComponent<EnemyController>();
                    if (enemy == null)
                    {
                        Destroy(go);
                    }
                    else
                    {
                        enemy.Setup(waypoints);
                    }
                    yield return new WaitForSeconds(grouptemp.enemyDelay);
                }
                currentGroundIndex++;
                yield return new WaitForSeconds(grouptemp.nextGroupDelay);
            }
            currentGroundIndex = 0;
            currentWaveIndex++;
        }
        waveEnd = true;
    }

}

[System.Serializable]
public class EnemyGroup
{
    public GameObject enemyPrefab;
    public int enemyCount;
    public float enemyDelay;
    public float nextGroupDelay;
}

[System.Serializable]
public class WaveData
{
    public EnemyGroup[] groups;
    public float delayBeforeWave;
}
