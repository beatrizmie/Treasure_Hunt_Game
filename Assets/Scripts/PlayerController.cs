// https://assetstore.unity.com/packages/3d/props/rust-key-167590

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float _baseSpeed = 10.0f;
    float _gravity = 9.8f;

    CharacterController characterController;

    GameManager gm;

    // Referência usada para a câmera filha do jogador
    GameObject playerCamera;
    // Travar a rotação do player no ângulo que quisermos
    float cameraRotation;

    public GameObject key1;
    public GameObject key2;
    public GameObject key3;
    public GameObject chest;
    

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();  
        playerCamera = GameObject.Find("Main Camera");
        cameraRotation = 0.0f;  
        gm = GameManager.GetInstance();    
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.gameState != GameManager.GameState.GAME) return;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        float y = 0;
        if (!characterController.isGrounded) {
            y = -_gravity;
        }

        // Tratando movimentação do mouse
        float mouse_dX = Input.GetAxis("Mouse X");
        float mouse_dY = Input.GetAxis("Mouse Y");

        // Tratando a rotação da câmera
        cameraRotation += mouse_dY;
        Mathf.Clamp(cameraRotation, -75.0f, 75.0f);

        Vector3 direction = transform.right * x + transform.up * y + transform.forward * z;

        characterController.Move(direction * _baseSpeed * Time.deltaTime);
        transform.Rotate(Vector3.up, mouse_dX);

        playerCamera.transform.localRotation = Quaternion.Euler(cameraRotation, 0.0f, 0.0f);

        if (Input.GetKeyDown(KeyCode.Escape) && gm.gameState == GameManager.GameState.GAME) 
        {
            gm.ChangeState(GameManager.GameState.PAUSE);
        }
    }

    void LateUpdate()
    {
        RaycastHit hit;
        Debug.DrawRay(playerCamera.transform.position, transform.forward*10.0f, Color.magenta);

        if(Physics.Raycast(playerCamera.transform.position, transform.forward, out hit, 100.0f))
        {
            Debug.Log(hit.collider.name);
            if (hit.collider.name == "rust_key_1" || hit.collider.name == "rust_key_2" || hit.collider.name == "rust_key_3") {
                Destroy(hit.collider.gameObject);
                gm.keys++;
                Debug.Log(gm.keys);
            }
            else if (hit.collider.name == "chest" && gm.keys == 3 && gm.gameState == GameManager.GameState.GAME) 
            {
                gm.ChangeState(GameManager.GameState.ENDGAME);
            }
        }
    }

    public void Reset() 
    {
        transform.position = new Vector3(-107.5f, 3.1f, 29.9f);
        transform.localRotation = Quaternion.Euler(0.0f, -transform.rotation.y, 0.0f);
        Instantiate(key1, new Vector3(-2.02f, 8.63f, 12.68f), Quaternion.identity, transform);
        Instantiate(key2, new Vector3(453.69f, 7.22f, -292.53f), Quaternion.identity, transform);
        Instantiate(key3, new Vector3(452.52f, 7.4f, -80.72f), Quaternion.identity, transform);
        Instantiate(chest, new Vector3(147.51f, 10.86f, 4.11f), Quaternion.identity, transform);
    }
}
