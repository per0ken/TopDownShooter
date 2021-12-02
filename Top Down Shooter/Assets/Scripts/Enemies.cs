using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Enemies : MonoBehaviour
{
    public Rigidbody2D rb;
    private GameObject Player;
    private PlayerController playerController;

    /* private float movementDuration = 2.0f;
     private float waitBeforeMoving = 2.0f;
     private bool Arrived = false;*/

    //private float speed = 5.0f;
    // private float waitTime;
    // public float startWaitTime = 0.75f;

    //public Vector2 moveSpot;
    Vector3 deadPosition;
    protected bool dead = false;
    public float minX = -9.5f;
    public float maxX = 9.5f;
    public float minY = -4.6f;
    public float maxY = 3.5f;
    private float timer = 0.0f;

    protected bool canExplode = true;
    protected bool canPlaySound = true;
    protected bool canMove = true;
    protected bool canLook = true;
    protected bool canShoot = true;
    //a boolok azért vannak hogy egyszer tudjon animációt lejátszani a halott enemy és ne tudjon mozogni illetve a playerre nézni, 1 boollal nem engedte

    BoxCollider2D ObjectCollider; //bemegyek a mapba, utána megakad a dolgokba ha MoveTowards alapú lesz a mozgása

    private Animator mAnimator; //robbanásanimáció

    private RuntimeAnimatorController soldierExplodeController;

    private Vector3 myLatestNewPosition;

    private NavMeshAgent navMeshAgent;

    public float enemyLives = 3;

    void OnEnable()
    {
        Player = GameObject.Find("Player");
        playerController = Player?.GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
        soldierExplodeController = Resources.Load<RuntimeAnimatorController>("MySoldierExplodeAnim");
        ObjectCollider = GetComponent<BoxCollider2D>();
        mAnimator = GetComponent<Animator>(); //getanimator
        RuntimeAnimatorController thisController = Instantiate(soldierExplodeController);
        //waitTime = startWaitTime;
        //moveSpot = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        if (thisController != null)
            mAnimator.runtimeAnimatorController = thisController;
        //a fenti sor ráállytja az animatior component animator controllerjére a SoldierExplode7-et
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.Warp(transform.position);
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
        ManageMovement();
    }
    
    void Update()
    {
        if (dead == true)
            transform.position = deadPosition;

        if (canMove == true)
        {
            timer += Time.deltaTime;
            if (timer > 3)
            {
                ManageMovement();
                timer = 0;
            }
            // {
            //    ManageMovement();
            //}
        }
    }
    
    void ManageMovement()
    {
        myLatestNewPosition = GetRandomLocationNearPlayer();
        navMeshAgent.SetDestination(myLatestNewPosition);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Bullet")) enemyLives--;
        if(enemyLives>0)
            if (collider.gameObject.CompareTag("Bullet")) Destroy(collider.gameObject);
        if (enemyLives == 0)
        {
            dead = true;
            deadPosition = transform.position;
            playerController.enemyKilled.Invoke(collider.GetHashCode());
            SoundEffect(); //robbanáshang
            Explosion(); //robbanásanimáció
            Destroy(this.gameObject,1.5f); //törli a szétrobbant objectet 1.5 mp után, de elõtte még mozog és újra lejátssza a robbanáshangot és az animációt is pedig nem lövök rá
            canMove = false;
            canLook = false;
            canShoot = false;
            //if(ObjectCollider != null)
                //ObjectCollider.isTrigger = true;
        }
    }
    
   /* private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bush"))
            ObjectCollider.isTrigger = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bush"))
            ObjectCollider.isTrigger = false;
    }*/
    /*
    private void Update()
    {
        /*if (!Arrived)
        {
            Arrived = true;
            int randomX = Random.Range(-10, 10);
            int randomY = Random.Range(4, -5);
            StartCoroutine(MoveToPoint(new Vector2(randomX,randomY)));
        }*/

        /*if (canMove == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, moveSpot, speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, moveSpot) < 0.6f || // nem kap új irányt hiába megy közel a kövekhez
                Vector2.Distance(transform.position, rockThree.Three.position) < 0.8f ||
                Vector2.Distance(transform.position, rockTwo.Two.position) < 0.8f ||
                Vector2.Distance(transform.position, rockOne.One.position) < 0.8f)
            {
                if (waitTime <= 0)
                {
                    moveSpot = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
                    waitTime = startWaitTime;
                }
                else
                {
                    waitTime -= Time.deltaTime;
                }
            }
        }

    }*/

    void FixedUpdate()
    {
        if (Player == null)
            return;

        Vector2 Playerr = Player.transform.position;
        Vector2 lookDir = Playerr - rb.position;
        if (canLook == true)
        {
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
            rb.rotation = angle;
        }
    }

   /* private IEnumerator MoveToPoint(Vector2 targetPos)
    {
        float timer = 0.0f;
        Vector3 startPos = transform.position;

        if (canMove == true)
        {
            while (timer < movementDuration)
            {
                timer += Time.deltaTime;
                float t = timer / movementDuration;
                t = t * t * t * (t * (6f * t - 15f) + 10f);
                transform.position = Vector3.Lerp(startPos, targetPos, t);

                yield return null;
            }
        }

        yield return new WaitForSeconds(waitBeforeMoving);
        Arrived = false;
    }*/

    void Explosion()
    {
        if (mAnimator == null)
            return;

        if (canExplode == true)
        {
            mAnimator.SetTrigger("TrExplode");
            canExplode = false;
        }

    }

    void SoundEffect()
    {
        if (canPlaySound == true)
        {
            SoundController.enemyDied.Invoke();
            canPlaySound = false;
        }
    }

    Vector3 GetRandomLocationNearPlayer() => new Vector3(Random.Range(minX,maxX), Random.Range(minY,maxY) + Random.Range(0,0));
}
