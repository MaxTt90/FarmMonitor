using System;
using System.Diagnostics;
using GMap.NET;

namespace MapModule.Controls
{
    public class AMapSateliteProvider : AMapProviderBase
    {
        public static readonly AMapSateliteProvider Instance;

        readonly Guid id = new Guid("FCA94AF4-3467-47c6-BDA2-6F52E4A145BC");
        public override Guid Id
        {
            get { return id; }
        }

        public override string Name
        {
            get
            {
                return "AMapSatelite";
            }
        }

        static AMapSateliteProvider()
        {
            Instance = new AMapSateliteProvider();
        }

        public override PureImage GetTileImage(GPoint pos, int zoom)
        {
            try
            {
                string url = MakeTileImageUrl(pos, zoom, LanguageStr);
                return GetTileImageUsingHttp(url);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        string MakeTileImageUrl(GPoint pos, int zoom, string language)
        {
            //http://webst04.is.autonavi.com/appmaptile?x=23&y=12&z=5&lang=zh_cn&size=1&scale=1&style=6
            string url = string.Format(UrlFormat , pos.X, pos.Y, zoom);
            return url;
        }

        static readonly string UrlFormat = "http://webst04.is.autonavi.com/appmaptile?x={0}&y={1}&z={2}&lang=zh_cn&size=1&scale=1&style=6";
        //static readonly string UrlFormat = "http://webst04.is.autonavi.com/appmaptile?x={0}&y={1}&z={2}&lang=zh_cn&size=1&scale=1&style=6";
    }
}
