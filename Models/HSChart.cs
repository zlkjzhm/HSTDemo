using LiveCharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSTDemo.Models
{
    class HSChart
    {
        private SeriesCollection _seriesCollection;

        public SeriesCollection SeriesCollection
        {
            get { return _seriesCollection; }
            set { _seriesCollection = value; }
        }

        private VisualElementsCollection _cartesianVisuals;
        public VisualElementsCollection CartesianVisuals
        {
            get { return _cartesianVisuals; }
            set { _cartesianVisuals = value; }
        }
    }
}
