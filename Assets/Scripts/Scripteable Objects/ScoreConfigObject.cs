using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Score Object",menuName = "ScriptableObjects/Create Score Object", order = 3)]
public class ScoreConfigObject : ScriptableObject
{
    public int scoreAmmount = 20;
    public Color lightColor = Color.red;
    public Material material;


    public void ResetDefaultValues()
    {
        scoreAmmount = 20;
        lightColor = Color.red;
        material = null;
    }
}
