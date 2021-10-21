using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public Animator animator;

    public PlayerController controller; //Añadimos el controlador del jugador que es el que se encargará del movimiento
    public float speed = 100; //Variable que controlará la velocidad del personaje


    private float horizontal; //Varaiable que guardará la cantidad de movimiento horizontal
    public float vertical; //Variable que guaradará la cantidad de movimiento vertical
    private bool jump; //Variable que nos dirá si hay que saltar o no
    private bool glide; //Variable que nos dirá si hay que planear o no
    private bool dash; //Variable que nos dirá si hau que dashear o no


    private bool ground;

    //Variables para controlar la frecuencia del dash
    private float nextDash = 0f;
    private float dashRate = 1f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
            glide = true;

            if (controller.canJump)
            {
                animator.SetBool("jumping", true);
                Invoke("jumpFinished", 0.05f);
                animator.speed = 1;
            }
            animator.speed = 1;

        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            animator.speed = 1;
            glide = false;
        }
        //En el caso del dash no podemos permitir que esté todo el rato activo por lo que
        //limitaremos su uso a X veces por segundo
        if(Time.time >= nextDash)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                dash = true;
                nextDash = Time.time + 1f / dashRate;
                if (controller.canDash)
                {
                    FindObjectOfType<AudioManager>().Play("dash");
                }
            }
        }
        //*************Animaciones********************//
        //Controla si el personaje se mueve
        animator.SetFloat("speed", Mathf.Abs(horizontal));

        //Controla si el jugador ha tocado el suelo
        if(controller.getGrounded())
        {
            animator.SetBool("suelo", true);
        }
        else
        {
            animator.SetBool("suelo", false);
        }

        //Controla si el jugador está agarrado a la pared
        if (controller.p_hanging)
        {
            animator.SetBool("pared", true);
            
            if(vertical < 1)
            {
                animator.speed = 0;
            }
            else
            {
                animator.speed = 1;
            }
        }
        else
        {
            animator.SetBool("pared", false);
        }


        if (controller.getAlive())
        {
            animator.SetBool("Vivo", true);
        }
        else
        {
            animator.SetBool("Vivo", false);
        }

        if (controller.getRespawn())
        {
            animator.SetBool("Respawn", true);
        }
        else
        {
            animator.SetBool("Respawn", false);
        }

    }

    private void FixedUpdate()
    {
        horizontal = Input.GetAxisRaw("Horizontal") * speed; //Se calcula el movimiento 
        vertical = Input.GetAxisRaw("Vertical");
        controller.Move(horizontal * Time.deltaTime, dash, glide, vertical); //Se llama al método del controlador de personaje que se encargue del movimiento
        controller.Jump(jump);
        jump = false;
        dash = false;

    }


    private void jumpFinished()
    {
        animator.SetBool("jumping", false);  
    }
}
