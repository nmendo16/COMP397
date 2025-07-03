using UnityEngine;
using UnityEngine.UI;

public class BossBulletSpawner : MonoBehaviour
{
    public GameObject Boss;
    public Vector3 offset = new Vector3(0f, 0f, 20f);
   
    void Start()
    {
        // place this gameobject's position  = boss' postiion + offset
        if (Boss == null)
            Boss = GetComponentInParent<Enemy>().gameObject;

    }

    void Update()
    {

        transform.position = Boss.transform.position + offset;
        transform.rotation = Quaternion.Euler(45f, 0f, 0f);

    }
}