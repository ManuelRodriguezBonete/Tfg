using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockSkillPlayerScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] string skillName;
    private GameObject player;
    private Inventory inventory;
    private GameObject cont;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        cont = GameObject.FindGameObjectWithTag("GameController");
        inventory = cont.GetComponent<Inventory>();
    }
    private void OnRenderObject()
    {
        if (skillName == "Dash" && player.GetComponent<PlayerMovement>().UnlockedDash) Destroy(gameObject);
        else if (skillName == "WallJump" && player.GetComponent<PlayerMovement>().UnlockedWallJump) Destroy(gameObject);
        else if (skillName == "WallGrab" && player.GetComponent<PlayerMovement>().UnlockedWallGrab) Destroy(gameObject);
        else if (skillName == "Climbing" && player.GetComponent<PlayerMovement>().UnlockedClimbing) Destroy(gameObject);
        else if (skillName == "BreakItems" && player.GetComponent<PlayerMovement>().UnlockedBreakItems) Destroy(gameObject);
        else if (skillName == "ExtraJump" && player.GetComponent<PlayerMovement>().NExtraJumps == 1) Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            collision.gameObject.GetComponent<PlayerMovement>().UnlockSkill(skillName, false);
            inventory.skillList.Add(skillName);
            Destroy(gameObject);
            inventory.UpdateSkills();
            
        }
    }
}
