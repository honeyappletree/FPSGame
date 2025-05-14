using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHP : MonoBehaviour
{
    [SerializeField] private Image hpBar;
    [SerializeField] private TextMeshProUGUI hpText;
    private float remainHP;
    private float maxHP = 100;
    private float currentHP;
    [SerializeField] private GameObject resultPanel;

    // Start is called before the first frame update
    void Start()
    {
        currentHP = maxHP;
        hpBar.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHP == 0)
        {
            resultPanel.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EnemyBullet"))
        {
            currentHP -= 20;
            hpBar.fillAmount = currentHP / maxHP;
            remainHP = hpBar.fillAmount * 100;
            hpText.text = remainHP.ToString();
            Debug.Log("currentHp: " + currentHP);

            if (currentHP <= 20)
            {
                hpText.color = Color.red;
            }
        }
    }
}
