using System;
using System.Collections.Generic;
using System.Linq;

namespace AuPaysDuPereNoel
{
     public class Node<T>
    {
        public T Valeur { get; set; }
        internal Node<T>? Next { get; set; }

        public Node(T valeur, Node<T>? next)
        {
            Valeur = valeur;
            Next = next;
        }
    }

    // ======== LISTE CHAINEE GENERIQUE ========
    public class MyList<T>
    {
        private Node<T>? _head;

        public MyList()
        {
            _head = null;
        }

        // Renvoie le premier noeud (ou null)
        public Node<T>? Head()
        {
            return _head;
        }

        // Valeur du premier élément
        public T GetFirstValue()
        {
            if (_head == null)
                throw new ArgumentOutOfRangeException("liste vide");
            return _head.Valeur;
        }

        // Valeur du dernier élément
        public T GetLastValue()
        {
            if (_head == null)
                throw new ArgumentOutOfRangeException("liste vide");

            T valeur = _head.Valeur;
            Node<T>? tmp = _head;
            while (tmp != null)
            {
                valeur = tmp.Valeur;
                tmp = tmp.Next;
            }
            return valeur;
        }

        // Ajout en début
        public void AddFirst(T valeur)
        {
            _head = new Node<T>(valeur, _head);
        }

        // Ajout en fin
        public void AddLast(T valeur)
        {
            if (_head == null)
            {
                AddFirst(valeur);
                return;
            }

            Node<T> pt = _head;
            while (pt.Next != null)
            {
                pt = pt.Next;
            }

            pt.Next = new Node<T>(valeur, null);
        }

        // Insertion à l'indice donné
        public void Insert(int index, T valeur)
        {
            if (index < 0)
                throw new ArgumentOutOfRangeException(nameof(index),
                    "INSERT MYLIST : L'index ne peut pas être négatif.");

            if (index == 0)
            {
                AddFirst(valeur);
                return;
            }

            if (_head == null)
                throw new ArgumentOutOfRangeException(nameof(index),
                    "INSERT MYLIST : L'index dépasse la taille de la liste.");

            Node<T> pt = _head;
            for (int i = 0; i < index - 1; i++)
            {
                if (pt.Next == null)
                    throw new ArgumentOutOfRangeException(nameof(index),
                        "INSERT MYLIST : L'index dépasse la taille de la liste.");
                pt = pt.Next;
            }

            pt.Next = new Node<T>(valeur, pt.Next);
        }

        // Affichage simple (un élément par ligne)
        public void ShowList()
        {
            if (_head == null)
            {
                Console.WriteLine("Liste vide");
                return;
            }

            Node<T>? pt = _head;
            while (pt != null)
            {
                Console.WriteLine(pt.Valeur);
                pt = pt.Next;
            }
            Console.WriteLine();
        }

        // Affichage sur une ligne (utile pour la file)
        public void ShowList4File()
        {
            if (_head == null)
            {
                Console.WriteLine("Liste vide");
                return;
            }

            Node<T>? pt = _head;
            while (pt != null)
            {
                Console.Write(pt.Valeur + " ");
                pt = pt.Next;
            }
            Console.WriteLine();
        }

        // Supprimer le premier élément et le renvoyer
        public T RemoveFirst()
        {
            if (_head == null)
                throw new InvalidOperationException(
                    "REMOVEFIRST MYLIST : Impossible de supprimer : la liste est vide.");

            T valeur = _head.Valeur;
            _head = _head.Next;
            return valeur;
        }

        // Supprimer le dernier élément et le renvoyer
        public T RemoveLast()
        {
            if (_head == null)
                throw new InvalidOperationException(
                    "REMOVELAST MYLIST : Impossible de supprimer : la liste est vide.");

            // Un seul élément
            if (_head.Next == null)
            {
                T valeurUnique = _head.Valeur;
                _head = null;
                return valeurUnique;
            }

            Node<T> pt = _head;
            while (pt.Next != null && pt.Next.Next != null)
            {
                pt = pt.Next;
            }

            T valeur = pt.Next!.Valeur;
            pt.Next = null;
            return valeur;
        }

        // Suppression à un indice
        public void RemoveAt(int index)
        {
            if (index < 0)
                throw new ArgumentOutOfRangeException(nameof(index),
                    "L'index ne peut pas être négatif.");

            if (_head == null)
                throw new InvalidOperationException(
                    "REMOVEAT MYLIST : Impossible de supprimer : la liste est vide.");

            if (index == 0)
            {
                RemoveFirst();
                return;
            }

            if (_head.Next == null)
                throw new ArgumentOutOfRangeException(nameof(index),
                    "REMOVEAT MYLIST : L'index dépasse la taille de la liste.");

            Node<T> pt = _head;
            for (int i = 0; i < index - 1; i++)
            {
                if (pt.Next == null)
                    throw new ArgumentOutOfRangeException(nameof(index),
                        "REMOVEAT MYLIST : L'index dépasse la taille de la liste.");
                pt = pt.Next;
            }

            pt.Next = pt.Next!.Next;
        }

        // Vider la liste
        public void Clear()
        {
            _head = null;
        }

        public bool IsEmpty()
        {
            return _head == null;
        }

        public bool IsFull()
        {
            return false;
        }
    }

     // ======== PILE GENERIQUE (MyStack) ========
    public class MyStack<T>
    {
        public int Size { get; private set; }
        private MyList<T> _pile;

        public MyStack()
        {
            Size = 0;
            _pile = new MyList<T>();
        }

        // Empiler
        public void Empiler(T valeur)   // équivalent Push
        {
            _pile.AddFirst(valeur);
            Size++;
        }

        // Dépiler
        public T Depiler()              // équivalent Pop
        {
            if (IsEmpty())
                throw new InvalidOperationException(
                    "Impossible de dépiler : la pile est vide.");

            Size--;
            return _pile.RemoveFirst();
        }

        // Voir le sommet
        public T Peek()
        {
            if (IsEmpty())
                throw new InvalidOperationException(
                    "Impossible de Peek : la pile est vide.");

            return _pile.GetFirstValue();
        }

        public bool IsEmpty()
        {
            return Size == 0;
        }

        // Affichage pour test
        public void AfficherPileForExample()
        {
            _pile.ShowList();
        }

        // Transformer la pile en liste (et la vider)
        public MyList<T> ToList()
        {
            MyList<T> liste = new MyList<T>();
            while (!IsEmpty())
            {
                liste.AddLast(Depiler());
            }
            return liste;
        }

        // Recharger la pile depuis une liste (en repartant du dernier)
        public void FromList(MyList<T> liste)
        {
            while (!liste.IsEmpty())
            {
                Empiler(liste.GetLastValue());
                liste.RemoveLast();
            }
        }

        // Version simple si on reste sur liste chaînée
        public MyList<T> ToList2()
        {
            return _pile;
        }

        public void FromList2(MyList<T> liste)
        {
            _pile = liste;
            // mettre à jour Size si besoin (parcours pour compter)
        }
    }

     // ======== FILE GENERIQUE (MyQueue) ========
    public class MyQueue<T>
    {
        public int Size { get; private set; }
        private MyList<T> _file;

        public MyQueue()
        {
            Size = 0;
            _file = new MyList<T>();
        }

        // Enfiler
        public void Enfiler(T valeur)   // équivalent Enqueue
        {
            _file.AddFirst(valeur);
            Size++;
        }

        // Défiler
        public T Defiler()              // équivalent Dequeue
        {
            if (IsEmpty())
                throw new InvalidOperationException(
                    "Impossible de défiler : la file est vide.");

            Size--;
            return _file.RemoveLast();
        }

        // Voir le premier élément logique
        public T Peek()
        {
            if (IsEmpty())
                throw new InvalidOperationException(
                    "Impossible de Peek : la file est vide.");

            return _file.GetLastValue();
        }

        public bool IsEmpty()
        {
            return Size == 0;
        }

        // Affichage pour test
        public void AfficherFileForExample()
        {
            _file.ShowList4File();
        }

        // Transformer la file en liste (et la vider)
        public MyList<T> ToList()
        {
            MyList<T> liste = new MyList<T>();
            while (!IsEmpty())
            {
                liste.AddLast(Defiler());
            }
            return liste;
        }

        public void FromList(MyList<T> liste)
        {
            while (!liste.IsEmpty())
            {
                Enfiler(liste.GetFirstValue());
                liste.RemoveFirst();
            }
        }

        // Version simple si on reste sur liste chaînée
        public MyList<T> ToList2()
        {
            return _file;
        }

        public void FromList2(MyList<T> liste)
        {
            _file = liste;
            // idem, Size à recalc si besoin
        }
    }

    // ============= ENUMS ET STRUCTURES =============

    public enum TypeCadeau
    {
        Nounours,
        Tricycle,
        Jumelles,
        AbonnementGK, // Abonnement Geeky Junior
        Ordinateur
    }
    public enum Continent
    {
        Afrique,
        Europe,
        Amerique,
        Asie,
        Oceanie
    }

    public enum StatutEmploye
    {
        EnTravail,
        EnAttente,
        EnRepos
    }

  


    // ============= CLASSE LETTRE =============

    public class Lettre
    {
        public string NomEnfant { get; set; }
        public string Adresse { get; set; }
        public Continent Continent { get; set; }
        public int Age { get; set; }

        public Lettre(string nom, string adresse, Continent continent, int age)
        {
            NomEnfant = nom;
            Adresse = adresse;
            Continent = continent;
            Age = age;
        }

        // Méthodes statiques utilitaires
        public static TypeCadeau DeterminerTypeCadeau(int age)
        {
            if ((age >= 0) && (age <= 2))
            {
                return (TypeCadeau.Nounours);
            }
             else if ((age > 2) && (age <= 5))
            {
                return (TypeCadeau.Tricycle);
            }
             else if ((age > 5) && (age <= 10))
            {
                return (TypeCadeau.Jumelles);
            }
             else if ((age > 10) && (age <= 15))
            {
                return (TypeCadeau.AbonnementGK);
            }
             else if ((age > 15) && (age <= 18))
            {
                return (TypeCadeau.Ordinateur);
            }
            throw new ArgumentException("Age invalide !") ;
        }

        public static int ObtenirDureeFabrication(int age)
        {
            if ((age >= 0) && (age <= 2))
            {
                return (3);
            }
             else if ((age > 2) && (age <= 5))
            {
                return (4);
            }
             else if ((age > 5) && (age <= 10))
            {
                return (6);
            }
             else if ((age > 10) && (age <= 15))
            {
                return (1);
            }
             else if ((age > 15) && (age <= 18))
            {
                return (10);
            }
            throw new ArgumentException("Age invalide !") ;
        }

        // Propriétés d'instance pour faciliter l'accès
        public TypeCadeau TypeCadeau
        {
            get
            {
                return DeterminerTypeCadeau(Age);
            }
        }

        public int DureeFabrication
        {
            get
            {
                return ObtenirDureeFabrication(Age);
            }
        }
    }

    // ============= CLASSE LUTIN =============

    public class Lutin
    {
        public int Id { get; set; }
        public StatutEmploye Statut { get; set; }
        public Lettre LettreEnCours { get; set; }
        public int HeuresRestantes { get; set; }

        public Lutin (int id)
        {
            Id = id;
            Statut = StatutEmploye.EnAttente;
            LettreEnCours = null;
            HeuresRestantes = 0;
        }

        public void CommencerFabrication(Lettre lettre)
        {
            if (lettre == null) {return ;}
            LettreEnCours = lettre;
            HeuresRestantes = lettre.DureeFabrication;
            Statut = StatutEmploye.EnTravail;

        }

        public Lettre Travailler()
        {
            if (Statut != StatutEmploye.EnTravail || LettreEnCours == null)
                return null;

            HeuresRestantes--;

            if (HeuresRestantes <= 0)
            {
                Lettre lettreTerminee = LettreEnCours;
                LettreEnCours = null;
                Statut = StatutEmploye.EnAttente;
                return lettreTerminee;
            }

            return null;
        }
    }

    // ============= CLASSE NAIN =============

    public class Nain
    {
        public int Id { get; set; }
        public StatutEmploye Statut { get; set; }
        public Lettre LettreEnCours { get; set; }
        public int HeuresRestantes { get; set; }

        public Nain(int id)
        {
            Id =id;
            Statut = StatutEmploye.EnAttente;
            LettreEnCours = null;
            HeuresRestantes = 0;
        }

        public void CommencerEmballage(Lettre lettre)
        {
            if (lettre == null) return;
            Statut = StatutEmploye.EnTravail ;
            HeuresRestantes = 2;
            LettreEnCours = lettre;
        }

        public Lettre Travailler()
        {
            if (Statut != StatutEmploye.EnTravail || LettreEnCours == null)
                return null;

            HeuresRestantes--;

            if (HeuresRestantes <= 0)
            {
                Lettre lettreTerminee = LettreEnCours;
                LettreEnCours = null;
                Statut = StatutEmploye.EnAttente;
                return lettreTerminee;
            }

            return null;
        }
    }

    // ============= CLASSE TRAINEAU =============

    public class Traineau
    {
        public Continent Continent { get; set; }
        public MyStack<Lettre> Lettres { get; set; }
        public int CapaciteMax { get; set; }
        public bool EnVoyage { get; set; }
        public int HeuresVoyageRestantes { get; set; }

        public Traineau(Continent continent, int capacite)
        {
            Continent = continent;
            CapaciteMax = capacite;
            Lettres = new MyStack<Lettre> ();
            EnVoyage = false;
            HeuresVoyageRestantes = 0;
        }

        public bool PeutCharger()   //Est-ce que l'on peut charger le traineau ?
        {
            //On ne peut charger que si on n'est pas en voyage et que la capacité n,est pas atteinte
            if (!EnVoyage && Lettres.Size < CapaciteMax)
            return true;
            else
            return false;
        }

        public void ChargerLettre(Lettre lettre)    // charger le traineau si c'est possible
        {
            if (lettre == null)
             {return;}
            if (PeutCharger()) 
            {
                Lettres.Empiler(lettre);
            }
        }

        public bool EstPlein()  // Le traineau est-il plein ?
        {
            if (Lettres.Size >= CapaciteMax)
             {return true;}
            else
             {return false;}
                    
        }

        public void PartirEnVoyage()    // Le départ du traineau.
        {
            if (EnVoyage) return;
            if (Lettres.Size > 0)
            {
                EnVoyage = true;
                HeuresVoyageRestantes = 6;
            }
        }

        public List<Lettre> AvancerVoyage()
        {
            if (!EnVoyage) return null;

            HeuresVoyageRestantes--;

            if (HeuresVoyageRestantes <= 0)
            {
                // Retour de voyage, décharger les lettres
                List<Lettre> lettresLivrees = new List<Lettre>() ; // Transforme une pile en Liste
                while (!Lettres.IsEmpty())
                {
                    lettresLivrees.Add(Lettres.Depiler());
                }
                EnVoyage = false;
                return lettresLivrees;
            }

            return null;
        }
    }

    // ============= CLASSE ELFE =============

    public class Elfe
    {
        public int Id { get; set; }
        public Continent Continent { get; set; }
        public Traineau Traineau { get; set; }

        public Elfe(int id, Continent continent, int capaciteTraineau)
        {
            Id = id;
            Continent = continent;
            Traineau = new Traineau(Continent,capaciteTraineau);
        }

        public bool PeutRecevoirLettre()
        {
            return Traineau.PeutCharger();
        }

        public void RecevoirLettre(Lettre lettre)
        {
            Traineau.ChargerLettre(lettre);

            // Si le traîneau est plein, partir en voyage
            if (Traineau.EstPlein())
            {
                Traineau.PartirEnVoyage();
            }
        }
    }

    // ============= CLASSE ENTREPOT =============

    public class Entrepot
    {
        public Continent Continent { get; set; }
        public MyStack<Lettre> Lettres { get; set; }

        public Entrepot(Continent continent)
        {
            Continent = continent;
            Lettres = new MyStack<Lettre>();
        }

        public void AjouterLettres(List<Lettre> lettres)
        {
            foreach (Lettre lettre in lettres)
            {
                Lettres.Empiler(lettre);
            }
        }

        public int[] GetStatistiques()
        {
            // Tableau indexé par TypeCadeau (0=Nounours, 1=Tricycle, etc.)
            int[] stats = new int[5];

            MyStack<Lettre> temp = new MyStack<Lettre>();

            // Vider la pile dans temp en comptant
            while (!Lettres.IsEmpty())
            {
                Lettre lettre = Lettres.Depiler();
                stats[(int)lettre.TypeCadeau]++;
                temp.Empiler(lettre);
            }

            // Remettre les lettres dans l'entrepôt
            while (!temp.IsEmpty())
            {
                Lettres.Empiler(temp.Depiler());
            }

            return stats;
        }
    }

    // ============= CLASSE SIMULATION =============

    public class Simulation // La classe qui gère la simulation.
    {
        // Paramètres configurables
        public int NbLutins { get; set; }
        public int NbNains { get; set; }
        public int NbEnfants { get; set; }
        public int NbLettresParHeure { get; set; }
        public int NbJouetParTraineau { get; set; }

        // Personnel
        public List<Lutin> Lutins { get; set; }
        public List<Nain> Nains { get; set; }
        public List<Elfe> Elfes { get; set; }

        // Files et piles
        public MyStack<Lettre> BureauPereNoel { get; set; }
        public MyQueue<Lettre> FileAttenteFabrication { get; set; }
        public MyQueue<Lettre> FileAttenteEmballage { get; set; }
        public MyQueue<Lettre>[] FilesAttenteContinents { get; set; }
        // Entrepôts (un par continent, indexé par l'enum Continent)
        public Entrepot[] Entrepots { get; set; }

        // Temps et coûts
        public int HeureActuelle { get; set; }
        public int JourActuel { get; set; }
        public double CoutTotal { get; set; }
        public List<double> CoutsParHeure { get; set; }

        // Gestion du personnel
        public int DerniereModificationLutins { get; set; }
        public int DerniereModificationNains { get; set; }

        // Générateur aléatoire
        private Random random;

        // La mise en route de la simulation avec la mise à jour de tous les paramètres.
        public Simulation(int nbLutins, int nbNains, int nbEnfants, int nbLettresParHeure, int nbJouetParTraineau)
        {
            NbLutins = nbLutins;
            NbNains = nbNains;
            NbEnfants = nbEnfants;
            NbLettresParHeure = nbLettresParHeure;
            NbJouetParTraineau = nbJouetParTraineau;

            random = new Random();
            HeureActuelle = 0;
            JourActuel = 1;
            CoutTotal = 0;
            CoutsParHeure = new List<double>();
            DerniereModificationLutins = -12;   // Pour bien gérer le fait qu'on ne peut recruter des lutins pendant 12 heures si on en a mis en repos.
            DerniereModificationNains = -24;    // Pour bien gérer le fait qu'on ne peut recruter des nains pendant 12 heures si on en a mis en repos.

            // Initialiser les lutins
            Lutins = new List<Lutin>();
            for (int i = 0; i < nbLutins; i++)
            {
                Lutins.Add(new Lutin(i + 1));
            }

            // Initialiser les nains
            Nains = new List<Nain>();
            for (int i = 0; i < nbNains; i++)
            {
                Nains.Add(new Nain(i + 1));
            }

            // Initialiser les elfes (5, un par continent)
            Elfes = new List<Elfe>();
            int elfId = 1;
            foreach (Continent continent in Enum.GetValues(typeof(Continent)))
            {
                Elfes.Add(new Elfe(elfId++, continent, nbJouetParTraineau));
            }

            // Initialiser les structures de données
            BureauPereNoel = new MyStack<Lettre>();
            FileAttenteFabrication = new MyQueue<Lettre>();
            FileAttenteEmballage = new MyQueue<Lettre>();

            FilesAttenteContinents = new MyQueue<Lettre>[5];
            for (int i = 0; i < 5; i++)
            {
                FilesAttenteContinents[i] = new MyQueue<Lettre>();
            }

            Entrepots = new Entrepot[5];
            for (int i = 0; i < 5; i++)
            {
                Entrepots[i] = new Entrepot((Continent)i);
            }

            // Générer toutes les lettres
            GenererLettres();
        }

        private void GenererLettres()
        {
            string[] prenoms = { "Alice", "Bob", "Charlie", "Diana", "Emma", "Frank", "Grace", "Hugo",
                               "Iris", "Jack", "Kate", "Leo", "Marie", "Nathan", "Olivia", "Paul" };
            string[] villes = { "Paris", "Londres", "Tokyo", "New York", "Sydney", "Le Caire",
                              "Berlin", "Rome", "Pékin", "Rio", "Moscou", "Toronto" };

            List<Lettre> toutesLesLettres = new List<Lettre>();

            for (int i = 0; i < NbEnfants; i++)
            {
                string nom = prenoms[random.Next(prenoms.Length)] + i;
                string adresse = villes[random.Next(villes.Length)];
                Continent continent = (Continent)random.Next(5);
                int age = random.Next(0, 19);

                toutesLesLettres.Add(new Lettre(nom, adresse, continent, age));
            }

            // Mélanger les lettres (algorithme Fisher-Yates)
            for (int i = toutesLesLettres.Count - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                Lettre temp = toutesLesLettres[i];
                toutesLesLettres[i] = toutesLesLettres[j];
                toutesLesLettres[j] = temp;
            }

            foreach (Lettre lettre in toutesLesLettres)
            {
                BureauPereNoel.Empiler(lettre);
            }
        }

        public void AvancerUneHeure()
        {
            //Avancer l'heure
            HeureActuelle = HeureActuelle + 1;

            //Gérer le changement de jour (1 jour = 12 heures)
            JourActuel = (HeureActuelle / 12) + 1;
          
            // Ajouter de nouvelles lettres au bureau du Père Noël
            int nbLettresArrivantes = random.Next(0, NbLettresParHeure + 1);
            for (int i = 0; i < nbLettresArrivantes && !BureauPereNoel.IsEmpty(); i++)
            {
                 Lettre lettre = BureauPereNoel.Depiler();
                FileAttenteFabrication.Enfiler(lettre);
            }

            // Distribuer les lettres aux lutins disponibles
            foreach (Lutin lutin in Lutins)
            {
                if (lutin.Statut == StatutEmploye.EnAttente && !FileAttenteFabrication.IsEmpty())
                {
                    Lettre lettre = FileAttenteFabrication.Defiler();
                    lutin.CommencerFabrication(lettre);
                }
            }

            // Faire travailler les lutins
            foreach (Lutin lutin in Lutins)
            {
                Lettre lettreTerminee = lutin.Travailler();
                if (lettreTerminee != null)
                    {
                        FileAttenteEmballage.Enfiler(lettreTerminee);
                    }
            }

            // Distribuer les lettres aux nains disponibles pour l'emballage
            foreach (Nain nain in Nains)
            {
                if (nain.Statut == StatutEmploye.EnAttente && !FileAttenteEmballage.IsEmpty())
                {
                     Lettre lettre = FileAttenteEmballage.Defiler();
                     nain.CommencerEmballage(lettre);
                }
            }
            Elfe TrouverElfeParContinent(Continent continent)
                    {
                        foreach (Elfe e in Elfes)
                        {
                            if (e.Continent == continent)
                                return e;
                        }
                        // Si aucun elfes est trouvé , lancer une exception
                        throw new ArgumentException($"Aucun elfes a été trouver pour le continant {continent}");
                    }

            // Faire travailler les nains
            foreach (Nain nain in Nains)
            {
                Lettre lettreTerminee = nain.Travailler();
                if (lettreTerminee != null)
                {
                    //Fonction local pour trouver l'elfe du continent

                    Elfe elfe = TrouverElfeParContinent(lettreTerminee.Continent);
                    if (elfe.PeutRecevoirLettre())
                    {
                        elfe.RecevoirLettre(lettreTerminee);
                    }
                    else
                    {
                        FilesAttenteContinents[(int)lettreTerminee.Continent].Enfiler(lettreTerminee);
                    }
                }
            }

            // Avancer le voyage des traîneaux
            foreach (Elfe elfe in Elfes)
            {
                List<Lettre> lettresLivrees = elfe.Traineau.AvancerVoyage();
                if (lettresLivrees != null)
                {
                    Entrepot entrepot = Entrepots[(int)elfe.Continent];
                    entrepot.AjouterLettres(lettresLivrees);
                }
                // Recharger le traîneau depuis la file d'attente du continent (si pas en voyage)
                if (elfe.Traineau.EnVoyage == false)
                {
                    MyQueue<Lettre> fileC = FilesAttenteContinents[(int)elfe.Continent];
                    while (!fileC.IsEmpty() && elfe.Traineau.PeutCharger())
                    {
                        Lettre l = fileC.Defiler();
                        elfe.RecevoirLettre(l);
                    }
                }
                if (!elfe.Traineau.EnVoyage && !elfe.Traineau.Lettres.IsEmpty())
                {
                       elfe.Traineau.PartirEnVoyage();

                }

            }
            // somme des coups par heure + modification des heures restantes 
            double coutsheure = 0.0; // on crée une variable qui va contenir le total des couts calculer
            foreach (Lutin lutin in Lutins) // on va parcourir la liste les lutin et le multiplier par le coup en dépandant de son statut
            {
                 //compteure qui va compter le nombre de lutin en attente
                if (lutin.Statut == StatutEmploye.EnTravail )
                {
                    coutsheure = coutsheure + 1.5 ;
                }
                else if (lutin.Statut == StatutEmploye.EnAttente)
                {
                    coutsheure = coutsheure +  1.0 ;

                }
            }
           
              
            foreach (Nain nain in Nains)
                {

                    if (nain.Statut == StatutEmploye.EnTravail)
                    {
                        coutsheure = coutsheure + 1.0;
                    }
                    else if (nain.Statut == StatutEmploye.EnAttente)
                    {
                        coutsheure = coutsheure +  0.5;

                    }
                }
            

            foreach (Elfe e in Elfes)
            {
                if (e.Traineau.EnVoyage == true)
                {
                    coutsheure = coutsheure + 2.0;
                }
                else
                {
                    coutsheure = coutsheure + 1.5; 
                }

            }
            CoutsParHeure.Add(coutsheure); //et on ajoute a la liste la valeur du couts par heure dans une case
            CoutTotal = CoutTotal + coutsheure;    

        }

        public bool ToutesLettresTraitees()
        {
            if (!BureauPereNoel.IsEmpty()) return false;
            if (!FileAttenteFabrication.IsEmpty()) return false;
            if (!FileAttenteEmballage.IsEmpty()) return false;

            // Vérifier si des lutins travaillent
            foreach (Lutin lutin in Lutins)
            {
                if (lutin.Statut == StatutEmploye.EnTravail)
                    return false;
            }

            // Vérifier si des nains travaillent
            foreach (Nain nain in Nains)
            {
                if (nain.Statut == StatutEmploye.EnTravail)
                    return false;
            }

            // Vérifier les files d'attente des continents
            foreach (MyQueue<Lettre> file in FilesAttenteContinents)
            {
                if (!file.IsEmpty())
                    return false;
            }

            // Vérifier les elfes et traîneaux
            foreach (Elfe elfe in Elfes)
            {
                if (elfe.Traineau.EnVoyage || !elfe.Traineau.Lettres.IsEmpty())
                    return false;
            }

            return true;
        }

        public void AvancerJusquaJourSuivant()
        {
            int heureDansJour = HeureActuelle % 12;
            int heuresRestantes = 12 - heureDansJour;
            if (heuresRestantes == 12)
            {
                heuresRestantes = 0;
            }

            for (int i = 0; i < heuresRestantes; i++)
            {
                AvancerUneHeure();
            }
            
        }

        public void ModifierNombreLutins(int nouveauNombreActifs)
        {
            if ((HeureActuelle - DerniereModificationLutins) < 12)
            throw new ArgumentException("Modification lutins impossible avant 12h.");

            // Compter actifs actuels
            int actifs = 0;
            foreach (var l in Lutins)
            if (l.Statut != StatutEmploye.EnRepos) actifs++;

             if (nouveauNombreActifs == actifs) return;

            if (nouveauNombreActifs < 0 || nouveauNombreActifs > Lutins.Count)
            throw new ArgumentException("Nombre d'actifs invalide.");

            if (nouveauNombreActifs < actifs)
            {
                int aMettreEnRepos = actifs - nouveauNombreActifs;
                

               // On  met en repos tous les lutins en attente
               foreach (var l in Lutins)
                {
                    if (aMettreEnRepos == 0) break;

                    if (l.Statut == StatutEmploye.EnAttente)
                    {
                        l.Statut = StatutEmploye.EnRepos;
                        aMettreEnRepos--;
                    }
                }
                // 2) S'il en manque encore, mettre en repos des lutins en travail
                 foreach (var l in Lutins)
                {
                    if (aMettreEnRepos == 0) break;

                
                    if (l.Statut == StatutEmploye.EnTravail)
                    {
                        l.Statut = StatutEmploye.EnRepos;
                        aMettreEnRepos--;
                    }
                }
                // 3) S'il en manque toujours, alors seulement on bloque

                if (aMettreEnRepos > 0)
                throw new ArgumentException("Impossible de réduire le nombre de lutins actifs.");
            }
            else
            {
                int aReveiller = nouveauNombreActifs - actifs;

                foreach (var l in Lutins)
                {
                    if (aReveiller == 0) break;

                    if (l.Statut == StatutEmploye.EnRepos)
                    {
                        l.Statut = StatutEmploye.EnAttente;
                        aReveiller--;
                    }
                }

                if (aReveiller > 0)
                throw new ArgumentException("Pas assez de lutins en repos à rappeler.");
            }

             DerniereModificationLutins = HeureActuelle;
        }
  
        

        public void ModifierNombreNains(int nouveauNombreActifs)
        {
             // 1) Contrôle délai 24h
            if ((HeureActuelle - DerniereModificationNains) < 24)
            throw new ArgumentException("On ne peut modifier le nombre de nains deux fois en moins de 24 heures.");

             // 2) Contrôle bornes
            if (nouveauNombreActifs < 0 || nouveauNombreActifs > Nains.Count)
            throw new ArgumentException("Nombre de nains actifs invalide.");

             // 3) Compter le nombre d'actifs actuels (≠ EnRepos)
            int actifs = 0;
            foreach (Nain n in Nains)
                if (n.Statut != StatutEmploye.EnRepos)
                actifs++;

            if (nouveauNombreActifs == actifs)
            return; // rien à changer

            // 4) Diminuer : mettre en repos des nains EN ATTENTE uniquement
            if (nouveauNombreActifs < actifs)
            {
                int aMettreEnRepos = actifs - nouveauNombreActifs;

                foreach (Nain n in Nains)
                {
                    if (aMettreEnRepos == 0) break;

                    if (n.Statut == StatutEmploye.EnAttente)
                    {
                        n.Statut = StatutEmploye.EnRepos;
                        aMettreEnRepos--;
                    }
                }

                if (aMettreEnRepos > 0)
                throw new ArgumentException("Impossible : pas assez de nains en attente pour les mettre en repos.");
            }
            // 5) Augmenter : rappeler des nains EN REPOS -> EN ATTENTE
            else
            {
                int aRappeler = nouveauNombreActifs - actifs;

                foreach (Nain n in Nains)
                {
                if (aRappeler == 0) break;

                if (n.Statut == StatutEmploye.EnRepos)
                    {
                        n.Statut = StatutEmploye.EnAttente;
                        aRappeler--;
                    }
                }

                if (aRappeler > 0)
                throw new ArgumentException("Impossible : pas assez de nains en repos à rappeler.");
            }

            // 6) Mettre à jour l'heure de la dernière modif
            DerniereModificationNains = HeureActuelle;
        }


        public void AfficherIndicateurs()
        {
            Console.WriteLine("\n╔════════════════════════════════════════════════════════════╗");
            Console.WriteLine($"║  INDICATEURS - Jour {JourActuel}, Heure {HeureActuelle % 12 + 1}/12");
            Console.WriteLine("╠════════════════════════════════════════════════════════════╣");

            // Lutins - compter manuellement
            int lutinsEnTravail = 0;
            int lutinsEnAttente = 0;
            int lutinsEnRepos = 0;
            foreach (Lutin lutin in Lutins)
            {
                if (lutin.Statut == StatutEmploye.EnTravail)
                    lutinsEnTravail++;
                else if (lutin.Statut == StatutEmploye.EnAttente)
                    lutinsEnAttente++;
                else if (lutin.Statut == StatutEmploye.EnRepos)
                    lutinsEnRepos++;
            }

            Console.WriteLine("║ LUTINS:");
            Console.WriteLine($"║   • En travail: {lutinsEnTravail}");
            Console.WriteLine($"║   • En attente: {lutinsEnAttente}");
            Console.WriteLine($"║   • En repos: {lutinsEnRepos}");
            Console.WriteLine($"║   • Cadeaux en fabrication: {lutinsEnTravail}");
            Console.WriteLine($"║   • Lettres en attente: {FileAttenteFabrication.Size}");

            // Nains - compter manuellement
            int nainsEnTravail = 0;
            int nainsEnAttente = 0;
            int nainsEnRepos = 0;
            foreach (Nain nain in Nains)
            {
                if (nain.Statut == StatutEmploye.EnTravail)
                    nainsEnTravail++;
                else if (nain.Statut == StatutEmploye.EnAttente)
                    nainsEnAttente++;
                else if (nain.Statut == StatutEmploye.EnRepos)
                    nainsEnRepos++;
            }

            Console.WriteLine("║ NAINS:");
            Console.WriteLine($"║   • En travail: {nainsEnTravail}");
            Console.WriteLine($"║   • En attente: {nainsEnAttente}");
            Console.WriteLine($"║   • En repos: {nainsEnRepos}");
            Console.WriteLine($"║   • Cadeaux en emballage: {nainsEnTravail}");
            Console.WriteLine($"║   • Cadeaux en attente d'emballage: {FileAttenteEmballage.Size}");

            // Elfes et traîneaux
            Console.WriteLine("║ ELFES ET TRAÎNEAUX:");
            foreach (Elfe elfe in Elfes)
            {
                string statut = elfe.Traineau.EnVoyage ?
                    $"En voyage ({elfe.Traineau.HeuresVoyageRestantes}h restantes)" :
                    $"Au chargement ({elfe.Traineau.Lettres.Size}/{NbJouetParTraineau})";
                int enAttente = FilesAttenteContinents[(int)elfe.Continent].Size;
                Console.WriteLine($"║   • {elfe.Continent}: {statut}, En attente: {enAttente}");
            }

            // Coûts - calculer la moyenne manuellement
            Console.WriteLine("║ COÛTS:");
            Console.WriteLine($"║   • Coût total: {CoutTotal:F2} pièces d'or");

            double coutMoyen = 0;
            if (CoutsParHeure.Count > 0)
            {
                double somme = 0;
                foreach (double cout in CoutsParHeure)
                {
                    somme += cout;
                }
                coutMoyen = somme / CoutsParHeure.Count;
            }
            Console.WriteLine($"║   • Coût moyen/heure: {coutMoyen:F2} pièces d'or");

            Console.WriteLine("╚════════════════════════════════════════════════════════════╝\n");
        }

        public void AfficherBilan()
        {
            Console.WriteLine("\n╔════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                     BILAN FINAL");
            Console.WriteLine("╠════════════════════════════════════════════════════════════╣");
            Console.WriteLine($"║ Durée totale: {JourActuel - 1} jours et {HeureActuelle % 12} heures");
            Console.WriteLine($"║ Coût total: {CoutTotal:F2} pièces d'or");

            // Calculer la moyenne manuellement
            double coutMoyen = 0;
            if (CoutsParHeure.Count > 0)
            {
                double somme = 0;
                foreach (double cout in CoutsParHeure)
                {
                    somme += cout;
                }
                coutMoyen = somme / CoutsParHeure.Count;
            }
            Console.WriteLine($"║ Coût moyen par heure: {coutMoyen:F2} pièces d'or");

            Console.WriteLine("╠════════════════════════════════════════════════════════════╣");
            Console.WriteLine("║ ENTREPÔTS CONTINENTAUX:");

            for (int i = 0; i < Entrepots.Length; i++)
            {
                Entrepot entrepot = Entrepots[i];
                Console.WriteLine($"║ {entrepot.Continent}:");
                Console.WriteLine($"║   Total: {entrepot.Lettres.Size} jouets");
                int[] stats = entrepot.GetStatistiques();

                // Parcourir chaque type de cadeau
                for (int j = 0; j < stats.Length; j++)
                {
                    if (stats[j] > 0)
                    {
                        TypeCadeau type = (TypeCadeau)j;
                        Console.WriteLine($"║   • {type}: {stats[j]}");
                    }
                }
            }

            Console.WriteLine("╚════════════════════════════════════════════════════════════╝\n");
        }
    }

    // ============= PROGRAMME PRINCIPAL =============

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Simulation simulation = null;
            bool continuer = true;

            while (continuer)
            {
                if (simulation == null)
                {
                    simulation = MenuInitialisation();
                }
                else
                {
                    continuer = MenuPrincipal(simulation);

                    if (continuer && simulation.ToutesLettresTraitees())
                    {
                        Console.WriteLine("\n🎄 Toutes les lettres ont été traitées! 🎄");
                        simulation.AfficherBilan();

                        Console.Write("\nVoulez-vous lancer une nouvelle simulation? (o/n): ");
                        string choix = Console.ReadLine().ToLower();
                        if (choix == "o" || choix == "oui")
                        {
                            simulation = null;
                        }
                        else
                        {
                            continuer = false;
                        }
                    }
                }
            }

            Console.WriteLine("\n🎅 Joyeux Noël! 🎄");
        }

        static Simulation MenuInitialisation()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║          BIENVENUE AU PAYS DU PÈRE NOËL                    ║");
            Console.WriteLine("║              Initialisation de la simulation               ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════╝\n");

            Console.Write("Nombre de lutins disponibles: ");
            int nbLutins = int.Parse(Console.ReadLine());

            Console.Write("Nombre de nains disponibles: ");
            int nbNains = int.Parse(Console.ReadLine());

            Console.Write("Nombre d'enfants: ");
            int nbEnfants = int.Parse(Console.ReadLine());

            Console.Write("Nombre maximum de lettres par heure: ");
            int nbLettresParHeure = int.Parse(Console.ReadLine());

            Console.Write("Capacité de chaque traîneau: ");
            int nbJouetParTraineau = int.Parse(Console.ReadLine());

            Console.WriteLine("\n✅ Simulation initialisée avec succès!");
            Console.WriteLine("Appuyez sur une touche pour continuer...");
            Console.ReadKey();

            return new Simulation(nbLutins, nbNains, nbEnfants, nbLettresParHeure, nbJouetParTraineau);
        }

        static bool MenuPrincipal(Simulation simulation)
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                 GESTION DU PÈRE NOËL                       ║");
            Console.WriteLine("╠════════════════════════════════════════════════════════════╣");
            Console.WriteLine("║ 1. Avancer d'une heure                                     ║");
            Console.WriteLine("║ 2. Avancer jusqu'au début de la journée suivante           ║");
            Console.WriteLine("║ 3. Afficher les indicateurs                                ║");
            Console.WriteLine("║ 4. Gérer les lutins                                        ║");
            Console.WriteLine("║ 5. Gérer les nains                                         ║");
            Console.WriteLine("║ 6. Visualiser les entrepôts                                ║");
            Console.WriteLine("║ 7. Afficher le bilan et continuer                          ║");
            Console.WriteLine("║ 8. Afficher le bilan et arrêter                            ║");
            Console.WriteLine("║ 9. Afficher le bilan et relancer                           ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════╝");
            Console.Write("\nVotre choix: ");

            string choix = Console.ReadLine();

            switch (choix)
            {
                case "1":
                    simulation.AvancerUneHeure();   // Cette Méthode est à écrire.
                    Console.WriteLine($"\n✅ Avancé à: Jour {simulation.JourActuel}, Heure {simulation.HeureActuelle % 12 + 1}");
                    Console.WriteLine("Appuyez sur une touche pour continuer...");
                    Console.ReadKey();
                    return true;

                case "2":
                    int heuresAvant = simulation.HeureActuelle;
                    simulation.AvancerJusquaJourSuivant();  // Cette Méthode est à écrire.
                    Console.WriteLine($"\n✅ Avancé jusqu'à: Jour {simulation.JourActuel}, Heure 1");
                    Console.WriteLine($"   ({simulation.HeureActuelle - heuresAvant} heures écoulées)");
                    Console.WriteLine("Appuyez sur une touche pour continuer...");
                    Console.ReadKey();
                    return true;

                case "3":
                    simulation.AfficherIndicateurs();
                    Console.WriteLine("Appuyez sur une touche pour continuer...");
                    Console.ReadKey();
                    return true;

                case "4":
                    MenuGestionLutins(simulation);
                    return true;

                case "5":
                    MenuGestionNains(simulation);
                    return true;

                case "6":
                    AfficherEntrepots(simulation);
                    Console.WriteLine("Appuyez sur une touche pour continuer...");
                    Console.ReadKey();
                    return true;

                case "7":
                    simulation.AfficherBilan();
                    Console.WriteLine("Appuyez sur une touche pour continuer...");
                    Console.ReadKey();
                    return true;

                case "8":
                    simulation.AfficherBilan();
                    return false;

                case "9":
                    simulation.AfficherBilan();
                    Console.WriteLine("\n🔄 Redémarrage de la simulation...");
                    Console.WriteLine("Appuyez sur une touche pour continuer...");
                    Console.ReadKey();
                    return false;

                default:
                    Console.WriteLine("❌ Choix invalide.");
                    Console.WriteLine("Appuyez sur une touche pour continuer...");
                    Console.ReadKey();
                    return true;
            }
        }

        static void MenuGestionLutins(Simulation simulation)
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                   GESTION DES LUTINS                       ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════╝\n");

            // Compter les lutins actifs
            int actifs = 0;
            foreach (Lutin lutin in simulation.Lutins)
            {
                if (lutin.Statut != StatutEmploye.EnRepos)
                    actifs++;
            }

            Console.WriteLine($"Lutins actuellement actifs: {actifs}/{simulation.NbLutins}");
            Console.WriteLine($"Dernière modification: il y a {simulation.HeureActuelle - simulation.DerniereModificationLutins} heures");
            Console.WriteLine($"(Modification possible après 12 heures)\n");

            Console.Write("Nouveau nombre de lutins actifs (ou 'c' pour annuler): ");
            string input = Console.ReadLine();

            if (input.ToLower() != "c")
            {
                if (int.TryParse(input, out int nouveau))
                {
                    simulation.ModifierNombreLutins(nouveau);   // Cette Méthode est à écrire.
                }
                else
                {
                    Console.WriteLine("❌ Nombre invalide.");
                }
            }

            Console.WriteLine("\nAppuyez sur une touche pour continuer...");
            Console.ReadKey();
        }

        static void MenuGestionNains(Simulation simulation)
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                   GESTION DES NAINS                        ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════╝\n");

            // Compter les nains actifs
            int actifs = 0;
            foreach (Nain nain in simulation.Nains)
            {
                if (nain.Statut != StatutEmploye.EnRepos)
                    actifs++;
            }

            Console.WriteLine($"Nains actuellement actifs: {actifs}/{simulation.NbNains}");
            Console.WriteLine($"Dernière modification: il y a {simulation.HeureActuelle - simulation.DerniereModificationNains} heures");
            Console.WriteLine($"(Modification possible après 24 heures)\n");

            Console.Write("Nouveau nombre de nains actifs (ou 'c' pour annuler): ");
            string input = Console.ReadLine();

            if (input.ToLower() != "c")
            {
                if (int.TryParse(input, out int nouveau))
                {
                    simulation.ModifierNombreNains(nouveau);    // Cette Méthode est à écrire.
                }
                else
                {
                    Console.WriteLine("❌ Nombre invalide.");
                }
            }

            Console.WriteLine("\nAppuyez sur une touche pour continuer...");
            Console.ReadKey();
        }

        static void AfficherEntrepots(Simulation simulation)
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║              ÉTAT DES ENTREPÔTS CONTINENTAUX               ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════╝\n");

            for (int i = 0; i < simulation.Entrepots.Length; i++)
            {
                Entrepot entrepot = simulation.Entrepots[i];
                Console.WriteLine($"═══ {entrepot.Continent} ═══");
                Console.WriteLine($"Total de jouets stockés: {entrepot.Lettres.Size}");

                int[] stats = entrepot.GetStatistiques();
                Console.WriteLine("Répartition par catégorie d'âge:");

                // Parcourir les types de cadeaux dans l'ordre de l'enum
                foreach (TypeCadeau type in Enum.GetValues(typeof(TypeCadeau)))
                {
                    int index = (int)type;
                    if (stats[index] > 0)
                    {
                        string categorie;
                        switch (type)
                        {
                            case TypeCadeau.Nounours:
                                categorie = "0-2 ans (Nounours)";
                                break;
                            case TypeCadeau.Tricycle:
                                categorie = "3-5 ans (Tricycle)";
                                break;
                            case TypeCadeau.Jumelles:
                                categorie = "6-10 ans (Jumelles)";
                                break;
                            case TypeCadeau.AbonnementGK:
                                categorie = "11-15 ans (Abonnement)";
                                break;
                            case TypeCadeau.Ordinateur:
                                categorie = "16-18 ans (Ordinateur)";
                                break;
                            default:
                                categorie = "Inconnu";
                                break;
                        }
                        Console.WriteLine($"  • {categorie}: {stats[index]}");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
