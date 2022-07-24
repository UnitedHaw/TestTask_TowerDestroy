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
    private Vector3 touchStartPosition;
    private float direction;

    

    private void Awake()
    {
        Instance = this;
        shellSpawnPosition = transform.Find("shellSpawnPosition");
        cannonAim = transform.Find("cannonAim");
    }
    private void Update()
    {

        if (Input.GetMouseButtonUp(0))
        {
            Instantiate(pfCannonShell, shellSpawnPosition.position, Quaternion.identity);
        }

        GetTouchInput();
    }
    private void GetTouchInput()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Moved:

                    var directionAngle = touchStartPosition - transform.position;
                    var angle = Mathf.Atan(directionAngle.y / directionAngle.x) * Mathf.Rad2Deg;
                    cannonAim.rotation = Quaternion.Euler(0, 0, angle);

                    direction = touch.position.y > touchStartPosition.y ? 1f : -1f;

                    break;
                default:
                    touchStartPosition = touch.position;
                    direction = 0;
                    break;
            }
        }     
    }
    public Vector3 GetShotAimPosition()
    {
        return aimTransform.position;
    }
}