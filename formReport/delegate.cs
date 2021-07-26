using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace formReport
{
  public delegate void syncCom(string msg);
  public delegate void quitApp();

  public delegate void GotFocus(object sender, string msg);
  public delegate void syncStep(argSyncStep e);
  public delegate void Unsaved(int saveType);
  public delegate void Loaded(int what);
  public delegate void speedChanged(object sender);
  public delegate void changeRichText(int qualo, string msg);

  public delegate void btnPress(object snd, EventArgs evt);


  public delegate void endTest(int faseEnd);
  public delegate void reportGenerato(string strMessage);

}
