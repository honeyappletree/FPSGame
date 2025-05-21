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
    [SerializeField] private GameObject warningPanel;

    // Start is called before the first frame update
    void Start()
    {
        currentHP = maxHP;
        hpBar.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        RemainHP();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EnemyBullet"))
        {
            currentHP -= 10;
            hpBar.fillAmount = currentHP / maxHP;
            remainHP = hpBar.fillAmount * 100;
            hpText.text = remainHP.ToString();
            Debug.Log("currentHp: " + currentHP);
        }
    }

    void RemainHP()
    {
        if (currentHP <= 20)
        {
            hpText.color = Color.red;
        }

        if(currentHP <= 10)
        {
            warningPanel.SetActive(true);
        }

        if (currentHP <= 0)
        {
            resultPanel.SetActive(true);
            warningPanel.SetActive(false);
            Time.timeScale = 0;
        }
    }

    public void IncreaseHP(int _count)
    {
        if(currentHP + _count < maxHP)
        {
            currentHP += _count;
            hpBar.fillAmount = currentHP / maxHP;
            remainHP = hpBar.fillAmount * 100;
            hpText.text = remainHP.ToString();
            Debug.Log(_count + "만큼 회복되었습니다.");
        }
        else
        {
            currentHP = maxHP;
            Debug.Log("현재 hp가 풀hp입니다.");
        }
    }
}
