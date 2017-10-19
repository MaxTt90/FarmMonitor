using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using GMap.NET.WindowsPresentation;
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
            MapControl.Manager.Mode = AccessMode.ServerAndCache;

            MapControl.MapProvider = AMapProvider.Instance;
            MapControl.MinZoom = 2; //最小缩放
            MapControl.MaxZoom = 17; //最大缩放
            MapControl.Zoom = 12; //当前缩放
            MapControl.ShowCenter = false; //不显示中心十字点
            MapControl.DragButton = MouseButton.Left; //左键拖拽地图
            MapControl.SetPositionByKeywords("Shanghai, China");

            var marker = new GMapMarker(new PointLatLng(31.11, 121.29))
            {
                Shape = new Ellipse()
                {
                    Width = 10,
                    Height = 10,
                    Stroke = Brushes.Black,
                    StrokeThickness = 1.5
                }
            };
            MapControl.Markers.Add(marker);

        }

        private void MapControl_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                PointLatLng point = MapControl.FromLocalToLatLng((int)e.GetPosition(MapControl).X, (int)e.GetPosition(MapControl).Y);

                var marker = new GMapMarker(point)
                {
                    Shape = new Ellipse()
                    {
                        Width = 10,
                        Height = 10,
                        Stroke = Brushes.Black,
                        StrokeThickness = 1.5
                    }
                };
                MapControl.Markers.Add(marker);
            }
        }
    }
}
