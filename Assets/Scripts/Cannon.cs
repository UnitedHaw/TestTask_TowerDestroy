using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public static Cannon Instance { get; private set; }

    [SerializeField] private Transform aimTransform;
    [SerializeField] private float shootTimerMax;
    private float shootTimer;
    private Transform shellSpawnPosition;
    private Transform cannonAim;

    private Touch touch;
    Vector3 touchStartPosition;
    public float offset;


    private void Awake()
    {
        Instance = this;
        
        cannonAim = transform.Find("cannonAim");
        shellSpawnPosition = cannonAim.Find("shellSpawnPosition");
    }
    private void Update()
    {
        shootTimer -= Time.deltaTime;
        if (Mathf.Abs(shootTimer) > shootTimerMax)
            shootTimer = shootTimerMax;

        GetMouseInput();
        GetTouchInput();
    }

    private void GetMouseInput()
    {
        if(Input.GetMouseButton(0))
        {
            GetFolowAngleFromVector(UtilClass.GetMouseWorldPosition());
        }
        if(Input.GetMouseButtonUp(0))
        {
            HandleShooting();
        }
    }
    private void GetTouchInput()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            touchStartPosition = Camera.main.ScreenToWorldPoint(touch.position);
            touchStartPosition.z = 0f;
            switch (touch.phase)
            {
                case TouchPhase.Moved:
                    
                    GetFolowAngleFromVector(touchStartPosition);
                    break;
                case TouchPhase.Ended:
                    HandleShooting();
                    break;

            }
        }     
    }
    private void GetFolowAngleFromVector(Vector3 vectorPos)
    {
        Vector3 aimDirection = (vectorPos - cannonAim.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        angle = Mathf.Clamp(angle, 90f, 320f);
        Debug.Log(angle);
        cannonAim.eulerAngles = new Vector3(0, 0, angle);
    }
    private void HandleShooting()
    {
        if(shootTimer <= 0f)
        {
            shootTimer += shootTimerMax;
            CannonShell.Create(shellSpawnPosition.position);
        }  
    }
    public Vector3 GetShotAimPosition()
    {
        return aimTransform.position;
    }
}