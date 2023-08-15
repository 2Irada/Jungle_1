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
        foreach (Transform child in transform)
        {
            //Debug.Log(child.name);
            GameObject temp = Instantiate(prefabMask, child.transform.position, Quaternion.identity);
            temp.name = "Platform_Mask";
            temp.transform.localScale = new Vector3(child.transform.localScale.x + margin, child.transform.localScale.y + margin, child.transform.localScale.z);
            temp.SetActive(false);
            temp.transform.parent = child.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
