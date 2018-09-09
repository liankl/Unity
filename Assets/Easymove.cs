using UnityEngine;

using System.Collections;

using UnityEngine.Audio;

//如果添加该脚本的对象上没有挂载 AudioSource组件，将自动添加该组件

[RequireComponent(typeof(AudioSource))]

public class Easymove : MonoBehaviour
{
    private AudioSource _audioSources;

    public AudioMixerGroup _audioMixerGroup;
    // Subscribe to events  

    void Start()
    {
        _audioSources = GetComponent<AudioSource>();

        if (_audioSources == null)

        {

            Debug.LogWarning("audioSources is null");

            return;

        }

        if (_audioMixerGroup != null)

            _audioSources.outputAudioMixerGroup = _audioMixerGroup; // 设置音效混合器

        AudioClip audioClip = Resources.Load("脚步声/WALK") as AudioClip;

        if (audioClip == null)

        {

            Debug.LogWarning("audioClip is null");

            return;

        }

        _audioSources.clip = audioClip;     //动态添加音效

        _audioSources.playOnAwake = false;   //运行开始立即播放

        _audioSources.loop = true;          //设置循环播放

        _audioSources.priority = 128;       //设置优先权

        _audioSources.volume = 0;           //设置音量大小为 1

        _audioSources.pitch = 1;            //设置音高为 1

        _audioSources.spatialBlend = 0;     //设置音效为 2D 音效

        _audioSources.minDistance = 1;      //设置最小开始衰减距离

        _audioSources.maxDistance = 100;    //设置最大能听到音量距离

        //设置音量衰减类型为线性衰减

        _audioSources.rolloffMode = AudioRolloffMode.Linear;

        _audioSources.time = currentSecond;

        _audioSources.Play();
    }
    private float currentSecond = 0; //记录当前播放到了第几秒


    void OnEnable()
    {

        EasyTouch.On_TouchStart += On_TouchStart;

        EasyJoystick.On_JoystickMove += OnJoystickMove;

        EasyJoystick.On_JoystickMoveEnd += OnJoystickMoveEnd;

    }

    // Unsubscribe  

    void OnDisable()
    {

        EasyTouch.On_TouchStart -= On_TouchStart;

    }

    // Unsubscribe  

    void OnDestroy()
    {

        EasyTouch.On_TouchStart -= On_TouchStart;

    }

    // Touch start event  

    public void On_TouchStart(Gesture gesture)
    {

        Debug.Log("Touch in " + gesture.position);

    }

    //摇杆结束  

    void OnJoystickMoveEnd(MovingJoystick move)
    {

        if (move.joystickName == "MoveJoystick")
        {
            GetComponent<Animation>().Play("Take 001");
            _audioSources.volume = 0;//声音变为0
        }

    }

    //移动摇杆中  

    void OnJoystickMove(MovingJoystick move)
    {

        if (move.joystickName != "MoveJoystick")
        {

            return;

        }

        //摇杆偏移坐标 

        float joyPositionX = move.joystickAxis.x;

        float joyPositionY = move.joystickAxis.y;

        if (joyPositionY != 0 || joyPositionX != 0)
        {

            //设置人物朝向

            transform.LookAt(new Vector3(transform.position.x +joyPositionX, transform.position.y, transform.position.z +joyPositionY));

            //移动

            transform.Translate(-Vector3.forward * Time.deltaTime * 3);
            transform.GetComponent<Animation>().Play("walk");
            _audioSources.volume = 1;

        }

    }

}