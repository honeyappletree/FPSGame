using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Loading : MonoBehaviour
{
    [SerializeField] private Image Loadingbar_front;
    [SerializeField] private TextMeshProUGUI Loading_num;
    [SerializeField] private string sceneName;
    [SerializeField] GameObject Tip1;
    [SerializeField] GameObject Tip2;
    private float currentLoading;

    // Start is called before the first frame update
    void Start()
    {
        Tip2.SetActive(false);
        StartCoroutine(SceneCoroutine());
        Invoke("TipChange", 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SceneCoroutine()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);

        while (!op.isDone)
        {

            yield return null;

            op.allowSceneActivation = false;

            float timer = 0;
            yield return new WaitForSeconds(0.3f);
            while (!op.isDone)
            {
                currentLoading = Loadingbar_front.fillAmount * 100;
                Loading_num.text = currentLoading.ToString("F2") + "%";
                timer += Time.deltaTime;
                if (op.progress < 0.9f)
                {
                    Loadingbar_front.fillAmount = Mathf.Lerp(Loadingbar_front.fillAmount, op.progress, timer);

                    if (Loadingbar_front.fillAmount >= op.progress)
                    {
                        timer += Time.deltaTime;
                        Debug.Log("timer: " + timer);
                    }
                }
                else
                {
                    Loadingbar_front.fillAmount = Mathf.Lerp(Loadingbar_front.fillAmount, 1f, timer);
                    if (Loadingbar_front.fillAmount == 1.0f)
                    {
                        op.allowSceneActivation = true;
                        SceneManager.LoadSceneAsync(sceneName);
                        Debug.Log("timer: " + timer);
                    }

                }
                
                yield return new WaitForSeconds(0.2f);
                yield return null;
            }
        }
    }

    void TipChange()
    {
        Tip1.SetActive(false);
        Tip2.SetActive(true);
    }
}
