using LiveCharts;

namespace HSTDemo.Models
{
    class HSChart
    {
        public HSChart()
        {
            _seriesCollection = new SeriesCollection();
            _cartesianVisuals = new VisualElementsCollection();
        }
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
