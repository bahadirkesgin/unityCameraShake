using UnityEngine;

public class cameraShake : MonoBehaviour {
    
    private float timer = 0f; // zaman
    float bobbingSpeed = 0.2f; // bob şiddeti
    float bobbingAmount = 0.2f; // bob sayısı
    float xBobbingAmount = 0.5f; // x axis bobbing
    float midpoint = 2.0f; // orta nokta
    public float stamina = 100f;
    public bool isMoving = false;
    public GameObject cam;
    
    Animator animator;
    Transform transformCam;
    public Vector3 position;
    Vector3 lastPosition;
    
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        transformCam = gameObject.GetComponent<Transform>();
        isMoving = false;
        position = transformCam.position;
    }
    void Update() 
    {
        if ( transformCam.position != lastPosition )
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
        lastPosition = transformCam.position;
        if (isMoving) 
        {
			stamina -= 5f;
		}
        else
        {
            stamina += 5f;
        }
        if (animator.GetBool("JumpAnim")) // Anim adi degisecek
        {
            HeadBob("y", bobbingAmount);
        }
        else
        {
            HeadBob("z", bobbingAmount);
            HeadBob("x", xBobbingAmount);
        }
    }

    void HeadBob(string axis, float bobbingConst)
    {
        if (stamina < 25)
        {
            timer = 0f;
            bobbingSpeed = 0.2f;
            bobbingConst = 0.2f; 
            midpoint = 2.0f; 
            float waweConst = 0.0f; 
            float horizontal = Input.GetAxis("Horizontal"); 
            float vertical = Input.GetAxis("Vertical"); 
            if (Mathf.Abs(horizontal) == 0 && Mathf.Abs(vertical) == 0) 
            { 
                timer = 0.0f; 
            } 
            else 
            { 
                waweConst = Mathf.Sin(timer); 
                timer = timer + bobbingSpeed; 
                if (timer > Mathf.PI * 2.0f) 
                { 
                    timer = timer - (Mathf.PI * 2.0f); 
                } 
            } 
            if (waweConst != 0) 
            { 
                float translateChange = waweConst * bobbingConst; 
                float totalAxes = Mathf.Abs(horizontal) + Mathf.Abs(vertical); 
                totalAxes = Mathf.Clamp (totalAxes, 0f, 1f); 
                translateChange = totalAxes * translateChange;
                float total = midpoint + translateChange; 
                if (axis == "z")
                {
                    cam.transform.rotation = Quaternion.Euler(0f, 0f, total); 
                }
                else if(axis == "y")
                {
                    cam.transform.rotation = Quaternion.Euler(0f, total, 0f); 
                }
                else if(axis == "x")
                {
                    cam.transform.rotation = Quaternion.Euler(total, 0f, 0f);  
                }
            } 
            else 
            {
                if (axis == "z")
                {
                    cam.transform.rotation = Quaternion.Euler(0f, 0f, midpoint); 
                }
                else if(axis == "y")
                {
                    cam.transform.rotation = Quaternion.Euler(0f,  midpoint, 0f);  
                }
                else if(axis == "x")
                {
                    cam.transform.rotation = Quaternion.Euler(midpoint, 0f, 0f); 
                }
                
            } 
        }
        if (stamina > 50)
        {
            timer = 0f;
            bobbingSpeed = 0f;
            bobbingConst = 0f; 
            midpoint = 1.0f; 
        }
        if (stamina == 100)
        {
            stamina = 100f;
        }
        if(stamina == 0)
        {
            stamina = 0f; 
        }
    }
}
