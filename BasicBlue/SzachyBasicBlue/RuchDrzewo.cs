using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using SzachyBasicBlue;

namespace BasicBlue.SzachyBasicBlue
{
    public class RuchDrzewo
    {
        public Bierka bierka;
        public Point punkt;
        public int ile;
        public bool czyBicie;


        public RuchDrzewo(Bierka b, Point ruch, int wartosc, bool bicie)
        {
            this.czyBicie = bicie;
            this.bierka = b;
            this.ile = wartosc;
            this.punkt = ruch;
        }


        public RuchDrzewo()
        {

        }

    }
}
