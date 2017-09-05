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
            /*
             * 情形1: 2-3-树中不存在待删key, 返回失败.
             * 
             * 情形2: 待删key不为叶子节点:
             *        a> 它属于一个3-节点, 那么直接删除该键.
             *        b> 它是一个2-节点, 那么合并它所处的那一层父节点与兄弟节点, 形成4-节点, 然后删除, 留下一个3-节点.(在这个过程中会涉及底下子节点和上层父级节点的调整)
             * 
             * 
             * 情形3: 待删key属于一个叶子节点:
             * 
             *        a> 这个叶子节点为3-节点, 直接删除该键.
             *        
             *        b> 这个叶子节点为2-节点:
             *                      b.1>  若其兄弟节点为3-节点, 则下沉父节点与本节点合并, 原父节点所处位置由原父节点的后继取代
             *                      b.2>  若其兄弟节点为2-节点:
             *                                             b.2.1> 父节点非2-节点, 将 父节点中的相关键 与 兄弟节点 与 待删节点(若于树姿调整中, 则无) 合并为 一个临时4-(若于树姿调整中则为3-)节点, 然后删除待删key(若非树姿调整中).
             *                                             b.2.2> 父节点为2-节点, 将 父节点           与 兄弟节点 与 待删节点(若于树姿调整中, 则无) 合并为 一个临时4-(若于树姿调整中则为3-)节点, 
             * 
 */
        }
    }
}
