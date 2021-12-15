using System;
using System.CodeDom.Compiler;
using System.Reflection;
namespace FreeSCADA.Interfaces
{
	public interface IScriptHost
	{
		string[] Types
		{
			get;
		}
		string[] References
		{
			get;
			set;
		}
		bool IsCompiled
		{
			get;
		}
		string CompilerInfo
		{
			get;
		}
		CompilerErrorCollection CompilerErrors
		{
			get;
		}
		string SourceText
		{
			get;
			set;
		}
		string CompilerOptions
		{
			get;
			set;
		}
		string[] GetMethods(string type);
		System.Reflection.MethodInfo[] MethodInfos(string type);
		void AddReference(string __strAssemblyName);
		void AddReferenceAssemblyByType(System.Type SourceType);
		bool Compile(bool bsave);
		void Invoke(string _strModule, string _strMethod);
		System.Reflection.MethodInfo GetMethod(string _strModule, string _strMethod);
        System.Reflection.MethodInfo GetMethod(string _strMethod);
		System.Type GetClassType(string strType);
		void Invoke(string _strModule, string _strMethod, object[] args);
		object FullInvoke(string __strModule, string __strMethod, object[] __Arguments);
		System.Type[] GetTypes();
		System.Reflection.Assembly GetAssembly();
	}
}
