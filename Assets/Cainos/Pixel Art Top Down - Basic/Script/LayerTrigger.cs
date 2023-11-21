using UnityEngine;

namespace Assets.Cainos.Pixel_Art_Top_Down___Basic.Script
{
    //when object exit the trigger, put it to the assigned layer and sorting layers
    //used in the stair objects for player to travel between layers
    public class LayerTrigger : MonoBehaviour
    {
        public string Layer;
        public string SortingLayer;

        private void OnTriggerExit2D(Collider2D other)
        {
            other.gameObject.layer = LayerMask.NameToLayer(Layer);

            other.gameObject.GetComponent<SpriteRenderer>().sortingLayerName = SortingLayer;
            SpriteRenderer[] srs = other.gameObject.GetComponentsInChildren<SpriteRenderer>();
            foreach ( SpriteRenderer sr in srs)
            {
                sr.sortingLayerName = SortingLayer;
            }
        }

    }
}
