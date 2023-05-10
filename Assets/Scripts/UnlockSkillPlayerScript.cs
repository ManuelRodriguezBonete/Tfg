using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockSkillPlayerScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] string skillName;
    [SerializeField] GameObject player;
    [SerializeField] Inventory inventory;

    private bool first = false;
    void Start()
    {
        
    }
    private void OnEnable()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnRenderObject()
    {
        if (skillName == "Dash" && player.GetComponent<PlayerMovement>().UnlockedDash) gameObject.SetActive(false);
        else if (skillName == "WallJump" && player.GetComponent<PlayerMovement>().UnlockedWallJump) gameObject.SetActive(false);
        else if (skillName == "WallGrab" && player.GetComponent<PlayerMovement>().UnlockedWallGrab) gameObject.SetActive(false);
        else if (skillName == "Climbing" && player.GetComponent<PlayerMovement>().UnlockedClimbing) gameObject.SetActive(false);
        else if (skillName == "BreakItems" && player.GetComponent<PlayerMovement>().UnlockedBreakItems) gameObject.SetActive(false);
        else if (skillName == "ExtraJump" && player.GetComponent<PlayerMovement>().NExtraJumps == 1) gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            collision.gameObject.GetComponent<PlayerMovement>().UnlockSkill(skillName, false);
            inventory.skillList.Add(skillName);
            gameObject.SetActive(false);
            inventory.UpdateSkills();
            
        }
    }
}
