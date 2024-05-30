using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Permet de changer la position et la taille du gameobject possedant ce composant. 
/// </summary>
public class UpdatePositionHelper : MonoBehaviour
{
    public int maxX, maxY, minX, minY;



    public void SetPositionX(float val)
    {
        Vector3 pos = transform.position;
        pos.x = val;
        transform.position = pos;
    }
    public void SetPositionY(float val)
    {
        Vector3 pos = transform.position;
        pos.y = val;
        transform.position = pos;
    }

    public void setScale(float val)
    {
        Vector3 pos = transform.localScale;
        pos.y = val;
        pos.x = val;
        transform.localScale = pos;
    }
}
