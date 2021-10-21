using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;


public class CollideBox : MonoBehaviour
{
    public CinemachineVirtualCamera camCerca;
    public CinemachineVirtualCamera camLejos;
    public CinemachineVirtualCamera camNormal;
    public Dialog dialog;
    private PlayerController pC;

    public VideoPlayer videoMuerte1;
    public VideoPlayer videoMuerte2;
    public VideoPlayer videoMuerte3;
    public VideoPlayer videoFinal;
    public GameObject render;


    private bool jump = false;
    private bool dash = false;
    private bool climb = false;
    private bool glide = false;
    private bool wallJump = false;

    // Comparar los tags según la acción que queremos

    private void Start()
    {
        pC = GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("prueba"))
        {
            // Crear un if por cada acción que queramos e invocar el metodo que queremos: invoke("nombre metodo", int segundos);
            // Crear tags y añadirlo al tag del objeto que tiene colision.
            // Si queremos meter más a escena: Arrastrar el prefab triggers a escena y configurar el tamaño, añadir un tag y ponerselo en el inspector, añadir un if que compruebe ese tag (perfectamente escrito) y crear la función que queremos hacer dentro del if
            Debug.Log("funciono wey");
        }
        else if (collision.CompareTag("lava"))
        {
            pC.Die();
            dialog.dialogLava();
            //Llamar a función animación lava

        }
        else if (collision.CompareTag("spikes"))
        {
            pC.Die();
            dialog.dialogSpikes();
            //Llamar a función animación spikes

        }
        else if (collision.CompareTag("potion"))
        {

            dialog.dialogPotion();
            Destroy(collision.gameObject);

        }
        else if (collision.CompareTag("fall"))
        {
            pC.Die();
            dialog.dialogFall();
            //Llamar a función animación caida

        }

        else if (collision.CompareTag("Enemy"))
        {
            pC.Die();
            dialog.dialogEnemy();
        }
        else if (collision.CompareTag("final"))
        {

            render.SetActive(true);
            videoFinal.gameObject.SetActive(true);
            videoFinal.Play();
            Invoke("videoFinalVuelta", 45f);
            
            FindObjectOfType<AudioManager>().SetVolume("dash", 0.0f);
            //LLamar a video final
            //Llamar a función animación caida

        }
        else if (collision.CompareTag("Camara2"))
        {
            camCerca.Priority = 5;
            camLejos.Priority = 5;
            camNormal.Priority = 10;

        }
        else if (collision.CompareTag("Camara1"))
        {
            camCerca.Priority = 5;
            camLejos.Priority = 10;
            camNormal.Priority = 5;
        }
        else if (collision.CompareTag("Camara3"))
        {
            camCerca.Priority = 10;
            camLejos.Priority = 5;
            camNormal.Priority = 5;
        }
        if (!jump)
        {
            if (collision.CompareTag("UnlockJump"))
            {
                //Video
                render.SetActive(true);
                videoMuerte1.gameObject.SetActive(true);
                videoMuerte1.Play();
                Invoke("pauseVideo1", 7f);
                StartCoroutine(ExplainJump());
            }
        }
        if (!dash)
        {
            if (collision.CompareTag("UnlockDash"))
            {
                //Video
                render.SetActive(true);
                videoMuerte2.gameObject.SetActive(true);
                videoMuerte2.Play();
                Invoke("pauseVideo2", 8f);
                StartCoroutine(ExplainDash());
            }
        }
        if (!climb)
        {
            if (collision.CompareTag("UnlockClimb"))
            {
                StartCoroutine(ExplainClimb());
            }
        }
        if (!glide)
        {
            if (collision.CompareTag("UnlcokGlide"))
            {
                //Video
                render.SetActive(true);
                videoMuerte3.gameObject.SetActive(true);
                videoMuerte3.Play();
                Invoke("pauseVideo3", 13f);
                StartCoroutine(ExplainGlide());
            }
        }
        if (!wallJump)
        {
            if (collision.CompareTag("UnlockWallJump"))
            {
                StartCoroutine(ExplainWallJump());
            }
        }
    }
    private IEnumerator ExplainClimb()
    {
        climb = true;
        yield return new WaitForSeconds(4);
        dialog.dialogUnlockClimb();
        pC.canClimb = true;
    }

    private IEnumerator ExplainJump()
    {
        jump = true;
        yield return new WaitForSeconds(9);
        dialog.dialogUnlockJump();
        pC.canJump = true;
    }

    private IEnumerator ExplainDash()
    {
        dash = true;
        yield return new WaitForSeconds(9);
        dialog.dialogUnlockDash();
        pC.canDash = true;
    }

    private IEnumerator ExplainGlide()
    {
        yield return new WaitForSeconds(19);
        dialog.dialogUnlockGlide();
        pC.canGlide = true;
    }

    private IEnumerator ExplainWallJump()
    {
        wallJump = true;
        yield return new WaitForSeconds(4);
        dialog.dialogUnlockWallClimb();
        pC.canWallJump = true;
    }



    public void pauseVideo1()
    {

        render.SetActive(false);
        videoMuerte1.gameObject.SetActive(false);
        videoMuerte1.Stop();
        
    }

    public void pauseVideo2()
    {

        render.SetActive(false);
        videoMuerte2.gameObject.SetActive(false);
        videoMuerte2.Stop();
    }

    public void pauseVideo3()
    {

        render.SetActive(false);
        videoMuerte3.gameObject.SetActive(false);
        videoMuerte3.Stop();
    }

    public void videoFinalVuelta()
    {

        render.SetActive(false);
        videoFinal.gameObject.SetActive(false);
        videoMuerte3.Stop();
        SceneManager.LoadScene(1);

    }
}
