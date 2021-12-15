using FreeSCADA.Interfaces;
using FreeSCADA.Common.Scripting;
using FreeSCADA.Common;
using System;
using System.Collections.Generic;
using System.IO;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;

namespace FreeSCADA.Common.Scripting
{
	public class ScriptManager : IScriptsManager
	{
		//public delegate void NewScriptCreatedHandler(Object sender, Script script);
		public event NewScriptCreatedHandler NewScriptCreated;
		public event EventHandler ScriptsUpdated;

		public const string ChannelsScriptName = "ChannelHandlers";
        ScriptHost scriptHost;

		//ScriptEngine python = InitializePython();
		//List<Script> scripts = new List<Script>();
		ChannelsScriptHandlers channelsScriptHandlers = new ChannelsScriptHandlers();
        FScadaApplication application = new FScadaApplication();
        public Dictionary<string, string> scripts = new Dictionary<string, string>();
        public Assembly assembly;

		public IChannelsScriptHandlers ChannelsHandlers
		{
			get { return channelsScriptHandlers; }
		}

        public FScadaApplication ScriptApplication
		{
			get { return application; }
		}

        public IScriptHost ScriptHost
        {
            get { return scriptHost; }
        }
        public string[] Refrences
        {
            get { return scriptHost.References; }
            set {
                scriptHost.References = value;
            }
        }
        public string[] Sources
        {
            get 
            {
                string[] sources = new string[scripts.Count];
                scripts.Values.CopyTo(sources, 0);
                return sources;
            }
        }
        public string[] ScriptNames
        {
            get
            {
                string[] scriptNames = new string[scripts.Count];
                scripts.Keys.CopyTo(scriptNames, 0);
                return scriptNames;
            }
        }

		/// <summary>
		/// Initialize Script manager. This method should be called after all plugins are loaded. (In order to set right handlers for channels)
		/// </summary>
		public void Initialize()
		{
			Env.Current.Project.ProjectClosed += new EventHandler(OnProjectClosed);
			Env.Current.Project.ProjectLoaded += new EventHandler(OnProjectLoaded);
			Env.Current.Project.EntitySetChanged += new EventHandler(OnProjectEntitySetChanged);
            scriptHost = new ScriptHost(this);
		}

        public void SaveRefrence()
        {
            /*using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                if (ms.Length != 0)
                {
                    ms.SetLength(0);
                    ms.Seek(0, System.IO.SeekOrigin.Begin);
                }

                XmlDocument doc = new System.Xml.XmlDocument();
                XmlElement reference_elem = doc.CreateElement("referencelist");
                reference_elem.SetAttribute("referencelist", string.Join(";", Refrences));
                doc.AppendChild(reference_elem);
                doc.Save(ms);
                Env.Current.Project["settings\\referencelist"] = ms;
            }*/
            string content = string.Join(";", Refrences);
            using (MemoryStream stream = new MemoryStream())
            using (StreamWriter writer = new StreamWriter(stream))
            {
                writer.Write(content);
                writer.Flush();
                Env.Current.Project.SetData(ProjectEntityType.Settings, "referencelist", stream);
            }
        }

        public void Clear()
        {
            scripts.Clear();
            Refrences = null;
            scriptHost.AddReference("Common.dll");
            scriptHost.AddReference("System.dll");
            scriptHost.AddReference("System.Drawing.dll");
            scriptHost.AddReference("System.Xml.dll");
            scriptHost.AddReference("System.Windows.Forms.dll");
            scriptHost.AddReference("System.Xaml.dll");
            scriptHost.AddReference("Common.dll");
            scriptHost.AddReference("%wpf%PresentationCore.dll");
            scriptHost.AddReference("%wpf%PresentationFramework.dll");
            scriptHost.AddReference("%wpf%WindowsBase.dll");
        }

		void OnProjectEntitySetChanged(object sender, EventArgs e)
		{
			OnProjectClosed(sender, e);
			OnProjectLoaded(sender, e);
		}

		void OnProjectLoaded(object sender, EventArgs e)
		{
			Dictionary<string, string> scriptTexts = new Dictionary<string, string>();

			foreach (string name in Env.Current.Project.GetEntities(ProjectEntityType.Script))
			{
				string scriptText = "";
				using (Stream stream = Env.Current.Project.GetData(ProjectEntityType.Script, name))
				using (StreamReader reader = new StreamReader(stream))
				{
					string line;
					while ((line = reader.ReadLine()) != null)
					{
						if (scriptText.Length > 0)
							scriptText += "\n";
						scriptText += line;
					}
				}
				scriptTexts[name] = scriptText;
			}

			while (scriptTexts.Count > 0)
			{
                LoadScript(scriptTexts);
                break;
				//Get first script to load
				/*string scriptName = "";
				foreach(string key in scriptTexts.Keys)
				{
					scriptName = key;
					break;
				}

				if (!string.IsNullOrEmpty(scriptName))
				{
					if (LoadScript(scriptTexts, scriptName, new List<string>()) == false)
						return;
				}*/
			}

			channelsScriptHandlers.Load();
			channelsScriptHandlers.InstallHandlers();
            //load script referencelist
            using (Stream stream = Env.Current.Project.GetData(ProjectEntityType.Settings, "referencelist"))
                if (stream != null)
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        string referencelist = reader.ReadToEnd();
                        Refrences = referencelist.Split(';');
                    }
                }
            //compile after loaded.
            this.scriptHost.Compile(false);
		}

		/// <summary>
		/// Load scripts with checking for dependencies (e.g. script_1 depends on script_2, then script_2 should be loaded first)
		/// </summary>
		/// <param name="scriptTexts"></param>
		/// <param name="name"></param>
		/// <returns></returns>
		/*bool LoadScript(Dictionary<string, string> scriptTexts, string name, List<string> loadingStack)
		{
			if (loadingStack.Contains(name))
			{
				Env.Current.Logger.LogError(string.Format("Circular dependency of module {0}: {1}", name, loadingStack.ToString()));
				return false;
			}
			loadingStack.Add(name);

			List<string> modules = Script.GetImportedModules(scriptTexts[name]);
			foreach (string module in modules)
			{
				if (scriptTexts.ContainsKey(module))
				{
					if (LoadScript(scriptTexts, module, loadingStack) == false)
						return false;
				}
			}

			if (scriptTexts.ContainsKey(name))
			{
				Script script = new Script(scriptTexts[name], python, name);
				scripts.Add(script);
				scriptTexts.Remove(name);
			}

			return true;
		}*/

        bool LoadScript(Dictionary<string, string> scriptTexts)
        {
            scripts = scriptTexts;
            return true;
        }

		void OnProjectClosed(object sender, EventArgs e)
		{
			scripts.Clear();
            
		}

        /*
		static ScriptEngine InitializePython()
		{
			Dictionary<string, object> options = new Dictionary<string, object>();
			options["DivisionOptions"] = IronPython.PythonDivisionOptions.New;
			return Python.CreateEngine(options);
		}*/
        /*
		public Script CreateNewScript(string name)
		{
			using (MemoryStream stream = new MemoryStream())
			using (StreamWriter writer = new StreamWriter(stream))
			{
				writer.WriteLine("");
				writer.Flush();
				Env.Current.Project.SetData(ProjectEntityType.Script, name, stream);

				//Script script = new Script("", python, name);
				//scripts.Add(script);
                scripts[name] = "";

				if(NewScriptCreated != null)
					NewScriptCreated(this, name);

				if (ScriptsUpdated != null)
					ScriptsUpdated(this, new EventArgs());

				return script;
			}
		}*/

        /// <summary>
        /// Load scripts with checking for dependencies (e.g. script_1 depends on script_2, then script_2 should be loaded first)
        /// </summary>
        /// <param name="name">sciprt name</param>
        /// <param name="show">show it in SharpCodeView after created</param>
        /// <param name="schemaevent">event of schematic</param>
        /// <returns></returns>
        public void CreateNewScript(string name, bool show = true, bool schemaevent = false)
        {
            using (MemoryStream stream = new MemoryStream())
            using (StreamWriter writer = new StreamWriter(stream))
            {
                writer.WriteLine("");
                writer.Flush();
                Env.Current.Project.SetData(ProjectEntityType.Script, name, stream);

                //Script script = new Script("", python, name);
                //scripts.Add(script);
                if (name == "global")
                {
                    scripts[name] = File.ReadAllText("global.txt");
                }
                else
                {
                    scripts[name] = "";
                }
                
                if (NewScriptCreated != null)
                    NewScriptCreated(this, name);       //notify new script created, usually open new script
                //notify other view for script updated
                if (ScriptsUpdated != null)
                    ScriptsUpdated(this, new EventArgs());

                //return script;
            }
        }

        public bool ContainsScript(string name)
        {
            return scripts.ContainsKey(name);
        }

        /*
		public Script GetScript(string name)
		{
			foreach (Script s in scripts)
			{
				if (s.Name == name)
					return s;
			}

			return null;
		}*/
        public string GetScriptText(string name)
        {
            string ret = null;
            if (scripts.ContainsKey(name))
                ret = scripts[name];

            return ret;
        }

        public MethodInfo GetMethod(string module, string name)
        {
            MethodInfo method = null;
            if (assembly != null)
            {
                Type type = assembly.GetType(module);
                if (type != null)
                    method = type.GetMethod(name);
            }
            return method;
        }

        public System.Type GetClassByName(string strType)
        {
            Type type = null;
            if (assembly != null)
            {
                type = assembly.GetType(strType);
            }
            return type;
        }

        public void InvokeMethod(string strType, string strMethod)
        {
            if (assembly != null)
            {
                Type type = assembly.GetType(strType);
                if (type != null)
                {
                    MethodInfo method = type.GetMethod(strMethod);
                    method.Invoke(null, null);
                }
            }
        }

        public void Save(string strName, string source)
        {
            scripts[strName] = source;
            using (MemoryStream stream = new MemoryStream())
            using (StreamWriter writer = new StreamWriter(stream))
            {
                writer.Write(source);
                writer.Flush();
                Env.Current.Project.SetData(ProjectEntityType.Script, strName, stream);
            }
        }

        public void Save()
        {
            foreach (KeyValuePair<string,string> pair in scripts)
            {
                Save(pair.Key,pair.Value);
            }
        }

        public void Remove(string strName)
        {
            scripts.Remove(strName);
        }
	}
}
