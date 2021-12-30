using UnityEngine;

public class PlayerSavePos : MonoBehaviour
{
    private void OnEnable()
    {
        EventBroker.OnSavePlayerPos += Save;
    }
    private void OnDisable()
    {
        EventBroker.OnSavePlayerPos -= Save;
    }
    public void Save(SavePoint savePoint)
    {
        PlayerPrefs.SetFloat("PlayerPosX", savePoint.transform.position.x);
        PlayerPrefs.SetFloat("PlayerPosY", savePoint.transform.position.y);
        PlayerPrefs.SetFloat("PlayerPosZ", savePoint.transform.position.z);
    }

    public Vector3 Load()
    {
        if (PlayerPrefs.HasKey("PlayerPosX") == false)
            return Vector3.zero;
        else
        {
            float x = PlayerPrefs.GetFloat("PlayerPosX");

            float y = PlayerPrefs.GetFloat("PlayerPosY");
            float z = PlayerPrefs.GetFloat("PlayerPosZ");

            return new Vector3(x, y, z);
        }
    }
}
