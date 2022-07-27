using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Construction : MonoBehaviour
{
    private HealthSystem healthSystem;
    private void Start()
    {
        healthSystem = GetComponent<HealthSystem>();

        healthSystem.OnDied += HealthSystem_OnDied;
    }
    private void HealthSystem_OnDied(object sender, System.EventArgs e)
    {
        Shild shild = GetComponent<Shild>();
        if(shild.tag == "Player")
        {
            PlayerControl.HasPlayerShild = false;
        }

        if (shild.tag == "Enemy")
        {
            EnemyAI.HasEnemyShild = false;
        }
        Destroy(gameObject);
    }
}
