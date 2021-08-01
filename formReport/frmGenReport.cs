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
using NPOI;
using NPOI.HSSF.UserModel;

namespace formReport
{
  public partial class frmGenReport : Form
  {
    private ExcelFile exFileData;
    private int nElementi = 40;
    private double fsPress = 3.51;
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

      double percTolleranza = 0.025;
 
      double fsPressDin = fsPress;
      double fsPressSup = fsPress * (1 + percTolleranza);
      double fsPressInf = fsPress * (1 - percTolleranza);



      int dimElementi = nMinuti * nCicliMIn;
      string myData = "";
      Random random = new Random();

      for (int p = 0; p <= dimElementi; p++)
      {
        for (int n = 0; n < myOriginData.myDoubleListIn.Count; n++)
        {
          puntoAcq tmp = new puntoAcq();
          //myData = myOriginData.myDoubleListIn[n].ToString();
          tmp.idCiclo = n;
          tmp.idAcq = p;
          int myInitRandom = random.Next(1, 10000) - 5000;
          double myRandom = Convert.ToDouble(myInitRandom) / 100000.0;

          fsPressDin += myRandom;

          if (fsPressDin > fsPressSup)
          {
            fsPressDin -= percTolleranza / 10;
          }
          if (fsPressDin < fsPressInf)
          {
            fsPressDin += percTolleranza / 10;
          }

          tmp.myDoubleListIn.Add(fsPressDin * myOriginData.myDoubleListIn[n]);
          tmp.myDoubleListOut.Add(fsPressDin * myOriginData.myDoubleListOut[n]);
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



            xFile.Worksheets["data"].Cells[nRiga + 1, 1 ].Value = myList[n].idCiclo;
            xFile.Worksheets["data"].Cells[nRiga + 1, 2 ].Value = myList[n].idAcq;
            xFile.Worksheets["data"].Cells[nRiga + 1, 3 ].Value = apptDFt;
            xFile.Worksheets["data"].Cells[nRiga + 1, 4 ].Value = myList[n].pressIn;
            xFile.Worksheets["data"].Cells[nRiga + 1, 5 ].Value = myList[n].pressOut;
            xFile.Worksheets["data"].Cells[nRiga + 1, 6 ].Value = myList[n].tempFluidoIn;
            xFile.Worksheets["data"].Cells[nRiga + 1, 7 ].Value = myList[n].tempFluidoOut;
            xFile.Worksheets["data"].Cells[nRiga + 1, 8 ].Value = myList[n].tempCella;
            xFile.Worksheets["data"].Cells[nRiga + 1, 9 ].Value = millisecondToAdd;
            xFile.Worksheets["data"].Cells[nRiga + 1, 10].Value = myList[n].portFluido;
            xFile.Worksheets["data"].Cells[nRiga + 1, 11].Value = myList[n].note;
            xFile.Worksheets["data"].Cells[nRiga + 1, 12].Value = myList[n].myDoubleListIn[y];
            xFile.Worksheets["data"].Cells[nRiga + 1, 13].Value = myList[n].myDoubleListOut[y];
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
