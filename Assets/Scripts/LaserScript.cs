using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private bool horizontal;
    [SerializeField] private bool facingRightTop;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float shotSpeed;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject baseObj;
    [SerializeField] private GameObject cargarObj;
    [SerializeField] private float delayShot;
    [SerializeField] public float bulletTimeAlive = 0;

    private Vector2 faceShot = new Vector2();
    void Awake()
    {
        CheckFacing();
        baseObj.SetActive(true);
        cargarObj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        delayShot -= Time.deltaTime;
        if (delayShot < shotSpeed/2)
        {
            baseObj.SetActive(false);
            cargarObj.SetActive(true);
        }
        if (delayShot <= 0)
        {
            baseObj.SetActive(true);
            cargarObj.SetActive(false);
            Shot();
            ResetTimer();
        }
    }
    private void Shot()
    {
        Quaternion rotation = Quaternion.identity;
        rotation.eulerAngles = new Vector3(0,0, 90*faceShot.y);
        Instantiate(bulletPrefab, this.transform.position, rotation, this.transform);
    }
    private void CheckFacing()
    {
        if (horizontal)
        {
            //Disparo a la derecha
            if (facingRightTop)
            {
                faceShot = new Vector2(1, 0);
            }
            //Disparo a la izq
            else
            {
                faceShot = new Vector2(-1, 0);
            }
        }
        else
        {
            //Disparo hacia arriba
            if (facingRightTop)
            {
                faceShot = new Vector2(0, 1);
            }
            //Disparo hacia abajo
            else
            {
                faceShot = new Vector2(0, -1);
            }
        }
    }
    public float GetBulletSpeed()
    {
        return bulletSpeed;
    }
    public Vector2 GetDir()
    {
        return faceShot;
    }
    private void ResetTimer()
    {
        delayShot = shotSpeed;
    }
}
