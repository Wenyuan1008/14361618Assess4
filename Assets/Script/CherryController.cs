using UnityEngine;
using System.Collections;

public class CherryController : MonoBehaviour
{
    public GameObject cherryPrefab; 
    public float moveSpeed = 10.0f; 
    private float spawnInterval = 10.0f; 

    private void Start()
    {
        InvokeRepeating("SpawnCherry", 0f, spawnInterval);
    }

    void SpawnCherry()
    {
        Vector3 spawnPosition = Vector3.zero;
        Vector3 endPosition = Vector3.zero;


        int side = Random.Range(0, 4);

        float screenTop = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y;
        float screenBottom = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).y;
        float screenLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;
        float screenRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;

        switch (side)
        {
            case 0: 
                spawnPosition = new Vector3(Random.Range(screenLeft, screenRight), screenTop + 1, 0);
                endPosition = new Vector3(spawnPosition.x, screenBottom - 1, 0);
                break;
            case 1: 
                spawnPosition = new Vector3(Random.Range(screenLeft, screenRight), screenBottom - 1, 0);
                endPosition = new Vector3(spawnPosition.x, screenTop + 1, 0);
                break;
            case 2: 
                spawnPosition = new Vector3(screenLeft - 1, Random.Range(screenBottom, screenTop), 0);
                endPosition = new Vector3(screenRight + 1, spawnPosition.y, 0);
                break;
            case 3: 
                spawnPosition = new Vector3(screenRight + 1, Random.Range(screenBottom, screenTop), 0);
                endPosition = new Vector3(screenLeft - 1, spawnPosition.y, 0);
                break;
        }

        GameObject cherry = Instantiate(cherryPrefab, spawnPosition, Quaternion.identity);
        StartCoroutine(MoveCherry(cherry, spawnPosition, endPosition));
    }

    IEnumerator MoveCherry(GameObject cherry, Vector3 start, Vector3 end)
    {
        float journeyLength = Vector3.Distance(start, end);
        float startTime = Time.time;

        float distanceCovered = (Time.time - startTime) * moveSpeed;
        float fractionOfJourney = distanceCovered / journeyLength;

        while (fractionOfJourney < 1)
        {
            distanceCovered = (Time.time - startTime) * moveSpeed;
            fractionOfJourney = distanceCovered / journeyLength;

            cherry.transform.position = Vector3.Lerp(start, end, fractionOfJourney);

            yield return null;
        }

        Destroy(cherry);
    }
}
