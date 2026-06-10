using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private GameObject panel;

    [SerializeField] private TMP_Text skillText1;
    [SerializeField] private TMP_Text skillText2;
    [SerializeField] private TMP_Text skillText3;

    private SkillData skill1;
    private SkillData skill2;
    private SkillData skill3;

    public void OpenLevelUp(SkillData[] skills)
    {
        Time.timeScale = 0f;

        Debug.Log(skills.Length);
        Debug.Log(panel.activeSelf);

        skill1 = skills[Random.Range(0, skills.Length)];
        skill2 = skills[Random.Range(0, skills.Length)];
        skill3 = skills[Random.Range(0, skills.Length)];


        Debug.Log(skillText1);
        Debug.Log(skill1);
        Debug.Log(skill1.skillName);

        skillText1.text = skill1.skillName;
        skillText2.text = skill2.skillName;
        skillText3.text = skill3.skillName;


        Debug.Log(skillText1);
        Debug.Log(skill1.skillName);

        panel.SetActive(true);
    }

    public void SelectSkill(int index)
    {
        SkillData skill = null;

        if (index == 1) skill = skill1;
        if (index == 2) skill = skill2;
        if (index == 3) skill = skill3;

        skill.skillLevel++;

        panel.SetActive(false);

        Time.timeScale = 1f;
    }
}
