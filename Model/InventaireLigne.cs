using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FranceInformatiqueInventaire.Model
{
    public class InventaireLigne
    {
        public int id;
        public string type;
        public string marque;
        public string nom;
        public string annee;
        public float prix;
        public string dateEntree;
        public string dateSortie;
        public string commentaire;

        public InventaireLigne(int id, string type, string marque, string nom, string annee, float prix, string dateEntree, string dateSortie, string commentaire)
        {
            this.id = id;
            this.type = type;
            this.marque = marque;
            this.nom = nom;
            this.annee = annee;
            this.prix = prix;
            this.dateEntree = dateEntree;
            this.dateSortie = dateSortie;
            this.commentaire = commentaire;
        }
    }
}
