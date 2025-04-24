using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DDOL : MonoBehaviour
{
    private void Awake()
    {
        DDOL[] ddol = FindObjectsOfType<DDOL>();

        if (ddol.Length > 1)
        {
            Destroy(this.gameObject);
        }
        else if (ddol.Length == 1)
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
