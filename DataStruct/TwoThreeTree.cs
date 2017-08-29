using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStruct
{
    class Node<T1, T2> where T1 : IComparable
    {
        Node<T1, T2> lchild;
        Node<T1, T2> rchild;
    }
    class Node2_<T1, T2> : Node<T1, T2> where T1 : IComparable
    {
        T1 key;
    }
    class Node3_<T1, T2> : Node<T1, T2> where T1 : IComparable
    {
        T1 lkey;
        T1 rkey;
    }
    class Node4_<T1, T2> : Node<T1, T2> where T1 : IComparable
    {
        T1 lkey;
        T1 bkey;
        T1 rkey;
    }
    class TwoThreeTree<T1, T2> where T1 : IComparable
    {
        //2-3树的约束(性质): 3- and 4- 节点的键值都是按从小到大从左到右排列。
        public void Insert(Node<T1, T2> node)
        {
            /* 
             * setp 1: 按照普通二叉查找树找到这个新节点应该插入的位置.
             * 
             * setp 2: 
             *        情形a: 这个位置是个2-节点, 那直接将其变为3-节点, over。
             *        情形b: 这个位置是个3-节点, 那么:
             *          setp 2.1: 将它变为4-节点.
                        setp 2.2: 变为4-节点后, 首先判断自己是否为根节点:
                             情形 b.a: 若自己为根节点, 则将自己分解为普通二叉树, 左键与右键分别引领左右子树, 中键成为新根, over.
                             情形 b.b: 若自己非根节点, 那么查看自己的父节点:
                                  情形b.b.a: 若父节点为3-节点, 将自己的中键插入父节点. 此时父节点变为4-节点.
                                             父节点迭代步骤2.2, 直到某祖辈节点符合情形b.a或b.b.b, over.
                                  情形b.b.b: 父节点为2-节点, 那么变为3-节点, over。
             */
        }
        public void Erase(T1 key)
        {
        }
    }
}
