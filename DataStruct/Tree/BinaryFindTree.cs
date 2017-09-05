using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStruct
{
    class BinarySearchTree<T1, T2> where T1 : IComparable
    {
        private BSTnode<T1, T2> _root;
        public BSTnode<T1, T2> root
        {
            get { return _root; }
            set { _root = value; }
        }

        /*查找指定节点 */
        public BSTnode<T1, T2> Find(T1 key)
        {
            if (null == _root)
            {
                return null;
            }
            BSTnode<T1, T2> cur = _root;
            while (null != cur)
            {
                int eq = key.CompareTo(cur.key);
                if (eq == 0)
                {
                    break;
                }
                cur = (eq < 0) ? cur.lchild : cur.rchild;
            }
            return cur;
        }

        /*插入一个节点*/
        public void Insert(BSTnode<T1, T2> node)
        {
            if (null == _root)
            {
                _root = node;
                return;
            }

            BSTnode<T1, T2> cur = _root;
            BSTnode<T1, T2> parent = null;
            while (null != cur)
            {
                parent = cur;
                int eq = node.key.CompareTo(cur.key);
                if (eq == 0)
                {
                    cur.value = node.value;
                    return;
                }
                cur = (eq < 0) ? cur.lchild : cur.rchild;
            }

            if (node.key.CompareTo(parent.key) > 0)
            {
                parent.rchild = node;
            }
            else
            {
                parent.lchild = node;
            }
            return;
        }
        
        /*删除树中给定一个节点*/
        public bool Erase(T1 key)
        {
            if (_root == null)
            {
                return true;
            }

            BSTnode<T1, T2> del = _root;
            BSTnode<T1, T2> parent = null;
            int lr = 0;
            while (del != null)
            {
                int eq = del.key.CompareTo(key);
                if (eq == 0)
                {
                    if (del.lchild == null)
                    {
                        if (lr > 0)
                        {
                            parent.lchild = del.rchild;
                        }
                        else
                        {
                            parent.rchild = del.rchild;
                        }
                    }
                    else if (del.rchild == null)
                    {
                        if (lr > 0)
                        {
                            parent.lchild = del.lchild;
                        }
                        else
                        {
                            parent.rchild = del.lchild;
                        }
                        break;
                    }
                    else
                    {
                        BSTnode<T1, T2> succeed = min(del.rchild);
                        del.rchild = deleteMin(del.rchild);
                        succeed.lchild = del.lchild;
                        succeed.rchild = del.rchild;
                        if (parent != null)
                        {
                            if (eq > 0)
                            {
                                parent.lchild = succeed;
                            }
                            else
                            {
                                parent.rchild = succeed;
                            }
                        }
                        else
                        {
                            _root = succeed;
                        }
                    }
                    break;
                }
                parent = del;
                del = (eq > 0) ? del.lchild : del.rchild;
                lr = eq;
            }
            return true;
        }

        private BSTnode<T1, T2> deleteMin(BSTnode<T1, T2> root)
        {
            BSTnode<T1, T2> m = min(root);
            if(m != root)
            {
                BSTnode<T1, T2> parent = ceiling(root, m.key);
                parent.lchild = m.rchild;
            }
            else
            {
                root = root.rchild;
            }
            return root;
        }

        private BSTnode<T1, T2> deleteMax(BSTnode<T1, T2> root)
        {
            BSTnode<T1, T2> m = max(root);
            if (m != root)
            {
                BSTnode<T1, T2> parent = floor(root, m.key);
                parent.rchild = m.lchild;
            }
            else
            {
                root = root.lchild;
            }
            return root;
        }

        /*求小于给定key值节点中键值最大的那个  (有序数组化后, key左手边的那个)*/
        public BSTnode<T1, T2> floor(BSTnode<T1, T2> root, T1 key)
        {
            BSTnode<T1, T2> parent = null;
            BSTnode<T1, T2> cur = root;
            while (cur != null)
            {
                int eq = cur.key.CompareTo(key);
                if (eq >= 0)
                {
                    cur = cur.lchild;
                }
                else
                {
                    parent = cur;
                    cur = cur.rchild;
                }
            }
            return parent;
        }
        public BSTnode<T1, T2> Floor(T1 key)
        {
            return floor(_root, key);
        }

        /*求大于给定key值节点中键值最小的那个  (有序数组化后, key右手边的那个)*/
        private BSTnode<T1, T2> ceiling(BSTnode<T1, T2> root, T1 key)
        {
            BSTnode<T1, T2> parent = null;
            BSTnode<T1, T2> cur = root;
            while (cur != null)
            {
                int eq = cur.key.CompareTo(key);
                if (eq <= 0)
                {
                    cur = cur.rchild;
                }
                else
                {
                    parent = cur;
                    cur = cur.lchild;
                }
            }
            return parent;
        }
        public BSTnode<T1, T2> Ceiling(T1 key)
        {
            return ceiling(_root, key);
        }

        /*返回key最小的节点*/
        private BSTnode<T1, T2> min(BSTnode<T1, T2> root)
        {
            BSTnode<T1, T2> cur = root;
            if (cur != null)
            {
                while (cur.lchild != null)
                {
                    cur = cur.lchild;
                }
            }
            return cur;
        }
        public BSTnode<T1, T2> Min()
        {
            return min(_root);
        }

        /*返回key最大的节点*/
        private BSTnode<T1, T2> max(BSTnode<T1, T2> root)
        {
            BSTnode<T1, T2> cur = root;
            if (cur != null)
            {
                while (cur.rchild != null)
                {
                    cur = cur.rchild;
                }
            }
            return cur;
        }
        public BSTnode<T1, T2> Max()
        {
            return max(_root);
        }
    }
    class BSTnode<T1, T2> where T1 : IComparable
    {
        private T1 _key;
        private T2 _value;
        private BSTnode<T1, T2> _lchild;
        private BSTnode<T1, T2> _rchild;
        public T1 key
        {
            get { return _key; }
            private set { _key = value; }
        }
        public T2 value
        {
            get { return _value; }
            set { _value = value; }
        }

        internal BSTnode<T1, T2> lchild
        {
            get { return _lchild; }
            set { _lchild = value; }
        }
        internal BSTnode<T1, T2> rchild
        {
            get { return _rchild; }
            set { _rchild = value; }
        }

        public BSTnode(T1 arg_key, T2 arg_value)
        {
            _key = arg_key;
            _value = arg_value;
            _lchild = null;
            _rchild = null;
        }
    }
}
