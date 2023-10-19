using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawnerScript : MonoBehaviour
{
    public GameObject cloud1;
    public GameObject cloud2;
    public float spawnInterval = 1;
    public float maxOffset = 3;
    private float timer = 0;

    private void spawnCloud()
    {
        GameObject cloud = Random.Range(0, 2) == 0 ? cloud1 : cloud2;

        float positionOffset = Random.Range(-maxOffset, maxOffset);
        Vector3 middleLeft = Camera.main.ScreenToWorldPoint(
            new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight / 2, Camera.main.nearClipPlane)
            );
        Vector3 cloudPosition = middleLeft + Vector3.right + Vector3.up * positionOffset;
        GameObject cloudObject = Instantiate(cloud, cloudPosition, transform.rotation);

        float size = Random.Range(0.8f, 1.6f);
        cloudObject.transform.localScale = new Vector3(size, size, size);

        SpriteRenderer spriteRenderer = cloudObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = Random.Range(0, 3) == 0 ? 3 : 1;

        float moveSpeed = Random.Range(0.9f, 1.2f);
        PipesScript pipesScript = cloudObject.GetComponent<PipesScript>();
        pipesScript.moveSpeed = moveSpeed;
    }

    void Update()
    {
        if (timer < spawnInterval)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0;
            spawnCloud();
        }
    }
}
