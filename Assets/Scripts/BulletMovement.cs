using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    private float m_fBulletSpd = 1.0f;
    private bool m_fIsBulletReleased = false;
    private bool m_fIsBulletLeft = false;
    private float m_fLifeTimer = 20.0f;
    // Start is called before the first frame update
    void Start()
    {
        if(this.gameObject.tag.Equals("LeftBullet"))
        {
            m_fIsBulletLeft = true;
        } else if (this.gameObject.tag.Equals("RightBullet"))
        {
            m_fIsBulletLeft = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(m_fIsBulletReleased)
        {
            transform.position += transform.right * m_fBulletSpd * Time.deltaTime;
            m_fLifeTimer -= Time.deltaTime;
        } else
        {
            if(m_fIsBulletLeft)
            {
                if(Input.GetKeyDown(KeyCode.W)) {
                    transform.position += new Vector3(0.0f, 0.5f, 0.0f);
                } else if(Input.GetKeyDown(KeyCode.S))
                {
                    transform.position -= new Vector3(0.0f, 0.5f, 0.0f);
                }
                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    m_fIsBulletReleased = true;
                }
            } else if (!m_fIsBulletLeft)
            {
                if (Input.GetKeyDown(KeyCode.I))
                {
                    transform.position += new Vector3(0.0f, 0.5f, 0.0f);
                }
                else if (Input.GetKeyDown(KeyCode.K))
                {
                    transform.position -= new Vector3(0.0f, 0.5f, 0.0f);
                }
                if (Input.GetKeyDown(KeyCode.RightShift))
                {
                    m_fIsBulletReleased = true;
                }
            }
        }

        if(m_fLifeTimer <= 0.0f)
        {
            Destroy(this.gameObject);
        }
        
        
    }
}
