using UnityEngine;
using System.Collections;

public class HealthBar_IMGUI : MonoBehaviour
{
    private float HP = 100;
    private Camera camera;
    public GameObject player;
    public Texture2D blood_red_texture;
    public Texture2D blood_black_texture;

    void Start()
    {
        camera = Camera.main;
       
    }

    void OnGUI()
    {
        HP -= 0.1f;
        if (HP > 100f)
        {
            HP = 100f;
        }
        //3D坐标换算成2D坐标
        Vector3 worldPosition = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        Vector2 position = camera.WorldToScreenPoint(worldPosition);

        //Player的2D坐标
        position = new Vector2(position.x, Screen.height - position.y);
        //没血血条的长宽
        Vector2 bloodSize = GUI.skin.label.CalcSize(new GUIContent(blood_red_texture));
        bloodSize.x -= 950;
        //红色血条宽度
        float blood_width = (bloodSize.x) * HP / 100;
        //没血血条
        GUI.DrawTexture(new Rect(position.x - (bloodSize.x / 2), 170, bloodSize.x, 5), blood_black_texture);
        //红色血条,
        GUI.DrawTexture(new Rect(position.x - (bloodSize.x / 2), 170, blood_width, 5), blood_red_texture);
    }
}