using UnityEngine;

namespace SpawnUser
{
    public class SpawnScriptUserControlled : MonoBehaviour
    {
        [SerializeField] SpawnableThings _spawnableThing;
        
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                SpawnThing();
            }
        }
    
        private void SpawnThing()
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Instantiate(_spawnableThing.prefab, mousePos, Quaternion.identity);
        }
    }
}
