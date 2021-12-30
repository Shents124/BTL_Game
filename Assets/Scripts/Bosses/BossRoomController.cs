using UnityEngine;

public class BossRoomController : MonoBehaviour
{
    public GameObject boss;
    public Transform startPos;
    public void EnableBoss()
    {
        boss.SetActive(true);
        boss.transform.position = startPos.position;
    } 

    public void ResetBossFight()
    {
        boss.SetActive(false);
    }
}
