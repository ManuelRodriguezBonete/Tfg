using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissapearingPlatformScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float initialTimer = 2f;
    [SerializeField] private float timer;
    [SerializeField] private List<GameObject>  sprites;
    private float reAppearTimer;
    private bool reAppearBool = false;

    void Start()
    {
        ResetTimer();
    }

    // Update is called once per frame
    private void Update()
    {
        if (reAppearBool)
        {
            reAppearTimer -= Time.deltaTime;
            if (reAppearTimer <= 0)
            {
                for (int i = 0; i < sprites.Count; i++)
                {
                    sprites[i].SetActive(true);
                }
                reAppearBool = false;
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                Debug.Log("Me desaparesco");
                for (int i = 0; i < sprites.Count; i++)
                {
                    sprites[i].SetActive(false);
                }
                reAppearBool = true;
                ResetTimer();
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        ResetTimer();
    }

    private void ResetTimer()
    {
        timer = initialTimer;
        reAppearTimer = initialTimer;
    }
   
    
}
