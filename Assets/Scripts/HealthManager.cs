using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public int maxHealth = 3;
    public int currentHealth;
    public SubmissionCheck SubCheck;
    public List<RectTransform> hearts;
    public float heartAnimSpeed = 1.0f;
    public float maxHeartSize = 2f;
    private void Start()
    {
        SubCheck = FindObjectOfType<SubmissionCheck>();
        currentHealth = maxHealth;
        hearts = GetComponentsInChildren<RectTransform>().ToList();
        hearts.RemoveAt(0);//removes the parent recttrans
    }
    public void UpdateHealth()
    {

        if(!SubCheck.Correct)
        {
            currentHealth--;
            Destroy(hearts[hearts.Count - 1].gameObject);
            hearts.RemoveAt(hearts.Count - 1); 
            Debug.Log("current health:" +  currentHealth);
        }

    }
    public void Update()
    {
        for (int i = 0; i < hearts.Count; i++)
        {
            hearts[i].localScale = Mathf.Clamp(maxHeartSize * Mathf.Sin(Time.timeSinceLevelLoad * heartAnimSpeed + i * Mathf.PI / 4f), 1f, maxHeartSize) * Vector3.one;
        }

    }
}
