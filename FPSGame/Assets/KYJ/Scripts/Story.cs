using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class Story : MonoBehaviour
{
    [SerializeField] [TextArea] string[] dialogue; //대화 배열
    [SerializeField] TextMeshProUGUI dialogueText; //대화 텍스트
    [SerializeField] string[] name; //캐릭터 이름 배열
    [SerializeField] TextMeshProUGUI nameText; //이름 텍스트
    int readDialogue; //space키를 누른 횟수
    public static bool doneStory = false; //스토리를 다봤는지 확인하는 변수
    [SerializeField] GameObject titleImage;
    [SerializeField] Button nextButton; //다음 버튼

    // Start is called before the first frame update
    void Start()
    {
        readDialogue = 0;
        dialogueText.text = dialogue[0];
        nameText.text = name[0];
        titleImage.SetActive(false);
        nextButton.onClick.AddListener(ReadStory);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) == true)
        {
            readDialogue++;
            dialogueText.text = dialogue[readDialogue];
            nameText.text = name[readDialogue];

            if(readDialogue == 10)
            {
                titleImage.SetActive(true);
                Invoke("NextScene", 3f);
            }
        }
    }

    void ReadStory()
    {
        readDialogue++;
        dialogueText.text = dialogue[readDialogue];
        nameText.text = name[readDialogue];

        if (readDialogue == 10)
        {
            titleImage.SetActive(true);
            Invoke("NextScene", 3f);
        }
    }

    void NextScene()
    {
        doneStory = true;
        SceneManager.LoadScene("LobbyScene");
    }
}
