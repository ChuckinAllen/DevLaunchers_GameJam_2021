using BeardedManStudios.Forge.Networking.Frame;
using BeardedManStudios.Forge.Networking.Unity;
using System;
using UnityEngine;

namespace BeardedManStudios.Forge.Networking.Generated
{
	[GeneratedInterpol("{\"inter\":[0,0.15]")]
	public partial class HealthNetworkObject : NetworkObject
	{
		public const int IDENTITY = 7;

		private byte[] _dirtyFields = new byte[1];

		#pragma warning disable 0067
		public event FieldChangedEvent fieldAltered;
		#pragma warning restore 0067
		[ForgeGeneratedField]
		private bool _dead;
		public event FieldEvent<bool> deadChanged;
		public Interpolated<bool> deadInterpolation = new Interpolated<bool>() { LerpT = 0f, Enabled = false };
		public bool dead
		{
			get { return _dead; }
			set
			{
				// Don't do anything if the value is the same
				if (_dead == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x1;
				_dead = value;
				hasDirtyFields = true;
			}
		}

		public void SetdeadDirty()
		{
			_dirtyFields[0] |= 0x1;
			hasDirtyFields = true;
		}

		private void RunChange_dead(ulong timestep)
		{
			if (deadChanged != null) deadChanged(_dead, timestep);
			if (fieldAltered != null) fieldAltered("dead", _dead, timestep);
		}
		[ForgeGeneratedField]
		private float _health;
		public event FieldEvent<float> healthChanged;
		public InterpolateFloat healthInterpolation = new InterpolateFloat() { LerpT = 0.15f, Enabled = true };
		public float health
		{
			get { return _health; }
			set
			{
				// Don't do anything if the value is the same
				if (_health == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x2;
				_health = value;
				hasDirtyFields = true;
			}
		}

		public void SethealthDirty()
		{
			_dirtyFields[0] |= 0x2;
			hasDirtyFields = true;
		}

		private void RunChange_health(ulong timestep)
		{
			if (healthChanged != null) healthChanged(_health, timestep);
			if (fieldAltered != null) fieldAltered("health", _health, timestep);
		}

		protected override void OwnershipChanged()
		{
			base.OwnershipChanged();
			SnapInterpolations();
		}
		
		public void SnapInterpolations()
		{
			deadInterpolation.current = deadInterpolation.target;
			healthInterpolation.current = healthInterpolation.target;
		}

		public override int UniqueIdentity { get { return IDENTITY; } }

		protected override BMSByte WritePayload(BMSByte data)
		{
			UnityObjectMapper.Instance.MapBytes(data, _dead);
			UnityObjectMapper.Instance.MapBytes(data, _health);

			return data;
		}

		protected override void ReadPayload(BMSByte payload, ulong timestep)
		{
			_dead = UnityObjectMapper.Instance.Map<bool>(payload);
			deadInterpolation.current = _dead;
			deadInterpolation.target = _dead;
			RunChange_dead(timestep);
			_health = UnityObjectMapper.Instance.Map<float>(payload);
			healthInterpolation.current = _health;
			healthInterpolation.target = _health;
			RunChange_health(timestep);
		}

		protected override BMSByte SerializeDirtyFields()
		{
			dirtyFieldsData.Clear();
			dirtyFieldsData.Append(_dirtyFields);

			if ((0x1 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _dead);
			if ((0x2 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _health);

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
				if (deadInterpolation.Enabled)
				{
					deadInterpolation.target = UnityObjectMapper.Instance.Map<bool>(data);
					deadInterpolation.Timestep = timestep;
				}
				else
				{
					_dead = UnityObjectMapper.Instance.Map<bool>(data);
					RunChange_dead(timestep);
				}
			}
			if ((0x2 & readDirtyFlags[0]) != 0)
			{
				if (healthInterpolation.Enabled)
				{
					healthInterpolation.target = UnityObjectMapper.Instance.Map<float>(data);
					healthInterpolation.Timestep = timestep;
				}
				else
				{
					_health = UnityObjectMapper.Instance.Map<float>(data);
					RunChange_health(timestep);
				}
			}
		}

		public override void InterpolateUpdate()
		{
			if (IsOwner)
				return;

			if (deadInterpolation.Enabled && !deadInterpolation.current.UnityNear(deadInterpolation.target, 0.0015f))
			{
				_dead = (bool)deadInterpolation.Interpolate();
				//RunChange_dead(deadInterpolation.Timestep);
			}
			if (healthInterpolation.Enabled && !healthInterpolation.current.UnityNear(healthInterpolation.target, 0.0015f))
			{
				_health = (float)healthInterpolation.Interpolate();
				//RunChange_health(healthInterpolation.Timestep);
			}
		}

		private void Initialize()
		{
			if (readDirtyFlags == null)
				readDirtyFlags = new byte[1];

		}

		public HealthNetworkObject() : base() { Initialize(); }
		public HealthNetworkObject(NetWorker networker, INetworkBehavior networkBehavior = null, int createCode = 0, byte[] metadata = null) : base(networker, networkBehavior, createCode, metadata) { Initialize(); }
		public HealthNetworkObject(NetWorker networker, uint serverId, FrameStream frame) : base(networker, serverId, frame) { Initialize(); }

		// DO NOT TOUCH, THIS GETS GENERATED PLEASE EXTEND THIS CLASS IF YOU WISH TO HAVE CUSTOM CODE ADDITIONS
	}
}
