using UnityEngine;
namespace SebraeLabVR
{
    [RequireComponent(typeof(Renderer))]
    public sealed class ColorToggle : MonoBehaviour
    {
        [SerializeField] private Color selectedColor = Color.cyan;
        private Renderer target;
        private MaterialPropertyBlock properties;
        private bool selected;
        private void Awake()
        {
            target = GetComponent<Renderer>();
            properties = new MaterialPropertyBlock();
        }
        public void Toggle()
        {
            selected = !selected;
            target.GetPropertyBlock(properties);
            properties.SetColor("_Color", selected ? selectedColor : Color.white);
            target.SetPropertyBlock(properties);
        }
    }
}