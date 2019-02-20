using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.OrmLite;
using ServiceStack.DesignPatterns.Model;
using System.Data;
using System.Configuration;
using System.Security.Cryptography;

namespace StockNet.Service
{
    public class DBService
    {
        private static string _connstr = ConfigurationManager.ConnectionStrings["stdb"].ConnectionString;
        private static OrmLiteConnectionFactory _dbFactory = new OrmLiteConnectionFactory(_connstr, MySqlDialect.Provider);
        public static IDbConnection OpenDb()
        {
            return _dbFactory.OpenDbConnection();
        }

        public static string SHA1Hash(string origin)
        {
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            byte[] bytes_sha1_in = UTF8Encoding.Default.GetBytes(origin);
            byte[] bytes_sha1_out = sha1.ComputeHash(bytes_sha1_in);
            sha1.Clear();
            (sha1 as IDisposable).Dispose();
            return Convert.ToBase64String(bytes_sha1_out);
        }

        public static void AddEntity<TEntity>(TEntity entity) where TEntity:IHasId<int>,new()
        {
            if (entity != null)
            {
                using (var db = DBService.OpenDb())
                {
                    db.Insert<TEntity>(entity);
                }
            }
        }

        public static int AddEntity<TEntity>(TEntity entity, bool selectIdentity) where TEntity : IHasId<int>, new()
        {
            long res=0;
            if (entity != null)
            {
                using (var db = DBService.OpenDb())
                {
                    db.Insert<TEntity>(entity);
                    if (selectIdentity)
                        res = db.GetLastInsertId();
                }
            }
            return Convert.ToInt32(res);
        }

        public static void AddList<TEntity>(IList<TEntity> entitys) where TEntity : IHasId<int>, new()
        {
            if (entitys != null && entitys.Count > 0)
            {
                using (var db = DBService.OpenDb())
                {
                    db.InsertAll<TEntity>(entitys);
                }
            }
        }

        public static void UpdateEntity<TEntity>(TEntity entity) where TEntity : IHasId<int>, new()
        {
            if (entity != null)
            {
                using (var db = DBService.OpenDb())
                {
                    db.Update<TEntity>(entity);
                }
            }
        }

        public static void DeleteEntity<TEntity>(int id) where TEntity : IHasId<int>, new()
        {
            if (id>0)
            {
                using (var db = DBService.OpenDb())
                {
                    db.Delete<TEntity>(ent=>ent.Id == id);
                }
            }
        }

        public static IList<TEntity> GetList<TEntity>() where TEntity : IHasId<int>, new()
        {
            using (var db = DBService.OpenDb())
            {
                return db.Select<TEntity>(te=>te.OrderByDescending(ee=>ee.Id));
            }
        }

        public static TEntity GetEntity<TEntity>(int id) where TEntity : IHasId<int>, new()
        {
            using (var db = DBService.OpenDb())
            {
                return db.SingleOrDefault<TEntity>("Id={0}",id);
            }
        }
    }
}
