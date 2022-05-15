using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OW21BB
{
    public class BuildingProject<T> where T : Company
    {
        public delegate void ListingItems(T item, int number);
        public delegate void BBList(Company item, int number, int space);

        static Random rnd;

        static BuildingProject()
        {
            rnd = new Random();
        }

        public string Name { get; set; }
        public int Size { get; set; }

        public int RemainingSpace { get; set; }


        TreeItem root;
        public int CustomerCount { get; set; }

        class TreeItem
        {
            public T content;
            public string key;
            public TreeItem left;
            public TreeItem right;
        }

        public BuildingProject(string name, int size)
        {
            this.Name = name;
            this.Size = size;
            this.RemainingSpace = size;
        }

        public void BranchandBound(BBList _method, List<Company> r)
        {

            int[] E = new int[r.Count];
            int[] OPT = new int[r.Count];
            for (int i = 0; i < r.Count; i++)
            {
                E[i] = 1;
                OPT[i] = 1;
            }

            this.RemainingSpace = this.Size;

            BBList method = _method;
            _BranchandBound(0, ref E, ref OPT, r);


            int db = 1;
            for (int i = 0; i < OPT.Length; i++)
            {
                if (OPT[i] == 0)
                {
                    this.RemainingSpace -= r[i].SpaceNeeded;
                    method(r[i], db++, this.RemainingSpace);
                }
            }
        }

        void _BranchandBound(int szint, ref int[] E, ref int[] OPT, List<Company> r)
        {
            for (int i = 0; i < 2; i++)
            {
                E[szint] = i;
                if (Fk(szint, E, r))
                {
                    if (szint == r.Count - 1)
                    {
                        if (TotalPrice(szint, E, r) > TotalPrice(szint, OPT, r))
                        {
                            for (int k = 0; k < E.Length; k++)
                            {
                                OPT[k] = E[k];
                            }
                        }
                    }
                    else if (TotalPrice(szint, E, r) + Fb(szint, E, r) > TotalPrice(szint, OPT, r))
                    {
                        _BranchandBound(szint + 1, ref E, ref OPT, r);
                    }
                }
            }
        }

        int Fb(int szint, int[] E, List<Company> r)
        {
            int pfk = 0;
            for (int i = szint + 1; i < r.Count; i++)
            {
                if (TotalWeight(szint, E, r) + r[i].SpaceNeeded < this.Size)
                {
                    pfk += r[i].AmountPayed;
                }
            }

            return pfk;
        }

        bool Fk(int szint, int[] E, List<Company> r)
        {
            return TotalWeight(szint, E, r) <= this.Size;
        }

        int TotalWeight(int szint, int[] E, List<Company> r)
        {
            int sum = 0;
            for (int i = 0; i <= szint; i++)
            {
                if (E[i] == 0)
                {
                    sum += r[i].SpaceNeeded;
                }
            }

            return sum;
        }

        int TotalPrice(int szint, int[] E, List<Company> r)
        {
            int sum = 0;
            for (int i = 0; i <= szint; i++)
            {
                if (E[i] == 0)
                {
                    sum += r[i].AmountPayed;
                }
            }

            return sum;
        }


        public List<Company> Companies()
        {
            List<Company> companies = new List<Company>(CustomerCount);

            _Companies(ref companies, root);

            return companies;
        }

        void _Companies(ref List<Company> companies, TreeItem p)
        {
            if (p != null)
            {
                companies.Add(p.content);
                _Companies(ref companies, p.left);
                _Companies(ref companies, p.right);
            }
        }

        public List<string> Keys()
        {
            List<string> keys = new List<string>();

            _Keys(ref keys, root);

            return keys.Count != 0 ? keys : throw new EmptyArrayOrListException();
        }

        void _Keys(ref List<string> keys, TreeItem p)
        {
            if (p != null)
            {
                keys.Add(p.key);
                _Keys(ref keys, p.left);
                _Keys(ref keys, p.right);
            }
        }

        public void Upload(T content)
        {
            _Upload(ref root, content);
        }

        void _Upload(ref TreeItem p, T content)
        {
            if (p == null)
            {
                CustomerCount++;
                p = new TreeItem();
                p.content = content;
                p.key = content.ToString();
            }
            else
            {
                if (p.key.CompareTo(content.ToString()) < 0)
                {
                    _Upload(ref p.right, content);
                }
                else if (p.key.CompareTo(content.ToString()) > 0)
                {
                    _Upload(ref p.left, content);
                }
                else
                {
                    throw new ItemExistsException(content.ToString());
                }
            }
        }

        public void List(ListingItems _method)
        {
            int number = 1;
            if (root != null)
            {
                _List(_method, root, ref number);
            }
            else
            {
                throw new EmptyArrayOrListException();
            }
        }

        void _List(ListingItems _method, TreeItem p, ref int number)
        {
            ListingItems method = _method;
            if (p != null)
            {
                method?.Invoke(p.content, number++);
                _List(method, p.left, ref number);
                _List(method, p.right, ref number);
            }
        }

        public void Delete(string key)
        {
            _Delete(ref root, key);
        }

        void _Delete(ref TreeItem p, string key)
        {
            if (p != null)
            {
                if (p.key.CompareTo(key) > 0)
                {
                    _Delete(ref p.left, key);
                }
                else if (p.key.CompareTo(key) < 0)
                {
                    _Delete(ref p.right, key);
                }
                else
                {
                    if (p.left == null)
                    {
                        TreeItem q = p;
                        p = p.right;
                        Release(q);
                    }
                    else if (p.right == null)
                    {
                        TreeItem q = p;
                        p = p.left;
                        Release(q);
                    }
                    else
                    {
                        DeleteTwoChild(p, ref p.left);
                    }
                }
            }
            else
            {
                throw new ItemNotFound(key);
            }
        }

        void DeleteTwoChild(TreeItem e, ref TreeItem r)
        {
            if (r.right != null)
            {
                DeleteTwoChild(e, ref r.right);
            }
            else
            {
                e.content = r.content;
                e.key = r.key;
                TreeItem q = r;
                r = r.left;
                Release(q);
            }
        }

        void Release(TreeItem q)
        {
            q = null;
        }
    }
}
