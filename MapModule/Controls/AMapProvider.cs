// Copyright(c)2017, Elekta AB

using System;
using GMap.NET;

namespace MapModule.Controls
{
        public class AMapProvider : AMapProviderBase
        {
            public static readonly AMapProvider Instance;

            public override Guid Id { get; } = new Guid("EF3DD303-3F74-4938-BF40-232D0595EE88");

            public override string Name
            {
                get
                {
                    return "AMap";
                }
            }

            static AMapProvider()
            {
                Instance = new AMapProvider();
            }

            public override PureImage GetTileImage(GPoint pos, int zoom)
            {
                try
                {
                    string url = MakeTileImageUrl(pos, zoom, LanguageStr);
                    return GetTileImageUsingHttp(url);
                }
                catch (Exception)
                {
                    return null;
                }
            }

            private string MakeTileImageUrl(GPoint pos, int zoom, string language)
            {
                //var num = (pos.X + pos.Y) % 4 + 1;
                //string url = string.Format(UrlFormat, num, pos.X, pos.Y, zoom);
                string url = string.Format(UrlFormat, pos.X, pos.Y, zoom);
                return url;
            }

            static readonly string UrlFormat = "http://webrd04.is.autonavi.com/appmaptile?x={0}&y={1}&z={2}&lang=zh_cn&size=1&scale=1&style=7";
            //static readonly string UrlFormat = "http://webrd01.is.autonavi.com/appmaptile?lang=zh_cn&size=1&scale=1&style=7&x={0}&y={1}&z={2}";
        }
    }
