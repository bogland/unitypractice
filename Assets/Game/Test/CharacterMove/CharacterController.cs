using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    Joystick joyStick;
    CharacterMove character;
    // Start is called before the first frame update
    void Start()
    {
        joyStick = GameObject.FindObjectOfType<Joystick>();
        character = GameObject.FindObjectOfType<CharacterMove>();
    }
    // Update is called once per frame
    void Update()
    {
        character.Move(joyStick.Direction);
    }
}
