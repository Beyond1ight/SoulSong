namespace SuperTiled2Unity
{
    using UnityEngine;
    public class SuperTileLayer : SuperLayer
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Player")
            {

            }
        }
    }
}
