using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBoolDolly : MonoBehaviour
{
    [SerializeField] private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetBoolAnimation()
    {
        anim.SetBool("DollyEnabled", !anim.GetBool("DollyEnabled"));
    }
    
    public void SetBoolAnimation(bool b)
    {
        anim.SetBool("DollyEnabled", b);
    }
}
