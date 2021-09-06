using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class Note : MonoBehaviour
{
    private static IInputDeviceRumble[] _deviceRumbler = null;

    public float Speed = 3.0f;
    [SerializeField] private ComboText _comboPrefab;
    [SerializeField] private float _comboTextForce = 250.0f;
    [SerializeField] private float _coolRumbleDT = 0.4f;
    [SerializeField] private float _goodRumbleDT = 0.2f;
    [SerializeField] private float _failRumbleDT = 0.1f;

    private Transform _tr;

    private void Awake()
    {
        _tr = GetComponent<Transform>();

        if(_deviceRumbler == null)
        {
            _deviceRumbler = new IInputDeviceRumble[2];
            _deviceRumbler[0] = InputDeviceUtil.CreateDeviceRumbleOrNull<XRController>(CommonUsages.LeftHand);
            _deviceRumbler[1] = InputDeviceUtil.CreateDeviceRumbleOrNull<XRController>(CommonUsages.RightHand);
        }
    }

    private void Update()
    {
        transform.position += Time.deltaTime * transform.forward * Speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "RedSaber" && other.tag != "BlueSaber")
            return;

        var ct = Instantiate<ComboText>(_comboPrefab, _tr.position, Quaternion.identity);
        var rb = ct.GetComponent<Rigidbody>();
        var handDevice = (other.tag == "RedSaber") ? _deviceRumbler[0] : _deviceRumbler[1];
        var saber = other.GetComponent<Saber>();

        if ((other.tag == "RedSaber" && tag == "RedNote") || (other.tag == "BlueSaber" && tag == "BlueNote"))
        {

            if (Vector3.Angle(saber.MoveDirection, _tr.up) > 130)
            {
                GameManager.Inst.AddCool();
                ct.SetText("COOL! " + GameManager.Inst.ComboCount.ToString());
                handDevice.Rumble(1, _coolRumbleDT);
            }
            else
            {
                GameManager.Inst.AddGood();
                ct.SetText("GOOD! " + GameManager.Inst.ComboCount.ToString());
                handDevice.Rumble(1, _goodRumbleDT);
            }
        }
        else
        {
            GameManager.Inst.AddFail();
            ct.SetText("FAIL!");
            handDevice.Rumble(1, _failRumbleDT);
        }

        rb.AddForce(_tr.up * _comboTextForce);
        saber.PlayHitEffect(other.ClosestPoint(_tr.position));

        Destroy(gameObject);
    }
}
