using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    private static GameAssets instance;
    public static GameAssets Instance {   
        get { 
                if(instance == null)
            {
                instance = Resources.Load<GameAssets>("GameAssets");
            }
            return instance;
        }
        private set { }
    }

    public Transform pfCannonShell;
    public Transform pfEnemyShild;
    public Transform pfPlayerShild;
    public Transform pfDestroyedParticles;
    public Transform pfTowerDestroyedParticles;

}
