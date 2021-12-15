using FreeSCADA.Interfaces;
using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Reflection;

namespace FreeSCADA.Common.Scripting
{
    public class ScriptHost : IScriptHost
    {
        CSharpCodeProvider codeDomProvider = new CSharpCodeProvider();
        CompilerParameters compilerParameters = new CompilerParameters();
        string[] references;
        ScriptManager scriptsManager;
        //Module[] modules = assembly.GetModules();
        string wpfPath = Environment.ExpandEnvironmentVariables("%SystemRoot%") + "\\" + Env.WPFPath;
        ArrayList types = new ArrayList();
        public string[] Types
        {
            get
            {
                string[] rets = new string[types.Count];
                for (int i = 0; i < types.Count; i++)
                {
                    Type t = (Type)types[i];
                    rets[i] = t.FullName;
                }
                return rets;
            }
        }
        //ArrayList references = new ArrayList();
        public string[] References
        {
            get { return references; }
            set { references = value; }
        }
        bool isCompiled = false;
        public bool IsCompiled
        {
            get{ return isCompiled; }
        }
        string compilerInfo;
        public string CompilerInfo
        {
            get { return compilerInfo; }
        }
        CompilerErrorCollection compilerErrors;
        public CompilerErrorCollection CompilerErrors
        {
            get { return compilerErrors; }
        }
        string sourceText;
        public string SourceText
        {
            get { return sourceText; }
            set { sourceText = value; }
        }
        string compilerOptions;
        public string CompilerOptions
        {
            get { return compilerOptions; }
            set { compilerOptions = value; }
        }

        public ScriptHost(ScriptManager scriptsManager)
        {
            compilerParameters.GenerateExecutable = false;          // 生成类库
            compilerParameters.GenerateInMemory = true;            // 保存为文件
            compilerParameters.TreatWarningsAsErrors = false;       // 不将编译警告作为错误
            compilerParameters.OutputAssembly = Path.Combine(System.Environment.CurrentDirectory + "\\DLLCode", "RunTimeUserCode.dll");
            this.scriptsManager = scriptsManager;
        }
        public string[] GetMethods(string type)
        {
            MethodInfo[] methodInfos = MethodInfos(type);
            int size = methodInfos.Length;
            string[] methodNames = new string[size];
            for(int i=0; i<size; i++)
            {
                methodNames[i] = methodInfos[i].Name;
            }
            return methodNames;
        }

        public System.Reflection.MethodInfo[] MethodInfos(string type)
        {
            Type tp = scriptsManager.assembly.GetType(type);
            MethodInfo[] methodInfos = tp.GetMethods();
            return methodInfos;
        }

        public void AddReference(string __strAssemblyName)
        {
            if (references != null)
            {
                string[] array_new = new string[references.Length + 1];
                Array.Copy(references, array_new, references.Length);
                array_new[array_new.Length - 1] = __strAssemblyName;
                references = array_new;
            }
            else
            {
                references = new string[1];
                references[0] = __strAssemblyName;
            }
            //__strAssemblyName = __strAssemblyName.Replace("%wpf%", wpfPath);
            //compilerParameters.ReferencedAssemblies.Add(__strAssemblyName);
        }

        public void AddReferenceAssemblyByType(System.Type SourceType)
        {
            Assembly assembly = Assembly.GetAssembly(SourceType);
            AddReference(assembly.FullName);
        }

        public bool Compile(bool bsave)
        {
            if (bsave == true)
            {   //save text
            }
            isCompiled = false;
            if (codeDomProvider != null)
            {
                compilerParameters.ReferencedAssemblies.Clear();
                if (references != null)
                {
                    foreach (string val in references)
                    {
                        string str = val.Replace("%wpf%", wpfPath);
                        compilerParameters.ReferencedAssemblies.Add(str);
                    }
                }
                string[] fileNames = new string[scriptsManager.scripts.Count];
                int i = 0;
                foreach (KeyValuePair <string, string> pair in scriptsManager.scripts)
                {
                    fileNames[i] = Path.Combine(System.Environment.CurrentDirectory + "\\DLLCode", pair.Key);
                    fileNames[i] = Path.ChangeExtension(fileNames[i], ".cs");
                    StreamWriter streamWriter = File.CreateText(fileNames[i]);
                    streamWriter.Write(pair.Value);
                    streamWriter.Close();
                    i++;
                }
                //CompilerResults compilerResults = codeDomProvider.CompileAssemblyFromSource(compilerParameters, scriptsManager.Sources);
                CompilerResults compilerResults = codeDomProvider.CompileAssemblyFromFile(compilerParameters, fileNames);
                compilerErrors = compilerResults.Errors;
                if (compilerResults.Errors.Count < 1)
                {
                    compilerErrors.Clear();
                    //Assembly asm = Assembly.Load(asmName);      // 加载
                    scriptsManager.assembly = compilerResults.CompiledAssembly;      // 加载
                    //DoSomething();                            //调用
                    types.Clear();
                    types.AddRange(scriptsManager.assembly.GetTypes());
                    compilerInfo = "";
                    isCompiled = true;
                }
                else
                {
                    compilerInfo = "";
                    foreach (CompilerError compilerError in compilerErrors)
                    {
                        compilerInfo += compilerError.FileName 
                            + "(" + compilerError.Line.ToString()
                            + "," + compilerError.Column.ToString()
                            + ") :" + (compilerError.IsWarning ? " warning " : " error ")
                            + compilerError.ErrorNumber + ": " 
                            + compilerError.ErrorText + "\r\n";
                    }
                    isCompiled = false;
                }
                foreach(string fileName in fileNames)
                {
                    File.Delete(fileName);
                }
            }
            return isCompiled;
        }

        public void Invoke(string _strModule, string _strMethod)
        {
            MethodInfo method = null;
            if (isCompiled)
            {
                Module module = scriptsManager.assembly.GetModule(_strModule);
                method = module.GetMethod(_strMethod);
                method.Invoke(null, null);
            }
        }

        public System.Reflection.MethodInfo GetMethod(string _strModule, string _strMethod)
        {
            MethodInfo method = null;
            if (isCompiled)
            {
                Module module = scriptsManager.assembly.GetModule(_strModule);
                method = module.GetMethod(_strMethod);
            }
            return method;
        }

        public System.Type GetClassType(string strType)
        {
            return scriptsManager.assembly.GetType(strType);
        }

        public void Invoke(string _strModule, string _strMethod, object[] args)
        {
             if (isCompiled)
            {
                Module module = scriptsManager.assembly.GetModule(_strModule);
                MethodInfo method = module.GetMethod(_strMethod);
                method.Invoke(null, args);
            }
        }

        public object FullInvoke(string __strModule, string __strMethod, object[] __Arguments)
        {
            object obj = null;
            if (isCompiled)
            {
                Module module = scriptsManager.assembly.GetModule(__strModule);
                MethodInfo method = module.GetMethod(__strMethod);
                obj = method.Invoke(null, __Arguments);
            }
            return obj;
        }

        public System.Type[] GetTypes()
        {
            Type[] rets = new Type[types.Count];
            for (int i = 0; i < types.Count; i++)
            {
                rets[i] = (Type)types[i];
            }
            return rets;
        }

        public System.Reflection.Assembly GetAssembly()
        {
            return scriptsManager.assembly;
        }

        public MethodInfo GetMethod(string __strMethod)
        {
            MethodInfo method = null;
            if (isCompiled)
            {
                Type type = scriptsManager.assembly.GetType("RunTime.Functions");
                //Module module = assembly.GetModule("Runtime.exe");
                //Type type = Type.GetType("RunTime.Functions");
                //method = module.GetMethod(__strMethod);
                method = type.GetMethod(__strMethod);
            }
            return method;
        }
    }
}
