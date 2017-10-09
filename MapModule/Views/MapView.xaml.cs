using System;
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
using MapModule.Controls;

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
            //MapControl.Manager.Mode = AccessMode.CacheOnly;
            //MessageBox.Show("No internet connection avaible, going to CacheOnly mode.", "GMap.NET Demo",
            //    MessageBoxButton.OK, MessageBoxImage.Warning);
            MapControl = new AMapControl();
            MapControl.MapProvider = AMapProvider.Instance; //高德地图
            MapControl.MinZoom = 2; //最小缩放
            MapControl.MaxZoom = 17; //最大缩放
            MapControl.Zoom = 5; //当前缩放
            MapControl.ShowCenter = false; //不显示中心十字点
            MapControl.DragButton = MouseButton.Left; //左键拖拽地图
            MapControl.Position = new PointLatLng(32.064, 118.704); //地图中心位置：南京
        }
    }
}
