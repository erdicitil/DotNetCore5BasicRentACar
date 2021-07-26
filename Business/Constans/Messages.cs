using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Results;

namespace Business.Constans
{
    public class Messages
    {
        public static string AuthorizationDenied = "Yetkiniz yok";
        public static string UserRegistered = "Kayıt Oldu";
        public static string UserNotFound = "Kullanıcı Bulunamadı.";
        public static string PasswordError = "Parola Hatası";
        public static string SuccessfulLogin = "Başarılı Giriş";
        public static string UserAlreadyExists = "Kullanıcı Mevcut";
        public static string AccessTokenCreated = "Token oluşturuldu";
        internal static string CarAdded;
        internal static string CarDeleted;
        internal static string MaintenanceTime;
        internal static string CarListed;
        internal static string CarUptaded;
        internal static string BrandListed;
        internal static string BrandAdded;
        internal static string BrandUptaded;
        internal static string BrandDeleted;
        internal static string ColorListed;
        internal static string ColorAdded;
        internal static string ColorUptaded;
        internal static string ColorDeleted;
        internal static string RentalListed;
        internal static string RentalAdded;
        internal static string RentalUptaded;
        internal static string RentalDeleted;
        public static string CustomerListed;
        internal static string CustomerAdded;
        internal static string CustomerUptaded;
        internal static string CustomerDeleted;
        internal static string UserUpdated;
        internal static string UserDeleted;
        internal static string UserAdded;
        internal static string UserListed;
        internal static string UsersListed;
        internal static string CarImageAdded;
    }
}
