using System;
using System.ComponentModel;
using FreeSCADA.Interfaces;
using FreeSCADA.Interfaces.Plugins;

namespace FreeSCADA.Common
{
    public abstract class BaseChannel : IChannel
    {
		private ChannelStatusFlags status = ChannelStatusFlags.Unknown;
		private string name;
        private string hierarchy;
        private string unit;
        private double rangeMax;
        private double rangeMin;
        private string description;
		private Type type;
		protected bool readOnly;
		private DateTime modifyTime;

        protected ICommunicationPlug plugin;
        private int level;
        private object tag;
        private object value = null;
		object defaultValue = null;

        public BaseChannel(string name, string hierarchy, bool readOnly, ICommunicationPlug plugin, Type type)
        {
            this.name = name;
            this.hierarchy = hierarchy;
            this.readOnly = readOnly;
            this.plugin = plugin;
            this.type = type;

			defaultValue = type.IsValueType ? Activator.CreateInstance(type) : null;
			if (type == typeof(string))
				defaultValue = "";
			value = defaultValue;
            
            modifyTime = DateTime.MinValue;
            status = ChannelStatusFlags.Unknown;
        }

        #region IChannel Members

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler ValueChanged;

        public string Name
        {
            get 
			{ 
				lock(this)
					return name; 
			}
        }

        public string Hierarchy
        {
            get
            {
                lock (this)
                    return hierarchy;
            }
        }

        public string Unit
        {
            get
            {
                lock (this)
                    return unit;
            }
            set
            {
                lock (this)
                    unit = value;
            }
        }
        public double RangeMax
        {
            get
            {
                lock (this)
                    return rangeMax;
            }
            set
            {
                lock (this)
                    rangeMax = value;
            }
        }
        public double RangeMin
        {
            get
            {
                lock (this)
                    return rangeMin;
            }
            set
            {
                lock (this)
                    rangeMin = value;
            }
        }
        public string Description
        {
            get
            {
                lock (this)
                    return description;
            }
            set
            {
                lock (this)
                    description = value;
            }
        }

		public string PluginId
		{
			get
			{
				lock (this)
					return plugin.PluginId;
			}
		}

		public string FullId
		{
			get
			{
				lock (this)
					return plugin.PluginId + "." + name;
			}
		}

        public Type Type
        {
            get 
			{
				lock (this) 
					return type;
			}
        }

        public bool IsReadOnly
        {
            get 
			{
				lock (this)
					return readOnly; 
			}
        }

        public virtual object Value
        {
            get
            {
				if (plugin.IsConnected)
				{
					lock (this)
						return value;
				}
				else
				{
					lock (this)
						return defaultValue;
				}
                
            }
            set
            {
				if (!IsReadOnly && plugin.IsConnected)
                    DoUpdate(value);
            }
        }

        public DateTime ModifyTime
        {
            get
            {
				lock(this)
					return modifyTime;
            }
        }

        public string Status
        {
            get
            {
				switch (StatusFlags)
				{
					case ChannelStatusFlags.Good:
						return "Good";
					case ChannelStatusFlags.Bad:
						return "Bad";
					default:
						return "Unknown";
				}
            }
        }

		public ChannelStatusFlags StatusFlags
		{
			get
			{
				lock (this)
					return status;
			}
			set
			{
                lock (this)
                {
                    status = value;
                    modifyTime = DateTime.Now;
                }
                FireValueChanged();
			}
		}

        public int Level
        {
            get
            {
                lock (this)
                    return level;
            }
        }

        public object Tag
        {
            get { return tag; }
            set { tag = value; }
        }

        #endregion

		public virtual void Reset()
		{
			object defVal;
			lock (this)
				defVal = defaultValue;

			InternalSetValue(defVal, DateTime.Now, ChannelStatusFlags.Unknown);
		}

        protected void FireValueChanged()
        {
            if (PropertyChanged != null)
            {
                OnPropertyChanged("Value");
                OnPropertyChanged("ModifyTime");
                OnPropertyChanged("Status");
				OnPropertyChanged("StatusFlags");
            }

			if (ValueChanged != null)
			{
				try
				{
					ValueChanged(this, new EventArgs());
				}
				catch (Exception e)
				{
					Env.Current.Logger.LogWarning(string.Format("Fail to update some of '{0}' subscribers: {1}", Name, e.Message));
				}
			}
        }

        protected void InternalSetValue(object value, DateTime externalTime, ChannelStatusFlags status)
        {
            bool fire = false;
            lock (this)
            {
                object old = this.value;

				if (value != null)
				{
					this.value = value;
					this.type = value.GetType();
					this.status = status;
				}
				else
				{
					this.value = defaultValue;
					this.type = defaultValue.GetType();
					this.status = ChannelStatusFlags.Unknown;
				}

                modifyTime = externalTime;
                if (old != null)
                    fire = !old.Equals(this.value);
                else
                    fire = true;
            }
            if (fire)
                FireValueChanged();
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

		public abstract void DoUpdate();
        public virtual void DoUpdate(object value)
        {
			InternalSetValue(value, DateTime.Now, ChannelStatusFlags.Good);
        }
		public virtual void DoUpdate(object value, DateTime externalTime, ChannelStatusFlags status)
		{
			InternalSetValue(value, externalTime, status);
		}
    }
}
