using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    const int UP = 0;
    const int DOWN = 1;
    const int LEFT = 2;
    const int RIGHT = 3;

    [SerializeField]
    GameObject AxisPointLeft;
    [SerializeField]
    GameObject AxisPointRight;
    [SerializeField]
    GameObject AxisPointUp;
    [SerializeField]
    GameObject AxisPointDown;

    Transform[] apPoint;

    [Range(0.0f,1.0f)]
    [SerializeField]
    float percentTest = 1.0f;
    [SerializeField]
    bool startLerp;

    bool allowSwitch;
    bool allowMove;
    float startTime;
    public float lerpTimeInterval;
    public enum Axis
    {
        horizontal,
        vertical,
        both
    }

    Vector2 lastInput;


    public Axis currentAxis;
    // Start is called before the first frame update
    void Start()
    {
        currentAxis = Axis.vertical;
        apPoint = new Transform[4];
        apPoint[UP] = AxisPointUp.transform;
        apPoint[DOWN] = AxisPointDown.transform;
        apPoint[RIGHT] = AxisPointRight.transform;
        apPoint[LEFT] = AxisPointLeft.transform;

        UpdateAxisScaleImmediate();

        allowSwitch = true;
        allowMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        ApplyInput();

        if(startLerp)
        {
            float timeElapsed = Time.time - startTime;
            float percent = timeElapsed / lerpTimeInterval;
            UpdateAxisScaleImmediate(percent);
            if(percent >= 1.0f)
            {
                allowSwitch = true;
                allowMove = true;
                startLerp = false;
            }
        }
    }

    void GetInput()
    {
        if( Input.GetKeyDown(KeyCode.UpArrow) )
        {
            lastInput.y += 1;
        } 
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            lastInput.y -= 1;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            lastInput.x -= 1;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            lastInput.x += 1;
        }

        if(Input.GetKeyDown(KeyCode.Space) && allowSwitch)
        {
            //UpdateAxisScaleImmediate();
            SetAxisScaleLerp();
        }
    }

    void SetAxisScaleLerp()
    {
        SwitchAxis();
        allowSwitch = false;
        allowMove = false;
        startTime = Time.time;
        startLerp = true;
    }
    void SwitchAxis()
    {
        ProjectileSpawner.Instance.PlaySwitchUp();

        if ( currentAxis == Axis.horizontal)
        {
            currentAxis = Axis.vertical;
        } 
        else if ( currentAxis == Axis.vertical)
        {
            currentAxis = Axis.horizontal;
        }
    }
    void ApplyInput()
    {
        if(lastInput != Vector2.zero && allowMove)
        {
            if(currentAxis == Axis.both)
            {
                transform.position +=  new Vector3(lastInput.x,lastInput.y);
            } 
            else if (currentAxis == Axis.vertical)
            {
                transform.position += new Vector3(0, lastInput.y);
            }
            else if (currentAxis == Axis.horizontal)
            {
                transform.position += new Vector3(lastInput.x, 0);
            }
            float height = Camera.main.orthographicSize - 0.5f;
            float width = Camera.main.orthographicSize * Screen.width / Screen.height - 0.5f;

            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, -width, width),
                Mathf.Clamp(transform.position.y, -height, height),0f);
            UpdateAxisScaleImmediate();
        }
        lastInput = Vector2.zero;
    }

    void UpdateAxisScaleImmediate(float percent = 1.0f)
    {
        float height = Camera.main.orthographicSize - 0.5f;
        float width = Camera.main.orthographicSize * Screen.width / Screen.height - 0.5f;

        if (currentAxis == Axis.vertical)
        {
            apPoint[UP].localScale = new Vector3(1, (height - transform.position.y) * percent,1);
            apPoint[DOWN].localScale = new Vector3(1, (height + transform.position.y) * percent,1);


            apPoint[RIGHT].localScale = new Vector3((width - transform.position.x) * (1.0f - percent), 1, 1);
            apPoint[LEFT].localScale = new Vector3((width + transform.position.x) * (1.0f - percent), 1, 1);
            //float width = height * Screen.width / Screen.height; 
        }  
        else if(currentAxis == Axis.horizontal)
        {
            apPoint[RIGHT].localScale = new Vector3( (width - transform.position.x) * percent, 1, 1);
            apPoint[LEFT].localScale = new Vector3( (width + transform.position.x) * percent, 1, 1);

            apPoint[UP].localScale = new Vector3(1, (height - transform.position.y) * (1.0f - percent), 1);
            apPoint[DOWN].localScale = new Vector3(1, (height + transform.position.y) * (1.0f - percent), 1);
        }
    }
}
