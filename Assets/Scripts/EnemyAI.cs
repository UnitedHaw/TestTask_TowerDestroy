using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    private Transform shellSpawnPosition;
    private Transform cannonAim;

    private float minRotationAngle = -0.9f;
    private float maxRotationAngle = 0f;
    private float rayDistance = 100f;
    private bool inUpperDir;
    private bool inDownDir;

    private void Awake()
    {
        cannonAim = transform.Find("cannonAim");
        shellSpawnPosition = cannonAim.Find("shellSpawnPosition");
        inDownDir = true;
        inUpperDir = false;
    }

    private void Update()
    {
        CannonRotationHandler();

        RaycastHit2D[] hitsInfo = Physics2D.RaycastAll(cannonAim.position, cannonAim.up, rayDistance);
        Debug.DrawRay(cannonAim.position, cannonAim.up * rayDistance, Color.green);

        for(int i = 0; i < hitsInfo.Length; i++)
        {
            if (hitsInfo[i].collider != null)
            {        
                Debug.DrawRay(cannonAim.position, cannonAim.up * rayDistance, Color.red);
                if (hitsInfo[i].collider.CompareTag("Player"))
                {
                    RandomShooting(hitsInfo[i].point);
                }
            }
        }     
    }

    private void RandomShooting(Vector3 target)
    {
        int rnd = Random.Range(0, 1000);
        int shootChance = 3;
        
        if (shootChance > rnd)
        {
            CannonShell.Create(shellSpawnPosition.position, target);
        }
    }

    private void CannonRotationHandler()
    {
        if (inDownDir == true && cannonAim.rotation.z >= minRotationAngle)
        {
            cannonAim.Rotate(Vector3.back * Time.deltaTime * rotationSpeed);
            if (cannonAim.rotation.z <= minRotationAngle)
            {
                inDownDir = false;
                inUpperDir = true;
            }
        }

        if (inUpperDir == true && cannonAim.rotation.z <= maxRotationAngle)
        {
            cannonAim.Rotate(Vector3.forward * Time.deltaTime * rotationSpeed);
            if (cannonAim.rotation.z >= maxRotationAngle)
            {
                inDownDir = true;
                inUpperDir = false;
            }
        }
    }
}
