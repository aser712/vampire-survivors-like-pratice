using NUnit.Framework;
using UnityEngine;

public class _SkillManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject bladePrefab;

    public SkillData[] skillDatas;


    private void Awake()
    {
        skillDatas = Resources.LoadAll<SkillData>("Scripts/Skill/Data");
        skillDatas[0].skillLevel = 3;
    }

    private void Start()
    {
        UseBladeSkill();
    }
    void SkillLevelUP(SkillData skill)
    {
        skill.skillLevel++;
    }

    public void UseBladeSkill()
    {
        int i;

        for(i = 0; i < skillDatas.Length; i++)
        {
            if (skillDatas[i].skillName == "SpinBlade")
            {
                break;
            }
        }

        int bladeInterval = 360 / skillDatas[i].skillLevel;

        for (int j = 0; j < skillDatas[i].skillLevel; j++)
        {
            float angle = bladeInterval * j;

            GameObject blade = Instantiate(
                bladePrefab,
                transform.position,
                Quaternion.Euler(0, 0, angle)
            );

            blade.GetComponent<SpinBlade>().Init(transform, angle);

        }



    }
}
