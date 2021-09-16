using OxyPlot;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Задание_курсовой
{
    class MainViewModel
    {
        public MainViewModel()
        {
            this.Title = "Функция";
            this.Points = new List<DataPoint>();
            for(int i = 0; i < Dan.X.Length; i++)
            {
                this.Points.Add(new DataPoint(Dan.ChartX[i], Dan.Y[i]));
            }

        }

        public string Title { get; private set; }

        public IList<DataPoint> Points { get; private set; }
    }
}
