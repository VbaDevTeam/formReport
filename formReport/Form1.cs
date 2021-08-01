using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace formReport
{
  public partial class Form1 : Form
  {
    dbGeneric dbAcc = new dbGeneric();
    gesDati gData = new gesDati();
    string query;
    string dataInit = "2021-04-25 18:30:35";
    string dataEnd = "2021-05-30 00:00:00";

    public Form1()
    {
      InitializeComponent();
      dtInitReport.CustomFormat = "yyyy-mm-dd hh:mm:ss";
      dtEndReport.CustomFormat = "yyyy-mm-dd hh:mm:ss";

    }

    private void btnGetData_Click(object sender, EventArgs e)
    {
      Button tmp = (Button)sender;

      switch (tmp.Name)
      {

        case "btnGetDataLfreq":
          recuperaDatiLfreq();
          break;

        case "btnGetDataHfreq":
          recuperaDatiHfreq();
          break;

      }



    }


    private void recuperaDatiLfreq()
    {
      query = "SELECT dlId, dlTimeSt, dlPrIn, dlPrOut, dlTFlIn, dlTflOut, dlTcella, dlRhCella, dlQFl " +
            "FROM dataLog where dlTimeSt > '" + dtInitReport.Value.Date.ToString("yyyy-MM-dd hh:mm:ss") + "' AND dlTimeSt < '" + dtEndReport.Value.Date.ToString("yyyy-MM-dd hh:mm:ss") + "'";


      MySqlDataReader result = dbAcc.readTest(DEF.strConnDb, query);


      DataTable dt = new DataTable();
      dt.Load(result);
      int numberOfResults = dt.Rows.Count;
      List<puntoAcq> myPoints = new List<puntoAcq>();
      //int dim = result.;

      for (int n = 0; n < dt.Rows.Count; n++)
      {

        Random random = new Random();
        int numRnd = random.Next(25);
        int punt = 0;
        bool speciale = true;

        if (n % 60 == 0)
        {
          if (speciale && n > 25)
            punt = n - numRnd;
          else
            punt = n;

          puntoAcq puntoAcq = new puntoAcq();
          puntoAcq.id = (int)dt.Rows[punt][0];
          puntoAcq.timeSt = (DateTime)dt.Rows[punt][1];
          puntoAcq.pressIn = (double)dt.Rows[punt][2];
          puntoAcq.pressOut = (double)dt.Rows[punt][3];
          puntoAcq.tempFluidoIn = (double)dt.Rows[punt][4];
          puntoAcq.tempFluidoOut = (double)dt.Rows[punt][5];
          puntoAcq.tempCella = (double)dt.Rows[punt][6];
          //puntoAcq.humidCella = (double)dt.Rows[punt][7];
          puntoAcq.portFluido = (double)dt.Rows[punt][8];
          puntoAcq.note = "";
          myPoints.Add(puntoAcq);
        }
      }
      numberOfResults = dt.Rows.Count;
      gData.scriviExcelNew(myPoints);

    }



    private void recuperaDatiHfreq()
    {
      query = "SELECT dhId, dhTimeSt, dhPrInBuf, dhPrOutBuf, dhPrInBuf_2, dhPrOutBuf_2 " +
            "FROM dataLogHs where dhTimeSt > '" + dtInitReport.Value.Date.ToString("yyyy-MM-dd hh:mm:ss") + "' AND dhTimeSt < '" + dtEndReport.Value.Date.ToString("yyyy-MM-dd hh:mm:ss") + "'";


      MySqlDataReader result = dbAcc.readTest(DEF.strConnDb, query);

      int index1 = 0;
      int index2 = 0;
      StringBuilder curvaIn = new StringBuilder("");
      StringBuilder curvaOut = new StringBuilder("");
      List<puntoAcq> myPoints = new List<puntoAcq>();

      while (result.Read())
      {
        index1++;
        double[] appIn = new double[10];
        double[] appOut = new double[10];

        string record2 = result[2].ToString();
        string record3 = result[3].ToString();
        string record4 = result[4].ToString();
        string record5 = result[5].ToString();

        //if (index1 % 2 != 0)
        if (record2 != "")
        {
          curvaIn = curvaIn.Append(result[2].ToString());
          curvaOut = curvaOut.Append(result[3].ToString());
        }
        else
        {
          curvaIn = curvaIn.Append(result[4].ToString());
          curvaOut = curvaOut.Append(result[5].ToString());

          List<double> tmpIn;
          List<double> tmpOut;

          tmpIn = getDataFromtring(curvaIn.ToString());
          tmpOut = getDataFromtring(curvaOut.ToString());

          puntoAcq tmpPoint = new puntoAcq();

          tmpPoint.timeSt = (DateTime)result[1];
          tmpPoint.myDoubleListIn = tmpIn;
          tmpPoint.myDoubleListOut = tmpOut;
          myPoints.Add(tmpPoint);
          curvaIn.Clear();
          curvaOut.Clear();

          GC.Collect();
        }
      }
      gData.scriviExcelNew(myPoints);

    }

    public List<double> getDataFromtring(string myData)
    {
      List<double> result = new List<double>();
      char[] separators = new char[] { ';', '+' };


      foreach (string sub in myData.Split(separators, StringSplitOptions.RemoveEmptyEntries))
      {
        double tmp = Convert.ToDouble(sub) / 100.0;

        result.Add(tmp);
        //Console.WriteLine($"Substring: {sub}" + "  " + tmp);
      }
      int lenghtData = myData.Length;
      myData = null;
      GC.Collect();

      return result;
    }





    private void dtInitReport_ValueChanged(object sender, EventArgs e)
    {
      dataInit = dtInitReport.Text;
    }


  }
}
