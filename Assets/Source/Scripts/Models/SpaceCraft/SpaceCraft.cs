using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SpaceCraft", fileName = "SpaceCraft")]
public class SpaceCraft : ScriptableObject
{
    public float speed;
    public float reload; // in seconds
    public float attack;
    public int health;
}
