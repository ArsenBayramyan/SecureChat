using System;
using System.Collections.Generic;
using System.Text;

namespace SecureChat.Core.Helper
{
    public static class CodingDecodingHelper
    {
        public static string CodeingCaesar(this string value)
        {
            List<char> originList = new List<char>()
                        { 'A','B','C','D','E','F','G','H','I','J','K','L','M',
                                 'N','O','P','Q','R','S','T','U','V','W','X','Y','Z',' ',','};
            List<char> codeList = new List<char>() {'D','E','F','G','H','I','J','K','L','M',
                                 'N','O','P','Q','R','S','T','U','V','W','X','Y','Z','A','B','C','$','#'};
            int index = 0;
            List<char> outputList = new List<char>();
            for (int i = 0; i < value.Length; i++)
            {
                index = originList.IndexOf(value[i]);
                outputList.Add(codeList[index]);
            }
            return CodingWithMatrix(new string(outputList.ToArray()));
        }
        private static string CodingWithMatrix(string value)
        {
            int MATRIX_ROWS = Convert.ToInt32(Math.Ceiling(Math.Sqrt(value.Length)));
            string[,] matrix = new string[MATRIX_ROWS, MATRIX_ROWS];
            int inpCounter = 0;

            for (int i = 0; i < MATRIX_ROWS; i++)
            {
                for (int j = 0; j < MATRIX_ROWS; j++)
                {

                    if (inpCounter >= value.Length)
                    {
                        matrix[i, j] = "0";
                    }
                    else
                    {
                        matrix[i, j] = value[inpCounter].ToString();
                        inpCounter++;
                    }
                }
            }
            for (int i = 0; i < MATRIX_ROWS; i++)
            {
                for (int j = i; j < MATRIX_ROWS; j++)
                {
                    string c = matrix[i, j];
                    matrix[i, j] = matrix[j, i];
                    matrix[j, i] = c;
                }
            }
            string _outPut = null;
            for (int i = 0; i < MATRIX_ROWS; i++)
            {
                for (int j = 0; j < MATRIX_ROWS; j++)
                {
                    _outPut += matrix[i, j];
                }
            }
            return _outPut;
        }
        private static string DecodingCaesar(string output)
        {
            List<char> originList = new List<char>()
                        { 'A','B','C','D','E','F','G','H','I','J','K','L','M',
                                 'N','O','P','Q','R','S','T','U','V','W','X','Y','Z',' ',','};
            List<char> codeList = new List<char>() {'D','E','F','G','H','I','J','K','L','M',
                                 'N','O','P','Q','R','S','T','U','V','W','X','Y','Z','A','B','C','$','#'};
            List<char> word = new List<char>();
            int index = 0;
            for (int i = 0; i < output.Length; i++)
            {
                index = codeList.IndexOf(output[i]);
                word.Add(originList[index]);
            }
            return new string(word.ToArray());
        }
        public static string DecodingMatrix(this string value)
        {
            int MATRIX_ROWS = Convert.ToInt32(Math.Ceiling(Math.Sqrt(value.Length)));
            string[,] matrix = new string[MATRIX_ROWS, MATRIX_ROWS];
            int inpCounter = 0;
            for (int i = 0; i < MATRIX_ROWS; i++)
            {
                for (int j = 0; j < MATRIX_ROWS; j++)
                {

                    if (inpCounter >= value.Length)
                    {
                        matrix[i, j] = "0";
                    }
                    else
                    {
                        matrix[i, j] = value[inpCounter].ToString();
                        inpCounter++;
                    }
                }
            }

            for (int i = 0; i < MATRIX_ROWS; i++)
            {
                for (int j = i; j < MATRIX_ROWS; j++)
                {
                    string c = matrix[i, j];
                    matrix[i, j] = matrix[j, i];
                    matrix[j, i] = c;
                }
            }
            string outcode = null;
            for (int i = 0; i < MATRIX_ROWS; i++)
            {
                for (int j = 0; j < MATRIX_ROWS; j++)
                {
                    outcode += matrix[i, j];
                }
            }
            string decode = outcode.Substring(0, value.Length);
            return DecodingCaesar(decode);
        }
    }
}
