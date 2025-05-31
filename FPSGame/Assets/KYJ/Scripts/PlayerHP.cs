using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SocialPlatforms;

public class PlayerHP : MonoBehaviour
{
    [SerializeField] private Image hpBar;
    [SerializeField] private TextMeshProUGUI hpText;
    private float remainHP;
    private float maxHP = 100;
    private float currentHP;
    [SerializeField] private GameObject resultPanel;
    [SerializeField] private GameObject warningPanel;
    private float timer = 3;
    private bool isFont = false;

    // Start is called before the first frame update
    void Start()
    {
        currentHP = maxHP;
        hpBar.GetComponent<Image>();
        isFont = false;
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

        if(other.gameObject.CompareTag("Fire"))
        {
            currentHP -= 5;
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
            StartCoroutine(FontColor());
            Debug.Log(_count + "만큼 회복되었습니다.");
        }
        else
        {
            currentHP = maxHP;
            Debug.Log("현재 hp가 풀hp입니다.");
        }
    }

    IEnumerator FontColor() //폰트의 색을 일시적으로 변하게 하는 함수
    {
        isFont = true;

        timer = 2;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            hpText.color = Color.green;
            Debug.Log("폰트 색이 초록색으로 변하였습니다.");
            Time.timeScale = 1; //폰트 색은 일시정지 되는 것과 관계없이 인벤토리 활성화된 상태에서도 변화를 주기 위해서
            yield return null;
        }
        hpText.color = Color.black;
        Debug.Log("폰트 색이 검은색으로 변하였습니다.");
    }
}
