using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Extensions;

namespace UnityEngine.Tilemaps
{
    [Serializable]
    internal enum GridInformationType
    {
        Integer,
        String,
        Float,
        Double,
        UnityObject,
        Color
    }

    [Serializable]
    [AddComponentMenu("Tilemap/Grid Information")]
    public class GridInformation : MonoBehaviour, ISerializationCallbackReceiver
    {
        // 構造体たち
        // value
        internal struct GridInformationValue
        {
            public GridInformationType type;
            public object data;
        }

        // key
        [Serializable]
        internal struct GridInformationKey
        {
            public Vector3Int position;
            public String name;
        }

        private Dictionary<GridInformationKey, GridInformationValue> _positionProperties = new Dictionary<GridInformationKey, GridInformationValue>();
        internal Dictionary<GridInformationKey, GridInformationValue> positionProperties
        {
            get { return _positionProperties; }
        }

        [SerializeField]
        [ReadOnly]
        private List<GridInformationKey> _positionIntKeys = new List<GridInformationKey>();

        [SerializeField]
        [ReadOnly]
        private List<int> _positionIntValues = new List<int>();

        [SerializeField]
        [ReadOnly]
        private List<GridInformationKey> _positionStringKeys = new List<GridInformationKey>();

        [SerializeField]
        [ReadOnly]
        private List<String> _positionStringValues = new List<String>();

        [SerializeField]
        [ReadOnly]
        private List<GridInformationKey> _positionFloatKeys = new List<GridInformationKey>();

        [SerializeField]
        [ReadOnly]
        private List<float> _positionFloatValues = new List<float>();

        [SerializeField]
        [ReadOnly]
        private List<GridInformationKey> _positionDoubleKeys = new List<GridInformationKey>();

        [SerializeField]
        [ReadOnly]
        private List<Double> _positionDoubleValues = new List<Double>();

        [SerializeField]
        [ReadOnly]
        private List<GridInformationKey> _positionObjectKeys = new List<GridInformationKey>();

        [SerializeField]
        [ReadOnly]
        private List<Object> _positionObjectValues = new List<Object>();

        [SerializeField]
        [ReadOnly]
        private List<GridInformationKey> _positionColorKeys = new List<GridInformationKey>();

        [SerializeField]
        [ReadOnly]
        private List<Color> m_PositionColorValues = new List<Color>();

        /// <summary>
        /// シリアライズ前のコールバック
        /// </summary>
        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            Grid grid = GetComponentInParent<Grid>();
            if (grid == null)
            {
                return;
            }

            // クリア
            _positionIntKeys.Clear();
            _positionIntValues.Clear();
            _positionStringKeys.Clear();
            _positionStringValues.Clear();
            _positionFloatKeys.Clear();
            _positionFloatValues.Clear();
            _positionDoubleKeys.Clear();
            _positionDoubleValues.Clear();
            _positionObjectKeys.Clear();
            _positionObjectValues.Clear();
            _positionColorKeys.Clear();
            m_PositionColorValues.Clear();

            foreach (var prop in _positionProperties)
            {
                switch (prop.Value.type)
                {
                    case GridInformationType.Integer:
                        _positionIntKeys.Add(prop.Key);
                        _positionIntValues.Add((int)prop.Value.data);
                        break;
                    case GridInformationType.String:
                        _positionStringKeys.Add(prop.Key);
                        _positionStringValues.Add(prop.Value.data as String);
                        break;
                    case GridInformationType.Float:
                        _positionFloatKeys.Add(prop.Key);
                        _positionFloatValues.Add((float)prop.Value.data);
                        break;
                    case GridInformationType.Double:
                        _positionDoubleKeys.Add(prop.Key);
                        _positionDoubleValues.Add((double)prop.Value.data);
                        break;
                    case GridInformationType.Color:
                        _positionColorKeys.Add(prop.Key);
                        m_PositionColorValues.Add((Color)prop.Value.data);
                        break;
                    default:
                        _positionObjectKeys.Add(prop.Key);
                        _positionObjectValues.Add(prop.Value.data as Object);
                        break;
                }
            }
        }

        /// <summary>
        /// シリアライズ後のコールバック
        /// </summary>
        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            //
            _positionProperties.Clear();
            for (int i = 0; i != Math.Min(_positionIntKeys.Count, _positionIntValues.Count); i++)
            {
                GridInformationValue positionValue;
                positionValue.type = GridInformationType.Integer;
                positionValue.data = _positionIntValues[i];
                _positionProperties.Add(_positionIntKeys[i], positionValue);
            }
            for (int i = 0; i != Math.Min(_positionStringKeys.Count, _positionStringValues.Count); i++)
            {
                GridInformationValue positionValue;
                positionValue.type = GridInformationType.String;
                positionValue.data = _positionStringValues[i];
                _positionProperties.Add(_positionStringKeys[i], positionValue);
            }
            for (int i = 0; i != Math.Min(_positionFloatKeys.Count, _positionFloatValues.Count); i++)
            {
                GridInformationValue positionValue;
                positionValue.type = GridInformationType.Float;
                positionValue.data = _positionFloatValues[i];
                _positionProperties.Add(_positionFloatKeys[i], positionValue);
            }
            for (int i = 0; i != Math.Min(_positionDoubleKeys.Count, _positionDoubleValues.Count); i++)
            {
                GridInformationValue positionValue;
                positionValue.type = GridInformationType.Double;
                positionValue.data = _positionDoubleValues[i];
                _positionProperties.Add(_positionDoubleKeys[i], positionValue);
            }
            for (int i = 0; i != Math.Min(_positionObjectKeys.Count, _positionObjectValues.Count); i++)
            {
                GridInformationValue positionValue;
                positionValue.type = GridInformationType.UnityObject;
                positionValue.data = _positionObjectValues[i];
                _positionProperties.Add(_positionObjectKeys[i], positionValue);
            }
            for (int i = 0; i != Math.Min(_positionColorKeys.Count, m_PositionColorValues.Count); i++)
            {
                GridInformationValue positionValue;
                positionValue.type = GridInformationType.Color;
                positionValue.data = m_PositionColorValues[i];
                _positionProperties.Add(_positionColorKeys[i], positionValue);
            }
        }

        public bool SetPositionProperty<T>(Vector3Int position, String name, T positionProperty)
        {
            throw new NotImplementedException("Storing this type is not accepted in GridInformation");
        }

        public bool SetPositionProperty(Vector3Int position, String name, int positionProperty)
        {
            return SetPositionProperty(position, name, GridInformationType.Integer, positionProperty);
        }

        public bool SetPositionProperty(Vector3Int position, String name, string positionProperty)
        {
            return SetPositionProperty(position, name, GridInformationType.String, positionProperty);
        }

        public bool SetPositionProperty(Vector3Int position, String name, float positionProperty)
        {
            return SetPositionProperty(position, name, GridInformationType.Float, positionProperty);
        }

        public bool SetPositionProperty(Vector3Int position, String name, double positionProperty)
        {
            return SetPositionProperty(position, name, GridInformationType.Double, positionProperty);
        }

        public bool SetPositionProperty(Vector3Int position, String name, UnityEngine.Object positionProperty)
        {
            return SetPositionProperty(position, name, GridInformationType.UnityObject, positionProperty);
        }

        public bool SetPositionProperty(Vector3Int position, String name, Color positionProperty)
        {
            return SetPositionProperty(position, name, GridInformationType.Color, positionProperty);
        }

        private bool SetPositionProperty(Vector3Int position, String name, GridInformationType dataType, System.Object positionProperty)
        {
            Grid grid = GetComponentInParent<Grid>();
            if (grid != null && positionProperty != null)
            {
                GridInformationKey positionKey;
                positionKey.position = position;
                positionKey.name = name;

                GridInformationValue positionValue;
                positionValue.type = dataType;
                positionValue.data = positionProperty;

                _positionProperties.Add(positionKey, positionValue);
                return true;
            }
            return false;
        }

        public T GetPositionProperty<T>(Vector3Int position, String name, T defaultValue) where T : UnityEngine.Object
        {
            GridInformationKey positionKey;
            positionKey.position = position;
            positionKey.name = name;

            GridInformationValue positionValue;
            if (_positionProperties.TryGetValue(positionKey, out positionValue))
            {
                if (positionValue.type != GridInformationType.UnityObject)
                    throw new InvalidCastException("Value stored in GridInformation is not of the right type");
                return positionValue.data as T;
            }
            return defaultValue;
        }

        public int GetPositionProperty(Vector3Int position, String name, int defaultValue)
        {
            GridInformationKey positionKey;
            positionKey.position = position;
            positionKey.name = name;

            GridInformationValue positionValue;
            if (_positionProperties.TryGetValue(positionKey, out positionValue))
            {
                if (positionValue.type != GridInformationType.Integer)
                    throw new InvalidCastException("Value stored in GridInformation is not of the right type");
                return (int)positionValue.data;
            }
            return defaultValue;
        }

        public string GetPositionProperty(Vector3Int position, String name, string defaultValue)
        {
            GridInformationKey positionKey;
            positionKey.position = position;
            positionKey.name = name;

            GridInformationValue positionValue;
            if (_positionProperties.TryGetValue(positionKey, out positionValue))
            {
                if (positionValue.type != GridInformationType.String)
                    throw new InvalidCastException("Value stored in GridInformation is not of the right type");
                return (string)positionValue.data;
            }
            return defaultValue;
        }

        public float GetPositionProperty(Vector3Int position, String name, float defaultValue)
        {
            GridInformationKey positionKey;
            positionKey.position = position;
            positionKey.name = name;

            GridInformationValue positionValue;
            if (_positionProperties.TryGetValue(positionKey, out positionValue))
            {
                if (positionValue.type != GridInformationType.Float)
                    throw new InvalidCastException("Value stored in GridInformation is not of the right type");
                return (float)positionValue.data;
            }
            return defaultValue;
        }

        public double GetPositionProperty(Vector3Int position, String name, double defaultValue)
        {
            GridInformationKey positionKey;
            positionKey.position = position;
            positionKey.name = name;

            GridInformationValue positionValue;
            if (_positionProperties.TryGetValue(positionKey, out positionValue))
            {
                if (positionValue.type != GridInformationType.Double)
                    throw new InvalidCastException("Value stored in GridInformation is not of the right type");
                return (double)positionValue.data;
            }
            return defaultValue;
        }

        public Color GetPositionProperty(Vector3Int position, String name, Color defaultValue)
        {
            GridInformationKey positionKey;
            positionKey.position = position;
            positionKey.name = name;

            GridInformationValue positionValue;
            if (_positionProperties.TryGetValue(positionKey, out positionValue))
            {
                if (positionValue.type != GridInformationType.Color)
                    throw new InvalidCastException("Value stored in GridInformation is not of the right type");
                return (Color)positionValue.data;
            }
            return defaultValue;
        }

        public bool ErasePositionProperty(Vector3Int position, String name)
        {
            GridInformationKey positionKey;
            positionKey.position = position;
            positionKey.name = name;
            return _positionProperties.Remove(positionKey);
        }

        public virtual void Reset()
        {
            _positionProperties.Clear();
        }

        public Vector3Int[] GetAllPositions(string propertyName)
        {
            return _positionProperties.Keys.ToList().FindAll(x => x.name == propertyName).Select(x => x.position).ToArray();
        }
    }
}