using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStruct
{
    public enum RBNodeColor
    {
        NC_BLACK,
        NC_RED
    }
    public enum _23NodeType
    {
        _2NODE,
        _3NODE,
        _4NODE
    };
    class RBNode<T1, T2> where T1 : IComparable
    {
        private RBNodeColor _color;
        private RBNode<T1, T2> _parent;
        private RBNode<T1, T2> _lchild;
        private RBNode<T1, T2> _rchild;
        private T1 _key;
        private T2 _value;
        public RBNodeColor color
        {
            get { return _color; }
            set { _color = value; }
        }
        public RBNode<T1, T2> parent
        {
            get { return _parent; }
            set { _parent = value; }
        }
        public RBNode<T1, T2> lchild
        {
            get { return _lchild; }
            set { _lchild = value; }
        }
        public RBNode<T1, T2> rchild
        {
            get { return _rchild; }
            set { _rchild = value; }
        }
        public T1 key
        {
            get { return _key; }
        }
        public T2 value
        {
            get { return _value; }
            set { _value = value; }
        }
        public RBNode(T1 k, T2 v)
        {
            _key = k;
            _value = v;
            _color = RBNodeColor.NC_BLACK;
            _lchild = null;
            _rchild = null;
            _parent = null;
        }
    }
    class RedBlackTree<T1, T2> where T1 : IComparable
    {
        RBNode<T1, T2> _root;
        
        /*2-节点向3-节点的转变 或 3-节点向4-节点的转变*/
        private void LeftRotate(RBNode<T1, T2> location, RBNode<T1, T2> wait)
        {
            wait.color = location.color;
            location.color = RBNodeColor.NC_RED;

            wait.parent = location.parent;
            if (wait.parent == null)
            {
                _root = wait;
            }
            else
            {
                if (wait.parent.rchild == location)
                {
                    wait.parent.rchild = wait;
                }
                else
                {
                    wait.parent.lchild = wait;
                }
            }
            location.parent = wait;

            location.rchild = wait.lchild;
            if (location.rchild != null)
            {
                location.rchild.parent = location;
            }
            wait.lchild = location;

        }

        /*判断左旋后是否还需要右旋*/
        private bool NeedRightRotate(RBNode<T1, T2> middle)
        {
            bool need = (middle.lchild != null) && (middle.lchild.color == RBNodeColor.NC_RED) && (middle.color == RBNodeColor.NC_RED);
            return need;
        }

        /*判断右旋后是否还需要左旋*/
        private bool NeedLeftRotate(RBNode<T1, T2> middle)
        {
            bool need = (middle.parent != null);
            return need;
        }

        /*4-节点分解并褪色*/
        private void RightRotate(RBNode<T1, T2> middle)
        {
            //分解4-节点
            RBNode<T1, T2> right = middle.parent;
            middle.parent = right.parent;
            if(middle.parent == null)
            {
                _root = middle;
            }
            else
            {
                if (middle.parent.lchild == right)
                {
                    middle.parent.lchild = middle;
                }
                else
                {
                    middle.parent.rchild = middle;
                }
            }
            right.parent = middle;
            right.lchild = middle.rchild;
            if (right.lchild != null)
            {
                right.lchild.parent = right;
            }
            middle.rchild = right;
            //褪色
            middle.color = RBNodeColor.NC_BLACK;
            middle.lchild.color = RBNodeColor.NC_BLACK;
        }

        public RBNode<T1, T2> Find(T1 key)
        {
            RBNode<T1, T2> cur = _root;
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

        /*1>根据待插入节点的Key查找到它应该插入的位置*/
        private RBNode<T1, T2> FindInsLocation(T1 key)
        {
            RBNode<T1, T2> cur = _root;
            RBNode<T1, T2> parent = cur;
            while (null != cur)
            {
                int eq = key.CompareTo(cur.key);
                if (eq == 0)
                {
                    break;
                }
                parent = cur;
                cur = (eq < 0) ? cur.lchild : cur.rchild;
            }
            return parent;
        }

        public void Insert(RBNode<T1, T2> node)
        {
            //找到插入位置
            RBNode<T1, T2> location = FindInsLocation(node.key);
            if (location == null)
            {
                _root = node;
                return;
            }
            node.parent = location;
            
            //调整树姿
            if (node.key.CompareTo(location.key) > 0)
            {//如果是待插入节点的右孩子, 左旋.
                LeftRotate(location, node);
            }
            else
            {//是左孩子的话, 染色
                location.lchild = node;
                node.parent = location;
                node.color = RBNodeColor.NC_RED;
            }
            
            //取临时4-节点的中键
            RBNode<T1, T2> tmp = location.key.CompareTo(node.key) > 0 ? location : node;
            while (tmp != null)
            {
                if (NeedRightRotate(tmp))
                {
                    RightRotate(tmp);
                    if(NeedLeftRotate(tmp))
                    {
                        LeftRotate(tmp.parent, tmp);
                    }
                }
                tmp = tmp.lchild;
            }
        }

        /*返回key最小的节点*/
        private RBNode<T1, T2> min(RBNode<T1, T2> root)
        {
            RBNode<T1, T2> cur = root;
            if (cur != null)
            {
                while (cur.lchild != null)
                {
                    cur = cur.lchild;
                }
            }
            return cur;
        }
        public RBNode<T1, T2> Min()
        {
            return min(_root);
        }

        /*返回key最大的节点*/
        private RBNode<T1, T2> max(RBNode<T1, T2> root)
        {
            RBNode<T1, T2> cur = root;
            if (cur != null)
            {
                while (cur.rchild != null)
                {
                    cur = cur.rchild;
                }
            }
            return cur;
        }
        public RBNode<T1, T2> Max()
        {
            return max(_root);
        }

        public void Erase(T1 key)
        {
            RBNode<T1, T2> cur = _root;
            bool itsbottom = false;

            while (cur != null)
            {
                if(cur.lchild != null)
                {
                    if (cur.lchild.color == RBNodeColor.NC_BLACK)
                    {
                        //如果左孩儿为黑色, 那么右孩儿必然有且为黑色. 换言之, 这必然为一颗4-节点分裂的小树. => 反色则可.
                        cur.color = RBNodeColor.NC_BLACK;
                        cur.lchild.color = RBNodeColor.NC_RED;
                        cur.rchild.color = RBNodeColor.NC_RED;
                    }
                    else
                    {
                        //如果左孩儿为红色, 那么必然无右孩儿. 换言之, 这必然为一个3-节点. => 无需动作
                    }
                }
                else
                {
                    /* 进了这个判断说明:
                     * 1> 左孩儿为空的话, 那本节点必然为一个2-节点.
                     * 2> 这个节点就是待删除的节点
                     */
                    itsbottom = true;
                }

                int eq = key.CompareTo(cur.key);
                if (eq == 0)
                {
                    break;
                }
                cur = (eq < 0) ? cur.lchild : cur.rchild;
            }

            if (itsbottom)
            {
                if (cur == cur.parent.lchild)
                {
                    cur.parent.lchild = null;
                    LeftRotate(cur.parent, cur.parent.rchild);
                }
                else
                {
                    cur.parent.rchild = null;
                }
            }
            else
            {
                if (cur == cur.parent.lchild)
                {
                    cur.parent.lchild = cur.rchild;
                    cur.rchild.parent = cur.parent;
                }
                else
                {
                    cur.parent.rchild = cur.lchild;
                    cur.lchild.parent = cur.parent;
                }
                cur.lchild.color = RBNodeColor.NC_BLACK;
                LeftRotate(cur.lchild, cur.rchild);
                while(cur.parent != null && cur.parent.rchild.color == RBNodeColor.NC_RED)
                {
                    LeftRotate(cur.parent, cur.parent.rchild);
                    cur = cur.parent;
                }
            }
        }
    }
}
