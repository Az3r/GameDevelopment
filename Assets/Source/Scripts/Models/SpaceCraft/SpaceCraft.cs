using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SpaceCraft", fileName = "SpaceCraft")]
public class SpaceCraft : ScriptableObject
{
    public float speed;
    public int reload;
    public float attack;
    public float defense;
    public float health;
    public Material skin;
    public Mesh shape;
}
