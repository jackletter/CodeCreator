using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using System.IO;
using System.Reflection;
using System.Collections;

namespace CodeCreator
{
    public class DynamicCreator
    {
        public static string Create(string srcCodePath, string ClassFullName, string CreateMethod)
        {
            System.CodeDom.Compiler.CompilerParameters parameters = new System.CodeDom.Compiler.CompilerParameters();
            string srcCode = "";
            ParseSrc(srcCodePath, ref srcCode, parameters);
            parameters.GenerateExecutable = false;
            parameters.GenerateInMemory = true;
            using (var provider = new CSharpCodeProvider())
            {
                System.CodeDom.Compiler.ICodeCompiler compiler = provider.CreateCompiler();
                CompilerResults results = compiler.CompileAssemblyFromSource(parameters, srcCode);
                CompilerErrorCollection errorcollection = results.Errors;
                foreach (CompilerError error in errorcollection)
                {
                    if (error.IsWarning == true)
                    {
                        WriteLog("Line: " + error.Line.ToString() + " Warning Number: " + error.ErrorNumber + " Warning Message: " + error.ErrorText);
                    }
                    else if (error.IsWarning == false)
                    {
                        WriteLog("Line: " + error.Line.ToString() + " Error Number: " + error.ErrorNumber + " Error Message: " + error.ErrorText);
                    }
                    return "error";
                }
                Assembly asm = results.CompiledAssembly;
                object objSrcCreateClass = asm.CreateInstance(ClassFullName);
                Type SrcCreateType = objSrcCreateClass.GetType();
                object obj = SrcCreateType.GetMethod(CreateMethod).Invoke(objSrcCreateClass, new object[] { (Hashtable)DataTransfer.data });
                return obj.ToString();
            }

        }

        public static void ParseSrc(string srcCodePath, ref string srcCode, CompilerParameters parameters)
        {
            string[] lines = File.ReadAllLines(srcCodePath);
            srcCode = "";
            bool b = true;
            foreach (var item in lines)
            {
                string tmp = item.Trim(' ');
                if (tmp.StartsWith("//#import"))
                {
                    if (b)
                    {
                        tmp = tmp.Substring(9).Trim(' ');
                        parameters.ReferencedAssemblies.Add(tmp);
                    }
                }
                else
                {
                    b = false;
                    srcCode += "\r\n" + tmp;
                }
            }
        }

        public static void WriteLog(string mess)
        {
            if (!Directory.Exists("logs"))
            {
                Directory.CreateDirectory("logs");
            }
            string fileName = DateTime.Now.ToString("yyyy-MM-dd") + ".log";
            File.AppendAllText("logs/" + fileName, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")+":"+ mess+"\r\n");
        }
    }
}
