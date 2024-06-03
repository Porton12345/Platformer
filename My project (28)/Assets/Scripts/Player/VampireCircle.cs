using UnityEngine;

public class VampireCircle : MonoBehaviour
{   
    [SerializeField] private Vampirism _player;
    [SerializeField] private Material _material;
    
    private int _segments = 360;
    private LineRenderer _lineRenderer;
    private float _startWidth = 0.1f;
    private float _endWidth = 0f;

    private void Start()
    {
        _lineRenderer = gameObject.AddComponent<LineRenderer>();
        _lineRenderer.positionCount = _segments + 1;
        _lineRenderer.useWorldSpace = false;
        _lineRenderer.widthMultiplier = 0.1f;          
        _lineRenderer.material = _material;
        _lineRenderer.startColor = Color.red;
        _lineRenderer.endColor = Color.blue;
        _lineRenderer.startWidth = _startWidth;
        _lineRenderer.endWidth = _endWidth;
    }

    private void Update()
    {
        DrawCircle();
    }

    private void DrawCircle()
    {
        float x;
        float y;
        float z = 10f;

        float angle = 0f;
                
        for (int i = 0; i < (_segments + 1); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * _player.Radius;
            y = Mathf.Cos(Mathf.Deg2Rad * angle) * _player.Radius;

            _lineRenderer.SetPosition(i, new Vector3(x, y, z));
            angle += (360f / _segments);
        }            
    }
}