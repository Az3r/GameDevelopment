using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SpaceCraft", fileName = "SpaceCraft")]
public class SpaceCraft : ScriptableObject
{
    public float speed;
    // in seconds
    public float reload;
    public float attack;
    public float defense;
    public int health;
    public Material skin;
    public Mesh shape;
}
