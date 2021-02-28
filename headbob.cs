using UnityEngine;

public class headbob : MonoBehaviour {
    private float timer = 0f; 
    float bobbingSpeed = 0.2;
    float bobbingAmount = 0.2; 
    float midpoint = 2.0;
    public float stamina = 100f;
    public bool isMoving = false;
    Animator animator;
    
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        isMoving = false;
        otherTransform = transform;
        position = otherTransform.position;
    }
    void Update() 
    {
        if ( otherTransform.position != lastPosition )
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
        lastPosition = otherTransform.position;
        if (isMoving) 
        {
			stamina -= 5;
		}
        else
        {
            stamina +=5;
        }
        if (animator.GetBool("JumpAnim"))
        {
            HeadBob("y");
        }
        else
        {
            HeadBob("z");
        }
    }
    void HeadBob(string axis)
    {
        if (stamina < 25)
        {
            timer = 0f;
            bobbingSpeed = 0.2;
            bobbingAmount = 0.2; 
            midpoint = 2.0; 
            float waveslice = 0.0; 
            horizontal = Input.GetAxis("Horizontal"); 
            vertical = Input.GetAxis("Vertical"); 
            if (Mathf.Abs(horizontal) == 0 && Mathf.Abs(vertical) == 0) 
            { 
                timer = 0.0; 
            } 
            else 
            { 
                waveslice = Mathf.Sin(timer); 
                timer = timer + bobbingSpeed; 
                if (timer > Mathf.PI * 2) 
                { 
                    timer = timer - (Mathf.PI * 2); 
                } 
            } 
            if (waveslice != 0) 
            { 
                translateChange = waveslice * bobbingAmount; 
                totalAxes = Mathf.Abs(horizontal) + Mathf.Abs(vertical); 
                totalAxes = Mathf.Clamp (totalAxes, 0.0, 1.0); 
                translateChange = totalAxes * translateChange; 
                if (axis == "z")
                {
                    transform.localPosition.z = midpoint + translateChange; 
                }
                else if(axis == "y")
                {
                    transform.localPosition.y = midpoint + translateChange; 
                }
                else if(axis == "x")
                {
                    transform.localPosition.x = midpoint + translateChange; 
                }
            } 
            else 
            {
                if (axis == "z")
                {
                    transform.localPosition.z = midpoint; 
                }
                else if(axis == "y")
                {
                    transform.localPosition.y = midpoint; 
                }
                else if(axis == "x")
                {
                    transform.localPosition.x = midpoint; 
                }
                
            } 
        }
        if (stamina > 50)
        {
            time = 0f;
            bobbingSpeed = 0;
            bobbingAmount = 0; 
            midpoint = 1.0; 
        }
        if (stamina == 100)
        {
            stamina = 100;
        }
        if(stamina == 0)
        {
            stamina = 0; 
        }
    }
}
