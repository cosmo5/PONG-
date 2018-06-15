using BeardedManStudios.Forge.Networking.Frame;
using BeardedManStudios.Forge.Networking.Unity;
using System;
using UnityEngine;

namespace BeardedManStudios.Forge.Networking.Generated
{
	[GeneratedInterpol("{\"inter\":[0,0,0]")]
	public partial class NetworkManagerNetworkObject : NetworkObject
	{
		public const int IDENTITY = 6;

		private byte[] _dirtyFields = new byte[1];

		#pragma warning disable 0067
		public event FieldChangedEvent fieldAltered;
		#pragma warning restore 0067
		private Vector3 _SpawnPos1;
		public event FieldEvent<Vector3> SpawnPos1Changed;
		public InterpolateVector3 SpawnPos1Interpolation = new InterpolateVector3() { LerpT = 0f, Enabled = false };
		public Vector3 SpawnPos1
		{
			get { return _SpawnPos1; }
			set
			{
				// Don't do anything if the value is the same
				if (_SpawnPos1 == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x1;
				_SpawnPos1 = value;
				hasDirtyFields = true;
			}
		}

		public void SetSpawnPos1Dirty()
		{
			_dirtyFields[0] |= 0x1;
			hasDirtyFields = true;
		}

		private void RunChange_SpawnPos1(ulong timestep)
		{
			if (SpawnPos1Changed != null) SpawnPos1Changed(_SpawnPos1, timestep);
			if (fieldAltered != null) fieldAltered("SpawnPos1", _SpawnPos1, timestep);
		}
		private Vector3 _SpawnPos2;
		public event FieldEvent<Vector3> SpawnPos2Changed;
		public InterpolateVector3 SpawnPos2Interpolation = new InterpolateVector3() { LerpT = 0f, Enabled = false };
		public Vector3 SpawnPos2
		{
			get { return _SpawnPos2; }
			set
			{
				// Don't do anything if the value is the same
				if (_SpawnPos2 == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x2;
				_SpawnPos2 = value;
				hasDirtyFields = true;
			}
		}

		public void SetSpawnPos2Dirty()
		{
			_dirtyFields[0] |= 0x2;
			hasDirtyFields = true;
		}

		private void RunChange_SpawnPos2(ulong timestep)
		{
			if (SpawnPos2Changed != null) SpawnPos2Changed(_SpawnPos2, timestep);
			if (fieldAltered != null) fieldAltered("SpawnPos2", _SpawnPos2, timestep);
		}
		private bool _BothSpawned;
		public event FieldEvent<bool> BothSpawnedChanged;
		public Interpolated<bool> BothSpawnedInterpolation = new Interpolated<bool>() { LerpT = 0f, Enabled = false };
		public bool BothSpawned
		{
			get { return _BothSpawned; }
			set
			{
				// Don't do anything if the value is the same
				if (_BothSpawned == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x4;
				_BothSpawned = value;
				hasDirtyFields = true;
			}
		}

		public void SetBothSpawnedDirty()
		{
			_dirtyFields[0] |= 0x4;
			hasDirtyFields = true;
		}

		private void RunChange_BothSpawned(ulong timestep)
		{
			if (BothSpawnedChanged != null) BothSpawnedChanged(_BothSpawned, timestep);
			if (fieldAltered != null) fieldAltered("BothSpawned", _BothSpawned, timestep);
		}

		protected override void OwnershipChanged()
		{
			base.OwnershipChanged();
			SnapInterpolations();
		}
		
		public void SnapInterpolations()
		{
			SpawnPos1Interpolation.current = SpawnPos1Interpolation.target;
			SpawnPos2Interpolation.current = SpawnPos2Interpolation.target;
			BothSpawnedInterpolation.current = BothSpawnedInterpolation.target;
		}

		public override int UniqueIdentity { get { return IDENTITY; } }

		protected override BMSByte WritePayload(BMSByte data)
		{
			UnityObjectMapper.Instance.MapBytes(data, _SpawnPos1);
			UnityObjectMapper.Instance.MapBytes(data, _SpawnPos2);
			UnityObjectMapper.Instance.MapBytes(data, _BothSpawned);

			return data;
		}

		protected override void ReadPayload(BMSByte payload, ulong timestep)
		{
			_SpawnPos1 = UnityObjectMapper.Instance.Map<Vector3>(payload);
			SpawnPos1Interpolation.current = _SpawnPos1;
			SpawnPos1Interpolation.target = _SpawnPos1;
			RunChange_SpawnPos1(timestep);
			_SpawnPos2 = UnityObjectMapper.Instance.Map<Vector3>(payload);
			SpawnPos2Interpolation.current = _SpawnPos2;
			SpawnPos2Interpolation.target = _SpawnPos2;
			RunChange_SpawnPos2(timestep);
			_BothSpawned = UnityObjectMapper.Instance.Map<bool>(payload);
			BothSpawnedInterpolation.current = _BothSpawned;
			BothSpawnedInterpolation.target = _BothSpawned;
			RunChange_BothSpawned(timestep);
		}

		protected override BMSByte SerializeDirtyFields()
		{
			dirtyFieldsData.Clear();
			dirtyFieldsData.Append(_dirtyFields);

			if ((0x1 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _SpawnPos1);
			if ((0x2 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _SpawnPos2);
			if ((0x4 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _BothSpawned);

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
				if (SpawnPos1Interpolation.Enabled)
				{
					SpawnPos1Interpolation.target = UnityObjectMapper.Instance.Map<Vector3>(data);
					SpawnPos1Interpolation.Timestep = timestep;
				}
				else
				{
					_SpawnPos1 = UnityObjectMapper.Instance.Map<Vector3>(data);
					RunChange_SpawnPos1(timestep);
				}
			}
			if ((0x2 & readDirtyFlags[0]) != 0)
			{
				if (SpawnPos2Interpolation.Enabled)
				{
					SpawnPos2Interpolation.target = UnityObjectMapper.Instance.Map<Vector3>(data);
					SpawnPos2Interpolation.Timestep = timestep;
				}
				else
				{
					_SpawnPos2 = UnityObjectMapper.Instance.Map<Vector3>(data);
					RunChange_SpawnPos2(timestep);
				}
			}
			if ((0x4 & readDirtyFlags[0]) != 0)
			{
				if (BothSpawnedInterpolation.Enabled)
				{
					BothSpawnedInterpolation.target = UnityObjectMapper.Instance.Map<bool>(data);
					BothSpawnedInterpolation.Timestep = timestep;
				}
				else
				{
					_BothSpawned = UnityObjectMapper.Instance.Map<bool>(data);
					RunChange_BothSpawned(timestep);
				}
			}
		}

		public override void InterpolateUpdate()
		{
			if (IsOwner)
				return;

			if (SpawnPos1Interpolation.Enabled && !SpawnPos1Interpolation.current.UnityNear(SpawnPos1Interpolation.target, 0.0015f))
			{
				_SpawnPos1 = (Vector3)SpawnPos1Interpolation.Interpolate();
				//RunChange_SpawnPos1(SpawnPos1Interpolation.Timestep);
			}
			if (SpawnPos2Interpolation.Enabled && !SpawnPos2Interpolation.current.UnityNear(SpawnPos2Interpolation.target, 0.0015f))
			{
				_SpawnPos2 = (Vector3)SpawnPos2Interpolation.Interpolate();
				//RunChange_SpawnPos2(SpawnPos2Interpolation.Timestep);
			}
			if (BothSpawnedInterpolation.Enabled && !BothSpawnedInterpolation.current.UnityNear(BothSpawnedInterpolation.target, 0.0015f))
			{
				_BothSpawned = (bool)BothSpawnedInterpolation.Interpolate();
				//RunChange_BothSpawned(BothSpawnedInterpolation.Timestep);
			}
		}

		private void Initialize()
		{
			if (readDirtyFlags == null)
				readDirtyFlags = new byte[1];

		}

		public NetworkManagerNetworkObject() : base() { Initialize(); }
		public NetworkManagerNetworkObject(NetWorker networker, INetworkBehavior networkBehavior = null, int createCode = 0, byte[] metadata = null) : base(networker, networkBehavior, createCode, metadata) { Initialize(); }
		public NetworkManagerNetworkObject(NetWorker networker, uint serverId, FrameStream frame) : base(networker, serverId, frame) { Initialize(); }

		// DO NOT TOUCH, THIS GETS GENERATED PLEASE EXTEND THIS CLASS IF YOU WISH TO HAVE CUSTOM CODE ADDITIONS
	}
}
