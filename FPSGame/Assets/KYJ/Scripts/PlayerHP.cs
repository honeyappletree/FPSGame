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
    private float recoveryHP;

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
        if(other.gameObject.CompareTag("Fire"))
        {
            currentHP -= 5;
            hpBar.fillAmount = currentHP / maxHP;
            remainHP = hpBar.fillAmount * 100;
            hpText.text = remainHP.ToString();
            Debug.Log("currentHp: " + currentHP);
        }
    }

    public void DamageAction()
    {
        currentHP -= 10;
        hpBar.fillAmount = currentHP / maxHP;
        remainHP = hpBar.fillAmount * 100;
        hpText.text = remainHP.ToString();
        Debug.Log("currentHp: " + currentHP);
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
        if (currentHP < maxHP)
        {
            if (currentHP + _count <= maxHP) //현재 hp + hp 회복량이 maxHP보다 작거나 같은 경우
            {
                currentHP += _count;
                hpBar.fillAmount = currentHP / maxHP;
                remainHP = hpBar.fillAmount * 100;
                hpText.text = remainHP.ToString();
                StartCoroutine(FontColor());
                Debug.Log(_count + "만큼 회복되었습니다.");
            }

            else if (currentHP + _count > maxHP) //현재 hp + hp 회복량이 maxHP보다 큰 경우
            {
                recoveryHP = (currentHP + _count) - maxHP; //현재 hp + hp 회복량에서 maxHP를 뺀 만큼 회복하여 maxHP만큼만 회복하게끔
                currentHP += recoveryHP;
                hpBar.fillAmount = currentHP / maxHP;
                remainHP = hpBar.fillAmount * 100;
                hpText.text = remainHP.ToString();
                StartCoroutine(FontColor());
                Debug.Log(recoveryHP + " 만큼 회복되었습니다.");
            }
        }

        else if(currentHP == maxHP) //현재 hp와 maxHP가 같은 경우(현재 hp == 100)
        {
            _count = 0;
            currentHP += _count;
            remainHP = hpBar.fillAmount * 100;
            hpText.text = remainHP.ToString();
            Debug.Log("풀hp입니다.");
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
