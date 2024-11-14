using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FranceInformatiqueInventaire.Model
{
    public class FacturePrestation
    {
        public string nom;
        public float pourcentageTVA;

        public FacturePrestation(string nom, float pourcentageTVA)
        {
            this.nom = nom;
            this.pourcentageTVA = pourcentageTVA;
        }
    }
}
