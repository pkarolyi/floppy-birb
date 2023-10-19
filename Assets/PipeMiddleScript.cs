using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeMiddleScript : MonoBehaviour
{
    public LogicManagerScript logicManagerScript;

    void Start()
    {
        logicManagerScript = GameObject.FindGameObjectWithTag("logic").GetComponent<LogicManagerScript>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        logicManagerScript.increaseScore();
    }
}
