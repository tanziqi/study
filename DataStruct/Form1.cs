using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using DataStruct.TextSearch;
//using DataStruct.Sort;

namespace DataStruct
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
#if false //二叉查找树
            BinarySearchTree<int, string> tree = new BinarySearchTree<int, string>();
            tree.Insert(new BSTnode<int, string>(9, "tanziqi"));
            tree.Insert(new BSTnode<int, string>(6, "taoli"));
            tree.Insert(new BSTnode<int, string>(12, "yudan"));
            tree.Insert(new BSTnode<int, string>(10, "wuxiaoyan"));
            tree.Insert(new BSTnode<int, string>(16, "zhoujiahuan"));
            tree.Insert(new BSTnode<int, string>(5, "liqi"));
            tree.Insert(new BSTnode<int, string>(15, "yanglin"));
            tree.Insert(new BSTnode<int, string>(11, "wangyi"));
            //BSTnode<int, string> man = tree.Find(9);
            //BSTnode<int, string> man = tree.Ceiling(5);
            tree.Erase(11);
            tree.Erase(10);
            tree.Erase(9);
            tree.Erase(6);
            //BSTnode<int, string> man = tree.root;
            //BSTnode<int, string> man = tree.Floor(12);
            BSTnode<int, string> man = tree.Ceiling(12);
            
            //BSTnode< int, string> man = tree.Min();
            button1.Text = man.value;
#endif

#if true //红黑树
            RedBlackTree<int, string> rbtree = new RedBlackTree<int, string>();
            rbtree.Insert(new RBNode<int, string>(1, "1"));
            rbtree.Insert(new RBNode<int, string>(2, "2"));
            rbtree.Insert(new RBNode<int, string>(3, "3"));
            rbtree.Insert(new RBNode<int, string>(4, "4"));
            rbtree.Insert(new RBNode<int, string>(5, "5"));
            rbtree.Insert(new RBNode<int, string>(6, "6"));
            rbtree.Insert(new RBNode<int, string>(7, "7"));
            rbtree.Insert(new RBNode<int, string>(8, "8"));
            rbtree.Insert(new RBNode<int, string>(9, "9"));
            rbtree.Insert(new RBNode<int, string>(10, "10"));
            rbtree.Insert(new RBNode<int, string>(11, "11"));
            rbtree.Insert(new RBNode<int, string>(12, "12"));
            rbtree.Insert(new RBNode<int, string>(13, "13"));
            rbtree.Insert(new RBNode<int, string>(14, "14"));
            rbtree.Insert(new RBNode<int, string>(15, "15"));
            rbtree.Insert(new RBNode<int, string>(16, "16"));

            rbtree.Erase(2);
            //RBNode<int, string> man = rbtree.Find(6);
            //RBNode<int, string> man = rbtree.Max();
            //button1.Text = man.value;
#endif

#if false
            //rabin-karp指纹字符串查找算法
            RabinKarp rk = new RabinKarp("ziqi");
            int ret = rk.Search("woshitan09tanziqi940");
            button1.Text = ret.ToString();
#else
            //dfa版kmp算法
            DFA_KMP kmp = new DFA_KMP("abcde");
            button1.Text = kmp.Search("abcdfabcde").ToString();
#endif
#if false
            //boyer Moore
            BoyerMoore bm = new BoyerMoore("examplei");
            button1.Text = bm.Search("here is a simplei examplei").ToString();
                                    //examplei
                                            //examplei
                                             //examplei
                                                     //examplei
                                                      //examplei
//#else
            BoyerMoore bm = new BoyerMoore("0000");
            button1.Text = bm.Search("0589000c10000").ToString();
                                    //aabcaab
                                           //aabcaab
#endif
        }
    }
}
