using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StretchCube : Cube
{
    public float force;
    public Sprite playerOnSprite;
    SpriteRenderer spriteRenderer;
    Sprite playerOffSprite;
    float timer;

    private void Start() {
        timer = 0.2f;
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerOffSprite = spriteRenderer.sprite;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
            other.GetComponent<testCharacter>().isCanJump = true;

            spriteRenderer.sprite = playerOnSprite;

            float x = 0;
            float y = 0;
            if(transform.rotation.eulerAngles.z == 0){
                y = force;
            }else if(transform.rotation.eulerAngles.z == 180 || transform.rotation.eulerAngles.z == -180){
                y = -force;
            }
            other.GetComponent<Rigidbody2D>().velocity = new Vector2(x,y);
               
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "Player"){
            other.GetComponent<testCharacter>().isCanJump = false;
            timer = 0;
        }
    }

    private void Update() {
        if(timer < 0.2){
            timer += Time.deltaTime;
            
            if(spriteRenderer.sprite != playerOnSprite)
                spriteRenderer.sprite = playerOnSprite;
        }else{
            timer = 0.2f;
            spriteRenderer.sprite = playerOffSprite;
        }

        if(spriteRenderer.sortingLayerName != "Maps"){
            spriteRenderer.sortingLayerName = "Maps";
        }
    }
}
