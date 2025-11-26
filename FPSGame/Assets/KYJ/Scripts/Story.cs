using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Story : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] [TextArea] string[] dialogue; //대화 배열
    [SerializeField] Text dialogueText; //대화 텍스트
    [SerializeField] string[] name; //캐릭터 이름 배열
    [SerializeField] Text nameText; //이름 텍스트
    int readDialogue; 
    public static bool doneStory = false; //스토리를 다봤는지 확인하는 변수
    [SerializeField] GameObject titleImage;
    [SerializeField] Button nextButton; //다음 버튼
    AudioSource storyAudio1;
    public AudioClip callSound; //전화 거는 신호음
    public AudioClip carSound; //경찰차 사운드

    // Start is called before the first frame update
    void Start()
    {
        dialogueText.text = " ";
        nameText.text = " ";
        readDialogue = 0;
        titleImage.SetActive(false);
        storyAudio1 = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) == true)
        {
            if(readDialogue < 11)
            {
                nameText.text = name[readDialogue];
                dialogueText.text = dialogue[readDialogue];
                readDialogue++;

                if(readDialogue == 11)
                {
                    titleImage.SetActive(true);
                    Invoke("NextScene", 3f);
                }

                if(readDialogue == 4)
                {
                    storyAudio1.PlayOneShot(callSound);
                }
                if(readDialogue == 5)
                {
                    storyAudio1.Stop();
                }
                if (readDialogue == 11)
                {
                    storyAudio1.PlayOneShot(carSound);
                }
            }
        }
    }

    void NextScene()
    {
        SceneManager.LoadScene("LobbyScene");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        
    }
}
