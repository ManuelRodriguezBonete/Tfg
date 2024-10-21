using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostEnemie : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject player;
    [SerializeField] float duration;
    private float elapsed;
    public bool seeking;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        seeking= false;  
        //StartCoroutine(nameof(MoveToPlayer));
    }
    public IEnumerator MoveToPlayer() 
    {
        Vector3 startPos= transform.position;
        Vector3 endPos = player.transform.position;
        elapsed = 0;
        seeking= true;
        while (elapsed < duration) 
        {
            transform.position = Vector3.Lerp(startPos, endPos, (elapsed / duration));
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = endPos;
        seeking= false;
    }
    
}
