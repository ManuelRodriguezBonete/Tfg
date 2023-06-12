using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableScript : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("1er digito nivel, 2o y 3o número de coleccionable dentro del nivel")]
    [SerializeField] string key;
    private Inventory inventory;
    void Start()
    {
        inventory = FindAnyObjectByType<Inventory>();
        
    }
    private void OnRenderObject()
    {
        if (inventory.colectablesDict.ContainsKey(key))
        {
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inventory.colectablesDict.Add(key, true);
            inventory.numCollectables++;
            Destroy(gameObject);
        }
    }
}
