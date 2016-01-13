using UnityEngine;
using System.Collections;

public class minimapCameraScript : MonoBehaviour {

    // object the camera follows, in this case the player object
    public GameObject targetToFollow;

    public GameObject enemy1;
    public GameObject enemy2;
    public Camera minimapCamera;
    public Texture2D texturePlayer;
    public Texture2D textureEnemy;
    public Texture2D borderMap;
    public Texture2D fogOfCheat;
    //private float angleView;
    private float sizeTextureEnemy;

    // get the center of the player/enemies to position the textures in the middle
    private Vector2 centerPlayer;
    private Vector2 centerEnemy1;
    private Vector2 centerEnemy2;

    // keep the camera at a distinct offset above the player
    private Vector3 offset;

    // player/enemy position ont he minimap
    //private Vector3 objPosition;
    private Vector3 positionEnemy1;
    private Vector3 positionEnemy2;

    void Start() {
        offset.y = 70;
        sizeTextureEnemy = 10;
    }
	
	// Update is called once per frame
	void Update () {

        // allways position Kamera above palyer
        transform.position = targetToFollow.transform.position + offset;
    }

    void OnGUI() {

        // get position of player on viewport of minimapcamera
        //objPosition = minimapCamera.WorldToViewportPoint(transform.position);

        // get viewing angle of player
        //angleView = targetToFollow.transform.eulerAngles.y - 90;
        //Matrix4x4 guiRot = GUI.matrix;

        // draw frame around miniMap
        GUI.DrawTexture(new Rect(minimapCamera.rect.x * Screen.width, ((1 - minimapCamera.rect.y)* Screen.height) - (Screen.height * minimapCamera.rect.height), minimapCamera.rect.width * Screen.width, 
            minimapCamera.rect.height * Screen.height), borderMap, ScaleMode.StretchToFill, true, 10.0F);

        //GUI.DrawTexture(new Rect(minimapCamera.rect.x * Screen.width, ((1 - minimapCamera.rect.y) * Screen.height) - (Screen.height * minimapCamera.rect.height), minimapCamera.rect.width * Screen.width,
          // minimapCamera.rect.height * Screen.height), fogOfCheat, ScaleMode.StretchToFill, true, 10.0F);

        // get position of enemy, x and y between 0 and 1 if on viewport of minimapcamera
        positionEnemy1 = minimapCamera.WorldToViewportPoint(enemy1.transform.position);

        // if enemy coordinates are on viewport, calulate where to put the enemy texture on the "main" screen (right on top of minimap)
        if ((positionEnemy1.x < 1 && positionEnemy1.x > 0) && (positionEnemy1.y < 1 && positionEnemy1.y > 0))
        {
            float distance = Vector3.Distance(enemy1.transform.position, targetToFollow.transform.position);
            // get coordinates where to draw the textures
            //Debug.Log("Enemy1 spotted! Distance: " + distance);

            if (distance <= 8)
            {
                centerEnemy1.x = Screen.width * (minimapCamera.rect.x + (positionEnemy1.x * minimapCamera.rect.width));
                centerEnemy1.y = Screen.height * (1 - (minimapCamera.rect.y + (positionEnemy1.y * minimapCamera.rect.height)));
                GUI.DrawTexture(new Rect(centerEnemy1.x, centerEnemy1.y, sizeTextureEnemy, sizeTextureEnemy), textureEnemy);
            }
        }

        // get position of enemy, x and y between 0 and 1 if on viewport of minimapcamera
        positionEnemy2 = minimapCamera.WorldToViewportPoint(enemy2.transform.position);

        // if enemy coordinates are on viewport, calulate where to put the enemy texture on the "main" screen (right on top of minimap)
        if ((positionEnemy2.x < 1 && positionEnemy2.x > 0) && (positionEnemy2.y < 1 && positionEnemy2.y > 0))
        {
            float distance = Vector3.Distance(enemy2.transform.position, targetToFollow.transform.position);
            // get coordinates where to draw the textures
            if (distance <= 8)
            {
                // get coordinates where to draw the textures
                centerEnemy2.x = Screen.width * (minimapCamera.rect.x + (positionEnemy2.x * minimapCamera.rect.width));
                centerEnemy2.y = Screen.height * (1 - (minimapCamera.rect.y + (positionEnemy2.y * minimapCamera.rect.height)));
                GUI.DrawTexture(new Rect(centerEnemy2.x, centerEnemy2.y, sizeTextureEnemy, sizeTextureEnemy), textureEnemy);
            }
        }

        // get the center position of the player to place texture and rotate the texture later 
        //centerPlayer.x = Screen.width * (minimapCamera.rect.x + (objPosition.x * minimapCamera.rect.width));
        //centerPlayer.y = Screen.height * (1 - (minimapCamera.rect.y + (objPosition.y * minimapCamera.rect.height)));

        // rotate view angle of player around pivot point
        //GUIUtility.RotateAroundPivot(angleView, centerPlayer);

        // position the texture of player on minimap 
        //float xMini = (float)(Screen.width * (minimapCamera.rect.x + (objPosition.x * minimapCamera.rect.width)) - 7.5);
        //float yMini = (float)(Screen.height * (1 - (minimapCamera.rect.y + (objPosition.y * minimapCamera.rect.height))) - 7.5);
        //GUI.DrawTexture(new Rect(xMini, yMini, 20, 20), texturePlayer);
    }
}


