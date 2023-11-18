using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using UnityEngine;

public class pruebamesh : MonoBehaviour
{
    int ipos,ifpos,spos, sfpos ,fspos,ispos;
    StreamReader sr1;
    Thread hilo;
    string[] colors;
    Mesh m;
    List<Color32> colores;
    // Start is called before the first frame update

    void generatetriangles(ref List<List<Vector3>> puntos, ref List<int> trianglulos)
    {
        int tampuntos = puntos.Count;
        float trianglelimit = 0.0001f;
        for (int i = 0; i < tampuntos - 1; i++)
        {
            for (int j = 1; j < tampuntos; j++)
            {
                int tami = puntos[i].Count;
                int tamj = puntos[j].Count;

                for (int k = 0; k < tami - 1; k++)
                {
                    Vector3 p3 = puntos[j][k];
                    Vector3 p1 = puntos[i][k];
                    Vector3 p2 = puntos[i][k + 1];
                    if (distancia(p1, p2) < trianglelimit && distancia(p2,p3) < trianglelimit && distancia(p1,p3) < .1)
                    {
                        trianglulos.Add(i + k);
                        trianglulos.Add(j + k);
                        trianglulos.Add(i + k + 1);
                    }

                }

            }
        }
    }
    void generatetriangles(ref List<int> triangulos,ref List<Vector3> puntos,ref List<int> indices)
    {
        for(int i = 0; i < indices.Count-2; i++)
        {
            ipos = indices[i];
            ifpos = spos = indices[i + 1];
            sfpos = indices[i + 2];
            float trianglelimit = 0.0001f;
            for (; ipos < ifpos; ipos++)
            {
                for (; spos < sfpos; spos++)
                {
                    try
                    {
                        //Debug.LogWarning(distancia(puntos[spos], puntos[spos + 1]));
                        if(distancia(puntos[spos], puntos[spos + 1]) < trianglelimit && distancia(puntos[ipos], puntos[ipos + 1]) < trianglelimit && distancia(puntos[spos], puntos[ipos]) < .1)
                        {
                            triangulos.Add(ipos);
                            triangulos.Add(spos);
                            triangulos.Add(spos + 1);

                            triangulos.Add(ipos);
                            triangulos.Add(spos + 1);
                            triangulos.Add(ipos+1);
                            


                        }




                        /*
                        //if(distancia(puntos[spos],puntos[spos + 1]) < trianglelimit && distancia(a, puntos[spos]) < highlimit && distancia(a, puntos[spos + 1]) < highlimit)
                        //{
                        //    triangulos.Add(ipos);
                        //    triangulos.Add(spos);
                        //    triangulos.Add(spos + 1);
                        //}
                        //if (distancia(puntos[ipos], puntos[ipos + 1]) < trianglelimit && distancia(a, puntos[spos]) < highlimit && distancia(puntos[ipos + 1], puntos[spos]) < highlimit)
                        //{
                        //    triangulos.Add(spos);
                        //    triangulos.Add(ipos + 1);
                        //    triangulos.Add(ipos);
                            
                            
                        //}
                        //if (distancia(a, puntos[spos]) < trianglelimit && distancia(a, puntos[spos + 1]) < trianglelimit)
                        //{

                        //    triangulos.Add(ipos);
                        //    triangulos.Add(spos);
                        //    triangulos.Add(spos + 1);

                        //}
                        //else
                        //{

                        //    if (distancia(puntos[ipos], puntos[ipos+1]) < trianglelimit && distancia(puntos[spos], puntos[ipos + 1]) < trianglelimit) {
                        //        triangulos.Add(ipos);
                        //        triangulos.Add(ipos + 1);
                        //        triangulos.Add(spos);
                        //    }
                        //    ipos++;
                        //}
                        */
                    }
                    catch (System.Exception e)
                    {

                    }

                }

            }


        }
        
    }
    float distancia(Vector3 a, Vector3 b)
    {
     
        
        float difx = a.x - b.x;
        float dify = a.y - b.y;
        float difz = a.z - b.z;
       
        return difx * difx + dify * dify + difz * difz;
    }
    void Start()
    {
       

       
        
        m = GetComponent<MeshFilter>().mesh;
        
        sr1 = new StreamReader("D:\\IHCnuevo program\\LiveScan3D-master\\bin\\f00001.ply.txt");
        string[] data = sr1.ReadLine().Split(",");
        colors = sr1.ReadLine().Split(",");
        sr1.Close();
        colores = new List<Color32>();
        for (int i = 0; i < colors.Length - 1; i += 3)
        {
            colores.Add(new Color32(byte.Parse(colors[i]), byte.Parse(colors[i + 1]), byte.Parse(colors[i + 2]),1));

        }

        List<Vector3> puntos = new List<Vector3>();
        int c = 2000;
        List<List<Vector3>> semiorderedpoints = new List<List<Vector3>>(c);

        while (c != 0)
        {
            semiorderedpoints.Add(new List<Vector3>());
            c--;
        }
        var watch = System.Diagnostics.Stopwatch.StartNew();
        
        List<int> triangulos = new List<int>();
        List<int> indices = new List<int>();
        //indices.Add(0);
        //float val = 10.0f;
        //float diffy = 0.001f;
        //float posminy = 10.0f;
        //float posmaxy = -10.0f;
        //int ini = 0;
        //for (int i = 0; i < 123; i += 3)
        for (int i = 0; i < data.Length - 1; i += 3)
        {
            Vector3 vecaux = new Vector3(float.Parse(data[i]), float.Parse(data[i + 1]), float.Parse(data[i + 2]));

            float pos = (vecaux.y * 1000) + 1000;
            int ipos = Convert.ToInt32(pos);
            semiorderedpoints[ipos%2000].Add(vecaux);



            //puntos.Add(vecaux);


            //float aux = float.Parse(data[i]);
            //float actualy = float.Parse(data[i + 1]);

            //posminy = (posminy < actualy) ? posminy : actualy;
            //posmaxy = (posmaxy > actualy) ? posmaxy : actualy;
            //if (posmaxy - posminy > diffy)
            //{
            //    posminy = posmaxy;

            //    indices.Add(ini);
            //}
            //val = aux;
            //ini++;
        }

        //foreach(List<Vector3> a in semiorderedpoints)
        //{
        //    if (a.Count != 0)
        //    {
        //        foreach (Vector3 p in a)
        //        {
        //            puntos.Add(p);
        //        }
        //        if (puntos.Count == 0) indices.Add(puntos.Count + a.Count - 1);
        //        else indices.Add(puntos.Count + a.Count - 2);
        //    }

        //}

        //generacion de triángulos
        int ia = 0;
        while (ia != semiorderedpoints.Count)
        {
            if (semiorderedpoints[ia].Count == 0)
            {
                semiorderedpoints.RemoveAt(ia);
            }
            else
            {
                ia++;
            }
        }
        int ia2 = 0;
        indices.Add(ia2);
        foreach(List<Vector3> a in semiorderedpoints)
        {
            for(int i = 0; i < a.Count; i++)
            {
                puntos.Add(a[i]);
                ia2++;
            }
            indices.Add(ia2);
        }
        generatetriangles(ref semiorderedpoints, ref triangulos);
        //generatetriangles(ref triangulos,ref puntos,ref indices);
        watch.Stop();

        Debug.LogWarning($"Execution Time: {watch.ElapsedMilliseconds} ms");





        //Vector3[] puntos = { new Vector3(0.014f, 0.637f, -0.416f), 
        //    new Vector3(0.038f, 0.661f, -0.494f), new Vector3(-0.026f, 0.65f, -0.451f),
        //    new Vector3(0.045f,0.647f,-0.475f),new Vector3(0.037f,0.624f,-0.414f) };
        //Color32[] colores= { new Color32(26, 31, 20,255), new Color32(32, 35, 21, 255), new Color32(33, 33, 25, 255),
        //new Color32(38,24,41,255),new Color32(40,26,36,255)};
        //Color[] coloress = { new Color(255,0,0), new Color(255, 0, 0) , new Color(0, 255, 0) };
        //int[] triangulos = { 0, 1, 2, 1,4,3 };


        m.vertices = puntos.ToArray();
        m.triangles =triangulos.ToArray();
        m.colors32 = colores.ToArray();
        m.RecalculateNormals();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
