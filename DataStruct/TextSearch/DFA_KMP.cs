using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStruct.TextSearch
{

    /// <summary>
    /// 以模式串ababc为例, 其状态共0~5六种. 得状态机表如下:
    /// 其中非0非括号值即为将回退时的目的状态(即此时串的最长真前后缀, 如[4][a]状态时, 串为ababa, 最长真前后缀为3)
    ///---------------------------------
    ///|   |  0  |  1  |  2  |  3  |  4  |
    ///|---|-----|-----|-----|-----|-----|
    ///| a | [1] |  1  | [3] |  1  |  3  |
    ///|---|-----|-----|-----|-----|-----|
    ///| b |  0  | [2] |  0  | [4] |  0  |
    ///|---|-----|-----|-----|-----|-----|
    ///| c |  0  |  0  |  0  |  0  | [5] |
    ///---------------------------------
    ///
    /// </summary>
    class DFA_KMP
    {
        private string _pat;
        private int _patlen;
        private int[,] _dfa;
        private int _R;
        public DFA_KMP(string pattern)
        {
            _pat = pattern;
            _patlen = _pat.Length;
            _R = 256;//取ascii码表大小
            _dfa = new int[_patlen, _R];
            InitDfaKmp();
        }
        private void InitDfaKmp()
        {
            _dfa[0, _pat[0]] = 1;
            for (int X = 0, j = 1; j < _patlen; j++)
            {
                for (int c = 0; c < _R; c++)
                {
                    _dfa[j, c] = _dfa[X, c];
                }
                _dfa[j, _pat[j]] = j + 1;//比如上表中的[1][b], 就是状态机成功的路径节点
                X = _dfa[X, _pat[j]];//该式在4次循环中分别为: x=dfa[0,b]=0; x=dfa[0,a]=1; x=dfa[1,b]=2; x=dfa[2,c]=0;
            }
        }

        public int Search(string txt)
        {
            int i = 0, j = 0;
            int txtlen = txt.Length;
            for (; i < txtlen && j < _patlen; i++)//i不回退
            {
                j = _dfa[j, txt[i]];//更新状态机
                Form1.ActiveForm.Text += "i:" + i.ToString();
                Form1.ActiveForm.Text += "j:" + j.ToString();
                Form1.ActiveForm.Text += "   ";
            }
            return (j == _patlen) ? i - j : 0;//当j等于模式串时, 也就说明状态机已经到达终点了
        }
    }
}
///
/// 按《算法-第四版》中的代码, 其实这个图应该竖着画, 如下图。
/// 为了便于阅读, 所以我的代码dfa数组二维与书中原版代码是反的。
/// 图中R即为字母表， P即为pat串
///|\R|------------------
///|P\|  a  |  b  |  c  |
///|--|-----|-----|-----|
///|a | [1] |  0  |  0  |
///|--|-----|-----|-----|
///|b |  1  | [2] |  0  |
///|--|-----|-----|-----|
///|a | [3] |  0  |  0  |
///|--|-----|-----|-----|
///|b |  1  | [4] |  0  |
///|--|-----|-----|-----|
///|c |  3  |  0  | [5] |
///----------------------