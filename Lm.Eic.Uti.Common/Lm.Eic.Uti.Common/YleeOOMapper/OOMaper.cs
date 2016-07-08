using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Lm.Eic.Uti.Common.YleeOOMapper
{
    /// <summary>
    /// 对象映射器
    /// </summary>
    public static class OOMaper
    {
        public static void Mapper<TObjectSource, TObjectDestination>(TObjectSource source, TObjectDestination destination)
            where TObjectSource : class, new()
            where TObjectDestination : class, new()
        {
            try
            {
                Type sourceT = source.GetType();
                var destiPts = destination.GetType().GetProperties();
                var sourcePts = source.GetType().GetProperties().ToList();
                if (destiPts.Length > 0)
                {
                    foreach (PropertyInfo ptdesti in destiPts)
                    {
                        PropertyInfo ptsource = sourcePts.FirstOrDefault(p => p.Name == ptdesti.Name);
                        if (ptsource != null)
                        {
                            if (ptdesti.Name.ToUpper() == "ENTITYSTATE" || ptdesti.Name.ToUpper() == "ENTITYKEY")
                            { }
                            else
                            {
                                object piSourceValue = ptsource.GetValue(source, null);
                                ptdesti.SetValue(destination, piSourceValue, null);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public static IList<TObjectDestination> Mapper<TObjectSource, TObjectDestination>(IList<TObjectSource> sources)
            where TObjectSource : class, new()
            where TObjectDestination : class, new()
        {
            List<TObjectDestination> destinations = new List<TObjectDestination>();
            try
            {
                foreach (var s in sources)
                {
                    TObjectDestination d = new TObjectDestination();
                    Mapper<TObjectSource, TObjectDestination>(s, d);
                    destinations.Add(d);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return destinations;
        }
    }
}