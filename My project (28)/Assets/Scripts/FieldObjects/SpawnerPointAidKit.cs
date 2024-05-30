using UnityEngine;
using System.Collections;

public class SpawnerPointAidKit : SpawnerPoint
{    
    [SerializeField] private FirstAidKit _object;
   
    protected override void Start()
    {
        FirstAidKit kit = Instantiate(_object, _position, Quaternion.identity);
        _player.KitDisabled += DisableObject;
        _object.gameObject.SetActive(true);
    }

    private void DisableObject(FirstAidKit kit)
    {
        kit.gameObject.SetActive(false);
        WaitForSeconds wait = new WaitForSeconds(_delay);
        StartCoroutine(Enable(wait, kit));
    }

    private IEnumerator Enable(WaitForSeconds wait, FirstAidKit kit)
    {
        yield return wait;
        kit.gameObject.SetActive(true);
    }
}
