using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Pawn
{
    public class SpawnPawn : MonoBehaviour
    {
        [SerializeField] private GameObject[] pawnsToSpawn;
        [SerializeField] private GameObject[] selectionMarks; // gameobject to spawn when clicked
        [SerializeField] private GameObject selectionMark = null;
        private Ray _myRay;      // initializing the ray
        private RaycastHit _hit; // initializing the raycast hit
        private GameObject visual = null;
        private GameObject activeMark = null;
        private int currentPawnIndex;
        void Update ()
        {
            if (visual != null)
            {
                if (Camera.main != null)
                    _myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            
                if (Physics.Raycast (_myRay, out _hit)) //if myRay hits something, store all the info you can find in the raycasthit varible.
                {
                    visual.transform.position = RoundHitPoint(_hit.point);
                    activeMark.transform.position = RoundHitPoint(_hit.point);
                } 
            }
            SpawnThePawn(currentPawnIndex);
        }

        public void SpawnThePawn(int pawnIndex)
        {
            
            if (Camera.main != null)
                _myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            // my main camera to my mouse (This will give me a direction)

            if (Physics.Raycast (_myRay, out _hit)) //if myRay hits something, store all the info you can find in the raycasthit varible.
            {
                if ( PlayBoard.Board.GetValue(_hit.point) == 0 && Input.GetMouseButtonDown(0) && visual) {// what to do if i press the left mouse button
                    Destroy(visual);
                    Destroy(activeMark);
                    ResetSelectionMarks();
                    Instantiate (pawnsToSpawn[pawnIndex], RoundHitPoint(_hit.point), Quaternion.identity);// instatiate a prefab on the position where the ray hits the floor.
                    PlayBoard.Board.SetValue(RoundHitPoint(_hit.point),1);
                    Debug.Log (_hit.point);// debugs the vector3 of the position where I clicked
                }
            }  
        }


        private Vector3 RoundHitPoint(Vector3 hitPoint)
        {
            return new Vector3(Mathf.Round(hitPoint.x), 0, Mathf.Round(hitPoint.z));
        }
        public void MouseFollowPawn(int pawnIndex)
        {
            if (GoldControl(pawnsToSpawn[pawnIndex]))
            {
                currentPawnIndex = pawnIndex;
                selectionMarks[pawnIndex].SetActive(true);
                visual = Instantiate(pawnsToSpawn[pawnIndex].GetComponent<Pawn>().pawn.visual,transform.position,Quaternion.identity) as GameObject;
                activeMark = Instantiate(selectionMark, transform.position, Quaternion.identity) as GameObject;
                activeMark.transform.Rotate(Vector3.right,-90f);
            }
            else
            {
                ResetSelectionMarks();
            }
        }

        private bool GoldControl(GameObject pawnToSpawn)
        {
            if (GoldController.Gold >= pawnToSpawn.GetComponent<Pawn>().pawn.pawnCost)
            {
                GameObject.FindObjectOfType<GoldController>().SubstractGold(pawnToSpawn.GetComponent<Pawn>().pawn.pawnCost);
                return true;
            }
            else
            {
                return false;
            }
            
        }

        private void ResetSelectionMarks()
        {
            foreach (var selectionMark in selectionMarks)
            {
                selectionMark.SetActive(false);
            }
        }
    }
}
