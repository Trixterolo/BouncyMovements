using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 8);
    }
}
