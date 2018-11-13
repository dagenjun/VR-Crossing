using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Data;
using System.IO;
using System.Text;
using System;


public class end_time : MonoBehaviour {

    //变量声明部分

    /// <summary>
    /// 材质球的颜色
    /// </summary>
    public Material oldMat;
    public Material newMat;
    public AudioSource music;
    public static float Circle_radius; //判定区域的半径
    public static float thickness_z=0.001f; //cube的厚度
    

    /// <summary>
    /// 单次实验计算时间的变量
    /// </summary>
    public static int start1, start2, start3, start4 = 0;  //flag用于判断穿过任务的顺序是否是正确的
    public static int count_error = 0; //计算用户完成一次实验的错误次数
    public static int is_error = 0; //1为错误，0为正确；用来统计用户一次实验是否正确。
    public static int start_second, end_second; //一次实验的开始和结束对应的秒
    public static int start_millisecond, end_millisecond; //一次实验的开始和结束对应的毫秒 
    public static int delta_second, delta_millisecond; //完成一次实验所需要的时间对应的秒和毫秒
    public static int delta_time = 0; //完成一次实验所需的时间，单位为毫秒
    public static int count_click = 0; //完成一次实验的点击次数
    public static bool is_away = false; //光线时候离开物体

    /// <summary>
    /// 改变实验物体位置的调控参数
    /// </summary>
    public static int Z; //用来约束trail的次数
    public static int trail=2; //用来设置trail的次数
    public static int C = 0; //用来限制组合数
    public static int D = 30; //用来设置组合数
    //public static float thickness = 0.001f; //实验圆饼的厚度
    //public static float Vertical_distance1 = 0.542114f; //设置相机到物体平面的距离1
    //public static float Vertical_distance2 = 0.755862f; //设置相机到物体平面的距离2

    //public static float Vertical_distance1 = 2.439515f; //设置相机到物体平面的距离1
    //public static float Vertical_distance2 = 3.401381f; //设置相机到物体平面的距离2
    public static float thickness; //cube的宽度的一半
    public static float Vertical_distance1;
    public static float Vertical_distance2;

    public static float Vertical_distance1_far = 2.439515f; //设置相机到物体平面的距离1远
    public static float Vertical_distance2_far = 3.401381f; //设置相机到物体平面的距离2远
    public static float Vertical_distance1_medium = 1.626343f; //设置相机到物体平面的距离1中
    public static float Vertical_distance2_medium = 2.267587f; //设置相机到物体平面的距离1中
    public static float Vertical_distance1_near = 0.542114f; //设置相机到物体平面的距离1近
    public static float Vertical_distance2_near = 0.755862f; //设置相机到物体平面的距离1近


    public static float thickness_far = 0.00225f;
    public static float thickness_medium = 0.0015f;
    public static float thickness_near = 0.0005f;

    public static float Circle_radius_near = 0.005f;
    public static float Circle_radius_medium = 0.015f;
    public static float Circle_radius_far = 0.0225f;

 

    public static float Cylinder34_Y = 0.002f;
    public static float Cylinder34_Z = 0.001f;


    /// <summary>
    /// 记录实验用的参数
    /// </summary>
    public static DataTable dt = new DataTable(); //用来存储数据记录的表 
    public static int[] newNum = new int[D]; //声明一个数组用来存放下标
    public static float D1; //用来记录物体之间的距离D
    public static float W1; //用来记录物体的W
    public static float posx; //实际物体距离中心的距离，单位为米
    public static float scale; //实际物体的直径，单位为米

    //存储D和W的表，D=D1/2,W=W1
    //static float[,] dw_pos = new float[10, 2] { { 544, 136 }, { 544, 68 }, { 544, 34 }, { 544, 17 }, { 544, 8.5f }, { 136, 136 }, { 136, 68 }, { 136, 34 }, { 136, 17 }, { 136, 8.5f } };
    static float[,] dw_pos = new float[30, 2] { { 2448, 612 }, { 2448, 306 }, { 2448, 153 }, { 2448, 76.5f }, { 2448, 38.25f }, { 612, 612 }, { 612, 306 }, { 612, 153 }, { 612, 76.5f }, { 612, 38.25f }, { 1632, 408 }, { 1632, 204 }, { 1632, 102 }, { 1632, 51 }, { 1632, 25.5f }, { 408, 408 }, { 408, 204 }, { 408, 102 }, { 408, 51 }, { 408, 25.5f }, { 544, 136 }, { 544, 68 }, { 544, 34 }, { 544, 17 }, { 544, 8.5f }, { 136, 136 }, { 136, 68 }, { 136, 34 }, { 136, 17 }, { 136, 8.5f } };
    public static float depth ;//设置实验的景深
    public static float depth_near = 768;
    public static float depth_medium = 2304;
    public static float depth_far = 3456;

    //static float[,] dw_pos = new float[10, 2] { { 2448, 612 }, { 2448, 306 }, { 2448, 153 }, { 2448, 76.5f }, { 2448, 38.25f }, { 612, 612 }, { 612, 306 }, { 612, 153 }, { 612, 76.5f }, { 612, 38.25f } };
    //public static float depth = 3456;//设置实验的景深

    public static float ID; //记录一次实验的完成难度的参数
    public static float angle; //记录两个物体和人之间的角度的参数





    // Use this for initialization
    void Start () {
        Change();
	}
	


	// Update is called once per frame
	void Update () {
        if (ui_change.flag3 == 1) {
            if (is_away == true) {
                if ((start1 == 1 && start2 == 0) || (start1 == 1 && start2 == 1))
                {
                    if (OVRInput.GetUp(OVRInput.Button.One))
                    {
                        count_click++;
                        is_error = 1;
                        count_error = count_click;
                        music.Play();
                       

                    }

                }
            }
           
        }
        if (ui_change.flag1 == 1) {
            if ((start1 == 1 && start2 == 1 && start3 == 0 && start4 == 0)||(start1 == 1 && start2 == 1 && start3 == 2 && start4 == 2)) {
                if (OVRInput.GetUp(OVRInput.Button.One)) {
                    Debug.Log("连续任务按钮不要松开");

                    is_error = 1;
                    music.loop = true;
                    music.Play();
                    //Initial_cylinder_color();
                    //start1 = 0;
                    //start2 = 0;
                    //start3 = 0;
                    //start4 = 0;
                    count_error++;
                    
                }
                if (OVRInput.GetDown(OVRInput.Button.One)) {
                    music.loop = false;

                    music.Stop();
                    
                }
            }
        }
        if (ui_change.flag5 == 1) {
            if ((start1 == 1 && start2 == 1 && start3 == 0 && start4 == 0) || (start1 == 1 && start2 == 1 && start3 == 1 && start4 == 1))
            {
                if (OVRInput.GetUp(OVRInput.Button.One))
                {
                    Debug.Log("连续任务按钮不要松开");

                    is_error = 1;
                    music.loop = true;
                    music.Play();
                    //Initial_cylinder_color();
                    //start1 = 0;
                    //start2 = 0;
                    //start3 = 0;
                    //start4 = 0;
                    count_error++;

                }
                if (OVRInput.GetDown(OVRInput.Button.One))
                {
                    music.loop = false;

                    music.Stop();

                }

            }
        }
       
        
	}

    public void Away_sphere(Transform t) {
        if (t.gameObject.name == "Cylinder2")
        {
            is_away = true;
        }
        else if (t.gameObject.name == "Cylinder1")
        {
            is_away = true;
        }
       
    }


    public void Enter_sphere(Transform t)
    {
        if (t.gameObject.name == "Cylinder2")
        {
            is_away = false;
        }
        else if (t.gameObject.name == "Cylinder1")
        {
            is_away = false;
        }

    }

    /// <summary>
    /// 初始化错误、错误次数、点击次数
    /// </summary>
    public void Initial_error() {
        is_error = 0;
        count_click = 0;
        count_error = 0;
    }

    /// <summary>
    /// ray点击物体时引用
    /// </summary>
    public void HoverSelect(Transform t) {
        if (ui_change.flag3 == 1) {
            if (t.gameObject.name == "Cylinder1")
            {
                if (start1 == 0 && start2 == 0)
                {
                    start1 = 1;
                    GameObject obj1 = GameObject.Find("Cylinder1");
                    obj1.GetComponent<Renderer>().material = newMat;
                    start_second = System.DateTime.Now.Second;
                    start_millisecond = System.DateTime.Now.Millisecond;
                }
                else if (start1 == 1 && start2 == 1) { 
                    
                    start2 = 0;
                    GameObject obj1 = GameObject.Find("Cylinder1");
                    obj1.GetComponent<Renderer>().material = newMat;
                    //时间记录功能并且计算所花费的时间
                    end_second = System.DateTime.Now.Second;
                    end_millisecond = System.DateTime.Now.Millisecond;
                    delta_second = end_second - start_second;
                    delta_millisecond = end_millisecond - start_millisecond;
                    if (delta_millisecond < 0)
                    {
                        delta_millisecond = delta_millisecond + 1000;
                        delta_second = delta_second - 1;
                    }
                    if (delta_second < 0)
                    {
                        delta_second = delta_second + 60;
                    }
                    delta_time = delta_second * 1000 + delta_millisecond;

                    Deltatime_recording(); //记录向表中添加行

                    Initial_error();

                    //记录完数据开始以结束时间为开始时间记录下次数据时间
                    start_second = end_second;
                    start_millisecond = end_millisecond;

                    //重置Cylinder2的颜色
                    GameObject obj2 = GameObject.Find("Cylinder2");
                    obj2.GetComponent<Renderer>().material = oldMat;
                }
                else {
                    //is_error = 1;
                   
                    //Debug.Log("左边点击发生错误");
                    //start1++; ;
                    //start2 = 0;

                }
               
            }
            if (t.gameObject.name == "Cylinder2") {
                if (start1 == 1 && start2 == 0)
                {
                    
                    start2 = 1;

                    GameObject obj2 = GameObject.Find("Cylinder2");
                    obj2.GetComponent<Renderer>().material = newMat;

                    //时间记录功能并且计算所花费的时间
                    end_second = System.DateTime.Now.Second;
                    end_millisecond = System.DateTime.Now.Millisecond;
                    delta_second = end_second - start_second;
                    delta_millisecond = end_millisecond - start_millisecond;
                    if (delta_millisecond < 0)
                    {
                        delta_millisecond = delta_millisecond + 1000;
                        delta_second = delta_second - 1;
                    }
                    if (delta_second < 0)
                    {
                        delta_second = delta_second + 60;
                    }
                    delta_time = delta_second * 1000 + delta_millisecond;

                    Deltatime_recording(); //记录向表中添加行

                    Initial_error();

                    //记录完数据开始以结束时间为开始时间记录下次数据时间
                    start_second = end_second;
                    start_millisecond = end_millisecond;

                    //将开始的物体标记为结束的物体，重置其颜色
                    GameObject obj1 = GameObject.Find("Cylinder1");
                    obj1.GetComponent<Renderer>().material = oldMat;

                    Change();

                    //Refresh_pos(); //物体位置的变化
                }
                else {
                    //is_error = 1;
                    
                }
                
            }
        }
       
    }



    /// <summary>
    /// ray进入一个物体时触发，用来是否连续进行操作
    /// </summary>
    public void HoverEnter(Transform t) {
        if (ui_change.flag2 == 1 || ui_change.flag4 == 1)
        {
            if (OVRInput.Get(OVRInput.Button.One)) {
                if (t.gameObject.name == "Cube") {
                    if ((start1 == 1 && start2 == 1 && start3 == 0 && start4 == 0)|| (start1 == 1 & start2 == 1 & start3 == 1 & start4 == 1)) {
                        is_error = 1;
                        //Initial_cylinder_color();
                        //start1 = 0;
                        //start2 = 0;
                        //start3 = 0;
                        //start4 = 0;
                        count_error++;
                        music.Play();
                    }
                }
            }
        }

    }

    /// <summary>
    ///  ray离开一个物体时调用
    /// </summary>
    public void HoverExit(Transform t)
    {
        Debug.Log("sss1");

        if (ui_change.flag2 == 1 || ui_change.flag4 == 1||ui_change.flag5==1)
        {

            Debug.Log("sss2");
            //如果按住primary button则触发

            if (OVRInput.Get(OVRInput.Button.One))
            {
                Debug.Log("sss3");
                if (t.gameObject.name == "Cylinder1")
                {
                    if (start1 == 0 && start2 == 0 && start3 == 0 && start4 == 0)
                    {
                        //t.gameObject.GetComponent<Renderer>().material.color = Color.red;
                        start1 = 1;
                    }
                    else if (start1 == 1 & start2 == 1 & start3 == 1 & start4 == 1)
                    {
                        start1 = 2;
                        start2 = 0;
                        start3 = 0;
                        start4 = 0;
                    }
                    else
                    {
                        /*
                        is_error = 1;
                        count_error++;
                        Debug.Log("左边穿过发生错误");
                        start1 = 0;
                        start2 = 0;
                        start3 = 0;
                        start4 = 0;
                        Initial_cylinder_color(); //初始化圆饼的颜色
                        */
                    }

                }
                if (t.gameObject.name == "Cylinder2")
                {
                    if (start1 == 1 && start2 == 0 && start3 == 0 && start4 == 0)
                    {
                        start2 = 1;

                        //从上往下从左往右穿过的时候变颜色
                        GameObject obj1 = GameObject.Find("Cylinder1");
                        obj1.GetComponent<Renderer>().material = newMat;
                        GameObject obj2 = GameObject.Find("Cylinder2");
                        obj2.GetComponent<Renderer>().material = newMat;

                        //记录穿过的时间
                        start_second = System.DateTime.Now.Second;
                        start_millisecond = System.DateTime.Now.Millisecond;
                    }
                    else if (start1 == 2 && start2 == 0 && start3 == 0 && start4 == 0)
                    {
                        start1 = 1;
                        start2 = 1;

                        //从上往下从左往右穿过的时候变颜色
                        GameObject obj1 = GameObject.Find("Cylinder1");
                        obj1.GetComponent<Renderer>().material = newMat;
                        GameObject obj2 = GameObject.Find("Cylinder2");
                        obj2.GetComponent<Renderer>().material = newMat;


                        //时间记录功能并且计算所花费的时间
                        end_second = System.DateTime.Now.Second;
                        end_millisecond = System.DateTime.Now.Millisecond;
                        delta_second = end_second - start_second;
                        delta_millisecond = end_millisecond - start_millisecond;
                        if (delta_millisecond < 0)
                        {
                            delta_millisecond = delta_millisecond + 1000;
                            delta_second = delta_second - 1;
                        }
                        if (delta_second < 0)
                        {
                            delta_second = delta_second + 60;
                        }
                        delta_time = delta_second * 1000 + delta_millisecond;

                        Deltatime_recording(); //记录向表中添加行

                        Initial_error();

                        GameObject obj3 = GameObject.Find("Cylinder3");
                        obj3.GetComponent<Renderer>().material = oldMat;
                        GameObject obj4 = GameObject.Find("Cylinder4");
                        obj4.GetComponent<Renderer>().material = oldMat;

                        start_second = end_second;
                        start_millisecond = end_millisecond;

                        //判定区域移动至另一侧
                        if (ui_change.flag2 == 1 || ui_change.flag5 == 1)
                        {
                            GameObject obj34up = GameObject.Find("Cylinder34up");
                            GameObject obj34down = GameObject.Find("Cylinder34down");
                            Transform obj34up_trans = obj34up.GetComponent<Transform>();
                            Transform obj34down_trans = obj34down.GetComponent<Transform>();
                            obj34up_trans.localPosition = new Vector3(posx - scale / 2 - Circle_radius, 0, 0);
                            obj34down_trans.localPosition = new Vector3(posx + scale / 2 + Circle_radius, 0, 0);
                        }
                        if (ui_change.flag4 == 1)
                        {
                            GameObject obj34up = GameObject.Find("Cylinder34up");
                            GameObject obj34down = GameObject.Find("Cylinder34down");
                            Transform obj34up_trans = obj34up.GetComponent<Transform>();
                            Transform obj34down_trans = obj34down.GetComponent<Transform>();
                            obj34up_trans.localPosition = new Vector3(posx + 0.0011f, scale / 2 + Circle_radius, 0);
                            obj34down_trans.localPosition = new Vector3(posx + 0.0011f, -Circle_radius - scale / 2, 0);
                        }



                    }

                    else
                    {
                        /*
                        is_error = 1;
                        start1 = 0;
                        start2 = 1;
                        start3 = 0;
                        start4 = 0;
                        count_error++;
                        Debug.Log("左边穿过发生错误");
                        Initial_cylinder_color(); //初始化圆饼的颜色
                        */

                    }
                }
                if (t.gameObject.name == "Cylinder3")
                {
                    Debug.Log("穿过cylinder3");
                    if (start1 == 1 && start2 == 1 && start3 == 0 && start4 == 0)
                    {
                        start3 = 1;
                    }
                    else
                    {
                        /*
                        is_error = 1;
                        count_error++;
                        Debug.Log("右边穿过发生错误");
                        start1 = 0;
                        start2 = 0;
                        start3 = 0;
                        start4 = 0;
                        Initial_cylinder_color(); //初始化圆饼的颜色
                        */
                    }

                }
                if (t.gameObject.name == "Cylinder4")
                {

                    if (start1 == 1 && start2 == 1 && start3 == 1 && start4 == 0)
                    {
                        start4 = 1;

                        //从上往下从左往右穿过的时候变颜色
                        GameObject obj3 = GameObject.Find("Cylinder3");
                        obj3.GetComponent<Renderer>().material = newMat;
                        GameObject obj4 = GameObject.Find("Cylinder4");
                        obj4.GetComponent<Renderer>().material = newMat;

                        
                       

                        //时间记录功能并且计算所花费的时间
                        end_second = System.DateTime.Now.Second;
                        end_millisecond = System.DateTime.Now.Millisecond;
                        delta_second = end_second - start_second;
                        delta_millisecond = end_millisecond - start_millisecond;
                        if (delta_millisecond < 0)
                        {
                            delta_millisecond = delta_millisecond + 1000;
                            delta_second = delta_second - 1;
                        }
                        if (delta_second < 0)
                        {
                            delta_second = delta_second + 60;
                        }
                        delta_time = delta_second * 1000 + delta_millisecond;

                        Deltatime_recording(); //记录向表中添加行

                        Initial_error();

                        start_second = end_second;
                        start_millisecond = end_millisecond;

                        //初始化颜色
                        GameObject obj1 = GameObject.Find("Cylinder1");
                        obj1.GetComponent<Renderer>().material = oldMat;
                        GameObject obj2 = GameObject.Find("Cylinder2");
                        obj2.GetComponent<Renderer>().material = oldMat;

                        Change();

                        

                        //判定区域移动至另一侧
                        if (ui_change.flag2 == 1 || ui_change.flag5 == 1)
                        {
                            GameObject obj34up = GameObject.Find("Cylinder34up");
                            GameObject obj34down = GameObject.Find("Cylinder34down");
                            Transform obj34up_trans = obj34up.GetComponent<Transform>();
                            Transform obj34down_trans = obj34down.GetComponent<Transform>();
                            obj34up_trans.localPosition = new Vector3(-posx - scale / 2 - Circle_radius, 0, 0);
                            obj34down_trans.localPosition = new Vector3(-posx + scale / 2 + Circle_radius, 0, 0);
                        }
                        if (ui_change.flag4 == 1)
                        {
                            GameObject obj34up = GameObject.Find("Cylinder34up");
                            GameObject obj34down = GameObject.Find("Cylinder34down");
                            Transform obj34up_trans = obj34up.GetComponent<Transform>();
                            Transform obj34down_trans = obj34down.GetComponent<Transform>();
                            obj34up_trans.localPosition = new Vector3(-posx + 0.0011f, scale / 2 + Circle_radius, 0);
                            obj34down_trans.localPosition = new Vector3(-posx + 0.0011f, -Circle_radius - scale / 2, 0);
                        }

                        if (Z == 1) {
                            if (ui_change.flag2 == 1 || ui_change.flag5 == 1)
                            {
                                GameObject obj34up = GameObject.Find("Cylinder34up");
                                GameObject obj34down = GameObject.Find("Cylinder34down");
                                Transform obj34up_trans = obj34up.GetComponent<Transform>();
                                Transform obj34down_trans = obj34down.GetComponent<Transform>();
                                obj34up_trans.localPosition = new Vector3(posx - scale / 2 - Circle_radius, 0, 0);
                                obj34down_trans.localPosition = new Vector3(posx + scale / 2 + Circle_radius, 0, 0);
                            }
                            if (ui_change.flag4 == 1)
                            {
                                GameObject obj34up = GameObject.Find("Cylinder34up");
                                GameObject obj34down = GameObject.Find("Cylinder34down");
                                Transform obj34up_trans = obj34up.GetComponent<Transform>();
                                Transform obj34down_trans = obj34down.GetComponent<Transform>();
                                obj34up_trans.localPosition = new Vector3(posx + 0.0011f, scale / 2 + Circle_radius, 0);
                                obj34down_trans.localPosition = new Vector3(posx + 0.0011f, -Circle_radius - scale / 2, 0);
                            }
                        }

                        //Refresh_pos(); //物体位置的变化
                    }
                    else
                    {
                        //is_error = 1;
                        //start1 = 0;
                        //start2 = 0;
                        //start3 = 0;
                        //start4 = 0;
                        //count_error++;
                        //Debug.Log("右边穿过发生错误");
                        //Initial_cylinder_color(); //初始化圆饼的颜色
                    }
                }

                if (t.gameObject.name == "Cylinder34up")
                {
                    if ((start1 == 1 && start2 == 1 && start3 == 0 && start4 == 0)||(start1 == 1 && start2 == 1 && start3 == 1 && start4 == 1))
                    {
                        is_error = 1;
                        //start1 = 0;
                        //start2 = 0;
                        //start3 = 0;
                        //start4 = 0;
                        count_error++;
                        music.Play();
                        Debug.Log("没有穿过圆饼");
                        //Initial_cylinder_color(); //初始化圆饼的颜色

                    }
                }

                if (t.gameObject.name == "Cylinder34down")
                {
                    if (start1 == 1 && start2 == 1 && start3 == 0 && start4 == 0 || (start1 == 1 && start2 == 1 && start3 == 1 && start4 == 1))
                    {
                        is_error = 1;
                        //start1 = 0;
                        //start2 = 0;
                        //start3 = 0;
                        //start4 = 0;
                        count_error++;
                        music.Play();
                        Debug.Log("没有穿过圆饼");
                        //Initial_cylinder_color(); //初始化圆饼的颜色

                    }
                }

            }
            /*
            else
            {
                if (start1 == 1 && start2 == 1 && start3 == 0 && start4 == 0)
                {
                    if (t.gameObject.name == "Cylinder3")
                    {
                        is_error = 1;

                        count_error++;

                        music.Play();

                        Debug.Log("没有穿过圆饼");

                    }
                }
                if (start1 == 1 & start2 == 1 & start3 == 1 & start4 == 1) {
                    if (t.gameObject.name == "Cylinder1") {
                        is_error = 1;

                        count_error++;

                        music.Play();

                        Debug.Log("没有穿过圆饼");

                    }
                }
            }
            */
        }
        if (ui_change.flag1==1)
        {

            Debug.Log("flag1");
            //如果按住primary button则触发

            if (OVRInput.Get(OVRInput.Button.One))
            {
                Debug.Log("按住A");
                if (t.gameObject.name == "Cylinder1")
                {
                    if (start1 == 0 && start2 == 0 && start3 == 0 && start4 == 0)
                    {
                        //t.gameObject.GetComponent<Renderer>().material.color = Color.red;
                        start1 = 1;


                    }
                    else if (start1 == 1 & start2 == 2 & start3 == 2 & start4 == 2)
                    {
                        Debug.Log("回来穿过成功");
                        start1 = 0;
                        start2 = 0;
                        start3 = 0;
                        start4 = 0;
                        //回来穿过时候颜色改变
                        GameObject obj1 = GameObject.Find("Cylinder1");
                        obj1.GetComponent<Renderer>().material = newMat;
                        GameObject obj2 = GameObject.Find("Cylinder2");
                        obj2.GetComponent<Renderer>().material = newMat;

                        //回来穿过成功时改变判定区域的位置
                        GameObject obj34up = GameObject.Find("Cylinder34up");
                        GameObject obj34down = GameObject.Find("Cylinder34down");
                        Transform obj34up_trans = obj34up.GetComponent<Transform>();
                        Transform obj34down_trans = obj34down.GetComponent<Transform>();
                        obj34up_trans.localPosition = new Vector3(-posx + 0.0011f, scale / 2 + Circle_radius, 0);
                        obj34down_trans.localPosition = new Vector3(-posx + 0.0011f, -Circle_radius - scale / 2, 0);


                        //时间记录功能并且计算所花费的时间
                        end_second = System.DateTime.Now.Second;
                        end_millisecond = System.DateTime.Now.Millisecond;
                        delta_second = end_second - start_second;
                        delta_millisecond = end_millisecond - start_millisecond;
                        if (delta_millisecond < 0)
                        {
                            delta_millisecond = delta_millisecond + 1000;
                            delta_second = delta_second - 1;
                        }
                        if (delta_second < 0)
                        {
                            delta_second = delta_second + 60;
                        }
                        delta_time = delta_second * 1000 + delta_millisecond;

                        Deltatime_recording(); //记录向表中添加行

                        Initial_error();

                        //初始化颜色
                        GameObject obj3 = GameObject.Find("Cylinder3");
                        obj3.GetComponent<Renderer>().material = oldMat;
                        GameObject obj4 = GameObject.Find("Cylinder4");
                        obj4.GetComponent<Renderer>().material = oldMat;

                        //过短暂时间恢复颜色便于进行下次往复实验
                        Invoke("Change_color12", 0.1f);


                    }
                    else
                    {
                        /*
                        is_error = 1;
                        count_error++;
                        Debug.Log("左边穿过发生错误");
                        start1 = 0;
                        start2 = 0;
                        start3 = 0;
                        start4 = 0;
                        Initial_cylinder_color(); //初始化圆饼的颜色
                        */
                    }

                }
                if (t.gameObject.name == "Cylinder2")
                {
                    if (start1 == 1 && start2 == 0 && start3 == 0 && start4 == 0)
                    {
                        start2 = 1;

                        //从上往下从左往右穿过的时候变颜色
                        GameObject obj1 = GameObject.Find("Cylinder1");
                        obj1.GetComponent<Renderer>().material = newMat;
                        GameObject obj2 = GameObject.Find("Cylinder2");
                        obj2.GetComponent<Renderer>().material = newMat;

                        GameObject obj34up = GameObject.Find("Cylinder34up");
                        GameObject obj34down = GameObject.Find("Cylinder34down");
                        Transform obj34up_trans = obj34up.GetComponent<Transform>();
                        Transform obj34down_trans = obj34down.GetComponent<Transform>();
                        obj34up_trans.localPosition = new Vector3(posx + 0.0011f, scale / 2 + Circle_radius, 0);
                        obj34down_trans.localPosition = new Vector3(posx + 0.0011f, -Circle_radius - scale / 2, 0);

                        //记录穿过的时间
                        start_second = System.DateTime.Now.Second;
                        start_millisecond = System.DateTime.Now.Millisecond;
                    }
                    else if (start1 == 1 && start1 == 1 && start3 == 2 && start4 == 2)
                    {
                        Debug.Log("回来穿过 Clinder2");
                        start2 = 2;

                    }

                    else
                    {
                        /*
                        is_error = 1;
                        start1 = 0;
                        start2 = 1;
                        start3 = 0;
                        start4 = 0;
                        count_error++;
                        Debug.Log("左边穿过发生错误");
                        Initial_cylinder_color(); //初始化圆饼的颜色
                        */

                    }
                }
                if (t.gameObject.name == "Cylinder3")
                {
                    Debug.Log("穿过cylinder3");
                    if (start1 == 1 && start2 == 1 && start3 == 0 && start4 == 0)
                    {
                        start3 = 1;
                    }
                    else if (start1 == 1 && start2 == 1 && start3 == 1 && start4 == 2) {
                        Debug.Log("回来穿过 Clinder3");
                        start3 = 2;

                        //记录开始时间
                        start_second = System.DateTime.Now.Second;
                        start_millisecond = System.DateTime.Now.Millisecond;

                        //穿过改变颜色
                        GameObject obj3 = GameObject.Find("Cylinder3");
                        obj3.GetComponent<Renderer>().material = newMat;
                        GameObject obj4 = GameObject.Find("Cylinder4");
                        obj4.GetComponent<Renderer>().material = newMat;

                        //回来穿过时改变判定区域的位置
                        GameObject obj34up = GameObject.Find("Cylinder34up");
                        GameObject obj34down = GameObject.Find("Cylinder34down");
                        Transform obj34up_trans = obj34up.GetComponent<Transform>();
                        Transform obj34down_trans = obj34down.GetComponent<Transform>();
                        obj34up_trans.localPosition = new Vector3(-posx - 0.0031f, scale / 2 + Circle_radius, 0);
                        obj34down_trans.localPosition = new Vector3(-posx - 0.0031f, -Circle_radius - scale / 2, 0);
                    }
                    else
                    {
                        /*
                        is_error = 1;
                        count_error++;
                        Debug.Log("右边穿过发生错误");
                        start1 = 0;
                        start2 = 0;
                        start3 = 0;
                        start4 = 0;
                        Initial_cylinder_color(); //初始化圆饼的颜色
                        */
                    }

                }
                if (t.gameObject.name == "Cylinder4")
                {

                    if (start1 == 1 && start2 == 1 && start3 == 1 && start4 == 0)
                    {
                        start4 = 1;

                        //从上往下从左往右穿过的时候变颜色
                        GameObject obj3 = GameObject.Find("Cylinder3");
                        obj3.GetComponent<Renderer>().material = newMat;
                        GameObject obj4 = GameObject.Find("Cylinder4");
                        obj4.GetComponent<Renderer>().material = newMat;

                        //时间记录功能并且计算所花费的时间
                        end_second = System.DateTime.Now.Second;
                        end_millisecond = System.DateTime.Now.Millisecond;
                        delta_second = end_second - start_second;
                        delta_millisecond = end_millisecond - start_millisecond;
                        if (delta_millisecond < 0)
                        {
                            delta_millisecond = delta_millisecond + 1000;
                            delta_second = delta_second - 1;
                        }
                        if (delta_second < 0)
                        {
                            delta_second = delta_second + 60;
                        }
                        delta_time = delta_second * 1000 + delta_millisecond;

                        Deltatime_recording(); //记录向表中添加行

                        Initial_error();

                        

                        //初始化颜色
                        GameObject obj1 = GameObject.Find("Cylinder1");
                        obj1.GetComponent<Renderer>().material = oldMat;
                        GameObject obj2 = GameObject.Find("Cylinder2");
                        obj2.GetComponent<Renderer>().material = oldMat;

                        //改变判定区域的位置
                        GameObject obj34up = GameObject.Find("Cylinder34up");
                        GameObject obj34down = GameObject.Find("Cylinder34down");
                        Transform obj34up_trans = obj34up.GetComponent<Transform>();
                        Transform obj34down_trans = obj34down.GetComponent<Transform>();
                        obj34up_trans.localPosition = new Vector3(posx - 0.0031f, scale / 2 + Circle_radius, 0);
                        obj34down_trans.localPosition = new Vector3(posx - 0.0031f, -Circle_radius - scale / 2, 0);


                        //过短暂时间恢复颜色便于进行下次往复实验
                        Invoke("Change_color34", 0.1f);

                        Change();
                        //Refresh_pos(); //物体位置的变化
                    }
                    else if(start1== 1 && start2 == 1 && start3 == 1 && start4 == 1)
                    {
                        Debug.Log("回来穿过 Clinder4");
                        start4 = 2;
                    }
                    else
                    {
                        //is_error = 1;
                        //start1 = 0;
                        //start2 = 0;
                        //start3 = 0;
                        //start4 = 0;
                        //count_error++;
                        //Debug.Log("右边穿过发生错误");
                        //Initial_cylinder_color(); //初始化圆饼的颜色
                    }
                }

                if (t.gameObject.name == "Cylinder34up")
                {
                    if ((start1 == 1 && start2 == 1 && start3 == 0 && start4 == 0)|| (start1 == 1 && start1 == 1 && start3 == 2 && start4 == 2))
                    {
                        is_error = 1;
                        //start1 = 0;
                        //start2 = 0;
                        //start3 = 0;
                        //start4 = 0;
                        count_error++;
                        music.Play();
                        Debug.Log("没有穿过圆饼");
                        //Initial_cylinder_color(); //初始化圆饼的颜色

                    }
                }

                if (t.gameObject.name == "Cylinder34down")
                {
                    if ((start1 == 1 && start2 == 1 && start3 == 0 && start4 == 0) || (start1 == 1 && start1 == 1 && start3 == 2 && start4 == 2))
                    {
                        is_error = 1;
                        //start1 = 0;
                        //start2 = 0;
                        //start3 = 0;
                        //start4 = 0;
                        music.Play();
                        count_error++;
                        Debug.Log("没有穿过圆饼");
                        //Initial_cylinder_color(); //初始化圆饼的颜色

                    }
                }

            }
            
        }
     
       
        
    }
    /// <summary>
    /// 恢复cylinder1和2的颜色
    /// </summary>
    public void Change_color12() {
        GameObject obj1 = GameObject.Find("Cylinder1");
        obj1.GetComponent<Renderer>().material = oldMat;
        GameObject obj2 = GameObject.Find("Cylinder2");
        obj2.GetComponent<Renderer>().material = oldMat;

    }

    /// <summary>
    /// 恢复cylindear3和4的颜色
    /// </summary>
    public void Change_color34() {
        GameObject obj3 = GameObject.Find("Cylinder3");
        obj3.GetComponent<Renderer>().material = oldMat;
        GameObject obj4 = GameObject.Find("Cylinder4");
        obj4.GetComponent<Renderer>().material = oldMat;
    }

    /// <summary>
    /// 测试函数，没用
    /// </summary>
    /// <param name="t"></param>
    public void Test_onhover(Transform t)
    {
        if (OVRInput.Get(OVRInput.Button.One)) {
            if (t.gameObject.name == "Cylinder34")
            {
                Debug.Log("选中Cylinder34");
                if (t.gameObject.name == "Cylinder3")
                {
                    Debug.Log("选中cylinder3test");
                    if (start1 == 1 && start2 == 1 && start3 == 0 && start4 == 0)
                    {
                        start3 = 1;
                    }
                    else
                    {
                        /*
                        is_error = 1;
                        count_error++;
                        Debug.Log("右边穿过发生错误");
                        start1 = 0;
                        start2 = 0;
                        start3 = 0;
                        start4 = 0;
                        Initial_cylinder_color(); //初始化圆饼的颜色
                        */
                    }

                }
            }
        }
          
    }


    /// <summary>
    /// 初始化圆饼/球体的颜色
    /// </summary>
    public void Initial_cylinder_color() {

        GameObject obj1 = GameObject.Find("Cylinder1");
        obj1.GetComponent<Renderer>().material = oldMat;
        GameObject obj2 = GameObject.Find("Cylinder2");
        obj2.GetComponent<Renderer>().material = oldMat;
        if (ui_change.flag1 == 1 || ui_change.flag2 == 1 || ui_change.flag4 == 1 || ui_change.flag5 == 1){
            GameObject obj3 = GameObject.Find("Cylinder3");
            obj3.GetComponent<Renderer>().material = oldMat;
            GameObject obj4 = GameObject.Find("Cylinder4");
            obj4.GetComponent<Renderer>().material = oldMat;
        }
    }


    /// <summary>
    /// 记录向表中添加行
    /// </summary>
    public void Deltatime_recording() {
        D1 = dw_pos[newNum[C - 1], 0] * 2;

        W1 = dw_pos[newNum[C - 1], 1];

        ID = Mathf.Log((D1 / W1) + 1, 2);

        if (D1 == 1088 || D1 == 272)
        {
            depth = depth_near;
        }
        if (D1 == 3264 || D1 == 816)
        {
            depth = depth_medium;
        }
        if (D1 == 4896 || D1 == 1224)
        {
            depth = depth_far;
        }


        angle = Mathf.Acos(1 - D1 * D1 / (2 * depth * depth)) * Mathf.Rad2Deg;

        dt.Rows.Add(ui_change.Name, ui_change.exp_type, ui_change.block, C, depth, D1, W1, ID, angle, end_time.delta_time, end_time.is_error);
        ui_change.export_data();



    }


    /// <summary>
    /// 返回主界面
    /// </summary>
    public void goto_mainstart()
    {

        SceneManager.LoadScene("main_start");
        
    }

    /// <summary>
    /// 返回主界面的控制函数以及重置参数
    /// </summary>
    public void initial_uichange_var()
    {

        if (ui_change.flag1 == 1)
        {

            if (ui_change.block <= 2)
            {

                ui_change.flag1 = 0;
            }
            else
            {
                ui_change.flag1 = -1;
                ui_change.block = 0;
            }
        }
        if (ui_change.flag2 == 1)
        {

            if (ui_change.block <= 2)
            {

                ui_change.flag2 = 0;
            }
            else
            {
                ui_change.flag2 = -1;
                ui_change.block = 0;
            }
        }
        if (ui_change.flag3 == 1)
        {

            if (ui_change.block <= 2)
            {

                ui_change.flag3 = 0;
            }
            else
            {
                ui_change.flag3 = -1;
                ui_change.block = 0;
            }
        }
        if (ui_change.flag4 == 1)
        {

            if (ui_change.block <= 2)
            {

                ui_change.flag4 = 0;
            }
            else
            {
                ui_change.flag4 = -1;
                ui_change.block = 0;
            }
        }
        if (ui_change.flag5 == 1)
        {

            if (ui_change.block <= 2)
            {

                ui_change.flag5 = 0;
            }
            else
            {
                ui_change.flag5 = -1;
                ui_change.block = 0;
            }
        }
  
    }



    /// <summary>
    /// 生成0到m-1的随机数，并将其存在数组里面
    /// </summary>
    /// <param name="m"></param>
    public static void random_n(int m)
    {

        int[] num = new int[m];

        int i, r = m - 1;
        int n;
        int tmp;
        System.Random rand = new System.Random();
        for (i = 0; i < m; i++)//初始化0~m
        {
            num[i] = i;
        }

        for (i = 0; i < m; i++)
        {
            n = rand.Next(0, r);//随机生一个0~m的数，r的初始是m
            newNum[i] = num[n];
            tmp = num[n];//避免重复
            num[n] = num[r];
            num[r] = tmp;
            r--;//自减，下次生的随机数就可以从0到m-2了，
        }
    }


    /// <summary>
    /// 物体位置的变化
    /// </summary>
    public void Refresh_pos() {
        GameObject obj1 = GameObject.Find("Cylinder1");
        GameObject obj2 = GameObject.Find("Cylinder2");

        Transform obj1_trans = obj1.GetComponent<Transform>();
        Transform obj2_trans = obj2.GetComponent<Transform>();

        obj1_trans.localPosition = new Vector3(-1000, 0, 0);
        obj2_trans.localPosition = new Vector3(-1000, 0, 0);

        if (ui_change.flag1 == 1 || ui_change.flag2 == 1 || ui_change.flag4 == 1 || ui_change.flag5 == 1) {
            GameObject obj3 = GameObject.Find("Cylinder3");
            GameObject obj4 = GameObject.Find("Cylinder4");
            GameObject obj34up = GameObject.Find("Cylinder34up");
            GameObject obj34down = GameObject.Find("Cylinder34down");

            Transform obj3_trans = obj3.GetComponent<Transform>();
            Transform obj4_trans = obj4.GetComponent<Transform>();
            Transform obj34up_trans = obj34up.GetComponent<Transform>();
            Transform obj34down_trans = obj34down.GetComponent<Transform>();


            obj3_trans.localPosition = new Vector3(-1000, 0, 0);
            obj4_trans.localPosition = new Vector3(-1000, 0, 0);
            obj34up_trans.localPosition = new Vector3(-1000, 0, 0);
            obj34down_trans.localPosition = new Vector3(-1000, 0, 0);
        }

        Invoke("Change", 1.0f);

    }

  



    /// <summary>
    /// 改变每次实验物体的位置关系
    /// </summary>
    public void Change() {
        Z++;
        if (Z == trail + 1) {
            Z = 1;
        }
        if (Z == 1) {
            C++;
            Initial_endtime_var();
        }
        if (C <= D) {

            float x = dw_pos[newNum[C - 1], 0];

            float y = dw_pos[newNum[C - 1], 1];
            if (ui_change.flag1 == 1 || ui_change.flag2 == 1 || ui_change.flag4 == 1 || ui_change.flag5 == 1)
            {
                Change_pos(x, y);
            }
            else if (ui_change.flag3 == 1) {
                Change_pos_b(x, y);
            }

           
        }
        if (C == D+1 && end_time.Z == 1)
        {

            C = 0;

            end_time.Z = 0;

            initial_uichange_var();

            goto_mainstart();

        }

    }


    public void Initial_endtime_var() {
        start1 = 0;
        start2 = 0;
        start3 = 0;
        start4 = 0;
        is_error = 0;
        count_error = 0;
        delta_time = 0;
        Initial_cylinder_color();
    }



    /// <summary>
    /// 圆饼情况下物体移动规则（flag1、flag2、flag4、flag5）
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public static void Change_pos(float x, float y) {
        posx = x/1000;
        scale = y/1000;


        //改变相机的深度使得物体到人的深度固定
        if (posx == 2.448f)
        {
            Debug.Log("111");
            GameObject ovrcamera_Obj = GameObject.Find("OVRCameraRig");

            ovrcamera_Obj.transform.position = new Vector3(0, 0, -Vertical_distance1_far);
            thickness = thickness_far;
            Circle_radius = Circle_radius_far;

            GameObject obj_canvas = GameObject.Find("Canvas");
            RectTransform objcanvas_trans = obj_canvas.GetComponent<RectTransform>();
            objcanvas_trans.sizeDelta = new Vector2(7965, 2115);

            if (ui_change.flag2 == 1 || ui_change.flag4 == 1) {
                GameObject obj_cube = GameObject.Find("Cube");
                Transform objcube_trans = obj_cube.GetComponent<Transform>();
                objcube_trans.localScale = new Vector3(0.0225f, 1.8f, 0.0001f);
            }

        }
        if (posx == 0.612f)
        {
            Debug.Log("222");
            GameObject ovrcamera_Obj = GameObject.Find("OVRCameraRig");

            ovrcamera_Obj.transform.position = new Vector3(0, 0, -Vertical_distance2_far);
            thickness = thickness_far;
            Circle_radius = Circle_radius_far;

            GameObject obj_canvas = GameObject.Find("Canvas");
            RectTransform objcanvas_trans = obj_canvas.GetComponent<RectTransform>();
            objcanvas_trans.sizeDelta = new Vector2(7965, 2115);

            if (ui_change.flag2 == 1 || ui_change.flag4 == 1)
            {
                GameObject obj_cube = GameObject.Find("Cube");
                Transform objcube_trans = obj_cube.GetComponent<Transform>();
                objcube_trans.localScale = new Vector3(0.0225f, 1.8f, 0.0001f);
            }

        }
        if (posx == 1.632f)
        {
            Debug.Log("333");
            GameObject ovrcamera_Obj = GameObject.Find("OVRCameraRig");

            ovrcamera_Obj.transform.position = new Vector3(0, 0, -Vertical_distance1_medium);
            thickness = thickness_medium;
            Circle_radius = Circle_radius_medium;

            GameObject obj_canvas = GameObject.Find("Canvas");
            RectTransform objcanvas_trans = obj_canvas.GetComponent<RectTransform>();
            objcanvas_trans.sizeDelta = new Vector2(5310, 1410);

            if (ui_change.flag2 == 1 || ui_change.flag4 == 1)
            {
                GameObject obj_cube = GameObject.Find("Cube");
                Transform objcube_trans = obj_cube.GetComponent<Transform>();
                objcube_trans.localScale = new Vector3(0.015f, 1.2f, 0.0001f);
            }

        }
        if (posx == 0.408f)
        {
            Debug.Log("444");
            GameObject ovrcamera_Obj = GameObject.Find("OVRCameraRig");

            ovrcamera_Obj.transform.position = new Vector3(0, 0, -Vertical_distance2_medium);
            thickness = thickness_medium;
            Circle_radius = Circle_radius_medium;

            GameObject obj_canvas = GameObject.Find("Canvas");
            RectTransform objcanvas_trans = obj_canvas.GetComponent<RectTransform>();
            objcanvas_trans.sizeDelta = new Vector2(5310, 1410);

            if (ui_change.flag2 == 1 || ui_change.flag4 == 1)
            {
                GameObject obj_cube = GameObject.Find("Cube");
                Transform objcube_trans = obj_cube.GetComponent<Transform>();
                objcube_trans.localScale = new Vector3(0.015f, 1.2f, 0.0001f);
            }

        }
        if (posx == 0.544f)
        {
            Debug.Log("555");
            GameObject ovrcamera_Obj = GameObject.Find("OVRCameraRig");

            ovrcamera_Obj.transform.position = new Vector3(0, 0, -Vertical_distance1_near);
            thickness = thickness_near;
            Circle_radius = Circle_radius_near;

            GameObject obj_canvas = GameObject.Find("Canvas");
            RectTransform objcanvas_trans = obj_canvas.GetComponent<RectTransform>();
            objcanvas_trans.sizeDelta = new Vector2(2000, 550);

            if (ui_change.flag2 == 1 || ui_change.flag4 == 1)
            {
                GameObject obj_cube = GameObject.Find("Cube");
                Transform objcube_trans = obj_cube.GetComponent<Transform>();
                objcube_trans.localScale = new Vector3(0.005f, 0.5f, 0.0001f);
            }

        }
        if (posx == 0.136f)
        {
            Debug.Log("666");
            GameObject ovrcamera_Obj = GameObject.Find("OVRCameraRig");

            ovrcamera_Obj.transform.position = new Vector3(0, 0, -Vertical_distance2_near);
            thickness = thickness_near;
            Circle_radius = Circle_radius_near;

            GameObject obj_canvas = GameObject.Find("Canvas");
            RectTransform objcanvas_trans = obj_canvas.GetComponent<RectTransform>();
            objcanvas_trans.sizeDelta = new Vector2(2000, 550);

            if (ui_change.flag2 == 1 || ui_change.flag4 == 1)
            {
                GameObject obj_cube = GameObject.Find("Cube");
                Transform objcube_trans = obj_cube.GetComponent<Transform>();
                objcube_trans.localScale = new Vector3(0.005f, 0.5f, 0.0001f);
            }

        }

        GameObject obj1 = GameObject.Find("Cylinder1");
        GameObject obj2 = GameObject.Find("Cylinder2");
        GameObject obj3 = GameObject.Find("Cylinder3");
        GameObject obj4 = GameObject.Find("Cylinder4");
        GameObject obj34up = GameObject.Find("Cylinder34up");
        GameObject obj34down = GameObject.Find("Cylinder34down");

        Transform obj1_trans = obj1.GetComponent<Transform>();
        Transform obj2_trans = obj2.GetComponent<Transform>();
        Transform obj3_trans = obj3.GetComponent<Transform>();
        Transform obj4_trans = obj4.GetComponent<Transform>();
        Transform obj34up_trans = obj34up.GetComponent<Transform>();
        Transform obj34down_trans = obj34down.GetComponent<Transform>();


        if (ui_change.flag2 == 1) {
            //调整位置信息
            
            obj1_trans.localPosition = new Vector3(-posx, thickness * 2, 0);
            obj2_trans.localPosition = new Vector3(-posx, 0, 0);
            obj3_trans.localPosition = new Vector3(posx, thickness * 2, 0);
            obj4_trans.localPosition = new Vector3(posx, 0, 0);
            obj34up_trans.localPosition = new Vector3(posx - scale / 2 - Circle_radius, 0, 0);
            obj34down_trans.localPosition = new Vector3(posx + scale / 2 + Circle_radius, 0, 0);

            //调整scale的大小（直径大小）
            obj1_trans.localScale = new Vector3(scale, thickness, thickness_z);
            obj2_trans.localScale = new Vector3(scale, thickness, thickness_z);
            obj3_trans.localScale = new Vector3(scale, thickness, thickness_z);
            obj4_trans.localScale = new Vector3(scale, thickness, thickness_z);
            obj34up_trans.localScale = new Vector3(Circle_radius+Circle_radius, Cylinder34_Y, Cylinder34_Z);
            obj34down_trans.localScale = new Vector3(Circle_radius + Circle_radius, Cylinder34_Y, Cylinder34_Z);
        }
        if (ui_change.flag5 == 1) {
            //调整位置信息
            
            obj1_trans.localPosition = new Vector3(-posx, thickness * 2, 0);
            obj2_trans.localPosition = new Vector3(-posx, 0, 0);
            obj3_trans.localPosition = new Vector3(posx, thickness * 2, 0);
            obj4_trans.localPosition = new Vector3(posx, 0, 0);
            obj34up_trans.localPosition = new Vector3(posx - scale / 2 - Circle_radius, 0, 0);
            obj34down_trans.localPosition = new Vector3(posx + scale / 2 + Circle_radius, 0, 0);

            //调整scale的大小（直径大小）
            obj1_trans.localScale = new Vector3(scale, thickness, thickness_z);
            obj2_trans.localScale = new Vector3(scale, thickness, thickness_z);
            obj3_trans.localScale = new Vector3(scale, thickness, thickness_z);
            obj4_trans.localScale = new Vector3(scale, thickness, thickness_z);
            obj34up_trans.localScale = new Vector3(Circle_radius + Circle_radius, Cylinder34_Y, Cylinder34_Z);
            obj34down_trans.localScale = new Vector3(Circle_radius + Circle_radius, Cylinder34_Y, Cylinder34_Z);
        }
        if (ui_change.flag1 == 1 || ui_change.flag4 == 1) {
            //调整位置信息
            
            obj1_trans.localPosition = new Vector3(-posx - thickness * 2, 0, 0);
            obj2_trans.localPosition = new Vector3(-posx, 0, 0);
            obj3_trans.localPosition = new Vector3(posx - thickness * 2, 0, 0);
            obj4_trans.localPosition = new Vector3(posx, 0, 0);
            obj34up_trans.localPosition = new Vector3(posx + 0.0011f, scale / 2 + Circle_radius, 0);
            obj34down_trans.localPosition = new Vector3(posx + 0.0011f, -Circle_radius - scale / 2, 0);

            //调整scale的大小（直径大小）
            obj1_trans.localScale = new Vector3(scale, thickness, thickness_z);
            obj2_trans.localScale = new Vector3(scale, thickness, thickness_z);
            obj3_trans.localScale = new Vector3(scale, thickness, thickness_z);
            obj4_trans.localScale = new Vector3(scale, thickness, thickness_z);
            obj34up_trans.localScale = new Vector3(Circle_radius + Circle_radius, Cylinder34_Y, Cylinder34_Z);
            obj34down_trans.localScale = new Vector3(Circle_radius + Circle_radius, Cylinder34_Y, Cylinder34_Z);
        }

    }

    public static void Change_pos_b(float x, float y) {
        posx = x / 1000;
        scale = y / 1000;


        
        //改变相机的深度使得物体到人的深度固定
        if (posx == 2.448f)
        {
            Debug.Log("111");
            GameObject ovrcamera_Obj = GameObject.Find("OVRCameraRig");

            ovrcamera_Obj.transform.position = new Vector3(0, 0, -Vertical_distance1_far);
            thickness = thickness_far;

            GameObject obj_canvas = GameObject.Find("Canvas");
            RectTransform objcanvas_trans = obj_canvas.GetComponent<RectTransform>();
            objcanvas_trans.sizeDelta = new Vector2(7965, 2115);

        }
        if (posx == 0.612f)
        {
            Debug.Log("222");
            GameObject ovrcamera_Obj = GameObject.Find("OVRCameraRig");

            ovrcamera_Obj.transform.position = new Vector3(0, 0, -Vertical_distance2_far);
            thickness = thickness_far;

            GameObject obj_canvas = GameObject.Find("Canvas");
            RectTransform objcanvas_trans = obj_canvas.GetComponent<RectTransform>();
            objcanvas_trans.sizeDelta = new Vector2(7965, 2115);

        }
        if (posx == 1.632f)
        {
            Debug.Log("333");
            GameObject ovrcamera_Obj = GameObject.Find("OVRCameraRig");

            ovrcamera_Obj.transform.position = new Vector3(0, 0, -Vertical_distance1_medium);
            thickness = thickness_medium;

            GameObject obj_canvas = GameObject.Find("Canvas");
            RectTransform objcanvas_trans = obj_canvas.GetComponent<RectTransform>();
            objcanvas_trans.sizeDelta = new Vector2(5310, 1410);

        }
        if (posx == 0.408f)
        {
            Debug.Log("444");
            GameObject ovrcamera_Obj = GameObject.Find("OVRCameraRig");

            ovrcamera_Obj.transform.position = new Vector3(0, 0, -Vertical_distance2_medium);
            thickness = thickness_medium;

            GameObject obj_canvas = GameObject.Find("Canvas");
            RectTransform objcanvas_trans = obj_canvas.GetComponent<RectTransform>();
            objcanvas_trans.sizeDelta = new Vector2(5310, 1410);

        }
        if (posx == 0.544f)
        {
            Debug.Log("555");
            GameObject ovrcamera_Obj = GameObject.Find("OVRCameraRig");

            ovrcamera_Obj.transform.position = new Vector3(0, 0, -Vertical_distance1_near);
            thickness = thickness_near;

            GameObject obj_canvas = GameObject.Find("Canvas");
            RectTransform objcanvas_trans = obj_canvas.GetComponent<RectTransform>();
            objcanvas_trans.sizeDelta = new Vector2(2000, 550);

        }
        if (posx == 0.136f)
        {
            Debug.Log("666");
            GameObject ovrcamera_Obj = GameObject.Find("OVRCameraRig");

            ovrcamera_Obj.transform.position = new Vector3(0, 0, -Vertical_distance2_near);
            thickness = thickness_near;

            GameObject obj_canvas = GameObject.Find("Canvas");
            RectTransform objcanvas_trans = obj_canvas.GetComponent<RectTransform>();
            objcanvas_trans.sizeDelta = new Vector2(2000, 550);

        }

        GameObject obj1 = GameObject.Find("Cylinder1");
        GameObject obj2 = GameObject.Find("Cylinder2");
        Transform obj1_trans = obj1.GetComponent<Transform>();
        Transform obj2_trans = obj2.GetComponent<Transform>();

        obj1_trans.localPosition = new Vector3(-posx, 0, 0);
        obj2_trans.localPosition = new Vector3(posx, 0, 0);

        obj1_trans.localScale = new Vector3(scale, scale, thickness_z);
        obj2_trans.localScale = new Vector3(scale, scale, thickness_z);

    }

    






















}

