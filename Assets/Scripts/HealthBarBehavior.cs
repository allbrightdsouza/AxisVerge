using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarBehavior : MonoBehaviour
{
    private float m_fBlockSize = 0.5f;
    private float m_fCurrDamage = 1.0f;
    private float m_fRawDamage = 8.0f;
    private static float m_fMaxDamage = 16.0f;
    public bool m_bIsLeft = true;
    public float m_fDamageMultiplier = 8.0f;
    private float m_fShortCoolTime = 1.0f;
    private float m_fLongCoolTime = 6.0f;
    private float m_fThiccCoolTime = 3.0f;
    private float m_fCooldownTimer = 0.0f;
    public GameObject m_goShort;
    public GameObject m_goThicc;
    public GameObject m_goLong;
    private bool m_bIsTimerStarted = true;
    // Start is called before the first frame update
    void Start()
    {
        if(m_bIsLeft)
        {
            transform.localScale = new Vector3(m_fCurrDamage, 8.0f, m_fBlockSize);
        } else
        {
            transform.localScale = new Vector3(m_fCurrDamage, -8.0f, m_fBlockSize);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        m_fCurrDamage = (m_fRawDamage / (8.0f / m_fBlockSize));
        float fVisibleDamage = 0.0f;
        fVisibleDamage = m_fCurrDamage - (m_fCurrDamage % m_fBlockSize);
        transform.localScale = new Vector3(fVisibleDamage, 8.0f, m_fBlockSize);
        if(m_bIsLeft)
        {
            transform.position = new Vector3(-8.5f + (fVisibleDamage / 2.0f), 0.0f, 0.0f);
            if(m_fCooldownTimer <= 0.0f && m_bIsTimerStarted)
            {
                float fRandomShape = 0.0f;
                fRandomShape = Random.Range(1.0f, 3.0f);
                if(fRandomShape > 2.0f)
                {
                    GameObject newBullet = Instantiate(m_goLong, new Vector3(-8.0f + (fVisibleDamage / 2.0f), 0.0f, 0.0f), Quaternion.Euler(new Vector3(0.0f, 0.0f, 0.0f)));
                    newBullet.tag = "LeftBullet";
                    m_fCooldownTimer = m_fLongCoolTime;
                } else if(fRandomShape > 1.0f)
                {
                    GameObject newBullet = Instantiate(m_goThicc, new Vector3(-8.0f + (fVisibleDamage / 2.0f), 0.0f, 0.0f), Quaternion.Euler(new Vector3(0.0f, 0.0f, 0.0f)));
                    newBullet.tag = "LeftBullet";
                    m_fCooldownTimer = m_fThiccCoolTime;
                } else
                {
                    GameObject newBullet = Instantiate(m_goShort, new Vector3(-8.0f + (fVisibleDamage / 2.0f), 0.0f, 0.0f), Quaternion.Euler(new Vector3(0.0f, 0.0f, 0.0f)));
                    newBullet.tag = "LeftBullet";
                    m_fCooldownTimer = m_fShortCoolTime;
                }
                m_bIsTimerStarted = false;
            }
        }
        else
        {
            transform.position = new Vector3(8.5f - (fVisibleDamage / 2.0f), 0.0f, 0.0f);
            if (m_fCooldownTimer <= 0.0f && m_bIsTimerStarted)
            {
                float fRandomShape = 0.0f;
                fRandomShape = Random.Range(1.0f, 3.0f);
                if (fRandomShape > 2.0f)
                {
                    GameObject newBullet = Instantiate(m_goLong, new Vector3(8.0f - (fVisibleDamage / 2.0f), 0.0f, 0.0f), Quaternion.Euler(new Vector3(0.0f, 0.0f, 180.0f)));
                    newBullet.tag = "RightBullet";
                    m_fCooldownTimer = m_fLongCoolTime;
                }
                else if (fRandomShape > 1.0f)
                {
                    GameObject newBullet = Instantiate(m_goThicc, new Vector3(8.0f - (fVisibleDamage / 2.0f), 0.0f, 0.0f), Quaternion.Euler(new Vector3(0.0f, 0.0f, 180.0f)));
                    newBullet.tag = "RightBullet";
                    m_fCooldownTimer = m_fThiccCoolTime;
                }
                else
                {
                    GameObject newBullet = Instantiate(m_goShort, new Vector3(8.0f - (fVisibleDamage / 2.0f), 0.0f, 0.0f), Quaternion.Euler(new Vector3(0.0f, 0.0f, 180.0f)));
                    newBullet.tag = "RightBullet";
                    m_fCooldownTimer = m_fShortCoolTime;
                }
                m_bIsTimerStarted = false;
            }
        }
        if((m_bIsLeft && Input.GetKeyDown(KeyCode.LeftShift)) || (!m_bIsLeft && Input.GetKeyDown(KeyCode.RightShift)))
        {
            m_bIsTimerStarted = true;
        }
        if(m_bIsTimerStarted)
        {
            m_fCooldownTimer -= Time.deltaTime;
        }
    }

    //Please give each player the health bar gameobject and link it by using GetComponent<HealthBarBehavior>()
    public void AddDamage()
    {
        m_fRawDamage += m_fBlockSize * m_fDamageMultiplier;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(m_bIsLeft)
        {
            if (collision.gameObject.tag.Equals("RightBullet")) {
                AddDamage();
            }
        } else
        {
            if(collision.gameObject.tag.Equals("LeftBullet")) {
                AddDamage();
            }
        }
    }
}
