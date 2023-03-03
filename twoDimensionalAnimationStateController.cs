using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class twoDimensionalAnimationStateController : MonoBehaviour
{
    Animator animator;
    float velocityZ = 0.0f;
    float velocityX = 0.0f;
    
    public float acceleration = 2.0f;
    public float deceleration = 2.0f;
    public float maximumWalkVelocity = 0.5f;
    public float maximumRunVelocity = 2.0f;

    //increse performance
    int VelocityZHash;
    int VelocityXHash;

    // Start is called before the first frame update
    void Start()
    {
        //ค้นหา gameobject คริปนี้ที่ใช้งารร่วม และ เรียกใช้ animator component
        animator = GetComponent<Animator>();

        //increase performance
        VelocityZHash = Animator.StringToHash("Velocity z");
        VelocityXHash = Animator.StringToHash("Velocity x");
    }
    //handles acceleration and deceleration
    void changeVelocity(bool forwardPressed, bool leftPressed, bool rightPressed, bool runPressed, bool backwardPressed, float currentMaxVelocity)
    {
        //ถ้า player กด forward, เพิ่ม velocity ในค่า z
        if (forwardPressed && velocityZ < currentMaxVelocity)
        {
            velocityZ += Time.deltaTime * acceleration;
        }

        if (backwardPressed && velocityZ > -currentMaxVelocity)
        {
            velocityZ -= Time.deltaTime * acceleration;
        }

        // เพิ่ม velocity ทางซ้าย
        if (leftPressed && velocityX > -currentMaxVelocity)
        {
            velocityX -= Time.deltaTime * acceleration;
        }

        // เพิ่ม velocity ทางขวา
        if (rightPressed && velocityX < currentMaxVelocity)
        {
            velocityX += Time.deltaTime * acceleration;
        }

        //ลดค่า velocityZ
        if (!forwardPressed && velocityZ > 0.0f)
        {
            velocityZ -= Time.deltaTime * deceleration;
        }

        //เพิ่มค่า velocityZ
        if (!backwardPressed && velocityZ < 0.0f)
        {
            velocityZ += Time.deltaTime * deceleration;
        }

        //เพิ่มค่า velocityX ถ้า ไม่ได้กดซ้าย และ VelocityX < 0
        if (!leftPressed && velocityX < 0.0f)
        {
            velocityX += Time.deltaTime * deceleration;
        }

        //ลดค่า velocityX ถ้า ไม่ได้กดขวา และ VelocityX > 0
        if (!rightPressed && velocityX > 0.0f)
        {
            velocityX -= Time.deltaTime * deceleration;
        }
    }

    void lockOrResetVelocity(bool forwardPressed, bool leftPressed, bool rightPressed, bool runPressed, bool backwardPressed, float currentMaxVelocity)
    {
        //reset velocityZ
        if (!forwardPressed && !backwardPressed && velocityZ != 0.0f && (velocityZ > -0.05f && velocityZ < 0.05f))
        {
            velocityZ = 0.0f;
        }

        //reset velocityX
        if (!leftPressed && !rightPressed && velocityX != 0.0f && (velocityX > -0.05f && velocityX < 0.05f))
        {
            velocityX = 0.0f;
        }

        //lock forward
        if (forwardPressed && runPressed && velocityZ > currentMaxVelocity)
        {
            velocityZ = currentMaxVelocity;
        }
        else if (forwardPressed && velocityZ > currentMaxVelocity)
        {
            //ปัด currentMaxVelocity ถ้าอยู่ในช่วงนั้น
            velocityZ -= Time.deltaTime * deceleration;
            if (velocityZ > currentMaxVelocity && velocityZ < (currentMaxVelocity + 0.05))
            {
                velocityZ = currentMaxVelocity;
            }
        }
        else if (forwardPressed && velocityZ < currentMaxVelocity && velocityZ > (currentMaxVelocity - 0.05f))
        {
            velocityZ = currentMaxVelocity;
        }
        //========================================================
        //lock backward
        if (backwardPressed && runPressed && velocityZ < -currentMaxVelocity)
        {
            velocityZ = -currentMaxVelocity;
        }
        else if (forwardPressed && velocityZ < -currentMaxVelocity)
        {
            //ปัด currentMaxVelocity ถ้าอยู่ในช่วงนั้น
            velocityZ += Time.deltaTime * deceleration;
            if (velocityZ < -currentMaxVelocity && velocityZ > (-currentMaxVelocity + 0.05))
            {
                velocityZ = -currentMaxVelocity;
            }
        }
        else if (forwardPressed && velocityZ > -currentMaxVelocity && velocityZ < (-currentMaxVelocity - 0.05f))
        {
            velocityZ = -currentMaxVelocity;
        }

        //========================================================
        //lock left
        if (leftPressed && runPressed && velocityX < -currentMaxVelocity)
        {
            velocityX = -currentMaxVelocity;
        }
        else if (leftPressed && velocityX < -currentMaxVelocity)
        {
            //ปัด currentMaxVelocity ถ้าอยู่ในช่วงนั้น
            velocityZ += Time.deltaTime * deceleration;
            if (velocityX < -currentMaxVelocity && velocityX > (-currentMaxVelocity - 0.05))
            {
                velocityX = -currentMaxVelocity;
            }
        }
        else if (leftPressed && velocityX > -currentMaxVelocity && velocityX < (-currentMaxVelocity + 0.05f))
        {
            velocityX = -currentMaxVelocity;
        }

        //========================================================
        //lock right
        if (rightPressed && runPressed && velocityX > currentMaxVelocity)
        {
            velocityX = currentMaxVelocity;
        }
        //ลดค่า เหลือ velocity velocity
        else if (rightPressed && velocityX > currentMaxVelocity)
        {
            //ปัด currentMaxVelocity ถ้าอยู่ในช่วงนั้น
            velocityZ -= Time.deltaTime * deceleration;
            if (velocityX > currentMaxVelocity && velocityX < (currentMaxVelocity + 0.05))
            {
                velocityX = currentMaxVelocity;
            }
        }
        else if (rightPressed && velocityX < currentMaxVelocity && velocityX > (currentMaxVelocity - 0.05f))
        {
            velocityX = currentMaxVelocity;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //รับค่าจาก player
        //input จะเป้นจริงเสมอ ถ้า ผู้เล่นกดลงผ่าน key parameter
        bool forwardPressed = Input.GetKey(KeyCode.W);
        bool leftPressed = Input.GetKey(KeyCode.A);
        bool rightPressed = Input.GetKey(KeyCode.D);
        bool backwardPressed = Input.GetKey(KeyCode.S);
        bool runPressed = Input.GetKey(KeyCode.LeftShift);

        //set current maxVelocity
        float currentMaxVelocity = runPressed ? maximumRunVelocity : maximumWalkVelocity;

        changeVelocity(forwardPressed, leftPressed, rightPressed, runPressed, backwardPressed, currentMaxVelocity);
        lockOrResetVelocity(forwardPressed, leftPressed, rightPressed, runPressed, backwardPressed, currentMaxVelocity);

        //set ค่า parameter ไปที่ ตัวแปร local
        animator.SetFloat(VelocityZHash, velocityZ);
        animator.SetFloat(VelocityXHash, velocityX);
    }
}
