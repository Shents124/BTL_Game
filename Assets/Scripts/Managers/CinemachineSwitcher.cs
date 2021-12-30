using System.Collections;
using UnityEngine;
using Cinemachine;

public class CinemachineSwitcher : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cameraFollowPlayer;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private PlayerInputHandle playerInputHandle;

    public void SwitchCamera()
    {
        StartCoroutine("SwitchBetweenTwoCamera");
    }

    IEnumerator SwitchBetweenTwoCamera()
    {
        playerInputHandle.canGetInput = false;
        yield return StartCoroutine(ChangePrority(0, 1));
        yield return new WaitForSeconds(2f);

        yield return StartCoroutine(ChangePrority(1, 0));
        yield return new WaitForSeconds(2f);

        playerInputHandle.canGetInput = true;
    }

    IEnumerator ChangePrority(int firtCamPriority, int secondCamPriority)
    {
        cameraFollowPlayer.Priority = firtCamPriority;
        virtualCamera.Priority = secondCamPriority;

        yield return null;
    }

    public void ChangeFieldOFView(float value)
    {
        cameraFollowPlayer.m_Lens.FieldOfView = value;
    }
}
