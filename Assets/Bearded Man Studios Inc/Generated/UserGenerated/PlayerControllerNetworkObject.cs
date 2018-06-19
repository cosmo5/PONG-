using BeardedManStudios.Forge.Networking.Frame;
using BeardedManStudios.Forge.Networking.Unity;
using System;
using UnityEngine;

namespace BeardedManStudios.Forge.Networking.Generated
{
	[GeneratedInterpol("{\"inter\":[0,0.15,0]")]
	public partial class PlayerControllerNetworkObject : NetworkObject
	{
		public const int IDENTITY = 8;

		private byte[] _dirtyFields = new byte[1];

		#pragma warning disable 0067
		public event FieldChangedEvent fieldAltered;
		#pragma warning restore 0067
		private bool _isMoving;
		public event FieldEvent<bool> isMovingChanged;
		public Interpolated<bool> isMovingInterpolation = new Interpolated<bool>() { LerpT = 0f, Enabled = false };
		public bool isMoving
		{
			get { return _isMoving; }
			set
			{
				// Don't do anything if the value is the same
				if (_isMoving == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x1;
				_isMoving = value;
				hasDirtyFields = true;
			}
		}

		public void SetisMovingDirty()
		{
			_dirtyFields[0] |= 0x1;
			hasDirtyFields = true;
		}

		private void RunChange_isMoving(ulong timestep)
		{
			if (isMovingChanged != null) isMovingChanged(_isMoving, timestep);
			if (fieldAltered != null) fieldAltered("isMoving", _isMoving, timestep);
		}
		private Vector3 _position;
		public event FieldEvent<Vector3> positionChanged;
		public InterpolateVector3 positionInterpolation = new InterpolateVector3() { LerpT = 0.15f, Enabled = true };
		public Vector3 position
		{
			get { return _position; }
			set
			{
				// Don't do anything if the value is the same
				if (_position == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x2;
				_position = value;
				hasDirtyFields = true;
			}
		}

		public void SetpositionDirty()
		{
			_dirtyFields[0] |= 0x2;
			hasDirtyFields = true;
		}

		private void RunChange_position(ulong timestep)
		{
			if (positionChanged != null) positionChanged(_position, timestep);
			if (fieldAltered != null) fieldAltered("position", _position, timestep);
		}
		private uint _ID;
		public event FieldEvent<uint> IDChanged;
		public Interpolated<uint> IDInterpolation = new Interpolated<uint>() { LerpT = 0f, Enabled = false };
		public uint ID
		{
			get { return _ID; }
			set
			{
				// Don't do anything if the value is the same
				if (_ID == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x4;
				_ID = value;
				hasDirtyFields = true;
			}
		}

		public void SetIDDirty()
		{
			_dirtyFields[0] |= 0x4;
			hasDirtyFields = true;
		}

		private void RunChange_ID(ulong timestep)
		{
			if (IDChanged != null) IDChanged(_ID, timestep);
			if (fieldAltered != null) fieldAltered("ID", _ID, timestep);
		}

		protected override void OwnershipChanged()
		{
			base.OwnershipChanged();
			SnapInterpolations();
		}
		
		public void SnapInterpolations()
		{
			isMovingInterpolation.current = isMovingInterpolation.target;
			positionInterpolation.current = positionInterpolation.target;
			IDInterpolation.current = IDInterpolation.target;
		}

		public override int UniqueIdentity { get { return IDENTITY; } }

		protected override BMSByte WritePayload(BMSByte data)
		{
			UnityObjectMapper.Instance.MapBytes(data, _isMoving);
			UnityObjectMapper.Instance.MapBytes(data, _position);
			UnityObjectMapper.Instance.MapBytes(data, _ID);

			return data;
		}

		protected override void ReadPayload(BMSByte payload, ulong timestep)
		{
			_isMoving = UnityObjectMapper.Instance.Map<bool>(payload);
			isMovingInterpolation.current = _isMoving;
			isMovingInterpolation.target = _isMoving;
			RunChange_isMoving(timestep);
			_position = UnityObjectMapper.Instance.Map<Vector3>(payload);
			positionInterpolation.current = _position;
			positionInterpolation.target = _position;
			RunChange_position(timestep);
			_ID = UnityObjectMapper.Instance.Map<uint>(payload);
			IDInterpolation.current = _ID;
			IDInterpolation.target = _ID;
			RunChange_ID(timestep);
		}

		protected override BMSByte SerializeDirtyFields()
		{
			dirtyFieldsData.Clear();
			dirtyFieldsData.Append(_dirtyFields);

			if ((0x1 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _isMoving);
			if ((0x2 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _position);
			if ((0x4 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _ID);

			// Reset all the dirty fields
			for (int i = 0; i < _dirtyFields.Length; i++)
				_dirtyFields[i] = 0;

			return dirtyFieldsData;
		}

		protected override void ReadDirtyFields(BMSByte data, ulong timestep)
		{
			if (readDirtyFlags == null)
				Initialize();

			Buffer.BlockCopy(data.byteArr, data.StartIndex(), readDirtyFlags, 0, readDirtyFlags.Length);
			data.MoveStartIndex(readDirtyFlags.Length);

			if ((0x1 & readDirtyFlags[0]) != 0)
			{
				if (isMovingInterpolation.Enabled)
				{
					isMovingInterpolation.target = UnityObjectMapper.Instance.Map<bool>(data);
					isMovingInterpolation.Timestep = timestep;
				}
				else
				{
					_isMoving = UnityObjectMapper.Instance.Map<bool>(data);
					RunChange_isMoving(timestep);
				}
			}
			if ((0x2 & readDirtyFlags[0]) != 0)
			{
				if (positionInterpolation.Enabled)
				{
					positionInterpolation.target = UnityObjectMapper.Instance.Map<Vector3>(data);
					positionInterpolation.Timestep = timestep;
				}
				else
				{
					_position = UnityObjectMapper.Instance.Map<Vector3>(data);
					RunChange_position(timestep);
				}
			}
			if ((0x4 & readDirtyFlags[0]) != 0)
			{
				if (IDInterpolation.Enabled)
				{
					IDInterpolation.target = UnityObjectMapper.Instance.Map<uint>(data);
					IDInterpolation.Timestep = timestep;
				}
				else
				{
					_ID = UnityObjectMapper.Instance.Map<uint>(data);
					RunChange_ID(timestep);
				}
			}
		}

		public override void InterpolateUpdate()
		{
			if (IsOwner)
				return;

			if (isMovingInterpolation.Enabled && !isMovingInterpolation.current.UnityNear(isMovingInterpolation.target, 0.0015f))
			{
				_isMoving = (bool)isMovingInterpolation.Interpolate();
				//RunChange_isMoving(isMovingInterpolation.Timestep);
			}
			if (positionInterpolation.Enabled && !positionInterpolation.current.UnityNear(positionInterpolation.target, 0.0015f))
			{
				_position = (Vector3)positionInterpolation.Interpolate();
				//RunChange_position(positionInterpolation.Timestep);
			}
			if (IDInterpolation.Enabled && !IDInterpolation.current.UnityNear(IDInterpolation.target, 0.0015f))
			{
				_ID = (uint)IDInterpolation.Interpolate();
				//RunChange_ID(IDInterpolation.Timestep);
			}
		}

		private void Initialize()
		{
			if (readDirtyFlags == null)
				readDirtyFlags = new byte[1];

		}

		public PlayerControllerNetworkObject() : base() { Initialize(); }
		public PlayerControllerNetworkObject(NetWorker networker, INetworkBehavior networkBehavior = null, int createCode = 0, byte[] metadata = null) : base(networker, networkBehavior, createCode, metadata) { Initialize(); }
		public PlayerControllerNetworkObject(NetWorker networker, uint serverId, FrameStream frame) : base(networker, serverId, frame) { Initialize(); }

		// DO NOT TOUCH, THIS GETS GENERATED PLEASE EXTEND THIS CLASS IF YOU WISH TO HAVE CUSTOM CODE ADDITIONS
	}
}
