using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FranceInformatiqueInventaire.Model
{
    public class SiteFavorisLigne
    {
        public string nom;
        public string url;

        public SiteFavorisLigne(string nom, string url)
        {
            this.nom = nom;
            this.url = url;
        }

        public override string ToString()
        {
            return nom;
        }
    }
}
