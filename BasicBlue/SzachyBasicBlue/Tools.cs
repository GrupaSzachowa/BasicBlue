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
    class Tools
    {
        public static TableLayoutPanelCellPosition GetCellPosotion(TableLayoutPanel panel)
        {
            //mouse position
            Point p = panel.PointToClient(Control.MousePosition);
            //Cell position
            TableLayoutPanelCellPosition pos = new TableLayoutPanelCellPosition(0, 0);
            //Panel size.
            Size size = panel.Size;
            //average cell size.
            SizeF cellAutoSize = new SizeF(size.Width / panel.ColumnCount, size.Height / panel.RowCount);

            //Get the cell row.
            //y coordinate
            float y = 0;
            for (int i = 0; i < panel.RowCount; i++)
            {
                //Calculate the summary of the row heights.
                SizeType type = panel.RowStyles[i].SizeType;
                float height = panel.RowStyles[i].Height;
                switch (type)
                {
                    case SizeType.Absolute:
                        y += height;
                        break;
                    case SizeType.Percent:
                        y += height / 100 * size.Height;
                        break;
                    case SizeType.AutoSize:
                        y += cellAutoSize.Height;
                        break;
                }
                //Check the mouse position to decide if the cell is in current row.
                if ((int)y > p.Y)
                {
                    pos.Row = i;
                    break;
                }
            }

            //Get the cell column.
            //x coordinate
            float x = 0;
            for (int i = 0; i < panel.ColumnCount; i++)
            {
                //Calculate the summary of the row widths.
                SizeType type = panel.ColumnStyles[i].SizeType;
                float width = panel.ColumnStyles[i].Width;
                switch (type)
                {
                    case SizeType.Absolute:
                        x += width;
                        break;
                    case SizeType.Percent:
                        x += width / 100 * size.Width;
                        break;
                    case SizeType.AutoSize:
                        x += cellAutoSize.Width;
                        break;
                }
                //Check the mouse position to decide if the cell is in current column.
                if ((int)x > p.X)
                {
                    pos.Column = i;
                    break;
                }
            }

            //return the mouse position.
            return pos;
        }

        public static List<Bierka> klonujBierki(List<Bierka> bb)
        {
            List<Bierka> ret = new List<Bierka>();
            foreach (Bierka b in bb)
            {
                Bierka nowa = (Bierka)b.Clone();
                ret.Add(nowa);

            }
            
            return ret;
        }

        public static List<Point> klonujPointy(List<Point> bb)
        {
            List<Point> ret = new List<Point>();
            foreach (Point b in bb) ret.Add(b);
            return ret;
        }

    }
}
