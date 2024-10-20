using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;
    public Mjolnir_Skill mjolnir_skill;
    // Start is called before the first frame update

    void Awake()
    {
        if (instance != null)
            Destroy(instance);
        else
            instance = this;
    }

    void Start()
    {
        mjolnir_skill = GetComponent<Mjolnir_Skill>();
    }

   
}
