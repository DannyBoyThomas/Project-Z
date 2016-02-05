using UnityEngine;
using System.Collections;

public class SaveTest : MonoBehaviour {

    [Save]
    public float Health;
    [Save]
    public float RandomNumber = 0.5f;
}
