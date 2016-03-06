using System;
using System.Collections.Generic;
using System.Text;
using System.CodeDom;

using TriadCompiler.Code.Generator;

namespace TriadCompiler
{
    /// <summary>
    /// ����� ��� ���������� ����������
    /// </summary>
    public class CompilerFacade
    {
        /// <summary>
        /// ��� �������������� �������
        /// </summary>
        private static string designTypeName = "";
        /// <summary>
        /// ���������� �������������� ���������� �� �������
        /// </summary>
        private static bool showExtendedErrorInfo = true;

        private static bool needClearArea = true;


        /// <summary>
        /// ��� �������������� �������
        /// </summary>
        public static string DesignTypeName
        {
            get
            {
                return designTypeName;
            }
        }


        /// <summary>
        /// ���������� �������������� ���������� �� �������
        /// </summary>
        public static bool ShowExtendedErrorInfo
        {
            get
            {
                //���� ����� ��� �������� �� ������������ ������, �� ����� ������������
                return showExtendedErrorInfo;
            }
            set
            {
                showExtendedErrorInfo = value;
                Fabric.Instance.ErrReg.PrintAllowedKeys = value;
            }
        }

        /// <summary>
        /// ���������� �������� ������� ����������
        /// </summary>
        public static bool NeedClearArea
        {
            get
            {
                return needClearArea;
            }
            set
            {
                needClearArea = value;
            }
        }


        /// <summary>
        /// ��������� ����������
        /// </summary>
        /// <param name="io">����-�����</param>
        /// <param name="builderMode">����� ����������</param>
        /// <param name="codeFormat">������ ��������������� ����</param>
        /// <param name="fileName">��� �����</param>
        private static void ConfigureCompiler(IO io, CodeBuilderMode builderMode, CodeFormat codeFormat, string fileName)
        {
            //��������� ����� ������� ����������
            if (needClearArea)
                CommonArea.CreateNewArea();

            Fabric.IO = io;
            CodeFabric.ReloadFabric(codeFormat);
            Fabric.ReloadFabric(builderMode);

            EndKeyList endKey = new EndKeyList();
            endKey = endKey.UniteWith(Key.EndOfFile);
            Fabric.Instance.Parser.Compile(endKey);
            designTypeName = Fabric.Instance.Parser.DesignTypeName;

            Fabric.Instance.Builder.Build();

            //���� ��� ������, �� ���������� ���
            if (Fabric.Instance.ErrReg.ErrorCount == 0)
                CodeFabric.Instance.GenerateCode(fileName);

            io.GetCh();
            needClearArea = true;
        }


        /// <summary>
        /// �������������� ������ � ��������� ����
        /// </summary>
        ///<param name="io">����-�����</param>
        ///<param name="fileName">��� ����� ��� ������ � ���� ����</param>
        public static void CompileModelToTxt(IO io, string fileName)
        {
            ConfigureCompiler(io, CodeBuilderMode.BuildModel, CodeFormat.Txt, fileName);
        }


        /// <summary>
        /// �������������� ������ � dll
        /// </summary>
        ///<param name="io">����-�����</param>
        ///<param name="fileName">��� ����� ��� ������ � ���� ����</param>
        public static void CompileModelToDll(IO io, string fileName)
        {
            ConfigureCompiler(io, CodeBuilderMode.BuildModel, CodeFormat.Dll, fileName);
        }


        /// <summary>
        /// �������������� ������ � ��������� ����
        /// </summary>
        ///<param name="io">����-�����</param>
        ///<param name="fileName">��� ����� ��� ������ � ���� ����</param>
        public static void CompileRoutineToTxt(IO io, string fileName)
        {
            ConfigureCompiler(io, CodeBuilderMode.BuildRoutine, CodeFormat.Txt, fileName);
        }


        /// <summary>
        /// �������������� ������ � dll
        /// </summary>
        /// <param name="io">����-�����</param>
        /// <param name="fileName">��� ����� ��� ������ � ���� ����</param>
        public static void CompileRoutineToDll(IO io, string fileName)
        {
            ConfigureCompiler(io, CodeBuilderMode.BuildRoutine, CodeFormat.Dll, fileName);
        }


        /// <summary>
        /// �������������� ��������� � txt
        /// </summary>
        /// <param name="io">����-�����</param>
        /// <param name="fileName">��� ����� ��� ������ � ���� ����</param>
        public static void CompileStructureToTxt(IO io, string fileName)
        {
            ConfigureCompiler(io, CodeBuilderMode.BuildStructure, CodeFormat.Txt, fileName);
        }


        /// <summary>
        /// �������������� ��������� � dll
        /// </summary>
        /// <param name="io">����-�����</param>
        /// <param name="fileName">��� ����� ��� ������ � ���� ����</param>
        public static void CompileStructureToDll(IO io, string fileName)
        {
            ConfigureCompiler(io, CodeBuilderMode.BuildStructure, CodeFormat.Dll, fileName);
        }

        /// <summary>
        /// �������������� �������������� ��������� � ��������� ����
        /// </summary>
        ///<param name="io">����-�����</param>
        ///<param name="fileName">��� ����� ��� ������ � ���� ����</param>
        public static void CompileIProcedureToTxt(IO io, string fileName)
        {
            ConfigureCompiler(io, CodeBuilderMode.BuildIProcedure, CodeFormat.Txt, fileName);
        }

        /// <summary>
        /// �������������� �������������� ��������� � dll
        /// </summary>
        /// <param name="io">����-�����</param>
        /// <param name="fileName">��� ����� ��� ������ � ���� ����</param>
        public static void CompileIProcedureToDll(IO io, string fileName)
        {
            ConfigureCompiler(io, CodeBuilderMode.BuildIProcedure, CodeFormat.Dll, fileName);
        }

        /// <summary>
        /// �������������� ������� ������������� � ��������� ����
        /// </summary>
        ///<param name="io">����-�����</param>
        ///<param name="fileName">��� ����� ��� ������ � ���� ����</param>
        public static void CompileIConditionToTxt(IO io, string fileName)
        {
            ConfigureCompiler(io, CodeBuilderMode.BuildICondition, CodeFormat.Txt, fileName);
        }

        /// <summary>
        /// �������������� ������� ������������� � dll
        /// </summary>
        /// <param name="io">����-�����</param>
        /// <param name="fileName">��� ����� ��� ������ � ���� ����</param>
        public static void CompileIConditionToDll(IO io, string fileName)
        {
            ConfigureCompiler(io, CodeBuilderMode.BuildICondition, CodeFormat.Dll, fileName);
        }

        /// <summary>
        /// �������������� ������ � ��������� ����
        /// </summary>
        ///<param name="io">����-�����</param>
        ///<param name="fileName">��� ����� ��� ������ � ���� ����</param>
        public static void CompileDesignToTxt(IO io, string fileName)
        {
            ConfigureCompiler(io, CodeBuilderMode.BuildDesign, CodeFormat.Txt, fileName);
        }

        /// <summary>
        /// �������������� ������ � dll
        /// </summary>
        /// <param name="io">����-�����</param>
        /// <param name="fileName">��� ����� ��� ������ � ���� ����</param>
        public static void CompileDesignToDll(IO io, string fileName)
        {
            ConfigureCompiler(io, CodeBuilderMode.BuildDesign, CodeFormat.Dll, fileName);
        }

        /// <summary>
        /// �������������� ������
        /// </summary>
        ///<param name="io">����-�����</param>
        ///<param name="fileName">��� ����� � �������� �������</param>
        public static void TestModel(IO io, string fileName)
        {
            ConfigureCompiler(io, CodeBuilderMode.TestModel, CodeFormat.Memory, fileName);
        }


        /// <summary>
        /// �������������� ������
        /// </summary>
        /// <param name="io">����-�����</param>
        /// <param name="sourceFileName">��� ����� � �������� �������</param>
        public static void TestRoutine(IO io, string sourceFileName)
        {
            ConfigureCompiler(io, CodeBuilderMode.TestRoutine, CodeFormat.Memory, sourceFileName);
        }


        /// <summary>
        /// �������������� ���������
        /// </summary>
        /// <param name="io">����-�����</param>
        /// <param name="sourceFileName">��� ����� � �������� �������</param>
        public static void TestStructure(IO io, string sourceFileName)
        {
            ConfigureCompiler(io, CodeBuilderMode.TestStructure, CodeFormat.Memory, sourceFileName);
        }


        /// <summary>
        /// �������������� �������������� ���������
        /// </summary>
        /// <param name="io">����-�����</param>
        /// <param name="sourceFileName">��� ����� � �������� �������</param>
        public static void TestIProcedure(IO io, string sourceFileName)
        {
            ConfigureCompiler(io, CodeBuilderMode.TestIProcedure, CodeFormat.Memory, sourceFileName);
        }


        /// <summary>
        /// �������������� ������� �������������
        /// </summary>
        /// <param name="io">����-�����</param>
        /// <param name="sourceFileName">��� ����� � �������� �������</param>
        public static void TestICondition(IO io, string sourceFileName)
        {
            ConfigureCompiler(io, CodeBuilderMode.TestICondition, CodeFormat.Memory, sourceFileName);
        }


        /// <summary>
        /// �������������� ������
        /// </summary>
        /// <param name="io">����-�����</param>
        /// <param name="sourceFileName">��� ����� � �������� �������</param>
        public static void TestDesign(IO io, string sourceFileName)
        {
            ConfigureCompiler(io, CodeBuilderMode.TestDesign, CodeFormat.Memory, sourceFileName);
        }

        //by jum
        /// <summary>
        /// ���������� � ����������� �������
        /// </summary>
        /// <returns></returns>
        public static DesignTypeInfo GetDesignTypeInfo()
        {
            return Fabric.Instance.Parser.DesignTypeInfo;
        }

    }
}
