using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostDetector : MonoBehaviour
{
    // Start is called before the first frame update
    private GhostEnemie ghost;

    void Start()
    {
        GameObject parent = transform.parent.gameObject;
        ghost = parent.transform.GetComponentInChildren<GhostEnemie>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ghost.StartCoroutine(ghost.MoveToPlayer());
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!ghost.seeking)
        {
            ghost.StartCoroutine(ghost.MoveToPlayer());
        }
    }
}
