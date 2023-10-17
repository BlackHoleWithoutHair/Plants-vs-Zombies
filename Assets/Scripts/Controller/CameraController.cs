using UnityEngine;

public class CameraController : AbstractController
{
    private GameObject MainCamera;
    private Animator m_Animator;
    private AnimatorStateInfo info;
    private bool isCameraLeft;
    private bool isCameraRight;
    public CameraController() { }
    protected override void Init()
    {
        base.Init();
        MainCamera = GameObject.Find("Main Camera");
        m_Animator = MainCamera.GetComponent<Animator>();

    }
    protected override void OnAfterRunInit()
    {
        base.OnAfterRunInit();
    }
    protected override void OnAfterRunUpdate()
    {
        base.OnAfterRunUpdate();
        info = m_Animator.GetCurrentAnimatorStateInfo(0);
        if (ArchiveCommand.Instance.StageId < 7)
        {
            if (info.normalizedTime > 1f && info.IsName("CameraMoveLeft")&&!isCameraLeft)
            {
                isCameraLeft = true;
                EventCenter.Instance.NotisfyObserver(EventType.OnCameraMoveLeftFinish);
            }
            if (info.normalizedTime > 1f && info.IsName("CameraMoveRight"))
            {
                m_Animator.SetBool("isLeft", true);
            }
        }
        else
        {
            if (info.normalizedTime > 1f && info.IsName("CameraMoveLeft") && !isCameraLeft)
            {
                isCameraLeft = true;
                EventCenter.Instance.NotisfyObserver(EventType.OnCameraMoveLeftFinish);
            }
            if (info.normalizedTime > 1f && info.IsName("CameraMoveRight")&&!isCameraRight)
            {
                isCameraRight = true;
                EventCenter.Instance.NotisfyObserver(EventType.OnCameraMoveRightFinish);
            }
        }
    }
    public void MoveCameraLeft()
    {
        m_Animator.SetBool("isLeft", true);
    }
}