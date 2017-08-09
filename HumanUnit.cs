using UnityEngine;
using System.Collections;

public class HumanUnit : MonoBehaviour {

    public Material Ghost;
    public Material GhostNone;
    public Material Normal;
    public Material Selected;
    public Material SelectedWeapon;
    public Material SelectedBeard;
    public Material SelectedHair;
    public Material SelectedBag;
    public Material Weapon;
    public Material Beard;
    public Material Hair;
    public Material Bag;

    string tagCreate = "CreateUnit";
    string tagComplete = "CompleteUnit";

    public BoxCollider bc1;
    public BoxCollider bc2;

    public GameObject NPC;
    public GameObject weapon_01;
    public GameObject weapon_02;
    public GameObject beard;
    public GameObject hair;
    public GameObject bag;
	
	
	void Update ()
    {
	    if((gameObject.tag == tagCreate) && (GlobalVar.onTriggerUnit == false))
        {
            
            NPC.GetComponentInChildren<SkinnedMeshRenderer>().material = Ghost;
            weapon_01.GetComponent<MeshRenderer>().material = Ghost;
            weapon_02.GetComponent<MeshRenderer>().material = Ghost;
            beard.GetComponent<MeshRenderer>().material = Ghost;
            hair.GetComponent<MeshRenderer>().material = Ghost;
            bag.GetComponent<SkinnedMeshRenderer>().material = Ghost;
        }

        if ((gameObject.tag == tagCreate) && (GlobalVar.onTriggerUnit == true))
        {
            NPC.GetComponent<SkinnedMeshRenderer>().material = GhostNone;
            weapon_01.GetComponent<MeshRenderer>().material = GhostNone;
            weapon_02.GetComponent<MeshRenderer>().material = GhostNone;
            beard.GetComponent<MeshRenderer>().material = GhostNone;
            hair.GetComponent<MeshRenderer>().material = GhostNone;
            bag.GetComponent<SkinnedMeshRenderer>().material = GhostNone;
        }

        if (GlobalVar.activeCreateUnit == false)
        {
            gameObject.tag = tagComplete;
            bc1.enabled = true;
            bc2.enabled = false;
            bc2.isTrigger = false;
            //Destroy(gameObject.GetComponent<Rigidbody>());
            UnitIDSignaliz();
        }

        if (GlobalVar.activeCreateUnit == false)
        {
            if ((gameObject.GetComponent<UnitOptions>().selected == true) && (gameObject.tag == tagComplete))
            {
                NPC.GetComponent<SkinnedMeshRenderer>().material = Selected;
                weapon_01.GetComponent<MeshRenderer>().material = SelectedWeapon;
                weapon_02.GetComponent<MeshRenderer>().material = SelectedWeapon;
                beard.GetComponent<MeshRenderer>().material = SelectedBeard;
                hair.GetComponent<MeshRenderer>().material = SelectedHair;
                bag.GetComponent<SkinnedMeshRenderer>().material = SelectedBag;
            }
        }
        
        if((gameObject.GetComponent<UnitOptions>().selected == false) && (gameObject.tag == tagComplete))
        {
            NPC.GetComponent<SkinnedMeshRenderer>().material = Normal;
            weapon_01.GetComponent<MeshRenderer>().material = Weapon;
            weapon_02.GetComponent<MeshRenderer>().material = Weapon;
            beard.GetComponent<MeshRenderer>().material = Beard;
            hair.GetComponent<MeshRenderer>().material = Hair;
            bag.GetComponent<SkinnedMeshRenderer>().material = Bag;
        }
    }

    void OnTriggerEnter()
    {
        GlobalVar.onTriggerUnit = true;
    }

    void OnTriggerExit()
    {
        GlobalVar.onTriggerUnit = false;
    }

    void UnitIDSignaliz()
    {
        for (int i = 0; i < GlobalVar.UnitID.Length; i++)
        {
            if ((GlobalVar.UnitID[i] == 0) && (gameObject.GetComponent<UnitOptions>().ID == 0))
            {
                GlobalVar.UnitID[i] = i + 1;
                gameObject.GetComponent<UnitOptions>().ID = i + 1;
            }
        }
    }
}



            //NPC.GetComponent<SkinnedMeshRenderer>().material = Normal;
            //weapon_01.GetComponent<MeshRenderer>().material = Weapon;
            //weapon_02.GetComponent<MeshRenderer>().material = Weapon;
            //beard.GetComponent<MeshRenderer>().material = Beard;
            //hair.GetComponent<MeshRenderer>().material = Hair;
            //bag.GetComponent<SkinnedMeshRenderer>().material = Bag;