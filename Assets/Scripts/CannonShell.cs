using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonShell : MonoBehaviour
{
    [SerializeField] private float shellSpeed = 10f;  
    private Vector3 moveDir; 
    private Vector3 aimDir;
    private float timeToDestroy = 5f;

    private void Start()
    {
        aimDir = Cannon.Instance.GetShotAimPosition();
    }
    private void Update()
    {
        Vector3 shotDir = (aimDir - transform.position).normalized;

        if (transform.position != shotDir)
            transform.position += shellSpeed * Time.deltaTime * shotDir;
        else
            transform.position = moveDir;

        timeToDestroy -= Time.deltaTime;

        if (timeToDestroy < 0)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Transform targetTransform = collision.GetComponent<Transform>();

        if(targetTransform != null) 
            Destroy(gameObject);
    }

}
