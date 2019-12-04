using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Tank {
    public delegate void destroy();
    public static event destroy destroyEvent;
    private float HP;
    private Camera camera;
    public Texture2D blood_red_texture;
    public Texture2D blood_black_texture;
    void Start () {
        // 初始血量
        setHp(1000.0f);
        HP = getHp();
        camera = Camera.main;
    }
	
	void Update () {
		if(getHp() <= 0)
        {
            this.gameObject.SetActive(false);
            if (destroyEvent != null)
                destroyEvent();
        }
	}
    void OnGUI()
    {
        HP = getHp();
        if (HP < 0f)
        {
            HP = 0f;
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
        float blood_width = (bloodSize.x) * HP / 1000;
        //没血血条
        GUI.DrawTexture(new Rect(position.x - (bloodSize.x / 2), position.y - 40, bloodSize.x, 5), blood_black_texture);
        //红色血条,
        GUI.DrawTexture(new Rect(position.x - (bloodSize.x / 2), position.y - 40, blood_width, 5), blood_red_texture);
    }
}
