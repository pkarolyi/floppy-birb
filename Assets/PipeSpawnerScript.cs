using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawnerScript : MonoBehaviour
{
    public GameObject pipes;
    public float spawnInterval = 5f;
    public float maxOffset = 3f;
    public float initialGap = 1.2f;
    public float gapGradient = 0.1f;
    public float minimumGap = 0.6f;
    private float timer;

    private float currentMinGap;

    private void spawnPipe()
    {
        float positionOffset = Random.Range(-maxOffset, maxOffset);
        Vector3 middleLeft = Camera.main.ScreenToWorldPoint(
            new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight / 2, Camera.main.nearClipPlane)
            );
        Vector3 pipesPosition = middleLeft + Vector3.right + Vector3.up * positionOffset;

        GameObject pipesInstance = Instantiate(pipes, pipesPosition, transform.rotation);

        float gap = Random.Range(currentMinGap, initialGap);

        Transform topPipeTransform = pipesInstance.transform.Find("TopPipe").transform;
        topPipeTransform.position = new Vector3(
            topPipeTransform.position.x, topPipeTransform.position.y + gap / 2f, topPipeTransform.position.z
            );

        Transform bottomPipeTransform = pipesInstance.transform.Find("BottomPipe").transform;
        bottomPipeTransform.position = new Vector3(
            bottomPipeTransform.position.x, bottomPipeTransform.position.y - gap / 2f, bottomPipeTransform.position.z
            );

        pipesInstance.transform.Find("MiddleSection").GetComponent<BoxCollider2D>().size = new Vector2(0.05f, gap - 0.2f);
    }

    void Start()
    {
        timer = spawnInterval;
        currentMinGap = initialGap;
    }

    void Update()
    {
        if (minimumGap < currentMinGap)
        {
            currentMinGap -= gapGradient * Time.deltaTime;
        }

        if (timer < spawnInterval)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0;
            spawnPipe();
        }
    }
}
