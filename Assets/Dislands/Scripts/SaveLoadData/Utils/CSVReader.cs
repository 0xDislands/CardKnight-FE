﻿using System;
using System.Text;
using System.IO;
using System.Collections.Generic;

namespace Dislands
{
    public class CSVReader
    {
        public delegate void ReadLineDelegate(int line_index, List<string> line);

        public static void LoadFromString(string file_contents, ReadLineDelegate line_reader)
        {
            int file_length = file_contents.Length;
            // read char by char and when a , or \n, perform appropriate action
            int cur_file_index = 0; // index in the file
            List<string> cur_line = new List<string>(); // current line of data
            int cur_line_number = 0;
            StringBuilder cur_item = new StringBuilder("");
            bool inside_quotes = false; 
            while (cur_file_index < file_length)
            {
                char c = file_contents[cur_file_index++];
                switch (c)
                {
                    case '"':
                        if (!inside_quotes)
                        {
                            inside_quotes = true;
                        }
                        else
                        {
                            if (cur_file_index == file_length)
                            {
                                // end of file
                                inside_quotes = false;
                                goto case '\n';
                            }
                            else if (file_contents[cur_file_index] == '"')
                            {
                                // double quote, save one
                                cur_item.Append("\"");
                                cur_file_index++;
                            }
                            else
                            {
                                // leaving quotes section
                                inside_quotes = false;
                            }
                        }
                        break;
                    case '\r':
                        // ignore it completely
                        break;
                    case ',':
                        goto case '\n';
                    case '\n':
                        if (inside_quotes)
                        {
                            // inside quotes, this characters must be included
                            cur_item.Append(c);
                        }
                        else
                        {
                            // end of current item
                            cur_line.Add(cur_item.ToString());
                            cur_item.Length = 0;
                            if (c == '\n' || cur_file_index == file_length)
                            {
                                //Debug.Log("Finished reading current line " + cur_line_number);
                                // also end of line, call line reader
                                line_reader(cur_line_number++, cur_line);
                                cur_line.Clear();
                            }
                        }
                        break;
                    default:
                        // other cases, add char
                        cur_item.Append(c);
                        if (cur_file_index == file_length)
                        {
                            cur_line.Add(cur_item.ToString());
                            // also end of line, call line reader
                            line_reader(cur_line_number++, cur_line);
                            cur_line.Clear();
                        }
                        break;
                }
            }
        }
    }
}