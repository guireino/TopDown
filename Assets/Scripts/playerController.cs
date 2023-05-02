using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour{

    private Rigidbody playerRb;
    private Vector3 movement;
    private float camRayLength = 100f;
    private int groundMask;

    public float speed;

    // Start is called before the first frame update
    void Start(){

        playerRb = GetComponent<Rigidbody>();
        groundMask = LayerMask.GetMask("Ground"); // verifica onde esta chao

    }

    // Update is called once per frame
    void FixedUpdate(){

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Move(horizontal, vertical);

        Turning();
    }

    void Move(float h, float v){

        movement.Set(h, 0f, v); // vai deterninar movemento

        // When normalized, a vector keeps the same direction but its length is 1
        movement = movement.normalized * speed * Time.deltaTime;

        playerRb.MovePosition(transform.position + movement);

    }


    void Turning(){

        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);  // vai criar um linha na direcao mouse
        RaycastHit groundHit;

        // verifica se tem vai colidir com chao e ne outro objeto que esteja layer ground
        if(Physics.Raycast(camRay, out groundHit, camRayLength, groundMask)){
            
            Vector3 playerToMouse = groundHit.point - transform.position;
            playerToMouse.y = 0; // colocando y no valor 0 para ele nao rotacionar objeto

            Quaternion newRotation = Quaternion.LookRotation(playerToMouse); // develindo para onde vai olhar

            playerRb.MoveRotation(newRotation);

        }

    }

}