using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Enemy targetEnemy;
    public Image fillImage;

    //hook the options canvas here using inspector
    public GameObject canvasToShow; 
    
    public Vector3 offset = new Vector3(0f, 2f, 0f);
    Camera cam;

    void Start()
    {
        cam = Camera.main;
        if (targetEnemy == null)
            targetEnemy = GetComponentInParent<Enemy>();

        fillImage.type = Image.Type.Filled;
    }

    void LateUpdate()
    {
        

        // 2) Follow the boss
        transform.position = targetEnemy.transform.position + offset;
        transform.rotation = Quaternion.Euler(45f, 0f, 0f);

        // 3) Update fill
        float t = Mathf.Clamp01((float)targetEnemy.Health / targetEnemy.MaxHealth);
        fillImage.fillAmount = t;



        // 1) If the boss is destroyed (or the reference is gone), kill the bar too
        if (t == 0.1f)
        {
               

            // 2) Notify the EventManager
            if (EventManager.Instance != null)
                EventManager.Instance.BossDefeated();

            // 3) Destroy the health bar itself
            Destroy(gameObject);
            Destroy(targetEnemy.gameObject);

           canvasToShow.SetActive(true);
        }


    }
}