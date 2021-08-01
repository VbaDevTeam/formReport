using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GemBox.Spreadsheet;
using System.Data;
using MySql.Data.MySqlClient;
using System.Xml.Serialization;
using System.IO;

namespace formReport
{
  public class gesDati
  {
    private V v = new V();

    public gesDati()
    {
      SpreadsheetInfo.SetLicense("ESWL-ZQLT-26PA-QGPO");
    }

    public void readFile()
    {
      //nelle tracce non è conteggiato il time, per cui +1
      v.NoXml.datiTracce = new double[v.NoXml.numeroTracce + 1][];

      for (int n = 0; n <= v.NoXml.numeroTracce; n++)
      {
        v.NoXml.datiTracce[n] = new double[v.NoXml.nCampioniXTraccia];
      }

      for (int nTraccia = 0; nTraccia < v.NoXml.numeroTracce; nTraccia++)
      {
        int cols = v.NoXml.dtHeaderTracce.Columns.Count;
        string fullName = v.NoXml.dtHeaderTracce.Rows[nTraccia].ItemArray[1].ToString();
        char[] sepa = new char[1];
        sepa[0] = '.';
        string[] nameParts = fullName.Split(sepa);
        string trackName = nameParts[nameParts.Length - 1];

        for (int sampleNo = 0; sampleNo < v.NoXml.nCampioniXTraccia; sampleNo++)
        {
          v.NoXml.datiTracce[0][sampleNo] = Convert.ToDouble(((string)(v.NoXml.dtCampioni.Rows[nTraccia * v.NoXml.nCampioniXTraccia + sampleNo].ItemArray[0])).Replace('.', ','));
          v.NoXml.datiTracce[nTraccia + 1][sampleNo] = Convert.ToDouble(((string)(v.NoXml.dtCampioni.Rows[nTraccia * v.NoXml.nCampioniXTraccia + sampleNo].ItemArray[1])).Replace('.', ','));
        }
      }
    }

    public int scriveExcel(double[,,] ldatiTracceUpDown, int lNumTracce, double[][] lDatiTracceTot)
    {
      int result = 0;
      try
      {
        SpreadsheetInfo.SetLicense("ESWL-ZQLT-26PA-QGPO");


        int rowData = 78;
        int colDataPressUp = 11;
        int colDataPressDown = 19;
        int colDataUp = 14;
        int colDataDown = 22;
        v.NoXml.xFile = new ExcelFile();
        v.NoXml.xFile.LoadXlsx(@"C:\REM\Settings\0108\Modelli\modello_report_" + v.NoXml.nDisegno + ".xlsx", XlsxOptions.PreserveMakeCopy);
        v.NoXml.xFile.Worksheets["data"].Cells[4, 4].Value = v.NoXml.nDisegno;
        v.NoXml.xFile.Worksheets["data"].Cells[6, 4].Value = v.NoXml.serialePz;
        v.NoXml.xFile.Worksheets["data"].Cells[77, 4].Value = v.NoXml.valueApertura;
        v.NoXml.xFile.Worksheets["data"].Cells[78, 4].Value = v.NoXml.valueTrafilamento;
        //v.NoXml.xFile.Worksheets["data"].Cells[79, 4].Value = v.NoXml.valuePerc1;
        //v.NoXml.xFile.Worksheets["data"].Cells[80, 4].Value = v.NoXml.valuePerc2;
        v.NoXml.xFile.Worksheets["data"].Cells[2, 22].Value =
          DateTime.Now.Day.ToString() + " / " + DateTime.Now.Month.ToString() + " / " + DateTime.Now.Year.ToString();

        v.NoXml.xFile.Worksheets["data"].Cells[77, 6].Value = v.NoXml.minAper;
        v.NoXml.xFile.Worksheets["data"].Cells[77, 7].Value = v.NoXml.maxAper;
        //v.NoXml.xFile.Worksheets["data"].Cells[78, 6].Value = v.NoXml.minChiu;
        //v.NoXml.xFile.Worksheets["data"].Cells[78, 7].Value = v.NoXml.maxChiu;
        v.NoXml.xFile.Worksheets["data"].Cells[78, 6].Value = 0.0;
        v.NoXml.xFile.Worksheets["data"].Cells[78, 7].Value = v.NoXml.maxTraf;
        //v.NoXml.xFile.Worksheets["data"].Cells[80, 6].Value = v.NoXml.minPerc;
        //v.NoXml.xFile.Worksheets["data"].Cells[80, 7].Value = v.NoXml.maxPerc;

        //int appN0 = 0;
        //bool oneShot = true; 

        //for (int p=0; p< 500; p++)
        //{
        //  if (ldatiTracceUpDown[1, 2, p] > 0.001 && oneShot)
        //  {
        //    oneShot = false;
        //    appN0 = p;
        //  }
        //}




        //for (int n = 1; n <= 232; n++)
        //{
        //  if (rowData < 232)
        //  {
        //    v.NoXml.xFile.Worksheets["data"].Cells[rowData, colDataPressUp - 1].Value = n;
        //    v.NoXml.xFile.Worksheets["data"].Cells[rowData, colDataPressUp].Value = ldatiTracceUpDown[0, 1, n];
        //    v.NoXml.xFile.Worksheets["data"].Cells[rowData, colDataUp].Value = ldatiTracceUpDown[0, 2, n];
        //    v.NoXml.xFile.Worksheets["data"].Cells[rowData, colDataUp + 2].Value = ldatiTracceUpDown[0, 3, n];

        //    if (v.NoXml.salescende)
        //    {
        //      v.NoXml.xFile.Worksheets["data"].Cells[rowData, colDataPressDown - 1].Value = n;
        //      v.NoXml.xFile.Worksheets["data"].Cells[rowData, colDataPressDown].Value = ldatiTracceUpDown[1, 1, appN0 + n];
        //      v.NoXml.xFile.Worksheets["data"].Cells[rowData, colDataDown].Value = ldatiTracceUpDown[1, 2, appN0 + n];
        //      v.NoXml.xFile.Worksheets["data"].Cells[rowData, colDataDown + 2].Value = ldatiTracceUpDown[1, 3, appN0 + n];
        //    }
        //  }
        //  rowData++;
        //}


        double nStep = 160.0;

        //Determino il minimo punto di portata
        //Sarebbe il punto finale di discesa

        int indexMinDisc = 0;
        double minDiscPort = 0.0;
        bool oneShot = true;

        for (int n = v.NoXml.nCampioniXTraccia; n > 0; n--)
        {
          double app = lDatiTracceTot[2][n - 1];
          if (app > 0.02 && oneShot)
          {
            indexMinDisc = n;
            oneShot = false;
          }
        }


        //Determino il punto di inversione della curva
        //Punto finale di salita
        //Punto iniziale di discesa

        int indexMax = 0;
        double maxPort = 0.0;

        for (int n = 0; n <= lDatiTracceTot[2].Length - 1; n++)
        {
          if (maxPort < lDatiTracceTot[2][n])
          {
            indexMax = n;
            maxPort = lDatiTracceTot[2][n];
          }
        }

        double[][] lSalita = new double[2][];
        lSalita[0] = new double[indexMax + 1];
        lSalita[1] = new double[indexMax + 1];

        double[][] lDiscesa = new double[2][];
        lDiscesa[0] = new double[v.NoXml.nCampioniXTraccia - indexMax + 2];
        lDiscesa[1] = new double[v.NoXml.nCampioniXTraccia - indexMax + 2];




        for (int n = 0; n < v.NoXml.nCampioniXTraccia; n++)
        {
          //v.NoXml.xFile.Worksheets["data"].Cells[78, 34].Value = " indice ";
          //v.NoXml.xFile.Worksheets["data"].Cells[78, 35].Value = " Pressione ";
          //v.NoXml.xFile.Worksheets["data"].Cells[78, 36].Value = " Pressione ";
          //v.NoXml.xFile.Worksheets["data"].Cells[78, 37].Value = " Portata ";
          //v.NoXml.xFile.Worksheets["data"].Cells[78, 38].Value = " Temperatura ";


          //v.NoXml.xFile.Worksheets["data"].Cells[n + 79, 34].Value =  n;
          //v.NoXml.xFile.Worksheets["data"].Cells[n + 79, 35].Value = lDatiTracceTot[0][n];
          //v.NoXml.xFile.Worksheets["data"].Cells[n + 79, 36].Value = lDatiTracceTot[1][n];
          //v.NoXml.xFile.Worksheets["data"].Cells[n + 79, 37].Value = lDatiTracceTot[2][n];
          //v.NoXml.xFile.Worksheets["data"].Cells[n + 79, 38].Value = lDatiTracceTot[3][n];

          //v.NoXml.xFile.Worksheets["data"].Cells[n + 79, 40].Value = ldatiTracceUpDown[0, 1, n];
          //v.NoXml.xFile.Worksheets["data"].Cells[n + 79, 41].Value = ldatiTracceUpDown[0, 2, n];
          //v.NoXml.xFile.Worksheets["data"].Cells[n + 79, 42].Value = ldatiTracceUpDown[0, 3, n];

          //v.NoXml.xFile.Worksheets["data"].Cells[n + 79, 40 + 5].Value = ldatiTracceUpDown[1, 1, n];
          //v.NoXml.xFile.Worksheets["data"].Cells[n + 79, 41 + 5].Value = ldatiTracceUpDown[1, 2, n];
          //v.NoXml.xFile.Worksheets["data"].Cells[n + 79, 42 + 5].Value = ldatiTracceUpDown[1, 3, n];


          if (n < indexMax)
          {


            int initColonna = 45;
            v.NoXml.xFile.Worksheets["data"].Cells[78, initColonna + 0].Value = " indice ";
            v.NoXml.xFile.Worksheets["data"].Cells[78, initColonna + 1].Value = " Pressione ";
            v.NoXml.xFile.Worksheets["data"].Cells[78, initColonna + 2].Value = " Pressione ";
            v.NoXml.xFile.Worksheets["data"].Cells[78, initColonna + 3].Value = " Portata ";
            v.NoXml.xFile.Worksheets["data"].Cells[78, initColonna + 4].Value = " Temperatura ";


            v.NoXml.xFile.Worksheets["data"].Cells[n + 79, initColonna + 0].Value = n;
            v.NoXml.xFile.Worksheets["data"].Cells[n + 79, initColonna + 1].Value = lDatiTracceTot[0][n];
            v.NoXml.xFile.Worksheets["data"].Cells[n + 79, initColonna + 2].Value = lDatiTracceTot[1][n];
            v.NoXml.xFile.Worksheets["data"].Cells[n + 79, initColonna + 3].Value = lDatiTracceTot[2][n];
            v.NoXml.xFile.Worksheets["data"].Cells[n + 79, initColonna + 4].Value = lDatiTracceTot[3][n];

            lSalita[0][n] = lDatiTracceTot[1][n];
            lSalita[1][n] = lDatiTracceTot[2][n];

          }
          else
          {
            int initColonna = 52;
            v.NoXml.xFile.Worksheets["data"].Cells[78, initColonna + 0].Value = " indice ";
            v.NoXml.xFile.Worksheets["data"].Cells[78, initColonna + 1].Value = " Pressione ";
            v.NoXml.xFile.Worksheets["data"].Cells[78, initColonna + 2].Value = " Pressione ";
            v.NoXml.xFile.Worksheets["data"].Cells[78, initColonna + 3].Value = " Portata ";
            v.NoXml.xFile.Worksheets["data"].Cells[78, initColonna + 4].Value = " Temperatura ";


            v.NoXml.xFile.Worksheets["data"].Cells[n - indexMax + 79, initColonna + 0].Value = v.NoXml.nCampioniXTraccia - (n - indexMax);
            v.NoXml.xFile.Worksheets["data"].Cells[n - indexMax + 79, initColonna + 1].Value = lDatiTracceTot[0][(v.NoXml.nCampioniXTraccia - (n - indexMax)) - 1];
            v.NoXml.xFile.Worksheets["data"].Cells[n - indexMax + 79, initColonna + 2].Value = lDatiTracceTot[1][(v.NoXml.nCampioniXTraccia - (n - indexMax)) - 1];
            v.NoXml.xFile.Worksheets["data"].Cells[n - indexMax + 79, initColonna + 3].Value = lDatiTracceTot[2][(v.NoXml.nCampioniXTraccia - (n - indexMax)) - 1];
            v.NoXml.xFile.Worksheets["data"].Cells[n - indexMax + 79, initColonna + 4].Value = lDatiTracceTot[3][(v.NoXml.nCampioniXTraccia - (n - indexMax)) - 1];

            lDiscesa[0][n - indexMax] = lDatiTracceTot[1][(v.NoXml.nCampioniXTraccia - (n - indexMax)) - 1];
            lDiscesa[1][n - indexMax] = lDatiTracceTot[2][(v.NoXml.nCampioniXTraccia - (n - indexMax)) - 1];

          }







        }



        for (int i = 0; i < nStep; i++)
        {
          double fattoreKSalita = (double)(indexMax / nStep);
          int app = (int)Math.Truncate(i * fattoreKSalita);

          //if (app > ops)
          //  app = ops;


          //int initColonna = 72;
          //v.NoXml.xFile.Worksheets["data"].Cells[78, initColonna + 0].Value = "SALITA: Pressione ";
          //v.NoXml.xFile.Worksheets["data"].Cells[78, initColonna + 1].Value = "SALITA: Portata ";

          v.NoXml.xFile.Worksheets["data"].Cells[i + 79, colDataPressUp].Value = lSalita[0][app];
          v.NoXml.xFile.Worksheets["data"].Cells[i + 79, colDataUp].Value = lSalita[1][app];

          double fattoreKDiscesa = (double)((v.NoXml.nCampioniXTraccia - indexMax) / nStep);
          app = (int)Math.Truncate(i * fattoreKDiscesa);

          //if (app > ops)
          //app = ops;

          //initColonna = 76;
          //v.NoXml.xFile.Worksheets["data"].Cells[78, initColonna + 0].Value = "SALITA: Pressione ";
          //v.NoXml.xFile.Worksheets["data"].Cells[78, initColonna + 1].Value = "SALITA: Portata ";
          v.NoXml.xFile.Worksheets["data"].Cells[i + 79, colDataPressDown].Value = lDiscesa[0][app];
          v.NoXml.xFile.Worksheets["data"].Cells[i + 79, colDataDown].Value = lDiscesa[1][app];




        }


        v.NoXml.originalFileName = @"C:\REM\Settings\0108\Reports\BeRqaf - uut - " + v.NoXml.nDisegno + "- seriale - " + v.NoXml.serialePz + " - "
            + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + "."
            + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + ".xlsx";
        v.NoXml.xFile.SaveXlsx(v.NoXml.originalFileName);
      }
      catch (Exception ex)
      {
        string error = ex.ToString();
        result = -1;

      }
      return result;
      //string fileName = originalFileName.Substring(0, originalFileName.Length - 5) + ".xlsx";


      //System.Diagnostics.Process.Start(originalFileName);

    }




    public int scriviExcelNew(List<puntoAcq> myList)
    {

      int result = 0;
      try
      {
        SpreadsheetInfo.SetLicense("ESWL-ZQLT-26PA-QGPO");


        int rowData = 78;
        int colDataPressUp = 11;
        int colDataPressDown = 19;
        int colDataUp = 14;
        int colDataDown = 22;
        v.NoXml.xFile = new ExcelFile();
        v.NoXml.xFile.LoadXlsx(@"C:\REM\Settings\0387\Modelli\modello_report.xlsx", XlsxOptions.PreserveMakeCopy);

        int nRiga = 0;
        double millisecondToAdd = 0.0;


        for (int n=0; n< myList.Count; n++)
        {
          //millisecondToAdd = 0.0;
          for (int y = 0; y < myList[n].myDoubleListIn.Count; y++)
          {


            DateTime myDt = myList[n].timeSt;
            DateTime apptDFt = myDt.AddMilliseconds(millisecondToAdd);



            v.NoXml.xFile.Worksheets["data"].Cells[nRiga + 1, 2].Value = myList[n].id;
            v.NoXml.xFile.Worksheets["data"].Cells[nRiga + 1, 3].Value = apptDFt;
            v.NoXml.xFile.Worksheets["data"].Cells[nRiga + 1, 4].Value = myList[n].pressIn;
            v.NoXml.xFile.Worksheets["data"].Cells[nRiga + 1, 5].Value = myList[n].pressOut;
            v.NoXml.xFile.Worksheets["data"].Cells[nRiga + 1, 6].Value = myList[n].tempFluidoIn;
            v.NoXml.xFile.Worksheets["data"].Cells[nRiga + 1, 7].Value = myList[n].tempFluidoOut;
            v.NoXml.xFile.Worksheets["data"].Cells[nRiga + 1, 8].Value = myList[n].tempCella;
            v.NoXml.xFile.Worksheets["data"].Cells[nRiga + 1, 9].Value  = millisecondToAdd;
            v.NoXml.xFile.Worksheets["data"].Cells[nRiga + 1, 10].Value = myList[n].portFluido;
            v.NoXml.xFile.Worksheets["data"].Cells[nRiga + 1, 11].Value = myList[n].note;
            v.NoXml.xFile.Worksheets["data"].Cells[nRiga + 1, 12].Value = myList[n].myDoubleListIn[y];
            v.NoXml.xFile.Worksheets["data"].Cells[nRiga + 1, 13].Value = myList[n].myDoubleListOut[y];
            millisecondToAdd = millisecondToAdd + 50.0;
            nRiga++;
          }


        }


        v.NoXml.originalFileName = @"C:\REM\Settings\0387\Report\Report"
    + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + "."
    + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + ".xlsx";
        v.NoXml.xFile.SaveXlsx(v.NoXml.originalFileName);

      }
      catch(Exception ex)
      {

        result = -1;

      }





        return result;

    }

















    private double[,,] stepGen(double[][] ldatiTracce, int lnCampioniXTraccia, int lColPassi)
    {




      int indexMax = 0;
      double maxPort = 0.0;

      for (int n = 0; n <= ldatiTracce[2].Length - 1; n++)
      {
        if (maxPort < ldatiTracce[2][n])
        {
          indexMax = n;
          maxPort = ldatiTracce[2][n];
        }
      }










      double[][] ldatiTracceUp;
      ldatiTracceUp = new double[5][];
      double[][] ldatiTracceDown;
      ldatiTracceDown = new double[5][];

      double[] stepPress = new double[161];

      for (int n = 0; n <= 160; n++)
      {
        stepPress[n] = n / 10.0;
      }


      double maxValue = 0.0;
      int indexMaxValue = 0;

      for (int n = 1; n < lnCampioniXTraccia; n++)
      {

        double myVal = v.NoXml.datiTracce[lColPassi][n];
        double precVal = v.NoXml.datiTracce[lColPassi][n - 1];

        if (myVal > maxValue)
        {

          maxValue = myVal;
          indexMaxValue = n;
        }
      }

      for (int n = 0; n < 5; n++)
      {
        ldatiTracceUp[n] = new double[indexMaxValue + 1];
        ldatiTracceDown[n] = new double[(lnCampioniXTraccia - indexMaxValue) + 1];
      }

      for (int n = 0; n <= indexMaxValue; n++)
      {
        for (int col = 0; col <= v.NoXml.numeroTracce; col++)
        {
          try
          {
            ldatiTracceUp[col][n] = ldatiTracce[col][n];
          }
          catch
          { }
        }
      }

      int indexMinDisc = 0;
      double minDiscPort = 0.0;
      bool oneShot = true;

      for (int n = lnCampioniXTraccia; n > 0; n--)
      {
        double app = ldatiTracce[2][n - 1];
        if (app > 0.02 && oneShot)
        {
          indexMinDisc = n;
          oneShot = false;
        }


      }


      for (int n = indexMaxValue; n <= indexMinDisc; n++)
      {
        for (int col = 0; col <= v.NoXml.numeroTracce; col++)
        {
          try
          {
            ldatiTracceDown[col][n - indexMaxValue] = ldatiTracce[col][(indexMinDisc - (n - indexMaxValue)) - 1];
          }
          catch
          { }
        }
      }

      //inizializzati ad 1 per il campo 0.0bar


      double[,,] myMatrix = new double[2, 4, lnCampioniXTraccia];


      bool pressFound = false;
      for (int i = 1; i <= indexMaxValue; i++)
      {
        double resultincna = (double)(indexMaxValue / 160.0);


        int app = (int)Math.Truncate(i * resultincna);

        if (i == 4)
        { }

        if (app > i)
          app = i;


        for (int col = 0; col <= v.NoXml.numeroTracce; col++)
        {
          myMatrix[0, col, i] = ldatiTracceUp[col][app];
        }


      }
      for (int i = 1; i <= indexMinDisc - indexMaxValue; i++)
      {
        double resultincna = (double)(indexMinDisc - indexMaxValue / 160.0);


        int app = (int)Math.Truncate(i * resultincna);

        if (i == 4)
        { }

        if (app > i)
          app = i;

        for (int col = 0; col <= v.NoXml.numeroTracce; col++)
        {
          myMatrix[1, col, i] = ldatiTracceDown[col][app];
        }

      }
      return myMatrix;
    }




    private double[,,] stepGenOLD(double[][] ldatiTracce, int lnCampioniXTraccia, int lColPassi)
    {
      int rowPtrUp = 1;
      int rowPtrDown = 1;

      double[,,] myMatrix = new double[2, 4, 200];

      double actualStep = 0.0;
      //spazzolo tutto il buffer ricevuto dal plc
      for (int n = 1; n < lnCampioniXTraccia; n++)
      {
        //leggo il prossimo dato di pressione disponibile
        double myVal = v.NoXml.datiTracce[lColPassi][n];
        double myPort = v.NoXml.datiTracce[2][n];
        if (myVal > actualStep + 0.1 && myVal < 16.2 && myPort > 0.0)
        {
          if (myVal > actualStep + 0.2)
            n--;

          actualStep += 0.1;
          for (int col = 0; col <= v.NoXml.numeroTracce; col++)
          {
            myMatrix[0, col, rowPtrUp] = ldatiTracce[col][n];
          }
          rowPtrUp++;
        }
        else if (myVal < actualStep - 0.1 && myVal < 16.2)
        {
          if (myVal < actualStep - 0.2)
            n--;

          actualStep -= 0.1;
          for (int col = 0; col <= v.NoXml.numeroTracce; col++)
          {
            myMatrix[1, col, rowPtrDown] = ldatiTracce[col][n];
          }
          rowPtrDown++;
        }
      }

      return myMatrix;
    }

    public void getDataMemory(bool init = true)
    {
      if (init)
      {
        v.Io.DTB10.CodDisegnoWork = v.NoXml.nDisegno;
        v.Io.DTB10.CodSerialeWork = v.NoXml.serialePz;
      }
      else
      {
        v.NoXml.nDisegno = v.Io.DTB10.CodDisegnoWork;
        v.NoXml.serialePz = v.Io.DTB10.CodSerialeWork;
      }





      v.NoXml.nCampioniXTraccia = (int)v.Io.DTB10.myIndex;
      v.NoXml.datiTracce = new double[4][];
      v.NoXml.numeroTracce = 3;
      for (int n = 0; n <= 3; n++)
      {
        v.NoXml.datiTracce[n] = new double[v.NoXml.nCampioniXTraccia];
      }
      for (int n = 1; n < v.NoXml.nCampioniXTraccia; n++)
      {
        v.NoXml.datiTracce[0][n] = v.Io.DTB10.myVett[n].press;
        v.NoXml.datiTracce[1][n] = v.Io.DTB10.myVett[n].press;
        v.NoXml.datiTracce[2][n] = v.Io.DTB10.myVett[n].port;
        v.NoXml.datiTracce[3][n] = v.Io.DTB10.myVett[n].temp;
      }

      v.NoXml.valueApertura = v.Io.DTB10.PR_OpenValue;
      v.NoXml.valueChiusura = v.Io.DTB10.PR_CloseValue;
      v.NoXml.valueTrafilamento = v.Io.DTB10.PO_Trafilamento;
      int app2 = (int)(v.Io.DTB10.PC_Cell1);
      int app1 = (int)(v.Io.DTB10.PC_Coll2); // Okkio modificato per singolo cella, da apportare modifica con booleano di controllo test

      v.NoXml.valuePerc1 = app1;
      v.NoXml.valuePerc2 = app2;

      if (v.NoXml.valuePerc1 > 100)
      {
        v.NoXml.valuePerc1 = 100;
      }
      if (v.NoXml.valuePerc2 > 100)
      {
        v.NoXml.valuePerc2 = 100;
      }

      v.NoXml.valuePercTot = v.Io.DTB10.PC_CollTot;


      v.NoXml.minAper = v.Io.DTB10.spOpenValPress - v.Io.DTB10.spOpenValToll;
      v.NoXml.maxAper = v.Io.DTB10.spOpenValPress + v.Io.DTB10.spOpenValToll;
      v.NoXml.minChiu = v.Io.DTB10.spClosValPress - v.Io.DTB10.spClosValToll;
      v.NoXml.maxChiu = v.Io.DTB10.spClosValPress + v.Io.DTB10.spClosValToll;
      v.NoXml.minPerc = v.Io.DTB10.spTrafValToll;




      // v.NoXml.maxTraf = (v.Io.BancoAnalisi.DTB10.spTrafValToll / 60.0) * v.Io.BancoAnalisi.DTB10.spTrafValDuraT;

      v.NoXml.maxTraf = v.Io.DTB10.spTrafValToll;
      //v.NoXml.minAper = v.dataConfTest.spOpenValuePress - v.dataConfTest.spOpenValueToll;
      //v.NoXml.maxAper = v.dataConfTest.spOpenValuePress + v.dataConfTest.spOpenValueToll;
      //v.NoXml.minChiu = v.dataConfTest.spCloseValuePress - v.dataConfTest.spCloseValueToll;
      //v.NoXml.maxChiu = v.dataConfTest.spCloseValuePress + v.dataConfTest.spCloseValueToll;
      //v.NoXml.minPerc = v.dataConfTest.spColliValueToll;
      v.NoXml.maxPerc = 100;
      if (init)
        saveDBData();
    }


    public void saveDBData()
    {
      MySqlConnection connessioneDB = new MySqlConnection(DEF.strConnDb);

      StringWriter strWriter = new StringWriter(new StringBuilder());
      XmlSerializer xSerizer = new XmlSerializer(typeof(datiBanco));
      xSerizer.Serialize(strWriter, v.Io.DTB10);
      string strData = strWriter.ToString();
      strData = strData.Replace("\"", "\"\"");
      strData = strData.Replace("'", "''");

      string comandoSQL = "INSERT INTO `vba0108_ber`.`tblProve`" +
          "(`prCodiceUtente`, `prCdId`, `prLtId`, `prSeriale`, `prDescr`, `prDataTest`, `prData`)" +
          "VALUES (" + 1 + "," + v.NoXml.codDis + "," + 1 + ", '" + v.NoXml.serialePz + "' , '" + v.NoXml.descrizTest + "', \"" + strData + "\", NOW()" + ")";
      connessioneDB.Open();

      MySqlCommand comandoDB = new MySqlCommand();

      comandoDB.CommandText = comandoSQL;
      comandoDB.Connection = connessioneDB;
      comandoDB.ExecuteNonQuery();
      //if (comandoDB.ExecuteNonQuery() != 0)
      //  result = -1;

      connessioneDB.Close();
      connessioneDB = null;
    }



    public int genAll()
    {
      if (v.NoXml.datiTracce != null)
      {
        double[,,] datiTracceUpDown = stepGen(v.NoXml.datiTracce, v.NoXml.nCampioniXTraccia, 1);
        int result = scriveExcel(datiTracceUpDown, 3, v.NoXml.datiTracce);
        return result;
      }
      else
      {
        return -1;
      }
    }

    public void oscxRead(string fileName)
    {
      //limitare tipi file da aprire
      v.NoXml.ds1 = new DataSet("dataIn");
      v.NoXml.numeroTracce = 0;
      v.NoXml.nCampioniXTraccia = 0;

      //lettura file xml su dataset
      v.NoXml.originalFileName = fileName;
      v.NoXml.ds1.ReadXml(fileName);
      v.NoXml.dtHeaderTracce = v.NoXml.ds1.Tables["track"];
      v.NoXml.dtCampioni = v.NoXml.ds1.Tables["sample"];

      v.NoXml.numeroTracce = v.NoXml.dtHeaderTracce.Rows.Count;
      v.NoXml.nCampioniXTraccia = v.NoXml.dtCampioni.Rows.Count / v.NoXml.numeroTracce;
      readFile();

    }

    public void excelRead(string fileName)
    {
      //limitare tipi file da aprire
      v.NoXml.ds1 = new DataSet("dataIn");
      v.NoXml.numeroTracce = 3;
      v.NoXml.nCampioniXTraccia = 2048;

      v.NoXml.datiTracce = new double[v.NoXml.numeroTracce + 1][];

      for (int n = 0; n <= v.NoXml.numeroTracce; n++)
      {
        v.NoXml.datiTracce[n] = new double[v.NoXml.nCampioniXTraccia];
      }

      //lettura file xml su dataset
      v.NoXml.originalFileName = fileName;

      v.NoXml.xFile = new ExcelFile();
      v.NoXml.xFile.LoadXlsx(fileName, XlsxOptions.PreserveMakeCopy);
      ExcelWorksheet rawData = v.NoXml.xFile.Worksheets["dataTot"];
      int y = 1;
      string testVal = rawData.Cells[y, 2].Value.ToString();
      double testDoub = Convert.ToDouble(testVal);
      while (Convert.ToDouble(rawData.Cells[y, 2].Value) != 0)
      {
        v.NoXml.datiTracce[0][y] = Convert.ToDouble(rawData.Cells[y, 1].Value);
        v.NoXml.datiTracce[1][y] = Convert.ToDouble(rawData.Cells[y, 2].Value);
        v.NoXml.datiTracce[2][y] = Convert.ToDouble(rawData.Cells[y, 3].Value);
        v.NoXml.datiTracce[3][y] = Convert.ToDouble(rawData.Cells[y, 4].Value);
        y++;
      }


      //readFile();
    }

  }
}