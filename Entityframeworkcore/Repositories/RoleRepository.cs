using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using ZTDomain.IRepositories;
using ZTDomain.Model;
using ZTDomain.ModelsExtended;

namespace Entityframeworkcore.Repositories
{
    public class RoleRepository : FonourRepositoryBase<Role>, IRoleRepository
    {

        ///** 长半径a=6378137 */
        //private static double a = 6378137;
        ///** 短半径b=6356752.3142 */
        //private static double b = 6378137;
        ///** 扁率f=1/298.2572236 */
        //private static double f = 1 / 298.2572236;
        //public static Point computerThatLonLat(Double lon, Double lat, Double brng, Double dist)
        //{
        //    Point longLat = new Point();
        //    double alpha1 = rad(brng);
        //    double sinAlpha1 = Math.Sin(alpha1);
        //    double cosAlpha1 = Math.Cos(alpha1);

        //    double tanU1 = (1 - f) * Math.Tan(rad(lat));
        //    double cosU1 = 1 / Math.Sqrt((1 + tanU1 * tanU1));
        //    double sinU1 = tanU1 * cosU1;
        //    double sigma1 = Math.Atan2(tanU1, cosAlpha1);
        //    double sinAlpha = cosU1 * sinAlpha1;
        //    double cosSqAlpha = 1 - sinAlpha * sinAlpha;
        //    double uSq = cosSqAlpha * (a * a - b * b) / (b * b);
        //    double A = 1 + uSq / 16384 * (4096 + uSq * (-768 + uSq * (320 - 175 * uSq)));
        //    double B = uSq / 1024 * (256 + uSq * (-128 + uSq * (74 - 47 * uSq)));

        //    double cos2SigmaM = 0;
        //    double sinSigma = 0;
        //    double cosSigma = 0;
        //    double sigma = dist / (b * A), sigmaP = 2 * Math.PI;
        //    while (Math.Abs(sigma - sigmaP) > 1e-12)
        //    {
        //        cos2SigmaM = Math.Cos(2 * sigma1 + sigma);
        //        sinSigma = Math.Sin(sigma);
        //        cosSigma = Math.Cos(sigma);
        //        double deltaSigma = B * sinSigma * (cos2SigmaM + B / 4 * (cosSigma * (-1 + 2 * cos2SigmaM * cos2SigmaM)
        //                - B / 6 * cos2SigmaM * (-3 + 4 * sinSigma * sinSigma) * (-3 + 4 * cos2SigmaM * cos2SigmaM)));
        //        sigmaP = sigma;
        //        sigma = dist / (b * A) + deltaSigma;
        //    }

        //    double tmp = sinU1 * sinSigma - cosU1 * cosSigma * cosAlpha1;
        //    double lat2 = Math.Atan2(sinU1 * cosSigma + cosU1 * sinSigma * cosAlpha1,
        //            (1 - f) * Math.Sqrt(sinAlpha * sinAlpha + tmp * tmp));
        //    double lambda = Math.Atan2(sinSigma * sinAlpha1, cosU1 * cosSigma - sinU1 * sinSigma * cosAlpha1);
        //    double C = f / 16 * cosSqAlpha * (4 + f * (4 - 3 * cosSqAlpha));
        //    double L = lambda - (1 - C) * f * sinAlpha
        //            * (sigma + C * sinSigma * (cos2SigmaM + C * cosSigma * (-1 + 2 * cos2SigmaM * cos2SigmaM)));

        //    //        double revAz = Math.atan2(sinAlpha, -tmp); // final bearing

        //    //        System.out.println(revAz);
        //    //        System.out.println(lon+deg(L)+","+deg(lat2));

        //    longLat.X =Convert.ToInt32(lon + deg(L));
        //    longLat.Y = Convert.ToInt32(deg(lat2));
        //    return longLat;
        //}
        //private static double deg(double x)
        //{
        //    return x * 180 / Math.PI;
        //}
        //private static double rad(double x)
        //{
        //    return x * 180 / Math.PI;
        //}
        public RoleRepository(ZTDbContext dbcontext) : base(dbcontext)
        {

        }
        /// <summary>
        /// 根据角色获取权限
        /// </summary>
        /// <returns></returns>
        public List<Guid> GetAllMenuListByRole(Guid roleId)
        {
            var roleMenus = _dbContext.Set<RoleMenu>().Where(it => it.RoleId == roleId);
            var menuIds = from t in roleMenus select t.MenuId;
            return menuIds.ToList();
        }

        /// <summary>
        /// 更新角色权限关联关系
        /// </summary>
        /// <param name="roleId">角色id</param>
        /// <param name="roleMenus">角色权限集合</param>
        /// <returns></returns>
        public bool UpdateRoleMenu(Guid roleId, List<RoleMenu> roleMenus)
        {
            var oldDatas = _dbContext.Set<RoleMenu>().Where(it => it.RoleId == roleId).ToList();
            oldDatas.ForEach(it => _dbContext.Set<RoleMenu>().Remove(it));
            _dbContext.SaveChanges();
            _dbContext.Set<RoleMenu>().AddRange(roleMenus);
            _dbContext.SaveChanges();
            return true;
        }
    }
}
