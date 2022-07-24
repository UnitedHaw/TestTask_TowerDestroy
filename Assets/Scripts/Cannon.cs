using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public static Cannon Instance { get; private set; }

    [SerializeField] private Transform pfCannonShell;
    [SerializeField] private Transform aimTransform;
    private Transform shellSpawnPosition;
    private Transform cannonAim;
    private Touch touch;


    private void Awake()
    {
        Instance = this;
        cannonAim = transform.Find("cannonAim");
        shellSpawnPosition = cannonAim.Find("shellSpawnPosition");
    }
    private void Update()
    {   
        GetTouchInput();
        GetMouseInput();
    }

    private void GetMouseInput()
    {
        if(Input.GetMouseButton(0))
        {
            GetFolowAngleFromVector(UtilClass.GetMouseWorldPosition());
        } 
    }
    private void GetTouchInput()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Moved:
                    GetFolowAngleFromVector(touch.position);
                    break;
                case TouchPhase.Ended:
                    Instantiate(pfCannonShell, shellSpawnPosition.position, Quaternion.identity);
                    break;
                default:
                    break;
            }
        }     
    }

    private void GetFolowAngleFromVector(Vector3 vectorPos)
    {
        Vector3 aimDirection = (vectorPos - cannonAim.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        cannonAim.eulerAngles = new Vector3(0, 0, angle);
    }
    public Vector3 GetShotAimPosition()
    {
        return aimTransform.position;
    }
}