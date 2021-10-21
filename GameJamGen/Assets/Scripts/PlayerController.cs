using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    //Particulas
    public ParticleSystem dust;

    public bool canJump;
    public bool canDash;
    public bool canClimb;
    public bool canGlide;
    public bool canWallJump;

    private Rigidbody2D p_Rigidbody; //Para coger el RigidBody del personaje
    [Range(0, .3f)] [SerializeField] private float p_MovementSmoothing = 0.0f; //Cuanto quieres suavizar el movimiento
    private Vector3 p_Velocity = Vector3.zero; //Un vector cero nos será de ayuda para algunas funciones
    public bool cancelMomentumY;

    //Declaramos las fuerzas que utilizaremos en el movimiento del personaje
    public float p_JumpForce = 400f;
    public float p_GlideSpeed = -10;
    public float p_DashForce = 100f;
    public float p_climbSpeed = 30f;
    public Vector2 p_WallJumpForce = new Vector2(400, 100);

    //Vamos a declarar variables necesarias para la detección del suelo
    [SerializeField] private Transform p_groundCheck; //Una posición de referencia para el suelo
    [SerializeField] private LayerMask whatIsGround; //Seleccionaremos qué es el suelo.
    public float groundedRadius = 0.4f; //Radio en el que se buscará suelo.
    private bool p_grounded; //Variable para guardar si el personaje está en el suelo.

    //Vamos a repetir el proceso para el salto en Pared
    [SerializeField] private Transform p_WallCheck; //Una posición de referencia para las paredes
    public float wallRadius = 0.4f; //Radio de búsqueda
    private bool p_nextToWall; //Variable para guardad si el personaje está cerca de la pared o no
    private bool p_sliding; //Variable que nos dirá si el personajé está colgando o no
    public float p_slideSpeed = -10; //Velocidad de caida cuando se está agarrado
    private bool p_facingRight = true; //Variable que nos dirá hacia donde mira el personaje

    //Repetimos para la mecánica de agarrarse a enredaderas
    [SerializeField] private LayerMask whatIsGrabale;
    //Utilizaremos whatIswall y WallRadius para detectar agarres
    public bool p_hanging;
    public float auxiliaraClmbingForce; //Impulso al terminar de escalar 

    private bool p_wallJumping; //Variable para saber si el jugador esta haciendo un salto en pared


    private bool respawning = false;
    public Transform spawn;//Ubicación del spawn
    private bool alive = true;
    public float spawnSpeed;

    public bool getGrounded()
    {
        return p_grounded;
    }

    public bool getAlive()
    {
        return alive;
    }
    public bool getRespawn()
    {
        return respawning;
    }

    // Start is called before the first frame update
    void Start()
    {
        p_Rigidbody = GetComponent<Rigidbody2D>(); //Guardamos el rigidbody del personaje en la variable
    }

    // Update is called once per frame
    void Update()
    {
        /*if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D)){
            FindObjectOfType<AudioManager>().Play("step");
        } else {
            FindObjectOfType<AudioManager>().Stop("step");
        }*/
    }

    private void FixedUpdate()
    {
        if (alive)
        {
            bool wasGrounded = p_grounded;
            bool wasHanging = p_hanging;
            p_grounded = false;
            p_nextToWall = false;
            p_sliding = false;
            p_wallJumping = false;
            p_hanging = false;
            //Seleccionamos todos los elementos cercanos al punto de groundCheck
            Collider2D[] colliders = Physics2D.OverlapCircleAll(p_groundCheck.position, groundedRadius, whatIsGround);
            //Recorremos cada uno de los elemntos del array para buscar si alguno de estos es suelo
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                {
                    p_grounded = true;
                }
            }
            //Repetimos el proceso para comprobar si estamos cercanos a una pared

            if (canClimb)
            {
                Collider2D[] grabCollidres = Physics2D.OverlapCircleAll(p_WallCheck.position, wallRadius, whatIsGrabale);
                if (grabCollidres.Length > 0)
                {
                    p_hanging = true;
                    p_nextToWall = true;
                }
            }

            if (wasHanging && !p_wallJumping && !p_hanging)
            {
                if (p_facingRight) { auxiliaraClmbingForce = Mathf.Abs(auxiliaraClmbingForce); }
                else { auxiliaraClmbingForce = Mathf.Abs(auxiliaraClmbingForce) * -1; }
                p_Rigidbody.AddForce(new Vector2(auxiliaraClmbingForce, 250));
            }

        }
        else
        {
            bool stop = false;
            if(transform.position.x < spawn.position.x + 1 && transform.position.y > spawn.position.y - 1)
            {
                stop = true;
                respawning = true;
                p_Rigidbody.velocity = Vector2.zero;
                StartCoroutine(Respawn());
            }
            if (!stop)
            {
                respawning = false;
                Vector3 spawnDirection = spawn.position - transform.position;
                p_Rigidbody.velocity = spawnDirection.normalized * spawnSpeed;
            }
        }
    }

    public void Move(float moveMent, bool dash, bool glide, float climb)
    {
        if (!alive){
            return;
        }
        if (!p_hanging)
        {
            Vector3 targetVelocity = new Vector2(moveMent * 10f, p_Rigidbody.velocity.y); //Cañculamos el movimiento
            p_Rigidbody.velocity = Vector3.SmoothDamp(p_Rigidbody.velocity, targetVelocity, ref p_Velocity, p_MovementSmoothing);//Le aplicamos el suavizado de movimiento
    
        }

        if (moveMent == 0 && p_grounded)
        {
            p_Rigidbody.velocity = new Vector2(0, p_Rigidbody.velocity.y);
        }
        

        if (canGlide)
        {
            if (glide && !p_grounded)
            {
                if (p_Rigidbody.velocity.y <= 0)
                {
                    p_Rigidbody.velocity = new Vector2(p_Rigidbody.velocity.x, p_GlideSpeed);
                }
            }
        }

        if (canDash)
        {
            if (dash && !glide)
            {
                if (!p_facingRight) { p_DashForce = Mathf.Abs(p_DashForce) * -1; }
                else { p_DashForce = Mathf.Abs(p_DashForce); }
                //p_Rigidbody.AddForce(new Vector2(p_DashForce, 0));
                p_Rigidbody.velocity = new Vector2(p_DashForce, p_Rigidbody.velocity.y);
            }
        }
        
        if (!p_grounded && p_hanging)
        {
            if(p_Rigidbody.velocity.y <= 0)
            {
                p_Rigidbody.velocity = new Vector2(0, p_slideSpeed);
            }
            p_sliding = true;
        }

        if(!p_grounded && p_hanging)
        {
            if(climb > 0)
            {
                p_Rigidbody.velocity = new Vector2(0, p_climbSpeed);
            }
            else if(climb < 0)
            {
                p_Rigidbody.velocity = new Vector2(0, -p_climbSpeed);
            }
            else { p_Rigidbody.velocity = new Vector2(0, 0); }            
        }

        if (!p_sliding)
        {
            //Si el jugador no está mirando hacia donde le corresponde se gira
            if (moveMent > 0 && !p_facingRight) { Flip(); }
            else if (moveMent < 0 && p_facingRight) { Flip(); }
        }
    }

    public void Jump(bool jump)
    {

        if (!alive)
        {
            return;
        }

        if (canJump)
        {
            if (jump && p_grounded)
            {
                p_Rigidbody.AddForce(new Vector2(0, p_JumpForce), ForceMode2D.Force);
            }

            //Mecánica para salto en pared
            if (canWallJump)
            {
                if (jump && p_sliding)
                {
                    p_hanging = false;
                    p_wallJumping = true;
                    //Para la mecánica de salto en pared tenemos que cuidar que tener en cuenta hacia donde esta
                    //mirando el personaje para aplicar las fuerzas en sentido contrario
                    float wJX = p_WallJumpForce.x;
                    if (p_facingRight)
                    {
                        wJX = Mathf.Abs(wJX) * -1;
                        p_WallJumpForce.x = wJX;
                    }
                    else
                    {
                        wJX = Mathf.Abs(wJX);
                        p_WallJumpForce.x = wJX;
                    }
                    //Cancelamos el momentum vertical para evitar "supersaltos"
                    if (p_Rigidbody.velocity.y > 0 && cancelMomentumY)
                    {
                        p_Rigidbody.velocity = new Vector2(0, 0);
                    }
                    p_Rigidbody.AddForce(p_WallJumpForce);
                    Flip();
                }
            }
        }
       
    }
    
    //Ahora introduciremos un método que nos mantenga informados sobre hacia donde está mirando el personaje
    private void Flip()
    {
        p_facingRight = !p_facingRight;
        
        //Rotamos el cuerpo del personaje cambiando su escala en X
        Vector3 newscale = transform.localScale;
        newscale.x *= -1;
        transform.localScale = newscale;

        //Rotamos la trasform de WallCheck para que cubra el mismo área a ambos lados
        p_WallCheck.Rotate(new Vector3(0, 180, 0));

        CreateDust();
    }



    //Particulas
    void CreateDust()
    {
        dust.Play();
    }

    public void Die()
    {
        alive = false;
        GetComponent<CapsuleCollider2D>().enabled = false;
        p_Rigidbody.gravityScale = 0;
    }

    public IEnumerator Respawn()
    {
        yield return new WaitForSeconds(2);
        GetComponent<CapsuleCollider2D>().enabled = true;
        p_Rigidbody.gravityScale = 3;
        alive = true;
    }
}