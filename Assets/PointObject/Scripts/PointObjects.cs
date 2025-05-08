using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Collectables/PointObject/Objects")]
public class PointObjects : Item
{
    [SerializeField] int Points;

    public int getPoints()
    {
        return Points;
    }
}
