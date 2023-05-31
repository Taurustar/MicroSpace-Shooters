using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentPlayerConfiguration : MonoBehaviour
{
    public static PersistentPlayerConfiguration Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    
    public int armorIndex;
    public int playerCredits;

    public List<GameObject> playerWeapons;
    public List<PlayerConfigObject> playerConfigurations;

}
