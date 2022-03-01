using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infa.Application.Utils
{
    public static class FilePath
    {
        #region Slider

        public const string SlideOriginPath  = "/img/slider/";

        #endregion

        #region Banner

        public const string BannerOriginPath = "/img/bg/";

        #endregion

        #region Avatar

        public static string DefaultAvatar = "/img/defaults/avatar.jpg";
        public static string UserAvatarOrigin = "/Content/Images/UserAvatar/origin/";
        public static string UserAvatarOriginServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Content/Images/UserAvatar/origin/");

        public static string UserAvatarThumb = "/Content/Images/UserAvatar/Thumb/";
        public static string UserAvatarThumbServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Content/Images/UserAvatar/Thumb/");

        #endregion


        #region products

        public static string ProductImage = "/content/images/product/origin/";

        public static string ProductImageImageServer =
            Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/images/product/origin/");

        public static string ProductThumbnailImage = "/content/images/product/thumb/";

        public static string ProductThumbnailImageImageServer =
            Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/images/product/thumb/");

        #endregion

    }
}
