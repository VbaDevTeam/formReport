using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rem.GeneralComponents;


namespace formReport
{
  class commonClass
  {
  }

  public class sampleCurve
  {
    public double[] arrayPress;
    public double[] arrayPort;
    public double[] arrayTemp;
  }

  public class datiBanco
  {
    public int stato;

    public puntoAcq[] myVett = new puntoAcq[5000];

    public uint myIndex;
    public double myStep;
    public bool salita = true;

    public BitInt mbStatus = new BitInt();

    public double TTaiTemOil;
    public double TTaiTemOilCor;
    public double PRaiManOilMa;
    public double PRaiManOilVa;
    public double POaiManOil;
    public double POaiManOilCor;
    public double PSaiPesOil;

    public double PR_Trafilamento;
    public double PO_Trafilamento;
    public double TM_Trafilamento;
    public double VL_Trafilamento;

    public double PR_OpenValue;
    public double PO_OpenValue;

    public double PR_CloseValue;
    public double PO_CloseValue;

    public double PR_CollimValue;
    public double PO_CollimValue;
    public double TM_CollimValue;
    public double PS_CollimStart;
    public double PS_CollimEnd;
    public double PC_CollimOil;
    public double QT_CollimErog;
    public double PC_Cell1;
    public double PC_Coll2;
    public double PC_CollTot;

    //public double 


    public int ptrFaseCiclo;
    public int ptrPhEndu;

    public double TmTimestamp;

    public double cicliEndurance;
    public double sr1MisPort;
    public double sr1MisPress;
    public double sr2MisPort;
    public double sr2MisPress;

    public BitInt comandi = new BitInt();

    public double spOpenValPort;
    public double spOpenValPress;
    public double spOpenValToll;

    public double spClosValPort;
    public double spClosValPress;
    public double spClosValToll;

    public double spRampValPress;
    public double spRampValPort;
    public double spRampValK1;
    public double spRampValK2;

    public double spTrafValPress;
    public double spTrafValDuraT;
    public double spTrafValToll;

    public string CodDisegnoWork;
    public string CodSerialeWork;

    public double SPtempOIL;
    public double SPpressOIL;
    public double SPportOIL;

    //per gestione solo 225cSt Endurance
    public double SPportPrep225;

    public int SPenduTurns;
    public int SPenduMeasC;

    public double SPlimArray;
    public double SPlimPrInf1;
    public double SPlimPrSup1;
    public double SPlimPoInf1;
    public double SPlimPoSup1;

    public double SPTmFlussAria;
    public double SPPressaturaAria;
    public double SPLimTrafilamento;
    public double SPTmDurTrafilamento;


  }
}

public class puntoAcq
{
  public int id;
  public int idAcq;
  public int idCiclo;
  public DateTime timeSt;
  public double pressIn;
  public double pressOut;
  public double tempFluidoIn;
  public double tempFluidoOut;
  public double tempCella;
  public double humidCella;
  public double portFluido;
  public string note;

  public double timeStamp;
  public double press;
  public double port;
  public double temp;
  public bool typeUp;
  public List<double> myDoubleListIn = new List<double>();
  public List<double> myDoubleListOut = new List<double>();
  public puntoAcq()
  {

  }

  public puntoAcq(double press)
  {
    this.press = press;
  }

  public puntoAcq(double press, double port)
  {
    this.press = press;
    this.port = port;
  }
}