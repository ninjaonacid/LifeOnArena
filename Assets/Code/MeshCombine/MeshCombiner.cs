using UnityEngine;

namespace Code.MeshCombine
{
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    public class MeshCombiner : MonoBehaviour
    {
        private MeshRenderer _renderer;
        private Mesh _mesh;
        [SerializeField] private Material _material;
        [SerializeField] private string _meshName;

        [ContextMenu("Combine")]
        void Combine()
        {
            _renderer = gameObject.GetComponent<MeshRenderer>();
            _mesh = transform.GetComponent<MeshFilter>().sharedMesh;

            MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();

            CombineInstance[] combines = new CombineInstance[meshFilters.Length];
            int i = 0;

            while (i < meshFilters.Length)
            {
                combines[i].mesh = meshFilters[i].sharedMesh;
                combines[i].transform = meshFilters[i].transform.localToWorldMatrix;
                meshFilters[i].gameObject.SetActive(false);
                i++;
            }

            _mesh = new Mesh();
            _mesh.CombineMeshes(combines);

            transform.gameObject.SetActive(true);

            gameObject.AddComponent<MeshCollider>();

            _renderer.material = Material.Instantiate(_material);

            MeshSaverEditor.SaveMesh(_mesh, _meshName, false, false);
        }
    }
}