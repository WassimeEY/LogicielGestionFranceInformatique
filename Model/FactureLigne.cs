using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FranceInformatiqueInventaire.Model
{
    public class FactureLigne
    {
        public int id;
        public string nom;
        public string prestation;
        public string date;
        public string commentaire;
        public float prixHT;
        public float prixTTC;
        public float difference;



        public FactureLigne(int id, string nom, string prestation, string date, string commentaire, float prixHT, float prixTTC, float difference)
        {
            this.id = id;
            this.nom = nom;
            this.prestation = prestation;
            this.date = date;
            this.commentaire = commentaire;
            this.prixHT = prixHT;
            this.prixTTC = prixTTC;
            this.difference = difference;
        }
    }
}
