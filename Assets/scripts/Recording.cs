using UnityEngine;
using System.Linq;
using System.Text;

public class Recording
{
    private readonly AnimationCurve _posXCurve = new AnimationCurve();
    private readonly AnimationCurve _posYCurve = new AnimationCurve();
    private readonly AnimationCurve _posZCurve = new AnimationCurve();

    private readonly AnimationCurve _rotXCurve = new AnimationCurve();
    private readonly AnimationCurve _rotYCurve = new AnimationCurve();
    private readonly AnimationCurve _rotZCurve = new AnimationCurve();
    private readonly AnimationCurve _rotWCurve = new AnimationCurve();

    public float Duration { get; private set; }
    private readonly Transform _target;

    public Recording(Transform target) {
        _target = target;
    }

    public void AddSnapshot(float elapsed) {
        Duration = elapsed;

        var pos = _target.position;
        var rot = _target.rotation; // Quaternion

        UpdateCurve(_posXCurve, elapsed, pos.x);
        UpdateCurve(_posYCurve, elapsed, pos.y);
        UpdateCurve(_posZCurve, elapsed, pos.z);

        UpdateCurve(_rotXCurve, elapsed, rot.x);
        UpdateCurve(_rotYCurve, elapsed, rot.y);
        UpdateCurve(_rotZCurve, elapsed, rot.z);
        UpdateCurve(_rotWCurve, elapsed, rot.w);

        void UpdateCurve(AnimationCurve curve, float time, float val) {
            var count = curve.length;
            var kf = new Keyframe(time, val);

            if (count > 1 &&
                Mathf.Approximately(curve.keys[count - 1].value, curve.keys[count - 2].value) &&
                Mathf.Approximately(val, curve.keys[count - 1].value)) {
                curve.MoveKey(count - 1, kf);
            } else {
                curve.AddKey(kf);
            }
        }
    }

    public Pose EvaluatePoint(float elapsed) {
        var position = new Vector3(
            _posXCurve.Evaluate(elapsed),
            _posYCurve.Evaluate(elapsed),
            _posZCurve.Evaluate(elapsed)
        );

        var rotation = new Quaternion(
            _rotXCurve.Evaluate(elapsed),
            _rotYCurve.Evaluate(elapsed),
            _rotZCurve.Evaluate(elapsed),
            _rotWCurve.Evaluate(elapsed)
        ).normalized; // Normalizacja zapobiega błędom interpolacji

        return new Pose(position, rotation);
    }

    public Recording(string data) {
        _target = null;
        Deserialize(data);
        Duration = new[] {
            _posXCurve.keys.LastOrDefault().time,
            _posYCurve.keys.LastOrDefault().time,
            _posZCurve.keys.LastOrDefault().time,
            _rotXCurve.keys.LastOrDefault().time,
            _rotYCurve.keys.LastOrDefault().time,
            _rotZCurve.keys.LastOrDefault().time,
            _rotWCurve.keys.LastOrDefault().time
        }.Max();
    }

    private const char DATA_DELIMITER = '|';
    private const char CURVE_DELIMITER = '\n';

    public string Serialize() {
        var builder = new StringBuilder();

        StringifyPoints(_posXCurve);
        StringifyPoints(_posYCurve);
        StringifyPoints(_posZCurve);

        StringifyPoints(_rotXCurve);
        StringifyPoints(_rotYCurve);
        StringifyPoints(_rotZCurve);
        StringifyPoints(_rotWCurve, addDelimiter:false);

        void StringifyPoints(AnimationCurve curve, bool addDelimiter = true) {
            for (var i = 0; i < curve.length; i++) {
                var point = curve[i];
                builder.Append($"{point.time:F3},{point.value:F6}"); // Więcej precyzji dla Quaternion
                if (i != curve.length - 1) builder.Append(DATA_DELIMITER);
            }
            if (addDelimiter) builder.Append(CURVE_DELIMITER);
        }

        return builder.ToString();
    }

    private void Deserialize(string data) {
        var components = data.Split(CURVE_DELIMITER);

        DeserializePoint(_posXCurve, components[0]);
        DeserializePoint(_posYCurve, components[1]);
        DeserializePoint(_posZCurve, components[2]);

        DeserializePoint(_rotXCurve, components[3]);
        DeserializePoint(_rotYCurve, components[4]);
        DeserializePoint(_rotZCurve, components[5]);
        DeserializePoint(_rotWCurve, components[6]);

        void DeserializePoint(AnimationCurve curve, string d) {
            var splitValues = d.Split(DATA_DELIMITER);
            foreach (var timeValPair in splitValues) {
                var s = timeValPair.Split(',');
                if (s.Length == 2) {
                    if (float.TryParse(s[0], out var time) && float.TryParse(s[1], out var val)) {
                        var kf = new Keyframe(time, val);
                        curve.AddKey(kf);
                    }
                }
            }
        }
    }
}
