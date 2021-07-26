using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Rem.GeneralComponents;
using System.Data;
namespace formReport
{
  class dbGeneric
  {
    public V v = new V();

    public MySqlDataReader loadDisegni()
    {
      MySqlConnection connessioneDB = new MySqlConnection(DEF.strConnDb);
      string comandoSQL = "SELECT cdId, cdName FROM tblCodiciDisegni where cdValid = 1 order by cdId asc;";
      MySqlCommand comandoDB = new MySqlCommand(comandoSQL, connessioneDB);
      connessioneDB.Open();
      MySqlDataReader readerDB = comandoDB.ExecuteReader();
      return readerDB;
    }

    public int deleteCodDisegno(int id)
    {
      int result = 1;
      //preparazione e scrittura su database
      MySqlConnection connessioneDB = new MySqlConnection(DEF.strConnDb);

      string comandoSQL = "";
      comandoSQL = "UPDATE tblCodiciDisegni SET cdValid = 0 where cdId = " + id + ";";
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

    public int insertDisegno(string codice, string descrizione)
    {
      int result = 1;
      //preparazione e scrittura su database
      MySqlConnection connessioneDB = new MySqlConnection(DEF.strConnDb);

      string comandoSQL = "";
      comandoSQL = "INSERT INTO tblCodiciDisegni " +
         "(cdName, cdDescr) VALUES " +
         "('" + codice +
         "', '" + descrizione + "')";


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

    public DataSet readElencoTest()
    {
      MySqlConnection connessioneDB = new MySqlConnection(DEF.strConnDb);

      string tabella = "viewTest";

      string comandoSQL = "SELECT * FROM " + tabella + " order by prId desc limit 1000";

      MySqlDataAdapter myDA = new MySqlDataAdapter(comandoSQL, connessioneDB);

      DataSet ds = new DataSet();

      myDA.Fill(ds, "test");
      return ds;
    }

    public void removeTest(int id)
    {
      //preparazione e scrittura su database
      MySqlConnection connessioneDB = new MySqlConnection(DEF.strConnDb);

      string comandoSQL = "";
      comandoSQL = "delete from vba0108_ber.tblprove where prId = " + id + ";";


      connessioneDB.Open();

      MySqlCommand comandoDB = new MySqlCommand();

      comandoDB.CommandText = comandoSQL;
      comandoDB.Connection = connessioneDB;
      comandoDB.ExecuteNonQuery();
      connessioneDB.Close();
      connessioneDB = null;
    }

    public MySqlDataReader readDatiBanco(int idTest)
    {
      MySqlConnection connessioneDB = new MySqlConnection(DEF.strConnDb);

      string comandoSQL = "SELECT prDataTest FROM tblprove";

      if (idTest != -1)
        comandoSQL += " WHERE prId=?prId";

      MySqlCommand comandoDB = new MySqlCommand(comandoSQL, connessioneDB);

      if (idTest != -1)
        comandoDB.Parameters.AddWithValue("?prId", idTest);

      connessioneDB.Open();

      MySqlDataReader readerDB = comandoDB.ExecuteReader();

      return readerDB;
    }

    public MySqlDataReader readTest(string connection, string query)
    {
      MySqlConnection connessioneDB = new MySqlConnection(connection);
      string comandoSQL = query;
      MySqlCommand comandoDB = new MySqlCommand(comandoSQL, connessioneDB);
      connessioneDB.Open();
      MySqlDataReader readerDB = comandoDB.ExecuteReader();
      return readerDB;
    }



  }
}
