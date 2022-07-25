using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private LayerMask objectSelectionMask;
    private Transform shellSpawnPosition;
    private Transform cannonAim;
    private Transform shootPoint;
    private RaycastHit2D hit;

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

        RaycastHit2D hitInfo = Physics2D.Raycast(cannonAim.position, cannonAim.up, rayDistance);
        if(hitInfo.collider != null)
        {
            Debug.DrawRay(cannonAim.position, cannonAim.up * rayDistance, Color.green);
            if(hitInfo.collider.CompareTag("Player"))
            {
                CannonShell.Create(shellSpawnPosition.position, hit.point);
                Debug.Log("Пиф! Паф!");
            }
        }
        else
        {

        }

        //var ray = new Ray(cannonAim.position, cannonAim.right);
        //Debug.DrawRay(cannonAim.position, cannonAim.up * rayDistance, Color.green);

        //if(Physics.Raycast(ray, out hit, rayDistance, objectSelectionMask) != false)
        //{
        //    CannonShell.Create(shellSpawnPosition.position);
        //}
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
