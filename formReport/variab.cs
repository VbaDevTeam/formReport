using System;
using System.Windows.Forms;
using System.Xml.Serialization;
using Rem.GeneralComponents;
using MySql.Data.MySqlClient;
using System.Data;
using GemBox.Spreadsheet;

using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.IO;


namespace formReport
{

  delegate void SetCallback(string msg);
  delegate void SetCallbackInt(int fase, string msg);
  delegate void SetCallbackIntEnd(int fase);
  public class argSyncStep
  {
    public int dummy;
  }

  public class V
  {
    public static DEF Def = new DEF();
    public System.Globalization.CultureInfo ci;
    public static System.Globalization.NumberFormatInfo ni = null;

    public V()
    {
      if (!initDone)
      {
        ci = System.Globalization.CultureInfo.InstalledUICulture;
        ni = (System.Globalization.NumberFormatInfo)ci.NumberFormat.Clone();
        ni.NumberDecimalSeparator = ".";

        initDone = true;
        this.init();
        //this.idChAn();
        this.initDbAppl();
      }
      //...
    }



    /* Situazione
     * 
     * 
     */

    #region VARIABILI

    //public static int plc_Cane = 0;
    //public static int plc_Gatto = 0;


    public static int temp_fatt_10 = 10;


    //esempio
    public static string config_XXX = "";
    public static string[] config_XXX_1 =
      {
            /*00*/"",
            /*01*/"Prova",
        };


    //elenchi
    public static int config_YYY = 0;
    public enum config_YYY_1
    {
      /*00*/
      nada,
      /*01*/
      Prova,
    };

    #endregion

    //definizione percorso generale file configurazione prove
    public const string strTestCfgFile_BasePath = @"C:\Rem\ABCD - Settings\Configurazione\";

    //definizione percorso generale modelli report prove
    public const string strReport_ModelBasePath = @"c:\REM\ABCD - Settings\Configurazione\";
    public const string strReport_ModelName = @"ModelloReport.xls";
    public const string strReport_BasePath = @"C:\REM\Settings\0108\Reports\";

    public const string strReport_BasePath_Temp = @"c:\Rem\ABCD - settings\Reports\Temp\";
    public const string strReport_BasePathMaster = @"c:\Rem\ABCD - settings\Reports\Master\";

    //costanti gestione "ini"
    public enum saveType { Application = 1, User, Recipe, TestConf, Test, TestConfWhere, TestInvecchiamento };
    public enum stato
    {
      locked,
      manual,

      completed
    };
    public static string[] statoText =
    {
      "Non pronto",
      "Manuale, attesa comando",

      "prova completata"
    };

    public static event Loaded loaded;


    //per "environment"
    // Variabili serializzate, riguardanti la configurazione d'ambiente
    //   - parametri connessione db
    //   - canali analogici definiti esplicitamente
    //   - 

    private static NO_XML noXml = new NO_XML();
    public NO_XML NoXml
    {
      get { return V.noXml; }
      set { V.noXml = value; }
    }

    private static DATA_APPL _dataAppl = new DATA_APPL();
    public DATA_APPL DataAppl
    {
      get { return _dataAppl; }
      set { _dataAppl = value; }
    }

    private static DATA_CONF_TEST data_conf_test = new DATA_CONF_TEST();
    public DATA_CONF_TEST dataConfTest
    {
      get { return data_conf_test; }
      set { data_conf_test = value; }
    }

    private static DATA_CONF_TEST_INVE data_conf_test_inve = new DATA_CONF_TEST_INVE();
    public DATA_CONF_TEST_INVE dataConfTest_inve
    {
      get { return data_conf_test_inve; }
      set { data_conf_test_inve = value; }
    }

    private static DATA_TEST data_test = new DATA_TEST();
    public DATA_TEST dataTest
    {
      get { return data_test; }
      set { data_test = value; }
    }

    private static IO _Io = new IO();

    public IO Io
    {
      get { return V._Io; }
      set { V._Io = value; }
    }

    private static ENV _Env = new ENV();
    public ENV Env
    {
      get { return _Env; }
      set { _Env = value; }
    }

    public static bool userChanging;
    public static string userNome;
    public static string userCognome;
    public static string user;
    public static int userCode;
    public static bool pAdminLogged;

    public static string mezzo;

    public static string prova;

    public static bool initDone = false;

    public static BitInt Diritti = new BitInt();


    public void init()
    {


    }

    public void initDbAppl()
    {
      bool salva = false;

      // l'operazione di deserializzazione sostituisce gli oggetti inizializzati.
      // dovendo aggiungere oggetti durante lo sviluppo, vanno aggiunti dopo il loadData, va fatto un giro con salvataggio e
      // quindi spostato il loadData alla fine del costruttore, in modo che resti la traccia ma non sovrascriva.
      // 20100614 - le analogiche e i pid godono della proprietà di autorigenerazione. La definizione di un nuovo oggetto
      // o il riordino ne causa la rigenerazione. ATTENZIONE: si perdono tutte le impostazioni
      this.DataAppl.aio = new analIo();
      this.DataAppl.aio.Ai = new List<AnCh>();
      this.DataAppl.aio.Ao = new List<AnChOut>();

      this.loadData((int)V.saveType.Application, 0);

      #region controllo AI

      AnCh tempAi;


      if (DataAppl.aio.Ai.Count < DEF.aiChNames.Length)
      {
        salva = true;
        DataAppl.aio.Ai.Clear();
        for (int n = 0; n < DEF.aiChNames.Length; n++)
        {
          tempAi = new AnCh();
          tempAi.Name = DEF.aiChNames[n];
          tempAi.LabelLong = DEF.aiLabelShort[n];
          tempAi.LabelShort = DEF.aiLabelLong[n];
          tempAi.ChNo = n;
          DataAppl.aio.Ai.Add(tempAi);
        }
      }

      //se il canale ha nome diverso, lo riassegno
      for (int p = 0; p < DEF.aiChNames.Length; p++)
      {
        if (DataAppl.aio.Ai[p].Name != DEF.aiChNames[p])
        {
          salva = true;
          DataAppl.aio.Ai.Clear();
          for (int n = 0; n < DEF.aiChNames.Length; n++)
          {
            tempAi = new AnCh();
            tempAi.Name = DEF.aiChNames[n];
            tempAi.LabelLong = DEF.aiLabelShort[n];
            tempAi.LabelShort = DEF.aiLabelLong[n];
            tempAi.ChNo = n;
            DataAppl.aio.Ai.Add(tempAi);
          }
          break;
        }
      }

      #endregion

      if (salva) saveData((int)V.saveType.Application, 0);
    }

    public void idChAn()
    {
      //// l'operazione di deserializzazione sostituisce gli oggetti inizializzati.
      //// dovendo aggiungere oggetti durante lo sviluppo, vanno aggiunti dopo il loadData, va fatto un giro con salvataggio e
      //// quindi spostato il loadData alla fine del costruttore, in modo che resti la traccia ma non sovrascriva.
      //  this.DataAppl.aio = new analIo();
      //  this.DataAppl.aio.Ai = new List<AnCh>();
      //  this.DataAppl.aio.Ao = new List<AnChOut>();


      //  AnCh tmpI = new AnCh();

      //  tmpI.Name = "cani_00";
      //  tmpI.LabelLong = "AAA| Trasduttore ...";
      //  tmpI.LabelShort = "AAA| Trasduttore ...";
      //  this.DataAppl.aio.Ai.Add(tmpI);


      //  //Ao

      //  AnChOut tmpO = new AnChOut();
      //  tmpO = new AnChOut();
      //  tmpO.Name = "cani_00";
      //  tmpO.LabelLong = "XXX| ?";
      //  tmpO.LabelShort = "XXX| ?";
      //  this.DataAppl.aio.Ao.Add(tmpO);


      //  this.loadData((int)V.saveType.Application, 0);
    }

    public int saveData(int what, int set, string where = "")
    {
      int result = what;
      //preparazione e scrittura su database
      MySqlConnection connessioneDB = new MySqlConnection(DEF.strConnDb);

      string comandoSQL = "";
      Type[] tipi = new Type[1];
      tipi[0] = typeof(analIo);

      //salvataggio dati applicazione
      //=============================
      if (what == (int)V.saveType.Application)
      {
        StringWriter outApp = new StringWriter(new StringBuilder());
        XmlSerializer serApp = new XmlSerializer(typeof(DATA_APPL), tipi);
        serApp.Serialize(outApp, this.DataAppl);
        string appData = outApp.ToString();
        appData = appData.Replace("\"", "\"\"");
        appData = appData.Replace("'", "''");

        comandoSQL = "INSERT INTO confapp " +
          "(caId, caDate, caSetNo, caType, caConfigData, caAuth) VALUES " +
          "(null, NOW(), " + set + ", \"Application\", \"" + appData + "\", \"" + V.user + "\")";
      }

      if (what == (int)V.saveType.User)
      {
        //comandoSQL = "INSERT INTO confapp " +
        //  "(caId, caDate, caSetNo, caType, caConfigData, caAuth) VALUES " +
        //  "(null, NOW(), " + set + ", \"User\", \"" + gbxData + "\", \"" + V.user + "\")";
      }

      //salvataggio dati applicazione
      //=============================
      if (what == (int)V.saveType.Recipe)
      {
      }

      //salvataggio dati configurazione prova
      //=============================
      if (what == (int)V.saveType.TestConf)
      {
        StringWriter strWriter = new StringWriter(new StringBuilder());
        XmlSerializer xSerizer = new XmlSerializer(typeof(DATA_CONF_TEST));
        xSerizer.Serialize(strWriter, this.dataConfTest);
        string strData = strWriter.ToString();
        strData = strData.Replace("\"", "\"\"");
        strData = strData.Replace("'", "''");

        comandoSQL = "INSERT INTO confapp " +
          "(caId, caDate, caSetNo, " +
          "caType, " + //caEngine, caGbx, " +
          "caConfigData, caAuth) VALUES " +
          "(null, NOW(), " + set +
          ", \"TestConf\",  \"" + strData + "\", \"" + V.user + "\")";
      }

      //salvataggio dati configurazione prova
      //=============================
      if (what == (int)V.saveType.TestConfWhere)
      {
        StringWriter strWriter = new StringWriter(new StringBuilder());
        XmlSerializer xSerizer = new XmlSerializer(typeof(DATA_CONF_TEST));
        xSerizer.Serialize(strWriter, this.dataConfTest);
        string strData = strWriter.ToString();
        strData = strData.Replace("\"", "\"\"");
        strData = strData.Replace("'", "''");

        comandoSQL = "INSERT INTO confapp " +
            "(caId, caDate, caSetNo, " +
            "caType, caGbx, " + //caEngine, caGbx, " +
            "caConfigData, caAuth) VALUES " +
            "(null, NOW(), " + set +
            ", \"TestConfWhere\",  \"" + where + "\",  \"" + strData + "\", \"" + V.user + "\")";
      }


      //salvataggio dati configurazione prova
      //=============================
      if (what == (int)V.saveType.TestInvecchiamento)
      {
        StringWriter strWriter = new StringWriter(new StringBuilder());
        XmlSerializer xSerizer = new XmlSerializer(typeof(DATA_CONF_TEST_INVE));
        xSerizer.Serialize(strWriter, this.dataConfTest_inve);
        string strData = strWriter.ToString();
        strData = strData.Replace("\"", "\"\"");
        strData = strData.Replace("'", "''");

        comandoSQL = "INSERT INTO confapp " +
            "(caId, caDate, caSetNo, " +
            "caType, caGbx, " + //caEngine, caGbx, " +
            "caConfigData, caAuth) VALUES " +
            "(null, NOW(), " + set +
            ", \"TestConfWhere\",  \"" + where + "\",  \"" + strData + "\", \"" + V.user + "\")";
      }


      //salvataggio dati prova
      //=============================
      if (what == (int)V.saveType.Test)
      {
        StringWriter strWriter = new StringWriter(new StringBuilder());
        XmlSerializer xSerizer = new XmlSerializer(typeof(DATA_TEST));
        xSerizer.Serialize(strWriter, this.dataTest);
        string strData = strWriter.ToString();
        strData = strData.Replace("\"", "\"\"");
        strData = strData.Replace("'", "''");

        comandoSQL = "INSERT INTO confapp " +
          "(caId, caDate, caSetNo, " +
          "caType, " + //caEngine, caGbx, " +
          "caConfigData, caAuth) VALUES " +
          "(null, NOW(), " + set +
          ", \"Test\",  \"" + strData + "\", \"" + V.user + "\")";
      }

      connessioneDB.Open();

      MySqlCommand comandoDB = new MySqlCommand();

      comandoDB.CommandText = comandoSQL;
      comandoDB.Connection = connessioneDB;
      comandoDB.ExecuteNonQuery();
      //if (comandoDB.ExecuteNonQuery() != 0)
      //  result = -1;

      connessioneDB.Close();
      connessioneDB = null;

      return result;
    }

    public int loadData(int what, int set, string where = "")
    {
      int result = 0;
      V v = new V();
      string settings = "";
      string comandoSQL = "";

      MySqlConnection connessioneDB = new MySqlConnection(DEF.strConnDb);


      if (what == (int)V.saveType.Application)
      {
        comandoSQL = "SELECT caId, caDate, caSetNo, caType, caEngine, " +
          "caGbx, caConfigData, caAuth FROM confapp " +
          "WHERE caType LIKE \"Application\" and caSetNo = " + set + " ORDER BY caId DESC;";
      }

      if (what == (int)V.saveType.User)
      {
        comandoSQL = "SELECT caId, caDate, caSetNo, caType, caEngine, " +
          "caGbx, caConfigData, caAuth FROM confapp " +
          "WHERE caType LIKE \"User\" and caSetNo = " + set + " ORDER BY caId DESC;";
      }

      //if (what == (int)V.saveType.Recipe)
      //{
      //  comandoSQL = "SELECT caId, caDate, caSetNo, caType, caEngine, " +
      //    "caGbx, caConfigData, caAuth FROM confapp " +
      //    "WHERE caType LIKE \"Recipe\" and caSetNo = " + set +
      //    " and caEngine like \"" + V.gbxMotore +
      //    "\" and caGbx like \"" + V.gbxTipo + "\" ORDER BY caId DESC;";
      //}

      if (what == (int)V.saveType.TestConf)
      {
        comandoSQL = "SELECT " +
          "caId, caDate, caSetNo, " +
          "caType, " + //caEngine, caGbx, " +
          "caConfigData, caAuth " +
          "FROM confapp " +
          "WHERE caType LIKE \"TestConf\" and caSetNo = " + set +
          " ORDER BY caId DESC;";
      }

      if (what == (int)V.saveType.TestConfWhere)
      {
        comandoSQL = "SELECT " +
        "caId, caDate, caSetNo, " +
        "caType, " + //caEngine, caGbx, " +
        "caConfigData, caAuth " +
        "FROM confapp " +
        "WHERE caType LIKE \"TestConfWhere\" and caSetNo = " + set + " and caGbx = '" + where +
        "' ORDER BY caId DESC;";
      }

      if (what == (int)V.saveType.TestInvecchiamento)
      {
        comandoSQL = "SELECT " +
        "caId, caDate, caSetNo, " +
        "caType, " + //caEngine, caGbx, " +
        "caConfigData, caAuth " +
        "FROM confapp " +
        "WHERE caType LIKE \"TestInvecchiamento\" and caSetNo = " + set + " and caGbx = '" + where +
        "' ORDER BY caId DESC;";
      }


      if (what == (int)V.saveType.Test)
      {
        comandoSQL = "SELECT " +
          "caId, caDate, caSetNo, " +
          "caType, " + //caEngine, caGbx, " +
          "caConfigData, caAuth " +
          "FROM confapp " +
          "WHERE caType LIKE \"Test\" and caSetNo = " + set +
          " ORDER BY caId DESC;";
      }



      connessioneDB.Open();

      MySqlCommand comandoDB = new MySqlCommand();

      comandoDB.CommandText = comandoSQL;
      comandoDB.Connection = connessioneDB;

      MySqlDataReader readerDB = comandoDB.ExecuteReader();

      if (readerDB.Read())
      {
        result = 4;
        //if (what == (int)V.saveType.Recipe)
        //{
        //  V.gbxMotore = (string)readerDB["caEngine"];
        //  V.gbxTipo = (string)readerDB["caGbx"];
        //}
        settings = (string)readerDB["caConfigData"];
      }
      readerDB.Close();
      readerDB = null;
      connessioneDB.Close();
      connessioneDB = null;

      TextReader reader = new StringReader(settings);
      try
      {
        if (what == (int)V.saveType.Application)
        {
          Type[] tipi = new Type[1];
          tipi[0] = typeof(analIo);
          XmlSerializer ser = new XmlSerializer(typeof(DATA_APPL), tipi);
          v.DataAppl = (DATA_APPL)ser.Deserialize(reader);
          if (loaded != null)
            loaded((int)V.saveType.Application);
        }

        if (what == (int)V.saveType.User)
        {
          //XmlSerializer ser = new XmlSerializer(typeof(GbxData));
          //v.GbD = (GbxData)ser.Deserialize(reader);
          if (loaded != null)
            loaded((int)V.saveType.User);
        }

        if (what == (int)V.saveType.Recipe)
        {
          //XmlSerializer ser = new XmlSerializer(typeof(GbxData));
          //v.GbD = (GbxData)ser.Deserialize(reader);
          //if (loaded != null)
          //  loaded((int)V.saveType.Recipe);
        }

        if (what == (int)V.saveType.Test)
        {
          XmlSerializer ser = new XmlSerializer(typeof(DATA_TEST));
          v.dataTest = (DATA_TEST)ser.Deserialize(reader);
          if (loaded != null)
            loaded((int)V.saveType.Test);
        }

        if (what == (int)V.saveType.TestConf || what == (int)V.saveType.TestConfWhere)
        {
          XmlSerializer ser = new XmlSerializer(typeof(DATA_CONF_TEST));
          v.dataConfTest = (DATA_CONF_TEST)ser.Deserialize(reader);
          if (loaded != null)
            loaded((int)V.saveType.TestConf);
        }

        if (what == (int)V.saveType.TestInvecchiamento)
        {
          XmlSerializer ser = new XmlSerializer(typeof(DATA_CONF_TEST_INVE));
          v.dataConfTest_inve = (DATA_CONF_TEST_INVE)ser.Deserialize(reader);
          if (loaded != null)
            loaded((int)V.saveType.TestInvecchiamento);
        }

        v = null;
        reader.Close();
        reader = null;
      }
      catch (Exception ex)
      {
        string mess = ex.ToString();
        if (loaded != null)
          loaded((int)V.saveType.Recipe);
        return -1;
      }
      return result;
    }
  }

  public class IO
  {
    //public GestioneComunicazione gesCom;
    //public BitInt[] alSistema = new BitInt[10];
    public datiBanco DTB10 = new datiBanco();
  }

  public class NO_XML
  {
    public bool closing = false;
    public bool running = false;

    public ushort statusIO;
    public ushort statusIO_100;

    public int mbErrWr;
    public int mbErrWrCtr;
    public int mbErrRd;
    public int mbErrRd_110;
    public int mbErrRdCtr;
    public int mbErrRdCtr_110;

    public bool pDiagnAnalOpen;
    public bool testH;
    public bool testC;
    public int count_pass = 0;
    public int id_provaH = 0;
    public int id_provaC = 0;
    public string name_provaH = "";
    public string name_provaC = "";

    public string IdReport;

    public string ipAddrHot;
    public string ipAddrCold;

    public BitInt avgOnH;
    public int ptrBdSendingH;
    public int ptrBdSaveH;
    public int timeStampH;
    public BitInt statusH;
    public int cottaXPc;

    public BitInt mbCommandsH;
    public BitInt mbCommands1H;
    public int mbSearchDjdH;
    public int mbPcLastReadH;
    public int mbPtrBdReceivdH;
    public int mbCottaYPlcH;
    public string descrizTest;
    public bool inviato;

    public cntTastiFunzione cTastiFunz;
    //public cntDiagnAnal cDiagnAnal;

    public cntAnAttrType cAttrAnag;
    public ExcelFile xFile;



    public puntoAcq pntAcq = new puntoAcq();


    //blocco variabili applicativo B&R grafici
    //=========================================

    //varie
    public int numeroTracce;
    public int nCampioniXTraccia;
    public double[][] datiTracce;
    public DataSet ds1;
    public DataTable dtCampioni;
    public DataTable dtHeaderTracce;
    public string originalFileName = "";
    public int colPassi;


    //identificativi articolo e prova
    public double valueApertura;
    public double valueChiusura;
    public double valuePerc1;
    public double valuePerc2;
    public double valuePercTot;
    public double valueTrafilamento;


    public double minAper;
    public double maxAper;
    public double minChiu;
    public double maxChiu;
    public double minPerc;
    public double maxPerc;

    public double maxTraf;

    public string nDisegno = "";
    public string serialePz = "";
    public int codDis;
    public bool salescende = true;

    public bool testAperturaEnd;
    public bool testChiusuraEnd;
    public bool testRampaEnd;
    public bool testTrafilamentoEnd;


    public bool setLimit = false;
    public bool setCycle = false;
    public bool setAzzeraCycle = false;
    public bool setMaserati = false;

    public int qtCicliTotSp = 0;
    public int qtCicliInternalSp = 0;
    public int qtCicliTotPv = 0;
    public int qtCicliInternalPv = 0;

    public int srPort1Pv;
    public int srPress1Pv;
    public int srPort2Pv;
    public int srPress2Pv;

    public int srsPortSp;
    public int srsPressSp;


    //variabili di consumo
    //public int rowPtrUp = 1;
    //public int rowPtrDown = 1;
  }

  public class DATA_APPL
  {
    public ParametriComuni comPar;
    public analIo aio;
    public analIo aiH1;
    public analIo aiH2;
    public analIo aiC1;
    public analIo aiC2;

    public int userNdx;

    public int ptrRead;
    public int ptrAppRead;
    public int ptrWrite;
    public int appPtr;
    public int numGiorni;

    public int numeroCicliInve;
    public double spPressIve;
    public int duraErog;
    public int duraStop;

    public int numCicliFatti;

  }

  public class DATA_TEST
  {
    public BitInt[] We = new BitInt[3];
    public BitInt[] We_DaForzare_Num = new BitInt[3];
    public BitInt[] We_DaForzare_Stato = new BitInt[3];

    public BitInt[] Wu = new BitInt[1];
    public BitInt[] Wu_Image = new BitInt[1];

    public BitInt[] Wu_PassDati = new BitInt[1];
  }

  public class DATA_CONF_TEST_INVE
  {

    public int spPressInve;
    public int spDurErog;
    public int spDupPausa;

    public int spInterStop;

    public double SPtempOIL = 50.0;
    public double SPpressOIL = 2.50;
    public double SPportOIL = 1.50;


  }

  public class DATA_CONF_TEST
  {
    public string nDisegno = "";
    //Impostazioni test apertura
    public double spOpenValuePort = 0.30;
    public double spOpenValuePress = 1.60;
    public double spOpenValueToll = 0.10;
    public bool testOpenAbil = true;

    //Impostazioni test chiusura
    public double spCloseValuePort = 0.29;
    public double spCloseValuePress = 1.60;
    public double spCloseValueToll = 0.10;
    public bool testCloseAbil = true;

    //Impostazioni test rampa
    public double spRampaValuePress = 6.00;
    public double spRampaValuePort = 15.0;
    public double spRampaValueK1 = 5;
    public double spRampaValueK2 = 10;

    public double spRampa_1_Iniz = 0.00;
    public double spRampa_1_Maxi = 25.00;
    public double spRampa_1_Fine = 25.00;
    public double spRampa_1_Perc = 12.00;

    public double spRampa_2_Iniz = 0.00;
    public double spRampa_2_Maxi = 25.00;
    public double spRampa_2_Fine = 25.00;
    public double spRampa_2_Perc = 12.00;
    public bool testrampaAbil = true;

    public int CodDisegnoWork = 1;
    //Impostazioni test Trafilamento
    public double spTrafValuePress = 3.0;
    public double spTrafValueDuraT = 30.0;
    public double spTrafValueToll = 70;
    public bool testTrafAbil = true;

    public double SPtempOIL = 50.0;
    public double SPpressOIL = 2.50;
    public double SPportOIL = 1.50;

    //Impostazioni macchina
    public double spTemperatura = 50.0;

    public bool fronteDiscesa = true;


    //Impostazioni MASERATI
    public double SPTmFlussAria;
    public double SPPressaturaAria;
    public double SPLimTrafilamento;
    public double SPTmDurTrafilamento;

  }

  public class DATA_USR
  {


  }

  public class ENV
  {
    public AnCh[] analIn = new AnCh[8];
    //public double assorbimento_1,             //836
    //    assorbimento_2,
    //    assorbimento_3,
    //    pid;
    public double pid;
  }

  public class ParametriComuni
  {
    private static bool _AttivaPolling;
    public bool AttivaPolling
    {
      get { return ParametriComuni._AttivaPolling; }
      set { ParametriComuni._AttivaPolling = value; }
    }

    private static string _IPAddress_134;
    public string IPAddress_134
    {
      get { return ParametriComuni._IPAddress_134; }
      set { ParametriComuni._IPAddress_134 = value; }
    }

    private static string _commPort;
    public string CommPort
    {
      get { return ParametriComuni._commPort; }
      set { ParametriComuni._commPort = value; }
    }

    private static int _CycleRate;
    public int CycleRate
    {
      get { return ParametriComuni._CycleRate; }
      set { ParametriComuni._CycleRate = value; }
    }

    private static int _PortNumber;
    public int PortNumber
    {
      get { return ParametriComuni._PortNumber; }
      set { ParametriComuni._PortNumber = value; }
    }

    private static int _nRegRead;
    public int NRegRead
    {
      get { return _nRegRead; }
      set { _nRegRead = value; }
    }
  }



}
