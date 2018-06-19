using BeardedManStudios.Forge.Networking.Frame;
using BeardedManStudios.Forge.Networking.Unity;
using System;
using UnityEngine;

namespace BeardedManStudios.Forge.Networking.Generated
{
	[GeneratedInterpol("{\"inter\":[0,0]")]
	public partial class ScoreSystemNetworkObject : NetworkObject
	{
		public const int IDENTITY = 9;

		private byte[] _dirtyFields = new byte[1];

		#pragma warning disable 0067
		public event FieldChangedEvent fieldAltered;
		#pragma warning restore 0067
		private int _Player1Score;
		public event FieldEvent<int> Player1ScoreChanged;
		public Interpolated<int> Player1ScoreInterpolation = new Interpolated<int>() { LerpT = 0f, Enabled = false };
		public int Player1Score
		{
			get { return _Player1Score; }
			set
			{
				// Don't do anything if the value is the same
				if (_Player1Score == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x1;
				_Player1Score = value;
				hasDirtyFields = true;
			}
		}

		public void SetPlayer1ScoreDirty()
		{
			_dirtyFields[0] |= 0x1;
			hasDirtyFields = true;
		}

		private void RunChange_Player1Score(ulong timestep)
		{
			if (Player1ScoreChanged != null) Player1ScoreChanged(_Player1Score, timestep);
			if (fieldAltered != null) fieldAltered("Player1Score", _Player1Score, timestep);
		}
		private int _Playaer2Score;
		public event FieldEvent<int> Playaer2ScoreChanged;
		public Interpolated<int> Playaer2ScoreInterpolation = new Interpolated<int>() { LerpT = 0f, Enabled = false };
		public int Playaer2Score
		{
			get { return _Playaer2Score; }
			set
			{
				// Don't do anything if the value is the same
				if (_Playaer2Score == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x2;
				_Playaer2Score = value;
				hasDirtyFields = true;
			}
		}

		public void SetPlayaer2ScoreDirty()
		{
			_dirtyFields[0] |= 0x2;
			hasDirtyFields = true;
		}

		private void RunChange_Playaer2Score(ulong timestep)
		{
			if (Playaer2ScoreChanged != null) Playaer2ScoreChanged(_Playaer2Score, timestep);
			if (fieldAltered != null) fieldAltered("Playaer2Score", _Playaer2Score, timestep);
		}

		protected override void OwnershipChanged()
		{
			base.OwnershipChanged();
			SnapInterpolations();
		}
		
		public void SnapInterpolations()
		{
			Player1ScoreInterpolation.current = Player1ScoreInterpolation.target;
			Playaer2ScoreInterpolation.current = Playaer2ScoreInterpolation.target;
		}

		public override int UniqueIdentity { get { return IDENTITY; } }

		protected override BMSByte WritePayload(BMSByte data)
		{
			UnityObjectMapper.Instance.MapBytes(data, _Player1Score);
			UnityObjectMapper.Instance.MapBytes(data, _Playaer2Score);

			return data;
		}

		protected override void ReadPayload(BMSByte payload, ulong timestep)
		{
			_Player1Score = UnityObjectMapper.Instance.Map<int>(payload);
			Player1ScoreInterpolation.current = _Player1Score;
			Player1ScoreInterpolation.target = _Player1Score;
			RunChange_Player1Score(timestep);
			_Playaer2Score = UnityObjectMapper.Instance.Map<int>(payload);
			Playaer2ScoreInterpolation.current = _Playaer2Score;
			Playaer2ScoreInterpolation.target = _Playaer2Score;
			RunChange_Playaer2Score(timestep);
		}

		protected override BMSByte SerializeDirtyFields()
		{
			dirtyFieldsData.Clear();
			dirtyFieldsData.Append(_dirtyFields);

			if ((0x1 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _Player1Score);
			if ((0x2 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _Playaer2Score);

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
				if (Player1ScoreInterpolation.Enabled)
				{
					Player1ScoreInterpolation.target = UnityObjectMapper.Instance.Map<int>(data);
					Player1ScoreInterpolation.Timestep = timestep;
				}
				else
				{
					_Player1Score = UnityObjectMapper.Instance.Map<int>(data);
					RunChange_Player1Score(timestep);
				}
			}
			if ((0x2 & readDirtyFlags[0]) != 0)
			{
				if (Playaer2ScoreInterpolation.Enabled)
				{
					Playaer2ScoreInterpolation.target = UnityObjectMapper.Instance.Map<int>(data);
					Playaer2ScoreInterpolation.Timestep = timestep;
				}
				else
				{
					_Playaer2Score = UnityObjectMapper.Instance.Map<int>(data);
					RunChange_Playaer2Score(timestep);
				}
			}
		}

		public override void InterpolateUpdate()
		{
			if (IsOwner)
				return;

			if (Player1ScoreInterpolation.Enabled && !Player1ScoreInterpolation.current.UnityNear(Player1ScoreInterpolation.target, 0.0015f))
			{
				_Player1Score = (int)Player1ScoreInterpolation.Interpolate();
				//RunChange_Player1Score(Player1ScoreInterpolation.Timestep);
			}
			if (Playaer2ScoreInterpolation.Enabled && !Playaer2ScoreInterpolation.current.UnityNear(Playaer2ScoreInterpolation.target, 0.0015f))
			{
				_Playaer2Score = (int)Playaer2ScoreInterpolation.Interpolate();
				//RunChange_Playaer2Score(Playaer2ScoreInterpolation.Timestep);
			}
		}

		private void Initialize()
		{
			if (readDirtyFlags == null)
				readDirtyFlags = new byte[1];

		}

		public ScoreSystemNetworkObject() : base() { Initialize(); }
		public ScoreSystemNetworkObject(NetWorker networker, INetworkBehavior networkBehavior = null, int createCode = 0, byte[] metadata = null) : base(networker, networkBehavior, createCode, metadata) { Initialize(); }
		public ScoreSystemNetworkObject(NetWorker networker, uint serverId, FrameStream frame) : base(networker, serverId, frame) { Initialize(); }

		// DO NOT TOUCH, THIS GETS GENERATED PLEASE EXTEND THIS CLASS IF YOU WISH TO HAVE CUSTOM CODE ADDITIONS
	}
}
