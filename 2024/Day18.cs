using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using Utils;

namespace _2024
{
    public static class Day18
    {
        public const string test_input_1 = "5,4\r\n4,2\r\n4,5\r\n3,0\r\n2,1\r\n6,3\r\n2,4\r\n1,5\r\n0,6\r\n3,3\r\n2,6\r\n5,1\r\n1,2\r\n5,5\r\n2,5\r\n6,5\r\n1,4\r\n0,4\r\n6,4\r\n1,1\r\n6,1\r\n1,0\r\n0,5\r\n1,6\r\n2,0";
        public const string input_1 = "5,17\r\n11,27\r\n21,9\r\n67,26\r\n54,57\r\n11,32\r\n10,11\r\n55,47\r\n35,20\r\n13,3\r\n23,7\r\n58,41\r\n63,68\r\n5,30\r\n59,70\r\n41,70\r\n10,23\r\n68,61\r\n11,39\r\n47,57\r\n14,5\r\n5,13\r\n35,1\r\n37,3\r\n65,45\r\n50,49\r\n67,37\r\n9,5\r\n17,12\r\n10,35\r\n39,10\r\n35,10\r\n35,22\r\n9,31\r\n11,41\r\n65,69\r\n33,15\r\n3,19\r\n69,19\r\n7,5\r\n63,54\r\n6,43\r\n14,9\r\n17,38\r\n5,33\r\n12,47\r\n32,15\r\n11,19\r\n5,35\r\n61,35\r\n41,68\r\n69,35\r\n56,59\r\n69,68\r\n69,18\r\n22,11\r\n19,13\r\n1,13\r\n10,49\r\n15,9\r\n7,34\r\n55,56\r\n25,8\r\n9,35\r\n53,57\r\n57,43\r\n1,21\r\n69,20\r\n1,12\r\n6,33\r\n45,65\r\n65,29\r\n2,39\r\n61,69\r\n67,65\r\n49,54\r\n67,31\r\n54,69\r\n29,23\r\n63,49\r\n33,21\r\n41,17\r\n11,26\r\n41,66\r\n65,51\r\n3,6\r\n66,69\r\n1,20\r\n69,36\r\n52,47\r\n42,5\r\n39,67\r\n13,43\r\n57,68\r\n16,13\r\n31,12\r\n47,66\r\n23,20\r\n7,25\r\n43,65\r\n17,21\r\n5,41\r\n1,11\r\n51,60\r\n69,42\r\n68,21\r\n28,11\r\n21,18\r\n26,9\r\n9,8\r\n3,31\r\n3,24\r\n15,10\r\n6,47\r\n63,43\r\n53,59\r\n61,55\r\n57,67\r\n69,55\r\n61,22\r\n43,54\r\n3,3\r\n3,9\r\n4,3\r\n7,39\r\n51,51\r\n49,48\r\n5,23\r\n69,54\r\n65,43\r\n7,31\r\n7,0\r\n32,1\r\n47,64\r\n39,69\r\n67,47\r\n21,21\r\n69,47\r\n15,16\r\n50,61\r\n69,22\r\n37,1\r\n26,11\r\n53,69\r\n17,17\r\n12,43\r\n68,27\r\n11,36\r\n49,47\r\n1,7\r\n47,69\r\n5,6\r\n66,49\r\n61,51\r\n18,21\r\n59,56\r\n61,63\r\n62,67\r\n23,1\r\n6,1\r\n11,18\r\n33,9\r\n33,1\r\n69,38\r\n60,53\r\n12,9\r\n51,57\r\n3,11\r\n21,5\r\n3,38\r\n45,63\r\n69,39\r\n57,60\r\n16,9\r\n15,24\r\n2,21\r\n53,44\r\n61,65\r\n49,61\r\n54,53\r\n40,5\r\n11,38\r\n7,20\r\n14,1\r\n43,67\r\n24,1\r\n65,27\r\n13,44\r\n33,10\r\n25,11\r\n67,51\r\n10,1\r\n25,15\r\n62,65\r\n9,25\r\n44,67\r\n51,47\r\n3,17\r\n12,23\r\n9,16\r\n18,23\r\n40,19\r\n36,19\r\n61,59\r\n9,11\r\n11,17\r\n47,6\r\n69,25\r\n17,11\r\n47,59\r\n64,41\r\n13,27\r\n59,49\r\n25,12\r\n15,17\r\n24,13\r\n39,9\r\n8,33\r\n39,1\r\n6,41\r\n63,58\r\n13,25\r\n58,47\r\n1,43\r\n69,50\r\n53,67\r\n69,21\r\n9,45\r\n59,67\r\n49,67\r\n65,57\r\n5,10\r\n29,7\r\n55,66\r\n14,27\r\n19,19\r\n9,37\r\n26,15\r\n63,63\r\n3,7\r\n13,17\r\n4,41\r\n67,67\r\n35,15\r\n21,31\r\n53,49\r\n41,63\r\n21,8\r\n37,19\r\n63,67\r\n16,23\r\n52,43\r\n5,27\r\n49,46\r\n8,37\r\n11,11\r\n3,23\r\n54,65\r\n28,5\r\n29,1\r\n38,17\r\n61,57\r\n45,64\r\n4,31\r\n5,7\r\n64,61\r\n7,10\r\n12,5\r\n6,3\r\n35,4\r\n33,8\r\n7,35\r\n1,15\r\n63,48\r\n47,46\r\n13,39\r\n69,44\r\n22,1\r\n18,15\r\n5,22\r\n70,27\r\n2,3\r\n45,68\r\n13,13\r\n39,16\r\n54,61\r\n7,3\r\n37,65\r\n23,13\r\n68,53\r\n65,67\r\n43,51\r\n14,41\r\n51,56\r\n51,55\r\n41,9\r\n43,70\r\n65,59\r\n60,65\r\n56,67\r\n1,31\r\n51,63\r\n56,69\r\n68,67\r\n33,11\r\n61,41\r\n24,9\r\n55,58\r\n15,19\r\n4,45\r\n3,28\r\n11,9\r\n62,49\r\n53,53\r\n28,3\r\n39,53\r\n15,1\r\n28,9\r\n9,13\r\n31,17\r\n13,42\r\n67,68\r\n3,34\r\n57,40\r\n28,23\r\n6,5\r\n59,57\r\n7,9\r\n3,43\r\n49,53\r\n44,53\r\n11,31\r\n69,59\r\n9,6\r\n5,45\r\n67,57\r\n10,25\r\n31,20\r\n9,19\r\n1,9\r\n33,66\r\n53,39\r\n53,48\r\n69,27\r\n10,3\r\n46,39\r\n45,43\r\n61,39\r\n53,46\r\n11,15\r\n31,10\r\n19,15\r\n15,36\r\n9,39\r\n46,55\r\n16,1\r\n43,69\r\n5,8\r\n19,21\r\n11,2\r\n57,48\r\n5,3\r\n47,54\r\n47,63\r\n27,27\r\n33,6\r\n27,1\r\n59,62\r\n1,1\r\n45,52\r\n59,55\r\n13,12\r\n13,4\r\n67,60\r\n55,63\r\n61,58\r\n45,51\r\n17,5\r\n51,52\r\n41,69\r\n1,5\r\n48,61\r\n31,15\r\n34,29\r\n69,41\r\n5,14\r\n56,63\r\n36,1\r\n7,26\r\n61,38\r\n48,51\r\n65,28\r\n62,31\r\n58,53\r\n50,69\r\n14,33\r\n0,43\r\n15,40\r\n19,11\r\n10,13\r\n31,1\r\n58,35\r\n13,14\r\n5,25\r\n17,3\r\n47,45\r\n8,13\r\n17,33\r\n63,40\r\n21,2\r\n35,3\r\n30,23\r\n33,19\r\n15,44\r\n33,7\r\n17,1\r\n11,7\r\n19,2\r\n19,8\r\n6,25\r\n65,44\r\n65,47\r\n33,3\r\n1,4\r\n15,11\r\n39,28\r\n63,52\r\n32,7\r\n14,3\r\n69,66\r\n41,15\r\n1,39\r\n35,26\r\n5,15\r\n64,39\r\n2,5\r\n65,55\r\n69,49\r\n55,48\r\n63,60\r\n19,24\r\n25,21\r\n1,33\r\n69,61\r\n39,5\r\n7,18\r\n9,27\r\n12,31\r\n15,13\r\n43,53\r\n59,60\r\n1,18\r\n4,19\r\n45,59\r\n60,67\r\n18,3\r\n45,60\r\n37,4\r\n65,46\r\n1,23\r\n53,43\r\n23,3\r\n16,19\r\n65,41\r\n69,23\r\n33,69\r\n5,36\r\n67,43\r\n61,46\r\n25,9\r\n9,30\r\n17,7\r\n39,24\r\n13,11\r\n49,69\r\n35,9\r\n13,37\r\n69,64\r\n4,13\r\n30,15\r\n67,59\r\n7,22\r\n66,63\r\n67,34\r\n43,57\r\n15,35\r\n64,65\r\n9,22\r\n7,15\r\n31,19\r\n10,39\r\n35,21\r\n35,5\r\n17,19\r\n29,9\r\n7,21\r\n67,46\r\n21,27\r\n43,56\r\n3,20\r\n53,47\r\n15,5\r\n67,28\r\n3,29\r\n64,55\r\n12,11\r\n54,63\r\n17,14\r\n11,23\r\n51,45\r\n49,45\r\n25,5\r\n41,21\r\n15,6\r\n57,55\r\n59,59\r\n13,1\r\n7,43\r\n36,15\r\n25,1\r\n7,37\r\n39,14\r\n69,57\r\n2,9\r\n1,14\r\n11,3\r\n10,9\r\n33,64\r\n69,69\r\n23,4\r\n4,23\r\n21,20\r\n32,25\r\n47,55\r\n57,63\r\n58,57\r\n17,16\r\n19,10\r\n11,5\r\n51,70\r\n66,19\r\n22,5\r\n41,14\r\n1,35\r\n48,67\r\n25,7\r\n15,15\r\n15,18\r\n63,59\r\n67,38\r\n49,59\r\n11,43\r\n14,17\r\n17,9\r\n60,41\r\n56,57\r\n57,69\r\n47,56\r\n67,41\r\n47,65\r\n15,30\r\n69,63\r\n68,49\r\n15,43\r\n12,15\r\n3,45\r\n65,24\r\n1,8\r\n39,19\r\n47,58\r\n9,43\r\n3,30\r\n61,67\r\n53,68\r\n41,18\r\n51,42\r\n50,45\r\n13,38\r\n41,19\r\n63,53\r\n3,35\r\n1,34\r\n69,30\r\n45,57\r\n62,29\r\n4,11\r\n69,51\r\n30,1\r\n16,21\r\n50,65\r\n35,7\r\n59,53\r\n11,21\r\n27,3\r\n4,27\r\n63,44\r\n45,21\r\n46,51\r\n51,67\r\n60,63\r\n49,56\r\n56,43\r\n39,13\r\n9,23\r\n15,7\r\n27,9\r\n43,20\r\n27,6\r\n45,61\r\n29,61\r\n61,52\r\n51,53\r\n69,58\r\n6,37\r\n19,1\r\n29,13\r\n35,55\r\n53,61\r\n67,29\r\n1,17\r\n62,61\r\n39,23\r\n57,47\r\n67,62\r\n10,15\r\n39,2\r\n35,2\r\n67,42\r\n14,21\r\n59,45\r\n23,5\r\n11,45\r\n66,57\r\n30,19\r\n62,55\r\n69,65\r\n7,29\r\n67,45\r\n5,39\r\n52,67\r\n63,69\r\n23,32\r\n31,5\r\n13,15\r\n64,51\r\n41,61\r\n47,68\r\n5,26\r\n29,14\r\n42,51\r\n1,41\r\n9,41\r\n69,29\r\n15,3\r\n31,7\r\n51,50\r\n35,11\r\n12,17\r\n53,63\r\n21,32\r\n62,43\r\n11,1\r\n56,51\r\n65,22\r\n57,45\r\n37,9\r\n17,27\r\n9,33\r\n64,63\r\n45,67\r\n65,21\r\n3,39\r\n30,17\r\n16,7\r\n61,60\r\n65,65\r\n29,3\r\n70,31\r\n7,38\r\n20,1\r\n25,6\r\n55,50\r\n20,25\r\n28,17\r\n29,17\r\n21,1\r\n1,32\r\n69,67\r\n33,2\r\n27,7\r\n43,5\r\n24,21\r\n8,41\r\n54,43\r\n1,19\r\n55,61\r\n16,43\r\n52,59\r\n48,63\r\n34,5\r\n13,32\r\n57,41\r\n37,13\r\n42,61\r\n66,39\r\n49,63\r\n66,51\r\n1,44\r\n9,7\r\n64,57\r\n62,63\r\n1,48\r\n7,11\r\n4,33\r\n10,33\r\n53,52\r\n27,11\r\n4,7\r\n29,5\r\n1,37\r\n63,51\r\n30,9\r\n47,53\r\n35,12\r\n37,5\r\n31,13\r\n7,13\r\n44,47\r\n32,17\r\n65,66\r\n53,50\r\n53,41\r\n69,24\r\n58,67\r\n49,57\r\n12,37\r\n7,40\r\n13,8\r\n60,55\r\n69,52\r\n13,20\r\n68,57\r\n51,64\r\n67,35\r\n23,16\r\n53,45\r\n68,33\r\n67,63\r\n35,16\r\n9,42\r\n67,69\r\n1,16\r\n64,47\r\n61,49\r\n65,53\r\n21,11\r\n8,19\r\n49,39\r\n9,15\r\n47,51\r\n45,69\r\n1,29\r\n6,31\r\n7,19\r\n12,21\r\n7,23\r\n47,67\r\n2,25\r\n51,69\r\n8,31\r\n5,21\r\n29,0\r\n9,9\r\n19,17\r\n45,47\r\n33,23\r\n31,9\r\n69,31\r\n57,57\r\n59,61\r\n21,7\r\n51,54\r\n35,8\r\n4,35\r\n41,55\r\n67,48\r\n68,63\r\n33,13\r\n55,43\r\n4,39\r\n63,39\r\n21,3\r\n69,46\r\n60,47\r\n24,5\r\n53,60\r\n47,60\r\n7,41\r\n41,65\r\n9,17\r\n7,1\r\n57,54\r\n43,58\r\n41,51\r\n17,13\r\n55,42\r\n39,58\r\n41,67\r\n37,14\r\n37,67\r\n19,5\r\n28,7\r\n21,12\r\n7,8\r\n19,25\r\n69,33\r\n13,19\r\n17,41\r\n1,2\r\n19,4\r\n45,55\r\n35,17\r\n48,49\r\n66,21\r\n2,27\r\n37,6\r\n22,23\r\n18,11\r\n17,25\r\n64,69\r\n1,38\r\n67,25\r\n7,16\r\n65,42\r\n8,45\r\n3,1\r\n35,65\r\n69,37\r\n69,43\r\n65,63\r\n9,3\r\n25,18\r\n50,67\r\n32,13\r\n11,35\r\n19,12\r\n9,1\r\n67,24\r\n63,50\r\n7,12\r\n13,31\r\n4,17\r\n45,58\r\n0,7\r\n10,19\r\n67,36\r\n55,69\r\n31,11\r\n55,41\r\n1,36\r\n19,3\r\n3,27\r\n59,65\r\n57,61\r\n61,61\r\n49,65\r\n29,11\r\n37,24\r\n51,58\r\n9,21\r\n15,41\r\n61,21\r\n30,7\r\n2,15\r\n7,14\r\n27,2\r\n69,40\r\n60,35\r\n65,39\r\n56,45\r\n14,13\r\n53,40\r\n13,7\r\n62,39\r\n17,6\r\n15,25\r\n1,45\r\n22,25\r\n65,33\r\n19,18\r\n5,9\r\n57,62\r\n23,11\r\n42,65\r\n58,45\r\n59,63\r\n23,10\r\n69,45\r\n61,24\r\n5,43\r\n39,0\r\n19,9\r\n38,3\r\n58,65\r\n0,29\r\n33,17\r\n53,51\r\n55,67\r\n25,13\r\n55,65\r\n17,23\r\n49,51\r\n46,69\r\n8,3\r\n15,21\r\n39,66\r\n7,28\r\n2,11\r\n62,45\r\n29,15\r\n52,65\r\n4,1\r\n18,7\r\n63,55\r\n5,19\r\n3,42\r\n14,23\r\n13,23\r\n67,55\r\n63,65\r\n57,64\r\n66,65\r\n67,54\r\n55,59\r\n11,6\r\n7,24\r\n5,37\r\n54,37\r\n27,18\r\n65,61\r\n0,25\r\n69,56\r\n53,65\r\n67,27\r\n57,65\r\n3,5\r\n41,54\r\n25,19\r\n50,43\r\n36,7\r\n7,7\r\n34,13\r\n67,21\r\n35,13\r\n59,69\r\n60,57\r\n31,4\r\n1,27\r\n32,5\r\n26,3\r\n1,22\r\n6,17\r\n2,31\r\n9,29\r\n19,23\r\n67,30\r\n12,1\r\n43,55\r\n55,49\r\n5,11\r\n8,9\r\n63,41\r\n1,25\r\n66,15\r\n70,47\r\n59,38\r\n10,41\r\n1,40\r\n44,65\r\n16,3\r\n21,13\r\n30,3\r\n28,25\r\n24,23\r\n31,3\r\n33,25\r\n3,37\r\n27,5\r\n11,37\r\n39,15\r\n55,46\r\n1,3\r\n45,62\r\n34,15\r\n45,7\r\n62,69\r\n65,37\r\n3,25\r\n45,56\r\n67,44\r\n49,43\r\n67,23\r\n26,1\r\n5,16\r\n67,17\r\n51,41\r\n59,42\r\n2,43\r\n3,33\r\n49,41\r\n39,21\r\n51,49\r\n9,4\r\n29,21\r\n33,5\r\n3,21\r\n11,13\r\n5,1\r\n65,49\r\n63,57\r\n51,62\r\n3,41\r\n66,59\r\n67,32\r\n16,69\r\n51,0\r\n3,60\r\n69,7\r\n27,63\r\n29,55\r\n54,27\r\n37,53\r\n1,63\r\n21,23\r\n40,57\r\n27,20\r\n25,69\r\n41,13\r\n53,26\r\n43,41\r\n49,13\r\n22,29\r\n35,59\r\n56,1\r\n66,1\r\n15,54\r\n47,33\r\n49,27\r\n31,36\r\n21,29\r\n17,48\r\n45,37\r\n19,45\r\n39,26\r\n39,17\r\n33,42\r\n37,33\r\n1,55\r\n47,31\r\n7,49\r\n29,39\r\n26,21\r\n17,57\r\n47,41\r\n25,35\r\n13,55\r\n25,58\r\n18,65\r\n39,11\r\n11,69\r\n27,41\r\n37,25\r\n50,35\r\n64,33\r\n55,7\r\n43,45\r\n42,47\r\n31,31\r\n5,53\r\n15,39\r\n47,48\r\n67,11\r\n23,67\r\n17,64\r\n7,45\r\n69,10\r\n3,53\r\n12,63\r\n41,39\r\n67,6\r\n36,27\r\n19,47\r\n35,19\r\n8,47\r\n37,17\r\n55,23\r\n7,33\r\n31,49\r\n59,3\r\n46,17\r\n51,27\r\n17,56\r\n11,68\r\n40,45\r\n48,17\r\n46,3\r\n25,43\r\n43,33\r\n57,5\r\n7,59\r\n38,11\r\n36,35\r\n46,41\r\n55,11\r\n21,15\r\n53,27\r\n33,48\r\n31,58\r\n49,58\r\n29,49\r\n19,27\r\n57,8\r\n24,59\r\n48,29\r\n28,55\r\n12,61\r\n50,13\r\n63,61\r\n42,57\r\n65,16\r\n31,51\r\n59,14\r\n31,55\r\n21,41\r\n17,43\r\n31,56\r\n12,49\r\n1,65\r\n25,63\r\n35,38\r\n17,35\r\n27,57\r\n39,50\r\n25,68\r\n59,21\r\n47,20\r\n25,62\r\n67,14\r\n55,57\r\n42,3\r\n45,36\r\n59,27\r\n37,49\r\n39,55\r\n8,55\r\n40,51\r\n24,67\r\n47,3\r\n52,13\r\n35,54\r\n19,31\r\n26,39\r\n57,6\r\n25,3\r\n43,18\r\n53,33\r\n46,21\r\n55,29\r\n23,17\r\n59,33\r\n45,53\r\n45,25\r\n31,28\r\n6,49\r\n5,70\r\n56,25\r\n11,57\r\n19,58\r\n53,21\r\n49,26\r\n59,23\r\n63,23\r\n43,7\r\n43,50\r\n21,35\r\n36,55\r\n35,39\r\n59,13\r\n51,12\r\n33,37\r\n25,49\r\n20,69\r\n34,31\r\n25,53\r\n42,31\r\n5,52\r\n1,69\r\n33,31\r\n55,53\r\n51,61\r\n47,39\r\n63,9\r\n53,15\r\n2,57\r\n36,41\r\n53,23\r\n15,62\r\n57,51\r\n3,59\r\n25,27\r\n55,31\r\n63,3\r\n49,19\r\n29,25\r\n3,49\r\n4,57\r\n5,64\r\n1,53\r\n5,63\r\n5,65\r\n55,19\r\n15,60\r\n57,33\r\n27,17\r\n33,45\r\n39,48\r\n17,36\r\n41,7\r\n17,67\r\n15,31\r\n17,39\r\n40,55\r\n46,25\r\n29,46\r\n22,57\r\n37,50\r\n21,53\r\n9,55\r\n37,69\r\n35,69\r\n38,29\r\n63,12\r\n22,61\r\n40,27\r\n66,7\r\n35,29\r\n59,19\r\n61,26\r\n59,29\r\n15,64\r\n67,53\r\n60,13\r\n2,51\r\n51,31\r\n11,59\r\n59,37\r\n16,39\r\n23,56\r\n31,33\r\n30,67\r\n63,19\r\n43,11\r\n69,12\r\n57,23\r\n48,35\r\n21,36\r\n65,35\r\n41,49\r\n39,29\r\n61,3\r\n49,23\r\n43,31\r\n25,54\r\n61,31\r\n33,38\r\n60,37\r\n29,53\r\n36,65\r\n37,68\r\n7,53\r\n10,55\r\n56,21\r\n17,26\r\n29,41\r\n2,69\r\n45,2\r\n31,52\r\n44,19\r\n48,25\r\n49,40\r\n63,22\r\n39,37\r\n67,19\r\n63,33\r\n53,3\r\n7,64\r\n29,31\r\n19,63\r\n19,33\r\n23,61\r\n10,45\r\n27,59\r\n39,27\r\n35,47\r\n69,1\r\n28,65\r\n19,44\r\n47,15\r\n7,61\r\n69,13\r\n14,59\r\n43,21\r\n7,68\r\n60,17\r\n55,38\r\n19,55\r\n19,65\r\n41,16\r\n65,17\r\n65,13\r\n15,50\r\n5,5\r\n37,41\r\n59,7\r\n49,38\r\n70,5\r\n21,65\r\n4,65\r\n46,33\r\n41,42\r\n61,23\r\n47,9\r\n9,69\r\n5,46\r\n9,68\r\n65,23\r\n59,50\r\n46,11\r\n11,58\r\n53,37\r\n37,61\r\n33,53\r\n19,7\r\n61,1\r\n51,13\r\n8,51\r\n40,39\r\n43,10\r\n48,43\r\n37,58\r\n67,3\r\n23,33\r\n7,47\r\n57,25\r\n50,9\r\n21,42\r\n35,66\r\n47,24\r\n43,30\r\n63,18\r\n49,7\r\n65,9\r\n63,4\r\n22,47\r\n11,66\r\n8,65\r\n25,56\r\n39,8\r\n45,17\r\n33,61\r\n20,67\r\n15,67\r\n19,29\r\n20,61\r\n43,28\r\n13,65\r\n65,31\r\n13,57\r\n59,39\r\n31,25\r\n13,49\r\n27,26\r\n31,29\r\n62,9\r\n39,36\r\n27,13\r\n61,8\r\n11,70\r\n50,3\r\n45,31\r\n50,39\r\n31,50\r\n51,20\r\n67,39\r\n43,47\r\n57,21\r\n57,11\r\n36,43\r\n3,13\r\n23,53\r\n49,25\r\n27,25\r\n23,47\r\n69,2\r\n21,43\r\n59,28\r\n17,65\r\n43,24\r\n27,61\r\n42,15\r\n11,60\r\n45,23\r\n59,22\r\n37,23\r\n21,63\r\n22,7\r\n67,7\r\n29,27\r\n39,43\r\n65,1\r\n33,49\r\n21,59\r\n43,36\r\n61,47\r\n34,35\r\n31,23\r\n43,29\r\n5,47\r\n60,33\r\n33,55\r\n46,43\r\n17,34\r\n41,31\r\n12,67\r\n52,21\r\n53,36\r\n58,1\r\n43,12\r\n55,18\r\n64,25\r\n27,47\r\n2,47\r\n49,8\r\n5,66\r\n55,35\r\n31,30\r\n31,69\r\n21,25\r\n32,29\r\n14,39\r\n3,68\r\n20,7\r\n23,36\r\n47,49\r\n41,6\r\n55,9\r\n15,61\r\n67,4\r\n37,11\r\n21,51\r\n35,27\r\n28,21\r\n43,43\r\n18,53\r\n13,41\r\n9,51\r\n55,3\r\n24,35\r\n69,15\r\n5,60\r\n51,11\r\n25,44\r\n37,48\r\n52,33\r\n54,31\r\n15,33\r\n15,69\r\n59,41\r\n36,47\r\n67,15\r\n69,53\r\n7,67\r\n33,56\r\n13,67\r\n29,24\r\n22,65\r\n39,64\r\n21,57\r\n45,39\r\n59,32\r\n5,31\r\n37,36\r\n67,5\r\n35,45\r\n58,25\r\n40,61\r\n23,40\r\n64,37\r\n47,38\r\n23,59\r\n46,45\r\n31,27\r\n45,33\r\n10,29\r\n19,36\r\n55,55\r\n45,26\r\n52,7\r\n38,53\r\n33,62\r\n27,49\r\n35,60\r\n3,63\r\n45,3\r\n44,41\r\n31,64\r\n27,45\r\n63,25\r\n69,11\r\n25,25\r\n23,26\r\n39,45\r\n57,7\r\n23,30\r\n27,33\r\n19,51\r\n23,45\r\n54,7\r\n61,5\r\n5,59\r\n23,15\r\n25,26\r\n49,17\r\n33,54\r\n45,50\r\n67,1\r\n11,61\r\n49,34\r\n59,47\r\n35,67\r\n61,2\r\n53,5\r\n48,11\r\n67,12\r\n24,29\r\n17,60\r\n25,37\r\n37,21\r\n59,5\r\n41,59\r\n0,55\r\n35,33\r\n29,60\r\n23,63\r\n65,0\r\n56,15\r\n21,39\r\n43,60\r\n61,13\r\n9,52\r\n59,11\r\n9,49\r\n23,31\r\n22,15\r\n12,29\r\n60,21\r\n19,61\r\n7,51\r\n63,5\r\n35,50\r\n40,3\r\n41,1\r\n63,35\r\n39,63\r\n9,44\r\n37,29\r\n13,53\r\n14,69\r\n6,53\r\n61,45\r\n55,37\r\n58,19\r\n41,24\r\n31,61\r\n12,25\r\n29,52\r\n32,61\r\n63,17\r\n49,6\r\n30,43\r\n31,63\r\n33,20\r\n11,65\r\n57,27\r\n35,23\r\n27,23\r\n28,45\r\n16,53\r\n15,47\r\n23,52\r\n37,55\r\n51,33\r\n51,29\r\n39,59\r\n41,3\r\n63,37\r\n49,5\r\n37,70\r\n61,17\r\n53,35\r\n49,3\r\n41,12\r\n7,69\r\n65,19\r\n23,25\r\n40,35\r\n45,38\r\n61,33\r\n33,22\r\n1,58\r\n9,53\r\n27,67\r\n17,62\r\n69,16\r\n30,27\r\n43,6\r\n43,39\r\n49,33\r\n1,61\r\n63,30\r\n63,11\r\n39,61\r\n23,65\r\n17,63\r\n61,7\r\n21,19\r\n49,55\r\n45,13\r\n21,50\r\n19,39\r\n27,55\r\n31,26\r\n15,57\r\n33,24\r\n49,37\r\n53,34\r\n52,9\r\n41,44\r\n17,42\r\n55,25\r\n39,12\r\n11,54\r\n11,64\r\n41,57\r\n49,49\r\n3,55\r\n25,29\r\n54,21\r\n15,63\r\n39,46\r\n46,31\r\n26,69\r\n45,45\r\n53,1\r\n23,62\r\n25,59\r\n13,29\r\n21,22\r\n27,65\r\n49,32\r\n41,47\r\n39,7\r\n56,29\r\n21,28\r\n3,69\r\n43,27\r\n67,9\r\n32,47\r\n13,9\r\n34,41\r\n61,29\r\n37,27\r\n35,64\r\n65,10\r\n25,45\r\n66,53\r\n55,21\r\n55,15\r\n65,5\r\n61,11\r\n20,47\r\n41,27\r\n45,4\r\n14,47\r\n51,19\r\n32,39\r\n11,25\r\n23,69\r\n59,4\r\n15,49\r\n41,5\r\n19,70\r\n51,35\r\n44,23\r\n39,3\r\n25,31\r\n1,49\r\n15,34\r\n24,65\r\n51,59\r\n47,27\r\n1,67\r\n43,23\r\n10,47\r\n56,3\r\n57,3\r\n27,14\r\n1,54\r\n33,67\r\n57,19\r\n43,9\r\n9,60\r\n32,35\r\n41,35\r\n15,26\r\n21,33\r\n47,10\r\n29,57\r\n26,45\r\n39,20\r\n47,23\r\n33,33\r\n0,51\r\n23,34\r\n51,36\r\n41,37\r\n9,59\r\n9,67\r\n41,64\r\n57,28\r\n54,1\r\n63,21\r\n26,23\r\n41,26\r\n53,4\r\n37,56\r\n19,66\r\n29,43\r\n25,60\r\n34,51\r\n18,29\r\n69,5\r\n27,53\r\n56,13\r\n47,22\r\n45,41\r\n27,69\r\n39,41\r\n37,7\r\n31,57\r\n27,35\r\n27,43\r\n64,7\r\n51,9\r\n2,65\r\n45,35\r\n61,53\r\n50,25\r\n7,63\r\n35,41\r\n57,35\r\n15,53\r\n30,55\r\n23,18\r\n48,41\r\n37,15\r\n33,63\r\n31,39\r\n47,61\r\n63,34\r\n7,58\r\n41,30\r\n47,11\r\n28,41\r\n23,38\r\n11,20\r\n23,41\r\n24,51\r\n32,43\r\n43,40\r\n4,49\r\n29,32\r\n38,9\r\n27,32\r\n45,16\r\n43,49\r\n49,11\r\n43,61\r\n31,45\r\n65,4\r\n47,29\r\n8,49\r\n5,58\r\n37,47\r\n23,21\r\n17,55\r\n27,34\r\n13,54\r\n50,17\r\n51,37\r\n39,25\r\n19,53\r\n17,30\r\n21,40\r\n63,13\r\n57,12\r\n39,68\r\n11,55\r\n51,5\r\n42,35\r\n57,36\r\n21,69\r\n7,17\r\n55,24\r\n13,35\r\n37,40\r\n55,4\r\n41,23\r\n5,51\r\n13,61\r\n50,29\r\n31,65\r\n29,59\r\n39,47\r\n9,65\r\n23,37\r\n13,46\r\n29,45\r\n1,57\r\n39,33\r\n69,3\r\n37,60\r\n43,17\r\n17,59\r\n20,29\r\n28,63\r\n27,37\r\n23,19\r\n29,69\r\n25,23\r\n14,57\r\n64,9\r\n53,55\r\n21,49\r\n49,29\r\n55,40\r\n5,54\r\n21,37\r\n61,42\r\n11,49\r\n1,47\r\n58,11\r\n41,11\r\n29,47\r\n53,29\r\n52,31\r\n31,35\r\n27,56\r\n30,63\r\n13,47\r\n39,35\r\n27,66\r\n23,29\r\n33,35\r\n19,57\r\n3,56\r\n4,69\r\n35,44\r\n37,38\r\n29,33\r\n17,51\r\n49,15\r\n23,48\r\n54,13\r\n64,31\r\n51,39\r\n17,15\r\n25,52\r\n34,47\r\n39,30\r\n3,57\r\n33,58\r\n25,42\r\n13,33\r\n33,41\r\n31,62\r\n44,5\r\n35,57\r\n25,47\r\n55,5\r\n43,44\r\n7,60\r\n53,31\r\n47,14\r\n65,7\r\n1,66\r\n17,37\r\n37,43\r\n43,22\r\n43,26\r\n17,53\r\n14,67\r\n56,23\r\n55,20\r\n29,40\r\n62,1\r\n63,15\r\n29,65\r\n32,51\r\n51,7\r\n63,47\r\n1,59\r\n11,53\r\n35,52\r\n53,19\r\n24,37\r\n17,32\r\n29,67\r\n12,57\r\n31,44\r\n37,26\r\n24,43\r\n31,32\r\n49,21\r\n39,65\r\n15,45\r\n22,67\r\n37,63\r\n16,45\r\n23,55\r\n63,27\r\n11,63\r\n51,15\r\n33,57\r\n9,63\r\n20,45\r\n14,29\r\n26,51\r\n43,15\r\n43,38\r\n47,4\r\n30,41\r\n52,39\r\n37,62\r\n46,35\r\n39,32\r\n53,25\r\n3,67\r\n28,69\r\n23,27\r\n9,61\r\n38,45\r\n45,28\r\n44,9\r\n66,31\r\n5,29\r\n43,59\r\n37,35\r\n50,31\r\n19,49\r\n37,31\r\n59,20\r\n57,15\r\n19,26\r\n25,61\r\n63,36\r\n63,31\r\n17,61\r\n33,47\r\n11,51\r\n26,49\r\n15,59\r\n16,51\r\n25,30\r\n38,41\r\n43,32\r\n35,58\r\n49,2\r\n60,29\r\n31,60\r\n51,25\r\n10,67\r\n57,31\r\n59,1\r\n13,69\r\n51,1\r\n26,65\r\n10,59\r\n27,28\r\n17,69\r\n43,42\r\n31,59\r\n3,65\r\n20,63\r\n49,31\r\n20,15\r\n47,0\r\n35,43\r\n21,55\r\n57,16\r\n37,44\r\n45,19\r\n18,33\r\n25,51\r\n3,51\r\n49,18\r\n19,40\r\n13,62\r\n45,27\r\n25,33\r\n21,52\r\n56,11\r\n67,13\r\n49,35\r\n67,61\r\n55,10\r\n55,1\r\n53,54\r\n61,15\r\n43,25\r\n31,53\r\n19,54\r\n7,50\r\n25,55\r\n49,1\r\n36,31\r\n41,43\r\n8,57\r\n37,51\r\n27,29\r\n39,39\r\n33,59\r\n22,69\r\n53,13\r\n35,25\r\n64,15\r\n27,15\r\n40,53\r\n20,17\r\n50,23\r\n65,26\r\n13,56\r\n62,3\r\n17,68\r\n63,6\r\n29,68\r\n25,39\r\n54,35\r\n66,35\r\n61,6\r\n33,18\r\n34,45\r\n41,46\r\n11,28\r\n54,23\r\n41,38\r\n27,54\r\n45,9\r\n43,63\r\n11,52\r\n69,9\r\n47,7\r\n17,31\r\n45,1\r\n53,2\r\n61,16\r\n27,19\r\n51,17\r\n23,43\r\n7,55\r\n44,33\r\n43,19\r\n27,21\r\n35,68\r\n13,63\r\n47,8\r\n35,49\r\n15,65\r\n51,10\r\n25,24\r\n13,51\r\n61,10\r\n36,11\r\n16,57\r\n35,63\r\n57,30\r\n31,66\r\n23,51\r\n26,59\r\n19,67\r\n35,61\r\n26,37\r\n57,59\r\n31,47\r\n68,9\r\n20,55\r\n64,19\r\n23,23\r\n45,8\r\n55,8\r\n5,49\r\n53,17\r\n31,22\r\n43,3\r\n45,5\r\n35,31\r\n24,47\r\n11,50\r\n55,51\r\n21,47\r\n47,13\r\n34,39\r\n48,13\r\n63,29\r\n17,45\r\n47,28\r\n38,65\r\n52,25\r\n39,51\r\n7,66\r\n47,43\r\n65,15\r\n47,17\r\n67,33\r\n34,57\r\n55,13\r\n41,10\r\n15,29\r\n31,43\r\n55,45\r\n29,37\r\n47,47\r\n40,59\r\n31,67\r\n18,51\r\n13,45\r\n37,22\r\n59,31\r\n21,45\r\n14,35\r\n29,48\r\n9,62\r\n13,52\r\n45,18\r\n25,65\r\n25,16\r\n22,55\r\n39,22\r\n42,21\r\n58,15\r\n22,13\r\n35,53\r\n19,59\r\n28,29\r\n18,49\r\n57,38\r\n17,66\r\n9,57\r\n58,7\r\n19,62\r\n21,61\r\n50,21\r\n18,41\r\n37,52\r\n23,35\r\n1,51\r\n54,15\r\n39,62\r\n25,64\r\n52,17\r\n14,49\r\n63,28\r\n57,1\r\n53,18\r\n41,34\r\n65,3\r\n31,21\r\n19,43\r\n57,26\r\n17,29\r\n47,35\r\n57,17\r\n20,31\r\n33,43\r\n65,25\r\n19,37\r\n52,5\r\n30,33\r\n20,35\r\n28,37\r\n21,17\r\n53,7\r\n27,42\r\n28,13\r\n57,9\r\n23,46\r\n6,67\r\n28,49\r\n31,37\r\n45,12\r\n16,47\r\n35,36\r\n53,30\r\n15,37\r\n63,45\r\n57,18\r\n37,59\r\n36,29\r\n21,67\r\n34,69\r\n29,19\r\n63,1\r\n39,57\r\n7,62\r\n3,61\r\n22,43\r\n32,69\r\n23,39\r\n19,41\r\n20,51\r\n59,17\r\n50,5\r\n60,25\r\n43,37\r\n7,44\r\n67,18\r\n5,69\r\n63,20\r\n59,25\r\n61,18\r\n57,13\r\n53,16\r\n35,24\r\n47,21\r\n48,21\r\n35,37\r\n29,50\r\n41,40\r\n45,29\r\n5,57\r\n45,14\r\n57,29\r\n41,25\r\n61,37\r\n7,57\r\n61,9\r\n43,13\r\n49,52\r\n69,8\r\n29,38\r\n23,9\r\n15,27\r\n47,5\r\n33,65\r\n56,53\r\n18,19\r\n39,31\r\n5,67\r\n69,17\r\n55,33\r\n32,33\r\n29,63\r\n1,64\r\n51,65\r\n35,35\r\n51,43\r\n29,58\r\n22,59\r\n2,53\r\n41,53\r\n37,37\r\n59,51\r\n17,28\r\n23,49\r\n47,19\r\n5,62\r\n31,46\r\n53,9\r\n17,49\r\n25,67\r\n33,29\r\n43,62\r\n68,13\r\n53,10\r\n27,39\r\n7,27\r\n17,47\r\n59,15\r\n16,27\r\n61,27\r\n33,26\r\n43,2\r\n52,1\r\n11,67\r\n33,39\r\n47,25\r\n35,51\r\n31,41\r\n19,69\r\n61,43\r\n53,11\r\n41,45\r\n18,45\r\n21,34\r\n51,14\r\n68,5\r\n28,53\r\n4,55\r\n61,19\r\n53,28\r\n47,1\r\n18,47\r\n3,46\r\n19,38\r\n41,29\r\n27,30\r\n11,47\r\n64,13\r\n25,40\r\n43,35\r\n23,50\r\n62,15\r\n39,49\r\n42,1\r\n57,37\r\n41,8\r\n24,15\r\n45,30\r\n66,11\r\n42,49\r\n50,27\r\n45,11\r\n29,44\r\n3,50\r\n68,1\r\n56,5\r\n28,59\r\n13,5\r\n15,23\r\n57,53\r\n33,51\r\n13,59\r\n55,17\r\n60,11\r\n27,31\r\n16,59\r\n57,39\r\n63,7\r\n18,57\r\n21,54\r\n19,35\r\n2,61\r\n57,49\r\n62,19\r\n65,11\r\n5,55\r\n5,61\r\n29,34\r\n41,33\r\n21,38\r\n30,69\r\n65,2\r\n9,26\r\n60,3\r\n59,35\r\n51,23\r\n52,23\r\n60,9\r\n59,30\r\n7,65\r\n59,48\r\n67,49\r\n57,44\r\n62,13\r\n37,39\r\n19,60\r\n3,62\r\n31,40\r\n51,3\r\n9,47\r\n62,25\r\n57,34\r\n45,48\r\n26,33\r\n60,51\r\n33,27\r\n3,15\r\n35,62\r\n37,18\r\n35,32\r\n37,57\r\n43,1\r\n14,65\r\n9,56\r\n51,21\r\n29,29\r\n27,62\r\n21,64\r\n37,45\r\n61,25\r\n25,17\r\n45,49\r\n35,34\r\n23,57\r\n51,38\r\n27,51\r\n0,61\r\n38,55\r\n11,33\r\n49,16\r\n49,30\r\n3,47\r\n25,57\r\n55,39\r\n39,42\r\n44,45\r\n15,51\r\n26,47\r\n38,33\r\n57,32\r\n15,55\r\n27,36\r\n39,34\r\n59,6\r\n41,41\r\n49,9\r\n48,3\r\n47,37\r\n59,43\r\n59,9\r\n62,35\r\n11,29\r\n29,51\r\n43,14\r\n25,41\r\n29,35\r\n45,15\r\n13,21\r\n55,32\r\n55,27\r\n32,38\r\n2,8\r\n24,44\r\n38,66\r\n56,2\r\n14,8\r\n1,56\r\n34,64\r\n62,70\r\n66,52\r\n66,43\r\n52,68\r\n2,18\r\n52,60\r\n19,56\r\n53,42\r\n26,2\r\n8,15\r\n6,13\r\n50,28\r\n53,20\r\n26,43\r\n10,62\r\n37,30\r\n8,42\r\n56,49\r\n44,7\r\n38,58\r\n38,60\r\n62,60\r\n0,50\r\n4,53\r\n67,2\r\n54,66\r\n6,18\r\n52,0\r\n37,66\r\n14,50\r\n22,49\r\n30,44\r\n24,58\r\n27,52\r\n20,21\r\n14,66\r\n23,44\r\n46,4\r\n21,48\r\n38,64\r\n18,42\r\n50,55\r\n56,27\r\n4,8\r\n18,43\r\n12,68\r\n2,54\r\n45,54\r\n12,62\r\n4,66\r\n13,68\r\n6,4\r\n41,56\r\n8,44\r\n5,24\r\n47,50\r\n30,37\r\n70,40\r\n22,66\r\n17,46\r\n12,42\r\n7,36\r\n8,58\r\n26,24\r\n28,16\r\n28,47\r\n24,49\r\n45,24\r\n55,26\r\n33,36\r\n30,70\r\n12,35\r\n27,24\r\n23,64\r\n2,40\r\n0,21\r\n15,2\r\n64,8\r\n54,5\r\n19,46\r\n6,48\r\n26,31\r\n22,58\r\n20,32\r\n36,25\r\n68,31\r\n53,24\r\n20,19\r\n54,45\r\n12,24\r\n30,11\r\n16,66\r\n36,10\r\n49,62\r\n61,70\r\n50,22\r\n62,5\r\n3,54\r\n52,70\r\n64,2\r\n30,22\r\n20,59\r\n61,32\r\n62,44\r\n6,38\r\n33,50\r\n10,40\r\n36,33\r\n6,54\r\n42,22\r\n44,25\r\n44,26\r\n2,24\r\n38,46\r\n42,39\r\n52,61\r\n4,60\r\n26,29\r\n18,66\r\n13,36\r\n34,46\r\n24,38\r\n10,70\r\n47,70\r\n67,50\r\n42,16\r\n65,8\r\n8,48\r\n58,10\r\n8,64\r\n34,52\r\n58,20\r\n36,21\r\n46,40\r\n8,38\r\n70,42\r\n8,46\r\n0,49\r\n28,68\r\n20,2\r\n18,38\r\n33,46\r\n4,62\r\n53,12\r\n2,30\r\n21,26\r\n12,13\r\n48,22\r\n56,20\r\n52,69\r\n20,30\r\n30,16\r\n30,61\r\n8,24\r\n28,6\r\n64,17\r\n32,0\r\n30,35\r\n69,70\r\n54,22\r\n0,34\r\n19,48\r\n66,5\r\n40,67\r\n49,20\r\n46,18\r\n45,20\r\n38,61\r\n36,24\r\n44,55\r\n25,46\r\n68,10\r\n33,68\r\n56,0\r\n9,20\r\n10,64\r\n34,37\r\n0,22\r\n0,1\r\n8,60\r\n4,0\r\n56,32\r\n50,63\r\n24,40\r\n66,47\r\n37,34\r\n54,50\r\n57,58\r\n67,8\r\n32,59\r\n20,43\r\n5,0\r\n68,2\r\n50,46\r\n42,52\r\n56,41\r\n23,24\r\n14,2\r\n23,22\r\n30,31\r\n16,44\r\n34,44\r\n48,42\r\n10,8\r\n36,44\r\n30,59\r\n2,22\r\n53,32\r\n48,26\r\n38,31\r\n17,22\r\n8,32\r\n1,24\r\n68,7\r\n40,2\r\n12,66\r\n16,56\r\n45,46\r\n11,24\r\n59,44\r\n54,8\r\n54,49\r\n29,66\r\n29,22\r\n57,4\r\n46,27\r\n62,57\r\n23,68\r\n30,66\r\n56,52\r\n70,53\r\n30,48\r\n26,48\r\n54,33\r\n23,28\r\n34,70\r\n38,59\r\n10,58\r\n34,34\r\n18,24\r\n34,33\r\n48,70\r\n32,52\r\n14,16\r\n10,37\r\n8,63\r\n13,22\r\n11,56\r\n8,6\r\n44,59\r\n18,10\r\n30,68\r\n14,60\r\n35,46\r\n52,30\r\n12,4\r\n35,42\r\n65,52\r\n16,37\r\n60,70\r\n7,54\r\n61,56\r\n15,48\r\n31,68\r\n18,34\r\n14,20\r\n0,54\r\n46,30\r\n41,52\r\n68,6\r\n48,36\r\n11,22\r\n15,52\r\n1,52\r\n60,43\r\n8,8\r\n42,25\r\n46,44\r\n70,44\r\n43,34\r\n27,64\r\n4,54\r\n6,24\r\n42,63\r\n24,12\r\n14,19\r\n11,42\r\n24,45\r\n46,9\r\n66,10\r\n44,40\r\n26,61\r\n36,37\r\n58,8\r\n16,38\r\n22,38\r\n57,20\r\n32,20\r\n60,4\r\n68,62\r\n56,47\r\n19,68\r\n11,14\r\n12,58\r\n46,63\r\n54,70\r\n57,70\r\n25,34\r\n14,52\r\n4,9\r\n24,54\r\n54,17\r\n54,0\r\n19,50\r\n36,40\r\n4,47\r\n18,35\r\n18,58\r\n42,19\r\n46,10\r\n0,23\r\n51,66\r\n16,25\r\n40,46\r\n25,66\r\n56,22\r\n4,28\r\n54,47\r\n9,32\r\n36,56\r\n28,60\r\n36,38\r\n48,19\r\n6,68\r\n11,62\r\n38,63\r\n70,33\r\n28,22\r\n10,60\r\n48,52\r\n40,31\r\n46,52\r\n16,64\r\n32,44\r\n44,68\r\n11,4\r\n22,44\r\n30,49\r\n9,54\r\n33,16\r\n2,37\r\n8,4\r\n17,58\r\n30,47\r\n23,42\r\n20,20\r\n54,6\r\n0,17\r\n32,57\r\n1,68\r\n54,28\r\n58,6\r\n14,24\r\n15,12\r\n52,6\r\n30,24\r\n24,20\r\n36,53\r\n36,61\r\n0,48\r\n2,67\r\n70,8\r\n54,42\r\n19,22\r\n54,38\r\n12,6\r\n15,22\r\n70,51\r\n5,34\r\n56,38\r\n4,30\r\n66,70\r\n58,4\r\n20,70\r\n66,54\r\n68,52\r\n6,44\r\n32,42\r\n49,66\r\n4,48\r\n9,70\r\n10,14\r\n32,27\r\n38,56\r\n56,39\r\n42,27\r\n64,52\r\n29,64\r\n42,8\r\n56,4\r\n68,0\r\n1,42\r\n20,66\r\n36,50\r\n68,28\r\n44,49\r\n48,32\r\n8,59\r\n10,21\r\n9,66\r\n0,67\r\n34,50\r\n52,55\r\n12,45\r\n26,63\r\n54,25\r\n25,2\r\n49,36\r\n56,18\r\n26,50\r\n38,8\r\n42,43\r\n0,44\r\n42,67\r\n7,4\r\n12,26\r\n14,6\r\n68,11\r\n40,58\r\n52,40\r\n0,24\r\n70,58\r\n41,58\r\n14,32\r\n24,50\r\n33,44\r\n36,34\r\n14,12\r\n18,12\r\n29,36\r\n32,66\r\n48,24\r\n26,52\r\n38,47\r\n26,54\r\n55,36\r\n2,59\r\n44,61\r\n60,42\r\n6,22\r\n36,39\r\n5,12\r\n24,19\r\n57,0\r\n40,21\r\n22,22\r\n15,4\r\n51,8\r\n15,8\r\n24,17\r\n2,56\r\n27,10\r\n68,56\r\n42,9\r\n53,6\r\n3,66\r\n4,38\r\n32,56\r\n68,38\r\n16,31\r\n70,9\r\n8,36\r\n32,34\r\n50,20\r\n63,2\r\n64,11\r\n60,49\r\n40,62\r\n6,40\r\n14,7\r\n35,14\r\n18,2\r\n12,18\r\n5,28\r\n18,40\r\n36,58\r\n34,10\r\n60,32\r\n5,20\r\n45,40\r\n18,36\r\n18,16\r\n28,64\r\n55,14\r\n61,0\r\n38,10\r\n10,5\r\n38,36\r\n4,20\r\n4,22\r\n50,52\r\n20,65\r\n70,19\r\n62,36\r\n57,42\r\n38,54\r\n68,16\r\n55,6\r\n16,6\r\n6,30\r\n36,66\r\n47,34\r\n70,17\r\n30,2\r\n46,56\r\n12,10\r\n16,11\r\n7,42\r\n53,66\r\n16,52\r\n56,19\r\n28,38\r\n16,60\r\n32,46\r\n34,4\r\n44,39\r\n6,63\r\n2,46\r\n48,1\r\n52,11\r\n3,4\r\n44,50\r\n58,31\r\n4,67\r\n14,54\r\n32,23\r\n56,64\r\n36,48\r\n36,62\r\n20,28\r\n66,33\r\n18,64\r\n32,45\r\n6,35\r\n42,66\r\n60,54\r\n40,42\r\n6,21\r\n70,13\r\n50,36\r\n40,16\r\n64,23\r\n6,69\r\n42,54\r\n60,22\r\n49,0\r\n52,32\r\n50,44\r\n64,27\r\n0,60\r\n36,54\r\n24,4\r\n55,12\r\n2,66\r\n48,7\r\n70,28\r\n18,13\r\n70,52\r\n6,26\r\n60,69\r\n26,44\r\n35,56\r\n53,14\r\n56,66\r\n15,20\r\n70,25\r\n0,5\r\n22,50\r\n22,45\r\n65,34\r\n54,67\r\n30,45\r\n61,12\r\n8,34\r\n9,64\r\n24,18\r\n20,18\r\n34,67\r\n15,56\r\n2,13\r\n17,10\r\n26,35\r\n21,62\r\n20,60\r\n4,68\r\n70,67\r\n26,62\r\n52,4\r\n3,58\r\n6,8\r\n53,38\r\n55,16\r\n55,60\r\n33,52\r\n66,0\r\n48,33\r\n51,30\r\n60,12\r\n6,16\r\n67,40\r\n30,62\r\n12,3\r\n14,34\r\n40,47\r\n7,6\r\n52,26\r\n22,24\r\n34,66\r\n43,68\r\n42,32\r\n50,40\r\n5,68\r\n28,36\r\n22,21\r\n16,26\r\n40,6\r\n31,54\r\n40,14\r\n28,58\r\n36,6\r\n36,60\r\n65,12\r\n19,42\r\n38,25\r\n18,63\r\n22,51\r\n54,58\r\n6,50\r\n34,53\r\n20,50\r\n0,31\r\n20,38\r\n10,52\r\n38,51\r\n65,58\r\n25,4\r\n70,50\r\n0,2\r\n10,20\r\n54,16\r\n70,66\r\n51,2\r\n50,19\r\n6,39\r\n20,14\r\n4,37\r\n45,22\r\n3,2\r\n16,0\r\n54,2\r\n28,2\r\n68,19\r\n20,57\r\n29,8\r\n14,68\r\n38,50\r\n2,42\r\n5,50\r\n22,62\r\n36,23\r\n67,22\r\n40,17\r\n19,32\r\n58,68\r\n26,14\r\n61,36\r\n65,70\r\n30,5\r\n8,68\r\n70,69\r\n28,18\r\n58,3\r\n51,16\r\n50,54\r\n43,48\r\n8,21\r\n64,6\r\n0,45\r\n33,0\r\n41,36\r\n46,34\r\n1,46\r\n34,2\r\n20,40\r\n40,32\r\n34,61\r\n47,18\r\n65,50\r\n20,0\r\n64,36\r\n54,64\r\n50,33\r\n37,10\r\n0,32\r\n56,30\r\n21,16\r\n38,16\r\n50,14\r\n9,14\r\n54,39\r\n4,58\r\n10,2\r\n48,8\r\n43,4\r\n58,21\r\n47,44\r\n56,58\r\n20,39\r\n4,34\r\n36,57\r\n44,8\r\n48,57\r\n5,2\r\n50,48\r\n36,68\r\n48,27\r\n70,45\r\n59,2\r\n2,19\r\n62,37\r\n65,40\r\n33,4\r\n10,61\r\n48,58\r\n42,34\r\n42,42\r\n16,29\r\n40,18\r\n25,22\r\n18,26\r\n48,4\r\n48,20\r\n34,63\r\n21,24\r\n16,70\r\n42,69\r\n47,16\r\n41,60\r\n68,32\r\n62,21\r\n36,51\r\n60,68\r\n68,12\r\n44,2\r\n4,5\r\n44,16\r\n23,60\r\n38,35\r\n12,70\r\n48,56\r\n46,22\r\n2,28\r\n24,53\r\n60,39\r\n6,11\r\n56,12\r\n66,4\r\n66,26\r\n46,32\r\n55,52\r\n18,14\r\n38,32\r\n38,15\r\n57,50\r\n37,54\r\n16,68\r\n63,32\r\n29,28\r\n46,62\r\n10,27\r\n7,48\r\n45,42\r\n60,0\r\n24,34\r\n62,4\r\n38,34\r\n42,60\r\n12,60\r\n52,42\r\n67,10\r\n22,17\r\n30,30\r\n26,22\r\n62,38\r\n44,36\r\n0,13\r\n46,19\r\n58,18\r\n42,18\r\n6,62\r\n44,31\r\n50,37\r\n49,70\r\n68,42\r\n2,68\r\n68,47\r\n41,28\r\n6,61\r\n22,53\r\n36,69\r\n32,21\r\n2,64\r\n64,50\r\n56,16\r\n32,58\r\n54,56\r\n70,39\r\n60,59\r\n64,18\r\n22,20\r\n47,52\r\n68,65\r\n62,56\r\n2,63\r\n52,34\r\n34,16\r\n20,13\r\n26,7\r\n10,42\r\n46,14\r\n27,48\r\n10,54\r\n52,48\r\n70,10\r\n59,36\r\n12,27\r\n58,54\r\n68,66\r\n24,60\r\n4,4\r\n22,16\r\n40,64\r\n6,29\r\n60,19\r\n26,20\r\n32,62\r\n32,26\r\n13,24\r\n38,44\r\n24,39\r\n59,12\r\n34,19\r\n6,45\r\n19,16\r\n30,8\r\n5,56\r\n24,14\r\n7,32\r\n55,34\r\n36,28\r\n10,32\r\n45,32\r\n40,37\r\n6,7\r\n40,66\r\n44,0\r\n26,57\r\n52,28\r\n16,67\r\n38,4\r\n29,18\r\n42,50\r\n64,21\r\n31,18\r\n39,18\r\n62,58\r\n58,38\r\n54,34\r\n66,27\r\n8,52\r\n47,2\r\n6,15\r\n59,16\r\n2,26\r\n8,22\r\n58,46\r\n32,24\r\n46,1\r\n40,40\r\n30,42\r\n68,37\r\n26,16\r\n50,4\r\n60,56\r\n68,35\r\n70,0\r\n0,68\r\n44,11\r\n23,54\r\n36,67\r\n18,59\r\n48,44\r\n13,16\r\n44,6\r\n48,15\r\n64,3\r\n44,60\r\n28,50\r\n48,2\r\n48,54\r\n28,26\r\n16,10\r\n22,2\r\n70,65\r\n18,27\r\n21,0\r\n16,30\r\n36,42\r\n56,55\r\n8,70\r\n56,28\r\n0,6\r\n8,67\r\n54,51\r\n24,11\r\n26,32\r\n50,11\r\n66,37\r\n70,16\r\n50,32\r\n53,8\r\n40,69\r\n14,22\r\n29,42\r\n41,62\r\n56,17\r\n65,62\r\n60,45\r\n38,24\r\n51,28\r\n30,56\r\n62,20\r\n60,44\r\n13,0\r\n66,6\r\n32,22\r\n55,70\r\n49,22\r\n20,12\r\n69,62\r\n42,44\r\n42,38\r\n61,4\r\n16,2\r\n68,8\r\n25,70\r\n8,5\r\n34,32\r\n29,56\r\n30,20\r\n38,7\r\n46,48\r\n68,45\r\n27,0\r\n40,63\r\n68,14\r\n38,13\r\n16,5\r\n27,44\r\n66,62\r\n46,15\r\n18,69\r\n40,60\r\n14,0\r\n60,34\r\n14,14\r\n37,46\r\n56,24\r\n46,53\r\n66,46\r\n58,55\r\n54,9\r\n64,24\r\n25,10\r\n67,20\r\n44,66\r\n58,26\r\n55,64\r\n64,40\r\n58,52\r\n57,24\r\n68,36\r\n";
        public const string test_input_2 = test_input_1;
        public const string input_2 = input_1;
        private static readonly (int, int)[] directions = new (int, int)[] { (1, 0), (0, -1), (-1, 0), (0, 1) };
        private const int gridLen = 71;
        private const int bytesFallen = 1024;

        private class Node : IEquatable<Node>
        {
            public (int, int) pos;
            public int fCost { get => gCost + hCost; }
            public int gCost;
            public int hCost;
            public Node? parent;
            public Node((int, int) pos, int gCost, int hCost, Node? parent)
            {
                this.pos = pos;
                this.gCost = gCost;
                this.hCost = hCost;
                this.parent = parent;
            }

            public bool Equals(Node? other)
            {
                if (other == null) { throw new ArgumentNullException("other is null"); }
                if (other.pos == pos) { return true; }
                return false;
            }
        }
        public static long Part_1(string input)
        {
            string[] lineStr = input.Split("\r\n", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            char[][] grid = new char[gridLen][];
            List<Node> open = new List<Node>();
            List<Node> closed = new List<Node>();
            (int, int) startPos = (0, 0);
            (int, int) endPos = (gridLen - 1, gridLen - 1);
            for (int x = 0; x < gridLen; x++)
            {
                grid[x] = new char[gridLen];
                for (int y = 0; y < gridLen; y++)
                {
                    grid[x][y] = '.';
                }
            }
            List<(int,int)> obstructed = new List<(int,int)>();
            for (int i = 0; i < bytesFallen; i++)
            {
                Match match = Regex.Match(lineStr[i], @"(\d+),(\d+)");
                int x = int.Parse(match.Groups[1].Value);
                int y = int.Parse(match.Groups[2].Value);
                obstructed.Add((x,y));
                grid[x][y] = '#';
            }

            Node current = new Node(startPos, 0, HCost(startPos, endPos), null);
            while (true)
            {
                closed.Add(current);
                if (current.pos == endPos) { break; }

                for (int i = 0; i < 4; i++)
                {
                    (int, int) nextPos = (current.pos.Item1 + directions[i].Item1, current.pos.Item2 + directions[i].Item2);
                    if (nextPos.Item1 < 0 || nextPos.Item1 >= gridLen) { continue; }
                    if (nextPos.Item2 < 0 || nextPos.Item2 >= gridLen) { continue; }
                    if (grid[nextPos.Item1][nextPos.Item2] == '#') { continue; }
                    Node neighbour = new Node(nextPos, current.gCost + 1, HCost(nextPos, endPos), current);
                    if (closed.Contains(neighbour) == false && open.Contains(neighbour) == false)
                    {
                        open.Add(neighbour);
                    }
                }

                current = GetOpen(ref open);
            }

            List<Node> path = new List<Node>();
            while (true)
            {
                path.Add(current);
                if (current.parent == null) { break; }
                current = current.parent;
            }

            for (int i = 0; i < path.Count; i++)
            {
                grid[path[i].pos.Item1][path[i].pos.Item2] = 'O';
            }
            
            StringBuilder render = new StringBuilder();
            for (int y = 0; y < gridLen; y++)
            {
                for (int x = 0; x < gridLen; x++)
                {
                    render.Append(grid[x][y]);
                }
                render.Append("\r\n");
            }
            Console.WriteLine(render.ToString());
            return path.Count - 1;
        }



        public static long Part_2(string input)
        {
            string[] lineStr = input.Split("\r\n", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            char[][] grid = new char[gridLen][];
            List<Node> open = new List<Node>();
            List<Node> closed = new List<Node>();
            (int, int) startPos = (0, 0);
            (int, int) endPos = (gridLen - 1, gridLen - 1);
            List<(int, int)> obstructed = new List<(int, int)>();
            List<(int, int)> path = new List<(int, int)>();
            for (int x = 0; x < gridLen; x++)
            {
                grid[x] = new char[gridLen];
                for (int y = 0; y < gridLen; y++)
                {
                    grid[x][y] = '.';
                }
            }

            for (int i = 0; i < lineStr.Length; i++)
            {
                obstructed.Clear();
                for (int j = 0; j < i; j++)
                {
                    Match match = Regex.Match(lineStr[j], @"(\d+),(\d+)");
                    int x = int.Parse(match.Groups[1].Value);
                    int y = int.Parse(match.Groups[2].Value);
                    obstructed.Add((x, y));
                    grid[obstructed[j].Item1][obstructed[j].Item2] = '#';
                }
                

                open.Clear();
                closed.Clear();
                Node current = new Node(startPos, 0, HCost(startPos, endPos), null);
                while (true)
                {
                    closed.Add(current);
                    if (current.pos == endPos) { break; }

                    for (int j = 0; j < 4; j++)
                    {
                        (int, int) nextPos = (current.pos.Item1 + directions[j].Item1, current.pos.Item2 + directions[j].Item2);
                        if (nextPos.Item1 < 0 || nextPos.Item1 >= gridLen) { continue; }
                        if (nextPos.Item2 < 0 || nextPos.Item2 >= gridLen) { continue; }
                        if (grid[nextPos.Item1][nextPos.Item2] == '#') { continue; }
                        Node neighbour = new Node(nextPos, current.gCost + 1, HCost(nextPos, endPos), current);
                        if (closed.Contains(neighbour) == false && open.Contains(neighbour) == false)
                        {
                            open.Add(neighbour);
                        }
                    }

                    if (open.Count == 0) 
                    {
                        Console.WriteLine(obstructed[obstructed.Count - 1].Item1 + "," + obstructed[obstructed.Count - 1].Item2);
                        return 0;
                    }
                    current = GetOpen(ref open);
                }

                path.Clear();
                while (true)
                {
                    path.Add(current.pos);
                    if (current.parent == null) { break; }
                    current = current.parent;
                }

                for (int j = i + 1; j < lineStr.Length; j++)
                {
                    Match match = Regex.Match(lineStr[j], @"(\d+),(\d+)");
                    int x = int.Parse(match.Groups[1].Value);
                    int y = int.Parse(match.Groups[2].Value);
                    if (path.Contains((x,y)) == true)
                    {
                        i = j - 1;
                        break;
                    }
                }

            }

            return -1;
        }

        private static int HCost((int, int) pos, (int, int) end)
        {
            return Math.Abs(end.Item1 - pos.Item1) + Math.Abs(end.Item2 - pos.Item2);
        }

        private static Node GetOpen(ref List<Node> open)
        {
            int idx = 0;
            Node current = open[0];
            for (int i = 1; i < open.Count; i++)
            {
                if (open[i].fCost < current.fCost)
                {
                    current = open[i];
                    idx = i;
                }
            }
            open.RemoveAt(idx);
            return current;
        }

    }
}
