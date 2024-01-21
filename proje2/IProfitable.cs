using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proje2
{
    public interface IProfitable
    {
        decimal ZararHesapla(DateTime tarih, string firmaAdi, string aracTuru, string aracId);

    }
}
