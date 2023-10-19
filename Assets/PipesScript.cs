using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipesScript : MonoBehaviour
{
    public float moveSpeed = 1;

    void Update()
    {
        transform.position = transform.position + Vector3.left * moveSpeed * Time.deltaTime;

        Vector3 middleLeft = Camera.main.ScreenToWorldPoint(
            new Vector3(0, Camera.main.pixelHeight / 2, Camera.main.nearClipPlane)
            );
        if (transform.position.x < middleLeft.x - 1)
        {
            Destroy(this.gameObject);
        }
    }
}
