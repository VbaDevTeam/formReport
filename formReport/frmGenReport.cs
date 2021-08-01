using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GemBox.Spreadsheet;


namespace formReport
{
  public partial class frmGenReport : Form
  {
    private ExcelFile exFileData;
    private int nElementi = 40;
    private double fsPress = 3.58;
    //private int nMinuti = 874;
    private int nMinuti = 3;
    private int nCicliMIn = 30;
    private string pathExFile = @"C:\REM\Settings\0387\Report\Test.xlsx";
    private puntoAcq myOriginData = new puntoAcq();
    private List<puntoAcq> myListDef = new List<puntoAcq>();
    public frmGenReport()
    {
      SpreadsheetInfo.SetLicense("ESWL-ZQLT-26PA-QGPO");
      exFileData = new ExcelFile();
      InitializeComponent();

    }

    private void loadData()
    {
      int myRowEx = 0;
      int myColumnExCan1 = 1;
      int myColumnExCan2 = 2;
      exFileData.LoadXlsx(pathExFile, XlsxOptions.PreserveKeepOpen);
      string myData = "";

      for (int n = 0; n < nElementi; n++)
      {
        myData = exFileData.Worksheets["originData"].Cells[n, myColumnExCan1].Value.ToString();
        myOriginData.myDoubleListIn.Add(Convert.ToDouble(exFileData.Worksheets["originData"].Cells[n, myColumnExCan1].Value));
        myOriginData.myDoubleListOut.Add(Convert.ToDouble(exFileData.Worksheets["originData"].Cells[n, myColumnExCan2].Value));
        insertRow(ref dgvLista, myData);
      }
      exFileData.ClosePreservedXlsx();
    }

    private void btnClick(object sender, EventArgs e)
    {
      Button tmp = (Button)sender;
      switch(tmp.Name)
      {
        case "btnSelFile":
          FolderBrowserDialog fDialEx = new FolderBrowserDialog();
          DialogResult result = fDialEx.ShowDialog();
          if (result == DialogResult.OK)
          {
            pathExFile = fDialEx.SelectedPath;
            tbPath.Text = pathExFile;
          }
          break;
        case "btnCarica":
          loadData();
          break;
        case "btnCreate":
          createData();
          break;
      }
    }

    private void insertRow(ref DataGridView dgv, string textSub)
    {
      dgv.Rows.Add(textSub, true, false, "start");
    }

    private void createData()
    {
      myListDef.Clear();

      double percTolleranza = 0.0125;
 
      double fsPressDin = fsPress;
      double fsPressSup = fsPress * (1 + percTolleranza);
      double fsPressInf = fsPress * (1 - percTolleranza);


      nMinuti = 860;
      //nMinuti = 1;
      int dimElementi = nMinuti * nCicliMIn;
      string myData = "";
      Random random = new Random();
      double prMonte = 0;
      double prValle = 0;
      double myNoiseRandom;
      double myNoiseDivider = 40;
      for (int p = 0; p <= dimElementi; p++)
      {
        for (int n = 0; n < myOriginData.myDoubleListIn.Count; n++)
        {
          puntoAcq tmp = new puntoAcq();
          //myData = myOriginData.myDoubleListIn[n].ToString();
          tmp.idCiclo = n;
          tmp.idAcq = p;
          int myInitRandom = random.Next(1, 10000) - 5000;
          double myRandom = Convert.ToDouble(myInitRandom) / 1.0e7;

          fsPressDin += myRandom;

          if (fsPressDin > fsPressSup)
          {
            fsPressDin -= percTolleranza / 5;
          }
          if (fsPressDin < fsPressInf)
          {
            fsPressDin += percTolleranza / 5;
          }
          myNoiseRandom = 1.0 + (random.NextDouble() - 0.5) / myNoiseDivider;
          prMonte = fsPressDin * myOriginData.myDoubleListIn[n] * myNoiseRandom;
          myNoiseRandom = 1.0 + (random.NextDouble() - 0.5) / myNoiseDivider;
          prValle = fsPressDin * myOriginData.myDoubleListOut[n] * myNoiseRandom;
          tmp.myDoubleListIn.Add(prMonte);
          tmp.myDoubleListOut.Add(prValle);
          myListDef.Add(tmp);

          //insertRow(ref dgvListaCreate, myData);
        }
      }
      scriviExcelNew(myListDef);
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
        ExcelFile xFile = new ExcelFile();
        xFile.LoadXlsx(@"C:\REM\Settings\0387\Modelli\modello_report.xlsx", XlsxOptions.PreserveMakeCopy);

        int nRiga = 0;
        int nColonna = 0;
        double millisecondToAdd = 0.0;


        for (int n = 0; n < myList.Count; n++)
        {
          //millisecondToAdd = 0.0;
          for (int y = 0; y < myList[n].myDoubleListIn.Count; y++)
          {


            DateTime myDt = myList[n].timeSt;
            DateTime apptDFt = myDt.AddMilliseconds(millisecondToAdd);


            nColonna = 1;
            xFile.Worksheets["data"].Cells[nRiga + 1, nColonna++ ].Value = myList[n].idCiclo;
            xFile.Worksheets["data"].Cells[nRiga + 1, nColonna++ ].Value = myList[n].idAcq;
            xFile.Worksheets["data"].Cells[nRiga + 1, nColonna++ ].Value = millisecondToAdd;
            xFile.Worksheets["data"].Cells[nRiga + 1, nColonna++].Value = myList[n].myDoubleListIn[y];
            xFile.Worksheets["data"].Cells[nRiga + 1, nColonna++].Value = myList[n].myDoubleListOut[y];
            millisecondToAdd = millisecondToAdd + 50.0;
            nRiga++;
          }


        }


        string originalFileName = @"C:\REM\Settings\0387\Report\Report"
    + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + "."
    + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + ".xlsx";
        xFile.SaveXlsx(originalFileName);

      }
      catch (Exception ex)
      {

        result = -1;

      }





      return result;

    }




  }
}
