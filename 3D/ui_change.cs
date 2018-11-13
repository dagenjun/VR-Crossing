using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Data;
using System.IO;
using System.Text;
using System;

public class ui_change : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public static int flag1 = 0;

    public static int flag2 = 0;

    public static int flag3 = 0;

    public static int flag4 = 0;

    public static int flag5 = 0;

    //public static int flag6 = 0;

    public static string exp_type;

    /// <summary>
    /// 使用者姓名
    /// </summary>
    public static string Name = "hss";

    public static bool is_initial_table = false;

    public static int block = 0;

    

    public void GotoScene_ui1()
    {

        if (is_initial_table == false)
        {
            import_datatable();
        }

        exp_type = "C/DC";

        

        if (flag1 == -1)
        {

            return;
        }
        else if (flag1 == 0)
        {

            flag1 = 1;

            block++;

            //第三个block将C/OC按钮移动到视野外，并将下一个按钮（D/CC）移动到视野内
            /*
            if (block == 3)
            {

                Ccc_remove();
                Dcc_refresh();

            }
            */

            end_time.random_n(end_time.D);

            Debug.Log(end_time.newNum[end_time.D - 1]);


            SceneManager.LoadScene("ui1");



        }

    }

    public void GotoScene_ui2()
    {
        if (is_initial_table == false)
        {
            import_datatable();
        }

        exp_type = "D/AC";


        if (flag2 == -1)
        {

            return;
        }
        else if (flag2 == 0)
        {

            flag2 = 1;

            block++;

            //第三个block将D/CC按钮移动到视野外，并将下一个按钮（CP）移动到视野内
            /*
            if (block == 3)
            {
                Dcc_remove();
                Cp_refresh();
               
            }
            */

            end_time.random_n(end_time.D);

            Debug.Log(end_time.newNum[end_time.D - 1]);

            SceneManager.LoadScene("ui2");

        }
    }

    public void GotoScene_ui3()
    {
        if (is_initial_table == false)
        {
            import_datatable();
        }

        exp_type = "A/DP";
        if (flag3 == -1)
        {

            return;
        }
        else if (flag3 == 0)
        {

            flag3 = 1;

            block++;

            //第三个block将CP按钮移动到视野外，并将下一个按钮（D/OC）移动到视野内
            /*
            if (block == 3)
            {
                Cp_remove();
                Doc_refresh();

            }
            */

            end_time.random_n(end_time.D);

            Debug.Log(end_time.newNum[end_time.D - 1]);

            SceneManager.LoadScene("ui3");

        }
    }

    public void GotoScene_ui4()
    {
        if (is_initial_table == false)
        {
            import_datatable();
        }

        exp_type = "D/DC";
        if (flag4 == -1)
        {

            return;
        }
        else if (flag4 == 0)
        {

            flag4 = 1;

            block++;

            //第三个block将D/OC按钮移动到视野外，并将下一个按钮（C/CC）移动到视野内
            /*
            if (block == 3)
            {

                Doc_remove();
                Ccc_refresh();

            }
            */


            end_time.random_n(end_time.D);

            Debug.Log(end_time.newNum[end_time.D - 1]);

            SceneManager.LoadScene("ui4");

        }
    }

    public void GotoScene_ui5()
    {

        if (is_initial_table == false)
        {
            import_datatable();
        }

        exp_type = "C/AC";
        if (flag5 == -1)
        {

            return;
        }
        else if (flag5 == 0)
        {

            flag5 = 1;

            block++;

            //第三个block将C/CC按钮移动到视野外，并将下一个按钮（OP）移动到视野内
            /*
            if (block == 3)
            {

                Ccc_remove();
                Op_refresh();

            }
            */

            end_time.random_n(end_time.D);

            Debug.Log(end_time.newNum[end_time.D - 1]);

            SceneManager.LoadScene("ui5");

        }
    }

    /*
    public void GotoScene_ui6()
    {
        exp_type = "OP";
        if (flag6 == -1)
        {

            return;
        }
        else if (flag6 == 0)
        {

            flag6 = 1;

            block++;

            //第三个block将OP按钮移动到视野外，并将下一个按钮（结束按钮）移动到视野内
            
          
            

            end_time.random_n(end_time.D);

            Debug.Log(end_time.newNum[end_time.D - 1]);

            SceneManager.LoadScene("ui6");

        }
    }
    */


    /// <summary>  
    /// 导出文件，使用文件流。该方法使用的数据源为DataTable,导出的Excel文件没有具体的样式。  
    /// </summary>  
    /// <param name="dt"></param>  
    public static string ExportToExcel(System.Data.DataTable dt, string path)
    {
        KillSpecialExcel();
        string result = string.Empty;
        try
        {
            // 实例化流对象，以特定的编码向流中写入字符。  
            StreamWriter sw = new StreamWriter(path, false, Encoding.GetEncoding("gb2312"));

            StringBuilder sb = new StringBuilder();
            for (int k = 0; k < dt.Columns.Count; k++)
            {
                // 添加列名称  
                sb.Append(dt.Columns[k].ColumnName.ToString() + "\t");
            }
            sb.Append(Environment.NewLine);
            // 添加行数据  
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow row = dt.Rows[i];
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    // 根据列数追加行数据  
                    sb.Append(row[j].ToString() + "\t");
                }
                sb.Append(Environment.NewLine);
            }
            sw.Write(sb.ToString());
            sw.Flush();
            sw.Close();
            sw.Dispose();

            // 导出成功后打开  
            //System.Diagnostics.Process.Start(path);  
        }
        catch (Exception)
        {
            result = "请保存或关闭可能已打开的Excel文件";
        }
        finally
        {
            dt.Dispose();
        }
        return result;
    }
    /// <summary>  
    /// 结束进程  
    /// </summary>  
    private static void KillSpecialExcel()
    {
        foreach (System.Diagnostics.Process theProc in System.Diagnostics.Process.GetProcessesByName("EXCEL"))
        {
            if (!theProc.HasExited)
            {
                bool b = theProc.CloseMainWindow();
                if (b == false)
                {
                    theProc.Kill();
                }
                theProc.Close();
            }
        }
    }

    /// <summary>
    /// 将表中数据导入到excel中
    /// </summary>
    public static void export_data()
    {

        if (exp_type == "C/DC")
        {
            ExportToExcel(end_time.dt, "D:\\正式实验数据\\3D实验数据\\CDC.xls");
        }
        if (exp_type == "D/AC")
        {
            ExportToExcel(end_time.dt, "D:\\正式实验数据\\3D实验数据\\DAC.xls");
        }
        if (exp_type == "A/DP")
        {
            ExportToExcel(end_time.dt, "D:\\正式实验数据\\3D实验数据\\ADP.xls");
        }
        if (exp_type == "D/DC")
        {
            ExportToExcel(end_time.dt, "D:\\正式实验数据\\3D实验数据\\DDC.xls");
        }
        if (exp_type == "C/AC")
        {
            ExportToExcel(end_time.dt, "D:\\正式实验数据\\3D实验数据\\CAC.xls");
        }
    }

    /// <summary>
    /// 初始化表格用来记录数据
    /// </summary>
    public static void import_datatable()
    {

        
        is_initial_table = true;
        end_time.dt.Columns.Add("Name", typeof(string));
        end_time.dt.Columns.Add("Exp_type", typeof(string));
        end_time.dt.Columns.Add("Block", typeof(int));
        end_time.dt.Columns.Add("Count", typeof(int));
        end_time.dt.Columns.Add("depth", typeof(float));
        end_time.dt.Columns.Add("D", typeof(float));
        end_time.dt.Columns.Add("W", typeof(float));
        end_time.dt.Columns.Add("ID", typeof(float));
        end_time.dt.Columns.Add("angle", typeof(float));
        //end_time.dt.Columns.Add ("Z", typeof(float));
        end_time.dt.Columns.Add("Time", typeof(int));
        end_time.dt.Columns.Add("Error", typeof(int));
        end_time.dt.Columns.Add("Before_time", typeof(int));
        end_time.dt.Columns.Add("After_time", typeof(int));
        Debug.Log("success");





        
    }


















    // C/OC实验类型的按钮出现在视野里
    public static void Coc_refresh()
    {

        GameObject btn1_btnObj = GameObject.Find("btn1");
        RectTransform btn1btn_pos = btn1_btnObj.GetComponent<RectTransform>();
        btn1btn_pos.anchoredPosition3D = new Vector3(0, 0, 0);

    }

    // C/OC实验类型的按钮消失在视野里
    public static void Coc_remove()
    {

        GameObject btn1_btnObj = GameObject.Find("btn1");
        RectTransform btn1btn_pos = btn1_btnObj.GetComponent<RectTransform>();
        btn1btn_pos.anchoredPosition3D = new Vector3(30000, 0, 0);

    }




    // D/CC实验类型的按钮出现在视野里
    public static void Dcc_refresh()
    {

        GameObject btn2_btnObj = GameObject.Find("btn2");
        RectTransform btn2btn_pos = btn2_btnObj.GetComponent<RectTransform>();
        btn2btn_pos.anchoredPosition3D = new Vector3(0, 0, 0);

    }

    // D/CC实验类型的按钮消失在视野里
    public static void Dcc_remove()
    {

        GameObject btn2_btnObj = GameObject.Find("btn2");
        RectTransform btn2btn_pos = btn2_btnObj.GetComponent<RectTransform>();
        btn2btn_pos.anchoredPosition3D = new Vector3(-30000, 0, 0);

    }




    // CP实验类型的按钮出现在视野里
    public static void Cp_refresh()
    {

        GameObject btn3_btnObj = GameObject.Find("btn3");
        RectTransform btn3btn_pos = btn3_btnObj.GetComponent<RectTransform>();
        btn3btn_pos.anchoredPosition3D = new Vector3(0, 0, 0);

    }

    // CP实验类型的按钮消失在视野里
    public static void Cp_remove()
    {

        GameObject btn3_btnObj = GameObject.Find("btn3");
        RectTransform btn3btn_pos = btn3_btnObj.GetComponent<RectTransform>();
        btn3btn_pos.anchoredPosition3D = new Vector3(-30000, 0, 0);

    }




    // D/OC实验类型的按钮出现在视野里
    public static void Doc_refresh()
    {

        GameObject btn4_btnObj = GameObject.Find("btn4");
        RectTransform btn4btn_pos = btn4_btnObj.GetComponent<RectTransform>();
        btn4btn_pos.anchoredPosition3D = new Vector3(0, 0, 0);

    }

    // D/OC实验类型的按钮消失在视野里
    public static void Doc_remove()
    {

        GameObject btn4_btnObj = GameObject.Find("btn4");
        RectTransform btn4btn_pos = btn4_btnObj.GetComponent<RectTransform>();
        btn4btn_pos.anchoredPosition3D = new Vector3(-30000, 0, 0);

    }




    // C/CC实验类型的按钮出现在视野里
    public static void Ccc_refresh()
    {

        GameObject btn5_btnObj = GameObject.Find("btn5");
        RectTransform btn5btn_pos = btn5_btnObj.GetComponent<RectTransform>();
        btn5btn_pos.anchoredPosition3D = new Vector3(0, 0, 0);

    }

    // C/CC实验类型的按钮消失在视野里
    public static void Ccc_remove()
    {

        GameObject btn5_btnObj = GameObject.Find("btn5");
        RectTransform btn5btn_pos = btn5_btnObj.GetComponent<RectTransform>();
        btn5btn_pos.anchoredPosition3D = new Vector3(-30000, 0, 0);

    }




    // OP实验类型的按钮出现在视野里
    public static void Op_refresh()
    {

        GameObject btn6_btnObj = GameObject.Find("btn6");
        RectTransform btn6btn_pos = btn6_btnObj.GetComponent<RectTransform>();
        btn6btn_pos.anchoredPosition3D = new Vector3(0, 0, 0);

    }

    // OP实验类型的按钮消失在视野里
    public static void Op_remove()
    {

        GameObject btn6_btnObj = GameObject.Find("btn6");
        RectTransform btn6btn_pos = btn6_btnObj.GetComponent<RectTransform>();
        btn6btn_pos.anchoredPosition3D = new Vector3(-30000, 0, 0);

    }

    // 结束按钮出现在视野里
    public static void Exportdata_refresh()
    {

        GameObject export_btnObj = GameObject.Find("export");
        RectTransform exportbtn_pos = export_btnObj.GetComponent<RectTransform>();
        exportbtn_pos.anchoredPosition3D = new Vector3(0, 0, 0);

    }

}
