using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using MapModule.Controls;
using System.Drawing;

namespace MapModule.Views
{
    /// <summary>
    /// Interaction logic for MapView.xaml
    /// </summary>
    public partial class MapView : UserControl
    {
        public MapView()
        {
            InitializeComponent();
            MapControl.Manager.Mode = AccessMode.ServerAndCache;
            //MessageBox.Show("No internet connection avaible, going to CacheOnly mode.", "GMap.NET Demo",
            //    MessageBoxButton.OK, MessageBoxImage.Warning);
            //MapControl.MapProvider = AMapProvider.Instance; //高德地图
            MapControl.MapProvider = AMapProvider.Instance;
            MapControl.MinZoom = 2; //最小缩放
            MapControl.MaxZoom = 17; //最大缩放
            MapControl.Zoom = 5; //当前缩放
            MapControl.ShowCenter = true; //不显示中心十字点
            MapControl.DragButton = MouseButton.Left; //左键拖拽地图
            MapControl.Position = new PointLatLng(32.064, 118.704); //地图中心位置：南京

            //GMapOverlay polyOverlay = new GMapOverlay("polygons");
            //var points = new List<PointLatLng>();
            //points.Add(new PointLatLng(-25.969562, 32.585789));
            //points.Add(new PointLatLng(-25.966205, 32.588171));
            //points.Add(new PointLatLng(-25.968134, 32.591647));
            //points.Add(new PointLatLng(-25.971684, 32.589759));
            //GMapPolygon polygon = new GMapPolygon(points, "mypolygon");
            //polygon.Fill = new SolidBrush(System.Drawing.Color.FromArgb(50, System.Drawing.Color.Red));
            //polygon.Stroke = new System.Drawing.Pen(System.Drawing.Color.Red, 1);
            //polyOverlay.Polygons.Add(polygon);
        }
    }
}
