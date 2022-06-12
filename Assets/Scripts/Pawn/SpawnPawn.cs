using UnityEngine;

namespace Pawn
{
    public class SpawnPawn : MonoBehaviour
    {
        public GameObject _pawnToSpawn; // gameobject to spawn when clicked
        private Ray _myRay;      // initializing the ray
        private RaycastHit _hit; // initializing the raycast hit
        void Update ()
        {
            SpawnThePawn();
        }

        private void SpawnThePawn()
        {
            if (Camera.main != null)
                _myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            // my main camera to my mouse (This will give me a direction)

            if (Physics.Raycast (_myRay, out _hit)) //if myRay hits something, store all the info you can find in the raycasthit varible.
            {
                if (Input.GetMouseButtonDown (0) && PlayBoard.Board.GetValue(_hit.point) != 1) {// what to do if i press the left mouse button
                    Instantiate (_pawnToSpawn, RoundHitPoint(_hit.point), Quaternion.identity);// instatiate a prefab on the position where the ray hits the floor.
                    PlayBoard.Board.SetValue(RoundHitPoint(_hit.point),1);
                    Debug.Log (_hit.point);// debugs the vector3 of the position where I clicked
                }
            }  
        }


        private Vector3 RoundHitPoint(Vector3 hitPoint)
        {
            return new Vector3(Mathf.Round(hitPoint.x), 0, Mathf.Round(hitPoint.z));
        }
    }
}
