using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace INISIC
{
    class Program
    {
        static Dictionary<string, string> iniLaunchDic = new Dictionary<string, string>();
        static Dictionary<string, string> iniDisplayDic = new Dictionary<string, string>();
        static string iniPath = "";


        static void Main(string[] args)
        {
            Console.WriteLine("作者AldarisX(bilibili)\nver0.0.2");
            iniPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\My Games\Skyrim\SkyrimPrefs.ini";
            if (File.Exists(iniPath))
            {
                File.SetAttributes(iniPath, FileAttributes.Normal);
                addIni();

                foreach (string key in iniLaunchDic.Keys)
                {
                    Kernel32.WriteIniKeys("Launch", key, iniLaunchDic[key], iniPath);
                }

                foreach (string key in iniDisplayDic.Keys)
                {
                    Kernel32.WriteIniKeys("Display", key, iniDisplayDic[key], iniPath);
                }


                if (checkIni())
                {
                    MessageBox.Show("很遗憾,没有成功\n看看是不是配置文件为只读");
                }
                else
                {
                    MessageBox.Show("修改完成");
                }

                //Console.WriteLine(Kernel32.ReadString("Launch", "bEnableFileSelection", "null", iniPath));
            }
            else
            {
                MessageBox.Show("ini不存在");
            }
        }

        public static void addIni()
        {
            iniLaunchDic.Add("bEnableFileSelection", "1");

            iniDisplayDic.Add("iMaxAnisotropy", "0");
            iniDisplayDic.Add("fGamma", "1.000");
            iniDisplayDic.Add("iWaterMultiSamples", "0");
            iniDisplayDic.Add("iMultiSample", "0");
            iniDisplayDic.Add("bTreesReceiveShadows","1");
            iniDisplayDic.Add("bDrawLandShadows", "1");
            iniDisplayDic.Add("bFloatPointRenderTarget", "1");
            iniDisplayDic.Add("bFXAAEnabled", "0");
            iniDisplayDic.Add("bShadowsOnGrass", "1");
            iniDisplayDic.Add("bTransparencyMultisampling", "0");
            iniDisplayDic.Add("bDeferredShadows", "1");
            iniDisplayDic.Add("bDrawShadows", "1");
        }

        public static bool checkIni()
        {
            bool isFail = false;
            foreach (string key in iniLaunchDic.Keys)
            {
                if (!isFail)
                {
                    if (Kernel32.ReadString("launch", key, "null", iniPath) != iniLaunchDic[key])
                    {
                        isFail = true;
                    }
                }

            }

            foreach (string key in iniDisplayDic.Keys)
            {
                if (!isFail)
                {
                    if (Kernel32.ReadString("display", key, "null", iniPath) != iniDisplayDic[key])
                    {
                        isFail = true;
                    }
                }
            }

            return isFail;
        }
    }
}
