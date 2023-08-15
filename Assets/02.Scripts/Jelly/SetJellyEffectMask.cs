using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetJellyEffectMask : MonoBehaviour
{
    [SerializeField]private GameObject prefabMask;
    [SerializeField] private float margin = 0.5f;

    // Start is called before the first frame update
    void Start()
    {

            GameObject temp = Instantiate(prefabMask, transform.position, Quaternion.identity);
            temp.name = "Platform_Mask";
            temp.transform.localScale = new Vector3(transform.localScale.x + margin, transform.localScale.y + margin, transform.localScale.z);
            temp.SetActive(false);
            temp.transform.parent = this.transform;
    }
}
