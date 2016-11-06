using UnityEngine;
using System.Collections;

public class ManageElementTab : MonoBehaviour {

    private bool _isHidinging;
    private bool _isShowing;
    public bool _toHide;
    public bool _toShow;
    private float _timeToHide;
    private ManageParticle[] _childComp;

	// Use this for initialization
	void Start () {
        _toHide = false;
        _timeToHide = 2f;
	    _childComp = GetComponentsInChildren<ManageParticle>();
    }
	
	// Update is called once per frame
	void Update () {
        if (_toHide)
            HideTab();
        else
            ShowTab();
        _timeToHide -= Time.deltaTime;
        if (Input.GetButton("ShowTabElem") || _timeToHide >= 0f)
            _toHide = false;
        else
            _toHide = true;

    }
    
    void HideTab()
    {
       // Debug.Log(transform.localPosition);
        foreach (ManageParticle comp in _childComp)
        {
            comp.Hide(transform.localPosition, 0f);
        }
    }

    void ShowTab()
    {
        foreach (ManageParticle comp in _childComp)
        {
            comp.Show(transform.parent.localPosition, 1f);
        }
    }

    public void SetElements(bool fire, bool water, bool elec, bool wind)
    {
        if (!fire)
            transform.FindChild("FireParticle").gameObject.SetActive(false);
        if (!water)
            transform.FindChild("WaterParticle").gameObject.SetActive(false);
        if (!elec)
            transform.FindChild("ElecParticle").gameObject.SetActive(false);
        if (!wind)
            transform.FindChild("WindParticle").gameObject.SetActive(false);
    }
}
