using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
    public virtual bool CanActivate() { return false; }
    public virtual void Activate() { }
}
