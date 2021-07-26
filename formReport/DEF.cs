using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Rem.GeneralComponents;

namespace formReport
{
  public class DEF
  {
    //non modificare la stringa di connessione
    //  intervenire piuttosto su
    //  c:\windows\system32\drivers\etc\hosts
    //  aggiungendo la riga 
    //  mysqlsrv  localhost
    //  oppure 
    //  mysqlsrv  10.0.0.3 (questa non è necessaria essendo definita sul dns)
    //public const string strConnDb = @"Database=rem1130_ellena;Data Source=mysqlsrv;User Id=root;Password=mysqlpwd";
    public const string strConnDb = @"Database=v0387_hosestress;Data Source=mysqlsrv;User Id=root;Password=mysqlpwd";


    #region fieldsNames

    public static string[] dataFieldNames =
      {
          "ltId"
        , "ltIdCotta"
        , "ltIdTavolo"
        , "ltTimeStampPc"
        , "ltTimeStampDjd"
        , "ltTc1"
        , "ltTc2"
        , "ltTc3"
        , "ltTc4"
        , "ltTc5"
        , "ltTc6"
        , "ltTc7"
        , "ltTc8"
        , "ltMedia"
        , "ltExMedia2"
        , "ltVuoto"
        , "ltTTavolo"
        , "ltIr"
        , "ltIs"
        , "ltIt"
        , "ltPidTavolo"
        , "ltPidPannelli"
      };

    #endregion

    #region GenericaWe
    public static string[,] we =
      {
  {
 "E00.0"
,"E00.1"
,"E00.2"
,"E00.3"
,"E00.4"
,"E00.5"
,"E00.6"
,"E00.7"

,"E00.8"
,"E00.9"
,"E00.A"
,"E00.B"
,"E00.C"
,"E00.D"
,"E00.E"
,"E00.F"
  },
  {
 "E01.0"
,"E01.1"
,"E01.2"
,"E01.3"
,"E01.4"
,"E01.5"
,"E01.6"
,"E01.7"

,"E01.8"
,"E01.9"
,"E01.A"
,"E01.B"
,"E01.C"
,"E01.D"
,"E01.E"
,"E01.F"
  }
      };
    #endregion

    #region we long
    public static string[,] weLong =
      {
  {
"E00.0	JB6.B",
"E00.1	JB6.F",
"E00.2	JB6.J",
"E00.3	JB6.M",
"E00.4	JB6.T",
"E00.5	JB7.A",
"E00.6	JB7.C",
"E00.7	JB7.D",
"E00.8	JB7.F",
"E00.9	JB7.G",
"E00.a	JB7.H",
"E00.b	JB7.L",
"E00.c	JB7.O",
"E00.d	JB7.R",
"E00.e	JB7.T",
"E00.f	JB7.Z"
},
{
"E01.0	JB7.c",
"E01.1	JB7.d",
"E01.2	JB7.f",
"E01.3	JB7.h",
"E01.4	JB7.k",
"E01.5	JB7.m",
"E01.6	JB7.n",
"E01.7	JB7.n",
"E01.8	JB7.p",
"E01.9	JB7.r",
"E01.a	JB7.s",
"E01.b	JB7.u",
"E01.c	JB7.x",
"E01.d	JB7.y",
"E01.e	JB7.z",
"E01.f	JB7.AA",
}
      };

    #endregion

    #region GenericaWu

    public static string[,] wu =
      {
  {
"U00.0",
"U00.1",
"U00.2",
"U00.3",
"U00.4",
"U00.5",
"U00.6",
"U00.7",
"U00.8",
"U00.9",
"U00.a",
"U00.b",
"U00.c",
"U00.d",
"U00.e",
"U00.f"
},
{
"U01.0",
"U01.1",
"U01.2",
"U01.3",
"U01.4",
"U01.5",
"U01.6",
"U01.7",
"U01.8",
"U01.9",
"U01.a",
"U01.b",
"U01.c",
"U01.d",
"U01.e",
"U01.f"
}
      };

    #endregion

    #region wu long

    public static string[,] wuLong =
      {
  {
    "U0.0	JB6.H",
"U00.1	JB6.L",
"U00.2	JB6.S",
"U00.3	JB7.N",
"U00.4	JB7.U",
"U00.5	JB7.V",
"U00.6	JB7.W",
"U00.7	JB7.X",
"U00.8	JB7.Y",
"U00.9	JB7.e",
"U00.a	JB7.v",
"U00.b	JB7.w",
"U00.c	JB8.H",
"U00.d	JB8.M",
"U00.e	JB8.P",
"U00.f	JB8.b"
},
{
"U01.0	JB8.c",
"U01.1",
"U01.2",
"U01.3",
"U01.4",
"U01.5",
"U01.6",
"U01.7",
"U01.8",
"U01.9",
"U01.a",
"U01.b",
"U01.c",
"U01.d",
"U01.e",
"U01.f"
}
      };

    #endregion

    #region fasi ciclo header cotta

    public enum operazione
    {
      _na
      , carico_caldo
      , invio_caldo
      , carico_freddo
      , invio_freddo
      , chiudi_caldo
      , chiudi_freddo
      , cancella_caldo
      , cancella_freddo
    }

    #endregion

    #region Fasi ciclo tavoli
    public enum Stato
    {
      offLine,
      standBy,
      preparazione,
      riscaldamento,
      mantenimento,
      raffreddamento,
      fineCiclo,
      anomalia,
      spento
    }

    public enum Test
    {
      apertura = 10001
        , chiusura
        , rampa
        , trafilamento
        , maserati
    }

    public enum faseTest
    {
      attesaComandi_0 = 0
    , pidPortata_5000 = 5000
    , pidPressione_5100 = 5100
    , apertura_5210 = 5210
    , prepChiusura_5300 = 5300
    , prepChiusura_5305 = 5305
    , chiusura_5310 = 5310
    , prepRampa_6005 = 6005
    , prepRampa_6010 = 6010
    , prepRampa_6020 = 6020
    , prepRampa_6024 = 6024
    , prepRampaSali_6025 = 6025
    , RampaSali_6030 = 6030
    , RampaSali_6040 = 6040
    , RampaSali_6050 = 6050
    , RampaDisc_6060 = 6060
    , RampaDisc_6070 = 6070
    , RampaDisc_6080 = 6080
    , prepColli_7000 = 7000
    , prepStartColli_7010 = 7010
    , prepEsecColli_7020 = 7020
    }

    public static string[] descrFasi =
  {
       "Off Line"
      ,"Standby"
      ,"Riscaldo tavolo"
      ,"Riscaldo vassoio"
      ,"Mantenimento vassoio"
      ,"Raffreddamento vassoio"
      ,"Ciclo completato"
      ,"Anomalia"
      ,"Spento"
    };

    public static Color[] stateCol =
      {
        Color.DarkGray,         //offLine,
        Color.DarkGreen,        //standBy,
        Color.Orange,           //preparazione,
        Color.Yellow,           //riscaldamento,
        Color.Green,            //mantenimento,
        Color.DarkCyan,         //raffreddamento,
        Color.Lime,             //fineCiclo,
        Color.Red,              //anomalia,
        Color.Violet,           //spento
        Color.Coral
      };


    #endregion

    public static string[] aiChNames =
      {
      };

    public static string[] aiLabelShort =
      {
      };

    public static string[] aiLabelLong =
      {

      "Portata"      //7
      ,"Pressione"
      };

    public static string[] floatFieldNames =
   {

      "Portata"      //7
      ,"Pressione"
    };


    public enum floatFieldNum
    {
      Portata      //7
            , Pressione
    };


    #region fButtons

    public enum fbScopeNames
    {
      main,
      sessione,
      opBase,
      sistema
    }

    #region 0 - Menu principale
    public enum fbName0_Main
    {
      testing,
      reports,
      nada3,
      nada4,
      nada7,
      nada8,
      sessione
    };

    public static btnProperties[] btScope0_Main =
    {
      new btnProperties("Testing >"       ,  1, Color.Green, Color.Green, Color.Green, true, (int)dirittiVal.test),
      new btnProperties("Report >"        ,  2, Color.DarkOrange, Color.DarkOrange, Color.DarkOrange, true, (int)dirittiVal.report),
      new btnProperties(""                ,  3, Color.Blue, Color.Blue, Color.Blue, true),
      new btnProperties(""                ,  4, Color.Tan, Color.Tan, Color.Tan, true),
      new btnProperties(""                ,  5, Color.Green, Color.Green, Color.Green, true, (int)dirittiVal.manutenzione),
      new btnProperties(""                ,  6, Color.SteelBlue, Color.SteelBlue, Color.SteelBlue, true),
      new btnProperties("sessione >"      ,  7, Color.Red, Color.Red, Color.Red, true)
    };
    #endregion

    #region 1 - sessioni
    public enum fbName1_sessione
    {
      logIn
      , logOff
      , nada3
      , gesUtenti
      , nada5
      , fineLavoro
      , nada7
      , mainMenu
    };

    public static btnProperties[] btScope1_sessione =
    {
      new btnProperties("login"             , 1, Color.Gainsboro, Color.Gainsboro, Color.Gainsboro, true),
      new btnProperties("logoff"            , 2, Color.Gainsboro, Color.Gainsboro, Color.Gainsboro, true, (int)dirittiVal.Logoff),
      new btnProperties(""                  , 3, Color.Gainsboro, Color.Gainsboro, Color.Gainsboro, true),
      new btnProperties("gestione\nutenti"  , 4, Color.Gainsboro, Color.Gainsboro, Color.Gainsboro, true, (int)dirittiVal.GesUtenti),
      new btnProperties(""                  , 5, Color.Gainsboro, Color.Gainsboro, Color.Gainsboro, true),
      new btnProperties("fine\nlavoro"      , 6, Color.Gainsboro, Color.Gainsboro, Color.Gainsboro, true, (int)dirittiVal.Esci),
      new btnProperties(""                  , 7, Color.Gainsboro, Color.Gainsboro, Color.Gainsboro, true),
      new btnProperties("main\nmenu"        , 8, Color.Purple, Color.Purple, Color.Purple, true)
    };
    #endregion

    #region 2 - menu Testing
    public enum fbName2_OpBase
    {
      testApertura,
      testChiusura,
      testRampa,
      testCollimazione,
      riserva_1,
      comandiManuali,
      riserva_2,
      menuMain
    };

    public static btnProperties[] btScope2_OpBase =
    {
      new btnProperties("Test Apertura"       ,  1, Color.LightSteelBlue, Color.SteelBlue, Color.SteelBlue, true),
      new btnProperties("Test Chiusura"       ,  2, Color.LightSteelBlue, Color.SteelBlue, Color.SteelBlue, true),
      new btnProperties("Test Rampa"          ,  3, Color.LightSteelBlue, Color.SteelBlue, Color.SteelBlue, true),
      new btnProperties("Test\nCollimazione"  ,  4, Color.LightSteelBlue, Color.SteelBlue, Color.SteelBlue, true),
      new btnProperties(""                    ,  5, Color.LightSteelBlue, Color.SteelBlue, Color.SteelBlue, true),
      new btnProperties("Comandi Manuali"     ,  6, Color.LightSteelBlue, Color.SteelBlue, Color.SteelBlue, true),
      new btnProperties(""                    ,  7, Color.LightSteelBlue, Color.SteelBlue, Color.SteelBlue, true),
      new btnProperties("main\nmenu"          ,  8, Color.Purple, Color.Purple, Color.Purple, true)
    };
    #endregion

    //#region 9 - menu sistema
    //public enum fbName9_sistema
    //{
    //  diagnDigit,
    //  diagnAnal,
    //  nada_0,
    //  nada_1,
    //  anagrafiche,
    //  nada_2,
    //  nada_3,
    //  mainMenu
    //};

    //public static btnProperties[] btScope9_sistema =
    //  {
    //    new btnProperties("diagn. digit."       ,  1, Color.SteelBlue, Color.SteelBlue, Color.SteelBlue, true, (int)dirittiVal.manutenzione),
    //    new btnProperties("diagn. anal."        ,  2, Color.SteelBlue, Color.SteelBlue, Color.SteelBlue, true, (int)dirittiVal.manutenzione),
    //    new btnProperties(""                    ,  3, Color.SteelBlue, Color.SteelBlue, Color.SteelBlue, true),
    //    new btnProperties(""                    ,  4, Color.SeaGreen, Color.SeaGreen, Color.SeaGreen, true),
    //    new btnProperties("anagrafiche"         ,  5, Color.SteelBlue, Color.SteelBlue, Color.SteelBlue, true, (int)dirittiVal.anagrafiche),
    //    new btnProperties(""                    ,  6, Color.SteelBlue, Color.SteelBlue, Color.SteelBlue, true),
    //    new btnProperties(""                    ,  7, Color.SteelBlue, Color.SteelBlue, Color.SteelBlue, true),
    //    new btnProperties("main\nmenu"          ,  8, Color.Purple, Color.Purple, Color.Purple, true),
    //  };
    //#endregion


    #endregion

    #region DIRITTI

    public static string[] dirittiLabel =
    {
       "logoff        "
      ,"gest. utenti  "
      ,"esci          "
      ,"test          "
      ,"report        "
      ,"manutenzione  "
    };

    public enum dirittiVal
    {
      Logoff = 0x00001,
      GesUtenti = 0x00002,
      Esci = 0x00004,
      test = 0x00008,
      report = 0x00010,
      manutenzione = 0x00020,

      //carSemiLav    = 0x800000
    }

    #endregion
  }
}
