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

    private void Start()
    {
        
    }
    private void Update()
    {
        Vector3 shotDir = (shellTargetPosition - transform.position).normalized;

        if (transform.position != shotDir)
            transform.position += shellSpeed * Time.deltaTime * shotDir;
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
        Transform enemyTowerTransform = collision.GetComponent<Transform>();

        if(enemyTowerTransform != null)
        {
            HealthSystem healthSystem = enemyTowerTransform.GetComponent<HealthSystem>();
            healthSystem.Damage(10);
            Destroy(gameObject);
        }
            
    }

}
