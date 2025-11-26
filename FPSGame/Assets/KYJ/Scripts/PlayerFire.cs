using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerFire : MonoBehaviour
{
    private float currentTime;
    [SerializeField] private Text coolTimeText2; //물폭탄 쿨타임 텍스트
    private float coolTime = 5;
    private float waterF; //ui 연출을 위한 변수
    private bool isWater = false; //쿨타임 true, false 여부
    private bool shootBomb = false; //물폭탄을 쏠 수 있는지 여부 
    private float waterTimer = 2; //속도를 일시적으로 올리기 위한 타이머
    [SerializeField] private Image water; //물폭탄 스킬의 맨 윗부분 이미지
    [SerializeField] private Transform waterPosition;
    [SerializeField] private GameObject waterBomb;
    [SerializeField] private float throwForce; //물폭탄을 던지는 힘
    [SerializeField] private float upForce; //위로 향하는 힘
    [SerializeField] private float range; //적과의 거리
    public static bool canShoot = false; //총을 쏠 수 있는지에 대한 여부
    private RaycastHit hitInfo;
    [SerializeField] private LayerMask[] layerMask;
    [SerializeField] private Text currentBulletText;
    public static int currentBullet; //현재 남아있는 총알 수
    private int maxBullet = 10; //탄환 속 최대 총알 수
    public static bool isReload;//재장전이 가능한지 여부
    [SerializeField] GameObject[] enemy;
    public static int plusNum = 0; //튜토리얼에서 두 가지 스킬을 모두 사용했는지 확인하기 위함.
    AudioSource audioSource;
    public AudioClip fireClip; //탄환 발사 시 재생되는 사운드
    public AudioClip reloadClip; //탄환 재장전 시 재생되는 사운드

    // Start is called before the first frame update
    void Start()
    {
        coolTimeText2.color = new Color(1, 1, 1, 0);
        currentTime = coolTime;
        water.GetComponent<Image>();
        currentBullet = maxBullet;
        canShoot = false;
        isReload = false;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B) && isWater == false) //물폭탄 공격 쿨타임
        {
            isWater = true;
            currentTime = coolTime;
            StartCoroutine(ShootWater());
        }
        ShootGun();
        if (!isReload && Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(GunReload());
        }
    }

    IEnumerator ShootWater()
    {
        isWater = true;

        waterTimer = 2;
        while (waterTimer > 0)
        {
            waterTimer -= Time.deltaTime;
            shootBomb = true;
            if(Input.GetKeyDown(KeyCode.B))
            {
                GameObject _water = Instantiate(waterBomb, waterPosition.position, Quaternion.identity);
                Rigidbody rigidbody = _water.GetComponent<Rigidbody>();
                Vector3 direction = waterPosition.forward * throwForce + Vector3.up * upForce;
                rigidbody.AddForce(direction, ForceMode.Impulse);
                Debug.Log(shootBomb);
            }
            yield return null;
        }
        Debug.Log("쿨타임 시작");

        while (currentTime > 0)
        {
            currentTime -= Time.deltaTime;

            shootBomb = false;
            Debug.Log(shootBomb);
            water.color = new Color(0.67f, 0.67f, 0.67f, 1);
            water.fillAmount = (int)currentTime / coolTime;
            waterF = water.fillAmount * 5;
            coolTimeText2.color = new Color(0, 0, 0, 255);
            coolTimeText2.text = waterF.ToString();

            yield return null;
        }
        coolTimeText2.color = new Color(0, 0, 0, 0);
        water.color = new Color(1, 1, 1, 1);

        isWater = false;
        plusNum++;
    }

    void ShootGun()
    {
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hitInfo, layerMask[0]))
        {
            if (hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("Enemy1"))
            {
                if(Input.GetMouseButtonDown(0) && currentBullet > 0 && isReload == false) //왼쪽 마우스 버튼을 누르고 있고 재장전 중이 아니며 현재 탄환 수가 0보다 클 떄
                {
                    if (Inventory.isActive == false)
                    {
                        currentBullet--;
                        currentBulletText.text = currentBullet.ToString();
                        var effect = EffectPool.GetObject();
                        Debug.Log("currentBullet: " + currentBullet);
                        canShoot = true;
                        Debug.Log("적이 감지되어 총을 사용하였습니다.");
                        audioSource.PlayOneShot(fireClip);
                        enemy[0].GetComponent<EnemyFSM>().HitEnemy();

                        if (currentBullet <= 4)
                        {
                            canShoot = true;
                            currentBulletText.color = Color.red;
                        }
                    }
                    else if(Inventory.isActive == true)
                    {
                        canShoot = false;
                        isReload = false;
                        Debug.Log("인벤토리가 활성화되어 있습니다.");
                    }
                }

                if (currentBullet <= 0 && Input.GetMouseButtonDown(0) && canShoot == true)
                {
                    canShoot = false;
                    currentBullet -= 0;
                    currentBulletText.text = currentBullet.ToString();
                    Debug.Log("현재 탄환 수는 0 이하입니다." + canShoot);
                }
            }
        }
        //else
        //{
        //    canShoot = false;
        //    Debug.Log("적 외에는 총을 사용할 수 없습니다.");
        //}

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hitInfo, layerMask[1]))
        {
            if (hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("Enemy2"))
            {
                if (Input.GetMouseButtonDown(0) && currentBullet > 0 && isReload == false) //왼쪽 마우스 버튼을 누르고 있고 재장전 중이 아니며 현재 탄환 수가 0보다 클 떄
                {
                    if (Inventory.isActive == false)
                    {
                        currentBullet--;
                        currentBulletText.text = currentBullet.ToString();
                        var effect = EffectPool.GetObject();
                        Debug.Log("currentBullet: " + currentBullet);
                        canShoot = true;
                        Debug.Log("적이 감지되어 총을 사용하였습니다.");
                        audioSource.PlayOneShot(fireClip);
                        enemy[1].GetComponent<EnemyFSM>().HitEnemy();

                        if (currentBullet <= 4)
                        {
                            canShoot = true;
                            currentBulletText.color = Color.red;
                        }
                    }
                    else if (Inventory.isActive == true)
                    {
                        canShoot = false;
                        isReload = false;
                        Debug.Log("인벤토리가 활성화되어 있습니다.");
                    }
                }

                if (currentBullet <= 0 && Input.GetMouseButtonDown(0) && canShoot == true)
                {
                    canShoot = false;
                    currentBullet -= 0;
                    currentBulletText.text = currentBullet.ToString();
                    Debug.Log("현재 탄환 수는 0 이하입니다." + canShoot);
                }
            }
        }
    //    else
    //    {
    //        canShoot = false;
    //        Debug.Log("적 외에는 총을 사용할 수 없습니다.");
    //    }
    }

    IEnumerator GunReload()
    {
        if(Input.GetKeyDown(KeyCode.R) && Inventory.isActive == false)
        {
            isReload = true;
            canShoot = false;
            audioSource.PlayOneShot(reloadClip);
            currentBullet = maxBullet;
            currentBulletText.text = currentBullet.ToString();
            currentBulletText.color = Color.blue;
            yield return new WaitForSeconds(1f);
            Debug.Log("탄환이 maxBullet만큼 재장전되었습니다.");
            currentBulletText.color = Color.white;
            isReload = false;
            canShoot = true;
        }
    }
}
