using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonShell : MonoBehaviour
{
    public static CannonShell Create(Vector3 position, Vector3 shellTarget)
    {
        Transform pfCannonShell = Resources.Load<Transform>("pfCannonShell");
        Transform shellTransform = Instantiate(pfCannonShell, position, Quaternion.identity);
        CannonShell cannonShell = shellTransform.GetComponent<CannonShell>();

        cannonShell.SetTarget(shellTarget);
        return cannonShell;
    }

    [SerializeField] private float shellSpeed = 10f;
    private Vector3 moveDir; 
    private Vector3 shellTargetPosition;
    private float timeToDestroy = 5f;

    private void Update()
    {
        if(shellTargetPosition == null)
        {
            shellTargetPosition = moveDir;
        }

        Vector3 shotDir = (shellTargetPosition - transform.position).normalized;

        if (transform.position != shotDir)
            transform.position += Time.deltaTime * shotDir * shellSpeed;
        else
            transform.position = moveDir;

        timeToDestroy -= Time.deltaTime;

        if (timeToDestroy < 0)
            Destroy(gameObject);
    }
    private void SetTarget(Vector3 shellTargetPosition)
    {
        this.shellTargetPosition = shellTargetPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Transform enemyTargetTransform = collision.GetComponent<Transform>();

        if(enemyTargetTransform != null)
        {
            if(enemyTargetTransform.CompareTag("Player") || enemyTargetTransform.CompareTag("Enemy"))
            {
                HealthSystem healthSystem = enemyTargetTransform.GetComponent<HealthSystem>();
                Debug.Log(healthSystem.gameObject.name);
                healthSystem.Damage(10);
                Destroy(gameObject);
            }    
        }
            
    }

}
