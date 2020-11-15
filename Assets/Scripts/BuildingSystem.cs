using UnityEngine;
using UnityEngine.UI;

public class BuildingSystem : MonoBehaviour
{
    [SerializeField]
    private GameObject toolbar;
    private int selectedSlot = 0;

    [SerializeField]
    private CharacterController characterController;
    private GameObject characterCamera;

    [SerializeField]
    private Camera playerCamera;

    private bool buildModeOn = false;
    private bool canBuild = false;

    private BlockSystem blockSystem;

    [SerializeField]
    private LayerMask buildableSurfacesLayer;

    private Vector3 buildPos;

    private GameObject currentTemplateBlock;

    [SerializeField]
    private GameObject blockTemplatePrefab;
    [SerializeField]
    private GameObject blockPrefab;

    [SerializeField]
    private Material templateMaterial;

    private int blockSelectCounter = 0;

    private void Start()
    {
        blockSystem = GetComponent<BlockSystem>();
        
        // Set to start game with the first slot
        HandleSlotChange(0);

        // get camera
        characterCamera = characterController.transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        // change to pick blocks with keys (1-9) or scroll
        if (Input.GetKeyDown("b"))
            buildModeOn = !buildModeOn;

        if (Input.GetAxis("Mouse ScrollWheel") < 0f) // forward
        {
            if(selectedSlot < 8)
                HandleSlotChange(selectedSlot+1);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") > 0f) // backwards
        {
            if (selectedSlot > 0)
                HandleSlotChange(selectedSlot - 1);
        }


        if (Input.GetKeyDown("1")) HandleSlotChange(0);
        if (Input.GetKeyDown("2")) HandleSlotChange(1);
        if (Input.GetKeyDown("3")) HandleSlotChange(2);
        if (Input.GetKeyDown("4")) HandleSlotChange(3);
        if (Input.GetKeyDown("5")) HandleSlotChange(4);
        if (Input.GetKeyDown("6")) HandleSlotChange(5);
        if (Input.GetKeyDown("7")) HandleSlotChange(6);
        if (Input.GetKeyDown("8")) HandleSlotChange(7);
        if (Input.GetKeyDown("9")) HandleSlotChange(8);

        if (buildModeOn)
        {
            RaycastHit buildPosHit;

            if (Physics.Raycast(playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0)), out buildPosHit, 8, buildableSurfacesLayer))
            {
                Vector3 point = buildPosHit.point;

                float xOffset;
                float zOffset;
                float xCameraOffset;

                // .02f, so you can place blocks in corners
                if (characterController.transform.rotation.eulerAngles.y > 0 && characterController.transform.rotation.eulerAngles.y < 180)
                    xOffset = -.02f;
                else
                    xOffset = .02f;

                if (characterController.transform.rotation.eulerAngles.y > 270 || characterController.transform.rotation.eulerAngles.y < 90)
                    zOffset = -.02f;
                else
                    zOffset = .02f;

                if (characterCamera.transform.rotation.eulerAngles.x > 270)
                    xCameraOffset = -.02f;
                else
                    xCameraOffset = .02f;

                buildPos = new Vector3(Mathf.Round(point.x + xOffset), Mathf.Round(point.y + xCameraOffset), Mathf.Round(point.z + zOffset));
                canBuild = true;


                if (Input.GetMouseButtonDown(1))
                    Destroy(buildPosHit.transform.gameObject);
            }
            else
            {
                if (currentTemplateBlock != null)
                    Destroy(currentTemplateBlock.gameObject);
                    canBuild = false;
            }
        }


        if (!buildModeOn && currentTemplateBlock != null)
        {
            Destroy(currentTemplateBlock.gameObject);
            canBuild = false;
        }

        if (canBuild && currentTemplateBlock == null)
        {
            currentTemplateBlock = Instantiate(blockTemplatePrefab, buildPos, Quaternion.identity);
            currentTemplateBlock.GetComponent<MeshRenderer>().material = templateMaterial;
        }

        if (canBuild && currentTemplateBlock != null)
        {
            currentTemplateBlock.transform.position = buildPos;

            if (Input.GetMouseButtonDown(0))
                PlaceBlock();
        }
    }

    private void HandleSlotChange(int newSelection)
    {
        blockSelectCounter = newSelection;

        GameObject oldSlot = toolbar.transform.GetChild(selectedSlot).gameObject;
        oldSlot.GetComponent<Image>().color = new Color32(0, 0, 0, 255);

        GameObject newSlot = toolbar.transform.GetChild(newSelection).gameObject;
        newSlot.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        selectedSlot = newSelection;
    }

    private void PlaceBlock()
    {
        GameObject newBlock = Instantiate(blockPrefab, buildPos, Quaternion.identity);
        Block tempBlock = blockSystem.allBlocks[blockSelectCounter];
        newBlock.name = tempBlock.blockName + "-Block";
        newBlock.GetComponent<MeshRenderer>().material = tempBlock.blockMaterial;
    }
}